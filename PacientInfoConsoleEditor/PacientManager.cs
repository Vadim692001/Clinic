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
    class PacientManager : CommandManager
    {
   
        protected override void IniCommandsInfo(
                      out CommandInfo[] commandInfoArray)
        {
            commandInfoArray = new CommandInfo[] {
                new CommandInfo("вийти", null),
               new CommandInfo("видалити всі дані про пацієнтів", Clear),
                new CommandInfo("зберегти", Save),
                new CommandInfo("зберегти як текст ...", SaveAsText),
                 new CommandInfo("детально про пацієнтів ...", OutObject),
                new CommandInfo("додати   пацієнта ...", AddObject),
                new CommandInfo("редагувати дані про  пацієнта ...", UpdateObject),
                new CommandInfo("видалити дані про  пацієнта ...", DeleteObject),
            };
        }
        protected override void PrepareScreen()
        {
            Console.Clear();
            OutObjectsTable();
        }

        private IDateSet dataSet;
        private PacientEditor editor;

        public PacientManager(IDateSet dataSet)
        {
            //
            this.dataSet = dataSet;
            editor = new PacientEditor(dataSet);
        }

        PatcientTabulator patcientTabulator
            = new PatcientTabulator();

        public void OutObjectsTable()
        {
            string tableString = patcientTabulator.CreateTable(
                dataSet.Patcients);
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
                "Список пацієнтів  та хвороб ");
            File.WriteAllText(fileName, s, Encoding.Unicode);
        }
        PacientSelector patcientSelector
            = new PacientSelector();

        public void OutObject()
        {
            Patcient inst = patcientSelector
                .SelectInstance(dataSet.Patcients);
            Console.WriteLine("Дані пацієнтів {0}:\n{1}", inst.Sorname, inst);
            Console.WriteLine("Натисніть довільну клавішу");
            Console.ReadKey(true);
        }


        public void AddObject()
        {
            editor.Create();
        }

        public void UpdateObject()
        {
            Patcient inst = patcientSelector.SelectInstance(dataSet.Patcients);
            editor.Update(inst);
        }

        public void DeleteObject()
        {
            Patcient inst = patcientSelector.SelectInstance(dataSet.Patcients);
            editor.Delete(inst);
        }



        private DataContext dataContext;

        public void Clear()
        {
            dataContext.Discases.Clear();
        }


    }
}
