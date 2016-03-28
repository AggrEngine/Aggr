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
                Console.WriteLine("Aggr engine server app");
                Console.WriteLine("  Version: {0}", Assembly.GetExecutingAssembly().GetName().Version);
                Console.WriteLine("Copyright (c) 2015-2018 AggrEngine, Inc.");
                Console.WriteLine();
                if (args.Length == 0)
                {
                    //command help
                    Aggr.Info("Usage args <AssembllyName> [ServerID].");
                    return;
                }
                Console.CancelKeyPress += OnCancel;
                AppManager.Start(args);
            }
            catch (Exception ex)
            {
                Aggr.Error("Aggr app error", ex);
            }
        }

        private static void OnCancel(object sender, ConsoleCancelEventArgs e)
        {
            AppManager.Stop();
        }
    }
}
