using Common.ConsoleIO.Executing;
using Common.Context.Extensions;
using PatcientInfo.ConsoleIO.Editing;
using PatcientInfo.ConsoleIO.Selection;
using PatcientInfo.Data;
using PatcientInfo.Data.Interfeces;
using PatcientInfo.Entities;
using PatcientInfo.Formatting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacientInfoConsoleEditor
{
    class DicaseManager : CommandManager
    {
        protected override void IniCommandsInfo(
                      out CommandInfo[] commandInfoArray)
        {
            commandInfoArray = new CommandInfo[] {
                new CommandInfo("вийти", null),
               new CommandInfo("видалити всі дані про   хвороби", Clear),
                new CommandInfo("зберегти", Save),
                new CommandInfo("зберегти як текст ...", SaveAsText),
                 new CommandInfo("детально про хворобу ...", OutObject),
                new CommandInfo("додати дані про  хвороби ...", AddObject),
                new CommandInfo("редагувати дані про  хвороби ...", UpdateObject),
                new CommandInfo("видалити дані про  хвороби ...", DeleteObject),
            };
        }
        protected override void PrepareScreen()
        {
            Console.Clear();
            OutObjectsTable();
        }

        private IDateSet dataSet;
        private DicaseEditor editor;

        public DicaseManager(IDateSet dataSet)
        {
            //
            this.dataSet = dataSet;
           editor = new DicaseEditor(dataSet);
        }

        DiscaseTabulator dicaseTabulator
            = new DiscaseTabulator();

        public void OutObjectsTable()
        {
            OutObjectsTable(dataSet.Discases);
        }

        public void OutObjectsTable(IEnumerable<Discase> collection)
        {
            string tableString = dicaseTabulator.CreateTable(
                collection);
            Console.WriteLine(tableString);
        }

        public event EventHandler DataSaving;

        public void Save()
        {
            if (DataSaving != null)
                DataSaving(this, EventArgs.Empty);
        }

        public void SaveAsText()
        {
            Console.Write("Назва файлу: ");
            string fileName = Console.ReadLine();
            string s = dataSet.Discases.ToLineList(
                "Список хвороб та види хвороб");
            File.WriteAllText(fileName, s, Encoding.Unicode);
        }
        DicaseSelector dicaseSelector
            = new DicaseSelector();

        public void OutObject()
        {
            Discase inst = dicaseSelector
                .SelectInstance(dataSet.Discases);
            Console.WriteLine("Дані хвороби {0}:\n{1}", inst.Nazva, inst);
            Console.WriteLine("Натисніть довільну клавішу");
            Console.ReadKey(true);
        }


        public void AddObject()
        {
            editor.Create();
        }

        public void UpdateObject()
        {
            Discase inst = dicaseSelector.SelectInstance(dataSet.Discases);
            editor.Update(inst);
        }

        public void DeleteObject()
        {
            Discase inst = dicaseSelector.SelectInstance(dataSet.Discases);
            editor.Delete(inst);
        }



        private DataContext dataContext;

        public void Clear()
        {
            dataContext.Discases.Clear();
        }

    }
}
