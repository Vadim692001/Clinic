using PatcientInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatcientInfo.ConsoleIO.Selection
{
 public   class PatcientMultiselector
    {
        delegate void Filter(ref IEnumerable<Patcient> collection);

        Filter emptyFilter = delegate { };

        enum FilterId { BySorname, ByDicaseType, ByMedical_card, ByDate, ByDoctor, Bynumber_Chamber, None };

        Filter[] filters = new Filter[(int)FilterId.None];

        public PatcientMultiselector()
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

        public IEnumerable<Patcient> ApplyFilters(
                IEnumerable<Patcient> initialCollection)
        {
            IEnumerable<Patcient> selectedInstances = initialCollection;
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

            public FilterInfo(FilterId filterId, string name, Filter filter,
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
                new FilterInfo(FilterId.None, "відмінити вибір команди", null, null),
                new FilterInfo(FilterId.None, "видалити усі фільтри",
                    null, InitializeFilters),

                new FilterInfo(FilterId.BySorname, "назва починається з ...",
                    NameStartsWith, EnterNameStart),
                new FilterInfo(FilterId.BySorname, "назва містить ...",
                    NamePatciens, EnterNameSubstring),
                new FilterInfo(FilterId.BySorname, "назва пацієнта починається з ...",
                    DicaseNameStartsWith, EnterDicaseNameStart),
                new FilterInfo(FilterId.ByDicaseType, "назва хвороби містить ...",
                 NameStartsWith, EnterNameStart),
                new FilterInfo(FilterId.ByDicaseType, "назва хвороби починається з ...",
                    NameStartsWith, EnterNameStart),
                new FilterInfo(FilterId.ByDicaseType, "назва хвороби містить ...",
                    NamePatciens, EnterNameSubstring),
       
      
                new FilterInfo(FilterId.ByMedical_card, "Лікарь починається з ...",  Doctorq, EnterDoctort),
               
                new FilterInfo(FilterId.ByMedical_card, "Палата невідома", number_Chamber , null),
              
                new FilterInfo(FilterId.ByDate, "Медична кардка яка є мінімальною",
                    Medical_cardIsMin , null),
                new FilterInfo(FilterId.ByDate, "Медична кардка яка є максимальною",
                    Medical_cardIsMaximal , null),
          

                new FilterInfo(FilterId.BySorname, "відмінити фільтрацію за назвою",
                    emptyFilter, null),
                new FilterInfo(FilterId.ByDicaseType,
                    "відмінити фільтрацію за назвою пацієнта",
                    emptyFilter, null),
           
            };
        }

        public void SelectFilters()
        {
            ShowFiltersMenu();
            int number;
            number = EnterFilterNumber();
            FilterInfo filterInfo = filtersInfo[number];
            if (filterInfo.filterId != FilterId.None)
            {
                filters[(int)filterInfo.filterId] = filterInfo.filter;
            }
            if (filterInfo.enterCommand != null)
                filterInfo.enterCommand();
        }

        private void ShowFiltersMenu()
        {
            Console.WriteLine("\n  Перелік фільтрів:\n");
            for (int i = 0; i < filtersInfo.Length; i++)
            {
                Console.WriteLine("\t{0,2} - {1}", i, filtersInfo[i].name);
            }
        }

        //
        private int EnterFilterNumber()
        {
            int number;
            while (true)
            {
                Console.Write("\n  Введіть номер команди фільтрації: ");
                if (int.TryParse(Console.ReadLine(), out number))
                {
                    if (number >= 0 && number < filtersInfo.Length)
                        break;
                    else
                        Console.WriteLine("\tНемає команди фільтрації з введеним номером!");
                }
                else
                {
                    Console.WriteLine("\tПомилка введення числового значення!");
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

        private void NameStartsWith(ref IEnumerable<Patcient> collection)
        {
            collection = collection.Where(e => e.Sorname.StartsWith(nameStart,
                StringComparison.InvariantCultureIgnoreCase));
        }

        private string nameSubstring = "";

        private void EnterNameSubstring()
        {
            Console.Write("\tФрагмент назви: ");
            nameSubstring = Console.ReadLine();
        }

        private void NamePatciens(ref IEnumerable<Patcient> collection)
        {
            collection = collection.Where(e => e.Sorname.Contains(nameSubstring));
        }

        #endregion

        #region filtering by macroregion name

        private string dicaseNameStart = "";

        private void EnterDicaseNameStart()
        {
            Console.Write("\tПочаток назви регіону: ");
            dicaseNameStart = Console.ReadLine();
        }

        private void DicaseNameStartsWith(ref IEnumerable<Patcient> collection)
        {
            collection = collection.Where(e => !string.IsNullOrEmpty(e.Dicase.Nazva)
                && e.Dicase.Nazva.StartsWith(dicaseNameStart,
                StringComparison.InvariantCultureIgnoreCase));
        }

        private string dicaseNameSubstring = "";

        private void EnterDicaseNameSubstring()
        {
            Console.Write("\tФрагмент назви регіону: ");
            dicaseNameSubstring = Console.ReadLine();
        }

        private void DicaseNameContains(ref IEnumerable<Patcient> collection)
        {
            collection = collection.Where(
                e => e.Dicase.Nazva.Contains(dicaseNameSubstring));
        }

        #endregion

        #region 2

        private string Doctor = "";

        private void EnterDoctort()
        {
            Console.Write("\tПочаток Doctor ");
            Doctor = Console.ReadLine();
        }

        private void Doctorq(ref IEnumerable<Patcient> collection)
        {
            collection = collection.Where(e => !string.IsNullOrEmpty(e.Doctor)
                && e.Doctor.StartsWith(Doctor,
                StringComparison.InvariantCultureIgnoreCase));
        }

        private string q = "";

     

      

        private void number_Chamber(ref IEnumerable<Patcient> collection)
        {
            collection = collection.Where(e => string.IsNullOrEmpty(e.number_Chamber));
        }

        #endregion

        #region 3

        

    

   

        private void Medical_cardIsMin(ref IEnumerable<Patcient> collection)
        {
            string minArea = collection.Select(e1 => e1.Medical_card).Min();
            collection = collection.Where(e => e.Medical_card == minArea);
        }

        private void Medical_cardIsMaximal(ref IEnumerable<Patcient> collection)
        {
            string maxArea = collection.Select(e1 => e1.Medical_card).Max();
            collection = collection.Where(e => e.Medical_card == maxArea);
        }
        

        #endregion
    }
}
