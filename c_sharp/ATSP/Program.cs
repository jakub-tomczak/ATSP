using System;
using ATSP.classes;

namespace ATSP
{
    class Program
    {
        static void Main(string[] args)
        {
            var permutator = new DefaultPermutator(1_000_000, 0);
            var timer = new TimeCounter<bool>().Run(() => {
                permutator.Permutate();
                return true;
            });
            Console.WriteLine($"Total mean elapsed time in milliseconds {timer.ElapsedMillis}, iterations {timer.Iterations}");
        }
    }
}
