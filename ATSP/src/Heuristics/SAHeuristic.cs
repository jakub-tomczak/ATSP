using System;



namespace ATSP.Heuristics{
    public class SAHeuristic: ATSPHeuristic{

        public Permutators.DefaultSwapper Swapper = new Permutators.DefaultSwapper();

        uint currentCost = 0;

        int temp_iteration = 0;
        int  cool_iteration = 0;


        public float T{get;set;}

        public float alfa = 0.95f;

        

        public float minimalT = 0.01f;
        Random rd = new Random();
        public int maxsteps{get;set;}
        public SAHeuristic() : base()
        {
            T = 0.9f;
            maxsteps = 1000;
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
            if(Steps==0)
            {
                currentCost = CalculateCost();
            }
            temp_iteration=0;
            uint bestSolutionCost = currentCost;
            var improvements = 0;
            var bestChange = (firstIndex: 0, secondIndex: 0, cost: bestSolutionCost);
            int i = 0 ;
            int j = 0;
            while(temp_iteration<maxsteps)
            {
                temp_iteration++;
                i = rd.Next(Solution.Length);
                j = rd.Next(Solution.Length);
                bestSolutionCost = CalculateSwapCost(Solution,currentCost,i,j);
                if((bestSolutionCost<bestChange.cost)||(rd.NextDouble()<=T)){
                    bestChange = (i,j,bestSolutionCost);
                    improvements++;
                }
            }
            if(improvements>0)
            {
                NumberOfImprovements++;
                currentCost = CalculateSwapCost(Solution, currentCost, bestChange.firstIndex, bestChange.secondIndex);
                Swapper.Swap(Solution, bestChange.firstIndex, bestChange.secondIndex);
            }


            IsEnd = (improvements == 0) || (T <= minimalT);
            T *= alfa;

            Steps++;

        }

    }
}