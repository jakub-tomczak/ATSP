using System;
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

        public uint Steps { get; protected set; }

        public abstract bool IsEnd { get; protected set; }
        public TravellingSalesmanProblemInstance Instance { get; private set; }
        public uint[] Solution { get; set; }
        protected uint[,] vertices;
        protected IPermutator permutator;
    }
}