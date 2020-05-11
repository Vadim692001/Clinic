using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConsoleIO.Executing
{
    public abstract class CommandManager
    {

        protected CommandInfo[] commandsInfo;

        protected abstract void IniCommandsInfo(
                        out CommandInfo[] commandsInfo);

        public CommandManager()
        {
            IniCommandsInfo(out commandsInfo);
        }

        public void Run()
        {
            while (true)
            {
                PrepareScreen();
                ShowCommandsMenu();
                Action command = EnterCommand();
                if (command == null)
                {
                    return;
                }
                //try
                //{
                    command();
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine("\n\t" + ex.Message);
                //}
            }
        }

        protected abstract void PrepareScreen();

        private void ShowCommandsMenu()
        {
            Console.WriteLine("  Перелік команд меню:");
            for (int i = 0; i < commandsInfo.Length; i++)
            {
                Console.WriteLine("\t{0,2} - {1}",
                    i, commandsInfo[i].name);
            }
        }

        private Action EnterCommand()
        {
            while (true)
            {
                int number;
                Console.Write("\n  Введіть номер команди меню: ");
                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine(
                        "\tПомилка введення числового значення!");
                    continue;
                }
                if (number < commandsInfo.Length)
                    return commandsInfo[number].command;
                Console.WriteLine(
                    "\tНемає команди з введеним номером!");
            }
        }
    }
}
