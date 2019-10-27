using ATSP.Data;
using ATSP.Permutators;
using System.Linq;

namespace ATSP.Heuristics
{
    public abstract class ATSPHeuristic
    {
        public ATSPHeuristic(TravellingSalesmanProblemInstance instance, IPermutator permutator)
        {
            this.Instance = instance;
            // transform data into uint array
            this.Instance.TransformToArray();
            // take reference to that array
            vertices = this.Instance.ToArray();

            Solution = new uint[instance.N];

            this.permutator = permutator;

            this.IsEnd = false;
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

        public abstract bool IsEnd { get; protected set; }
        public readonly TravellingSalesmanProblemInstance Instance;
        public readonly uint[] Solution;
        protected uint[,] vertices;
        protected IPermutator permutator;
    }
}