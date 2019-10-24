namespace ATSP.Permutators
{
    public class DefaultPermutator : Permutator, IPermutator
    {
        public DefaultPermutator(int arraySize, int seed)
            : base(arraySize, seed)
        {}
        public uint[] Permutate()
        {
            for(int i=indices.Length-1;i>0;i--)
            {
                var swapIndex = randomizer.Next(i);
                swapper.Swap(indices, ref swapIndex, ref i);
            }

            return indices;
        }

        private ISwapper swapper = new DefaultSwapper();
    }
}