using System;
using System.Diagnostics;

namespace ATSP.classes
{
    public class TimeCounter<T>
    {
        public TimeCounter<T> Run(Func<T> function)
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
            } while( ElapsedMillis < timeout * 1000 || Iterations < minIterations );
            return this;
        }

        public TimeCounter<T> SetTimeout(ulong value)
        {
            timeout = value;
            return this;
        }

        public double ElapsedMillis
        {
            get
            {
                var millis = totalTime / TimeSpan.TicksPerMillisecond;
                if(Iterations == 0)
                {
                    return 0;
                }
                return (double)millis / Iterations;
            }
        }

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