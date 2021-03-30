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

namespace FullTextGrammarAssistant
{
    public partial class 全文语法查询助手 : Form, IPlugin
    {
        public 全文语法查询助手()
        {
            InitializeComponent();
        }

        public string GetPluginDescription()
        {
            return "全文语法助手";
        }

        public Image GetPluginImage()
        {
            return this.Icon.ToBitmap();
        }

        public string GetPluginName()
        {
            return "全文语法助手";
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

        }
    }
}
