using PatcientInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatcientInfo.Formatting
{
    public class PatcientTabulator
    {
        public string Title { get; set; }
        private PatcientTableAttributeSet attributeSet;
    

        public PatcientTableAttributeSet AttributeSet
        {
            get { return attributeSet; }
            set
            {
                attributeSet = value;
                IniMembers();
            }
        }

        public PatcientTabulator()
        {
            Title = "Пацієнти\n";
            AttributeSet = new PatcientTableAttributeSet();
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
                    + AttributeSet.diagnosNameWidth
                    + AttributeSet.DoctorWidth
                    + AttributeSet.dataWidth
                    + AttributeSet.medCardWidth
                    + attributeSet.palataWidth
                    + 4;
            return indent + new string('-', length) + "\n";
        }

        private string CreateHeader()
        {
            string sNumber = UseId ? "Id" : "№";
            return indent
                + sNumber.PadLeft(AttributeSet.numberWidth) + " "
                + "Прізвище".PadRight(AttributeSet.nameWidth)
                + "Діагноз".PadRight(AttributeSet.diagnosNameWidth)
                + "Лікарь\t".PadLeft(AttributeSet.DoctorWidth)
                 + "Медична кардка\t".PadRight(AttributeSet.medCardWidth)
                + "Дата\t".PadRight(AttributeSet.dataWidth)
                + "Палата\t".PadLeft(AttributeSet.palataWidth)
                + "\n";
        }

        public string CreateTable(IEnumerable<Patcient> collection)
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
                IEnumerable<Patcient> collection)
        {
            var array = collection.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                CreateRow(sb, i + 1, array[i]);
            }
        }

        private void CreateRow(StringBuilder sb,
                int number, Patcient inst)
        {
            if (UseId)
                number = inst.Id;
            sb.Append(indent);
            sb.Append(number.ToString().PadLeft(AttributeSet.numberWidth));
            sb.Append(" ");
            sb.Append(inst.Sorname.PadRight(AttributeSet.nameWidth));
            sb.Append((inst.Dicase == null ? "" : inst.Dicase.Nazva)
                .PadRight(AttributeSet.diagnosNameWidth));
            sb.Append((inst.Doctor == null ? "" : inst.Doctor)
               .PadRight(AttributeSet.DoctorWidth));
            //sb.Append(inst.Medical_card.PadLeft(AttributeSet.medCardWidth)
            //    .PadRight(AttributeSet.medCardWidth));
            sb.Append((inst.Medical_card ?? "").PadRight(AttributeSet.medCardWidth));
           
            sb.Append(inst.Date.Value.ToString().PadLeft(AttributeSet.dataWidth));

            sb.Append((inst.number_Chamber ?? "").PadLeft(AttributeSet.palataWidth));
            sb.AppendLine();
        }
    }
}
