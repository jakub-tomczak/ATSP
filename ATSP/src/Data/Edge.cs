using System.Xml.Serialization;

namespace ATSP.Data
{
    public class Vertex
    {
        [XmlArray("edge")]
        [XmlArrayItem("edge", typeof(Edge))]
        public Edge[] edges { get; set; }
    }
}