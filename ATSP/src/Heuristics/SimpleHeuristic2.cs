using System;



namespace ATSP.Heuristics{
    public class SimpleHeuristic2: ATSPHeuristic{

        public Permutators.DefaultSwapper Swapper = new Permutators.DefaultSwapper();

        public SimpleHeuristic2() : base()
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
            var visited = new bool[Solution.Length];

            uint CurrSolutionCost = currentCost;
            for(uint i = 0; i < size - 1; i++)
            {
                uint currentVertex = Solution[i];
                visited[currentVertex] = true;
                uint nearestNeighbourCost = UInt32.MaxValue;
                uint nearestNeighbour = currentVertex;
                for(uint j = 0; j < size ; j++)
                {
                    if(visited[j])
                    {
                        continue;
                    }

                    if(vertices[currentVertex, j] < nearestNeighbourCost)
                    {
                        nearestNeighbourCost = vertices[currentVertex, j];
                        nearestNeighbour = j;
                    }
                }
                Solution[i+1] = nearestNeighbour;
                Steps++;
                SaveCost(currentCost);
            }
            IsEnd = true;
        }

        uint currentCost = 0;
    }
}