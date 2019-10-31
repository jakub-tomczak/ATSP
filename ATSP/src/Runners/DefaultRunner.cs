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
            Console.WriteLine($"Mean elapsed time in milliseconds {timer.MeanIterationTime}, iterations {timer.Iterations}");

            return new ExperimentResult();
        }
    }
}