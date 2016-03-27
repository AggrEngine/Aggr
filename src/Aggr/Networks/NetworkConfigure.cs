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
    /// <param name="nea"></param>
    public delegate void NetworkHandle(INetworkSession session, NetworkEventArgs nea);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public delegate INetwork NetworkFactory();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sessionID"></param>
    /// <returns></returns>
    public delegate INetworkSession SessionFactory(uint sessionID);

    /// <summary>
    /// Network configure info
    /// </summary>
    public class NetworkConfigure
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="networkID"></param>
        /// <param name="networkFactory"></param>
        /// <param name="sessionFactory"></param>
        public NetworkConfigure(string networkID, string url, NetworkFactory networkFactory = null, SessionFactory sessionFactory = null)
        {
            IsHttp = true;
            NetworkID = networkID;
            Url = url;
            NetworkCreate = networkFactory;
            SessionCreate = sessionFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="networkID"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="networkFactory"></param>
        /// <param name="sessionFactory"></param>
        public NetworkConfigure(string networkID, string host, int port, NetworkFactory networkFactory = null, SessionFactory sessionFactory = null)
        {
            NetworkID = networkID;
            Host = host;
            Port = port;
            NetworkCreate = networkFactory;
            SessionCreate = sessionFactory;
        }
        /// <summary>
        /// 
        /// </summary>
        public string NetworkID { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Host { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public int Port { get; private set; }
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
        public int ConnectTimeout { get; set; }

        internal NetworkFactory NetworkCreate;
        //session object creator
        internal SessionFactory SessionCreate;

        /// <summary>
        /// Accept connection of client callback function.
        /// </summary>
        public event NetworkHandle OnAccepted;
        /// <summary>
        /// Connect successfully to server callback function.
        /// </summary>
        public event NetworkHandle OnConnected;
        /// <summary>
        /// Handshake successfully of sever and client callback function.
        /// </summary>
        public event NetworkHandle OnHandshaked;
        /// <summary>
        /// 
        /// </summary>
        public event NetworkHandle OnReceived;
        /// <summary>
        /// 
        /// </summary>
        public event NetworkHandle OnClosed;
        /// <summary>
        /// Process failed callback function.
        /// </summary>
        public event NetworkHandle OnFailed;

        internal void DoAccepted(INetworkSession session, NetworkEventArgs nea)
        {
            if (OnAccepted != null) OnAccepted(session, nea);
        }

        internal void DoConnected(INetworkSession session, NetworkEventArgs nea)
        {
            if (OnConnected != null) OnConnected(session, nea);
        }

        internal void DoHandshaked(INetworkSession session, NetworkEventArgs nea)
        {
            if (OnHandshaked != null) OnHandshaked(session, nea);
        }

        internal void DoReceived(INetworkSession session, NetworkEventArgs nea)
        {
            if (OnReceived != null) OnReceived(session, nea);
        }

        internal void DoClosed(INetworkSession session, NetworkEventArgs nea)
        {
            if (OnClosed != null) OnClosed(session, nea);
        }
        internal void DoFailed(INetworkSession session, NetworkEventArgs nea)
        {
            if (OnFailed != null) OnFailed(session, nea);
        }
    }
}
