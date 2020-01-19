using System.Collections.Generic;
using System.Windows.Forms;
using Citta_T1;

namespace Citta_T1
{
    partial class DataGridView0
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1011, 137);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // DataGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "DataGridView";
            this.Size = new System.Drawing.Size(1011, 137);
            this.Load += new System.EventHandler(this.DataGridView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        private void InitializeDgv(string fileName="")
        {
            List<List<string>> datas;
            if (fileName == "")
            {
                datas  = this.OverViewFileFromResx(Properties.Resources.text_utf8);
            }
            else
            {
                datas = this.OverViewFileFromPath(fileName);
            }
            List<string> headers = datas[0];
            int numOfCols = headers.ToArray().Length;
            _InitializeColumns(headers);
            _InitializeRowse(datas.GetRange(1, datas.ToArray().Length - 1), numOfCols);

        }
        private void _InitializeColumns(List<string> headers)
        {
            /*
             * 初始化列
             */
            int numOfCols = headers.ToArray().Length;
            System.Windows.Forms.DataGridViewTextBoxColumn[] ColumnList = new System.Windows.Forms.DataGridViewTextBoxColumn[numOfCols];
            for (int i = 0; i < numOfCols; i++)
            {
                ColumnList[i] = new System.Windows.Forms.DataGridViewTextBoxColumn();
                ColumnList[i].HeaderText = headers[i];
                ColumnList[i].Name = "Col_" + i.ToString();
            }
            this.dataGridView1.Columns.AddRange(ColumnList);
        }

        private void _InitializeRowse(List<List<string>> datas, int numOfCols)
        {
            /*
             * 初始化行
             * TODO
             * 使用样例数据
             */
            string data;
            for (int i = 0; i < maxNumOfRows; i=this.dataGridView1.Rows.Add())
            {
                //this.dataGridView1.Rows.Add();
                for (int j = 0; j < numOfCols; j++)
                {
                    try
                    {
                        data = datas[i][j];
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        data = "";
                    }
                    this.dataGridView1.Rows[i].Cells[j].Value = data;
                }
            }
        }
        #endregion

        private List<List<string>> OverViewFileFromPath(string fileNameOrFile="", int maxNumOfFile = 50, char sep = '\t')
        {
            List<List<string>> datas = new List<List<string>> { }; 
            System.IO.StreamReader file = new System.IO.StreamReader(fileNameOrFile);
            int rowCounter = 0;
            string line;
            while (((line = file.ReadLine()) != null) && (rowCounter < maxNumOfFile))
            {
                List<string> eles = new List<string>(line.Split(sep));
                datas.Add(eles);
            }
            return datas;
        }
        private List<List<string>> OverViewFileFromResx(string resx = "", int maxNumOfFile = 50, char sep = '\t')
        {
            List<List<string>> datas = new List<List<string>> { };
            string[] contents = resx.Split('\n');
            int numOfRows = contents.Length;
            List<string> rows = new List<string>(contents);
            for (int i = 0; i < (numOfRows < maxNumOfFile ? numOfRows : maxNumOfRows); i++)
            {
                datas.Add(new List<string>(rows[i].Split('\t')));
            }
            return datas;
        }
        private System.Windows.Forms.DataGridView dataGridView1;
        private int maxNumOfRows = 20;
    }
}
