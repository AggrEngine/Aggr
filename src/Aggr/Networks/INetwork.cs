using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        void Send(byte[] data, int offset, int count);

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
