using System;

namespace ATSP.Heuristics{
    public class SAHeuristic: ATSPHeuristic{

        public Permutators.DefaultSwapper Swapper = new Permutators.DefaultSwapper();

        uint currentCost = 0;

        int temp_iteration = 0;

        private double temperature { get; set; }

        public float alfa = 0.95f;

        private float minimalT = 0.01f;
        private float initialAcceptanceCoefficient = .98f;

        // the fraction of the initial solution that will achieve the final solution
        // used to estimate the initial temperature
        public float ExpectedInitialSolutionImprovementFraction = .3f;
        Random rd = new Random();
        public int coolingDownTime { get; set; }
        public SAHeuristic() : base()
        {
            coolingDownTime = 1000;
        }
        public SAHeuristic(int coolingDownTime, float initialAcceptanceCoefficient)
            :base()
        {
            this.coolingDownTime = coolingDownTime;
            this.initialAcceptanceCoefficient = initialAcceptanceCoefficient;
        }

        public override void Reset()
        {
            base.Reset();
        }
        public override bool IsEnd { get; protected set; }

        public override void NextStep()
        {
            if(Steps==0)
            {
                currentCost = CalculateCost();
                temperature = CalculateInitialTemperature(currentCost);
            }
            temp_iteration=0;
            uint bestSolutionCost = currentCost;
            var improvements = 0;
            // do we need to store change? I think that we can update the solution immediately
            // var bestChange = (firstIndex: 0, secondIndex: 0, cost: bestSolutionCost);
            int i = 0 ;
            int j = 0;
            while(temp_iteration < coolingDownTime)
            {
                temp_iteration++;
                i = rd.Next(Solution.Length);

                // get j such that i!=j
                while(i == j)
                {
                    j = rd.Next(Solution.Length);
                }

                bestSolutionCost = CalculateSwapCost(Solution,currentCost,i,j);
                if(bestSolutionCost > currentCost ||
                    AcceptanceProbability(currentCost, bestSolutionCost) > rd.NextDouble())
                {
                    // currentCost = CalculateSwapCost(Solution, currentCost, bestChange.firstIndex, bestChange.secondIndex);
                    // Swapper.Swap(Solution, bestChange.firstIndex, bestChange.secondIndex);
                    currentCost = CalculateSwapCost(Solution, currentCost, i, j);
                    Swapper.Swap(Solution, i, j);
                    improvements++;
                }
                Steps++;
            }

            IsEnd = (improvements == 0) || (temperature <= minimalT);

            // UpdateTemperature
            temperature *= alfa;

            Steps++;

        }

        private double AcceptanceProbability(uint currentSolutionCost, uint newSolutionCost)
            => Math.Exp((currentSolutionCost - newSolutionCost)/temperature);

        private double CalculateInitialTemperature(uint initialCost)
            => (ExpectedInitialSolutionImprovementFraction*initialCost) / Math.Log(initialAcceptanceCoefficient);

    }
}