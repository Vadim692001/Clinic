using PatcientInfo.Data.Extensions;
using PatcientInfo.Data.Interfeces;
using PatcientInfo.Entities;
using PatcientInfo.IO.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PatcientInfo.IO
{
    public class XmlIOController : IFileIOController
    {

        public string FileExtension { get; set; }
        public string FileTypeCaption { get; set; }

        public XmlIOController()
        {
            FileExtension = ".xml";
            FileTypeCaption = "Файли XML";
        }
        public void Save(IReadOnlyDataSet dataSet, string filePath)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.Unicode;

            using (XmlWriter writer = XmlWriter.Create(
                    filePath + FileExtension, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("PatcientInfo");
                WriteDicaseTypes(dataSet.DicaseTypes, writer);
                WriteDiscases(dataSet.Discases, writer);
                WritePatcients(dataSet.Patcients, writer);
                writer.WriteEndElement();
                writer.WriteEndDocument();
            };
        }

        private static void WriteDicaseTypes(
                IEnumerable<DicaseType> collection, XmlWriter writer)
        {
            writer.WriteStartElement("DicaseTypesData");
            foreach (var obj in collection)
            {
                writer.WriteStartElement("DicaseType");
                writer.WriteElementString("Id", obj.Id.ToString());
                writer.WriteElementString("Nazva", obj.Nazva);
                int parentId = obj.Parent == null ? 0 : obj.Parent.Id;
                writer.WriteElementString("ParentId", parentId.ToString());
                writer.WriteElementString("Opuc", obj.Descripotion);
                writer.WriteEndElement();
                
            }
            writer.WriteEndElement();
        }

        private static void WriteDiscases(
                IEnumerable<Discase> collection, XmlWriter writer)
        {
            writer.WriteStartElement("DiscasesData");
            foreach (var obj in collection)
            {
                writer.WriteStartElement("Discase");
                writer.WriteElementString("Id", obj.Id.ToString());
                writer.WriteElementString("Nazva", obj.Nazva);
                writer.WriteElementString("DicaseTypeId",  obj.DicaseType.Id.ToString());
                writer.WriteElementString("Descripotion", obj.Descripotion);
                writer.WriteElementString("Note", obj.note);

                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        private static void WritePatcients(
              IEnumerable<Patcient> collection, XmlWriter writer)
        {
            writer.WriteStartElement("PatcientsData");
            foreach (var obj in collection)
            {
                writer.WriteStartElement("Patcient");
                writer.WriteElementString("Id", obj.Id.ToString());
                writer.WriteElementString("Sorname", obj.Sorname);
                writer.WriteElementString("DiscaseId",  obj.Dicase.Id.ToString());
                writer.WriteElementString("Doctor", obj.Doctor);
                writer.WriteElementString("Medical_card", obj.Medical_card);
                writer.WriteElementString("number_Chamber", obj.number_Chamber);
                writer.WriteElementString("Date",obj.Date.ToString());

                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }



        //-----------------------------------------------------------

        public void Load(IDateSet dataSet, string filePath)
        {
            string fileName = filePath + FileExtension;
            if (!File.Exists(fileName)) return;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            using (XmlReader reader = XmlReader.Create(
                    filePath + FileExtension, settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "DicaseType":
                                ReadDicaseTypes(reader, dataSet.DicaseTypes);
                                break;
                            case "Discase":
                                ReadDiscase(reader, dataSet);
                                break;
                            case "Patcient":
                                ReadPatcient(reader, dataSet);
                                break;
                        }
                    }
                }
            }
            dataSet.CreateObjectsLinks();
        }

        private static void ReadDicaseTypes(XmlReader reader,
                ICollection<DicaseType> collection)
        {
            DicaseType inst = new DicaseType();
            reader.ReadStartElement("DicaseType");
            inst.Id = reader.ReadElementContentAsInt();
            inst.Nazva = reader.ReadElementContentAsString();
            int parentId = reader.ReadElementContentAsInt();
            inst.Parent = collection
                .FirstOrDefault(e => e.Id == parentId);

            inst.Descripotion = reader.ReadElementContentAsString();
            collection.Add(inst);
        }
        private static void ReadDiscase(XmlReader reader, IDateSet dataSet)
        {
            Discase inst = new Discase();
            reader.ReadStartElement("Discase");
            inst.Id = reader.ReadElementContentAsInt();
            inst.Nazva = reader.ReadElementContentAsString();
            int dicaseTypeId = reader.ReadElementContentAsInt();
            inst.DicaseType = dataSet.DicaseTypes.FirstOrDefault(e => e.Id == dicaseTypeId);

            inst.Descripotion = reader.ReadElementContentAsString();
            dataSet.Discases.Add(inst);
        }


        private void ReadPatcient(XmlReader reader, IDateSet dataSet)
        {
            Patcient inst = new Patcient();
            reader.ReadStartElement("Patcient");
            inst.Id = reader.ReadElementContentAsInt();
            inst.Sorname = reader.ReadElementContentAsString();
            int patcientId = reader.ReadElementContentAsInt();
            inst.Dicase = dataSet.Discases.FirstOrDefault(e => e.Id == patcientId);

            inst.Medical_card = reader.ReadElementContentAsString();
            inst.Doctor = reader.ReadElementContentAsString();
            dataSet.Patcients.Add(inst);

        }
    }
    }
