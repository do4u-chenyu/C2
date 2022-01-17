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
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace C2
{
    static class Program
    {
        public const long OPEN_FILES_MESSAGE = 0x0999;
        [DllImport("shell32.dll")]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            if (string.Compare(DateTime.Now.ToString("yyyyMMddHHmmss"), "2022021700000000") > 0)
            {
                MessageBox.Show("产品可用时间截止到2022年2月17号");
                return;
            }

            if (PreProcessApplicationArgs(args))
                return;

            Application.SetCompatibleTextRenderingDefault(false);
            Options.Current.OpitonsChanged += Current_OpitonsChanged;
            Options.Current.Load(args);

            
            UIColorThemeManage.Initialize();
            LanguageManage.Initialize();
            RecentFilesManage.Default.Initialize();
            Current_OpitonsChanged(null, EventArgs.Empty);
            ConfigProgram();
            Application.EnableVisualStyles();//窗体启动前调用

            string ffp = args.Length == 0 ? string.Empty : args[0];
            Process instance = RunningC2Instance();
            SHChangeNotify(0x8000000, 0, IntPtr.Zero, IntPtr.Zero);

            if (instance == null)
                RunNewInstance(ffp);

            if (instance != null)
                NotifyInstance(ffp, instance);

            Options.Current.Save();
        }

        private static void ConfigProgram()
        {
            string workspaceDirectory = ConfigUtil.TryGetAppSettingsByKey("workspace", ConfigUtil.DefaultWorkspaceDirectory);

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

        public static void RunNewInstance(string ffp)
        {
            Application.Run(new MainForm(ffp));
        }

        private static Process RunningC2Instance()
        {
            Process curr = Process.GetCurrentProcess();
            Process[] all = Process.GetProcessesByName(curr.ProcessName);

            foreach (Process proc in all)
            {
                if (proc.Id == curr.Id)   
                    continue;
                // 有窗体
                if (proc.MainWindowHandle == IntPtr.Zero)
                    continue;
                return proc;
            }

            return null;
        }
        private static void NotifyInstance(string ffp, Process instance)
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
            User32.SendMessage(instance.MainWindowHandle, WinMessages.WM_COPYDATA, IntPtr.Zero, cbs_buffer);
        }

        static bool PreProcessApplicationArgs(string[] args)
        {
            return AssociationHelper.PreProcessApplicationArgs(args);
        }

        static void Current_OpitonsChanged(object sender, EventArgs e)
        {
            UITheme.Default.Colors = UIColorThemeManage.GetNamedTheme(CommonOptions.Appearances.UIThemeName);
            LanguageManage.ChangeLanguage(CommonOptions.Localization.LanguageID);
        }
    }
}
