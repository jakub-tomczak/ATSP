using System;
using System.Collections.Generic;
using ATSP.Data;
using ATSP.Permutators;

namespace ATSP.Heuristics
{
    public abstract class ATSPHeuristic
    {
        public ATSPHeuristic()
        {}

        public virtual void Reset()
        {
            this.IsEnd = false;
            this.Steps = 0;
            this.NumberOfImprovements = 0;
            IntermediateCosts = new List<uint>();
            ResetSolution();
        }

        public ATSPHeuristic UseInstance(TravellingSalesmanProblemInstance instance)
        {
            this.Instance = instance;
            // transform data into uint array
            this.Instance.TransformToArray();
            // take reference to that array
            vertices = this.Instance.ToArray();

            ResetSolution();
            return this;
        }

        protected void ResetSolution() => Solution = new uint[Instance.N];

        public ATSPHeuristic UsePermutator(IPermutator permutator)
        {
            this.permutator = permutator;
            return this;
        }

        public abstract void NextStep();

        public uint CalculateCost()
        {
            var cost = 0u;

            for(int i=0;i<Solution.Length;i++)
            {
                cost += vertices[Solution[i], Solution[(i+1)%Solution.Length]];
            }

            return cost;
        }


        // should be invoked before swap
        public uint CalculateSwapCost(uint [] solution, uint currentCost, int firstIndex, int secondIndex)
        {
            if(Math.Abs(firstIndex - secondIndex)%(solution.Length-2) == 1)
            {
                // corner case - swapping two neighbours
                var lowerIndex = Math.Min(firstIndex, secondIndex);
                var higherIndex = Math.Max(firstIndex, secondIndex);

                // swap lower with higher
                // if firstIndex = 0 and secondIndex = solution.Length-1 then lower = solution.Length-1, higher = 0
                if(lowerIndex == 0 && higherIndex == solution.Length-1)
                {
                    var temp = lowerIndex;
                    lowerIndex = higherIndex;
                    higherIndex = temp;
                }

                var previousLowerIndex = (lowerIndex - 1 + solution.Length) % solution.Length;
                var previousHigherIndex = (higherIndex - 1 + solution.Length) % solution.Length;
                // first and second index are neighbours
                currentCost -= (vertices[solution[previousLowerIndex], solution[lowerIndex]] +
                    vertices[solution[higherIndex], solution[(higherIndex+1)%solution.Length]] +
                    vertices[solution[lowerIndex], solution[higherIndex]]);

                currentCost += (vertices[solution[previousLowerIndex], solution[higherIndex]] +
                    vertices[solution[lowerIndex], solution[(higherIndex+1)%solution.Length]] +
                    vertices[solution[higherIndex], solution[lowerIndex]]);

            }
            else
            {
                var previousFirstIndex = (firstIndex - 1 + solution.Length) % solution.Length;
                var previousSecondIndex = (secondIndex - 1 + solution.Length) % solution.Length;

                currentCost -= (vertices[solution[previousFirstIndex], solution[firstIndex]] +
                    vertices[solution[firstIndex], solution[(firstIndex+1)%solution.Length]] +
                    vertices[solution[previousSecondIndex], solution[secondIndex]] +
                    vertices[solution[secondIndex], solution[(secondIndex+1)%solution.Length]]);

                currentCost += (vertices[solution[previousFirstIndex], solution[secondIndex]] +
                    vertices[solution[secondIndex], solution[(firstIndex+1)%solution.Length]] +
                    vertices[solution[previousSecondIndex], solution[firstIndex]] +
                    vertices[solution[firstIndex], solution[(secondIndex+1)%solution.Length]]);
            }
            return currentCost;
        }

        public void PrintSolution()
        {
            Console.WriteLine(string.Join(',', Solution));
        }

        protected void SaveCost(uint cost = 0)
        {
            return;
            if(Instance.N > 100)
            {
                return;
            }
            if(cost == 0)
            {
                IntermediateCosts.Add(CalculateCost());
            }
            else
            {
                IntermediateCosts.Add(cost);
            }
        }

        public uint Steps { get; protected set; }
        public uint NumberOfImprovements { get; protected set; }

        public abstract bool IsEnd { get; protected set; }
        public TravellingSalesmanProblemInstance Instance { get; private set; }
        public uint[] Solution { get; set; }
        protected uint[,] vertices;
        protected IPermutator permutator;

        public List<uint> IntermediateCosts {get; protected set; } = new List<uint>();
        public double TimeoutInMillis { get; protected set; } = 0.0f;
    }
}