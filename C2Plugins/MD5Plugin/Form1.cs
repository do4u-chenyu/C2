using C2.IAOLab.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MD5Plugin
{
    public partial class Form1 : Form, IPlugin
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string GetPluginDescription()
        {
            return "将字符串进行常用的加密、解密、编码和解码操作；如MD5加密，Base64，Url编码和解码，UTF8和GBK转码等。";
        }

        public Image GetPluginImage()
        {
            return this.Icon.ToBitmap();
        }

        public string GetPluginName()
        {
            return "MD5加密";
        }

        public string GetPluginVersion()
        {
            return "0.0.1";
        }

        public DialogResult ShowFormDialog()
        {
            return this.ShowDialog();
        }
    }
}
