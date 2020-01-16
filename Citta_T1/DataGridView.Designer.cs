using System.Windows.Forms;
using Citta_T1;

namespace Citta_T1
{
    partial class DataGridView
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
            this._InitializeColumns();
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
        private void _InitializeColumns()
        {
            System.Windows.Forms.DataGridViewTextBoxColumn[] ColumnList = new System.Windows.Forms.DataGridViewTextBoxColumn[numOfRows];
            for (int i = 0; i < this.numOfCols; i++)
            {
                ColumnList[i] = new System.Windows.Forms.DataGridViewTextBoxColumn();
                ColumnList[i].HeaderText = "Col" + i.ToString();
                ColumnList[i].Name = "列" + i.ToString();
            }
            this.dataGridView1.Columns.AddRange(ColumnList);
        }

        private void _InitializeRows()
        {
            for (int i = 0; i < this.numOfRows; i=this.dataGridView1.Rows.Add())
            {   
                //this.dataGridView1.Rows.Add();
                for (int j = 0; j < this.numOfCols; j++)
                {
                    this.dataGridView1.Rows[i].Cells[j].Value = i.ToString() + "_" + j.ToString();
                    //var cell = ((DataGridViewButtonCell)this.dataGridView1.Rows[i].Cells[j]);
                    //cell.FlatStyle = FlatStyle.Flat;
                    //if (i % 2 == 0)
                    //{
                    //    cell.Style.BackColor = System.Drawing.Color.Green;
                    //}
                }
            }
            // TODO change styel
            
        }
        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private int numOfCols = 20;
        private int numOfRows = 20;
    }
}
