using System;
using System.IO;

namespace ATSP.Runners
{
    public class CSVResultSaver : IResultSaver
    {
        public string SaveDirectory { get; set; }

        public void SaveResult(ExperimentResult experimentResults)
        {
            if(string.IsNullOrEmpty(SaveDirectory))
            {
                throw new ArgumentException("SaveDirectory is not set");
            }
            var path = Path.Combine(SaveDirectory, $"{experimentResults.Name}_{experimentResults.InstanceName}");
            using(var file = new StreamWriter(path))
            {
                file.WriteLine("Steps;Time;Cost");
                foreach(var execution in experimentResults.Executions)
                {
                    file.WriteLine(
                        $"{execution.Steps};{execution.Time};{execution.Cost}"
                    );
                }
            }
        }
    }
}