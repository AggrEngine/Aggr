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
        /// Info
        /// </summary>
        public static LoggerHandle InfoHandle;
        /// <summary>
        /// Debug
        /// </summary>
        public static LoggerHandle DebugHandle;
        /// <summary>
        /// Warn
        /// </summary>
        public static LoggerHandle WarnHandle;
        /// <summary>
        /// Error
        /// </summary>
        public static LoggerErrorHandle ErrorHandle;
        /// <summary>
        /// Ignore BOM head of utf8 encode
        /// </summary>
        public static readonly Encoding Utf8 = new UTF8Encoding(false);

        static Aggr()
        {
        }

        /// <summary>
        /// Write log of info level
        /// </summary>
        public static void Info(string message, params object[] args)
        {
            if (InfoHandle != null) InfoHandle(message, args);
            else LogWarpper.WriteInfo(message, args);
        }

        /// <summary>
        /// Write log of debug level
        /// </summary>
        public static void Debug(string message, params object[] args)
        {
            if (DebugHandle != null) DebugHandle(message, args);
            else LogWarpper.WriteDebug(message, args);
        }

        /// <summary>
        /// Write log of warn level
        /// </summary>
        public static void Warn(string message, params object[] args)
        {
            if (WarnHandle != null) WarnHandle(message, args);
            else LogWarpper.WriteWarn(message, args);
        }
        /// <summary>
        /// Write log of error level
        /// </summary>
        /// <param name="message"></param>
        /// <param name="error"></param>
        public static void Error(string message, Exception error)
        {
            if (ErrorHandle != null) ErrorHandle(message, error);
            else LogWarpper.WriteError(message, error);
        }
    }
}
