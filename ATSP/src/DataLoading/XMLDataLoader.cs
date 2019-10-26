using System;
using System.IO;
using ATSP.Data;

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

            using(var reader = new StreamReader(file))
            Console.WriteLine($"Loading instance from xml file {file}");
            return null;
        }
    }
}