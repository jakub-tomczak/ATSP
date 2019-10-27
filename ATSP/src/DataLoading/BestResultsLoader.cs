using System;
using System.IO;
using System.Collections.Generic;

namespace ATSP.DataLoading
{
    public class BestResultsLoader
    {
        public Dictionary<string, uint> LoadBestResults(string bestResultsFilename)
        {
            if(!File.Exists(bestResultsFilename))
            {
                throw new ArgumentException($"File {bestResultsFilename} doesn't exist.");
            }

            var results = new Dictionary<string, uint>();
            using(var fileReader = new StreamReader(bestResultsFilename))
            {
                while(!fileReader.EndOfStream)
                {
                    var line = fileReader.ReadLine().Split();
                    if(line.Length != 2)
                    {
                        throw new FormatException($"File {bestResultsFilename} is not formatted properly.");
                    }
                    if(UInt32.TryParse(line[1], out uint cost))
                    {
                        if(!results.ContainsKey(line[0]))
                            results.Add(line[0], cost);
                    }
                    else
                    {
                        throw new FormatException($"Cost in line {line[0]} is badly formatted.");
                    }
                }
            }
            BestResults = results;
            return BestResults;
        }

        public uint GetBestResultForInstance(string instanceName)
        {
            if(BestResults is null)
            {
                return 0;
            }

            return BestResults.ContainsKey(instanceName) ? BestResults[instanceName] : 0;
        }

        public Dictionary<string, uint> BestResults
        {
            get;
            private set;
        }
    }
}