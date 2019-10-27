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
                var instance = serializator.Deserialize(reader) as TravellingSalesmanProblemInstance;
                if(instance is null)
                {
                    return new TravellingSalesmanProblemInstance();
                }
                instance.TransformToArray();
                return instance;
            }
        }
    }
}