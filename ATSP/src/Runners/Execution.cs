using System.Collections.Generic;

namespace ATSP.Runners
{
    public struct Execution
    {
        public double Time;
        public uint Steps;
        public uint Cost;
        public uint BestKnownCost;
        public uint NumberOfImprovements;
        public List<uint> IntermediateCosts;

        public uint InitialCost;

        public uint[] FinalSolution;
    }
}