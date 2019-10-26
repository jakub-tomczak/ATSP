namespace ATSP.Data
{
    public class Vertex
    {
        [System.Xml.Serialization.XmlElement("edge")]
        public Edge[] Edges { get; set; }
    }
}