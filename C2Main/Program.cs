using C2.Configuration;
using C2.Controls;
using C2.Controls.OS;
using C2.Core;
using C2.Core.Win32Apis;
using C2.Globalization;
using C2.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace C2
{
    static class Program
    {
        public const long OPEN_FILES_MESSAGE = 0x0999;
        public const string DateTimeFormat = "yyyy年MM月dd号";
        public const string LinceseDeadLine = "2022年07月24号";

        public static string LinceseDeadLineDesc()
        {
            return string.Format("本次迭代装备使用期截止到 {0}", LinceseDeadLine);
        }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            if (MeetDeadline())
                return;

            string ffp = args.Length == 0 ? string.Empty : args[0];
            IntPtr handle = RunningC2MainFormHandle();
            // 已存在C2进程,通知它加载文件或显现
            if (handle != IntPtr.Zero)
            {
                NotifyInstance(ffp, handle);
                return;
            }
            Shell32.SHChangeNotify(0x8000000, 0, IntPtr.Zero, IntPtr.Zero);//关联文件自动刷新c2默认图标
            //窗体启动前调用 
            Application.EnableVisualStyles();   
            Application.SetCompatibleTextRenderingDefault(false);
            Options.Current.OpitonsChanged += Current_OpitonsChanged;
            Options.Current.Load(args);
       
            UIColorThemeManage.Initialize();
            LanguageManage.Initialize();
            RecentFilesManage.Default.Initialize();
            Current_OpitonsChanged(null, EventArgs.Empty);
            ConfigProgram();
            Application.Run(new MainForm(ffp));
            Options.Current.Save();
        }

        private static bool MeetDeadline()
        {
            DateTime deadline = ConvertUtil.TryParseDateTime(LinceseDeadLine, DateTimeFormat);
            DateTime now = DateTime.Now;

            if (now > deadline)
            {
                MessageBox.Show(LinceseDeadLineDesc());
                return true;
            }
            return false;
        }

        private static void ConfigProgram()
        {
            string workspaceDirectory = ConfigUtil.TryGetAppSettingsByKey("workspace", Global.WorkspaceDirectory);

            string root = FileUtil.TryGetPathRoot(workspaceDirectory);
            // 如果硬盘不存在,用程序所在目录
            if (!Directory.Exists(root))
                workspaceDirectory = Path.Combine(Directory.GetCurrentDirectory(), "FiberHomeIAOModelDocument");

            Global.WorkspaceDirectory = workspaceDirectory;

            //设置临时文件夹路径，默认操作系统临时文件夹路径。如果默认路径有访问权限，用程序的工作目录临时文件夹。
            string tempDir = FileUtil.TryGetSysTempDir();
            if (!string.IsNullOrEmpty(tempDir))
                Global.TempDirectory = Path.Combine(tempDir, "FiberHomeIAOTemp");
            else
                Global.TempDirectory = Path.Combine(Global.WorkspaceDirectory, "FiberHomeIAOTemp");
        }

        private static IntPtr RunningC2MainFormHandle()
        {
            Process curr = Process.GetCurrentProcess();
            Process[] all = Process.GetProcessesByName(curr.ProcessName);
            IntPtr handle = IntPtr.Zero;

            foreach (Process proc in all)
            {
                if (proc.Id == curr.Id)   
                    continue;
                
                if (proc.MainWindowHandle == IntPtr.Zero)
                    continue;
                // 有窗体
                handle = proc.MainWindowHandle;
                // 找到的窗体不对时
                if (proc.MainWindowTitle != Global.GetMainWindowTitle())  
                    handle = User32.FindWindow(null, Global.GetMainWindowTitle());
                
                return handle == IntPtr.Zero ? proc.MainWindowHandle : handle;
            }
            return handle;
        }
        private static void NotifyInstance(string ffp, IntPtr instance)
        {
            var data = Encoding.UTF8.GetBytes(ffp);
            var buffer = OSHelper.IntPtrAlloc(data);

            var cds = new COPYDATASTRUCT
            {
                dwData = new IntPtr(OPEN_FILES_MESSAGE),
                cbData = data.Length,
                lpData = buffer
            };
            var cbs_buffer = OSHelper.IntPtrAlloc(cds);
            User32.SendMessage(instance, WinMessages.WM_COPYDATA, IntPtr.Zero, cbs_buffer);
        }

        static void Current_OpitonsChanged(object sender, EventArgs e)
        {
            UITheme.Default.Colors = UIColorThemeManage.GetNamedTheme(CommonOptions.Appearances.UIThemeName);
            LanguageManage.ChangeLanguage(CommonOptions.Localization.LanguageID);
        }
    }
}
