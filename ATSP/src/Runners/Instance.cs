using System;

namespace ATSP.Runners{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("travellingSalesmanProblemInstance")]
    public class TravellingSalesmanProblemInstance{

        [System.Xml.Serialization.XmlElement("name")]
        public string name{get;set;}

        [System.Xml.Serialization.XmlElement("source")]
        public string source{get;set;}
        [System.Xml.Serialization.XmlElement("description")]
        public string description{get;set;}
        [System.Xml.Serialization.XmlElement("doublePrecision")]
        public int doublePrecision {get;set;}

        [System.Xml.Serialization.XmlElement("ignoredDigits")]
        public int ignoredDigits {get;set;}
        
        [System.Xml.Serialization.XmlArray("graph")]
        [System.Xml.Serialization.XmlArrayItem("vertex")]
        public Vertex[] vertices{get;set;}
    }

    public class Vertex{
        [System.Xml.Serialization.XmlElement("edge")]
        public Edge[] edges{get;set;}
    }

    public class Edge{


        [System.Xml.Serialization.XmlText(typeof(int))]
        public int no{get;set;}
        [System.Xml.Serialization.XmlAttribute]
        public string cost{get;set;}
    }
}