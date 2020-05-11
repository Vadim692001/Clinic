using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConsoleIO.Controls
{
  public abstract  class ConsolControl
    {
        protected int left = 0;
        protected int top = 0;
        protected int width = 30;
        protected int height = 1;

        public ConsolControl(int left, int top, int width, int height)
        {
            this.left = left;
            this.top = top;
            this.width = width;
            this.height = height;
        }

        private string text = "";

        public virtual string Text
        {
            get { return text; }
            set { text = value; }
        }

        protected ConsoleColor programBackColor;
        protected ConsoleColor programForeColor;
        protected int programCursorLeft;
        protected int programCursorTop;

        protected void RememberConsoleSettings()
        {
            programBackColor = Console.BackgroundColor;
            programForeColor = Console.ForegroundColor;
            programCursorLeft = Console.CursorLeft;
            programCursorTop = Console.CursorTop;
        }

        protected void RestoreConsoleSettings()
        {
            Console.BackgroundColor = programBackColor;
            Console.ForegroundColor = programForeColor;
            Console.CursorLeft = programCursorLeft;
            Console.CursorTop = programCursorTop;
        }
    }
}
