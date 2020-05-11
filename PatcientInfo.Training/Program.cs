using Common.ConsoleIO;
using Common.Context.Extensions;
using PatcientInfo.Entities;
using PatcientInfo.Training;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatcientInfo

{
    class Program
    {
        static void Main(string[] args)
        {
            Common.ConsoleIO.Settings.SetConsoleParam();
          //  DataContextStudying.Run();
           // StudyEntities();
            FileIOStudying.Run();
           // UserInterfaceStudying.Run();

        }

        static void Q()
        {
            int[] arr1 = { 4, -2, 0, 5, 3 };
            List<int> list1 = new List<int> { 8, 0, 2, 45, 465, 67, 878889, 090, 34, 565 };

            IEnumerable<int> collection = arr1;
            OutCollection(collection);

            collection = list1;
            OutCollection(collection);
            Console.ReadKey();
        }


        private static void OutCollection(
            IEnumerable<int> collection,
            string separator = " ")
        {
            IEnumerator<int> enumerator =
                collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.Write(enumerator.Current
                    + separator);
            }
            Console.WriteLine();
        }

        private static void StudyEntities()
        {
            Console.WriteLine(" --- StudyEntities ---");

            DicaseType inst1 = new DicaseType("Хвороба", null)
            { Id = 1 };
            Console.WriteLine("inst1:\t" + inst1);
            DicaseType inst2 = new DicaseType("Інфекційна", inst1)
            { Id = 2 };
            Console.WriteLine("inst2:\t" + inst2);
            DicaseType inst3 = new DicaseType("Краснуха", inst2)
            { Id = 3 };
            Console.WriteLine("inst3:\t" + inst3);
            DicaseType inst4 = new DicaseType(
               "  без ускладнення", inst3)
            { Id = 4 };
            Console.WriteLine("inst4:\t" + inst4);

            List<DicaseType> CildsOjects =
                new List<DicaseType>()
                { inst1, inst2, inst3,inst4 };

            Console.WriteLine(CildsOjects.ToLineList("Хвороби"));
            Discase inst5 = new Discase(
                "Краснуха", inst4)
            {
                Id = 5,

                note = "авал",

            };
            List<Discase> discases = new List<Discase>()
            {
                inst5,
                new Discase("Краснуха",
                     CildsOjects.First(
                         e => e.Nazva == "Інфекційна"))
            };
            Console.WriteLine(discases.ToLineList("discases"));

           
            Console.WriteLine("inst5:\t" + inst5);
            DateTime date1 = new DateTime(2015, 7, 20);
            Patcient inst6 = new Patcient(
             "Коваленко",date1,"45",inst5 )
            {
                Id = 5,


            };
            List<Patcient> patcients = new List<Patcient>()
            {
                inst6,
                new Patcient("Петренко", date1,"34",discases.First(
                         e => e.Nazva == "Краснуха"))
            };
            Console.WriteLine(patcients.ToLineList("xworodu"));

            Console.ReadKey();

        }
    }
}


