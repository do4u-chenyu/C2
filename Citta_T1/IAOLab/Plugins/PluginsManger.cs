using System.Collections.Generic;

namespace C2.IAOLab.Plugins
{
    class PluginsManger
    {
        private static PluginsManger pluginsManger;

        private List<IPlugin> plugins;

        internal List<IPlugin> Plugins { get => plugins; }

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
