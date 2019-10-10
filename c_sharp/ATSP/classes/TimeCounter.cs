using System;
using System.Diagnostics;

namespace ATSP.classes
{
    public class TimeCounter<T>
    {
        public TimeCounter<T> Run(Func<T> function)
        {
            iterations = 0;
            var timer = new Stopwatch();
            do
            {
                timer.Restart();
                function();
                timer.Stop();
                var time = timer.ElapsedMilliseconds;
                totalTime += time;
                iterations++;
            } while( totalTime < timeout * 1000 || iterations < minIterations );
            return this;
        }

        public long GetElapsedTime()
        {
            if(iterations == 0)
            {
                return 0;
            }
            return totalTime / iterations;
        }

        private const long timeout = 1; // in seconds
        private long totalTime = 0;
        private long iterations = 0;
        private long minIterations = 10;
    }
}