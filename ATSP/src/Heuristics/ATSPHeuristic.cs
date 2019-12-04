using System;
using System.Collections.Generic;
using System.Linq;
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

            if(Steps == 0)
            {
                InitialCost = cost;
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
            if(!SaveIntermediateCosts)
            {
                return;
            }
            if(SaveCostPoints.Contains(Steps))
            {
                if(cost == 0)
                {
                    IntermediateCosts.Add(CalculateCost());
                }
                else
                {
                    IntermediateCosts.Add(cost);
                }
            }
        }

        protected Tuple<int, int> GetIndicesForSwap(int arraySize)
        {
            int j = 0;
            var i = Randomizer.Next(arraySize);
            // get j such that i!=j
            do
            {
                j = Randomizer.Next(arraySize);
            } while(i == j);

            return new Tuple<int, int>(i, j);
        }

        public uint Steps { get; protected set; }
        public uint NumberOfImprovements { get; protected set; }

        public bool SaveIntermediateCosts { get; set; } = true;
        public static uint[] SaveCostPoints => Enumerable
            .Range(0, 40)
            .Select(x => (uint)x*10)
            .ToArray();

        public abstract bool IsEnd { get; protected set; }
        public TravellingSalesmanProblemInstance Instance { get; private set; }
        public uint[] Solution { get; set; }
        protected uint[,] vertices;
        protected IPermutator permutator;

        public List<uint> IntermediateCosts {get; protected set; } = new List<uint>();
        public double TimeoutInMillis { get; protected set; } = 0.0f;

        public Random Randomizer { get; set; } = new Random();

        public uint InitialCost { get; protected set; } = 0;

    }
}