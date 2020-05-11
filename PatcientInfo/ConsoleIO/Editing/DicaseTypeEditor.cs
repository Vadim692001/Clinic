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
  public  class DicaseTypeEditor
    {
        private IDateSet dataSet;
        private ICollection<DicaseType> collection;

        public DicaseTypeEditor(IDateSet dataSet)
        {
            //
            this.dataSet = dataSet;
            collection = dataSet.DicaseTypes;
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
            DicaseType inst = new DicaseType();
            Console.WriteLine("\tВведіть дані виду хвороби, що додається:");
            inst = new DicaseType();

            inst.Nazva = EnterAndValidateName();
            inst.Parent = SelectParent();

            inst.Descripotion = Entering.EnterString("Опис");
            inst.Id = ++counter;
            collection.Add(inst);
            inst.Parent.CildsOjects.Add(inst);
        }

        private string EnterAndValidateName()
        {
            while (true)
            {
                string name = Entering.EnterString("Назва виду хвороби");
                if (!collection.Any(e => e.Nazva == name))
                {
                    return name;
                }
                Console.WriteLine("\tПомилка: у сховищі вже зберігається об'єкт, "
                    + "що представляє вид хвороби з назвою " + name);
            }
        }

        private DicaseType SelectParent()
        {
            while (true)
            {
                string parentName = Entering.EnterStringOrNull(
                    "Хвороба вищого рівня");
                if (parentName == null)
                {
                    return null;
                }
                DicaseType inst = collection.FirstOrDefault(e => e.Nazva == parentName);
                if (inst != null)
                {
                    return inst;
                }
                Console.WriteLine("\tПомилка: у сховищі відсутній об'єкт, "
                    + "що представляє вид хвороби з назвою " + parentName);
            }
        }

        public void Update(DicaseType inst)
        {
            Console.WriteLine("Відредагуйте дані виду хвороби {0}", inst.Nazva);
            inst.Nazva = Entering.EnterString("Назва", inst.Nazva);
        
            inst.Parent = SelectOrConfirmParent(inst.Parent);

            inst.Descripotion = Entering.EnterString("Опис", inst.Descripotion);
        }

        private DicaseType SelectOrConfirmParent(DicaseType current)
        {
            while (true)
            {
                string dicasetypeName = Entering.EnterString(
                    "Вид хвороби", current.Nazva);
                if (dicasetypeName == current.Nazva)
                    return current;
                if (string.IsNullOrEmpty(dicasetypeName))
                {
                    return null;
                }
                DicaseType inst = collection
                    .FirstOrDefault(e => e.Nazva == dicasetypeName);
                if (inst != null)
                {
                    return inst;
                }
                Console.WriteLine("\tПомилка: у сховищі відсутній об'єкт, "
                    + "що представляє вид хвороби з назвою " + dicasetypeName);
            }
        }

        public void Delete(DicaseType inst)
        {
            if (RemovingIsValid(inst))
            {
                collection.Remove(inst);
            }
     
        }

        private void RemoveReferences(DicaseType inst)
        {
            foreach (Discase dicase in inst.Discases)
            {
                dicase.DicaseType = null;
            }
            inst.Discases.Clear(); 
            foreach (DicaseType dicasetype in inst.CildsOjects)
            {
                dicasetype.Parent = null;
            }
            inst.CildsOjects.Clear(); 
        }

        private bool RemovingIsValid(DicaseType inst)
        {
          
            if (inst.Discases.Count > 0)
            {
                Console.WriteLine("\tПомилка видалення: у сховищі "
                    + "є об'єкти, що представляють пацієнти, які "
                    + "хворіють видом хвороби з назвою " + inst.Nazva);
                return false;
            }
         
            if (inst.CildsOjects.Count > 0)
            {
                Console.WriteLine("\tПомилка видалення: у сховищі є "
                    + "об'єкти, що представляю вид хвороби, "
                    + "які розташовані у хворобах "
                    + "хвороба з назвою " + inst.Nazva);
                return false;
            }
            return true;
        }
    }
}
