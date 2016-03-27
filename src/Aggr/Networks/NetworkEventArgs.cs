using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggrEngine.Networks
{

    /// <summary>
    /// 
    /// </summary>
    public class NetworkEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public NetworkEventCode EventCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; }
    }
}
