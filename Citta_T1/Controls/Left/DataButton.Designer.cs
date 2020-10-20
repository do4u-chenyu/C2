namespace C2.Controls.Left
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
            this.txtButton = new C2.Controls.Common.NoFocusButton();
            this.leftPictureBox = new System.Windows.Forms.PictureBox();
            this.rightPictureBox = new System.Windows.Forms.PictureBox();
            this.helpToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ReviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyFilePathToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtButton
            // 
            this.txtButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtButton.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtButton.FlatAppearance.BorderSize = 0;
            this.txtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtButton.Location = new System.Drawing.Point(25, 1);
            this.txtButton.Margin = new System.Windows.Forms.Padding(2);
            this.txtButton.Name = "txtButton";
            this.txtButton.Size = new System.Drawing.Size(95, 25);
            this.txtButton.TabIndex = 9;
            this.txtButton.UseVisualStyleBackColor = false;
            this.txtButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TxtButton_MouseDown);
            // 
            // leftPictureBox
            // 
            this.leftPictureBox.Image = global::C2.Properties.Resources.u72;
            this.leftPictureBox.Location = new System.Drawing.Point(-1, 1);
            this.leftPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.leftPictureBox.Name = "leftPictureBox";
            this.leftPictureBox.Size = new System.Drawing.Size(23, 23);
            this.leftPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.leftPictureBox.TabIndex = 10;
            this.leftPictureBox.TabStop = false;
            this.leftPictureBox.MouseEnter += new System.EventHandler(this.LeftPictureBox_MouseEnter);
            // 
            // rightPictureBox
            // 
            this.rightPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("rightPictureBox.Image")));
            this.rightPictureBox.Location = new System.Drawing.Point(125, 3);
            this.rightPictureBox.Name = "rightPictureBox";
            this.rightPictureBox.Size = new System.Drawing.Size(19, 21);
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
            this.RemoveToolStripMenuItem,
            this.toolStripSeparator1,
            this.ExplorerToolStripMenuItem,
            this.CopyFilePathToClipboardToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(197, 120);
            // 
            // ReviewToolStripMenuItem
            // 
            this.ReviewToolStripMenuItem.Name = "ReviewToolStripMenuItem";
            this.ReviewToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ReviewToolStripMenuItem.Text = "预览";
            this.ReviewToolStripMenuItem.Click += new System.EventHandler(this.ReviewToolStripMenuItem_Click);
            // 
            // RenameToolStripMenuItem
            // 
            this.RenameToolStripMenuItem.Name = "RenameToolStripMenuItem";
            this.RenameToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.RenameToolStripMenuItem.Text = "重命名";
            this.RenameToolStripMenuItem.Click += new System.EventHandler(this.RenameToolStripMenuItem_Click);
            // 
            // RemoveToolStripMenuItem
            // 
            this.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem";
            this.RemoveToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.RemoveToolStripMenuItem.Text = "卸载";
            this.RemoveToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // ExplorerToolStripMenuItem
            // 
            this.ExplorerToolStripMenuItem.Name = "ExplorerToolStripMenuItem";
            this.ExplorerToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ExplorerToolStripMenuItem.Text = "打开所在文件夹";
            this.ExplorerToolStripMenuItem.Click += new System.EventHandler(this.OpenFilePathMenuItem_Click);
            // 
            // CopyFilePathToClipboardToolStripMenuItem
            // 
            this.CopyFilePathToClipboardToolStripMenuItem.Name = "CopyFilePathToClipboardToolStripMenuItem";
            this.CopyFilePathToClipboardToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.CopyFilePathToClipboardToolStripMenuItem.Text = "复制文件路径到剪切板";
            this.CopyFilePathToClipboardToolStripMenuItem.Click += new System.EventHandler(this.CopyFullFilePathToClipboard);
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.White;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBox.Location = new System.Drawing.Point(25, 3);
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(94, 23);
            this.textBox.TabIndex = 12;
            this.textBox.Visible = false;
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.textBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // DataButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.rightPictureBox);
            this.Controls.Add(this.leftPictureBox);
            this.Controls.Add(this.txtButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DataButton";
            this.Size = new System.Drawing.Size(145, 27);
            this.Load += new System.EventHandler(this.DataButton_Load);
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C2.Controls.Common.NoFocusButton txtButton;
        private System.Windows.Forms.PictureBox leftPictureBox;
        private System.Windows.Forms.PictureBox rightPictureBox;
        private System.Windows.Forms.ToolTip helpToolTip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ReviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyFilePathToClipboardToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox;
    }
}
