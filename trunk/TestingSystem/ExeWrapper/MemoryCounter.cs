using System;
using System.Diagnostics;
using System.Timers;

namespace TestingSystem
{
    public class MemoryCounter
    {
        /// <summary>
        /// Represents the process to be watched.
        /// </summary>
        private Process activeProcess;

        /// <summary>
        /// Represents the amount of memoryUsed by process.
        /// </summary>
        private long memoryUsage;

        /// <summary>
        /// Timer used to tick the interval.
        /// </summary>
        private Timer timer;

        /// <summary>
        /// Gets current watched process.
        /// </summary>
        /// 
        /// <value>
        /// The watched process.
        /// </value>
        public Process Process
        {
            get
            {
                return activeProcess;
            }
        }

        /// <summary>
        /// Gets the amount of memory used by process.
        /// </summary>
        /// 
        /// <value>
        /// The amount of memory used by process.
        /// </value>
        public long Memory
        {
            get
            {
                return memoryUsage;
            }
        }

        public MemoryCounter(Process process, int interval)
        {
            if (process == null)
            {
                throw new ArgumentNullException("process", "process can not be null.");
            }
            if (interval <= 0)
            {
                throw new ArgumentException("interval must be positive", "interval");
            }
            activeProcess = process;
            timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        public void Stop()
        {
            timer.Elapsed -= timer_Elapsed;
            if (!activeProcess.HasExited)
            {
                memoryUsage = Math.Max(memoryUsage, activeProcess.PeakPagedMemorySize64);
            }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!activeProcess.HasExited)
            {
                memoryUsage = Math.Max(memoryUsage, activeProcess.PeakPagedMemorySize64);
            }
        }


    }
}
