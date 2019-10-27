using System;
using ATSP.Data;
using ATSP.Permutators;

namespace ATSP.Runners
{
    public class ClassBasedRunner: AbstractRunner
    {
        public override void Run(int arraySize, int seed)
        {
            var permutator = new DefaultPermutator(arraySize, seed);
            var timer = new TimeCounter().Run(() => {
                permutator.Permutate();
            });
            Console.WriteLine($"mean elapsed time in milliseconds {timer.MeanIterationTime}, iterations {timer.Iterations}");
        }
    }
}