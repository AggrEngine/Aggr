using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggrEngine
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="args"></param>
    public delegate void LoggerHandle(string message, params object[] args);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="error"></param>
    public delegate void LoggerErrorHandle(string message, Exception error);

    /// <summary>
    /// AggrEngine API
    /// </summary>
    public static partial class Aggr
    {
        /// <summary>
        /// 
        /// </summary>
        public static LoggerHandle InfoHandle;
        /// <summary>
        /// 
        /// </summary>
        public static LoggerHandle DebugHandle;
        /// <summary>
        /// 
        /// </summary>
        public static LoggerErrorHandle ErrorHandle;

        static Aggr()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Info(string message, params object[] args)
        {
            if (InfoHandle != null) InfoHandle(message, args);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Debug(string message, params object[] args)
        {
            if (DebugHandle != null) DebugHandle(message, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="error"></param>
        public static void Error(string message, Exception error)
        {
            if (ErrorHandle != null) ErrorHandle(message, error);
        }
    }
}
