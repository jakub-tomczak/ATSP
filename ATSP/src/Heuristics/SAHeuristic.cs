using System;



namespace ATSP.Heuristics{
    public class SAHeuristic: ATSPHeuristic{

        public Permutators.DefaultSwapper Swapper = new Permutators.DefaultSwapper();


        public float T{get;set;}

        public float alfa = 0.95f;

        public float minimalT = 0.01f;
        Random rd = new Random();
        public int maxsteps{get;set;}
        public SAHeuristic() : base()
        {
            T = 0.9f;
            maxsteps = 100;
        }
        public SAHeuristic(float temperature,int maxsteps):base(){
            this.T = temperature;
            this.maxsteps = maxsteps;
        }

        public override void Reset()
        {
            base.Reset();
        }
        public override bool IsEnd { get; protected set; }


        public override void NextStep()
        {
            // TODO : TO JEST ŹLE XDDD I NA 2 ETAP
            uint currentCost = 0;
            int size = Solution.Length;
            currentCost = CalculateCost();
            uint CurrSolutionCost = currentCost;
            var BestSolution = new uint[size];
            Solution.CopyTo(BestSolution,0);
            for(int i = 0 ; i < size ; i++){
                for(int j = 0 ; j < size ; j++){
                        Swapper.Swap(Solution, i, j);
                        CurrSolutionCost = CalculateCost();
                        if((CurrSolutionCost < currentCost) || rd.NextDouble() <= T)
                        {
                            currentCost = CurrSolutionCost;
                            Solution.CopyTo(BestSolution,0);
                            goto End;
                        }
                        Swapper.Swap(Solution, i, j);

                }
            }
            End:
                T *= alfa;
                IsEnd = true;
        }

    }
}