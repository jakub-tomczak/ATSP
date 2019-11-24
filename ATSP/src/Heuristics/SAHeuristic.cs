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
        public float ExpectedInitialSolutionImprovementFraction = .90f;
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
                // Console.WriteLine($"SA, instance {Instance.Name}, fraction {ExpectedInitialSolutionImprovementFraction}");
                currentCost = CalculateCost();
                temperature = CalculateInitialTemperature(currentCost);
            }
            temp_iteration=0;
            uint nextSolutionCost = currentCost;
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
                do
                {
                    j = rd.Next(Solution.Length);
                } while(i == j);

                nextSolutionCost = CalculateSwapCost(Solution,currentCost,i,j);
                if(nextSolutionCost <= currentCost ||
                    AcceptanceProbability(currentCost, nextSolutionCost) > rd.NextDouble())
                {
                    currentCost = CalculateSwapCost(Solution, currentCost, i, j);
                    Swapper.Swap(Solution, i, j);
                    improvements++;
                }
                SaveCost(currentCost);
                Steps++;
            }

            // UpdateTemperature
            temperature *= alfa;
            IsEnd = temperature <= minimalT;
        }

        private double AcceptanceProbability(uint currentSolutionCost, uint newSolutionCost)
        {
            var delta = (float)((int)currentSolutionCost - newSolutionCost);
            return Math.Exp(delta/temperature);
        }

        private double CalculateInitialTemperature(uint initialCost)
            => -(ExpectedInitialSolutionImprovementFraction*initialCost) / Math.Log(initialAcceptanceCoefficient);

    }
}