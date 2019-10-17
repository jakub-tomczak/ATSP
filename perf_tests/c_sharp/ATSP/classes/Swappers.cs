namespace ATSP.classes
{
    public class DefaultSwapper : ISwapper
    {
        public void Swap(uint[] collection, int firstIndex, int secondIndex)
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
            collection[firstIndex] ^= collection[secondIndex];
            collection[secondIndex] ^= collection[firstIndex];
            collection[firstIndex] ^= collection[secondIndex];
        }
    }

    public class ArithmeticSwapper: ISwapper
    {
        public void Swap(uint[] collection, int firstIndex, int secondIndex)
        {
            collection[firstIndex] += collection[secondIndex];
            collection[secondIndex] = collection[firstIndex] - collection[secondIndex];
            collection[firstIndex] = collection[firstIndex] - collection[secondIndex];
        }
    }
}