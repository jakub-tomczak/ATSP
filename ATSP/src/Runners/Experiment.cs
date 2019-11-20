using System;
using ATSP.Permutators;
using ATSP.Data;
using ATSP.DataLoading;
using System.IO;
using ATSP.Heuristics;

namespace ATSP.Runners
{
    public class Experiment
    {
        public Experiment(string experimentName, bool saveResults = false, bool runOnlyOnce = false)
        {
            Name = experimentName;
            SaveResults = saveResults;
            RunOnlyOnce = runOnlyOnce;
        }
        public Experiment UseInstance(string instanceName)
        {
            this.InstanceName = instanceName;
            return this;
        }

        public Experiment UseLoader(IDataLoader loader)
        {
            this.dataLoader = new XMLDataLoader();
            return this;
        }

        public Experiment SetInstancesLocation(string location)
        {
            this.instancesLocation = location;
            return this;
        }

        public Experiment UsePermutator(IPermutator permutator)
        {
            this.permutator = permutator;
            return this;
        }

        public Experiment UseBestResultsLoader(BestResultsLoader resultsLoader)
        {
            this.bestResultsLoader = resultsLoader;
            return this;
        }

        public Experiment UseRunner(IRunner runner)
        {
            this.runner = runner;
            return this;
        }

        public Experiment UseHeuristic(ATSPHeuristic heuristic)
        {
            this.Heuristic = heuristic;
            return this;
        }

        public Experiment UseInitializer(ISolutionInitializer solutionInitializer)
        {
            this.Initializer = solutionInitializer;
            return this;
        }

        public Experiment SetNumberOfExecutions(ulong numberOfExecutions)
        {
            if(this.runner is null)
            {
                Console.WriteLine("Runner is null, cannot set number of executions");
                return this;
            }
            this.runner.MinExecutions = numberOfExecutions;
            return this;
        }

        public ExperimentResult Run()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nRunning experiment {Name}");
            Console.ResetColor();
            if(RunOnlyOnce && Runs > 0)
            {
                Console.WriteLine("Already run.");
                return Result;
            }
            if(!InitializeExperiment())
            {
                Console.WriteLine($"Failed to initialize experiment {InstanceName}");
                return new ExperimentResult();
            }
            else
            {
                Console.WriteLine($"Running experiment with instance {InstanceName}");
                Result = runner.Run(this);
                Runs++;
                Result.Name = Name;
                Result.InstanceName = InstanceName;
                Console.Write("\n\n");
                return Result;
            }
        }

        private bool InitializeExperiment()
        {
            if(!initialized)
            {
                Console.WriteLine($"Initializing experiment with instance {InstanceName}");
                if(dataLoader is null)
                {
                    Console.WriteLine($"DataLoader cannot be empty!");
                    return false;
                }

                Instance = dataLoader.LoadInstance($"{Path.Combine(instancesLocation, InstanceName, InstanceName)}");
                if(Instance.N == 0)
                {
                    return false;
                }

                Instance.BestKnownCost = Instance.BestKnownCost = bestResultsLoader.GetBestResultForInstance(Instance.Name);

                if(Heuristic is null)
                {
                    Console.WriteLine("Heuristic cannot be null");
                    return false;
                }

                Heuristic.UseInstance(Instance).UsePermutator(permutator);

                initialized = true;
            }
            return true;
        }

        public ExperimentResult Result { get; private set; }
        public TravellingSalesmanProblemInstance Instance { get; private set; } = new TravellingSalesmanProblemInstance();
        private IPermutator permutator = new DefaultPermutator();
        private IDataLoader dataLoader = new XMLDataLoader();
        private BestResultsLoader bestResultsLoader = new BestResultsLoader();
        public string InstanceName = string.Empty;
        private IRunner runner = new DefaultRunner();
        public ISolutionInitializer Initializer { get; private set; } = new RandomSolutionInitializer();
        public ATSPHeuristic Heuristic { get; private set; } = new RandomHeuristic();
        public readonly bool SaveResults = false;
        public readonly bool RunOnlyOnce = false;
        public int Runs {get;private set;} = 0;
        private string instancesLocation = "../instances";
        private bool initialized = false;
        public readonly string Name = "";
    }

}