using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ATSP.Runners
{
    public class DefaultRunner: TimeCounter, IRunner
    {
        public ExperimentResult Run(Experiment experiment)
        {
            experimentToRun = experiment;
            var result = new ExperimentResult();

            result.Executions = Run();
            result.MeanExecutionTime = MeanExecutionTime;

            return result;
        }

        public override List<Execution> Run()
        {
            if(experimentToRun is null)
            {
                throw new ArgumentException("Experiment is not specified.");
            }

            var executions = new List<Execution>();
            NumberOfExecutions = 0;
            var timer = new Stopwatch();
            do
            {
                timer.Restart();

                experimentToRun.Heuristic.Reset();
                experimentToRun.Heuristic.Solution = experimentToRun.Initializer.InitializeSolution(experimentToRun.Instance.N);
                while(!experimentToRun.Heuristic.IsEnd)
                {
                    experimentToRun.Heuristic.NextStep();
                }

                timer.Stop();
                var time = (ulong)timer.ElapsedTicks;
                totalTime += time;
                NumberOfExecutions++;

                executions.Add(new Execution()
                {
                    Time = TicksToMillis(time),
                    Steps = experimentToRun.Heuristic.Steps,
                    Cost = experimentToRun.Heuristic.CalculateCost(),
                    IntermediateCosts = experimentToRun.Heuristic.IntermediateCosts
                });
            } while( NumberOfExecutions < minExecutions );
            Console.WriteLine($"Mean execution time in milliseconds {MeanExecutionTime}, number of the algorithm executions {NumberOfExecutions}");
            return executions;
        }

        Experiment experimentToRun = null;
    }
}