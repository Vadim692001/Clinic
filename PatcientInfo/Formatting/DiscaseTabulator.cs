using PatcientInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatcientInfo.Formatting
{
   public class DiscaseTabulator
    {
        public string Title { get; set; }
        private DicaseTableAttributeSet attributeSet;


 

        public DicaseTableAttributeSet AttributeSet
        {
            get { return attributeSet; }
            set
            {
                attributeSet = value;
                IniMembers();
            }
        }

        public DiscaseTabulator()
        {
            Title = " Хвороби\n";
            AttributeSet = new DicaseTableAttributeSet();
        }

        private string indent;
        private string line;
        private string header;

        // ?
        private void IniMembers()
        {
            indent = new string(' ', AttributeSet.indent);
            line = CreateLine();
            header = CreateHeader();
        }

        private bool useId = true;

        public bool UseId
        {
            get { return useId; }
            set
            {
                useId = value;
                header = CreateHeader();
            }
        }

        private string CreateLine()
        {
            int length = AttributeSet.numberWidth
                    + AttributeSet.nameWidth
                    + AttributeSet.parentNameWidth
                    + AttributeSet.descrWidth
                    + 2;
            return indent + new String('-', length) + "\n";
        }

        private string CreateHeader()
        {
            string sNumber = UseId ? "Id" : "№";
            return indent
                + sNumber.PadLeft(AttributeSet.numberWidth) + " "
                + "Назва хвороби".PadRight(AttributeSet.nameWidth)
                + "Вид хвороба".PadRight(AttributeSet.parentNameWidth)
                + "Опис".PadLeft(AttributeSet.descrWidth)
                + "\n";
        }

        public string CreateTable(IEnumerable<Discase> collection)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(indent + Title);
            sb.Append(line);
            sb.Append(header);
            sb.Append(line);
            CreateRows(sb, collection);
            sb.Append(line);
            return sb.ToString();
        }

        private void CreateRows(StringBuilder sb,
                IEnumerable<Discase> collection)
        {
            var array = collection.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                CreateRow(sb, i + 1, array[i]);
            }
        }

        private void CreateRow(StringBuilder sb,
                int number, Discase inst)
        {
            if (UseId)
                number = inst.Id;
            sb.Append(indent);
            sb.Append(number.ToString().PadLeft(AttributeSet.numberWidth));
            sb.Append(" ");
            sb.Append(inst.Nazva.PadRight(AttributeSet.nameWidth));
            sb.Append((inst.DicaseType == null ? "" : inst.DicaseType.Nazva)
                .PadRight(AttributeSet.parentNameWidth));

            sb.AppendLine();
        }
    }
}
