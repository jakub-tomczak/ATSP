namespace ATSP.Permutators
{
    public interface IPermutator
    {
        void Permutate(uint []array);
        IPermutator UseSwapper(ISwapper swapper);

        IPermutator SetSeed(int seed);
    }
}