namespace ATSP.Runners
{
    public interface IRunner
    {
        ExperimentResult Run(Experiment experiment);
        ulong MinExecutions { get; set; }
    }
}