using System;

namespace ATSP.classes
{
    public class ClassBasedRunner: IRunner
    {
        public void Run(int arraySize, int seed)
        {
            var permutator = new DefaultPermutator(arraySize, seed);
            var timer = new TimeCounter().Run(() => {
                permutator.Permutate();
            });
            Console.WriteLine($"Total mean elapsed time in milliseconds {timer.ElapsedMillis}, iterations {timer.Iterations}");
        }
    }
}