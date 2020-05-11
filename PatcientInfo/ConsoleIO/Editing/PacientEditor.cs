using Common.ConsoleIO.Controls.Selection;
using Common.ConsoleIO.Editing;
using PatcientInfo.Data.Interfeces;
using PatcientInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatcientInfo.ConsoleIO.Editing
{
  public  class PacientEditor
    {
        private IDateSet dataSet;
        private ICollection<Patcient> collection;

        public PacientEditor(IDateSet dataSet)
        {
            //
            this.dataSet = dataSet;
            collection = dataSet.Patcients;
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
            Console.WriteLine("\tВведіть дані пацієнта, що додається:");
            Patcient inst = new Patcient();

            inst.Sorname = EnterAndValidateName();
            inst.Dicase = SelectDicaseTypeFromList();
            inst.Doctor = Entering.EnterString("Лікарь");
            inst.Date = new DateTime(2013, 09, 23);
            inst.Medical_card = Entering.EnterString("Медична кардка");
            inst.number_Chamber = Entering.EnterString("Палата");
            inst.Id = ++counter;
            dataSet.Patcients.Add(inst);
        }

        private string EnterAndValidateName()
        {
            while (true)
            {
                string name = Entering.EnterString("Прізвище");
                if (!collection.Any(e => e.Sorname == name))
                {
                    return name;
                }
                Console.WriteLine(
                    "\tПомилка: у сховищі вже зберігається об'єкт, "
                    + "що представляє прізвище з назвою " + name);
            }
        }
       
        private Discase SelectDicaseTypeFromList()
        {
            string[] dicasetypeNames = dataSet.Discases
                .Where(e => !e.Patcients.Any())
                .Select(e => e.Nazva).OrderBy(e => e).ToArray();
            Console.Write(Entering.format, " хвороба");
            SimpleLineSelector selector = new SimpleLineSelector(
                Console.CursorLeft, Console.CursorTop, 37,
                dicasetypeNames);
            selector.Focus();
            Console.WriteLine();
            return dataSet.Discases
                .FirstOrDefault(e => e.Nazva == selector.Text);
        }
        //----------------------------------------------------

        public void Update(Patcient inst)
        {
            Console.WriteLine("Відредагуйте значення атрибутів пацієнта {0}",
                inst.Sorname);
            inst.Sorname = Entering.EnterString("Назва", inst.Sorname);
            inst.Dicase = SelectOrConfirmDiscase(inst.Dicase);

            string patcientName = inst.Dicase.Nazva;
            patcientName = Entering.EnterString(
                " хвороба", patcientName);
            if (patcientName != inst.Dicase.Nazva)
            {
                inst.Dicase = dataSet.Discases.FirstOrDefault(
                    e => e.Nazva == patcientName);
            }

           

        }

        private Discase SelectOrConfirmDiscase(Discase current)
        {
            while (true)
            {
                string patcientName = Entering.EnterString(
                    "хвороба", current.Nazva);
                if (patcientName == current.Nazva)
                    return current;
                if (patcientName == null)
                {
                    Console.WriteLine("\tПомилка: потрібно ввести назву "
                        + "наявного у сховищі об'єкта, що представляє "
                        + "хворобу, до якого відноситься пацієнт");
                    continue;
                }
                Discase inst = dataSet.Discases
                    .FirstOrDefault(e => e.Nazva == patcientName);
                if (inst != null)
                {
                    return inst;
                }
                Console.WriteLine("\tПомилка: у сховищі відсутній об'єкт, "
                    + "що представляє хворобу з назвою " + patcientName);
            }
        }

        public void Delete(Patcient inst)
        {
            collection.Remove(inst);
            RemoveReferences(inst);
        }
        private void RemoveReferences(Patcient inst)
        {
            inst.Dicase.Patcients.Remove(inst);
            inst.Dicase = null;
        }
       

    }
}
