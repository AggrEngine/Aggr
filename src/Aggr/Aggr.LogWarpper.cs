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
        static class LogWarpper
        {
            private static readonly object syncRoot = new object();


            internal static void WriteInfo(string message, params object[] args)
            {
                //todo
                WriteOutConsole(string.Format(message, args));
            }

            internal static void WriteDebug(string message, params object[] args)
            {
                //todo
                WriteOutConsole(string.Format(message, args), ConsoleColor.Green);
            }

            internal static void WriteWarn(string message, object[] args)
            {
                //todo
                WriteOutConsole(string.Format(message, args), ConsoleColor.Yellow);
            }

            internal static void WriteError(string message, Exception error)
            {
                //todo
                if (error == null)
                {
                    WriteOutConsole(message, ConsoleColor.Red);
                    return;
                }
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
