using System;
using System.IO;
using System.Collections.Generic;

namespace ATSP.DataLoading
{
    public static class BestResultsLoader
    {
        public static Dictionary<string, uint> LoadBestResults(string bestResultsFilename, bool forceResultsReloading = false)
        {
            if(bestResults != null && !forceResultsReloading)
            {
                return bestResults;
            }

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
            bestResults = results;
            return bestResults;
        }

        public static uint GetBestResultForInstance(string instanceName)
        {
            if(bestResults is null)
            {
                return 0;
            }

            return bestResults.ContainsKey(instanceName) ? bestResults[instanceName] : 0;
        }

        private static Dictionary<string, uint> bestResults;
    }
}