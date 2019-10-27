using System;

namespace ATSP.Heuristics
{
    public class RandomHeuristic : ATSPHeuristic
    {
        public RandomHeuristic()
            : base()
        {
            maxSteps = new Random().Next(30000, 50000);
        }

        public override bool IsEnd { get; protected set; }

        public override void NextStep()
        {
            step++;
            if(step > maxSteps)
            {
                IsEnd = true;
                Console.WriteLine($"Steps {step}, total cost {CalculateCost()}");
                PrintSolution();
            }
        }

        public override void Reset()
        {
            base.Reset();
            maxSteps = new Random().Next(30000, 50000);
        }

        int maxSteps = 0;

    }
}