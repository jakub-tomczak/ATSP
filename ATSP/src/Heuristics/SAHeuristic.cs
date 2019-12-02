using System;

namespace ATSP.Heuristics{
    public class SAHeuristic: ATSPHeuristic{

        public Permutators.DefaultSwapper Swapper = new Permutators.DefaultSwapper();

        uint currentCost = 0;

        int temp_iteration = 0;

        private double temperature { get; set; }

        public float alfa = 0.95f;

        private float minimalT = 0.01f;
        private float acceptanceCoefficient = .98f;

        // the fraction of the initial solution that will achieve the final solution
        // used to estimate the initial temperature
        public float ExpectedInitialSolutionImprovementFraction = .90f;
        public int coolingDownTime { get; set; }
        public SAHeuristic() : base()
        {
            coolingDownTime = 1000;
        }
        public SAHeuristic(int coolingDownTime, 
        float acceptanceCoefficient)
            :base()
        {
            this.coolingDownTime = coolingDownTime;
            this.acceptanceCoefficient = acceptanceCoefficient;
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
                var meanDiff = GetMeanInitialSolutionChange(20);
                currentCost = CalculateCost();
                temperature = CalculateInitialTemperature(meanDiff);
            }
            temp_iteration=0;
            uint nextSolutionCost = currentCost;
            var improvements = 0;

            while(temp_iteration < coolingDownTime)
            {
                temp_iteration++;
                (var i, var j) = GetIndicesForSwap(Instance.N);

                nextSolutionCost = CalculateSwapCost(Solution,currentCost,i,j);
                if(nextSolutionCost <= currentCost ||
                    AcceptanceProbability(currentCost, nextSolutionCost) > Randomizer.NextDouble())
                {
                    currentCost = CalculateSwapCost(Solution, currentCost, i, j);
                    Swapper.Swap(Solution, i, j);
                    improvements++;
                    NumberOfImprovements++;
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
            => -(ExpectedInitialSolutionImprovementFraction*initialCost) / Math.Log(acceptanceCoefficient);


        private double CalculateInitialTemperature(double iniitialDelta)
            => -iniitialDelta / Math.Log(acceptanceCoefficient);

        private double GetMeanInitialSolutionChange(int numberOfSamples)
        {
            long improvements = 0;
            var numberOfImprovements = 0;
            Solution = initializer.InitializeSolution(Instance.N);
            var initialCost = CalculateCost();
            do
            {
                (var i, var j) = GetIndicesForSwap(Instance.N);
                var diff = initialCost - (int)CalculateSwapCost(Solution, initialCost, i, j);
                if(diff > 0)
                {
                    numberOfImprovements++;
                    improvements += diff;
                }
            } while(numberOfImprovements < numberOfSamples);

            return (double)improvements / numberOfImprovements;
        }
        private RandomSolutionInitializer initializer = new RandomSolutionInitializer();
    }
}