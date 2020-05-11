using PatcientInfo.Data.Interfeces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatcientInfo.IO.Interfaces
{
    public interface IFileIOController
    {
        string FileExtension { get; }
        string FileTypeCaption { get; }
        void Save(IReadOnlyDataSet instance, string filePath);
        void Load(IDateSet instance, string filePath);
    }
}
