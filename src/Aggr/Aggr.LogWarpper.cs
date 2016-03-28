using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggrEngine
{
    /// <summary>
    /// AggrEngine API
    /// </summary>
    public static partial class Aggr
    {
        enum LogLevel
        {
            Info,
            Debug,
            Warn,
            Error
        }
        /// <summary>
        /// Logger
        /// </summary>
        static class LogWarpper
        {
            private static readonly object syncLock = new object();
            public static string LayerOutFormat { get; set; } = "$DateTime [$Level] $Message";

            internal static void WriteInfo(string message, params object[] args)
            {
                //todo
                WriteOutConsole(LogLevel.Info, string.Format(message, args));
            }

            internal static void WriteDebug(string message, params object[] args)
            {
                //todo
                WriteOutConsole(LogLevel.Debug, string.Format(message, args), ConsoleColor.Green);
            }

            internal static void WriteWarn(string message, object[] args)
            {
                //todo
                WriteOutConsole(LogLevel.Warn, string.Format(message, args), ConsoleColor.Yellow);
            }

            internal static void WriteError(string message, Exception error)
            {
                //todo
                if (error == null)
                {
                    WriteOutConsole(LogLevel.Error, message, ConsoleColor.Red);
                    return;
                }
                WriteOutConsole(LogLevel.Error, string.Format("{0}-{1}:{2}", message, error.Message, error.StackTrace), ConsoleColor.Red);
            }

            private static void WriteOutConsole(LogLevel logLevel, string str, ConsoleColor? color = null)
            {
                if (color.HasValue)
                {
                    lock (syncLock)
                    {
                        ConsoleColor c = Console.ForegroundColor;
                        Console.ForegroundColor = color.Value;
                        Console.WriteLine(FormatMessage(str, logLevel));
                        Console.ForegroundColor = c;
                    }
                }
                else
                {
                    Console.WriteLine(FormatMessage(str, logLevel));
                }
            }

            private static string FormatMessage(string str, LogLevel logLevel)
            {
                return LayerOutFormat.Replace("$Level", logLevel.ToString())
                    .Replace("$DateTime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
                    .Replace("$Message", str);
            }
        }
    }
}
