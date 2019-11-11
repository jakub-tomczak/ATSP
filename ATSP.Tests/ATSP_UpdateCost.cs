using Xunit;
using ATSP.Heuristics;
using ATSP.DataLoading;
using ATSP.Permutators;
using System;

namespace ATSP.Tests
{
    public class ATSP_UpdateCosts
    {
        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(4, 3)]
        [InlineData(3, 4)]
        [InlineData(4, 0)]
        [InlineData(0, 4)]
        public void UpdateCostNeighboursTest(int firstIndex, int secondIndex)
        {
            var solution = new uint[]{ 3, 2, 0, 1, 4 };

            var filename = "../../../simpleMatrix";
            var xmlLoader = new XMLDataLoader();
            var loadedInstance = xmlLoader.LoadInstance(filename);

            heuristic.UseInstance(loadedInstance);
            heuristic.Solution = solution;

            var initialCost = heuristic.CalculateCost();

            var swapper = new DefaultSwapper();

            var updatedCost = heuristic.CalculateSwapCost(solution, initialCost, firstIndex, secondIndex);
            swapper.Swap(solution, firstIndex, secondIndex);
            var updateCalculatedCost = heuristic.CalculateCost();
            Assert.Equal(updateCalculatedCost, updatedCost);

            // revert the swap
            updatedCost = heuristic.CalculateSwapCost(solution, updatedCost, firstIndex, secondIndex);
            swapper.Swap(solution, firstIndex, secondIndex);
            updateCalculatedCost = heuristic.CalculateCost();
            Assert.Equal(updateCalculatedCost, updatedCost);
        }

        [Fact]
        public void UpdateCostRandomlyTest()
        {
            var solution = new uint[]{ 3, 2, 1, 0, 4 };

            var filename = "../../../simpleMatrix";
            var xmlLoader = new XMLDataLoader();
            var loadedInstance = xmlLoader.LoadInstance(filename);

            heuristic.UseInstance(loadedInstance);
            heuristic.Solution = solution;

            var initialCost = heuristic.CalculateCost();

            var swapper = new DefaultSwapper();

            var randomizer = new Random(0);
            var updatedCost = initialCost;
            for(int i=0;i<1000;i++)
            {
                var firstToSwap = randomizer.Next(solution.Length - 1);
                var secondToSwap = randomizer.Next(solution.Length - 1);

                updatedCost = heuristic.CalculateSwapCost(solution, updatedCost, firstToSwap, secondToSwap);
                swapper.Swap(solution, firstToSwap, secondToSwap);
                var updateCalculatedCost = heuristic.CalculateCost();
                Assert.Equal(updateCalculatedCost, updatedCost);
            }
        }

        [Fact]
        public void UpdateCostBR17InstanceTest()
        {
            var solution = new uint[]{ 3, 2, 1, 0, 4, 6, 12, 11, 7, 8, 5, 9, 13, 15, 16, 14, 10 };

            var filename = "../../../../instances/br17/br17";
            var xmlLoader = new XMLDataLoader();
            var loadedInstance = xmlLoader.LoadInstance(filename);

            heuristic.UseInstance(loadedInstance);
            heuristic.Solution = solution;

            var initialCost = heuristic.CalculateCost();

            var swapper = new DefaultSwapper();

            var randomizer = new Random(0);
            var updatedCost = initialCost;
            for(int i=0;i<10000;i++)
            {
                var firstToSwap = randomizer.Next(solution.Length - 1);
                var secondToSwap = randomizer.Next(solution.Length - 1);

                updatedCost = heuristic.CalculateSwapCost(solution, updatedCost, firstToSwap, secondToSwap);
                swapper.Swap(solution, firstToSwap, secondToSwap);
                var updateCalculatedCost = heuristic.CalculateCost();
                Assert.Equal(updateCalculatedCost, updatedCost);

                updatedCost = heuristic.CalculateSwapCost(solution, updatedCost, firstToSwap, secondToSwap);
                swapper.Swap(solution, firstToSwap, secondToSwap);
                updateCalculatedCost = heuristic.CalculateCost();
                Assert.Equal(updateCalculatedCost, updatedCost);
            }
        }


        RandomHeuristic heuristic = new RandomHeuristic();
    }

}
