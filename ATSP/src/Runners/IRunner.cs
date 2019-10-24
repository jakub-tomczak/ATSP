using System;

namespace ATSP.Runners
{
    public interface IRunner
    {
        void Run(int arraySize, int seed);
    }
}