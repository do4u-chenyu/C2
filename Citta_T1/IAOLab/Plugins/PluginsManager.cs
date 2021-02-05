using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.IAOLab.Plugins
{
    class PluginsManager
    {
        private readonly static LogUtil log = LogUtil.GetInstance("PluginsManager");

        private static PluginsManager pluginsManager;
        
        private Dictionary<string, IPlugin> plugins;

        private PluginsDownloader downloader;

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
            List<string> pluginInfo = WebPluginList(htmlContent);

            return downloader.WebPluginInfo(pluginInfo, Global.DLLPackageUrl);
        }
        private List<string> WebPluginList(string webContent)
        {
            List<string> result = new List<string>();
            string dllPattern = string.Format(@"\>(.*?info)\<");
            try
            {
                MatchCollection matchItems = Regex.Matches(webContent, dllPattern, RegexOptions.IgnoreCase);
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
                webPlugins.RemoveAll(x => x.StartsWith(plugin.GetPluginName() + OpUtil.TabSeparator));
            }
            return webPlugins;
        }

        /// <summary>
        /// 异常：
        /// <para>DownloadFailureException</para>
        /// </summary>
        public void DownloadPlugin(string pluginName)
        {
            string selectedDll = Global.DLLPackageUrl + pluginName;
            string savePath = Path.Combine(Global.LocalPluginPath, pluginName);
            try
            {
                downloader.PluginsDownload(selectedDll, savePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           

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
        }

        private void TryLoadOne(string ffp)
        {    
            IPlugin dll = LoadDll(ffp);

            string name = dll.GetPluginName();

            if (!String.IsNullOrEmpty(name)) 
                plugins[name] = dll;             // 存在相同名称插件时，覆盖或者取先，逻辑意义上都一样，前者代码简单，用之

            return;
        }

        private IPlugin LoadDll(string ffp)
        {
            if (!File.Exists(ffp))
                return DLLPlugin.Empty;

            try 
            {
                Assembly assembly = Assembly.LoadFrom(ffp);
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
