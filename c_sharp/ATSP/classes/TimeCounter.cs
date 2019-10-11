using System;
using System.Diagnostics;

namespace ATSP.classes
{
    public class TimeCounter
    {
        public TimeCounter Run(Action function)
        {
            Iterations = 0;
            var timer = new Stopwatch();
            do
            {
                timer.Restart();
                function();
                timer.Stop();
                var time = timer.ElapsedTicks;
                totalTime += (ulong)time;
                Iterations++;
                // Console.WriteLine($"Iter {Iterations}, time {TicksToMillis((ulong)time)}");
            } while( ElapsedMillis < timeout * 1000 || Iterations < minIterations );
            return this;
        }

        public TimeCounter SetTimeout(ulong value)
        {
            timeout = value;
            return this;
        }

        public double ElapsedMillis
        {
            get
            {
                if(Iterations == 0)
                {
                    return 0;
                }
                var millis = TicksToMillis(totalTime);
                return millis / Iterations;
            }
        }

        private double TicksToMillis(ulong ticks) => (double)ticks / TimeSpan.TicksPerMillisecond;

        public ulong Iterations
        {
            get;
            private set;
        }

        private ulong timeout = 1; // in seconds
        private ulong totalTime = 0;
        private ulong minIterations = 10;
    }
}