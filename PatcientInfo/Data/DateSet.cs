using System;
using PatcientInfo.Data.Interfeces;
using System.Collections.Generic;
using PatcientInfo.Entities;
using System.Data;
using PatcientInfo.Data.Extensions;


namespace PatcientInfo.Data
{
    [Serializable]
    public class DateSet : IDateSet, IReadOnlyDataSet
    {
        public List<DicaseType> DicaseTypes { get; private set; }

        public List<Discase> Discases { get; private set; }

        public List<Patcient> Patcients { get; private set; }
        

        public DateSet()
        {
            DicaseTypes = new List<DicaseType>();
            Discases = new List<Discase>();
            Patcients = new List<Patcient>();

      
        }

        ICollection<Patcient> IDateSet.Patcients
        {
            get { return Patcients; }
        }

        ICollection<Discase> IDateSet.Discases
        {
            get { return Discases; }
        }
        ICollection<DicaseType> IDateSet.DicaseTypes
        {
            get { return DicaseTypes; }
        }


        IEnumerable<Patcient> IReadOnlyDataSet.Patcients
        {
            get { return Patcients; }
        }

        IEnumerable<Discase> IReadOnlyDataSet.Discases
        {
            get { return Discases; }
        }
        IEnumerable<DicaseType> IReadOnlyDataSet.DicaseTypes
        {
            get { return DicaseTypes; }
        }

        public override string ToString()
        {
            return this.ToDataString();
        }
    }
}
