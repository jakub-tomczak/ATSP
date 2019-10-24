﻿using System;
using ATSP.Runners;

namespace ATSP
{
    class Program
    {
        static void Main(string[] args)
        {
            new NoClassRunner().Run(10000000, 0);
            Console.WriteLine();
            new ClassBasedRunner().Run(10000000, 0);
        }
    }
}