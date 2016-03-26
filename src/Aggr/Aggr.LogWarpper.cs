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

        /// <summary>
        /// Logger
        /// </summary>
        public static class LogWarpper
        {
            private static readonly object syncRoot = new object();
            /// <summary>
            /// use aggr provide logger lib
            /// </summary>
            public static void Init()
            {
                if (Aggr.InfoHandle == null)
                {
                    Aggr.InfoHandle += WriteInfo;
                }
                if (Aggr.DebugHandle == null)
                {
                    Aggr.DebugHandle += WriteDebug;
                }
                if (Aggr.ErrorHandle == null)
                {
                    Aggr.ErrorHandle += WriteError;
                }
            }

            private static void WriteInfo(string message, params object[] args)
            {
                //todo
                WriteOutConsole(string.Format(message, args));
            }

            private static void WriteDebug(string message, params object[] args)
            {
                //todo
                WriteOutConsole(string.Format(message, args), ConsoleColor.Yellow);
            }

            private static void WriteError(string message, Exception error)
            {
                //todo
                WriteOutConsole(string.Format("{0}-{1}:{2}", message, error.Message, error.StackTrace), ConsoleColor.Red);
            }

            private static void WriteOutConsole(string str, ConsoleColor? color = null)
            {
                if (color.HasValue)
                {
                    lock (syncRoot)
                    {
                        ConsoleColor c = Console.ForegroundColor;
                        Console.ForegroundColor = color.Value;
                        Console.WriteLine(str);
                        Console.ForegroundColor = c;
                    }
                }
                else
                {
                    Console.WriteLine(str);
                }
            }
        }
    }
}
