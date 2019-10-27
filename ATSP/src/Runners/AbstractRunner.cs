using ATSP.Data;
using ATSP.Permutators;

namespace ATSP.Runners
{
    public abstract class AbstractRunner : IRunner
    {
        public abstract void Run(int arraySize, int seed);
        public IRunner UseInstance(TravellingSalesmanProblemInstance instance)
        {
            this.instance = instance;
            return this;
        }

        public IRunner UsePermutator(IPermutator permutator)
        {
            this.permutator = permutator;
            return this;
        }

        protected TravellingSalesmanProblemInstance instance;
        protected IPermutator permutator;
    }
}