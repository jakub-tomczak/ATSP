using ATSP.Data;
using ATSP.Permutators;

namespace ATSP.Runners
{
    public interface IRunner
    {
        IRunner UseInstance(TravellingSalesmanProblemInstance instance);
        IRunner UsePermutator(IPermutator permutator);
        void Run(int arraySize, int seed);
    }
}