using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;

namespace C2.IAOLab.Plugins
{
    public class PluginsDownloader
    {
        public PluginsDownloader() { }

        public AsyncCompletedEventHandler Client_DownloadFileCompleted;
        public DownloadProgressChangedEventHandler Client_DownloadProgressChanged;
        public string GetHtmlContent(string pluginURL)
        {
            string htmlContent = string.Empty;
            Stream resStream = null;
            try
            {
                resStream = WebRequest.Create(pluginURL)
                                             .GetResponse()
                                             .GetResponseStream();
                resStream.ReadTimeout = 1000 * 10; // 浏览页面设置超时,后续下载不用,相隔很短,最开始做个验证即可
                htmlContent = new StreamReader(resStream, Encoding.UTF8).ReadToEnd();

            }
            catch { }
            finally
            {
                if (resStream != null)
                    resStream.Close();
            }
            return htmlContent;
        }

        public string GetPluginInfo(string pluginName, string packageURL)
        {
            List<string> tmp = GetPluginsInfoList(new List<string> { pluginName }, packageURL);
            return tmp.Count == 0 ? string.Empty : tmp[0];
        }
        public List<string> GetPluginsInfoList(List<string> pluginsName, string packageURL)
        {
            List<string> result = new List<string>();
            Stream resStream = null;
            
            foreach (string pluginName in pluginsName)
            {
                try
                {
                    resStream = WebRequest.Create(packageURL + pluginName)
                                               .GetResponse()
                                               .GetResponseStream();
                    string info = new StreamReader(resStream, Encoding.UTF8).ReadToEnd();
                    result.Add(info);
                }
                catch { }
                finally
                {
                    if (resStream != null)
                        resStream.Close();
                }

            }
            return result;
        }

        public void PluginDownload(string pluginUrl, string savePath)
        {
            if (File.Exists(savePath))
                return;
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            new WebClient().DownloadFile(pluginUrl, savePath);
        }

        #region 更新包下载

        public void SoftwareDownload(string url, string savePath)
        {
                 
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            WebClient client = new WebClient();//实例化webclient
            client.DownloadFileCompleted += Client_DownloadFileCompleted;//下载完文件触发此事件
            client.DownloadProgressChanged += Client_DownloadProgressChanged;//下载进度变化触发事件
            client.DownloadFileAsync(new Uri(url), savePath);

        }
        #endregion
    }
}
