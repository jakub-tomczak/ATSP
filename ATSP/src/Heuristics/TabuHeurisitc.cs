using System;
using System.Collections.Generic;
using System.Linq;

namespace ATSP.Heuristics{
    public class TabuHeuristic: ATSPHeuristic{

        public Permutators.DefaultSwapper Swapper = new Permutators.DefaultSwapper();

        uint currentCost = 0;
        int [,] tabuList;
        uint bestsolutionCost=0;
        Random rd = new Random();

        List<Tuple<int, int, uint>> masterList = new List<Tuple<int, int, uint>>();
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

        void generateMasterList()
        {
            masterList.Clear();
            var numberOfCandidates = Solution.Length*Solution.Length*.2;
            for(var i=0;i<numberOfCandidates;i++)
            {
                (var candidate1, var candidate2) = GetIndicesForSwap(Solution.Length);
                var swapCost = CalculateSwapCost(Solution, currentCost, candidate1, candidate2);
                masterList.Add(Tuple.Create(candidate1, candidate2, swapCost));
            }
            masterList.Sort((x, y) => x.Item3.CompareTo(y.Item3));
        }

        void updateMasterList(){
            var numberOfCandidates = Solution.Length*Solution.Length*.2;
            for(var i = 0; i < numberOfCandidates; i++){
                var swapCost = CalculateSwapCost(Solution,currentCost, masterList[i].Item1,masterList[i].Item2);
                masterList[i] = Tuple.Create(masterList[i].Item1,masterList[i].Item2,swapCost);
            }


        }

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