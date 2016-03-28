using System;
using System.Collections.Generic;
using System.Linq;
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
