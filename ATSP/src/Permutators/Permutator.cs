using System;

namespace ATSP.Permutators
{
    public abstract class Permutator: IPermutator
    {
        public abstract void Permutate(uint []array);

        public IPermutator SetSeed(int seed)
        {
            this.Seed = seed;
            randomizer = new Random(seed);
            return this;
        }

        public IPermutator UseSwapper(ISwapper swapper)
        {
            this.swapper = swapper;
            return this;
        }

        protected Random randomizer = new Random(0);
        public int Seed { get; private set; } = 0;

        protected ISwapper swapper = new DefaultSwapper();
    }
}