using ATSP.DataLoading;
using ATSP.Runners;

namespace ATSP
{
    class Program
    {
        static void Main(string[] args)
        {
            var bestInstancesFilename = "best_known_instances";
            var bestResults = new BestResultsLoader();
            bestResults.LoadBestResults($"../instances/{bestInstancesFilename}");

            var instanceName = "br17";
            var instance = new XMLDataLoader().LoadInstance($"../instances/{instanceName}/{instanceName}.xml");

            instance.BestKnownCost = bestResults.GetBestResultForInstance(instance.Name);

            new ClassBasedRunner().UseInstance(instance).Run(10000000, 0);
        }
    }
}
