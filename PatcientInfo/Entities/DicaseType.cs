using Common.Context.LineIndents;
using Common.Context.StringFormatters;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatcientInfo.Entities
{
    [Serializable]
    public class DicaseType : Entity
    {
        public string Nazva { get; set; }
        public DicaseType Parent { get; set; }
        public string Descripotion { get; set; }
        public List<Discase> Discases { get; set; }
        public List<DicaseType> CildsOjects { get; set; }

        public DicaseType(string nazva, DicaseType parent)
        {
            Nazva = nazva;
            Parent = parent;
            CildsOjects = new List<DicaseType>();
            Discases = new List<Discase>();
        }
        public DicaseType() : this(null,null) { }

        public override string ToString()
        {
            string s = IdentifyingInfo;
            LineIndent.Current.Increase();
            s += string.Concat( TextualInfo,
                ChildObjectsInfo);
            LineIndent.Current.Decrease();
            return s;
        }

        public string IdentifyingInfo
        {
            get
            {
                return string.Format("{0}{1}{2}\n",
                    LineIndent.Current.Value, Nazva,
                    Parent == null ? "" : " -> " + Parent.Nazva);
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

        public string ChildObjectsInfo
        {
            get
            {
                string s = null;
                if (CildsOjects.Count > 0)
                {
                    s += string.Format("{0}Включає  хвороби:\n",
                        LineIndent.Current.Value);
                    LineIndent.Current.Increase();
                    foreach (var obj in CildsOjects)
                    {
                        s += string.Format("{0}{1}\n",
                            LineIndent.Current.Value, obj.Nazva);
                    }
                    LineIndent.Current.Decrease();
                }
                return s;
            }
        }

    }
        }


