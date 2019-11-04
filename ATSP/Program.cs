﻿using System;
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
            var resultsSaver = new CSVResultSaver();
            resultsSaver.SaveDirectory = "results";

            new Program()
                .PrepareExperiments()
                .RunExperiments()
                .SaveResults(resultsSaver)
                .PrepareRaport(resultsSaver.SaveDirectory, resultsSaver.Extension, "../Raport/plots");
        }

        public Program PrepareExperiments()
        {
            var instancesLocation = @"../instances";
            var bestInstancesFilename = "best_known_results";
            var instanceName = "ft70";
            var seed = 50;

            var bestResults = new BestResultsLoader();
            bestResults.LoadBestResults($"../instances/{bestInstancesFilename}");
            var permutator = new DefaultPermutator().SetSeed(seed)
                                                    .UseSwapper(new DefaultSwapper());
            var solutionInitializer =  new RandomSolutionInitializer()
            {
                NumberOfShufflesOnStartup = 5
            };

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
                new Experiment("simple2", saveResults: true)
                                .UseInstance(instanceName)
                                .SetInstancesLocation(instancesLocation)
                                .UseBestResultsLoader(bestResults)
                                .UsePermutator(permutator)
                                .UseHeuristic(new SimpleHeuristic2())
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

        public Program PrepareRaport(string resultsDirectory, string raportFilesExtension, string plotsDirectory)
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

            return this;
        }

        private Experiment[] experiments;
    }
}
