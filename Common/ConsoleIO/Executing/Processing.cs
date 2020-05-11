using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConsoleIO.Executing
{
    public class Processing
    {
        public static void ShowCommandsMenu(CommandInfo[] commandInfoArray)
        {
            Console.WriteLine("  Перелік команд меню:");
            for (int i = 0; i < commandInfoArray.Length; i++)
            {
                Console.WriteLine("\t{0,2} - {1}", i, commandInfoArray[i].name);
            }
        }

        public static Action EnterCommand(CommandInfo[] commandInfoArray)
        {
            while (true)
            {
                int number;
                Console.Write("\n  Введіть номер команди меню: ");
                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("\tПомилка введення числового значення!");
                    continue;
                }
                if (number < commandInfoArray.Length)
                    return commandInfoArray[number].command;
                Console.WriteLine("\tНемає команди з введеним номером!");
            }
        }
    }
}
