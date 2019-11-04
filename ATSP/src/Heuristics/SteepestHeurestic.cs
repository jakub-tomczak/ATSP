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
            int size = Solution.Length;
            if(Steps == 0)
            {
                currentCost = CalculateCost();
            }
            uint CurrSolutionCost = currentCost;
            var BestSolution = new uint[size];
            Solution.CopyTo(BestSolution,0);

            for(int i = 0; i < size ; i++)
            {
                for(int j = 0; j < size ; j++)
                {
                    if(i != j){
                        Swapper.Swap(Solution, i, j);
                        CurrSolutionCost = UpdateCost(Solution, currentCost, i, j);
                        if(CurrSolutionCost < currentCost)
                        {
                            currentCost = CurrSolutionCost;
                            Solution.CopyTo(BestSolution,0);
                        }
                        Steps++;
                        SaveCost(currentCost);
                        Swapper.Swap(Solution, i, j);
                    }
                }
            }
            BestSolution.CopyTo(Solution,0);
            IsEnd = true;
        }



        uint currentCost = 0;
    }
}