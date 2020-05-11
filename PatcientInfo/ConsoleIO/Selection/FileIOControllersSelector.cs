using Common.ConsoleIO.Editing;
using PatcientInfo.IO;
using PatcientInfo.IO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatcientInfo.ConsoleIO.Selection
{
    public class FileIOControllersSelector
    {

        private IFileIOController[] controllers = {
            new XmlIOController(),
            new BinarySerializationController(),
        };

        public IFileIOController Controller { get; private set; }

        public FileIOControllersSelector()
        {
            Controller = controllers[0];
        }

        public string FileFormatString
        {
            get
            {
                return string.Format("{0} ({1})",
                    Controller.FileTypeCaption,
                    Controller.FileExtension);
            }
        }

        public void Select()
        {
            ShowFileFormatsMenu();
            int number = Entering.EnterInt32(
                "\n\tВведіть номер формату файлу");
            Controller = controllers[number - 1];
        }

        private void ShowFileFormatsMenu()
        {
            Console.WriteLine("\n  Перелік форматів файлів:\n");
            for (int i = 0; i < controllers.Length; i++)
            {
                Console.WriteLine("{0,8}{1,2} - {2} ({3})",
                    controllers[i] == Controller ? "√" : "", // √►
                    i + 1,
                    controllers[i].FileTypeCaption,
                    controllers[i].FileExtension);
            }
        }
    }
    }
