using System.Diagnostics;

namespace C2.Utils
{
    class ProcessUtil
    {
        public static void ProcessOpen(string ffp)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "explorer.exe";  //资源管理器
                processStartInfo.Arguments = ffp;
                Process.Start(processStartInfo);
            } catch { }

        }
    }
}
