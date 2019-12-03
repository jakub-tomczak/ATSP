using Xunit;
using System;
using ATSP.Metrics;

namespace ATSP.Tests
{
    public class ATSP_ComparesSolutions
    {
        [Fact]
        public void ConvertsInstanceToArrayUsingIndexers()
        {
            var s1 = new uint[]
            {
                1, 2, 3, 4, 5, 6
            };

            var s2 = new uint[]
            {
                1, 2, 3, 4, 5, 6
            };

            var s3 = new uint[]
            {
                6, 5, 4, 3, 2, 1
            };

            var s4 = new uint[]
            {
                1, 3, 4, 2, 5, 6
            };

            var s5 = new uint[]
            {
                1, 2, 3, 4, 5
            };

            var s6 = new uint[]
            {
                1, 3, 2, 4, 5
            };

            ISolutionComparator comparator = new PairsComparator();
            Assert.Equal(1, comparator.CompareSolutions(s1, s2), 2);
            Assert.Equal(0, comparator.CompareSolutions(s1, s3), 2);

            comparator = new NeighboursComparator();
            Assert.Equal(1, comparator.CompareSolutions(s1, s2), 2);
            Assert.Equal(0, comparator.CompareSolutions(s1, s3), 2);
        }
    }
}