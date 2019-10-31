using System;
using System.Diagnostics;

namespace ATSP.Runners
{
    public class TimeCounter
    {
        public TimeCounter Run(Action function)
        {
            NumberOfExecutions = 0;
            var timer = new Stopwatch();
            do
            {
                timer.Restart();
                function();
                timer.Stop();
                var time = timer.ElapsedTicks;
                totalTime += (ulong)time;
                NumberOfExecutions++;
            } while( TicksToMillis(totalTime) < timeout * 1000 || NumberOfExecutions < minExecutions );
            return this;
        }

        public TimeCounter SetTimeout(ulong value)
        {
            timeout = value;
            return this;
        }

        public double MeanExecutionTime
        {
            get
            {
                if(NumberOfExecutions == 0)
                {
                    return 0;
                }
                var millis = TicksToMillis(totalTime);
                return millis / NumberOfExecutions;
            }
        }

        private double TicksToMillis(ulong ticks) => (double)ticks / Stopwatch.Frequency * 1000;

        public ulong NumberOfExecutions
        {
            get;
            private set;
        }

        private ulong timeout = 1; // in seconds
        private ulong totalTime = 0;
        private ulong minExecutions = 10;
    }
}