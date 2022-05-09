using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace C2.Utils
{
    class ProcessUtil
    {
        private static LogUtil log = LogUtil.GetInstance("MindMapView");
        public static string GetChromePath()
        {
            RegistryKey regKey = Registry.ClassesRoot;
            List<string> chromeKeyList = new List<string>();
            foreach (var chrome in regKey.GetSubKeyNames())
            {
                if (chrome.ToUpper().Contains("CHROMEHTML"))
                {
                    chromeKeyList.Add(chrome);
                }
            }
            foreach (string chromeKey in chromeKeyList)
            {
                string path = Registry.GetValue(@"HKEY_CLASSES_ROOT\" + chromeKey + @"\shell\open\command", null, null) as string;
                if (path != null)
                {
                    var split = path.Split('\"');
                    path = split.Length >= 2 ? split[1] : null;
                    if (File.Exists(path))
                        return path;
                }
            }
            return string.Empty;
        }

        public static void ProcessOpen(string ffp)
        {
            if (ffp.EndsWith(".bcp", StringComparison.CurrentCultureIgnoreCase))
                FileUtil.ExploreDirectory(ffp);
            else
                ProcessExplorer(ffp);
        }

        public static void TryProcessOpen(string ffp)
        {
            if (File.Exists(ffp))
                ProcessOpen(ffp);
        }
        
        public static void TryOpenDirectory(string ffp)
        {
            if (Directory.Exists(ffp))
                ProcessOpen(ffp);
        }

        private static void ProcessExplorer(string ffp)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "explorer.exe";  //资源管理器
                processStartInfo.Arguments = ffp;
                Process.Start(processStartInfo);
            }
            catch { }
        }
    }
}
