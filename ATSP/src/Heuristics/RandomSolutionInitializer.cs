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

            new DefaultPermutator().SetSeed(this.Seed).Permutate(solution);

            return solution;
        }

        public int Seed { get; private set; } = 0;

        private Random randomizer = new Random(0);

    }
}