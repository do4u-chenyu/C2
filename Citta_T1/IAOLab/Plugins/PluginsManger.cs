using System.Collections.Generic;

namespace C2.IAOLab.Plugins
{
    class PluginsManger
    {
        private static PluginsManger pluginsManger;

        private List<IPlugin> plugins;

        public List<IPlugin> Plugins { get => plugins; }

        public static PluginsManger Instance
        {
            get {
                if (pluginsManger == null)
                    pluginsManger = new PluginsManger();
                return pluginsManger;
            }
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

        public IPlugin FindPlugin(string name)
        {
            int index = Plugins.FindIndex(e => e.GetPluginName() == name);
            return index < 0 ? new EmptyPlugin() : Plugins[index];
        }
    }
}
