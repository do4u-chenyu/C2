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
        /// <summary>
        /// 异常：
        /// <para>WebRequestFailureException</para>
        /// </summary>
        public static string GetHtmlContent(string pluginUrl)
        {
            string htmlContent = string.Empty;
            try 
            {
                Stream resStream = WebRequest.Create(pluginUrl)
                                             .GetResponse()
                                             .GetResponseStream();

                StreamReader sr = new StreamReader(resStream, System.Text.Encoding.UTF8);
                htmlContent = sr.ReadToEnd();
                resStream.Close();
                sr.Close();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            return htmlContent;
        }

        public static List<string> WebPluginList(string webcontent)
        {
            List<string> result = new List<string>();
            if (string.IsNullOrEmpty(webcontent)) return result;
            string dllForm = string.Format(@"\>.*dll\<");
            try
            {
                MatchCollection matchItems = Regex.Matches(webcontent, dllForm, RegexOptions.IgnoreCase);
                foreach (Match match in matchItems)
                {
                    string pluginName = match.Value.Trim(new char[] { '>', '<' });
                    result.Add(pluginName);
                }
            }
            catch
            { }
            return result;
        }
        /// <summary>
        /// 异常：
        /// <para>DownloadFailureException</para>
        /// </summary>
        public static void PluginsDownload(string url, string savePath)
        {
            try
            {
                WebClient Client = new WebClient();
                Client.DownloadFile(url, savePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }   


    }
}
