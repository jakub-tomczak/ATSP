namespace ATSP.Permutators
{
    public class DefaultPermutator : Permutator, IPermutator
    {
        public uint[] Permutate()
        {
            for(int i=indices.Length-1;i>0;i--)
            {
                var swapIndex = randomizer.Next(i);
                swapper.Swap(indices, ref swapIndex, ref i);
            }

            return indices;
        }

        public IPermutator UseSwapper(ISwapper swapper)
        {
            this.swapper = swapper;
            return this;
        }

        public IPermutator SetSeed(int seed)
        {
            this.seed = seed;
            return this;
        }

        private ISwapper swapper = new DefaultSwapper();
    }
}