using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggrEngine
{
    /// <summary>
    /// App runtime
    /// </summary>
    public static class Runtime
    {
        private static long dueTs;

        static Runtime()
        {
            Frequency = 50;
        }
        /// <summary>
        /// Gets or sets run frequency of Update method
        /// </summary>
        public static int Frequency { get; set; }

        /// <summary>
        /// register a update handle method of interval frequent(20fps)
        /// </summary>
        public static Action UpdateHandle;

        /// <summary>
        /// Process is stop run
        /// </summary>
        public static bool IsCancel { get; private set; }

        /// <summary>
        /// Gets due second time.
        /// </summary>
        public static double DueTime
        {
            get { return (Stopwatch.GetTimestamp() - dueTs) / (double)Stopwatch.Frequency; }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string ServerID { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public static void Start()
        {
            dueTs = Stopwatch.GetTimestamp();
        }

        /// <summary>
        /// stop process
        /// </summary>
        public static void Stop()
        {
            IsCancel = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Update()
        {
            if (UpdateHandle != null) UpdateHandle();

            dueTs = Stopwatch.GetTimestamp();
        }
    }
}
