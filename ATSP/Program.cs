using System;
using ATSP.DataLoading;
using ATSP.Heuristics;
using ATSP.Permutators;
using ATSP.Runners;

namespace ATSP
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program()
                .PrepareExperiments()
                .RunExperiments();
        }

        public Program PrepareExperiments()
        {
            var instancesLocation = @"../instances";
            var bestInstancesFilename = "best_known_results";
            var instanceName = "br17";
            var seed = 50;

            var bestResults = new BestResultsLoader();
            bestResults.LoadBestResults($"../instances/{bestInstancesFilename}");
            var permutator = new DefaultPermutator().SetSeed(seed)
                                                    .UseSwapper(new DefaultSwapper());

            experiments = new [] {
                new Experiment()
                                .UseInstance(instanceName)
                                .SetInstancesLocation(instancesLocation)
                                .UseBestResultsLoader(bestResults)
                                .UsePermutator(permutator)
                                .UseLoader(new XMLDataLoader())
                                .UseHeuristic(new RandomHeuristic())
                                .UseRunner(new ClassBasedRunner()),
                new Experiment()
                                .UseInstance(instanceName)
                                .SetInstancesLocation(instancesLocation)
                                .UseBestResultsLoader(bestResults)
                                .UsePermutator(permutator)
                                .UseLoader(new XMLDataLoader())
                                .UseHeuristic(new RandomHeuristic())
                                .UseRunner(new ClassBasedRunner())

            };

            return this;
        }

        public bool RunExperiments()
        {
            var result = true;
            Console.WriteLine($"Experiments to run {experiments.Length}");
            foreach(var experiment in experiments)
            {
                result &= experiment.Run().Result;
            }
            return result;
        }

        private Experiment[] experiments;
    }
}
