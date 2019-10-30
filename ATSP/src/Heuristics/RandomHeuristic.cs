using System;
using ATSP.Permutators;

namespace ATSP.Heuristics
{
    public class RandomHeuristic : ATSPHeuristic
    {
        public RandomHeuristic()
            : base()
        {
            GetRandomMaxSteps();
        }

        public override bool IsEnd { get; protected set; }

        public override void NextStep()
        {
            if(step == 0)
            {
                currentCost = CalculateCost();
            }

            step++;
            if(step > maxSteps)
            {
                IsEnd = true;
                // PrintSolution();
            }

            for(int i=Solution.Length-1;i>0;i--)
            {
                var swapIndex = randomizer.Next(i);
                swapper.Swap(Solution, ref swapIndex, ref i);
                if(CalculateCost() < currentCost)
                {
                    // swap gives bteter result
                    currentCost = CalculateCost();
                }
                else
                {
                    // restore previous state
                    swapper.Swap(Solution, ref swapIndex, ref i);
                }
            }
        }

        public override void Reset()
        {
            base.Reset();
            GetRandomMaxSteps();
        }

        private void GetRandomMaxSteps()
        {
            maxSteps = randomizer.Next(30000, 50000);
        }

        int maxSteps = 30;
        uint currentCost = 0;
        Random randomizer = new Random();
        ISwapper swapper = new DefaultSwapper();
    }
}