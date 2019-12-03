using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ATSP.Runners
{
    public class CSVOneFileResultSaver : IResultSaver
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
            var experimentsDirectory = Path.Combine(SaveDirectory, experimentResults.InstanceName);
            if(!Directory.Exists(experimentsDirectory))
            {
                Directory.CreateDirectory(experimentsDirectory);
            }

            var path = Path.Combine(experimentsDirectory,
                $"{experimentResults.Name}_{experimentResults.InstanceName}.{Extension}");
            using(var file = new StreamWriter(path))
            {
                var header = "Algorithm;Instance name;Instance size;Number of executions;Execution time;" +
                    "Execution steps;Final cost;Initial quality;Best known cost;Similarity;Number of improvements;" +
                    "Quality;Initial to final execution ratio";

                file.WriteLine(header.Replace(' ', '_'));
                var line = new StringBuilder();
                foreach(var execution in experimentResults.Executions)
                {
                    line.Clear();
                    line.Append($"{experimentResults.Name};");
                    line.Append($"{experimentResults.InstanceName};");
                    line.Append($"{experimentResults.InstanceSize};");
                    line.Append($"{experimentResults.NumberOfExecutions};");
                    line.Append($"{execution.Time};");
                    line.Append($"{execution.Steps};");
                    line.Append($"{execution.Cost};");
                    line.Append($"{execution.InitialQuality};");
                    line.Append($"{execution.BestKnownCost};");
                    line.Append($"{execution.SimilarityWithBest};");
                    line.Append($"{execution.NumberOfImprovements};");
                    line.Append($"{execution.Quality};");
                    line.Append($"{execution.InitialToFinalExecutionRatio}");
                    file.WriteLine(line);
                }
            }
        }
    }
}