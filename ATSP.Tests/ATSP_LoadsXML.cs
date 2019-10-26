using Xunit;
using ATSP.Data;
using ATSP.DataLoading;

namespace ATSP.Tests
{
    public class ATSP_LoadsXML
    {
        [Fact]
        public void LoadXMLFromExistingFile()
        {
            var filename = "existingFile.xml";
            var xmlLoader = new XMLDataLoader();
            var loadedInstance = xmlLoader.LoadInstance(filename);

            Assert.NotNull(loadedInstance);
            Assert.Equal(4, loadedInstance.N);
        }

        [Fact]
        public void LoadXMLFromNonExistingFile()
        {
            var filename = "nonExistingFile.xml";
            var xmlLoader = new XMLDataLoader();
            var loadedInstance = xmlLoader.LoadInstance(filename);

            Assert.NotNull(loadedInstance);
            Assert.Equal(0, loadedInstance.N);
        }

        // [Theory]
        // public void LoadXMLFromString()
        // {
        //     ;
        // }
        // public void LoadXML(string data)
        // {
        // }
    }
}