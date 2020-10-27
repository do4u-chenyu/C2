namespace C2.Controls.Left
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
            this.customOPButton1 = new C2.Controls.Common.NoFocusButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pythonOPButton = new C2.Controls.Common.NoFocusButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.customOPButton2 = new C2.Controls.Common.NoFocusButton();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // customOPButton1
            // 
            this.customOPButton1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.customOPButton1.FlatAppearance.BorderSize = 0;
            this.customOPButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customOPButton1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.customOPButton1.Location = new System.Drawing.Point(55, 23);
            this.customOPButton1.Name = "customOPButton1";
            this.customOPButton1.Size = new System.Drawing.Size(74, 37);
            this.customOPButton1.TabIndex = 20;
            this.customOPButton1.Text = "AI实践";
            this.customOPButton1.UseVisualStyleBackColor = true;
            this.customOPButton1.Click += new System.EventHandler(this.customOPButton1_Click);
            this.customOPButton1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FlowChartControl_MouseDown);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(34, 31);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 21);
            this.pictureBox2.TabIndex = 19;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(32, 102);
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
            this.pythonOPButton.Location = new System.Drawing.Point(55, 95);
            this.pythonOPButton.Name = "pythonOPButton";
            this.pythonOPButton.Size = new System.Drawing.Size(99, 37);
            this.pythonOPButton.TabIndex = 22;
            this.pythonOPButton.Text = "Python算子";
            this.pythonOPButton.UseVisualStyleBackColor = true;
            this.pythonOPButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FlowChartControl_MouseDown);
            // 
            // customOPButton2
            // 
            this.customOPButton2.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.customOPButton2.FlatAppearance.BorderSize = 0;
            this.customOPButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customOPButton2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.customOPButton2.Location = new System.Drawing.Point(59, 58);
            this.customOPButton2.Name = "customOPButton2";
            this.customOPButton2.Size = new System.Drawing.Size(74, 37);
            this.customOPButton2.TabIndex = 24;
            this.customOPButton2.Text = "多源算子";
            this.customOPButton2.UseVisualStyleBackColor = true;
            this.customOPButton2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FlowChartControl_MouseDown);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(34, 66);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(20, 21);
            this.pictureBox4.TabIndex = 23;
            this.pictureBox4.TabStop = false;
            // 
            // FlowChartControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.customOPButton2);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pythonOPButton);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.customOPButton1);
            this.Controls.Add(this.pictureBox2);
            this.Name = "FlowChartControl";
            this.Size = new System.Drawing.Size(187, 637);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Common.NoFocusButton customOPButton1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private Common.NoFocusButton pythonOPButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private Common.NoFocusButton customOPButton2;
        private System.Windows.Forms.PictureBox pictureBox4;
    }
}
