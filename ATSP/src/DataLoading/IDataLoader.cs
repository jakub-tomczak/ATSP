using ATSP.Data;

namespace ATSP.DataLoading
{
    public interface IDataLoader
    {
        TravellingSalesmanProblemInstance LoadInstance(string file);
        string FileExtension { get; }
    }
}