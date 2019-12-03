namespace ATSP.Metrics
{
    public interface ISolutionComparator
    {
        double CompareSolutions(uint[] solution1, uint[] solution2);
    }
}