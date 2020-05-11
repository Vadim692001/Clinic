using PatcientInfo.Data.Extensions;
using PatcientInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatcientInfo.Data
{
  public  class StaticDataContext
    {
        private static DataContext dataContext =
              new DataContext("");

        public static ICollection<Patcient> Patcients
        {
            get { return dataContext.Patcients; }
        }
        public static ICollection<DicaseType> DicaseTypes
        {
            get { return dataContext.DicaseTypes; }
        }
        public static ICollection<Discase> Discases
        {
            get { return dataContext.Discases; }
        }


        //private static string directory;
        public static string Directory
        {
            get { return dataContext.Directory; }
            set
            {
                dataContext.Directory = value;
            }
        }

        public static string FileName { get; set; }
        private static string filePath;

        public static void CreateTestingData()
        {
            dataContext.CreateTestingData();
        }

        public static void Load()
        {
            dataContext.Load();
        }

        //public static void Save()
        //{
        //    dataContext.Save();
        //}

    }
}

