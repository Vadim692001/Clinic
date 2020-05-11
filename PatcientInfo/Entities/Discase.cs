using Common.Context.LineIndents;
using Common.Context.StringFormatters;
using Common.Entities;
using System;
using System.Collections.Generic;


namespace PatcientInfo.Entities
{
    [Serializable]
    public class Discase : Entity
    {
        public string Nazva { get; set; }
        public DicaseType DicaseType { get; set; } 
        public string Descripotion { get; set; }
        public string note { get; set; }
    

        public List<Patcient> Patcients { get; set; }
     
        public Discase(string nazva, DicaseType discases)
        {
            Nazva = nazva;
            this.DicaseType = discases;
            Patcients = new List<Patcient>();
           

        }
        public Discase() : this(null, null) { }

        public override string ToString()
        {
            string s = IdentifyingInfo;
            LineIndent.Current.Increase();
            s += string.Concat(DescribingInfo, TextualInfo,
                 RelatedObjectsInfo);
            LineIndent.Current.Decrease();
            return s;
        }

        public string IdentifyingInfo
        {
            get
            {
                return string.Format("{0}{1}{2}\n",
                    LineIndent.Current.Value, Nazva,
                    DicaseType == null ? " " : " -> " + DicaseType.Nazva);
            }
        }

        public string DescribingInfo
        {
            get
            {
                return string.Format("{0}Примітка: {1} \n",
                    LineIndent.Current.Value, note);
            }
        }

        public string TextualInfo
        {
            get
            {
                string s = null;
                if (!string.IsNullOrWhiteSpace(Descripotion))
                {
                    s += string.Format("{0}Опис:\n",
                        LineIndent.Current.Value);
                    LineIndent.Current.Increase();
                    s += StringFormatter.Current
                        .FormatWithLineBreaks(Descripotion + "\n");
                    LineIndent.Current.Decrease();
                }
                return s;
            }
        }

       

        public string RelatedObjectsInfo
        {
            get
            {
                string s = null;
                if (Patcients.Count > 0)
                {
                    s += string.Format("{0}Включає прізвище пацієнтів:\n",
                        LineIndent.Current.Value);
                    LineIndent.Current.Increase();
                    foreach (var obj in Patcients)
                    {
                        s += string.Format("{0}{1}\n",
                            LineIndent.Current.Value, obj.Sorname);
                    }
                    LineIndent.Current.Decrease();
                }
                return s;
            }
        }

        public static explicit operator Discase(DicaseType v)
        {
            throw new NotImplementedException();
        }
    }
}
