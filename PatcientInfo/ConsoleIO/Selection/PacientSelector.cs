using PatcientInfo.Entities;
using PatcientInfo.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatcientInfo.ConsoleIO.Selection
{
   public class PacientSelector
    {
        PatcientTabulator patcientTabulator = new PatcientTabulator();

        public Patcient SelectInstance(IEnumerable<Patcient> collection)
        {
            string tableString = patcientTabulator
                .CreateTable(collection);
            Console.WriteLine(tableString);
            if (patcientTabulator.UseId)
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
