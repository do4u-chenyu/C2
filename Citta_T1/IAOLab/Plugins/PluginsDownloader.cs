using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.IAOLab.Plugins
{
    public class PluginsDownloader
    {
        private static string webPath; 
        public delegate void DownloadEventHandler();
        public  event DownloadEventHandler DownloadEvent;
        private static  PluginsDownloader downloader;
        public static PluginsDownloader Instance
        {
            get
            {
                if (downloader == null)
                    downloader = new PluginsDownloader();
                return downloader;
            }
        }

        private  void PluginsDownload()
        {
            if (DownloadReady())
            {
                // manager刷新
                // left button刷新
                DownloadEvent?.Invoke();
            }
            else
            { 

            }
        }
        private static  bool  DownloadReady()
        {
            try
            { 

            }
            catch
            {
                return false;
            }
            return true;
        }


    }
}
