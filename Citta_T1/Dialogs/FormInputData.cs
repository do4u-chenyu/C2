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
        public FormInputData()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
                //1.读取文件
                //2.抽第一行，初始化列头
                //3.余下行作为数据，展示在dgv中

                //todo
                //1.设置dgv中的字体
                //2.设置dgv中的列宽
                System.IO.StreamReader sr = File.OpenText(file1.FileName);
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
    }
}
