using System.Collections.Generic;

namespace ATSP.Runners
{
    public struct Execution
    {
        public double Time;
        public uint Steps;
        public uint Cost;
        public uint BestKnownCost;
        public List<uint> IntermediateCosts;
    }
}