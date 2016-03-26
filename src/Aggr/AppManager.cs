using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggrEngine
{
    static class AppManager
    {
        internal static void Start(string[] args)
        {
            Aggr.LogWarpper.Init();

            Aggr.Info("test info...");
            Aggr.Debug("test debug...");
            Aggr.Error("test error...", new Exception());
        }
    }
}
