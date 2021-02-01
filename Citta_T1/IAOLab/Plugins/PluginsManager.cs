using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
            DLLPlugin dllPlugin;
            if (!File.Exists(dll))
                return;
            dllPlugin = GetPlugin(dll);
            if (dllPlugin == DLLPlugin.Empty)
                return;
            if (!Plugins.FindAll(x => x.GetPluginName() == dllPlugin.GetPluginName()).IsEmpty())
                return;
            this.Plugins.Add(dllPlugin);
            Global.GetIAOLabControl().GenIAOButton(dllPlugin);
        }
        private DLLPlugin GetPlugin(string dll)
        {
            var assembly = Assembly.LoadFrom(dll);
            Type[] clazzs = assembly.GetTypes();
            foreach (Type type in clazzs)
            {
                // dll需只有一个类接口IPlugin
                if (type.GetInterface("IPlugin") == null)
                   continue;
                var instance = assembly.CreateInstance(type.FullName);
                return new DLLPlugin(type, instance);
            }
            return DLLPlugin.Empty;
        }


    }
}
