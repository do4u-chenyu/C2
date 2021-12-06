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
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace C2
{
    static class Program
    {
        public const long OPEN_FILES_MESSAGE = 0x0999;
        public const string Software_Version = "1.4.14";

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            if (string.Compare(DateTime.Now.ToString("yyyyMMddHHmmss"), "2021121700000000") > 0)
            {
                MessageBox.Show("产品可用时间截止到2021年12月17号");
                return;
            }
            #region
            if (PreProcessApplicationArgs(args))
                return;

            // 如果需要打开文件, 偿试寻找是否有已经存在的应用实例打开
            if (!args.IsNullOrEmpty() && TryOpenByOtherInstance(args))
                return;

            Application.SetCompatibleTextRenderingDefault(false);
            Options.Current.OpitonsChanged += Current_OpitonsChanged;
            Options.Current.Load(args);

            
            UIColorThemeManage.Initialize();
            LanguageManage.Initialize();
            RecentFilesManage.Default.Initialize();
          

            Current_OpitonsChanged(null, EventArgs.Empty);
            #endregion

            ConfigProgram();
            LanguageManage.Initialize();
            Application.EnableVisualStyles();//窗体启动前调用
            Process instance = RunningInstance();


            if (instance == null)
            {
                //1.1 没有实例在运行
                if (args.Length > 0)
                {
                    string path = args.JoinString(string.Empty).TrimEnd(OpUtil.Blank);
                    RunByVersion(path);
                }
                else
                    RunByVersionExtend(); 
            }
            else
            {
                //1.2 已经有一个实例在运行
                HandleRunningInstance(instance);
            }      
            Options.Current.Save();
        }

        private static void ConfigProgram()
        {
            // 不存在配置项,用默认值
            string workspaceDirectory = ConfigUtil.TryGetAppSettingsByKey("workspace", ConfigUtil.DefaultWorkspaceDirectory);
            // 存在workspace配置项,但配置项为空
            if (String.IsNullOrEmpty(workspaceDirectory))
                workspaceDirectory = ConfigUtil.DefaultWorkspaceDirectory;

            string root = FileUtil.TryGetPathRoot(workspaceDirectory);
            // 如果硬盘不存在,用程序所在目录
            if (!Directory.Exists(root))
                workspaceDirectory = Path.Combine(Directory.GetCurrentDirectory(), "FiberHomeIAOModelDocument");

            Global.WorkspaceDirectory = workspaceDirectory;
            Global.VersionType = ConfigUtil.TryGetAppSettingsByKey("RunLevel", ConfigUtil.DefaultVersionType);
            if (Global.VersionType.Equals(Global.GreenLevel))
                Global.WorkspaceDirectory = Path.Combine(Environment.CurrentDirectory, Global.GreenPath);

            //设置临时文件夹路径，默认操作系统临时文件夹路径。如果默认路径有访问权限，用程序的工作目录临时文件夹。
            string tempDir = FileUtil.TryGetSysTempDir();
            if (!string.IsNullOrEmpty(tempDir))
                Global.TempDirectory = Path.Combine(tempDir, "FiberHomeIAOTemp");
            else
                Global.TempDirectory = Path.Combine(Global.WorkspaceDirectory, "FiberHomeIAOTemp");
        }

        public static void RunByVersion(string path)
        {
            Global.SetUsername("IAO");
            Application.Run(new MainForm(Global.GetUsername(), path));
        }

        public static void RunByVersionExtend()
        {
            Global.SetUsername("IAO");
            Application.Run(new MainForm(Global.GetUsername()));
        }

        #region 确保程序只运行一个实例
        private static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //遍历与当前进程名称相同的进程列表 
            foreach (Process process in processes)
            {
                //如果实例已经存在则忽略当前进程 
                if (process.Id != current.Id)
                {
                    //保证要打开的进程同已经存在的进程来自同一文件路径
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        //返回已经存在的进程
                        return process;
                    }
                }
            }
            return null;
        }
        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, 1); //调用api函数，正常显示窗口
            SetForegroundWindow(instance.MainWindowHandle); //将窗口放置最前端
        }
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion
        static bool TryOpenByOtherInstance(string[] args)
        {
            var files = args.Where(arg => !arg.StartsWith("-")).ToArray();
            if (files.IsEmpty())
                return false;

            var name = Process.GetCurrentProcess().ProcessName;
            var otherInstances = Process.GetProcessesByName(name)
                .Where(inst => inst != Process.GetCurrentProcess() && inst.MainWindowHandle != IntPtr.Zero)
                .ToArray();
            if (!otherInstances.IsNullOrEmpty())
            {
                var inst = otherInstances.First();
                var data = Encoding.UTF8.GetBytes(files.JoinString(";"));
                var buffer = OSHelper.IntPtrAlloc(data);

                var cds = new COPYDATASTRUCT
                {
                    dwData = new IntPtr(OPEN_FILES_MESSAGE),
                    cbData = data.Length,
                    lpData = buffer
                };
                var cbs_buffer = OSHelper.IntPtrAlloc(cds);
                IntPtr result = User32.SendMessage(inst.MainWindowHandle, WinMessages.WM_COPYDATA, IntPtr.Zero, cbs_buffer);
                OSHelper.IntPtrFree(cbs_buffer);
                OSHelper.IntPtrFree(buffer);

                return result != IntPtr.Zero;
            }

            return false;
        }

        static bool PreProcessApplicationArgs(string[] args)
        {
            if (AssociationHelper.PreProcessApplicationArgs(args))
                return true;

            return false;
        }

        static void Current_OpitonsChanged(object sender, EventArgs e)
        {
            UITheme.Default.Colors = UIColorThemeManage.GetNamedTheme(CommonOptions.Appearances.UIThemeName);
            LanguageManage.ChangeLanguage(CommonOptions.Localization.LanguageID);
        }
    }
}
