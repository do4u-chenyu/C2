using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Dialogs
{
    // 
    public delegate void delegateInputData(string name, string filePath, bool isutf8);
    public partial class FormInputData : Form
    {
        private bool m_isUTF8 = false;
        private string m_filePath;
        private int m_maxNumOfRow = 100;
        private System.Drawing.Font bold_font = new System.Drawing.Font("微软雅黑", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        private System.Drawing.Font font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        private bool textboxHasText = false;
        // 数据的存储形式，采用dict
        private Dictionary<string, string> contents = new Dictionary<string, string>();
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
            /*
             * 数据预览
             */
            string fileName="";
            OpenFileDialog fd = new OpenFileDialog();           
            fd.Filter = "files|*.txt";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                m_filePath = fd.FileName;     
                fileName = Path.GetFileNameWithoutExtension(@m_filePath);
                OverViewFile();              
            }
            if (fileName != "" && fileName == "请输入数据名称")
                this.textBox1.Text = fileName;

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

        // 添加按钮
        public event delegateInputData InputDataEvent;
        private void button2_Click(object sender, EventArgs e)
        {
            string name = this.textBox1.Text;
            if (this.textBox1.Text == "请输入数据名称")
            {
                MessageBox.Show("请输入数据名称！");
            }
            else if (m_filePath == null)
            {
                MessageBox.Show("请选择数据路径！");
            }
            else
            {
                PreLoadFile(m_filePath, this.m_isUTF8);
                InputDataEvent(name, m_filePath, this.m_isUTF8);
                DvgClean();
                Close();
                //if (this.m_isUTF8)
                //{
                //    content = File.ReadAllText(m_fileName, Encoding.UTF8);
                //}
                //else
                //{
                //    content = File.ReadAllText(m_fileName, Encoding.Default);
                //}

            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 关闭按钮
            DvgClean();
            Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.label4.Font = bold_font;
            this.label5.Font = font;
            this.m_isUTF8 = false;
            OverViewFile();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.label4.Font = font;
            this.label5.Font = bold_font;
            this.m_isUTF8 = true;
            OverViewFile();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void OverViewFile()
        {
            /*
             * @param this.isUTF8
             * @param this.fileName
             * 预览文件
             */
            System.IO.StreamReader sr;
            if (this.m_isUTF8)
            {
                sr = File.OpenText(m_filePath);
            }
            else
            {
                FileStream fs = new FileStream(m_filePath, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, System.Text.Encoding.Default);
            }
            String header = sr.ReadLine();
            String[] headers = header.Split('\t');
            int numOfCol = header.Split('\t').Length;
            System.Windows.Forms.DataGridViewTextBoxColumn[] ColumnList = new System.Windows.Forms.DataGridViewTextBoxColumn[numOfCol];
            try
            {
                // 初始化表头
                for (int i = 0; i < numOfCol; i++)
                {
                    ColumnList[i] = new System.Windows.Forms.DataGridViewTextBoxColumn();
                    ColumnList[i].HeaderText = headers[i];
                    ColumnList[i].Name = "Col " + i.ToString();
                }
                // 预览表格清理
                DvgClean(false);
                this.dataGridView1.Columns.AddRange(ColumnList);
                // 写入数据
                for (int row = 0; row < m_maxNumOfRow; row++)
                {
                    // 读取数据
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

            catch
            {;
                Console.WriteLine("FromInputData.OverViewFile occurs error! ");
            }
        }

        public void PreLoadFile(string filePath, bool isUTF8)
        {
            System.IO.StreamReader sr;
            string contents = "";
            if (isUTF8)
            {
                sr = File.OpenText(filePath);
            }
            else
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, System.Text.Encoding.Default);
            }
            for (int row = 0; row < m_maxNumOfRow; row++)
            {
                // 读取数据
                String line = sr.ReadLine();
                contents += (line + "\n");
            }
            Program.DataPreviewDict[filePath] = contents;
        }
        public void DvgClean(bool isClearDataName = true)
        {
            if (isClearDataName) { this.textBox1.Text = null; }
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Columns.Clear();
        }

        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>加密后字符串</returns>
        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

    }
}
