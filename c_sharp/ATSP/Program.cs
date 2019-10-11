using System;
using ATSP.classes;

namespace ATSP
{
    class Program
    {
        static void Main(string[] args)
        {
            new ClassBasedRunner().Run(10000000, 0);
            Console.WriteLine();
            new NoClassRunner().Run(10000000, 0);
        }
    }
}
