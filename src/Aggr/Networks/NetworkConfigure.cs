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
    /// <param name="session"></param>
    public delegate void NetworkHandle(INetworkSession session);

    /// <summary>
    /// Network configure info
    /// </summary>
    public class NetworkConfigure
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public NetworkConfigure(string url)
        {
            IsHttp = true;
            Url = url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public NetworkConfigure(string host, int port)
        {
            Host = host;
            Port = port;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsHttp { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int MaxConnection { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public NetworkHandle OnConnected;
        /// <summary>
        /// 
        /// </summary>
        public NetworkHandle OnHandshaked;
        /// <summary>
        /// 
        /// </summary>
        public NetworkHandle OnReceived;
        /// <summary>
        /// 
        /// </summary>
        public NetworkHandle OnClosed;
        /// <summary>
        /// 
        /// </summary>
        public NetworkHandle OnFailed;
    }
}
