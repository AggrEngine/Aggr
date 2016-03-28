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
        
        internal static void Start(string[] args)
        {
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
            appDomain.SetData("AppPath", AppPath);
            appDomain.SetData("ServerID", AppServerId);
            var app = appDomain.CreateInstanceAndUnwrap(appName, MainClass) as AppBase;
            if (app != null)
            {
                Aggr.Info("App {0} PID:{1} is started...", MasterAppId, Process.GetCurrentProcess().Id);
                app.Process();
            }
            else
            {
                throw new Exception(string.Format("Not found class \"{0}.cs\".", MainClass));
            }
            if (AppServerId == MasterAppId)
            {
                //TODO read config
                StartServerProcess("connector-server-1");
                StartServerProcess("connector-server-2");

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
            foreach (var p in serverProcess)
            {
                if (!p.HasExited)
                {
                    p.Kill();
                    p.Dispose();
                }
            }
        }

        private static void StartServerProcess(string serverID)
        {
            Process process = new Process();
            process.StartInfo.FileName = "Aggr.exe";
            process.StartInfo.Arguments = string.Format("{0} {1}", AppAssemblyFile, serverID);
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            serverProcess.Add(process);
            Aggr.Info("App {0} PID:{1} is started...", serverID, process.Id);
        }
    }
}
