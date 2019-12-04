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

            permutator.Permutate(Solution);

            var temp = CalculateCost();
            if(temp < currentCost)
            {
                NumberOfImprovements++;
            }
            currentCost = temp;
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