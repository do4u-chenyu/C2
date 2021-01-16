using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using C2.Controls;
using C2.IAOLab.ApkToolStart;


namespace C2.Dialogs.IAOLab
{
    public partial class ApkTool : BaseDialog
    {
        
        
        public delegate void UpdateLog(string log);//声明一个更新主线程日志的委托
        public UpdateLog UpdateLogDelegate;
        public ApkTool()
        {
            InitializeComponent();

        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            ApkToolStart.GetInstance().ExactApk(textBox1.Text,textBox2.Text);

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
