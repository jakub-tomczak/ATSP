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
            var resultsSaver = new CSVResultSaver();
            resultsSaver.SaveDirectory = "results";

            new Program()
                .PrepareExperiments()
                .RunExperiments()
                .SaveResults(resultsSaver);
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
                new Experiment("greedy", saveResults: true)
                                .UseInstance(instanceName)
                                .SetInstancesLocation(instancesLocation)
                                .UseBestResultsLoader(bestResults)
                                .UsePermutator(permutator)
                                .UseHeuristic(new GreedyHeuristic()),
                new Experiment("random", saveResults: true)
                                .UseInstance(instanceName)
                                .SetInstancesLocation(instancesLocation)
                                .UseBestResultsLoader(bestResults)
                                .UsePermutator(permutator)
                                .UseHeuristic(new RandomHeuristic())

            };

            return this;
        }

        public Program RunExperiments()
        {
            Console.WriteLine($"Experiments to run {experiments.Length}");
            foreach(var experiment in experiments)
            {
                var result = experiment.Run();
                Console.WriteLine($"Number of executions {result.NumberOfExecutions}, best cost {result.Executions.Min(x => x.Cost)}, worst cost {result.Executions.Max(x => x.Cost)}");
            }

            return this;
        }

        public Program SaveResults(IResultSaver saver)
        {
            foreach(var experiment in experiments)
            {
                if(experiment.Result != null && experiment.SaveResults)
                {
                    Console.WriteLine($"Saving results, experiment {experiment.Name} {experiment.InstanceName}");
                    saver.SaveResult(experiment.Result);
                }
            }

            return this;
        }

        private Experiment[] experiments;
    }
}
