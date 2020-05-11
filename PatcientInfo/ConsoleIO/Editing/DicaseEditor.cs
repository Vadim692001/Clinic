using Common.ConsoleIO.Editing;
using PatcientInfo.Data.Interfeces;
using PatcientInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using  Common.ConsoleIO.Controls.Selection;


namespace PatcientInfo.ConsoleIO.Editing
{
    public class DicaseEditor
    {
        private IDateSet dataSet;
        private ICollection<Discase> collection;

        public DicaseEditor(IDateSet dataSet)
        {
            //
            this.dataSet = dataSet;
            collection = dataSet.Discases;
            SetCounter();
        }

        private static int counter = 0;

        private void SetCounter()
        {
            if (collection.Count == 0)
            {
                counter = 0;
            }
            else
            {
                counter = collection.Select(e => e.Id).Max();
            }
        }

        public void Create()
        {
            Console.WriteLine("\tВведіть дані хвороби, що додається:");
            Discase inst = new Discase();
       
            inst.Nazva = EnterAndValidateName();
            inst.DicaseType = SelectDicaseTypeFromList();
            inst.note = Entering.EnterString("Примітка");
          
            inst.Id = ++counter;
            dataSet.Discases.Add(inst);
        }

        private string EnterAndValidateName()
        {
            while (true)
            {
                string name = Entering.EnterString("Назва хвороби");
                if (!collection.Any(e => e.Nazva == name))
                {
                    return name;
                }
                Console.WriteLine(
                    "\tПомилка: у сховищі вже зберігається об'єкт, "
                    + "що представляє хворобу з назвою " + name);
            }
        }

      

        private DicaseType SelectDicaseTypeFromList()
        {
            string[] dicasetypeNames = dataSet.DicaseTypes
                .Where(e => !e.CildsOjects.Any())
                .Select(e => e.Nazva).OrderBy(e => e).ToArray();
            Console.Write(Entering.format, "Вид хвороби");
            SimpleLineSelector selector = new SimpleLineSelector(
                Console.CursorLeft, Console.CursorTop, 37,
                dicasetypeNames);
            selector.Focus();
            Console.WriteLine();
            return dataSet.DicaseTypes
                .FirstOrDefault(e => e.Nazva == selector.Text);
        }
        //----------------------------------------------------

        public void Update(Discase inst)
        {
            Console.WriteLine("Відредагуйте значення атрибутів хвороби {0}",
                inst.Nazva);
            inst.Nazva = Entering.EnterString("Назва", inst.Nazva);
            inst.DicaseType = SelectOrConfirmDicaseType(inst.DicaseType);

            string dicaseName = inst.DicaseType.Nazva;
            dicaseName = Entering.EnterString(
                "Вид хвороби", dicaseName);
            if (dicaseName != inst.DicaseType.Nazva)
            {
                inst.DicaseType = dataSet.DicaseTypes.FirstOrDefault(
                    e => e.Nazva == dicaseName);
            }
           
            inst.note = Entering.EnterString("Примітка", inst.note);
            inst.Descripotion = Entering.EnterString("Опис", inst.Descripotion);
          
        }

        private DicaseType SelectOrConfirmDicaseType(DicaseType current)
        {
            while (true)
            {
                string dicaseName = Entering.EnterString(
                    "Вид хвороби", current.Nazva);
                if (dicaseName == current.Nazva)
                    return current;
                if (dicaseName == null)
                {
                    Console.WriteLine("\tПомилка: потрібно ввести назву "
                        + "наявного у сховищі об'єкта, що представляє "
                        + "вид хвороби, до якого відноситься хвороба");
                    continue;
                }
                DicaseType inst = dataSet.DicaseTypes
                    .FirstOrDefault(e => e.Nazva == dicaseName);
                if (inst != null)
                {
                    return inst;
                }
                Console.WriteLine("\tПомилка: у сховищі відсутній об'єкт, "
                    + "що представляє хвороба з назвою " + dicaseName);
            }
        }

        public void Delete(Discase inst)
        {
            collection.Remove(inst);
            RemoveReferences(inst);
        }

        private void RemoveReferences(Discase inst)
        {
            inst.DicaseType.Discases.Remove(inst);
            inst.DicaseType = null;
        }

     
    }
}