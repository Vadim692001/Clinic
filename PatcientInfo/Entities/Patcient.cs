using Common.Entities;
using System;
using Common.Context.LineIndents;
using System.Collections.Generic;

namespace PatcientInfo.Entities
{
    [Serializable]
    public class Patcient:Entity
    {
      
        public string Sorname { get; set; }
        


        public DateTime? Date { get; set; }
        public string Medical_card { get; set; }
         public Discase Dicase{ get; set; }
        public string Doctor { get; set; }
        public string number_Chamber { get; set; }

        public string Note { get; set; }
        public string Description { get; set; }

        public Patcient(string sorname,  DateTime? date, string medical_card,Discase discase)
        {
            Sorname = sorname;
            Date = date;
            Medical_card = medical_card;
            Dicase = discase;

        }
         public Patcient() : this(null, null, null,null) { }
        public override string ToString()
        {
            string s = IdentifyingInfo;
            LineIndent.Current.Increase();
            s +=string.Concat(DescribingInfo);
            LineIndent.Current.Decrease();
            return s;
        }

        public string IdentifyingInfo
        {
            get
            {
                return string.Format(
                    "{0}Прізвище пацієнта:{1}, Дата:{2}, медична кардка: {3}, Діагноз: {4}\n",
                    LineIndent.Current.Value,
                    Sorname, Date.ToString(), Doctor, Dicase.Nazva);
            }
        }

        public string DescribingInfo
       {
            get
            {
                return string.Format("{0} лікарь:{1}\tпалата:{2}\n",
                    LineIndent.Current.Value, Medical_card,number_Chamber);
            }
        }
        public IList<Patcient> Discases { get; set; }

        //public List<Patcient> Objects { get; set; }

        //public object PatcientByRegionsInfo()
        //{
        //    throw new NotImplementedException();
        //}
    }
    }
