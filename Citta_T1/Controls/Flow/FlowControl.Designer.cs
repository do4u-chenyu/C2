namespace C2.Controls.Flow
{
    partial class FlowControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlowControl));
            this.movePictureBox = new System.Windows.Forms.PictureBox();
            this.zoomUpPictureBox = new System.Windows.Forms.PictureBox();
            this.zoomDownPictureBox = new System.Windows.Forms.PictureBox();
            this.remarkPictureBox = new System.Windows.Forms.PictureBox();
            this.framePictureBox = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.customOPButton1 = new C2.Controls.Common.NoFocusButton();
            this.noFocusButton1 = new C2.Controls.Common.NoFocusButton();
            this.noFocusButton2 = new C2.Controls.Common.NoFocusButton();
            this.noFocusButton3 = new C2.Controls.Common.NoFocusButton();
            this.noFocusButton4 = new C2.Controls.Common.NoFocusButton();
            ((System.ComponentModel.ISupportInitialize)(this.movePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomUpPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomDownPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.remarkPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.framePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // movePictureBox
            // 
            this.movePictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.movePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("movePictureBox.Image")));
            this.movePictureBox.Location = new System.Drawing.Point(131, 5);
            this.movePictureBox.Name = "movePictureBox";
            this.movePictureBox.Size = new System.Drawing.Size(22, 22);
            this.movePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.movePictureBox.TabIndex = 0;
            this.movePictureBox.TabStop = false;
            this.toolTip1.SetToolTip(this.movePictureBox, "拖动当前视野屏幕");
            this.movePictureBox.Click += new System.EventHandler(this.MovePictureBox_Click);
            this.movePictureBox.MouseEnter += new System.EventHandler(this.MovePictureBox_MouseEnter);
            this.movePictureBox.MouseLeave += new System.EventHandler(this.MovePictureBox_MouseLeave);
            // 
            // zoomUpPictureBox
            // 
            this.zoomUpPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.zoomUpPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("zoomUpPictureBox.Image")));
            this.zoomUpPictureBox.Location = new System.Drawing.Point(53, 5);
            this.zoomUpPictureBox.Name = "zoomUpPictureBox";
            this.zoomUpPictureBox.Size = new System.Drawing.Size(22, 22);
            this.zoomUpPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.zoomUpPictureBox.TabIndex = 1;
            this.zoomUpPictureBox.TabStop = false;
            this.toolTip1.SetToolTip(this.zoomUpPictureBox, "放大屏幕中算子并支持三级放大");
            this.zoomUpPictureBox.Click += new System.EventHandler(this.ZoomUpPictureBox_Click);
            this.zoomUpPictureBox.MouseEnter += new System.EventHandler(this.ZoomUpPictureBox_MouseEnter);
            this.zoomUpPictureBox.MouseLeave += new System.EventHandler(this.ZoomUpPictureBox_MouseLeave);
            // 
            // zoomDownPictureBox
            // 
            this.zoomDownPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.zoomDownPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.zoomDownPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("zoomDownPictureBox.Image")));
            this.zoomDownPictureBox.Location = new System.Drawing.Point(92, 5);
            this.zoomDownPictureBox.Name = "zoomDownPictureBox";
            this.zoomDownPictureBox.Size = new System.Drawing.Size(22, 22);
            this.zoomDownPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.zoomDownPictureBox.TabIndex = 2;
            this.zoomDownPictureBox.TabStop = false;
            this.toolTip1.SetToolTip(this.zoomDownPictureBox, "缩小当前屏幕中算子");
            this.zoomDownPictureBox.Click += new System.EventHandler(this.ZoomDownPictureBox_Click);
            this.zoomDownPictureBox.MouseEnter += new System.EventHandler(this.ZoomDownPictureBox_MouseEnter);
            this.zoomDownPictureBox.MouseLeave += new System.EventHandler(this.ZoomDownPictureBox_MouseLeave);
            // 
            // remarkPictureBox
            // 
            this.remarkPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.remarkPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("remarkPictureBox.Image")));
            this.remarkPictureBox.Location = new System.Drawing.Point(13, 4);
            this.remarkPictureBox.Name = "remarkPictureBox";
            this.remarkPictureBox.Size = new System.Drawing.Size(23, 23);
            this.remarkPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.remarkPictureBox.TabIndex = 3;
            this.remarkPictureBox.TabStop = false;
            this.toolTip1.SetToolTip(this.remarkPictureBox, "编写备注信息");
            this.remarkPictureBox.Click += new System.EventHandler(this.RemarkPictureBox_Click);
            this.remarkPictureBox.MouseEnter += new System.EventHandler(this.RemarkPictureBox_MouseEnter);
            this.remarkPictureBox.MouseLeave += new System.EventHandler(this.RemarkPictureBox_MouseLeave);
            // 
            // framePictureBox
            // 
            this.framePictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.framePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("framePictureBox.Image")));
            this.framePictureBox.Location = new System.Drawing.Point(171, 4);
            this.framePictureBox.Name = "framePictureBox";
            this.framePictureBox.Size = new System.Drawing.Size(24, 24);
            this.framePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.framePictureBox.TabIndex = 4;
            this.framePictureBox.TabStop = false;
            this.toolTip1.SetToolTip(this.framePictureBox, "框选屏幕中算子进行整体拖动");
            this.framePictureBox.Click += new System.EventHandler(this.FramePictureBox_Click);
            this.framePictureBox.MouseEnter += new System.EventHandler(this.FramePictureBox_MouseEnter);
            this.framePictureBox.MouseLeave += new System.EventHandler(this.FramePictureBox_MouseLeave);
            // 
            // customOPButton1
            // 
            this.customOPButton1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.customOPButton1.FlatAppearance.BorderSize = 0;
            this.customOPButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customOPButton1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.customOPButton1.Location = new System.Drawing.Point(3, 22);
            this.customOPButton1.Name = "customOPButton1";
            this.customOPButton1.Size = new System.Drawing.Size(45, 29);
            this.customOPButton1.TabIndex = 38;
            this.customOPButton1.Text = "备注";
            this.customOPButton1.UseVisualStyleBackColor = true;
            this.customOPButton1.Click += new System.EventHandler(this.customOPButton1_Click);
            // 
            // noFocusButton1
            // 
            this.noFocusButton1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.noFocusButton1.FlatAppearance.BorderSize = 0;
            this.noFocusButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noFocusButton1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.noFocusButton1.Location = new System.Drawing.Point(41, 22);
            this.noFocusButton1.Name = "noFocusButton1";
            this.noFocusButton1.Size = new System.Drawing.Size(45, 29);
            this.noFocusButton1.TabIndex = 39;
            this.noFocusButton1.Text = "放大";
            this.noFocusButton1.UseVisualStyleBackColor = true;
            // 
            // noFocusButton2
            // 
            this.noFocusButton2.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.noFocusButton2.FlatAppearance.BorderSize = 0;
            this.noFocusButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noFocusButton2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.noFocusButton2.Location = new System.Drawing.Point(81, 22);
            this.noFocusButton2.Name = "noFocusButton2";
            this.noFocusButton2.Size = new System.Drawing.Size(45, 29);
            this.noFocusButton2.TabIndex = 40;
            this.noFocusButton2.Text = "缩小";
            this.noFocusButton2.UseVisualStyleBackColor = true;
            // 
            // noFocusButton3
            // 
            this.noFocusButton3.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.noFocusButton3.FlatAppearance.BorderSize = 0;
            this.noFocusButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noFocusButton3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.noFocusButton3.Location = new System.Drawing.Point(120, 22);
            this.noFocusButton3.Name = "noFocusButton3";
            this.noFocusButton3.Size = new System.Drawing.Size(45, 29);
            this.noFocusButton3.TabIndex = 41;
            this.noFocusButton3.Text = "拖拽";
            this.noFocusButton3.UseVisualStyleBackColor = true;
            // 
            // noFocusButton4
            // 
            this.noFocusButton4.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.noFocusButton4.FlatAppearance.BorderSize = 0;
            this.noFocusButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noFocusButton4.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.noFocusButton4.Location = new System.Drawing.Point(161, 22);
            this.noFocusButton4.Name = "noFocusButton4";
            this.noFocusButton4.Size = new System.Drawing.Size(45, 29);
            this.noFocusButton4.TabIndex = 42;
            this.noFocusButton4.Text = "框选";
            this.noFocusButton4.UseVisualStyleBackColor = true;
            // 
            // FlowControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.framePictureBox);
            this.Controls.Add(this.remarkPictureBox);
            this.Controls.Add(this.zoomDownPictureBox);
            this.Controls.Add(this.zoomUpPictureBox);
            this.Controls.Add(this.movePictureBox);
            this.Controls.Add(this.noFocusButton4);
            this.Controls.Add(this.noFocusButton3);
            this.Controls.Add(this.noFocusButton2);
            this.Controls.Add(this.noFocusButton1);
            this.Controls.Add(this.customOPButton1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.Name = "FlowControl";
            this.Size = new System.Drawing.Size(209, 51);
            this.Load += new System.EventHandler(this.FlowControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.movePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomUpPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zoomDownPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.remarkPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.framePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox movePictureBox;
        private System.Windows.Forms.PictureBox zoomUpPictureBox;
        private System.Windows.Forms.PictureBox zoomDownPictureBox;
        private System.Windows.Forms.PictureBox remarkPictureBox;
        private System.Windows.Forms.PictureBox framePictureBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private Common.NoFocusButton customOPButton1;
        private Common.NoFocusButton noFocusButton1;
        private Common.NoFocusButton noFocusButton2;
        private Common.NoFocusButton noFocusButton3;
        private Common.NoFocusButton noFocusButton4;
    }
}
