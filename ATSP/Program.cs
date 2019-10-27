using ATSP.DataLoading;
using ATSP.Permutators;
using ATSP.Runners;

namespace ATSP
{
    class Program
    {
        static void Main(string[] args)
        {
            var bestInstancesFilename = "best_known_instances";
            var instanceName = "br17";
            var seed = 50;

            var bestResults = new BestResultsLoader();
            bestResults.LoadBestResults($"../instances/{bestInstancesFilename}");

            var instance = new XMLDataLoader().LoadInstance($"../instances/{instanceName}/{instanceName}.xml");

            instance.BestKnownCost = bestResults.GetBestResultForInstance(instance.Name);

            var swapper = new DefaultSwapper();
            var permutator = new DefaultPermutator(instance.N, seed).UseSwapper(swapper);


            new ClassBasedRunner().UseInstance(instance)
                                  .UsePermutator(permutator)
                                  .Run(10000000, 0);
        }
    }
}
