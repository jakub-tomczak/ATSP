using System.Collections.Generic;


namespace ATSP.Runners
{
    public class ExperimentResult
    {
        public ulong NumberOfExecutions = 0;
        public double MeanExecutionTime = 0.0;

        public List<Execution> Executions = new List<Execution>();
    }
}