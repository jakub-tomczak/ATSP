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

            SaveCost(currentCost);
            Steps++;
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