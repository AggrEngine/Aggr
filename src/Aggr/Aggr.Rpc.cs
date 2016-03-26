using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggrEngine
{
    /// <summary>
    /// AggrEngine API
    /// </summary>
    public static partial class Aggr
    {

        /// <summary>
        /// Network rpc
        /// </summary>
        public static class Rpc
        {
            /// <summary>
            /// Notify message of server to client
            /// </summary>
            /// <param name="routerID"></param>
            /// <param name="message"></param>
            /// <exception cref="Exception"></exception>
            public static void Notify(uint routerID, string message)
            {
                var data = Utf8.GetBytes(message);
                Notify(routerID, data, 0, data.Length);
            }
            /// <summary>
            /// Notify message of server to client
            /// </summary>
            /// <param name="routerID"></param>
            /// <param name="data"></param>
            /// <param name="offset"></param>
            /// <param name="count"></param>
            /// <exception cref="Exception"></exception>
            public static void Notify(uint routerID, byte[] data, int offset, int count)
            {
                //todo
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="routerID"></param>
            /// <param name="sessionID"></param>
            /// <param name="message"></param>
            public static void SendMessage(uint routerID, uint sessionID, string message)
            {
                var data = Utf8.GetBytes(message);
                SendMessage(routerID, sessionID, data, 0, data.Length);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="routerID"></param>
            /// <param name="sessionID"></param>
            /// <param name="data"></param>
            /// <param name="offset"></param>
            /// <param name="count"></param>
            /// <exception cref="Exception"></exception>
            public static void SendMessage(uint routerID, uint sessionID, byte[] data, int offset, int count)
            {
                //todo
            }
        }

    }
}
