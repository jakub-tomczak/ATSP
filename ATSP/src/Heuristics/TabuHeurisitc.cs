using System;



namespace ATSP.Heuristics{
    public class TabuHeuristic: ATSPHeuristic{

        public Permutators.DefaultSwapper Swapper = new Permutators.DefaultSwapper();

        uint currentCost = 0;
        int [,] tabuList;
        uint bestsolutionCost=0;
        Random rd = new Random();
        public TabuHeuristic() : base()
        {
        }

        public override void Reset()
        {
            base.Reset();
        }
        public override bool IsEnd { get; protected set; }

        void initTabuList(){
            int n = Solution.Length;
            tabuList = new int[n,n];
            for(int i = 0; i < n; i++){
                for(int j = 0; j < n; j++){
                    tabuList[i,j] = 0;
                }
            }
        }

        void decrementTabuList(){
            int n = Solution.Length;
            for(int i = 0 ; i < n ; i++)
            {
                for(int j = 0; j < n ; j++)
                {
                    tabuList[i,j] = (tabuList[i,j]>0) ? tabuList[i,j]-- : 0; 
                }
            }
        }

        // master list: poleca komos
        

        public override void NextStep()
        {
            if(Steps==0)
            {
                currentCost = CalculateCost();
                initTabuList();
            }
        bestsolutionCost = currentCost;
        var improvements = 0;
        var bestChange = (firstIndex: 0, secondIndex: 0, cost: bestsolutionCost);

            for(int i = 0; i < Solution.Length; i++)
            {
                for(int j = 0; j < Solution.Length; j++)
                {
                    bestsolutionCost = CalculateSwapCost(Solution,currentCost,i,j);
                    if((bestsolutionCost<bestChange.cost)&&(tabuList[i,j]<=0)){
                        NumberOfImprovements++;
                        improvements++;
                        bestChange = (i,j,bestsolutionCost);
                        tabuList[i,j] = 4;
                    }
                    SaveCost(currentCost);
                    Steps++;
                }
            }
            if(improvements>0)
            {
                NumberOfImprovements++;
                currentCost = CalculateSwapCost(Solution,currentCost,bestChange.firstIndex,bestChange.secondIndex);
                Swapper.Swap(Solution, bestChange.firstIndex, bestChange.secondIndex);
            }

            IsEnd = improvements == 0;
            decrementTabuList();
            Steps++;

        }

    }
}