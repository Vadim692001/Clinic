using PatcientInfo.Entities;
using PatcientInfo.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatcientInfo.ConsoleIO.Selection
{
   public class DicaseTypeSelector
    {
        DicaseTypeTabulator dicasetypeTabulator = new DicaseTypeTabulator();

        public DicaseType SelectInstance(IEnumerable<DicaseType> collection)
        {
            string tableString = dicasetypeTabulator
                .CreateTable(collection);
            Console.WriteLine(tableString);
            if (dicasetypeTabulator.UseId)
            {
                int id = EnterId();
                return collection.First(e => e.Id == id);
            }
            else
            {
                int index = EnterIndex();
                return collection.ElementAt(index);
            }
        }

        private int EnterId()
        {
            Console.Write("\tномер об'єкта: ");
            int number = int.Parse(Console.ReadLine());
            return number;
        }

        private int EnterIndex()
        {
            Console.Write("\tномер об'єкта: ");
            int number = int.Parse(Console.ReadLine());
            return number - 1;
        }

    }
}
