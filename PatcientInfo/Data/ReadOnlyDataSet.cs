using PatcientInfo.Data.Extensions;
using PatcientInfo.Data.Interfeces;
using PatcientInfo.Entities;
using System.Collections.Generic;

namespace PatcientInfo.Data
{
    public  class ReadOnlyDataSet : IReadOnlyDataSet
    {
        public IEnumerable<DicaseType> DicaseTypes { get; set; }
        public IEnumerable<Discase> Discases { get; set; }
        public IEnumerable<Patcient> Patcients { get; set; }
     
        

        public override string ToString()
        {
            return this.ToDataString();
        }


    }
}
