using System;
using System.IO;
using ATSP.Data;

namespace ATSP.DataLoading
{
    public class XMLDataLoader : IDataLoader
    {
        public Instance LoadInstance(string file)
        {
            if(!File.Exists(file))
            {
                return new Instance();
            }

            using(var reader = new StreamReader(file))
            Console.WriteLine($"Loading instance from xml file {file}");
            return null;
        }
    }
}