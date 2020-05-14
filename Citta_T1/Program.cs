using System;
using System.IO;
using System.Configuration;
using System.Windows.Forms;

using Citta_T1.Utils;
using Citta_T1.Dialogs;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Citta_T1
{
    static class Program
    {
        //全局类  
        public class DesignerModelClass
        {
            //私有构造器，防止实例化  
            private DesignerModelClass() { }
            //用于标识运行时/设计时的bool型静态成员，初始值设为false  
            public static bool IsDesignerMode = true;
        }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            DesignerModelClass.IsDesignerMode = false;

            ConfigProgram();
            Application.EnableVisualStyles();
            Process instance = RunningInstance();
            if (instance == null)
            {
                //1.1 没有实例在运行

                Application.Run(new LoginForm());
                Application.EnableVisualStyles();

            }
            else
            {
                //1.2 已经有一个实例在运行
                HandleRunningInstance(instance);
            }

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
            if (!System.IO.Directory.Exists(root))
                workspaceDirectory = Path.Combine(Directory.GetCurrentDirectory(), "FiberHomeIAOModelDocument");

            Global.WorkspaceDirectory = workspaceDirectory;
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
        private static extern bool ShowWindowAsync(System.IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(System.IntPtr hWnd);
        #endregion
    }
}
