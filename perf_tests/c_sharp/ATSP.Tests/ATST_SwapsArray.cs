using System;
using Xunit;
using ATSP.classes;

namespace ATSP.Tests
{
    public class ATSP_SwapsArray
    {
        [Theory]
        [InlineData(0, 3)]
        [InlineData(2, 3)]
        public void DefaultSwapperTest(int firstIndex, int secondIndex)
        {
            swapper = new DefaultSwapper();
            SwapTest(firstIndex, secondIndex);
        }

        [Theory]
        [InlineData(0, 3)]
        [InlineData(2, 3)]
        public void XORSwapperTest(int firstIndex, int secondIndex)
        {
            swapper = new XORSwapper();
            SwapTest(firstIndex, secondIndex);
        }

        [Theory]
        [InlineData(0, 3)]
        [InlineData(2, 3)]
        public void ArithmeticSwapperTest(int firstIndex, int secondIndex)
        {
            swapper = new XORSwapper();
            SwapTest(firstIndex, secondIndex);
        }

        private void SwapTest(int firstIndex, int secondIndex)
        {
            Assert.NotNull(swapper);

            var arr = new uint[]{ 0, 1, 2, 3 };

            Assert.True(0 <= firstIndex);
            Assert.True(0 <= secondIndex);
            Assert.True(firstIndex < arr.Length);
            Assert.True(secondIndex < arr.Length);

            uint firstTruth = arr[firstIndex];
            uint secondTruth = arr[secondIndex];

            swapper.Swap(arr, firstIndex, secondIndex);

            Assert.True(arr[firstIndex] == secondTruth);
            Assert.True(arr[secondIndex] == firstTruth);
        }

        private ISwapper swapper;
    }
}
