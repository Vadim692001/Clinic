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
    class DicaseTypeManager : CommandManager
    {
         protected override void IniCommandsInfo(
                    out CommandInfo[] commandInfoArray)
            {
                commandInfoArray = new CommandInfo[] {
                new CommandInfo("вийти", null),
               new CommandInfo("видалити всі дані про види  хвороби", Clear),
                new CommandInfo("зберегти", Save),
                new CommandInfo("зберегти як текст ...", SaveAsText),
                new CommandInfo("додати дані про вид хвороби ...", CreateObject),
                new CommandInfo("редагувати дані про вид хвороби ...", UpdateObject),
                new CommandInfo("видалити дані про вид хвороби ...", DeleteObject),
            };
            }
        protected override void PrepareScreen()
        {
            Console.Clear();
            OutObjectsTable();
        }

        private IDateSet dataSet;
        private DicaseTypeEditor editor;

        public DicaseTypeManager(IDateSet dataSet)
        {
            //
            this.dataSet = dataSet;
            this.editor = new DicaseTypeEditor(dataSet);
        }

        DicaseTypeTabulator dicasetypeTabulator
            = new DicaseTypeTabulator();

        private void OutObjectsTable()
        {
            string tableString = dicasetypeTabulator
                .CreateTable(dataSet.DicaseTypes);
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
            string s = dataSet.DicaseTypes.ToLineList(
                "Список макрогеографічних та географічних регіонів");
            File.WriteAllText(fileName, s, Encoding.Unicode);
        }

        DicaseTypeSelector dicasetypeSelector
            = new DicaseTypeSelector();

        public void OutObject()
        {
            DicaseType inst = dicasetypeSelector
                .SelectInstance(dataSet.DicaseTypes);
            Console.WriteLine("Дані регіону {0}:\n{1}",
                inst.Nazva, inst);
            Console.WriteLine("Натисніть довільну клавішу");
            Console.ReadKey(true);
        }


        public void CreateObject()
        {
            editor.Create();
       
        }

        public void UpdateObject()
        {
            DicaseType inst = dicasetypeSelector.SelectInstance(
                dataSet.DicaseTypes);
            editor.Update(inst);
        }

        public void DeleteObject()
        {
            DicaseType inst = dicasetypeSelector.SelectInstance(
                dataSet.DicaseTypes);
            editor.Delete(inst);
         
        }

      

        private DataContext dataContext;
       
        public void Clear()
        {
            dataContext.Discases.Clear();
        }

      
    }
}
