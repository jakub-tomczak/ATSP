using System;
using System.Collections.Generic;

namespace ATSP.Runners
{
    public class CSVResultSaver : IResultSaver
    {
        public void SaveResults(List<ExperimentResult> experimentsResults)
        {
            foreach(var experiment in experimentsResults)
            {
                Console.WriteLine($"Saving an experiment with {experiment.NumberOfExecutions} executions.");
            }
        }
    }
}