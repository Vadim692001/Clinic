using Common.Context.Extensions;
using Common.Context.LineIndents;
using PatcientInfo.Data.Interfeces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatcientInfo.Data.Extensions
{
    public static class DataSetMethods
    {
        public static string ToDataString(this IReadOnlyDataSet dataSet)
        {
            string s = string.Concat(LineIndent.Current.Value,
                "Інформація про  \"Хвороби\"\n");
            LineIndent.Current.Increase();
            s += dataSet.DicaseTypes.ToLineList(
                "Вид хвороби");
            s += dataSet.Discases.ToLineList(
                "Залеижні хвороби");
            s += dataSet.Patcients.ToLineList(
               "Пацієнти");
            LineIndent.Current.Decrease();
            return s;
        }



        public static void CreateObjectss(this IDateSet dataSet)
        {
            TestDataCreation.CreateDicaseType(
                dataSet.DicaseTypes);
            TestDataCreation.CreateDicases(dataSet);

            TestDataCreation.CreatePatcient(dataSet);
        }

        public static void CreateObjectsLinks(this IDateSet dataSet)
        {
            foreach (var obj in dataSet.DicaseTypes)
            {
                if (obj.Parent != null)
                {

                    
                    obj.Parent.CildsOjects.Add(obj);
                }
            }
            foreach (var obj in dataSet.Discases)
            {
                if (obj.DicaseType != null)
                {
                    
                    obj.DicaseType.Discases.Add(obj);
                }
            }
            foreach (var obj in dataSet.Patcients)

            {
                if (obj.Dicase != null)
                {
                    obj.Dicase.Patcients.Add(obj);
                }
            }
        }

        public static void CreateTestingData(this IDateSet dataSet)
        {
            CreateObjectss(dataSet);
           // CreateObjectsLinks(dataSet);
        }

        public static void Clear(this IDateSet dataSet)
        {
         
            dataSet.DicaseTypes.Clear();
            dataSet.Discases.Clear();
            dataSet.Patcients.Clear();
        }

        public static void SaveAsText(
                this IReadOnlyDataSet dataSet, string filePath)
        {
            File.WriteAllText(filePath, dataSet.ToDataString(),
                Encoding.Unicode);
        }

    }
}
