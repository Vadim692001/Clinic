using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConsoleIO.Editing
{
    public static class Entering
    {

        public static string format = "{0,40}: ";

        public static int EnterInt32(string prompt)
        {
            Console.Write(format, prompt);
            string s = Console.ReadLine();
            int value = int.Parse(s);
            return value;
        }
        private static void OutPromptAndDefault(
              string prompt, object defaultValue)
        {
            Console.Write(format, prompt + " (" + defaultValue + ")");
        }

        public static string EnterString(string prompt)
        {
            Console.Write(format, prompt);
            string s = Console.ReadLine();
            return s.Trim();
        }

        public static string EnterString(
                string prompt, string defaultValue)
        {
            OutPromptAndDefault(prompt, defaultValue);
            string s = Console.ReadLine();
            return s == "" ? defaultValue : s == "\"\"" ? ""
                : s == "?" ? null : s.Trim();
        }

        public static string EnterStringOrNull(string prompt)
        {
            Console.Write(format, prompt);
            string s = Console.ReadLine();
            s = s.Trim();
            return s == "" ? null : s;
        }
    }
}
