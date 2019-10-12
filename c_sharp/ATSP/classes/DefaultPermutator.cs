using System;
using System.Linq;

namespace ATSP.classes
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
                Swap(ref swapIndex, ref i);
            }

            return indices;
        }
    }
}