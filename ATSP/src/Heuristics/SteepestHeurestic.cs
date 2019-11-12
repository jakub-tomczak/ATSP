using System;



namespace ATSP.Heuristics{
    public class SteepestHeurestic: ATSPHeuristic{

        public Permutators.DefaultSwapper Swapper = new Permutators.DefaultSwapper();

        public SteepestHeurestic() : base()
        {

        }

        public override void Reset()
        {
            base.Reset();
        }
        public override bool IsEnd { get; protected set; }


        public override void NextStep()
        {
            if(Steps == 0)
            {
                currentCost = CalculateCost();
            }
            uint bestSolutionCost = currentCost;
            var improvements = 0;
            var bestChange = (firstIndex: 0, secondIndex: 0, cost: bestSolutionCost);

            for(int i = 0; i < Solution.Length ; i++)
            {
                for(int j = i+1; j < Solution.Length ; j++)
                {
                    bestSolutionCost = CalculateSwapCost(Solution, currentCost, i, j);
                    if(bestSolutionCost < bestChange.cost)
                    {
                        NumberOfImprovements++;
                        improvements++;
                        bestChange = (i, j, bestSolutionCost);
                    }
                    Steps++;
                    SaveCost(currentCost);
                }
            }
            if(improvements > 0)
            {
                NumberOfImprovements++;
                currentCost = CalculateSwapCost(Solution, currentCost, bestChange.firstIndex, bestChange.secondIndex);
                Swapper.Swap(Solution, bestChange.firstIndex, bestChange.secondIndex);
            }
            IsEnd = improvements == 0;
        }



        uint currentCost = 0;
    }
}