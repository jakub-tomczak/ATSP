using System;
using ATSP.Permutators;
using ATSP.Data;
using ATSP.DataLoading;
using System.IO;

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

        public ExperimentResult Run()
        {
            InitializeExperiment();
            Result = runner.Run(this);
            return Result;
        }

        private void InitializeExperiment()
        {
            if(!initialized)
            {
                if(dataLoader is null)
                {
                    throw new ArgumentException($"DataLoader cannot be empty!");
                }

                Instance = dataLoader.LoadInstance($"{Path.Combine(instancesLocation, InstanceName)}");

                Instance.BestKnownCost = Instance.BestKnownCost = bestResultsLoader.GetBestResultForInstance(Instance.Name);
            }
        }

        public ExperimentResult Result { get; private set; }
        public TravellingSalesmanProblemInstance Instance { get; private set; }
        private IPermutator permutator;
        private ISwapper swapper;
        private IDataLoader dataLoader;
        private BestResultsLoader bestResultsLoader;
        public string InstanceName;
        private IRunner runner;
        private string instancesLocation;
        private bool initialized = false;
    }

}