namespace ATSP.Runners
{
    public interface IResultSaver
    {
        void SaveResult(ExperimentResult experimentResults);
        string SaveDirectory { get; set; }
    }
}