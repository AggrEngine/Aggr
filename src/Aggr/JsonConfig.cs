using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggrEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonConfig : Dictionary<string, object>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonConfig GetChaidren(string name)
        {
            return this[name] as JsonConfig;
        }
    }
}
