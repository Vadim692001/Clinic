using PatcientInfo.Data.Interfeces;
using PatcientInfo.IO.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PatcientInfo.IO
{
    public class BinarySerializationController : IFileIOController
    {
        public string FileExtension { get; set; }
        public string FileTypeCaption { get; set; }

        public BinarySerializationController()
        {
            FileExtension = ".polVad";
            FileTypeCaption = "Двійкові дані";
        }

        public void Load(IDateSet dataSet, string filePath)
        {
            IDateSet newDataSet = Load(filePath);
            foreach (var inst in newDataSet.DicaseTypes)
            {
                dataSet.DicaseTypes.Add(inst);
            }
            foreach (var inst in newDataSet.Discases)
            {
                dataSet.Discases.Add(inst);
            }
            foreach (var inst in newDataSet.Patcients)
            {
                dataSet.Patcients.Add(inst);
            }
        }

        public IDateSet Load(string filePath)
        {
            string fileName = filePath + FileExtension;
            if (!File.Exists(fileName))
                return null;
            IDateSet instance = null;
            BinaryFormatter bFormatter = new BinaryFormatter();
            using (FileStream fSteam = File.OpenRead(fileName))
            {
                instance = (IDateSet)bFormatter.Deserialize(fSteam);
            }
            return instance;
        }

        public void Save(IReadOnlyDataSet instance, string filePath)
        {
            if (instance == null)
                return; 
            BinaryFormatter bFormatter = new BinaryFormatter();
            using (var fStream = new FileStream(
                filePath + FileExtension, FileMode.Create,
                FileAccess.Write, FileShare.None))
            {
                bFormatter.Serialize(fStream, instance);
            }
        }
    }
    }
