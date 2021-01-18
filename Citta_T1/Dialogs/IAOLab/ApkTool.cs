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
            List<string> apkInfoList = ApkToolStart.GetInstance().ExactApk(textBox1.Text, textBox2.Text);
            foreach (string apkInfo in apkInfoList)
            {
                string[] fullApkInfo = apkInfo.Split('\t');
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells[0].Value = GetImage(fullApkInfo[0]);
                this.dataGridView1.Rows[index].Cells[1].Value = fullApkInfo[1];
                this.dataGridView1.Rows[index].Cells[2].Value = fullApkInfo[2];
                this.dataGridView1.Rows[index].Cells[3].Value = fullApkInfo[3];
                this.dataGridView1.Rows[index].Cells[4].Value = fullApkInfo[4];
                this.dataGridView1.Rows[index].Cells[5].Value = fullApkInfo[5];
            }

        }
        private Image GetImage(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            Image result = Image.FromStream(fs);
            fs.Close();
            return result;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
