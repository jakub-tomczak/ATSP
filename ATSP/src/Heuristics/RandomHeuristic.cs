using System;
using ATSP.Data;
using ATSP.Permutators;

namespace ATSP.Heuristics
{
    public class RandomHeuristic : ATSPHeuristic
    {
        public RandomHeuristic(TravellingSalesmanProblemInstance instance, IPermutator permutator)
            : base(instance, permutator)
        {
            maxSteps = new Random().Next(30, 50);
        }

        public override bool IsEnd { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }

        public override void NextStep()
        {
            step++;
            if(step > maxSteps)
            {
                IsEnd = true;
                return;
            }
        }

        int step = 0;
        int maxSteps = 0;

    }
}