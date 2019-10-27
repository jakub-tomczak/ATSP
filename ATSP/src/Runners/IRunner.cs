using ATSP.Data;

namespace ATSP.Runners
{
    public interface IRunner
    {
        IRunner UseInstance(TravellingSalesmanProblemInstance instance);
        void Run(int arraySize, int seed);
    }
}