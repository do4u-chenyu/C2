using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            if (!string.IsNullOrEmpty(GetChromePath()))
            {
                string chromePath = GetChromePath();
                System.Diagnostics.Process.Start(chromePath, "http://113.31.110.244:6663/ns/APPtest/home");
                this.Close();
            }
            else
            {
                this.webBrowser1.Url = new System.Uri("http://113.31.110.244:6663/ns/APPtest/home", System.UriKind.Absolute);
            }
        }

        public string GetChromePath()
        {
            RegistryKey regKey = Registry.ClassesRoot;
            List<string> chromeKeyList = new List<string>();
            foreach (var chrome in regKey.GetSubKeyNames())
            {
                if (chrome.ToUpper().Contains("CHROMEHTML"))
                {
                    chromeKeyList.Add(chrome);
                }
            }
            foreach (string chromeKey in chromeKeyList)
            {
                string path = Registry.GetValue(@"HKEY_CLASSES_ROOT\" + chromeKey + @"\shell\open\command", null, null) as string;
                if (path != null)
                {
                    var split = path.Split('\"');
                    path = split.Length >= 2 ? split[1] : null;
                    if (File.Exists(path))
                        return path;
                }
            }
            return string.Empty;
        }
    }
}
