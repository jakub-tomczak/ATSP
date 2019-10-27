using ATSP.DataLoading;
using ATSP.Runners;

namespace ATSP
{
    class Program
    {
        static void Main(string[] args)
        {
            var instanceName = "br17";
            var instance = new XMLDataLoader().LoadInstance($"../instances/{instanceName}/{instanceName}.xml");

            new ClassBasedRunner().UseInstance(instance).Run(10000000, 0);
        }
    }
}
