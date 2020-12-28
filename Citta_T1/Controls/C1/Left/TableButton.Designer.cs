using C2.Core;

namespace C2.Controls.Left
{
    partial class TableButton
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
            this.leftPictureBox = new System.Windows.Forms.PictureBox();
            this.rightPictureBox = new System.Windows.Forms.PictureBox();
            this.helpToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ReviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReviewStruToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyTablePathToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox = new System.Windows.Forms.TextBox();
            this.txtButton = new C2.Controls.Common.NoFocusButton();
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftPictureBox
            // 
            this.leftPictureBox.Image = global::C2.Properties.Resources.Table;
            this.leftPictureBox.Location = new System.Drawing.Point(1, 1);
            this.leftPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.leftPictureBox.Name = "leftPictureBox";
            this.leftPictureBox.Size = new System.Drawing.Size(24, 24);
            this.leftPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.leftPictureBox.TabIndex = 10;
            this.leftPictureBox.TabStop = false;
            this.leftPictureBox.MouseEnter += new System.EventHandler(this.LeftPictureBox_MouseEnter);
            // 
            // rightPictureBox
            // 
            this.rightPictureBox.Image = global::C2.Properties.Resources.提示;
            this.rightPictureBox.Location = new System.Drawing.Point(122, 6);
            this.rightPictureBox.Name = "rightPictureBox";
            this.rightPictureBox.Size = new System.Drawing.Size(16, 16);
            this.rightPictureBox.TabIndex = 11;
            this.rightPictureBox.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReviewToolStripMenuItem,
            this.ReviewStruToolStripMenuItem,
            this.RefreshToolStripMenuItem,
            this.CopyTablePathToClipboardToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(185, 114);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // ReviewToolStripMenuItem
            // 
            this.ReviewToolStripMenuItem.Name = "ReviewToolStripMenuItem";
            this.ReviewToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.ReviewToolStripMenuItem.Text = "表预览";
            this.ReviewToolStripMenuItem.ToolTipText = "预览表内容";
            this.ReviewToolStripMenuItem.Click += new System.EventHandler(this.ReviewToolStripMenuItem_Click);
            // 
            // ReviewStruToolStripMenuItem
            // 
            this.ReviewStruToolStripMenuItem.Name = "ReviewStruToolStripMenuItem";
            this.ReviewStruToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.ReviewStruToolStripMenuItem.Text = "表结构预览";
            this.ReviewStruToolStripMenuItem.Click += new System.EventHandler(this.ReviewStruToolStripMenuItem_Click);
            // 
            // RefreshToolStripMenuItem
            // 
            this.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem";
            this.RefreshToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.RefreshToolStripMenuItem.Text = "刷新";
            this.RefreshToolStripMenuItem.ToolTipText = "刷新程序缓存,重新加载数据源";
            this.RefreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItem_Click);
            // 
            // CopyTablePathToClipboardToolStripMenuItem
            // 
            this.CopyTablePathToClipboardToolStripMenuItem.Name = "CopyTablePathToClipboardToolStripMenuItem";
            this.CopyTablePathToClipboardToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.CopyTablePathToClipboardToolStripMenuItem.Text = "复制表路径到剪切板";
            this.CopyTablePathToClipboardToolStripMenuItem.Click += new System.EventHandler(this.CopyFullFilePathToClipboard);
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.White;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBox.Location = new System.Drawing.Point(25, 3);
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(94, 16);
            this.textBox.TabIndex = 12;
            this.textBox.Visible = false;
            // 
            // txtButton
            // 
            this.txtButton.BackColor = System.Drawing.Color.White;
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
            // TableButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.rightPictureBox);
            this.Controls.Add(this.leftPictureBox);
            this.Controls.Add(this.txtButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TableButton";
            this.Size = new System.Drawing.Size(140, 27);
            this.Load += new System.EventHandler(this.TableButton_Load);
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
        private System.Windows.Forms.ToolStripMenuItem ReviewStruToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyTablePathToClipboardToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ToolStripMenuItem RefreshToolStripMenuItem;
    }
}
