using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace C2.IAOLab.Plugins
{
    public class PluginsDownloader
    {
        private static string webPath;
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

                htmlContent = new StreamReader(resStream, System.Text.Encoding.UTF8).ReadToEnd();

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


        public List<string> WebPluginInfo(List<string> webPlugins, string packageURL)
        {
            List<string> result = new List<string>();
            Stream resStream = null;
            try
            {
                foreach (string pluginName in webPlugins)
                {
                    resStream = WebRequest.Create(packageURL + pluginName)
                                                 .GetResponse()
                                                 .GetResponseStream();
                    string info = new StreamReader(resStream, System.Text.Encoding.UTF8).ReadToEnd();
                    result.Add(info);
                }
            }
            catch
            { }
            finally
            {
                if (resStream != null)
                    resStream.Close();
            }
            return result;
        }
        /// <summary>
        /// 异常：
        /// <para>DownloadFailureException</para>
        /// </summary>
        public void PluginsDownload(string url, string savePath)
        {
            try
            {
                WebClient Client = new WebClient();
                if (File.Exists(savePath))
                    return;
                Client.DownloadFile(url, savePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}
