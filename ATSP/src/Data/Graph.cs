using System.Xml.Serialization;

namespace ATSP.Data
{
    public class Graph
    {
        [XmlArray("vertex")]
        [XmlArrayItem("vertex", typeof(Vertex))]
        public Vertex[] vertices { get; set; }
    }
}