using C2.IAOLab.Plugins;
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

namespace KnowledgeBase
{
    public partial class Form1 : Form, IPlugin
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string GetPluginDescription()
        {
            return "知识库入口";
        }

        public Image GetPluginImage()
        {
            return this.Icon.ToBitmap();
        }

        public string GetPluginName()
        {
            return "知识库";
        }

        public string GetPluginVersion()
        {
            return "0.0.1";
        }

        public DialogResult ShowFormDialog()
        {
            return this.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string chromePath = GetChromePath();
            if (!string.IsNullOrEmpty(chromePath))
            {
                System.Diagnostics.Process.Start(chromePath, "15.73.3.241:19001/KnowledgeBase/");
            }
            else
                MessageBox.Show("未能找到chrome启动路径");
             
            this.Close();
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
            foreach(string chromeKey in chromeKeyList)
            {
                path = Registry.GetValue(@"HKEY_CLASSES_ROOT\" + chromeKey + @"\shell\open\command", null, null) as string;
                if (path != null)
                {
                    var split = path.Split('\"');
                    path = split.Length >= 2 ? split[1] : null;
                    if(File.Exists(path))
                        return path;
                }
            }
            return string.Empty;
        }
    }
}
