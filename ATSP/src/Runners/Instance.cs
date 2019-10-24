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
        
        [System.Xml.Serialization.XmlElement("graph")]
        public Graph graph{get;set;}
    }

    [Serializable()]
    public class Graph{
        [System.Xml.Serialization.XmlArray("vertex")]
        [System.Xml.Serialization.XmlArrayItem("vertex", typeof(Vertex))]
        public Vertex[] Vertex {get;set;}

    }
    [Serializable()]
    public class Vertex{
        [System.Xml.Serialization.XmlArray("edge")]
        [System.Xml.Serialization.XmlArrayItem("edge", typeof(Edge))]
        public Edge[] edges{get;set;}
    }
    [Serializable()]
    public class Edge{
        [System.Xml.Serialization.XmlText]
        public int no{get;set;}

        [System.Xml.Serialization.XmlAttribute("cost")]
        public int cost {get;set;}
    }
}