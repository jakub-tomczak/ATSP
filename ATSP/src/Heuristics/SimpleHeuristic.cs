using System;

using System.Linq;

namespace ATSP.Heuristics{
    public class SimpleHeuristic: ATSPHeuristic{

        public Permutators.DefaultSwapper Swapper = new Permutators.DefaultSwapper();

        public SimpleHeuristic() : base()
        {

        }

        public override void Reset()
        {
            base.Reset();
        }
        public override bool IsEnd { get; protected set; }


        public int ChooseClosest(bool[] visited, uint[,] neibours,int currentNode){


            int ClossestID = 0;
            uint ClossestCost = uint.MaxValue;
            for(int i = 0; i < Solution.Length; i++)
            {
                if((!visited[i]) && (i!=currentNode))
                {
                    if(neibours[currentNode,i]<ClossestCost){
                        ClossestCost = neibours[currentNode,i];
                        ClossestID = i;
                    }
                }
                SaveCost();
            }

            return ClossestID;
        }

        public override void NextStep()
        {
            int size = Solution.Length;
            bool[] visited = new bool[size];
            Random r = new Random();
            int currentNode = r.Next(0,size);
            visited[currentNode] = true;
            Solution[0] = (uint) currentNode;
            int i = 1;
            while(!visited.All(x=>x)){
                currentNode = ChooseClosest(visited,vertices,currentNode);
                Solution[i] = (uint) currentNode;
                visited[currentNode] = true;
                i++;
            }


            IsEnd = true;
        }


    }
}