using System;
using ATSP.classes;

namespace ATSP
{
    class Program
    {
        static void Main(string[] args)
        {
            var permutator = new DefaultPermutator(10000000, 0);
            var timer = new TimeCounter().Run(() => {
                permutator.Permutate();
            });
            Console.WriteLine($"Total mean elapsed time in milliseconds {timer.ElapsedMillis}, iterations {timer.Iterations}");
        }
    }
}
