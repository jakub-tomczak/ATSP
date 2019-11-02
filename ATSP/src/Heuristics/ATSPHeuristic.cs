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
            IntermediateCosts.Clear();
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

        protected List<uint> IntermediateCosts = new List<uint>();
    }
}