using AggrEngine.Networks;
using System;
using System.Collections.Concurrent;
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
        /// 
        /// </summary>
        public struct RouterType
        {
            /// <summary>
            /// 
            /// </summary>
            public uint TpyeID;
        }

        /// <summary>
        /// Network router ID.
        /// </summary>
        public struct RouterIdentity
        {
            /// <summary>
            /// 
            /// </summary>
            public uint ID;
        }

        /// <summary>
        /// Network rpc
        /// </summary>
        public static class Rpc
        {
            private static ConcurrentDictionary<RouterIdentity, string> _routerMap;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="collection"></param>
            public static void Init(IEnumerable<KeyValuePair<RouterIdentity, string>> collection)
            {
                _routerMap = new ConcurrentDictionary<RouterIdentity, string>(collection);
            }

            private static void CheckRouter()
            {
                if (_routerMap == null) throw new NetworkException("Rpc router init fail.");
            }

            private static string GetRouter(RouterIdentity routerID)
            {
                string networkID;
                if (_routerMap.TryGetValue(routerID, out networkID))
                {
                    return networkID;
                }
                throw new NetworkException(string.Format("Rpc router ID:{0} not found.", routerID.ID));
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="routerID"></param>
            /// <param name="networkID"></param>
            public static void UpdateRouter(RouterIdentity routerID, string networkID)
            {
                CheckRouter();
                _routerMap[routerID] = networkID;
            }

            /// <summary>
            /// Notify message of server to client
            /// </summary>
            /// <param name="routerID"></param>
            /// <param name="message"></param>
            /// <exception cref="Exception"></exception>
            public static void Notify(RouterIdentity routerID, string message)
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
            public static void Notify(RouterIdentity routerID, byte[] data, int offset, int count)
            {
                CheckRouter();
                INetwork netwrok = Network.Get(GetRouter(routerID));
                netwrok.Notify(data, offset, count);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="routerID"></param>
            /// <param name="sessionID"></param>
            /// <param name="message"></param>
            public static void SendMessage(RouterIdentity routerID, SessionIdentity sessionID, string message)
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
            public static void SendMessage(RouterIdentity routerID, SessionIdentity sessionID, byte[] data, int offset, int count)
            {
                CheckRouter();
                INetwork netwrok = Network.Get(GetRouter(routerID));
                netwrok.Send(sessionID, data, offset, count);
            }
        }

    }
}
