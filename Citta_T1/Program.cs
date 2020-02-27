using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1
{
    static class Program
    {
        public static Dictionary<string, Citta_T1.Data> inputDataDict = new Dictionary<string, Citta_T1.Data>();
        public static Dictionary<string, string> inputDataDictN2I = new Dictionary<string, string>();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
