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
        public DetailsPageForm(string content, string filename)
        {
            InitializeComponent();
            this.Content = content;
            this.fileName = filename;
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

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "ext files (*.txt)|*.txt|All files(*.*)|*>**";
            saveFileDialog1.FileName = fileName;
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.OK && saveFileDialog1.FileName.Length > 0)
            {
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                MessageBox.Show("下载文件成功！", "保存文件");
            } 
        }
    }
}