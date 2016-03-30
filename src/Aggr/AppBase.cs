using AggrEngine.Networks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AggrEngine
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AppBase
    {
        /// <summary>
        /// call enter main method.
        /// </summary>
        public void StartRun()
        {
            Runtime.AppPath = AppDomain.CurrentDomain.GetData("AppPath") as string;
            ServerID = AppDomain.CurrentDomain.GetData("ServerID") as string;
            Start();
            StartAfter();
        }
        /// <summary>
        /// 
        /// </summary>
        public void RunWaitLoop()
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
        }
        /// <summary>
        /// 
        /// </summary>
        protected NetworkConfigure configure;
        /// <summary>
        /// 
        /// </summary>
        public string ServerID { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        protected abstract void Start();

        /// <summary>
        /// 
        /// </summary>
        private void StartAfter()
        {
            //TODO network
            if (configure != null)
            {
                Aggr.Network.Start(configure);
                Aggr.Debug("{0} network.io host:{1} port:{2} is started...", ServerID, configure.Host, configure.Port);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected virtual void Stop()
        {

        }

    }
}
