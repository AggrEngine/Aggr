using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AggrEngine.Networks
{
    /// <summary>
    /// Network connector interface
    /// </summary>
    public interface INetwork
    {

        /// <summary>
        /// 
        /// </summary>
        NetworkConfigure Configure { get; }
        /// <summary>
        /// 
        /// </summary>
        IPEndPoint RemoteAddress { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsClient { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configure"></param>
        void Start(NetworkConfigure configure);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        void Notify(byte[] data, int offset, int count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        void Send(SessionIdentity sessionID, byte[] data, int offset, int count);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        byte[] ReceiveWait();

        /// <summary>
        /// 
        /// </summary>
        void Close();
    }
}
