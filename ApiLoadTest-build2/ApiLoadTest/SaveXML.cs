using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApiLoadTest
{
   public class SaveXML
    {
        public static void Savedata(object obj,string filename)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            TextWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, obj);
            writer.Close();
        }

        public static void Getdata(ref SaveData obj, string filename)
        {
            using (FileStream fileStream = new FileStream(filename, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                obj=(SaveData)serializer.Deserialize(fileStream);
            }
            
        }
    }
}
