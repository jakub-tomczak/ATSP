using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ATSP.Runners
{
    public class DefaultRunner: TimeCounter, IRunner
    {
        public ExperimentResult Run(Experiment experiment)
        {
            experimentToRun = experiment;
            var result = new ExperimentResult();

            result.InstanceSize = experiment.Instance.N;
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
                    if(experimentToRun.Heuristic.TimeoutInMillis > 0.0f && timer.ElapsedMilliseconds > experimentToRun.Heuristic.TimeoutInMillis - 1e-2)
                    {
                        break;
                    }
                    experimentToRun.Heuristic.NextStep();
                }

                timer.Stop();
                var time = (ulong)timer.ElapsedTicks;
                totalTime += time;
                NumberOfExecutions++;

                if(VerifySolution(experimentToRun.Heuristic.Solution))
                {
                    executions.Add(new Execution()
                    {
                        AlgorithmName = experimentToRun.Name,
                        ExecutionID = NumberOfExecutions,
                        Time = TicksToMillis(time),
                        Steps = experimentToRun.Heuristic.Steps,
                        Cost = experimentToRun.Heuristic.CalculateCost(),
                        IntermediateCosts = experimentToRun.Heuristic.IntermediateCosts,
                        BestKnownCost = experimentToRun.Instance.BestKnownCost,
                        NumberOfImprovements = experimentToRun.Heuristic.NumberOfImprovements,
                        InitialCost = experimentToRun.Heuristic.InitialCost,
                        FinalSolution = experimentToRun.Heuristic.Solution
                    });
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Experiment {experimentToRun.Name}, execution no {NumberOfExecutions} wrong result.");
                    Console.ResetColor();
                }
            } while( NumberOfExecutions < minExecutions );
            Console.WriteLine($"Mean execution time in milliseconds {MeanExecutionTime}, number of the algorithm executions {NumberOfExecutions}");
            return executions;
        }

        private bool VerifySolution(uint []solution)
        {
            return solution.Length == solution.Distinct().Count();
        }

        Experiment experimentToRun = null;


        public ulong MinExecutions { get => minExecutions; set => minExecutions = value; }
        private ulong minExecutions = 10;
    }
}