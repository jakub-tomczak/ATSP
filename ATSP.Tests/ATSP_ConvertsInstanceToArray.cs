using Xunit;
using System;
using ATSP.DataLoading;

namespace ATSP.Tests
{
    public class ATSP_ConvertsInstanceToArray
    {
        [Fact]
        public void ConvertsInstanceToArrayUsingIndexers()
        {
            var instance = GetInstance();
            Assert.NotNull(instance);

            Assert.Equal(9999u, instance[0,0]);
            Assert.Equal(48u, instance[0,3]);
            Assert.Equal(74u, instance[3,2]);
        }

        [Fact]
        public void ConvertsInstanceToArray()
        {
            var instance = GetInstance();
            var instanceArray  = instance.ToArray();

            Assert.NotNull(instanceArray);
            Assert.Equal(instance.N, instanceArray.GetLength(0));

            Assert.Equal(9999u, instanceArray[0,0]);
            Assert.Equal(48u, instanceArray[0,3]);
            Assert.Equal(74u, instanceArray[3,2]);

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