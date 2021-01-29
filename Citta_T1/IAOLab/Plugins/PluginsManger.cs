using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.IAOLab.Plugins
{
    class PluginsManger
    {
        private static PluginsManger pluginsManger;

        private List<IPlugin> plugins;
        public PluginsManger Instance()
        {
            if (pluginsManger == null)
                pluginsManger = new PluginsManger();
            return pluginsManger;
        }

        private PluginsManger()
        {
            plugins = new List<IPlugin>
            {
                new ApkToolPlugin(),
                new BaseStationPlugin(),
                new WifiLocationPlugin(),
                new BankPlugin(),
                new GPSTransformPlugin(),
                new TimeAndIPTransformPlugin()
            };

        }
    }
}
