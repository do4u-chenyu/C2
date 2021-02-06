using System;
using System.Windows.Forms;

namespace TestPlugin
{
    static class Program
    {
        [STAThread]
        static void Main(params string[] args)
        {
            Application.Run(new Form1());
        }
    }
}
