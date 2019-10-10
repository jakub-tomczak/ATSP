using System;
using System.Threading;
using ATSP.classes;

namespace ATSP
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new TimeCounter<bool>().Run(() => {
                Thread.Sleep(100);
                return true;
            });
            Console.WriteLine($"Total mean elapsed time in milliseconds {timer.GetElapsedTime()}");
        }
    }
}
