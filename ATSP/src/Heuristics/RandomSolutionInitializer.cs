using System;
using ATSP.Permutators;

namespace ATSP.Heuristics
{
    public class RandomSolutionInitializer : ISolutionInitializer
    {
        public uint[] InitializeSolution(int size)
        {
            var solution = new uint[size];
            randomizer = new Random(this.Seed);

            for(uint i=0;i<solution.Length;i++)
            {
                solution[i] = i;
            }

            var permutator = new DefaultPermutator().SetSeed(this.Seed);
            for(var i = 0;i<NumberOfShufflesOnStartup;i++)
            {
                permutator.Permutate(solution);
            }

            return solution;
        }

        public int Seed { get; private set; } = 0;
        public uint NumberOfShufflesOnStartup { get; set; } = 1;
        private Random randomizer = new Random(0);

    }
}