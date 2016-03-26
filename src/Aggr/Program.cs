using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggrEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AppManager.Start(args);
            }
            catch (Exception ex)
            {
                Aggr.Error("Aggr app error", ex);
            }
        }
    }
}
