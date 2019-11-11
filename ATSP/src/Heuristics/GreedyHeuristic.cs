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
            if(Steps == 0)
            {
                currentCost = CalculateCost();
            }
            uint CurrSolutionCost = currentCost;
            var numberOfImprovements = 0;
            for(int i = 0; i < size ; i++)
            {
                for(int j = i+1; j < size ; j++)
                {
                    CurrSolutionCost = CalculateSwapCost(Solution, currentCost, i, j);
                    Swapper.Swap(Solution, i, j);
                    if(CurrSolutionCost < currentCost)
                    {
                        numberOfImprovements++;
                        currentCost = CurrSolutionCost;
                        break;
                    }
                    else
                    {
                        Swapper.Swap(Solution, i, j);
                    }
                    Steps++;
                    SaveCost(currentCost);
                }
            }
            IsEnd = numberOfImprovements == 0;
        }

        uint currentCost = 0;
    }
}