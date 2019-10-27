using Xunit;
using System;
using ATSP.DataLoading;

namespace ATSP.Tests
{
    public class ATSP_LoadsBestResults
    {
        [Fact]
        public void LoadsBestKnownResults()
        {
            var bestResults = new BestResultsLoader();
            var results = bestResults.LoadBestResults("../../../../instances/best_known_results");

            Assert.Equal(27, results.Count);
        }

        [Fact]
        public void AssignsBestKnownResults()
        {
            var bestResults = new BestResultsLoader();
            var results = bestResults.LoadBestResults("../../../best_results");

            Assert.Equal(1, results.Count);

            var instance = GetInstance();
            var instanceBestResult = bestResults.GetBestResultForInstance(instance.Name);

            Assert.Equal(100u, instanceBestResult);
        }

        [Fact]
        public void FailsWhenLoadingNonExisitingBestResults()
        {
            Assert.Throws<ArgumentException>( () => {
                new BestResultsLoader().LoadBestResults("../../../non_existing_best_results");
            });
        }

        [Fact]
        public void FailsWhenParsingBadlyFormattedBestResults()
        {
            Assert.Throws<FormatException>( () => {
                new BestResultsLoader().LoadBestResults("../../../badly_formatted_best_results");
            });
        }

        private Data.TravellingSalesmanProblemInstance GetInstance()
        {
            var filename = "../../../existingFile";
            var xmlLoader = new XMLDataLoader();
            var loadedInstance = xmlLoader.LoadInstance(filename);

            Assert.NotEqual(0, loadedInstance.N);

            return loadedInstance;
        }
    }
}