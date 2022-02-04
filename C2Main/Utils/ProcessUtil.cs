using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace C2.Utils
{
    class ProcessUtil
    {

        public static void ProcessOpen(string ffp)
        {
            if (ffp.EndsWith(".bcp", StringComparison.CurrentCultureIgnoreCase))
                FileUtil.ExploreDirectory(ffp);
            else
                ProcessExplorer(ffp);
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

        public static string ProcessStandErrorMessage(Process p)
        {
            try
            {
                return p.StandardError.ReadToEnd();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
