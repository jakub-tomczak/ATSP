using System;
using ATSP.Runners;
using System.Xml;
using System.Xml.Serialization;

namespace ATSP
{
    class Program
    {
        static void Main(string[] args)
        {
            // new NoClassRunner().Run(10000000, 0);
            // Console.WriteLine();
            // new ClassBasedRunner().Run(10000000, 0);
            XmlSerializer ser = new XmlSerializer(typeof(TravellingSalesmanProblemInstance));
            TravellingSalesmanProblemInstance travellingSalesmanProblemInstance;
            string path = "C:\\Users\\mikol\\Documents\\Studia\\2_semestr\\Metah\\ATSP\\instances\\br17\\br17.xml";

            using (XmlReader reader = XmlReader.Create(path))
            {
                travellingSalesmanProblemInstance = (TravellingSalesmanProblemInstance) ser.Deserialize(reader);
            }
            Console.WriteLine("name: {0}, source: {1}, description: {2}, doublePrecision: {3}, ignoredDigits: {4}", travellingSalesmanProblemInstance.name, travellingSalesmanProblemInstance.source,travellingSalesmanProblemInstance.description,travellingSalesmanProblemInstance.doublePrecision,travellingSalesmanProblemInstance.ignoredDigits);
        }
    }
}
