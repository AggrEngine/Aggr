using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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

        public static void Print(string str, ConsoleColor? color = null)
        {
            if (color.HasValue)
            {
                ConsoleColor c = Console.ForegroundColor;
                Console.ForegroundColor = color.Value;
                Console.WriteLine(str);
                Console.ForegroundColor = c;
            }
            else
            {
                Console.WriteLine(str);
            }
        }

        internal static void Start(string[] args)
        {
            AppAssemblyFile = args[0];
            if (args.Length > 1)
            {
                AppServerId = args[1];
            }
            var execulePath = AppDomain.CurrentDomain.BaseDirectory;
            CurrentDirectory = Environment.CurrentDirectory;
            AppPath = Path.Combine(CurrentDirectory, Path.GetDirectoryName(AppAssemblyFile));
            string appName = Path.Combine(AppPath, Path.GetFileNameWithoutExtension(AppAssemblyFile) + ".dll");
            string domainPath = Path.Combine(AppPath, "domain_temp");
            AppDomainSetup setup = new AppDomainSetup();
            setup.ApplicationBase = AppPath;
            setup.PrivateBinPath = "bin;bin/debug;bin/release";
            setup.ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            setup.CachePath = domainPath;
            setup.ShadowCopyFiles = "true";
            setup.ShadowCopyDirectories = AppPath;
            setup.ApplicationName = AppServerId;
            //AppDomain.CurrentDomain.SetShadowCopyFiles();

            // Set up the Evidence
            Evidence baseEvidence = AppDomain.CurrentDomain.Evidence;
            Evidence evidence = new Evidence(baseEvidence);
            // Create the AppDomain
            appDomain = AppDomain.CreateDomain("Domain" + AppServerId, evidence, setup);
            appDomain.SetData("AppPath", AppPath);
            appDomain.SetData("ServerID", AppServerId);
            var type = typeof(RemoteLoader);
            string name = type.Assembly.GetName().FullName;
            var remoteLoader = (RemoteLoader)appDomain.CreateInstanceAndUnwrap(name, type.FullName);
            Print(appName);
            Print(string.Format("App {0} PID:{1} is started...", MasterAppId, Process.GetCurrentProcess().Id));
            remoteLoader.ExecuteMothod(appName, MainClass, "StartRun");

            if (AppServerId == MasterAppId)
            {
                //TODO read config
                StartServerProcess("connector-server-1");
                StartServerProcess("connector-server-2");

            }

            remoteLoader.ExecuteMothod(appName, MainClass, "RunWaitLoop");
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
            Print(string.Format("App {0} PID:{1} is started...", serverID, process.Id));
        }
    }
}
