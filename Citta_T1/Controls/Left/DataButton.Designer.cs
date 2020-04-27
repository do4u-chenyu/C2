namespace Citta_T1.Controls.Left
{
    partial class DataButton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataButton));
            this.txtButton = new System.Windows.Forms.Button();
            this.leftPictureBox = new System.Windows.Forms.PictureBox();
            this.rightPictureBox = new System.Windows.Forms.PictureBox();
            this.helpToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ReviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.打开所在文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制文件路径到剪切板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtButton
            // 
            this.txtButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtButton.FlatAppearance.BorderSize = 0;
            this.txtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtButton.Location = new System.Drawing.Point(40, 1);
            this.txtButton.Name = "txtButton";
            this.txtButton.Size = new System.Drawing.Size(121, 33);
            this.txtButton.TabIndex = 9;
            this.txtButton.Text = "button1";
            this.txtButton.UseVisualStyleBackColor = false;
            // 
            // leftPictureBox
            // 
            this.leftPictureBox.Image = global::Citta_T1.Properties.Resources.u72;
            this.leftPictureBox.Location = new System.Drawing.Point(3, 1);
            this.leftPictureBox.Name = "leftPictureBox";
            this.leftPictureBox.Size = new System.Drawing.Size(31, 31);
            this.leftPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.leftPictureBox.TabIndex = 10;
            this.leftPictureBox.TabStop = false;
            // 
            // rightPictureBox
            // 
            this.rightPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("rightPictureBox.Image")));
            this.rightPictureBox.Location = new System.Drawing.Point(159, 4);
            this.rightPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.rightPictureBox.Name = "rightPictureBox";
            this.rightPictureBox.Size = new System.Drawing.Size(25, 28);
            this.rightPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rightPictureBox.TabIndex = 11;
            this.rightPictureBox.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReviewToolStripMenuItem,
            this.RenameToolStripMenuItem,
            this.DeleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.打开所在文件夹ToolStripMenuItem,
            this.复制文件路径到剪切板ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(241, 140);
            // 
            // ReviewToolStripMenuItem
            // 
            this.ReviewToolStripMenuItem.Name = "ReviewToolStripMenuItem";
            this.ReviewToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.ReviewToolStripMenuItem.Text = "预览";
            this.ReviewToolStripMenuItem.Click += new System.EventHandler(this.ReviewToolStripMenuItem_Click);
            // 
            // RenameToolStripMenuItem
            // 
            this.RenameToolStripMenuItem.Name = "RenameToolStripMenuItem";
            this.RenameToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.RenameToolStripMenuItem.Text = "重命名";
            this.RenameToolStripMenuItem.Click += new System.EventHandler(this.RenameToolStripMenuItem_Click);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Enabled = false;
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.DeleteToolStripMenuItem.Text = "删除";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(237, 6);
            // 
            // 打开所在文件夹ToolStripMenuItem
            // 
            this.打开所在文件夹ToolStripMenuItem.Name = "打开所在文件夹ToolStripMenuItem";
            this.打开所在文件夹ToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.打开所在文件夹ToolStripMenuItem.Text = "打开所在文件夹";
            this.打开所在文件夹ToolStripMenuItem.Click += new System.EventHandler(this.OpenFilePathMenuItem_Click);
            // 
            // 复制文件路径到剪切板ToolStripMenuItem
            // 
            this.复制文件路径到剪切板ToolStripMenuItem.Name = "复制文件路径到剪切板ToolStripMenuItem";
            this.复制文件路径到剪切板ToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.复制文件路径到剪切板ToolStripMenuItem.Text = "复制文件路径到剪切板";
            this.复制文件路径到剪切板ToolStripMenuItem.Click += new System.EventHandler(this.CopyFilePathToClipboard);
            // 
            // DataButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.rightPictureBox);
            this.Controls.Add(this.leftPictureBox);
            this.Controls.Add(this.txtButton);
            this.Name = "DataButton";
            this.Size = new System.Drawing.Size(188, 36);
            this.Load += new System.EventHandler(this.DataButton_Load);
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button txtButton;
        private System.Windows.Forms.PictureBox leftPictureBox;
        private System.Windows.Forms.PictureBox rightPictureBox;
        private System.Windows.Forms.ToolTip helpToolTip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ReviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 打开所在文件夹ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制文件路径到剪切板ToolStripMenuItem;
    }
}
