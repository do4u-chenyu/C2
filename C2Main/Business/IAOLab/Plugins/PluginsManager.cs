using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace C2.IAOLab.Plugins
{
    class PluginsManager
    {
        private readonly static LogUtil log = LogUtil.GetInstance("PluginsManager");

        private static PluginsManager pluginsManager;
        
        private readonly Dictionary<string, IPlugin> plugins;

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
        }

        public void Refresh()
        {
            string[] array = FileUtil.TryListFiles(Global.LocalPluginsPath, "*.dll");
            Array.Reverse(array);
            if (array.Length > 3)
            {
                string tmp = array[0];
                array[0] = array[2];
                array[2] = tmp;
            }
            foreach (string dll in array)
                TryLoadOne(dll);
            //foreach (string exe in FileUtil.TryListFiles(Global.LocalPluginsPath, "*.exe"))
            //    TryLoadOne(exe);
        }

        private void TryLoadOne(string ffp)
        {    
            IPlugin dll = LoadDll(ffp);

            string name = dll.GetPluginName();

            if (String.IsNullOrEmpty(name))
                return;

            if (!plugins.ContainsKey(name))
            {
                plugins[name] = dll;
                return;
            }
                
            // 存在相同名称插件时，加载最新版本
            string newVersion = plugins[name].GetPluginVersion();
            string oldVersion = dll.GetPluginVersion();
            if (newVersion.CompareTo(oldVersion) < 0)
                plugins[name] = dll;
        }

        private IPlugin LoadDll(string ffp)
        {
            if (!File.Exists(ffp))
                return DLLPlugin.Empty;

            try 
            {
                
                Assembly assembly = Assembly.LoadFile(ffp);
               
                Type[] types = assembly.GetTypes();

                Type type = types.Find(t => t.GetInterface("IPlugin") != null);

                if (type == null)
                    return DLLPlugin.Empty;

                object obj = assembly.CreateInstance(type.FullName);
                return Check(type) ? new DLLPlugin(type, obj) : DLLPlugin.Empty;
            }
            catch (Exception ex)
            {
                log.Warn(ffp + "插件加载失败:" + ex.Message);
                return DLLPlugin.Empty;
            }
          
        }

        private bool Check(Type type)
        {
            string[] methods = new string[] { "GetPluginDescription", 
                                              "GetPluginName", 
                                              "GetPluginVersion", 
                                              "GetPluginImage", 
                                              "ShowFormDialog" };
            bool ret = true;

            try 
            {
                foreach(string m in methods)
                    ret = ret && type.GetMethod(m) != null;  // 一个都不能少
            }
            catch { ret = false; }

            return ret;
        }
    }
}
