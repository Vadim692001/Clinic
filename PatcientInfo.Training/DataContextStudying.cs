using System;
using PatcientInfo.Data;


using Common.Context.Extensions;
using PatcientInfo.Data.Extensions;
using PatcientInfo.IO;
using System.Linq;

namespace PatcientInfo.Training
{
    class DataContextStudying
    {
        public static void Run()
        {
            Console.WriteLine("==== DataContextStudying ====");
          //  StudyDataSet();

          //  StudyDataSetExtensions();//
          //  StudyDataContext();

        }

        private static void StudyDataSet()
        {
            Console.WriteLine(" --- StudyDataSet ---");

            DateSet dataSet = new DateSet();

            Console.WriteLine(dataSet.DicaseTypes
                    .ToLineList("dataSet.DicaseTypes"));

            TestDataCreation.CreateDicaseType(dataSet.DicaseTypes);
            Console.WriteLine(dataSet.DicaseTypes
                   .ToLineList("dataSet.Discases"));
            Console.WriteLine(dataSet.Discases
                     .ToLineList("dataSet.Discases"));

             TestDataCreation.CreateDicases(dataSet);
             Console.WriteLine(dataSet.Discases
                     .ToLineList("dataSet.Discases"));
            Console.WriteLine(dataSet.Patcients
                    .ToLineList("dataSet.Patcients"));

            TestDataCreation.CreatePatcient(dataSet);
            Console.WriteLine(dataSet.Patcients
                    .ToLineList("dataSet.Patcients"));


             Console.ReadKey();
        }


        private static void StudyDataSetExtensions()
        {
            Console.WriteLine(" --- StudyDataSetExtensions ---");

            DateSet dataSet = new DateSet();
            Console.WriteLine("dataSet:\n" + dataSet.ToDataString());
            Console.WriteLine("dataSet:\n" + dataSet);

            dataSet.CreateObjectss();
            Console.WriteLine("dataSet:\n" + dataSet.ToDataString());

            dataSet.CreateObjectsLinks();
            Console.WriteLine("dataSet:\n" + dataSet.ToDataString());

            dataSet.Clear();
            Console.WriteLine("dataSet:\n" + dataSet.ToDataString());

            dataSet.CreateTestingData();
            Console.WriteLine("dataSet:\n" + dataSet.ToDataString());

            string DicaseTypesName = "Вітрянка";
            ReadOnlyDataSet readOnlyDataSet1 = new ReadOnlyDataSet
            {
                DicaseTypes = dataSet.DicaseTypes.Where(
                    e => e.Nazva == DicaseTypesName),
                Discases = dataSet.Discases.Where(
                    e => e.DicaseType.Nazva == DicaseTypesName),
                     Patcients= dataSet.Patcients.Where(
                    e => e.Dicase.Nazva == DicaseTypesName)
            };

            Console.WriteLine("readOnlyDataSet1:\n"
                + readOnlyDataSet1.ToDataString());

            Console.ReadKey();
        }

        private static void StudyDataContext()
        {
            Console.WriteLine(" --- StudyDataContext ---");

            //DataContext dataContext = new DataContext();
            DataContext dataContext =
                new DataContext(new XmlIOController());
            Console.WriteLine("dataContext:\n" +
                dataContext.ToDataString());

            dataContext.CreateTestingData();
            Console.WriteLine("dataContext:\n" +
                dataContext.ToDataString());

            Console.WriteLine("dataContext:\n" + dataContext);

            Console.WriteLine(dataContext.DicaseTypes
                .ToLineList("DicaseTypes"));
            Console.WriteLine(dataContext.Discases
                .ToLineList("Discases"));
            Console.WriteLine(dataContext.Patcients
                .ToLineList("Patcients"));


            ReadOnlyDataSet readOnlyDataSet = dataContext;
            Console.WriteLine("readOnlyDataSet:\n" + readOnlyDataSet);

            readOnlyDataSet.Discases = readOnlyDataSet.Discases .OrderBy(e => e.Nazva);
            Console.WriteLine("readOnlyDataSet:\n" + readOnlyDataSet);
            readOnlyDataSet.Patcients = readOnlyDataSet.Patcients.OrderBy(e => e.Sorname);
            Console.WriteLine("readOnlyDataSet:\n" + readOnlyDataSet);

            dataContext.SaveAsText("dataContext.txt");
            readOnlyDataSet.SaveAsText("readOnlyDataSet.txt");

            dataContext.SaveAsText();

            dataContext.FileName = "PatcientInfo";
            dataContext.SaveAsText();

            dataContext.Directory = @"..\..\..\data";
            dataContext.SaveAsText();

            Console.ReadKey();
        }
    }

    }

