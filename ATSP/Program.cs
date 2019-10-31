using System;
using System.Collections.Generic;
using System.Linq;
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
                                .UseHeuristic(new GreedyHeuristic()),
                new Experiment()
                                .UseInstance(instanceName)
                                .SetInstancesLocation(instancesLocation)
                                .UseBestResultsLoader(bestResults)
                                .UsePermutator(permutator)
                                .UseHeuristic(new RandomHeuristic())

            };

            return this;
        }

        public void RunExperiments()
        {
            Console.WriteLine($"Experiments to run {experiments.Length}");
            var results = new List<ExperimentResult>();
            foreach(var experiment in experiments)
            {
                results.Add(experiment.Run());
                Console.WriteLine($"Number of executions {results.Last().NumberOfExecutions}, best cost {results.Last().Executions.Min(x => x.Cost)}, worst cost {results.Last().Executions.Max(x => x.Cost)}");
            }

            new CSVResultSaver().SaveResults(results);
        }

        private Experiment[] experiments;
    }
}
