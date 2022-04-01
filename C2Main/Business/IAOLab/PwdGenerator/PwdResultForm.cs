using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace C2.Business.IAOLab.PwdGenerator
{
    public partial class PwdResultForm : Form
    {

        public string richTextBoxText { set { this.richTextBox1.Text = value; } }
        public string S { set { this.Show_label.Text = value; } }



        public PwdResultForm()
        {
            InitializeComponent();
        }

        int count = 0;
        private void ExportDatas()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "请选择要导出的位置";

            saveFile.Filter = "文本文件|*.txt";
            saveFile.FileName = "社工生成" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                string path = saveFile.FileName;
                string text = richTextBox1.Text;
                try
                {
                    using (StreamWriter file = new StreamWriter(path))
                    {
                        string[] lines = text.Split('\n');
                        foreach (string item in lines)
                        {
                            file.WriteLine(item);
                            count++;
                        }
                            

                        file.Close();
                        MessageBox.Show("数据导出成功","提示");
                    }
                }
                catch
                {
                    MessageBox.Show("导出失败");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExportDatas();
        }
 
    }
}
