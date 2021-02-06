using C2.IAOLab.Plugins;

using System.Drawing;
using System.Windows.Forms;

namespace TestPlugin
{
    public partial class Form1 : Form, IPlugin
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string GetPluginDescription()
        {
            return "适合新手的知识宝典，来自于老鸟们多年实战中的经验总结。";
        }

        public Image GetPluginImage()
        {
            return this.Icon.ToBitmap();
        }

        public string GetPluginName()
        {
            return "技能宝典";
        }

        public string GetPluginVersion()
        {
            return "0.0.3";
        }

        public DialogResult ShowFormDialog()
        {
            return this.ShowDialog();
        }
    }
}
