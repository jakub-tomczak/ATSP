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
            var SourceSolution = Solution;
            var BestSolution = Solution;
            uint BestSolutionCost = CalculateCost();
            uint CurrSolutionCost = BestSolutionCost;
            for(int i = 0; i < size ; i++)
            {
                for(int j = 0; j < size ; j++)
                {
                    if(i != j){
                        Solution = SourceSolution;
                        Swapper.Swap(Solution,i,j);
                        CurrSolutionCost = CalculateCost();
                        if(BestSolutionCost > CurrSolutionCost){
                            BestSolution = Solution;
                            BestSolutionCost = CurrSolutionCost;
                        }
                    }
                }
            }
            Solution = BestSolution;
            PrintSolution();
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


    }
}