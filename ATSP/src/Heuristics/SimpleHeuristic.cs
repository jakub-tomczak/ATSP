using System;
using ATSP.Permutators;
using System.Linq;

namespace ATSP.Heuristics{
    public class SimpleHeuristic: ATSPHeuristic{

        public DefaultSwapper swapper = new DefaultSwapper();

        public SimpleHeuristic() : base()
        {

        }

        public override void Reset()
        {
            base.Reset();
        }
        public override bool IsEnd { get; protected set; }


        public int ChooseClosest(bool[] visited, uint[,] neibours,int currentNode){


            var ClossestID = 0;
            var ClossestCost = uint.MaxValue;
            for(var i = 0; i < Solution.Length; i++)
            {
                if(!visited[i])
                {
                    if(neibours[currentNode,i]<ClossestCost){
                        ClossestCost = neibours[currentNode,i];
                        ClossestID = i;
                        NumberOfImprovements++;
                    }
                }
                Steps++;
                SaveCost(currentCost);
            }

            return ClossestID;
        }

        public override void NextStep()
        {
            if(Steps == 0)
            {
                currentCost = CalculateCost();
            }
            var size = Solution.Length;
            var visited = new bool[size];
            var currentNode = (int)Solution[0];
            visited[currentNode] = true;
            var i = 1;
            while(!visited.All(x=>x)){
                currentNode = ChooseClosest(visited, vertices, currentNode);
                currentCost = CalculateSwapCost(Solution, currentCost, i, currentNode);
                swapper.Swap(Solution, i, currentNode);
                visited[currentNode] = true;
                i++;
            }


            IsEnd = true;
        }

        private uint currentCost = 0;

    }
}