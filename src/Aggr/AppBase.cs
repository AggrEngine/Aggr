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
            Runtime.ServerID = AppDomain.CurrentDomain.GetData("ServerID") as string;

            Start();
            StartAfter();
        }
        /// <summary>
        /// 
        /// </summary>
        protected abstract void Start();
    
        /// <summary>
        /// 
        /// </summary>
        private void StartAfter()
        {
        }  
        /// <summary>
        /// 
        /// </summary>
        protected virtual void Stop()
        {

        }

    }
}
