using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace C2.IAOLab.Plugins
{
    class PluginsManager
    {
        private readonly static LogUtil log = LogUtil.GetInstance("PluginsManager");

        private static PluginsManager pluginsManager;
        
        private readonly Dictionary<string, IPlugin> plugins;

        private readonly PluginsDownloader downloader;

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

        public List<string> BrowserPluginsInfo()
        {
            //访问下载列表
            string htmlContent = downloader.GetHtmlContent(Global.DLLHostUrl);
            List<string> pluginsName = GetPluginsNameList(htmlContent);

            return downloader.GetPluginsInfoList(pluginsName, Global.DLLPackageUrl);
        }
        public List<string> GetPluginsNameList(string htmlContent)
        {
            List<string> result = new List<string>();
            string dllPattern = string.Format(@"\>(.*?info)\<");
            try
            {
                MatchCollection matchItems = Regex.Matches(htmlContent, dllPattern, RegexOptions.IgnoreCase);
                foreach (Match match in matchItems)
                {
                    string pluginName = match.Groups[1].Value;
                    result.Add(pluginName);
                }
            }
            catch
            { }
            return result;
        }
        public List<string> UpdatablePluginList()
        {
            List<string> webPlugins = BrowserPluginsInfo();
            // 例如: 2048111\tV3.1.4\t描述信息2049
            foreach (IPlugin plugin in PluginsManager.Instance.Plugins)
            {
                webPlugins.RemoveAll(x => x.StartsWith(plugin.GetPluginName() + OpUtil.TabSeparator + plugin.GetPluginVersion()));
            }
            return webPlugins;
        }

        public void DownloadPlugin(string pluginName)
        {
            string selectedDll = Global.DLLPackageUrl + pluginName;
            string savePath = Path.Combine(Global.LocalPluginPath, pluginName);
            downloader.PluginDownload(selectedDll, savePath);
        }

        private PluginsManager()
        {
            plugins = new Dictionary<string, IPlugin>() { };
            downloader = new PluginsDownloader();
        }

        public IPlugin FindPlugin(string pluginName)
        {
            return plugins.ContainsKey(pluginName) ? plugins[pluginName] : DLLPlugin.Empty;
        }

        public void Refresh()
        {
            foreach (string dll in FileUtil.TryListFiles(Global.LocalPluginPath, "*.dll"))
                TryLoadOne(dll);
            foreach (string exe in FileUtil.TryListFiles(Global.LocalPluginPath, "*.exe"))
                TryLoadOne(exe);
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
