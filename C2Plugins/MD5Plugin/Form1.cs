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
            throw new NotImplementedException();
        }

        public Image GetPluginImage()
        {
            throw new NotImplementedException();
        }

        public string GetPluginName()
        {
            throw new NotImplementedException();
        }

        public string GetPluginVersion()
        {
            throw new NotImplementedException();
        }

        public DialogResult ShowFormDialog()
        {
            throw new NotImplementedException();
        }
    }
}
