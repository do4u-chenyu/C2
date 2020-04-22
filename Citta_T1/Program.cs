using System;
using System.IO;
using System.Configuration;
using System.Windows.Forms;

using Citta_T1.Utils;

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
            string workspace = Path.Combine(Directory.GetCurrentDirectory(), "cittaModelDocument");
            try
            { 

                workspace = ConfigurationManager.AppSettings["workspace"];
                string root = Path.GetPathRoot(workspace);
                // 如果硬盘不存在,用程序所在目录
                if (!System.IO.Directory.Exists(root))
                    workspace = Path.Combine(Directory.GetCurrentDirectory(), "cittaModelDocument");
            }
            catch (ConfigurationErrorsException)
            {
                workspace = Path.Combine(Directory.GetCurrentDirectory(), "cittaModelDocument");
            }

            Global.WorkspaceDirectory = workspace;
        }
    }
}
