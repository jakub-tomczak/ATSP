using ATSP.Data;

namespace ATSP.DataLoading
{
    public interface IDataLoader
    {
        Instance LoadInstance(string file);
    }
}