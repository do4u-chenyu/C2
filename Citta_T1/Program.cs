using System;
using System.IO;
using System.Configuration;
using System.Windows.Forms;

using Citta_T1.Utils;
using Citta_T1.Dialogs;

namespace Citta_T1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConfigProgram();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }

        private static void ConfigProgram()
        {
           
            string workspaceDirectory = ConfigUtil.TryGetAppSettingsByKey("workspace", ConfigUtil.DefaultWorkspaceDirectory);
            if (String.IsNullOrEmpty(workspaceDirectory))
                workspaceDirectory = ConfigUtil.DefaultWorkspaceDirectory;
            string root = FileUtil.TryGetPathRoot(workspaceDirectory);
                // 如果硬盘不存在,用程序所在目录
            if (!System.IO.Directory.Exists(root))
                workspaceDirectory = Path.Combine(Directory.GetCurrentDirectory(), "cittaModelDocument");

            Global.WorkspaceDirectory = workspaceDirectory;
        }
    }
}
