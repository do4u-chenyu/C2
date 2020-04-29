namespace Citta_T1.Controls.Left
{
    partial class FlowChartControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlowChartControl));
            this.leftPanelOpRelate = new Citta_T1.Controls.Common.NoFocusButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.leftPanelOpCollide = new Citta_T1.Controls.Common.NoFocusButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // leftPanelOpRelate
            // 
            this.leftPanelOpRelate.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.leftPanelOpRelate.FlatAppearance.BorderSize = 0;
            this.leftPanelOpRelate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.leftPanelOpRelate.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.leftPanelOpRelate.Location = new System.Drawing.Point(66, 18);
            this.leftPanelOpRelate.Name = "leftPanelOpRelate";
            this.leftPanelOpRelate.Size = new System.Drawing.Size(104, 40);
            this.leftPanelOpRelate.TabIndex = 2;
            this.leftPanelOpRelate.Text = "社交网络分析";
            this.leftPanelOpRelate.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(32, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 21);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // leftPanelOpCollide
            // 
            this.leftPanelOpCollide.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.leftPanelOpCollide.FlatAppearance.BorderSize = 0;
            this.leftPanelOpCollide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.leftPanelOpCollide.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.leftPanelOpCollide.Location = new System.Drawing.Point(66, 64);
            this.leftPanelOpCollide.Name = "leftPanelOpCollide";
            this.leftPanelOpCollide.Size = new System.Drawing.Size(104, 40);
            this.leftPanelOpCollide.TabIndex = 20;
            this.leftPanelOpCollide.Text = "自定义算子";
            this.leftPanelOpCollide.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(32, 74);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 21);
            this.pictureBox2.TabIndex = 19;
            this.pictureBox2.TabStop = false;
            // 
            // FlowChartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.leftPanelOpCollide);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.leftPanelOpRelate);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FlowChartControl";
            this.Size = new System.Drawing.Size(187, 637);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Common.NoFocusButton leftPanelOpRelate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Common.NoFocusButton leftPanelOpCollide;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
