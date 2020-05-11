using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ConsoleIO.Controls.Selection
{
 public   class SimpleLineSelector: ConsolControl
    {
        protected int maxLength = 40;

        private int curPos = 0;

        public string[] Items { get; protected set; }

        public int SelectedIndex { get; protected set; }

        public SimpleLineSelector(int left, int top, int width, string[] items)
                                     : base(left, top, width, 1)
        {
            maxLength = width - 1;
            Items = items;
            //
            SelectedIndex = 0;
            Text = items[SelectedIndex];
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                curPos = Text.Length;
            }
        }

        protected string Pattern
        {
            get { return Text.Substring(0, curPos); }
        }

        private ConsoleColor backColor = ConsoleColor.Gray;
        private ConsoleColor textColor = ConsoleColor.Black;

        public void Focus()
        {
            Console.CursorVisible = false;
            RememberConsoleSettings();
            ShowArrow();
            ShowText();
            HandleKeyPress();
        }

        private void ShowArrow()
        {
            Console.BackgroundColor = backColor;
            Console.ForegroundColor = textColor;
            Console.SetCursorPosition(left + width - 1, top);
            Console.Write('↕'); // ↕↨
            Console.BackgroundColor = programBackColor;
            Console.Write('\u00A0');
        }

        protected void ShowText()
        {
            Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = textColor;
            Console.SetCursorPosition(left, top);
            Console.Write(Text);
            Console.CursorLeft = left + curPos;
        }


        private void Clear()
        {
            Console.SetCursorPosition(left, top);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = backColor;
            for (int i = 0; i < maxLength; i++)
            {
                Console.Write('░'); //░▒▓
            }
            Console.SetCursorPosition(left, top);
        }

        private void HandleKeyPress()
        {
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                if (!Console.KeyAvailable)
                    continue;
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        HandlePressEnter(keyInfo);
                        return;
                    case ConsoleKey.DownArrow:
                        HandlePressDownArrow(keyInfo);
                        break;
                    case ConsoleKey.UpArrow:
                        HandlePressUpArrow(keyInfo);
                        break;
                };
            };
        }

        private void HandlePressEnter(ConsoleKeyInfo KeyInfo)
        {
            curPos = Text.Length;
            ConsoleColor bgColor = backColor;
            backColor = programBackColor;
            ShowText();
            HideArrow();
            backColor = bgColor;
            RestoreConsoleSettings();
            Console.CursorVisible = true;
        }

        private void HideArrow()
        {
            Console.BackgroundColor = backColor;
            Console.SetCursorPosition(left + width - 1, top);
            Console.Write(' '); // ↕↨
        }

        private void HandlePressDownArrow(ConsoleKeyInfo keyInfo)
        {
            if (SelectedIndex < Items.Length - 1)
            {
                Text = Items[++SelectedIndex];
                curPos = 0;
                ShowText();
            }
        }

        private void HandlePressUpArrow(ConsoleKeyInfo keyInfo)
        {
            if (SelectedIndex > 0)
            {
                Text = Items[--SelectedIndex];
                curPos = 0;
                ShowText();
            }
        }
    }
}
