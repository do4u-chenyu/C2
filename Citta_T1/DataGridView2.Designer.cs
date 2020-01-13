namespace Citta_T1
{
    partial class DataGridView2
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
            this.ucDataGridView2 = new HZH_Controls.Controls.UCDataGridView();
            this.SuspendLayout();
            // 
            // ucDataGridView1
            // 
            this.ucDataGridView2.AutoScroll = true;
            this.ucDataGridView2.BackColor = System.Drawing.Color.White;
            this.ucDataGridView2.Columns = null;
            this.ucDataGridView2.DataSource = null;
            this.ucDataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataGridView2.HeadFont = new System.Drawing.Font("微软雅黑", 12F);
            this.ucDataGridView2.HeadHeight = 40;
            this.ucDataGridView2.HeadPadingLeft = 0;
            this.ucDataGridView2.HeadTextColor = System.Drawing.Color.Black;
            this.ucDataGridView2.IsShowCheckBox = false;
            this.ucDataGridView2.IsShowHead = true;
            this.ucDataGridView2.Location = new System.Drawing.Point(0, 0);
            this.ucDataGridView2.Name = "ucDataGridView1";
            this.ucDataGridView2.RowHeight = 40;
            this.ucDataGridView2.RowType = typeof(HZH_Controls.Controls.UCDataGridViewRow);
            this.ucDataGridView2.Size = new System.Drawing.Size(1011, 137);
            this.ucDataGridView2.TabIndex = 0;
            this.ucDataGridView2.Load += new System.EventHandler(this.ucDataGridView2_Load);
            // 
            // DataGridView2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucDataGridView2);
            this.Name = "DataGridView2";
            this.Size = new System.Drawing.Size(1011, 137);
            this.ResumeLayout(false);

        }

        #endregion

        private HZH_Controls.Controls.UCDataGridView ucDataGridView2;
    }
}
