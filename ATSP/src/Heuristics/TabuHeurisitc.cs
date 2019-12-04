using System;
using System.Collections.Generic;
using System.Linq;

//commit nie poszedł

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
            var n = Solution.Length;
            for(var i=0;i<n;i++)
            {
                for(var j =0 ; j < n; j++)
                {
                    if(i!=j){
                        var swapCost = CalculateSwapCost(Solution, currentCost, i, j);
                        masterList.Add(Tuple.Create(i, j, swapCost));
                    }
                }
            }
            masterList.Sort((x, y) => x.Item3.CompareTo(y.Item3));
        }

        public override void NextStep()
        {
            if(Steps==0)
            {
                currentCost = CalculateCost();
                initTabuList();
                bestsolutionCost = currentCost;
            }
            generateMasterList();

            var improvements = 0;
            var bestChange = (firstIndex: 0, secondIndex: 0, cost: bestsolutionCost);

            for(int i = 0; i < masterList.Count() * .1; i++)
            {
                var x = masterList[i].Item1;
                var y = masterList[i].Item2;
                var newCost = CalculateSwapCost(Solution, currentCost, x, y);

                if((tabuList[x,y]==0)||(newCost<=currentCost))
                {
                    currentCost = newCost;
                    tabuList[x,y] = Solution.Length;
                    if(bestsolutionCost >= currentCost)
                    {
                        bestsolutionCost = currentCost;
                        bestChange = (firstIndex: x,secondIndex:  y,cost: bestsolutionCost);
                        NumberOfImprovements++;
                    }
                    improvements++;
                    break;
                }
            }

            if(Steps % 1000 == 0)
            {
                Console.WriteLine($"{Instance.Name}, {Steps}");
            }

            if(improvements>0)
            {
                NumberOfImprovements++;
                currentCost = CalculateSwapCost(Solution,currentCost,bestChange.firstIndex,bestChange.secondIndex);
                Swapper.Swap(Solution, bestChange.firstIndex, bestChange.secondIndex);
            }

            IsEnd = improvements == 0;
            IsEnd = IsEnd || (Steps>50000);
            decrementTabuList();
            Steps++;
            if(IsEnd){
                currentCost = CalculateSwapCost(Solution,currentCost,bestChange.firstIndex,bestChange.secondIndex);
                Swapper.Swap(Solution, bestChange.firstIndex, bestChange.secondIndex);
            }

        }

    }
}