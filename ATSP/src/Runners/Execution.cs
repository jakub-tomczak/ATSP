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

        public double Quality => (double)(Cost - BestKnownCost) / BestKnownCost ;

        // it is easier to calculate effectiveness after importing data to python
        // effectiveness = 
        //  (Number_of_improvements/maxImprovements) / (Execution_time / meanExecutionTime * Execution_steps / maxSteps * Quality)
        // public double CalculateEffectiveness(uint worstResult)
        //     => Effectiveness = 1 - (double)(Cost - BestKnownCost) / (worstResult - BestKnownCost);


        public double InitialToFinalExecutionRatio
            => (double)(InitialCost) / Cost;

        public double Effectiveness { get; private set; }

        public uint[] FinalSolution { get; set; }
    }
}