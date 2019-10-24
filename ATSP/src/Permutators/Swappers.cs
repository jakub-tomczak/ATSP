namespace ATSP.Permutators
{
    public class DefaultSwapper : ISwapper
    {
        public void Swap(uint[] collection, int firstIndex, int secondIndex)
        {
            Swap(collection, ref firstIndex, ref secondIndex);
        }

        public void Swap(uint[] collection, ref int firstIndex, ref int secondIndex)
        {
            var tmp = collection[firstIndex];
            collection[firstIndex] = collection[secondIndex];
            collection[secondIndex] = tmp;
        }
    }

    public class XORSwapper: ISwapper
    {
        public void Swap(uint[] collection, int firstIndex, int secondIndex)
        {
            Swap(collection, ref firstIndex, ref secondIndex);
        }

        public void Swap(uint[] collection, ref int firstIndex, ref int secondIndex)
        {
            collection[firstIndex] ^= collection[secondIndex];
            collection[secondIndex] ^= collection[firstIndex];
            collection[firstIndex] ^= collection[secondIndex];
        }
    }

    public class ArithmeticSwapper: ISwapper
    {
        public void Swap(uint[] collection, int firstIndex, int secondIndex)
        {
            Swap(collection, ref firstIndex, ref secondIndex);
        }

        public void Swap(uint[] collection, ref int firstIndex, ref int secondIndex)
        {
            collection[firstIndex] += collection[secondIndex];
            collection[secondIndex] = collection[firstIndex] - collection[secondIndex];
            collection[firstIndex] = collection[firstIndex] - collection[secondIndex];
        }
    }
}