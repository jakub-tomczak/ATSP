using System.Collections.Generic;

namespace ATSP.Runners
{
    public class Execution
    {
        public override bool Equals(object obj)
        {
            if(obj is Execution execution)
            {
                return execution.AlgorithmName == this.AlgorithmName &&
                    execution.ExecutionID == this.ExecutionID;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return AlgorithmName.GetHashCode() + this.ExecutionID.GetHashCode();
        }

        public string AlgorithmName { get; set; }
        public ulong ExecutionID { get; set; }
        public double Time { get; set; }
        public uint Steps { get; set; }
        public uint Cost { get; set; }
        public uint BestKnownCost { get; set;}
        public uint NumberOfImprovements { get; set; }
        public List<uint> IntermediateCosts { get; set; }
        public double SimilarityWithBest { get; set; }
        public uint InitialCost { get; set; }

        public uint[] FinalSolution { get; set; }
    }
}