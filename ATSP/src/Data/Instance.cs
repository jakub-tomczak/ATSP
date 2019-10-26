using System;
using System.Globalization;

namespace ATSP.Data
{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("travellingSalesmanProblemInstance")]
    public class TravellingSalesmanProblemInstance
    {
        [System.Xml.Serialization.XmlElement("name")]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlElement("source")]
        public string Source { get; set; }
        [System.Xml.Serialization.XmlElement("description")]
        public string Description { get; set; }
        [System.Xml.Serialization.XmlElement("doublePrecision")]
        public int DoublePrecision { get; set; }

        [System.Xml.Serialization.XmlElement("ignoredDigits")]
        public int IgnoredDigits { get; set; }

        [System.Xml.Serialization.XmlArray("graph")]
        [System.Xml.Serialization.XmlArrayItem("vertex")]
        public Vertex[] Vertices { get; set; }
    }
}