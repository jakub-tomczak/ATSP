using System;

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
            step++;
            if(step > maxSteps)
            {
                IsEnd = true;
                PrintSolution();
            }
            permutator.Permutate(Solution);
        }

        public override void Reset()
        {
            base.Reset();
            GetRandomMaxSteps();
        }

        private void GetRandomMaxSteps()
        {
            maxSteps = new Random(0).Next(30000, 50000);
        }

        int maxSteps = 0;

    }
}