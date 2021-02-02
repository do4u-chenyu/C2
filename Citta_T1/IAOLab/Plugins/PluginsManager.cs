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
            Plugins = new List<IPlugin>();
            /*{
                new ApkToolPlugin(),
                new BaseStationPlugin(),
                new WifiLocationPlugin(),
                new BankPlugin(),
                new GPSTransformPlugin(),
                new TimeAndIPTransformPlugin()
            };*/
            PluginsDownloader.Instance.DownloadEvent += Refresh;

        }
        public Dictionary<string, IPlugin> defaultPlugin = new Dictionary<string, IPlugin>()
        { 
            {"APK", new ApkToolPlugin() },
            {"BaseStation", new BaseStationPlugin()},
            {"Wifi",new WifiLocationPlugin()},
            {"Card", new BankPlugin()},
            {"Tude",new GPSTransformPlugin()},
            {"Ip",  new TimeAndIPTransformPlugin()}
        };
        public void AddPlugin(string type)
        {
            if (defaultPlugin.Keys._Contains(type))
                this.Plugins.Add(defaultPlugin[type]);
        }
        public IPlugin FindPlugin(string name)
        {
            int index = Plugins.FindIndex(e => e.GetPluginName() == name);
            return index < 0 ? new EmptyPlugin() : Plugins[index];
        }

        public void Refresh()
        {
            foreach (string dll in FileUtil.TryListFiles(Global.DLLPluginPath, "*.dll"))
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
            try
            {
                if (!Plugins.FindAll(x => x.GetPluginName() == dllPlugin.GetPluginName()).IsEmpty())
                    return;
                Global.GetIAOLabControl().GenIAOButton(dllPlugin);
                this.Plugins.Add(dllPlugin);
            }
            catch(Exception ex)
            {
                HelpUtil.ShowMessageBox(ex.Message);
            }
           
           
           
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
