using System;
using ATSP.Permutators;

namespace ATSP.Heuristics
{
    public class RandomSolutionInitializer : ISolutionInitializer
    {
        public RandomSolutionInitializer()
        {
            permutator = new DefaultPermutator().SetSeed(this.Seed);
        }

        public uint[] InitializeSolution(int size)
        {
            var solution = new uint[size];
            randomizer = new Random(this.Seed);

            for(uint i=0;i<solution.Length;i++)
            {
                solution[i] = i;
            }

            permutator.Permutate(solution);
            return solution;
        }

        public int Seed
        {
            get => seed;
            set
            {
                if(value != seed)
                {
                    seed = value;
                    randomizer = new Random(value);
                    permutator.SetSeed(value);
                }
            }
        }
        private int seed = 0;
        private Random randomizer = new Random(0);
        private IPermutator permutator;

    }
}