using C2.Utils;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace C2.IAOLab.Plugins
{
    class PluginsManager
    {
        private static PluginsManager pluginsManager;

        public List<IPlugin> Plugins { get; }

        public static PluginsManager Instance
        {
            get {
                if (pluginsManager == null)
                    pluginsManager = new PluginsManager();
                return pluginsManager;
            }
        }


        private PluginsManager()
        {
            Plugins = new List<IPlugin>
            {
                new ApkToolPlugin(),
                new BaseStationPlugin(),
                new WifiLocationPlugin(),
                new BankPlugin(),
                new GPSTransformPlugin(),
                new TimeAndIPTransformPlugin()
            };
            PluginsDownloader.Instance.DownloadEvent += Refresh;

        }

        public IPlugin FindPlugin(string name)
        {
            int index = Plugins.FindIndex(e => e.GetPluginName() == name);
            return index < 0 ? new EmptyPlugin() : Plugins[index];
        }

        public void Refresh()
        {
            string pluginsDir = Path.Combine(Application.StartupPath, "plugins");
            foreach (string dll in FileUtil.TryListFiles(pluginsDir, "*.dll"))
                TryLoadOne(dll);
        }

        private void TryLoadOne(string dll)
        {
            if (!File.Exists(dll))
                return;
            // TODO 继续施工中
        }
        #region 持久化
        private void Load()
        { 
        }
        private void Save()
        { 

        }
        #endregion

    }
}
