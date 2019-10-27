using System;

namespace ATSP.Permutators
{
    public interface IPermutator
    {
        uint[] Permutate();
        IPermutator UseSwapper(ISwapper swapper);

        IPermutator SetSeed(int seed);
    }
}