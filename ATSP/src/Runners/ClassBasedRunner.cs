using System;
namespace ATSP.Runners
{
    public class ClassBasedRunner: IRunner
    {
        public ExperimentResult Run(Experiment experiment)
        {
            var timer = new TimeCounter().Run(() => {
                experiment.Heuristic.Reset();
                while(!experiment.Heuristic.IsEnd)
                {
                    experiment.Heuristic.NextStep();
                }
            });
            Console.WriteLine($"mean elapsed time in milliseconds {timer.MeanIterationTime}, iterations {timer.Iterations}");

            return new ExperimentResult();
        }
    }
}