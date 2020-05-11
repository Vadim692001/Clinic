using PatcientInfo.IO;
using System;
using PatcientInfo.Data;
using System.Linq;
using PatcientInfo.Data.Extensions;
using PatcientInfo.ConsoleIO.Selection;
namespace PatcientInfo.Training
{
    class FileIOStudying
    {

        public static void Run()
        {
            Console.WriteLine("==== FileIOStudying ====");
            StudyXmlIO();
            Console.WriteLine();
        }

        private static void StudyXmlIO()
        {
            Console.WriteLine(" --- StudyXmlIO ---");

            DataContext dataContext = new DataContext();

            dataContext.CreateTestingData();
            Console.WriteLine(dataContext);

           // dataContext.Save();

            dataContext.Clear();
            Console.WriteLine(dataContext);

            dataContext.Load();
            Console.WriteLine(dataContext);
        }
    }
    }
