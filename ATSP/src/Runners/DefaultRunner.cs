using System;
namespace ATSP.Runners
{
    public class DefaultRunner: IRunner
    {
        public ExperimentResult Run(Experiment experiment)
        {
            var timer = new TimeCounter().Run(() => {
                experiment.Heuristic.Reset();
                experiment.Heuristic.Solution = experiment.Initializer.InitializeSolution(experiment.Instance.N);
                while(!experiment.Heuristic.IsEnd)
                {
                    experiment.Heuristic.NextStep();
                }
            });
            Console.WriteLine($"Mean execution time in milliseconds {timer.MeanExecutionTime}, number of the algorithm executions {timer.Executions}");

            return new ExperimentResult();
        }
    }
}