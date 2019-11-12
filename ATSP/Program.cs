using System;
using System.Diagnostics;
using System.IO;
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
            var resultsSaver = new CSVResultSaver()
            {
                SaveDirectory = "results"
            };

            var bestResults = new BestResultsLoader();
            bestResults.LoadBestResults($"../instances/{bestInstancesFilename}");

            var instances = new []
            {
                "br17",
                "ft53",
                "ft70",
                "ftv33",
                "ftv35",
                "ftv38",
                "ftv44",
                "ftv47",
                "ftv55",
                "ftv64",
                "ftv70",
                "ftv170",
                "kro124p",
                "p43",
                "rbg323",
                "rbg358",
                "rbg403",
                "rbg443",
                "ry48p"
            };
            instances.ToList()
                .AsParallel()
                .ForAll(x =>
                    {
                        new Program()
                            .UseInstance(x)
                            .UseBestResults(bestResults)
                            .PrepareExperiments()
                            .RunExperiments()
                            .SaveResults(resultsSaver);
                    }
                );

            // Program.PrepareRaport(resultsSaver.SaveDirectory, resultsSaver.Extension, "../Raport/plots");
        }

        public Program UseInstance(string instanceName)
        {
            this.instanceName = instanceName;
            return this;
        }

        public Program UseBestResults(BestResultsLoader bestResults)
        {
            this.bestResults = bestResults;
            return this;
        }

        public Program PrepareExperiments()
        {
            var permutator = new DefaultPermutator().SetSeed(seed)
                                                    .UseSwapper(new DefaultSwapper());
            var solutionInitializer = new RandomSolutionInitializer();

            var greedy = new Experiment("greedy", saveResults: true, runOnlyOnce: true)
                                .UseInstance(instanceName)
                                .SetInstancesLocation(instancesLocation)
                                .UseBestResultsLoader(bestResults)
                                .UsePermutator(permutator)
                                .UseHeuristic(new GreedyHeuristic())
                                .UseInitializer(solutionInitializer);
            // run greedy to get max time for random experiment execution
            RunExperiment(greedy);

            experiments = new [] {
                greedy, // add to list with experiments to save its result
                new Experiment("random", saveResults: true)
                                .UseInstance(instanceName)
                                .SetInstancesLocation(instancesLocation)
                                .UseBestResultsLoader(bestResults)
                                .UsePermutator(permutator)
                                .UseHeuristic(new RandomHeuristic(timeoutInMillis: greedy.Result.MeanExecutionTime))
                                .UseInitializer(solutionInitializer),
                new Experiment("steepest", saveResults: true)
                                .UseInstance(instanceName)
                                .SetInstancesLocation(instancesLocation)
                                .UseBestResultsLoader(bestResults)
                                .UsePermutator(permutator)
                                .UseHeuristic(new SteepestHeurestic())
                                .UseInitializer(solutionInitializer),
                new Experiment("Simple", saveResults: true)
                                .UseInstance(instanceName)
                                .SetInstancesLocation(instancesLocation)
                                .UseBestResultsLoader(bestResults)
                                .UsePermutator(permutator)
                                .UseHeuristic(new SimpleHeuristic())
                                .UseInitializer(solutionInitializer),

            };

            return this;
        }

        private Program RunExperiment(Experiment experiment)
        {
            var result = experiment.Run();
            Console.WriteLine($"Number of executions {result.NumberOfExecutions}, best cost {result.Executions.Min(x => x.Cost)}, worst cost {result.Executions.Max(x => x.Cost)}, best know cost {experiment.Instance.BestKnownCost}");
            return this;
        }

        public Program RunExperiments()
        {
            Console.WriteLine($"Experiments to run {experiments.Length}");
            foreach(var experiment in experiments)
            {
                RunExperiment(experiment);
            }

            return this;
        }

        public Program SaveResults(IResultSaver saver)
        {
            Console.WriteLine("\nSaving experiments results");
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

        public static void PrepareRaport(string resultsDirectory, string raportFilesExtension, string plotsDirectory)
        {
            Console.WriteLine("\nPreparing raport");
            using(var raportProcess =  new Process())
            {
                raportProcess.StartInfo.FileName = "python";
                raportProcess.StartInfo.ArgumentList.Add("../Raport/raport_generator.py");
                raportProcess.StartInfo.ArgumentList.Add(Path.GetFullPath(resultsDirectory));
                raportProcess.StartInfo.ArgumentList.Add(raportFilesExtension);
                raportProcess.StartInfo.ArgumentList.Add(plotsDirectory);

                // set color for python's output
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                var executed = false;
                try
                {
                    raportProcess.Start();
                    raportProcess.WaitForExit();
                    executed = true;
                }
                catch(Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to run raport generator, error {e.Message}");
                    Console.WriteLine();
                }
                finally
                {
                    Console.ResetColor();
                }

                if(!executed || raportProcess.ExitCode > 0)
                {
                    Console.WriteLine("Failed to create raport.");
                }
                else
                {
                    Console.WriteLine("Raport created.");
                }

            }
        }

        private Experiment[] experiments;
        private static string instancesLocation = @"../instances";
        private static string bestInstancesFilename = "best_known_results";
        private string instanceName = string.Empty;
        private int seed = 50;

        private BestResultsLoader bestResults = new BestResultsLoader();
    }
}
