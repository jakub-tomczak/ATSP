using System.Collections.Generic;


namespace ATSP.Runners
{
    public class ExperimentResult
    {
        public int NumberOfExecutions => Executions.Count;
        public double MeanExecutionTime = 0.0;
        public string InstanceName { get; set; }

        public List<Execution> Executions = new List<Execution>();
    }
}