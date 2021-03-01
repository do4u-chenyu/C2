using System;
using System.Windows.Forms;

namespace RookieKnowledgePlugin
{
    static class Program
    {
        [STAThread]
        static void Main(params string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
