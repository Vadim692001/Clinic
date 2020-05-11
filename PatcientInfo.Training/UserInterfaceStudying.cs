using Common.Context.Extensions;
using PatcientInfo.Data;
using PatcientInfo.Data.Extensions;
using System;
using PatcientInfo.Formatting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatcientInfo.ConsoleIO.Selection;
using PatcientInfo.Entities;

namespace PatcientInfo.Training
{
    class UserInterfaceStudying
    {
        public static void Run()
        {
            Console.WriteLine("==== UserInterfaceStudying ====");
          //  StudyTabulators();
            StudyObjectSelectors();
        }

        private static void StudyTabulators()
        {
            Console.WriteLine(" --- StudyTabulators ---");

            DateSet dataSet = new DateSet();
            dataSet.CreateObjectss();

            var dicasetepe = dataSet.DicaseTypes.OrderBy(e => e.Nazva);
            Console.WriteLine("dicasetepe:\n"
                + dicasetepe.ToLineList("dicasetepe\""));

            DicaseTypeTabulator dicasetypeTabulator
                = new DicaseTypeTabulator();
            Console.WriteLine("DicaseTypeTabulator.CreateTable():\n"
                + dicasetypeTabulator.CreateTable(dicasetepe));

            dicasetypeTabulator.UseId = false;
            Console.WriteLine("DicaseTypeTabulator.CreateTable():\n"
                + dicasetypeTabulator.CreateTable(dicasetepe));
            var dicase = dataSet.Discases.OrderBy(e => e.Nazva);
            Console.WriteLine("dicase:\n"
                + dicase.ToLineList("dicase\""));

            DiscaseTabulator dicaseTabulator
                = new DiscaseTabulator();
            Console.WriteLine("DiscaseTabulator.CreateTable():\n"
                + dicaseTabulator.CreateTable(dicase));

            dicaseTabulator.UseId = false;
            Console.WriteLine("DiscaseTabulator.CreateTable():\n"
                + dicasetypeTabulator.CreateTable(dicasetepe));

            var patcient = dataSet.Patcients.OrderBy(e => e.Sorname);
            Console.WriteLine("patcient:\n" + patcient.ToLineList("patcient"));

            PatcientTabulator patcientsTabulator = new PatcientTabulator();
            Console.WriteLine("countriesTabulator.CreateTable():\n"
                + patcientsTabulator.CreateTable(patcient));

            patcientsTabulator.UseId = false;
            Console.WriteLine("patcientsTabulator.CreateTable():\n"
                + patcientsTabulator.CreateTable(patcient));
            Console.ReadKey();
        }
        private static void StudyObjectSelectors()
        {
            Console.WriteLine(" --- StudyObjectSelectors ---");

            DateSet dataSet = new DateSet();
            dataSet.CreateObjectss();

            char ch = '\0';
            do
            {
                DicaseTypeSelector dicasetypeTabulator = new DicaseTypeSelector();
                DicaseType inst1 = dicasetypeTabulator.SelectInstance(dataSet.DicaseTypes);
                Console.WriteLine("inst1:\n" + inst1);

                Console.WriteLine("\tПродовжити (<Enter>): ");
                ConsoleKeyInfo cki = Console.ReadKey();
                ch = cki.KeyChar;
            } while (ch == '\r');
            do
            {
                DicaseSelector dicaseTabulator = new DicaseSelector();
                Discase inst1 = dicaseTabulator.SelectInstance(dataSet.Discases);
                Console.WriteLine("inst1:\n" + inst1);

                Console.WriteLine("\tПродовжити (<Enter>): ");
                ConsoleKeyInfo cki = Console.ReadKey();
                ch = cki.KeyChar;
            } while (ch == '\r');

            do
            {
                PacientSelector patcientTabulator = new PacientSelector();
                Patcient inst2 = patcientTabulator.SelectInstance(dataSet.Patcients);
                Console.WriteLine("inst2:\n" + inst2);

                Console.WriteLine("\tПродовжити (<Enter>): ");
                ConsoleKeyInfo cki = Console.ReadKey();
                ch = cki.KeyChar;
            } while (ch == '\r');
            Console.ReadKey();
        }


    }

}
