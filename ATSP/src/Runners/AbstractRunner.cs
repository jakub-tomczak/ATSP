using ATSP.Data;

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

        private TravellingSalesmanProblemInstance instance;
    }
}