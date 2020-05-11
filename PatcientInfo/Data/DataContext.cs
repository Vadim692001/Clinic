using PatcientInfo.Data.Extensions;
using PatcientInfo.Data.Interfeces;
using PatcientInfo.Entities;
using PatcientInfo.IO;
using PatcientInfo.IO.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace PatcientInfo.Data
{
    public class DataContext : IDateSet, IReadOnlyDataSet
    {
        private DateSet dateSet = new DateSet();
        public ICollection<DicaseType> DicaseTypes
        {
            get { return dateSet.DicaseTypes; }
        }

        public ICollection<Discase> Discases
        {
            get { return dateSet.Discases; }
        }
        public ICollection<Patcient> Patcients
        {

            get { return dateSet.Patcients; }
        }

        IEnumerable<DicaseType> IReadOnlyDataSet.DicaseTypes
        {
            get { return dateSet.DicaseTypes; }
        }

        IEnumerable<Discase> IReadOnlyDataSet.Discases
        {
            get { return dateSet.Discases; }
        }
        IEnumerable<Patcient> IReadOnlyDataSet.Patcients
        {
            get { return dateSet.Patcients; }
        }



        public void Clear()
        {
            dateSet.Clear();
        }
        public override string ToString()
        {
            return this.ToDataString();
        }
     

        public void SaveAsText()
        {
            this.SaveAsText(
                Path.Combine(Directory, FileName + ".txt"));
        }

        public string FileName { get; set; }

        private string directory;

        public string Directory
        {
            get { return directory; }
            set
            {
                directory = value ?? "";
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }
            }
        }

        public DataContext()
        {
            FileName = "PatcientInfo";
            Directory = "files";
        }

        public DataContext(string directory)
        {
            FileName = "PatcientInfo";
            if (!string.IsNullOrEmpty(directory))
            {
                Directory = directory;
            }
        }

        public DataContext(IFileIOController fileIOController)
        {
            this.FileIOController = fileIOController;
            FileName = "PatcientInfo";
            Directory = "files";
        }

        public IFileIOController FileIOController =
            new XmlIOController();

        public void Save(string filePath)
        {
            FileIOController.Save(dateSet, filePath);
        }

        //public void Save()
        //{
        //    Save(Path.Combine(Directory, FileName));
        //}

        public void Load(string filePath)
        {
            FileIOController.Load(dateSet, filePath);
        }

        public void Load()
        {
            Load(Path.Combine(Directory, FileName));
        }

        public static implicit operator ReadOnlyDataSet(
                                DataContext dataContext)
        {
            return new ReadOnlyDataSet()
            {
                DicaseTypes = dataContext.DicaseTypes,
                Discases = dataContext.Discases,
                Patcients = dataContext.Patcients
            };
        }
    }
}
