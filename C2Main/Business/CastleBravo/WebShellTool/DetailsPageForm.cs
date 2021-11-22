using C2.Controls;
using Rebex.Net.Servers.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public partial class DetailsPageForm : Form
    {
        public string Content;
        public string fileName;
        public byte[] Download;
    
        public DetailsPageForm(string content,byte[] download, string filename)
        {
            InitializeComponent();
            this.Content = content;
            this.fileName = filename;
            this.Download = download;
            this.comboBox1.SelectedIndex = 0;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.comboBox1.SelectedItem.ToString() == "GB2312") 
                this.richTextBox1.Text = Content;
           
            else
            {
                byte[] getBt = Encoding.GetEncoding("GB2312").GetBytes(Content);
                this.richTextBox1.Text = Encoding.GetEncoding("utf-8").GetString(getBt);
            }
        }

        /*
        private void button1_Click(object sender, EventArgs e)
        {
           byte[] preDownload = Download.Skip(3).ToArray();
           byte[] endDownload = new byte[preDownload.Length-3];
            int count = 0;
            for (int i = 0; i < preDownload.Length-3; i++)
            {
                endDownload[count] = preDownload[i];
                count++;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "ext files (*.txt)|*.txt|All files(*.*)|*>**",
                FileName = fileName
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(endDownload, 0, endDownload.Length);
                        fs.Flush();
                    }
                    MessageBox.Show("下载文件成功！", "保存文件");
                }
                catch 
                {
                    MessageBox.Show("导出数据发生异常");
                }
            } 
        }
        */
    }
}