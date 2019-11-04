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


        // should be invoked after swap
        public uint UpdateCost(uint [] solution, uint currentCost, uint firstIndex, uint secondIndex)
        {
            currentCost -= (vertices[solution[(firstIndex-1)%solution.Length], solution[firstIndex]] +
                vertices[solution[firstIndex], solution[(firstIndex+1)%solution.Length]] +
                vertices[solution[(secondIndex-1)%solution.Length], solution[secondIndex]] +
                vertices[solution[secondIndex], solution[(secondIndex+1)%solution.Length]]);

            currentCost += (vertices[solution[(firstIndex-1)%solution.Length], solution[secondIndex]] +
                vertices[solution[secondIndex], solution[(firstIndex+1)%solution.Length]] +
                vertices[solution[(secondIndex-1)%solution.Length], solution[firstIndex]] +
                vertices[solution[firstIndex], solution[(secondIndex+1)%solution.Length]]);
            return currentCost;
        }

        public void PrintSolution()
        {
            Console.WriteLine(string.Join(',', Solution));
        }

        protected void SaveCost(uint cost = 0)
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

        public uint Steps { get; protected set; }

        public abstract bool IsEnd { get; protected set; }
        public TravellingSalesmanProblemInstance Instance { get; private set; }
        public uint[] Solution { get; set; }
        protected uint[,] vertices;
        protected IPermutator permutator;

        public List<uint> IntermediateCosts {get; protected set; } = new List<uint>();
        public double TimeoutInMillis { get; protected set; } = 0.0f;
    }
}