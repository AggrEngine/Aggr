using AggrEngine.Networks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggrEngine
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AppBase : MarshalByRefObject
    {
        /// <summary>
        /// 
        /// </summary>
        public void Process()
        {
            Runtime.AppPath = AppDomain.CurrentDomain.GetData("AppPath") as string;
            ServerID = AppDomain.CurrentDomain.GetData("ServerID") as string;
            Start();
            StartAfter();
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
