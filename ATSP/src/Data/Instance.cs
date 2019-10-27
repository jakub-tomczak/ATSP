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

        [System.Xml.Serialization.XmlIgnore]
        public int N { get => Vertices?.Length ?? 0; }

        [System.Xml.Serialization.XmlIgnore]
        public uint BestKnownCost { get; set; }

        public uint this[int row, int column]
        {
            get => Vertices[row].Edges[column].Cost;
            set => Vertices[row].Edges[column].Cost = value;
        }

        public uint[,] ToArray()
        {
            if(array is null)
            {
                TransformToArray();
            }

            return array;
        }

        public void TransformToArray()
        {
            array = new uint[Vertices.Length, Vertices.Length];
            for(int i=0;i<N;i++)
            {
                if(Vertices[i].Edges.Length != N)
                {
                    throw new ArgumentException($"Edges in the row no. {i} have different size than Vertices");
                }
                for(int j=0;j<N;j++)
                {
                    array[i, j] = Vertices[i].Edges[j].Cost;
                }
            }
        }

        private uint [,] array;
    }
}