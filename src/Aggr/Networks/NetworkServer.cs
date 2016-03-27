using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AggrEngine.Networks
{
    //todo
    class NetworkServer : INetwork
    {
        public NetworkConfigure Configure
        {
            get;
            set;
        }
        public IPEndPoint RemoteAddress
        {
            get;
            set;
        }
        public bool IsClient
        {
            get;
            set;
        }

        public void Start(NetworkConfigure configure)
        {
            Configure = configure;
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public byte[] ReceiveWait()
        {
            throw new NotImplementedException();
        }

        public void Notify(byte[] data, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public void Send(SessionIdentity sessionID, byte[] data, int offset, int count)
        {
            throw new NotImplementedException();
        }

    }
}
