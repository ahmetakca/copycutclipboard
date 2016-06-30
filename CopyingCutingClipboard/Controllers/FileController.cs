using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CopyingCutingClipboard.Controllers
{
    public class FileController
    {
        //public static void WriteStringToFile(List<>, string Tus)
        //{
        //    //StreamWriter file = new StreamWriter(DosyaAdresi, true);
        //    //file.WriteLine(Tus);
        //    //file.Close();
        //    System.IO.StreamWriter xmlSW = new System.IO.StreamWriter("Customers.xml");

        //    // This example assumes you have a custom dataset called ds holding customer data
        //    ds.WriteXml(xmlSW, XmlWriteMode.WriteSchema);

        //    xmlSW.Close();

        //}
        private static string ApplicationFolderLocation { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CopyingCutingClipboard");
        private static string FileName { get; } = "temp.xml";
        private static string XmlFileLocation { get; } = Path.Combine(ApplicationFolderLocation, FileName);
        public static void WriteObjectToFile<T>(T obje)
        {
            //var garage = new theGarage();

            // TODO init your garage..


            if (!Directory.Exists(ApplicationFolderLocation))
            {
                Directory.CreateDirectory(ApplicationFolderLocation);
            }

            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (TextWriter tw = new StreamWriter(XmlFileLocation))
            {
                xs.Serialize(tw, obje);
            }
        }

        public static T ReadObjectFromFile<T>()
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));

            if (File.Exists(XmlFileLocation))
            {
                using (var sr = new StreamReader(XmlFileLocation))
                {
                    return (T)xs.Deserialize(sr);
                }
            }
            else
            {
                return default(T);
            }

        }
    }
}
