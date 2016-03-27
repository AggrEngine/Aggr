using System;
using System.Threading;
using System.Threading.Tasks;

namespace AggrEngine
{
    static class AppManager
    {
        
        static double num;
        static long count;

        private static void TestUpdate()
        {
            num += Runtime.DueTime;
            count++;
            Console.Write("{0}:{1}\r",count, num);
        }

        internal static void Start(string[] args)
        {
            //todo test begin
            Aggr.Info("test info...");
            Aggr.Debug("test debug...");
            Aggr.Warn("test warn...");
            Aggr.Error("test error...", new Exception("error"));
            Runtime.UpdateHandle += TestUpdate;
            //10s stop
            Task.Factory.StartNew(() => {
                Thread.Sleep(10 * 1000);
                Console.WriteLine();
                Runtime.Stop();
                Console.WriteLine("stop.");
            });
            //test end

            RunAsync().Wait();
        }
        private static Task RunAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                Runtime.Start();
                while (!Runtime.IsCancel)
                {
                    try
                    {
                        Runtime.Update();
                    }
                    catch (Exception e)
                    {
                        Aggr.Error("App runtime Update method error", e);
                    }
                    Thread.Sleep(Runtime.Frequency);
                }
            });
        }
    }
}
