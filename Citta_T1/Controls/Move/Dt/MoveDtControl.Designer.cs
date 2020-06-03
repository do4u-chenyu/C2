namespace Citta_T1.Controls.Move.Dt
{
    partial class MoveDtControl
    {

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveDtControl));
            this.PreviewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RunMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyFilePathToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // rightPictureBox
            // 
            this.rightPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rightPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("rightPictureBox.Image")));
            this.rightPictureBox.Location = new System.Drawing.Point(151, 5);
            this.rightPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rightPictureBox.TabIndex = 1;
            this.rightPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveDtControl_MouseDown);
            this.rightPictureBox.MouseEnter += new System.EventHandler(this.RightPictureBox_MouseEnter);
            this.rightPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveDtControl_MouseMove);
            this.rightPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveDtControl_MouseUp);
            // 
            // leftPictureBox
            // 
            this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.u72;
            this.leftPictureBox.Size = new System.Drawing.Size(14, 19);
            this.leftPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.leftPictureBox.TabIndex = 0;
            this.leftPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveDtControl_MouseDown);
            this.leftPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveDtControl_MouseMove);
            this.leftPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveDtControl_MouseUp);
            // 
            // textBox
            // 
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.Location = new System.Drawing.Point(27, 0);
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(111, 23);
            this.textBox.TabIndex = 5;
            this.textBox.Visible = false;
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.textBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // txtButton
            // 
            this.txtButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.txtButton.FlatAppearance.BorderSize = 0;
            this.txtButton.Location = new System.Drawing.Point(27, 2);
            this.txtButton.Margin = new System.Windows.Forms.Padding(0);
            this.txtButton.Size = new System.Drawing.Size(110, 14);
            this.txtButton.TabIndex = 8;
            this.txtButton.UseVisualStyleBackColor = true;
            this.txtButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TxtButton_MouseDown);
            this.txtButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveDtControl_MouseMove);
            this.txtButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveDtControl_MouseUp);
            // 
            // PreviewMenuItem
            // 
            this.PreviewMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.PreviewMenuItem.Name = "PreviewMenuItem";
            this.PreviewMenuItem.Size = new System.Drawing.Size(196, 24);
            this.PreviewMenuItem.Text = "预览";
            this.PreviewMenuItem.ToolTipText = "会在底层数据预览面板展示本数据源的前100行";
            this.PreviewMenuItem.Click += new System.EventHandler(this.PreViewMenuItem_Click);
            // 
            // OptionMenuItem
            // 
            this.OptionMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.OptionMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.OptionMenuItem.Enabled = false;
            this.OptionMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.OptionMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.OptionMenuItem.Name = "OptionMenuItem";
            this.OptionMenuItem.Size = new System.Drawing.Size(196, 24);
            this.OptionMenuItem.Text = "设置";
            this.OptionMenuItem.Click += new System.EventHandler(this.OptionMenuItem_Click);
            // 
            // RunMenuItem
            // 
            this.RunMenuItem.Enabled = false;
            this.RunMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.RunMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.RunMenuItem.Name = "RunMenuItem";
            this.RunMenuItem.Size = new System.Drawing.Size(196, 24);
            this.RunMenuItem.Text = "运行到此";
            this.RunMenuItem.ToolTipText = "将执行模型到本阶段为止";
            // 
            // LogMenuItem
            // 
            this.LogMenuItem.Enabled = false;
            this.LogMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.LogMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.LogMenuItem.Name = "LogMenuItem";
            this.LogMenuItem.Size = new System.Drawing.Size(196, 24);
            this.LogMenuItem.Text = "异常日志";
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.DeleteMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.Size = new System.Drawing.Size(196, 24);
            this.DeleteMenuItem.Text = "删除";
            this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // ExplorerToolStripMenuItem
            // 
            this.ExplorerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.ExplorerToolStripMenuItem.Name = "ExplorerToolStripMenuItem";
            this.ExplorerToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.ExplorerToolStripMenuItem.Text = "打开所在文件夹";
            this.ExplorerToolStripMenuItem.Click += new System.EventHandler(this.ExplorerToolStripMenuItem_Click);
            // 
            // CopyFilePathToClipboardToolStripMenuItem
            // 
            this.CopyFilePathToClipboardToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.CopyFilePathToClipboardToolStripMenuItem.Name = "CopyFilePathToClipboardToolStripMenuItem";
            this.CopyFilePathToClipboardToolStripMenuItem.Size = new System.Drawing.Size(196, 24);
            this.CopyFilePathToClipboardToolStripMenuItem.Text = "复制文件路径到剪切板";
            this.CopyFilePathToClipboardToolStripMenuItem.Click += new System.EventHandler(this.CopyFilePathToClipboardToolStripMenuItem_Click);
            // 
            // MoveDtControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Name = "MoveDtControl";
            this.Size = new System.Drawing.Size(185, 25);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MoveDtControl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveDtControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveDtControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveDtControl_MouseUp);
            this.Controls.SetChildIndex(this.leftPictureBox, 0);
            this.Controls.SetChildIndex(this.rightPictureBox, 0);
            this.Controls.SetChildIndex(this.textBox, 0);
            this.Controls.SetChildIndex(this.txtButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    
        private System.Windows.Forms.ToolStripMenuItem OptionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RunMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LogMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PreviewMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyFilePathToClipboardToolStripMenuItem;
    }
}