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

        public Experiment UseSwapper(ISwapper swapper)
        {
            this.swapper = swapper;
            return this;
        }

        public Experiment UseHeuristic(ATSPHeuristic heuristic)
        {
            this.Heuristic = heuristic;
            return this;
        }

        public ExperimentResult Run()
        {
            Console.WriteLine($"Running experiment with instance {InstanceName}");
            if(!InitializeExperiment())
            {
                Console.WriteLine($"Failed to initialize experiment {InstanceName}");
                return new ExperimentResult();
            }
            else
            {
                Result = runner.Run(this);
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
        public TravellingSalesmanProblemInstance Instance { get; private set; }
        private IPermutator permutator;
        private ISwapper swapper;
        private IDataLoader dataLoader;
        private BestResultsLoader bestResultsLoader;
        public string InstanceName;
        private IRunner runner;
        public ATSPHeuristic Heuristic { get; private set; }
        private string instancesLocation;
        private bool initialized = false;
    }

}