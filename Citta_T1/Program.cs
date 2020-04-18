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
           // Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //System.Console.WriteLine(config.AppSettings.Settings["workspace"].Value);
            Global.WorkspaceDirectory = Path.Combine(Directory.GetCurrentDirectory(), "cittaModelDocument");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
