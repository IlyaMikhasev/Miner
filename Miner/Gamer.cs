using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Miner
{
    [Serializable]
    public class Gamer
    {
        [XmlAttribute]
        public string _name;
        [XmlAttribute]
        public int _score;
        public Gamer() { }
        public Gamer(string name, int score)
        {
            _name = name;
            _score = score;
        }
        static public void Serealize_it(List<Gamer> objectGrath, string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Gamer>));
            using (Stream fStream = new FileStream(filename,
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlSerializer.Serialize(fStream, objectGrath);
            }
        }
        static public void Deserealize_it(string filename, out List<Gamer> lst)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Gamer>));
            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    lst = (List<Gamer>)xmlSerializer.Deserialize(reader);
                }
            }
            catch
            {
                lst = new List<Gamer>();
            }
        }
    }
}
