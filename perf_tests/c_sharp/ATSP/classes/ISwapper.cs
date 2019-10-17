using System.Collections;

namespace ATSP.classes
{
    public interface ISwapper
    {
        void Swap(uint[] collection, int firstIndex, int secondIndex);
    }
}