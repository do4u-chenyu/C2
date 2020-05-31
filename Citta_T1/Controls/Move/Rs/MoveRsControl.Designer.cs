
namespace Citta_T1.Controls.Move.Rs
{
    partial class MoveRsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveRsControl));
            this.PreviewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RunMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ErrorLogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyFilePathToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // rightPictureBox
            // 
            this.rightPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("rightPictureBox.Image")));
            this.rightPictureBox.Location = new System.Drawing.Point(149, 4);
            this.rightPictureBox.Size = new System.Drawing.Size(19, 21);
            this.rightPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rightPictureBox.TabIndex = 1;
            this.rightPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseDown);
            this.rightPictureBox.MouseEnter += new System.EventHandler(this.RightPictureBox_MouseEnter);
            this.rightPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseMove);
            this.rightPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseUp);
            // 
            // leftPictureBox
            // 
            this.leftPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("leftPictureBox.Image")));
            this.leftPictureBox.Location = new System.Drawing.Point(11, 3);
            this.leftPictureBox.Size = new System.Drawing.Size(15, 21);
            this.leftPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.leftPictureBox.TabIndex = 0;
            this.leftPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseDown);
            this.leftPictureBox.MouseEnter += new System.EventHandler(this.LeftPicture_MouseEnter);
            this.leftPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseMove);
            this.leftPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseUp);
            // 
            // textBox
            // 
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.Location = new System.Drawing.Point(28, 1);
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(111, 23);
            this.textBox.TabIndex = 5;
            this.textBox.Text = "连接算子";
            this.textBox.Visible = false;
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.textBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // txtButton
            // 
            this.txtButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.txtButton.FlatAppearance.BorderSize = 0;
            this.txtButton.Location = new System.Drawing.Point(29, 2);
            this.txtButton.Margin = new System.Windows.Forms.Padding(2);
            this.txtButton.Size = new System.Drawing.Size(110, 23);
            this.txtButton.TabIndex = 8;
            this.txtButton.UseVisualStyleBackColor = true;
            this.txtButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TxtButton_MouseDown);
            this.txtButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseMove);
            this.txtButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseUp);
            // 
            // PreviewMenuItem
            // 
            this.PreviewMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.PreviewMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PreviewMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.PreviewMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.PreviewMenuItem.Name = "PreviewMenuItem";
            this.PreviewMenuItem.Size = new System.Drawing.Size(217, 24);
            this.PreviewMenuItem.Text = "预览";
            this.PreviewMenuItem.ToolTipText = "会在底层数据预览面板展示运算结果的前100行";
            this.PreviewMenuItem.Click += new System.EventHandler(this.PreviewMenuItem_Click);
            // 
            // RenameMenuItem
            // 
            this.RenameMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.RenameMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.RenameMenuItem.Name = "RenameMenuItem";
            this.RenameMenuItem.Size = new System.Drawing.Size(217, 24);
            this.RenameMenuItem.Text = "重命名";
            this.RenameMenuItem.Click += new System.EventHandler(this.RenameMenuItem_Click);
            // 
            // SaveAsToolStripMenuItem
            // 
            this.SaveAsToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.SaveAsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            this.SaveAsToolStripMenuItem.Size = new System.Drawing.Size(217, 24);
            this.SaveAsToolStripMenuItem.Text = "另存为";
            this.SaveAsToolStripMenuItem.ToolTipText = "将运算得到的结果文件导出到指定位置";
            this.SaveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // RunMenuItem
            // 
            this.RunMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.RunMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.RunMenuItem.Name = "RunMenuItem";
            this.RunMenuItem.Size = new System.Drawing.Size(217, 24);
            this.RunMenuItem.Text = "运行到此";
            this.RunMenuItem.ToolTipText = "将执行模型到本阶段为止";
            this.RunMenuItem.Click += new System.EventHandler(this.RunMenuItem_Click);
            // 
            // ErrorLogMenuItem
            // 
            this.ErrorLogMenuItem.Enabled = false;
            this.ErrorLogMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.ErrorLogMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.ErrorLogMenuItem.Name = "ErrorLogMenuItem";
            this.ErrorLogMenuItem.Size = new System.Drawing.Size(217, 24);
            this.ErrorLogMenuItem.Text = "异常日志";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(214, 6);
            // 
            // ExplorerToolStripMenuItem
            // 
            this.ExplorerToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.ExplorerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.ExplorerToolStripMenuItem.Name = "ExplorerToolStripMenuItem";
            this.ExplorerToolStripMenuItem.Size = new System.Drawing.Size(217, 24);
            this.ExplorerToolStripMenuItem.Text = "打开所在文件夹";
            this.ExplorerToolStripMenuItem.Click += new System.EventHandler(this.ExplorerToolStripMenuItem_Click);
            // 
            // CopyFilePathToClipboardToolStripMenuItem
            // 
            this.CopyFilePathToClipboardToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.CopyFilePathToClipboardToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.CopyFilePathToClipboardToolStripMenuItem.Name = "CopyFilePathToClipboardToolStripMenuItem";
            this.CopyFilePathToClipboardToolStripMenuItem.Size = new System.Drawing.Size(217, 24);
            this.CopyFilePathToClipboardToolStripMenuItem.Text = "复制文件路径到剪切板";
            this.CopyFilePathToClipboardToolStripMenuItem.Click += new System.EventHandler(this.CopyFilePathToClipboardToolStripMenuItem_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "bcp";
            this.saveFileDialog.Filter = "BCP 文件|*.bcp|所有文件|*.*";
            this.saveFileDialog.RestoreDirectory = true;
            this.saveFileDialog.Title = "导入结果文件";
            // 
            // MoveRsControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Description = "连接算子";
            this.Name = "MoveRsControl";
            this.Size = new System.Drawing.Size(185, 25);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MoveOpControl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseUp);
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
        private System.Windows.Forms.ToolStripMenuItem PreviewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RenameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RunMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ErrorLogMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyFilePathToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}
