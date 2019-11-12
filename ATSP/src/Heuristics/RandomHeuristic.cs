using System;
using ATSP.Permutators;

namespace ATSP.Heuristics
{
    public class RandomHeuristic : ATSPHeuristic
    {
        public RandomHeuristic()
            : base()
        {
        }

        public RandomHeuristic(double timeoutInMillis)
        {
            Console.WriteLine($"Constraining random execution to {timeoutInMillis} ms");
            this.TimeoutInMillis = timeoutInMillis;
        }

        public override bool IsEnd { get; protected set; }

        public override void NextStep()
        {
            if(Steps == 0)
            {
                currentCost = CalculateCost();
            }

            // permutator.Permutate(Solution);

            // the same code that is in the permutator, but we need to update cost,
            // so `for` should be here
            for(int i=Solution.Length-1;i>0;i--)
            {
                var swapIndex = randomizer.Next(i);
                currentCost = CalculateSwapCost(Solution, currentCost, swapIndex, i);
                swapper.Swap(Solution, ref swapIndex, ref i);
                SaveCost(currentCost);
            }

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