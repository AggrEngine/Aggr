using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;

namespace AggrEngine
{
    static class AppManager
    {

        private const string MasterAppId = "master";
        private const string MainClass = "App";
        private static AppDomain appDomain;
        private static string CurrentDirectory;
        internal static string AppAssemblyFile { get; set; }
        internal static string AppPath { get; set; }
        public static string AppServerId { get; internal set; } = MasterAppId;
        static List<Process> serverProcess = new List<Process>();

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



            AppAssemblyFile = args[0];
            if (args.Length > 1)
            {
                AppServerId = args[1];
            }
            CurrentDirectory = Environment.CurrentDirectory;
            AppPath = Path.Combine(CurrentDirectory, Path.GetDirectoryName(AppAssemblyFile));
            string appName = Path.GetFileNameWithoutExtension(AppAssemblyFile);
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = AppPath;
            setup.PrivateBinPath = "bin;bin/debug;bin/release";
            setup.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            setup.ShadowCopyFiles = "true";
            setup.ShadowCopyDirectories = setup.ApplicationBase;
            setup.ApplicationName = AppServerId;

            // Set up the Evidence
            Evidence baseEvidence = AppDomain.CurrentDomain.Evidence;
            Evidence evidence = new Evidence(baseEvidence);
            // Create the AppDomain
            appDomain = AppDomain.CreateDomain(AppServerId, evidence, setup);
            appDomain.SetData("ServerID", AppServerId);
            var app = appDomain.CreateInstanceAndUnwrap(appName, MainClass) as AppBase;
            if (app != null)
            {
                app.Process();
            }
            else
            {
                throw new Exception(string.Format("Not found class \"{0}.cs\".", MainClass));
            }
            if (AppServerId == MasterAppId)
            {
                //StartServerProcess("connector-server-1");
                //StartServerProcess("connector-server-2");

                //checkOutputTimer = new Timer(OnCheckOutput, null, 1000, 100);
            }
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

        /// <summary>
        /// 
        /// </summary>
        public static void Stop()
        {

        }
    }
}
