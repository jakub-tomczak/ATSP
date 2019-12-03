using System;
using ATSP.Permutators;

namespace ATSP.Heuristics
{
    public class RandomWalkHeuristic : ATSPHeuristic
    {
        public RandomWalkHeuristic()
            : base()
        {
        }

        public RandomWalkHeuristic(double timeoutInMillis)
        {
            Console.WriteLine($"Constraining random walk execution to {timeoutInMillis} ms");
            this.TimeoutInMillis = timeoutInMillis;
        }

        public override bool IsEnd { get; protected set; }

        public override void NextStep()
        {
            if(Steps == 0)
            {
                currentCost = CalculateCost();
            }
            (var i, var j) = GetIndicesForSwap(Solution.Length);
            // check whether this is the improvement
            var temp = CalculateSwapCost(Solution, currentCost, i, j);
            if(temp < currentCost)
            {
                NumberOfImprovements++;
                currentCost = temp;
                swapper.Swap(Solution, ref j, ref i);
            }
            SaveCost(currentCost);
            Steps++;
        }

        public override void Reset()
        {
            base.Reset();
        }
        uint currentCost = 0;
        Random randomizer = new Random();
        ISwapper swapper = new DefaultSwapper();
    }
}