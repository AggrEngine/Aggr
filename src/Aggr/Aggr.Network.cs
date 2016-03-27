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
        /// Network manager
        /// </summary>
        public static class Network
        {
            private static ConcurrentDictionary<string, INetwork> _pool = new ConcurrentDictionary<string, INetwork>();

            /// <summary>
            /// 
            /// </summary>
            /// <param name="networkID"></param>
            /// <returns></returns>
            public static INetwork Get(string networkID)
            {
                INetwork result;
                if (_pool.TryGetValue(networkID, out result))
                {
                    return result;
                }
                throw new NetworkException(string.Format("Network ID:{0} not found.", networkID));
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public static IEnumerable<INetwork> GetServer()
            {
                return _pool.Select(t => t.Value).Where(t => !t.IsClient);
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public static IEnumerable<INetwork> GetClient()
            {
                return _pool.Select(t => t.Value).Where(t => t.IsClient);
            }

            /// <summary>
            /// Start network service.
            /// </summary>
            /// <param name="configure"></param>
            public static void Start(NetworkConfigure configure)
            {
                INetwork network = configure.NetworkCreate == null ? new NetworkServer() : configure.NetworkCreate();
                network.Start(configure);
                _pool.TryAdd(configure.NetworkID, network);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="configure"></param>
            /// <returns></returns>
            public static INetwork Connect(NetworkConfigure configure)
            {
                INetwork network = configure.NetworkCreate == null ? new NetworkClient() : configure.NetworkCreate();
                network.Start(configure);
                _pool.TryAdd(configure.NetworkID, network);
                return network;
            }
        }

    }
}
