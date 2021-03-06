﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggrEngine.Networks
{
    /// <summary>
    /// 
    /// </summary>
    public struct SessionIdentity
    {
        /// <summary>
        /// 
        /// </summary>
        public uint ID;

    }

    /// <summary>
    /// Network session of client
    /// </summary>
    public interface INetworkSession
    {
        /// <summary>
        /// 
        /// </summary>
        SessionIdentity SessionID { get; }

        /// <summary>
        /// 
        /// </summary>
        INetwork Network { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="network"></param>
        void Bind(INetwork network);
        /// <summary>
        /// 
        /// </summary>
        void UnBind();
    }
}
