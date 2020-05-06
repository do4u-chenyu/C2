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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlowChartControl));
            this.networkOPButton = new Citta_T1.Controls.Common.NoFocusButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.customOPButton = new Citta_T1.Controls.Common.NoFocusButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pythonOPButton = new Citta_T1.Controls.Common.NoFocusButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // networkOPButton
            // 
            this.networkOPButton.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.networkOPButton.FlatAppearance.BorderSize = 0;
            this.networkOPButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.networkOPButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.networkOPButton.Location = new System.Drawing.Point(66, 18);
            this.networkOPButton.Name = "networkOPButton";
            this.networkOPButton.Size = new System.Drawing.Size(104, 40);
            this.networkOPButton.TabIndex = 2;
            this.networkOPButton.Text = "社交网络分析";
            this.networkOPButton.UseVisualStyleBackColor = true;
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
            // customOPButton
            // 
            this.customOPButton.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.customOPButton.FlatAppearance.BorderSize = 0;
            this.customOPButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customOPButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.customOPButton.Location = new System.Drawing.Point(66, 64);
            this.customOPButton.Name = "customOPButton";
            this.customOPButton.Size = new System.Drawing.Size(104, 40);
            this.customOPButton.TabIndex = 20;
            this.customOPButton.Text = "自定义算子";
            this.customOPButton.UseVisualStyleBackColor = true;
            this.customOPButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FlowChartControl_MouseDown);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(32, 72);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 21);
            this.pictureBox2.TabIndex = 19;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(32, 116);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 21);
            this.pictureBox3.TabIndex = 21;
            this.pictureBox3.TabStop = false;
            // 
            // pythonOPButton
            // 
            this.pythonOPButton.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pythonOPButton.FlatAppearance.BorderSize = 0;
            this.pythonOPButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pythonOPButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.pythonOPButton.Location = new System.Drawing.Point(66, 110);
            this.pythonOPButton.Name = "pythonOPButton";
            this.pythonOPButton.Size = new System.Drawing.Size(104, 40);
            this.pythonOPButton.TabIndex = 22;
            this.pythonOPButton.Text = "Python算子";
            this.pythonOPButton.UseVisualStyleBackColor = true;
            this.pythonOPButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FlowChartControl_MouseDown);
            // 
            // FlowChartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pythonOPButton);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.customOPButton);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.networkOPButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FlowChartControl";
            this.Size = new System.Drawing.Size(187, 637);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FlowChartControl_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Common.NoFocusButton networkOPButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Common.NoFocusButton customOPButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private Common.NoFocusButton pythonOPButton;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
