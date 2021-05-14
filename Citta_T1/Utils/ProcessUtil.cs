using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace C2.Utils
{
    class ProcessUtil
    {
        private const uint SW_NORMAL = 1;

        [DllImport("shell32.dll", SetLastError = true)]
        extern private static bool ShellExecuteEx(ref ShellExecuteInfo lpExecInfo); 


        [Serializable]
        internal struct ShellExecuteInfo
        {
            public int Size;
            public uint Mask;
            public IntPtr hwnd;
            public string Verb;
            public string File;
            public string Parameters;
            public string Directory;
            public uint Show;
            public IntPtr InstApp;
            public IntPtr IDList;
            public string Class;
            public IntPtr hkeyClass;
            public uint HotKey;
            public IntPtr Icon;
            public IntPtr Monitor;
        }

        public static void ProcessOpen(string ffp)
        {
            if (ffp.EndsWith(".bcp", StringComparison.CurrentCultureIgnoreCase))
                ProcessOpenEx(ffp);
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



        private static void ProcessOpenEx(string ffp)
        {
            // win没有指定打开方式时,提示打开方式选择窗口
            // 没有默认打开方式时, ProcessOpen是不报错的，必须提前决定用哪个函数打开文件
            try
            {
                ShellExecuteInfo sei = new ShellExecuteInfo();
                sei.Size = Marshal.SizeOf(sei);
                sei.Verb = "openas";
                sei.File = ffp;
                sei.Show = SW_NORMAL;

                ShellExecuteEx(ref sei);
            } catch { }
        }
    }
}
