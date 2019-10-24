using System;
using ATSP.Permutators;

namespace ATSP.Runners
{
    public class ClassBasedRunner: IRunner
    {
        public void Run(int arraySize, int seed)
        {
            var permutator = new DefaultPermutator(arraySize, seed);
            var timer = new TimeCounter().Run(() => {
                permutator.Permutate();
            });
            Console.WriteLine($"mean elapsed time in milliseconds {timer.MeanIterationTime}, iterations {timer.Iterations}");
        }
    }
}