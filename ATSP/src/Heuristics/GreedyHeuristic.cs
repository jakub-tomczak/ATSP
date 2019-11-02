using System;



namespace ATSP.Heuristics{
    public class GreedyHeuristic: ATSPHeuristic{

        public Permutators.DefaultSwapper Swapper = new Permutators.DefaultSwapper();

        public GreedyHeuristic() : base()
        {

        }

        public override void Reset()
        {
            base.Reset();
        }
        public override bool IsEnd { get; protected set; }


        public override void NextStep()
        {
            int size = Solution.Length;
            currentCost = CalculateCost();
            uint CurrSolutionCost = currentCost;
            for(uint i = 0; i < size ; i++)
            {
                uint nearestNeighbourCost = UInt32.MaxValue;
                uint nearestNeighbour = i;
                for(uint j = i + 1; j < size ; j++)
                {
                    if(vertices[i, j] < nearestNeighbourCost)
                    {
                        nearestNeighbourCost = vertices[i, j];
                        nearestNeighbour = j;
                    }
                }
            }
            IsEnd = true;
        }

        private uint CalculateCurrSolution(uint[] CurrSolution){
            var cost = 0u;
            for(int i=0;i<CurrSolution.Length;i++)
            {
                cost += vertices[CurrSolution[i], CurrSolution[(i+1)%CurrSolution.Length]];
            }
            return cost;
        }

        uint currentCost = 0;
    }
}