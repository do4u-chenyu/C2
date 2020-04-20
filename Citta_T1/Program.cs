using System;
using System.IO;
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
            Global.WorkspaceDirectory = Path.Combine(Directory.GetCurrentDirectory(), "cittaModelDocument");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
