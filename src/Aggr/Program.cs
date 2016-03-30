using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AggrEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AppManager.Print("Aggr engine server app", ConsoleColor.Yellow);
                AppManager.Print(string.Format("  Version: {0}", Assembly.GetExecutingAssembly().GetName().Version), ConsoleColor.Yellow);
                AppManager.Print("Copyright (c) 2015-2018 AggrEngine, Inc.", ConsoleColor.Yellow);
                Console.WriteLine();
                if (args.Length == 0)
                {
                    //command help
                    AppManager.Print("Usage args <AssembllyName> [ServerID].", ConsoleColor.Red);
                    return;
                }
                Console.CancelKeyPress += OnCancel;
                AppManager.Start(args);
            }
            catch (Exception ex)
            {
                AppManager.Print(string.Format("Aggr app error:{0}-{1}", ex.Message, ex.StackTrace), ConsoleColor.Red);
            }
        }

        private static void OnCancel(object sender, ConsoleCancelEventArgs e)
        {
            AppManager.Stop();
        }
    }
}
