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
            List<string> apkInfoList = ApkToolStart.GetInstance().ExtractApk(textBox1.Text, textBox2.Text);
            foreach (string apkInfo in apkInfoList)
            {
                DataGridViewImageColumn ic = new DataGridViewImageColumn();
                this.dataGridView1.Columns.Add(ic);//增加列，用于显示图片
                ic.ImageLayout = DataGridViewImageCellLayout.Zoom;
                string[] fullApkInfo = apkInfo.Split('\t');
                dataGridView1.Columns[0].HeaderCell.Value = "ICON";
                dataGridView1.Columns.Add("1", "文件名");
                dataGridView1.Columns.Add("2", "Apk名");
                dataGridView1.Columns.Add("3", "包名");
                dataGridView1.Columns.Add("4", "主函数");
                dataGridView1.Columns.Add("5", "大小");
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells[0].Value = GetImage(fullApkInfo[0]);
                this.dataGridView1.Rows[index].Cells[1].Value = fullApkInfo[1];
                this.dataGridView1.Rows[index].Cells[2].Value = fullApkInfo[2];
                this.dataGridView1.Rows[index].Cells[3].Value = fullApkInfo[3];
                this.dataGridView1.Rows[index].Cells[4].Value = fullApkInfo[4];
                this.dataGridView1.Rows[index].Cells[5].Value = fullApkInfo[5]+"m";
                this.dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);//自动调整列宽
                this.dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);//自动调整行宽
            }

        }
        public Image GetImage(string path)
        {
            return Image.FromFile(path);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
