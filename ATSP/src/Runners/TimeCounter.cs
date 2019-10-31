using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ATSP.Runners
{
    public abstract class TimeCounter
    {
        public abstract List<Execution> Run();

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

        protected double TicksToMillis(ulong ticks) => (double)ticks / Stopwatch.Frequency * 1000;

        public ulong NumberOfExecutions
        {
            get;
            protected set;
        }

        protected ulong timeout = 1; // in seconds
        protected ulong totalTime = 0;
        protected ulong minExecutions = 10;
    }
}