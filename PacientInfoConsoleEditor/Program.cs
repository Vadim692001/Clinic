using Common.ConsoleIO;
using PatcientInfo.ConsoleIO.Selection;
using PatcientInfo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacientInfoConsoleEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            
                Console.Title = "CountriesInfoConsoleEditor";
                Settings.SetConsoleParam();

                CreateDependencies();
                mainManager.Run();

                Console.ReadKey(true);
            }

            static DataContext dataContext;
            static FileIOControllersSelector fileIOControllersSelector
                = new FileIOControllersSelector();

            static MainManager mainManager;

            private static void CreateDependencies()
            {
                dataContext = new DataContext(
                    fileIOControllersSelector.Controller);
                mainManager = new MainManager(dataContext,
                    fileIOControllersSelector);
            }
        }
    }

