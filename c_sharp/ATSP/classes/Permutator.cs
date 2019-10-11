using System;

namespace ATSP.classes
{
    public abstract class Permutator
    {
        public Permutator()
            : this(100, 0)
        {}

        public Permutator(int arraySize, int seed)
        {
            Initialize(arraySize, seed);
        }


        protected void PrintArray()
        {
            Console.WriteLine(string.Join(',', indices));
        }

        protected void Swap(ref int firstIndex, ref int secondIndex)
        {
            var temp = indices[firstIndex];
            indices[firstIndex] = indices[secondIndex];
            indices[secondIndex] = temp;
        }

        private void Initialize(int arrSize, int seed)
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