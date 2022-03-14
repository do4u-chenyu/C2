using C2.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Dialogs.IAOLab
{
    public partial class BigAPKForm : Form
    {
        public BigAPKForm()
        {
            InitializeComponent();
            IntPtr Hicon = global::C2.Properties.Resources.BigAPK.GetHicon();
            this.Icon = Icon.FromHandle(Hicon);
        }
        
        private void BigApkForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ProcessUtil.GetChromePath()))
            {
                string chromePath = ProcessUtil.GetChromePath();
                System.Diagnostics.Process.Start(chromePath, "http://113.31.110.244:6663/ns/APPtest/home");
                this.Close();
            }
            else
            {
                this.webBrowser1.Url = new System.Uri("http://113.31.110.244:6663/ns/APPtest/home", System.UriKind.Absolute);
            }
        }
    }
}
