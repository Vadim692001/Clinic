using PatcientInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatcientInfo.Data.Interfeces
{
    public interface IReadOnlyDataSet
    {
          IEnumerable<DicaseType> DicaseTypes { get; }
          IEnumerable<Discase> Discases { get; }
          IEnumerable<Patcient> Patcients { get; }
       
    }
}

