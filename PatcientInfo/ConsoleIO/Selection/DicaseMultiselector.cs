using PatcientInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatcientInfo.ConsoleIO.Selection
{
 public   class DicaseMultiselector
    {
        delegate void Filter(
             ref IEnumerable<Discase> collection);

        Filter emptyFilter = delegate { };

        enum FilterId { ByName, ByDicaseType, ByDescripotion, None };

        Filter[] filters = new Filter[(int)FilterId.None];

        public DicaseMultiselector()
        {
            InitializeFilters();
            IniFiltersInfo();
        }

        private void InitializeFilters()
        {
            for (int i = 0; i < filters.Length; i++)
            {
                filters[i] = emptyFilter;
            }
        }

        public IEnumerable<Discase> ApplyFilters(
                IEnumerable<Discase> initialCollection)
        {
            IEnumerable<Discase> selectedInstances
                = initialCollection;
            foreach (Filter filter in filters)
            {
                filter(ref selectedInstances);
            }
            return selectedInstances;
        }

        class FilterInfo
        {
            public FilterId filterId;
            public string name;
            public Filter filter;
            public Action enterCommand;

            public FilterInfo(FilterId filterId,
                    string name, Filter filter,
                    Action enterCommand)
            {
                this.filterId = filterId;
                this.name = name;
                this.filter = filter;
                this.enterCommand = enterCommand;
            }
        };

        FilterInfo[] filtersInfo;

        private void IniFiltersInfo()
        {
            filtersInfo = new FilterInfo[] {
                new FilterInfo(FilterId.None,
                    "відмінити вибір команди", null, null),
                new FilterInfo(FilterId.None,
                    "видалити усі фільтри",
                    null, InitializeFilters),
                new FilterInfo(FilterId.ByName,
                    "назва починається з ...",
                    NameStartsWith, EnterNameStart),
                new FilterInfo(FilterId.ByName,
                    "назва містить ...",
                    NameContains, EnterNameSubstring),
                new FilterInfo(FilterId.ByName,
                    "відмінити фільтрацію за назвою",
                    emptyFilter, null),
                new FilterInfo(FilterId.ByDicaseType,
                    "назва хвороби вищого рівня "
                        + "починається з ...",
                    ParentNameStartsWith, EnterParentNameStart),
                new FilterInfo(FilterId.ByDicaseType,
                    "є вершиною ієрархії хвороби",
                    IsTopLevelRegion, null),
                new FilterInfo(FilterId.ByDicaseType,
                    "відмінити фільтрацію за назвою "
                        + "хвороби вищого рівня",
                    emptyFilter, null),

            };
        }


        public void SelectFilters()
        {
            ShowFiltersMenu();
            int number;
            number = EnterFilterNumber();
            FilterInfo filterInfo =
                filtersInfo[number];
            if (filterInfo.filterId != FilterId.None)
            {
                filters[(int)filterInfo.filterId]
                    = filterInfo.filter;
            }
            if (filterInfo.enterCommand != null)
                filterInfo.enterCommand();
        }

        private void ShowFiltersMenu()
        {
            Console.WriteLine("\n  Перелік фільтрів:\n");
            for (int i = 0;
                i < filtersInfo.Length; i++)
            {
                Console.WriteLine("\t{0,2} - {1}",
                    i, filtersInfo[i].name);
            }
        }

        //
        private int EnterFilterNumber()
        {
            int number;
            while (true)
            {
                Console.Write(
                    "\n  Введіть номер команди фільтрації: ");
                if (int.TryParse(Console.ReadLine(), out number))
                {
                    if (number >= 0 &&
                        number < filtersInfo.Length)
                        break;
                    else
                        Console.WriteLine("\tНемає команди "
                            + "фільтрації з введеним номером!");
                }
                else
                {
                    Console.WriteLine("\tПомилка введення "
                            + "числового значення!");
                    continue;
                }
            }
            return number;
        }
        #region filtering by name

        string nameStart = "";

        private void EnterNameStart()
        {
            Console.Write("\tПочаток назви: ");
            nameStart = Console.ReadLine();
        }

        private void NameStartsWith(
            ref IEnumerable<Discase> collection)
        {
            collection = collection
                .Where(e => e.Nazva.StartsWith(nameStart,
                StringComparison.InvariantCultureIgnoreCase));
        }

        private string nameSubstring = "";

        private void EnterNameSubstring()
        {
            Console.Write("\tФрагмент назви: ");
            nameSubstring = Console.ReadLine();
        }

        private void NameContains(
            ref IEnumerable<Discase> collection)
        {
            collection = collection
                .Where(e => e.Nazva.Contains(nameSubstring));
        }
        #endregion

        #region filtering by parent name

        private string parentNameStart = "";

        private void EnterParentNameStart()
        {
            Console.Write("\tПочаток назви хвороби вищого рівня: ");
            parentNameStart = Console.ReadLine();
        }

        private void ParentNameStartsWith(
            ref IEnumerable<Discase> collection)
        {
            collection = collection
                .Where(e => !string.IsNullOrEmpty(e.DicaseType.Nazva)
                && e.DicaseType.Nazva.StartsWith(parentNameStart,
                StringComparison.InvariantCultureIgnoreCase));
        }

        private void IsTopLevelRegion(
           ref IEnumerable<Discase> collection)
        {
            collection = collection.Where(
                e => string.IsNullOrEmpty(e.DicaseType.Nazva));
        }

        #endregion
    }
}
