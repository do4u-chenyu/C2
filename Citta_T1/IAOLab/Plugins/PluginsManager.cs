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

        public Dictionary<string, IPlugin> plugins;

        public ICollection<IPlugin> Plugins 
        { 
            get { return plugins.Values; } 
        }

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
            plugins = new Dictionary<string, IPlugin>() { };
            //PluginsDownloader.Instance.DownloadEvent += Refresh;
        }

        public IPlugin FindPlugin(string pluginName)
        {
            return plugins.ContainsKey(pluginName) ? plugins[pluginName] : new EmptyPlugin();
        }

        public void Refresh()
        {
            foreach (string dll in FileUtil.TryListFiles(Global.DLLPluginPath, "*.dll"))
                TryLoadOne(dll);
        }

        private void TryLoadOne(string ffp)
        {    
            DLLPlugin dll = LoadPlugin(ffp);

            if (dll == DLLPlugin.Empty)
                return;

            string dll_name = dll.GetPluginName();

            if (!plugins.ContainsKey(dll_name))
                plugins.Add(dll_name, dll);

            return;
        }
        private DLLPlugin LoadPlugin(string ffp)
        {
            if (!File.Exists(ffp))
                return DLLPlugin.Empty;

            var assembly = Assembly.LoadFrom(ffp);
            Type[] types = assembly.GetTypes();

            Type type = types.Find(t => t.GetInterface("IPlugin") != null);
            if (type == null)
                return DLLPlugin.Empty;

            object obj = assembly.CreateInstance(type.FullName);
            return Check(type, obj) ? new DLLPlugin(type, obj) : DLLPlugin.Empty;
        }

        private bool Check(Type type, object dll)
        {
            try 
            {
                if (type.GetMethod("GetPluginDescription") == null)
                    return false;

                if (type.GetMethod("GetPluginName") == null)
                    return false;

                if (type.GetMethod("GetPluginVersion") == null)
                    return false;

                if (type.GetMethod("GetPluginImage") == null)
                    return false;

                return true;
            }
            catch 
            {
                return false;
            }
        }


    }
}
