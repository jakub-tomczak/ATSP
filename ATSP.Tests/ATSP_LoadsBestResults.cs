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
            var results = BestResultsLoader.LoadBestResults("../../../../instances/best_known_results", true);

            Assert.Equal(27, results.Count);
        }

        [Fact]
        public void AssignsBestKnownResults()
        {
            var results = BestResultsLoader.LoadBestResults("../../../best_results", true);

            Assert.Equal(1, results.Count);

            var instance = GetInstance();
            var instanceBestResult = BestResultsLoader.GetBestResultForInstance(instance.Name);

            Assert.Equal(100u, instanceBestResult);
        }

        [Fact]
        public void FailsWhenLoadingNonExisitingBestResults()
        {
            Assert.Throws<ArgumentException>( () => {
                BestResultsLoader.LoadBestResults("../../../non_existing_best_results", true);
            });
        }

        [Fact]
        public void FailsWhenParsingBadlyFormattedBestResults()
        {
            Assert.Throws<FormatException>( () => {
                BestResultsLoader.LoadBestResults("../../../badly_formatted_best_results", true);
            });
        }

        private Data.TravellingSalesmanProblemInstance GetInstance()
        {
            var filename = "../../../existingFile.xml";
            var xmlLoader = new XMLDataLoader();
            var loadedInstance = xmlLoader.LoadInstance(filename);

            Assert.NotEqual(0, loadedInstance.N);

            return loadedInstance;
        }
    }
}