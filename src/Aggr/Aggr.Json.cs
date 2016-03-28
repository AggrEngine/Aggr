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
        /// Crypto encrypt collection
        /// </summary>
        public static class Json
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public static string ToString(object obj)
            {
                return "{}";
            }

            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="str"></param>
            /// <returns></returns>
            public static T Parse<T>(string str) where T : new()
            {
                return new T();
            }
        }
    }
}
