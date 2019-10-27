using System;

namespace ATSP.Permutators
{
    public abstract class Permutator
    {
        protected void PrintArray()
        {
            Console.WriteLine(string.Join(',', indices));
        }

        public void Initialize(int arrSize, int seed)
        {
            indices = new uint[arrSize];
            this.seed = seed;
            randomizer = new Random(this.seed);
            PopulateArray();
        }

        private void PopulateArray()
        {
            for(uint i=0;i<indices.Length;i++)
            {
                indices[i] = i;
            }
        }

        protected uint[] indices;
        protected Random randomizer;
        protected int seed = 0;
    }
}