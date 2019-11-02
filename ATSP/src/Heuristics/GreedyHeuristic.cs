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
            for(int i = 0; i < size ; i++)
            {
                for(int j = 0; j < size ; j++)
                {
                    if(i != j){
                        Swapper.Swap(Solution, i, j);
                        CurrSolutionCost = CalculateCost();
                        if(CurrSolutionCost < currentCost)
                        {
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
            }
            IsEnd = true;
        }

        uint currentCost = 0;
    }
}