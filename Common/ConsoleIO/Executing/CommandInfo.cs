using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConsoleIO.Executing
{
    public class CommandInfo
    {
        public string name;
        public Action command;

        public CommandInfo(string name, Action command)
        {
            this.name = name;
            this.command = command;
        }
    }
}
