using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.IAOLab
{
    public partial class FraudDialog : Form
    {
        public FraudDialog()
        {
            InitializeComponent();
        }

        private void FraudDialog_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(GetChromePath()))
            {
                string chromePath = GetChromePath();
                System.Diagnostics.Process.Start(chromePath, "http://103.43.17.9:8080/view/index.html");
                this.Close();
            }
            else
            {
                this.webBrowser1.Url = new System.Uri("http://103.43.17.9:8080/view/index.html", System.UriKind.Absolute);
            }
        }

        public string GetChromePath()
        {
            RegistryKey regKey = Registry.ClassesRoot;
            string path = string.Empty;
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
                path = Registry.GetValue(@"HKEY_CLASSES_ROOT\" + chromeKey + @"\shell\open\command", null, null) as string;
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
