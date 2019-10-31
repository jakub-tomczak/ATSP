using System.Collections.Generic;

namespace ATSP.Runners
{
    public interface IResultSaver
    {
        void SaveResults(List<ExperimentResult> experimentsResults);
    }
}