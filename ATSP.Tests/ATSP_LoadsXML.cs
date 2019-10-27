using Xunit;
using System;
using ATSP.DataLoading;

namespace ATSP.Tests
{
    public class ATSP_LoadsXML
    {
        [Fact]
        public void LoadXMLFromExistingFile()
        {
            var filename = "../../../existingFile";
            var xmlLoader = new XMLDataLoader();
            var loadedInstance = xmlLoader.LoadInstance(filename);

            Assert.NotNull(loadedInstance);
            Assert.Equal(4, loadedInstance.N);
        }

        [Fact]
        public void LoadXMLFromNonExistingFile()
        {
            var filename = "../../../nonExistingFile";
            var xmlLoader = new XMLDataLoader();
            var loadedInstance = xmlLoader.LoadInstance(filename);

            Assert.NotNull(loadedInstance);
            Assert.Equal(0, loadedInstance.N);
        }
    }
}