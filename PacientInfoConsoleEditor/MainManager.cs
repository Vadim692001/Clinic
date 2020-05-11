using Common.ConsoleIO.Executing;
using PatcientInfo.ConsoleIO.Selection;
using PatcientInfo.Data;
using PatcientInfo.Data.Extensions;
using PatcientInfo.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacientInfoConsoleEditor
{
    class MainManager : CommandManager
    {
        protected override void IniCommandsInfo(out CommandInfo[] commandsInfo)
        {
            IniCommandInfoDictionary();
            commandsInfo = this.commandsInfo.Values.ToArray();
        }

        new Dictionary<string, CommandInfo> commandsInfo;

        private void IniCommandInfoDictionary()
        {
            commandsInfo = new Dictionary<string, CommandInfo>() {
                { "Exit", new CommandInfo("завершити роботу", null) },
                { "Testing", new CommandInfo("створити тестові дані",
                    CreateTestingData) },
                { "ShowAsText", new CommandInfo("показати як текст ...",
                    ShowAsText) },
                { "Delete", new CommandInfo("видалити всі дані", Clear) },
               // { "Save", new CommandInfo("зберегти", Save) },
                { "IOFormat", new CommandInfo("змінити формат збереження ►",
                    SelectIOController) },
                { "SaveAsText", new CommandInfo("зберегти як текст ...",
                    SaveAsText) },
                { "DicaseTypeEditing", new CommandInfo("редагування даних про види хвороб ►",
                    RunDicaseTypeManagerEditing) },
                  { "DicaseEditing", new CommandInfo("редагування даних про хвороби ►",
                    RunDicaseManagerEditing) },
                { "PatcientEditing", new CommandInfo("редагування даних про пацієнтів ►",
                    RunPacientManagerEditing) },
                  { "DicaseTypeSelection", new CommandInfo("відбір виду хвороб ►",
                    DicaseTypeSelectorEding) },
                   { "DicaseSelection", new CommandInfo("відбір  хвороб ►",
                    DicaseSelectorEding) },
                { "PatcientsSelection", new CommandInfo("відбір пацієнтів ►",
                    PatcientsSelection) },
            };
        }

        private void SetSaveCommandName()
        {
            commandsInfo["Save"].name = string.Format(
                "зберегти в форматі {0} ({1})",
                dataContext.FileIOController.FileTypeCaption,
                dataContext.FileIOController.FileExtension);
        }

        protected override void PrepareScreen()
        {
            Console.Clear();
            ApplyFilters();
            OutStatistics();
            dicaseManager.OutObjectsTable(selectedDataSet.Discases);


        }

        private DataContext dataContext;
        private FileIOControllersSelector fileIOControllersSelector;
        private DicaseTypeManager dicasetypeManager;
        private DicaseManager dicaseManager;
        private PacientManager pacientManager;

        private DicaseTypeMultiselector dicasetypeMultiselector;
        private DicaseMultiselector dicaseMultiselector;
        private PatcientMultiselector patcientMultiselector;
        private ReadOnlyDataSet selectedDataSet = new ReadOnlyDataSet();
        private void ApplyFilters()
        {
            selectedDataSet.DicaseTypes = dicasetypeMultiselector.ApplyFilters(dataContext.DicaseTypes);
            selectedDataSet.Discases = dicaseMultiselector.ApplyFilters(dataContext.Discases);
            selectedDataSet.Patcients = patcientMultiselector
                .ApplyFilters(dataContext.Patcients
                .Where(e => selectedDataSet.Discases
                .Contains(e.Dicase)));
        }
        public MainManager(DataContext dataContext,
            FileIOControllersSelector fileIOControllersSelector)
        {
            //
            this.dataContext = dataContext;
            this.fileIOControllersSelector = fileIOControllersSelector;
            SetSaveCommandName();
            dicasetypeManager = new DicaseTypeManager(dataContext);
            dicaseManager = new DicaseManager(dataContext);
            pacientManager = new PacientManager(dataContext);
            dicasetypeMultiselector = new DicaseTypeMultiselector();
            dicaseMultiselector = new DicaseMultiselector();
            patcientMultiselector = new PatcientMultiselector();

            dataContext.Load();

        }

        private void OutStatistics()
        {
            Console.WriteLine(
                "Інформація про об'єкти ПО \"Хвороби\"");
            Console.WriteLine("\n  Представлено:\n"
                + "{0,7} Види хвороби\n"
                + "{1,7} Хвороби\n"
                + "{2,7} Пацієнти\n"
                + "  Відібрано:\n"
                + "{3,7} Види хвороби\n"
                + "{4,7} Хвороби\n"
                + "{5,7} Пацієнти\n",
                dataContext.DicaseTypes.Count,
                dataContext.Discases.Count,
                dataContext.Patcients.Count,
                selectedDataSet.DicaseTypes.Count(),
                selectedDataSet.Discases.Count(),
                selectedDataSet.Patcients.Count()
                );

        }

        private void SaveAsText()
        {
            dataContext.SaveAsText();
        }

        private void CreateTestingData()
        {
            dataContext.CreateTestingData();
        }

        private void ShowAsText()
        {
            Console.WriteLine();
            Console.WriteLine(dataContext);
            Console.ReadKey(true);
        }

        private void Clear()
        {
            dataContext.Clear();
        }

        //private void Save()
        //{
        //    dataContext.Save();
        //}


        private void SelectIOController()
        {
            fileIOControllersSelector.Select();
            dataContext.FileIOController = fileIOControllersSelector.Controller;
            SetSaveCommandName();
        }

        private void RunDicaseTypeManagerEditing()
        {
            dicasetypeManager.Run();
        }

        private void RunDicaseManagerEditing()
        {
            dicaseManager.Run();
        }
        private void RunPacientManagerEditing()
        {
            pacientManager.Run();
        }

        private void DicaseTypeSelectorEding()
        {
            dicasetypeMultiselector.SelectFilters();
        }
        private void DicaseSelectorEding()
        {
            dicaseMultiselector.SelectFilters();
            //ApplyFilters();
            //dicaseManager.OutObjectsTable(selectedDataSet.Discases);
            //Console.ReadKey(true);
        }
        private void PatcientsSelection()
        {
            patcientMultiselector.SelectFilters();
        }
    }
}
