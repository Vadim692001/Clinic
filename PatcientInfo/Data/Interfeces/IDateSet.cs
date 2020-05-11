using PatcientInfo.Entities;

using System.Collections.Generic;


namespace PatcientInfo.Data.Interfeces
{
    public interface IDateSet
    {
        ICollection<DicaseType> DicaseTypes { get; }
        ICollection<Discase> Discases { get; }
        ICollection<Patcient> Patcients { get; }
    
   


    }
}
