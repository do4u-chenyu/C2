using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace C2.IAOLab.Plugins
{
    public class PluginsDownloader
    {
        private readonly static LogUtil log = LogUtil.GetInstance("PluginsDownloader");
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
            catch (Exception ex)
            {
                log.Error("获取网页插件列表失败:" + ex.Message);
            }
            finally
            {
                if (resStream != null)
                    resStream.Close();
            }
            return htmlContent;
        }

        public string GetPluginsInfo(string pluginsName, string packageURL)
        {
            List<string> tmp = new List<string>() { pluginsName };
            tmp = GetPluginsInfoList(tmp, packageURL);
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


        public void PluginsDownload(string url, string savePath)
        {
            if (File.Exists(savePath))
                return;

            new WebClient().DownloadFile(url, savePath);
        }
    }
}
