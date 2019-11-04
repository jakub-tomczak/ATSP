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
        [InlineData(2, 3)]
        [InlineData(3, 1)]

        public void UpdateCostTest(int firstIndex, int secondIndex)
        {
            var solution = new uint[]{ 0, 1, 2, 3 };

            var filename = "../../../simpleMatrix";
            var xmlLoader = new XMLDataLoader();
            var loadedInstance = xmlLoader.LoadInstance(filename);

            heuristic.UseInstance(loadedInstance);
            heuristic.Solution = solution;

            var initialCost = heuristic.CalculateCost();

            var swapper = new DefaultSwapper();

            swapper.Swap(solution, firstIndex, secondIndex);
            var updatedCost = heuristic.UpdateCost(solution, initialCost, firstIndex, secondIndex);
            var updateCalculatedCost = heuristic.CalculateCost();
            Assert.Equal(updateCalculatedCost, updatedCost);

            var randomizer = new Random();
            for(int i=0;i<10;i++)
            {
                var firstToSwap = solution.Length-1;
                var secondToSwap = randomizer.Next(solution.Length - 1);

                swapper.Swap(solution, firstToSwap, secondToSwap);
                updatedCost = heuristic.UpdateCost(solution, initialCost, firstToSwap, secondToSwap);
                updateCalculatedCost = heuristic.CalculateCost();
                Assert.Equal(updateCalculatedCost, updatedCost);
            }
        }

        RandomHeuristic heuristic = new RandomHeuristic();
    }

}
