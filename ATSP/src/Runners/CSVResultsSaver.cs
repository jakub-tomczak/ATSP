using System;
using System.IO;

namespace ATSP.Runners
{
    public class CSVResultSaver : IResultSaver
    {
        public string SaveDirectory { get; set; }
        public string Extension { get => "csv"; }

        public void SaveResult(ExperimentResult experimentResults)
        {
            if(string.IsNullOrEmpty(SaveDirectory))
            {
                throw new ArgumentException("SaveDirectory is not set");
            }

            if(!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }
            var experimentsDirectory = Path.Combine(SaveDirectory, experimentResults.InstanceName, experimentResults.Name);
            if(!Directory.Exists(experimentsDirectory))
            {
                Directory.CreateDirectory(experimentsDirectory);
            }

            var executionIter = 0;
            foreach(var execution in experimentResults.Executions)
            {
                var path = Path.Combine(experimentsDirectory, $"{experimentResults.Name}_{experimentResults.InstanceName}_{executionIter}.{Extension}");
                using(var file = new StreamWriter(path))
                {
                    file.WriteLine($"Instance Name;{experimentResults.InstanceName}");
                    file.WriteLine($"Mean Execution Time;{experimentResults.MeanExecutionTime}");
                    file.WriteLine($"Execution time;{execution.Time}");
                    file.WriteLine($"Execution steps;{execution.Steps}");
                    file.WriteLine($"Execution final cost;{execution.Cost}");
                    file.WriteLine($"Best known cost;{execution.BestKnownCost}");
                    foreach(var cost in execution.IntermediateCosts)
                    {
                        file.WriteLine(
                            $"{cost}"
                        );
                    }
                }
                executionIter++;
            }
        }
    }
}