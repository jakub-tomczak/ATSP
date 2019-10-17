namespace ATSP.classes
{
    public interface ISwapper
    {
        void Swap(uint[] collection, int firstIndex, int secondIndex);
        void Swap(uint[] collection, ref int firstIndex, ref int secondIndex);
    }
}