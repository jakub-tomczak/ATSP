using System.Xml.Serialization;

namespace ATSP.Data
{
    public class Edge
    {
        [XmlText]
        public double Cost { get; set; }

        [XmlAttribute]
        public int ID { get; set; }
    }
}