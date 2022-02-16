namespace C2.Controls.Left
{
    partial class ModelButton
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
            this.rightPictureBox = new System.Windows.Forms.PictureBox();
            this.lelfPictureBox = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyFilePathToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textButton = new C2.Controls.Common.AutoEllipsisButton();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lelfPictureBox)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rightPictureBox
            // 
            this.rightPictureBox.Image = global::C2.Properties.Resources.提示;
            this.rightPictureBox.Location = new System.Drawing.Point(163, 8);
            this.rightPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.rightPictureBox.Name = "rightPictureBox";
            this.rightPictureBox.Size = new System.Drawing.Size(21, 20);
            this.rightPictureBox.TabIndex = 0;
            this.rightPictureBox.TabStop = false;
            // 
            // lelfPictureBox
            // 
            this.lelfPictureBox.Image = global::C2.Properties.Resources.聚沙成塔;
            this.lelfPictureBox.Location = new System.Drawing.Point(1, 1);
            this.lelfPictureBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lelfPictureBox.Name = "lelfPictureBox";
            this.lelfPictureBox.Size = new System.Drawing.Size(24, 24);
            this.lelfPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.lelfPictureBox.TabIndex = 1;
            this.lelfPictureBox.TabStop = false;
            this.toolTip1.SetToolTip(this.lelfPictureBox, "复杂运算流程");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem,
            this.toolStripSeparator1,
            this.ExplorerToolStripMenuItem,
            this.CopyFilePathToClipboardToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(229, 110);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.OpenToolStripMenuItem.Text = "打开";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(225, 6);
            // 
            // ExplorerToolStripMenuItem
            // 
            this.ExplorerToolStripMenuItem.Name = "ExplorerToolStripMenuItem";
            this.ExplorerToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.ExplorerToolStripMenuItem.Text = "打开所在文件夹";
            this.ExplorerToolStripMenuItem.Click += new System.EventHandler(this.ExplorerToolStripMenuItem_Click);
            // 
            // CopyFilePathToClipboardToolStripMenuItem
            // 
            this.CopyFilePathToClipboardToolStripMenuItem.Name = "CopyFilePathToClipboardToolStripMenuItem";
            this.CopyFilePathToClipboardToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
            this.CopyFilePathToClipboardToolStripMenuItem.Text = "复制文件路径到剪切板";
            this.CopyFilePathToClipboardToolStripMenuItem.Click += new System.EventHandler(this.CopyFilePathToClipboardToolStripMenuItem_Click);
            // 
            // textButton
            // 
            this.textButton.AutoEllipsis = true;
            this.textButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textButton.FlatAppearance.BorderSize = 0;
            this.textButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.textButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textButton.Location = new System.Drawing.Point(39, 4);
            this.textButton.Margin = new System.Windows.Forms.Padding(4);
            this.textButton.Name = "textButton";
            this.textButton.Size = new System.Drawing.Size(121, 31);
            this.textButton.TabIndex = 9;
            this.textButton.UseVisualStyleBackColor = false;
            this.textButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TextButton_MouseDown);
            this.textButton.MouseLeave += new System.EventHandler(this.TextButton_MouseLeave);
            this.textButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TextButton_MouseMove);
            this.textButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TextButton_MouseUp);
            // 
            // ModelButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.textButton);
            this.Controls.Add(this.lelfPictureBox);
            this.Controls.Add(this.rightPictureBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ModelButton";
            this.Size = new System.Drawing.Size(187, 34);
            this.Load += new System.EventHandler(this.ModelButton_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lelfPictureBox)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox rightPictureBox;
        private System.Windows.Forms.PictureBox lelfPictureBox;
        private C2.Controls.Common.AutoEllipsisButton textButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyFilePathToClipboardToolStripMenuItem;
    }
}
