using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Dialogs
{
    public partial class FormInputData : Form
    {
        private bool isUTF8 = false;
        private System.Drawing.Font bold_font = new System.Drawing.Font("微软雅黑", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        private System.Drawing.Font font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        private bool textboxHasText = false;
        public FormInputData()
        {
            InitializeComponent();
            this.textBox1.LostFocus += new EventHandler(this.textBox1_Leave);
            this.textBox1.GotFocus += new EventHandler(this.textBox1_Enter);
        }


        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textboxHasText == false)
            {
                this.textBox1.Text = "";
            }
            this.textBox1.ForeColor = Color.Black;
        }
        
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "")
            {
                this.textBox1.Text = "请输入数据名称";
                this.textBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                textboxHasText = false;
            }
            else
            {
                textboxHasText = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file1 = new OpenFileDialog();
            file1.Filter = "files|*.txt";
            if (file1.ShowDialog() == DialogResult.OK)
            {
                // 1.读取文件
                // 2.抽第一行，初始化列头
                // 3.余下行作为数据，展示在dgv中

                // TODO
                // 1.设置dgv中的字体
                // 2.设置dgv中的列宽
                // 3.关闭窗口后清除表格数据
                System.IO.StreamReader sr;
                if (this.isUTF8)
                {
                    sr = File.OpenText(file1.FileName);
                }
                else
                {
                    FileStream fs = new FileStream(file1.FileName, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(fs, System.Text.Encoding.Default);
                }
                String header = sr.ReadLine();
                String[] headers = header.Split('\t');
                int numOfCol = header.Split('\t').Length;
                int maxNumOfRow = 10;
                System.Windows.Forms.DataGridViewTextBoxColumn[] ColumnList = new System.Windows.Forms.DataGridViewTextBoxColumn[numOfCol];
                // 初始化表头
                for (int i = 0; i < numOfCol; i++)
                {
                    ColumnList[i] = new System.Windows.Forms.DataGridViewTextBoxColumn();
                    ColumnList[i].HeaderText = headers[i];
                    ColumnList[i].Name = "Col " + i.ToString();
                }
                // 预览表格清理
                this.dataGridView1.Rows.Clear();
                this.dataGridView1.Columns.Clear();
                this.dataGridView1.Columns.AddRange(ColumnList);
                // 写入数据
                for (int row = 0; row < maxNumOfRow; row++)
                {
                    String line = sr.ReadLine();
                    String[] eles = line.Split('\t');
                    System.Windows.Forms.DataGridViewRow dr = new System.Windows.Forms.DataGridViewRow();
                    this.dataGridView1.Rows.Add(dr);
                    for (int col = 0; col < numOfCol; col++)
                    {
                        this.dataGridView1.Rows[row].Cells[col].Value = eles[col];
                    }
                }

            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.label4.Font = bold_font;
            this.label5.Font = font;
            this.isUTF8 = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.label4.Font = font;
            this.label5.Font = bold_font;
            this.isUTF8 = true;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
