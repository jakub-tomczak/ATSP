using System;
using System.Diagnostics;

namespace ATSP.classes
{
    public class NoClassRunner: IRunner
    {
        public void Run(int arraySize, int seed)
        {
            var indices = new uint[arraySize];
            var randomizer = new Random();
            ulong minIterations = 10;
            ulong timeout = 1; // in seconds
            ulong totalTime = 0;

            for(uint i=0;i<indices.Length;i++)
            {
                indices[i] = i;
            }

            ulong iterations = 0;
            var timer = new Stopwatch();
            do
            {
                timer.Restart();
                for(int i=indices.Length-1;i>0;i--)
                {
                    var swapIndex = randomizer.Next(i);
                    Swap(indices, ref swapIndex, ref i);
                }
                timer.Stop();
                var time = timer.ElapsedTicks;
                totalTime += (ulong)time;
                iterations++;
            } while( TicksToMillis(totalTime) < timeout * millisToSeconds || iterations < minIterations );
            Console.WriteLine($"mean elapsed time in milliseconds {MeanIterationTime(iterations, totalTime)}, iterations {iterations}");
        }

        private void Swap(uint[] arr, ref int firstIndex, ref int secondIndex)
        {
            var temp = arr[firstIndex];
            arr[firstIndex] = arr[secondIndex];
            arr[secondIndex] = temp;
        }

        private double MeanIterationTime(ulong iterations, ulong totalTime)
        {
            if(iterations == 0)
            {
                return 0;
            }
            var millis = TicksToMillis(totalTime);
            return millis / iterations;
        }
        private double TicksToMillis(ulong ticks) => (double)ticks / Stopwatch.Frequency * millisToSeconds;

        const uint millisToSeconds = 1000;
    }
}