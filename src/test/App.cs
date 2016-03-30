using AggrEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class App : AppBase
{
    protected override void Start()
    {
        Aggr.Debug("test app start...");
        Aggr.Debug("test2...");
        var masterConfig = Runtime.LoadConfig("./config/master.json");
       
        configure = new AggrEngine.Networks.NetworkConfigure("connector", "127.0.0.1", 5301);
    }
}
