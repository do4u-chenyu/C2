using C2.Dialogs;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.IAOLab.Plugins
{
    public class PluginsDownloader
    {
        private UpdateProgressBar progressBar;
        public PluginsDownloader()
        {
            progressBar = new UpdateProgressBar()
            {
                StartPosition = FormStartPosition.CenterScreen
            };
        }
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

        public string GetPluginsInfo(string pluginName, string packageURL)
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
        ///<summary>
        ///异常:
        ///<para>WedDownloadException</para>
        ///</summary>
        public void PluginsDownload(string url, string savePath)
        {
            if (File.Exists(savePath))
                return;
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            new WebClient().DownloadFile(url, savePath);
        }
        #region 更新包下载
        ///<summary>
        ///异常:
        ///<para>WedDownloadException</para>
        ///</summary>
        public void SoftwareDownload(string url, string savePath)
        {
                 
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            this.progressBar.Show();
            WebClient client = new WebClient();//实例化webclient
            client.DownloadFileCompleted += Client_DownloadFileCompleted;//下载完文件触发此事件
            client.DownloadProgressChanged += Client_DownloadProgressChanged;//下载进度变化触发事件
            client.DownloadFileAsync(new Uri(url), savePath);

        }

        //下载进度变化触发事件
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

            this.progressBar.MinimumValue = 0;//进度条最小值
            this.progressBar.MaximumValue = (int)e.TotalBytesToReceive;//下载文件的总大小
            this.progressBar.CurrentValue = (int)e.BytesReceived;//已经下载的大小
            this.progressBar.ProgressPercentage = e.ProgressPercentage + "%";//更新界面展示

        }
        //下载完文件触发此事件
        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
        
            if (e.UserState == null)
            {
                this.progressBar.Status = "下载完成,请重启软件实现更新";
            }
        }

        #endregion
    }
}
