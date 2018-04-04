
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Assets.Scripts {
    public class XmlSerialization {

        private static string UTF8ByteArrayToString(byte[] characters) {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private static byte[] StringToUTF8ByteArray(string pXmlString) {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        public static string SerializeObject(object pObject) {
            string XmlizedString = null;
            using (MemoryStream memoryStream = new MemoryStream()) {

                XmlSerializer xs = new XmlSerializer(typeof(CubeiqContainer.Cubeiq));

                using (XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8)) 
                    xs.Serialize(xmlTextWriter, pObject);

                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
            }

            return XmlizedString;
        }

        public static object DeserializeObject(string pXmlizedString) {
            XmlSerializer xs = new XmlSerializer(typeof(CubeiqContainer.Cubeiq));

            using (MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString))) {
                return xs.Deserialize(memoryStream);
            }
        }
    }
}
