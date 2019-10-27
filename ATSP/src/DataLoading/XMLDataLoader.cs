using System;
using System.IO;
using ATSP.Data;
using System.Xml;
using System.Xml.Serialization;


namespace ATSP.DataLoading
{
    public class XMLDataLoader : IDataLoader
    {
        public TravellingSalesmanProblemInstance LoadInstance(string file)
        {
            if(!File.Exists(file))
            {
                return new TravellingSalesmanProblemInstance();
            }

            var serializator = new XmlSerializer(typeof(TravellingSalesmanProblemInstance));

            using (var reader = XmlReader.Create(file))
            {
                return serializator.Deserialize(reader) as TravellingSalesmanProblemInstance ?? new TravellingSalesmanProblemInstance();
            }
        }
    }
}