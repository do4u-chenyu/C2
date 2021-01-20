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

        private bool IsReady()
        {
            // 是否配置完毕
            bool isEmpty = string.IsNullOrEmpty(this.inputPath.Text) ||
                           string.IsNullOrEmpty(this.jdkPath.Text);
            if (isEmpty)
            {
                MessageBox.Show("请完善apk存放路径、jdk存放路径。");
                return false;
            }
            if (!Directory.Exists(this.inputPath.Text)
                &&!File.Exists(this.inputPath.Text))
            {
                MessageBox.Show("不合法的apk存放路径");
                return false;
            }
            if (!Directory.Exists(this.jdkPath.Text))
            {
                MessageBox.Show("不合法的jdk存放路径");
                return false;
            }
            return true;
        }
        private void Analyse_Click(object sender, EventArgs e)
        {

            if (!IsReady())
                return;

            List<List<string>> apkInfoList = ApkToolStart.GetInstance().ExtractApk(inputPath.Text, jdkPath.Text);
            foreach (List<string> apkInfo in apkInfoList)
            {
                if (apkInfo.Count < 5) continue;

                // 将结果展示在窗体
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells[0].Value = GetImage(apkInfo[0]);
                for (int i = 1; i < this.dataGridView1.Rows.Count; i++)
                    this.dataGridView1.Rows[index].Cells[i].Value = GetImage(apkInfo[i]);

            }

        }
        public Image GetImage(string path)
        {
            try 
            {
                Image image = Image.FromFile(path);
                return image;
            }
            catch
            {
                return null;
            }
            
        }

       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileFolder(inputPath);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileFolder(jdkPath);
        }
        private void OpenFileFolder(TextBox control)
        {
            FolderBrowserDialog fd2 = new FolderBrowserDialog();
            if (fd2.ShowDialog() == DialogResult.OK)
                control.Text = fd2.SelectedPath;
        }
    }
}
