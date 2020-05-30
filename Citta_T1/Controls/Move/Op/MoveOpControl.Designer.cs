
namespace Citta_T1.Controls.Move.Op
{
    partial class MoveOpControl
    {

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveOpControl));
            this.OptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemarkMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RunMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ErrorLogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBox)).BeginInit();
            this.SuspendLayout();
            // 
            // rightPictureBox
            // 
            this.rightPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("rightPictureBox.Image")));
            this.rightPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rightPictureBox.TabIndex = 1;
            this.rightPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.rightPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.rightPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            // 
            // leftPictureBox
            // 
            this.leftPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("leftPictureBox.Image")));
            this.leftPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.leftPictureBox.TabIndex = 0;
            this.leftPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.leftPictureBox.MouseEnter += new System.EventHandler(this.LeftPicture_MouseEnter);
            this.leftPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.leftPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.White;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.ReadOnly = true;
            this.textBox.TabIndex = 5;
            this.textBox.Text = "b";
            this.textBox.Visible = false;
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.textBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // txtButton
            // 
            this.txtButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.txtButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtButton.FlatAppearance.BorderSize = 0;
            this.txtButton.Margin = new System.Windows.Forms.Padding(2);
            this.txtButton.TabIndex = 8;
            this.txtButton.UseVisualStyleBackColor = false;
            this.txtButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TxtButton_MouseDown);
            this.txtButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.txtButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            // 
            // OptionMenuItem
            // 
            this.OptionMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.OptionMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OptionMenuItem.BackgroundImage")));
            this.OptionMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.OptionMenuItem.Enabled = false;
            this.OptionMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.OptionMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.OptionMenuItem.Name = "OptionMenuItem";
            this.OptionMenuItem.Size = new System.Drawing.Size(133, 24);
            this.OptionMenuItem.Text = "设置";
            this.OptionMenuItem.ToolTipText = "配置算子";
            this.OptionMenuItem.Click += new System.EventHandler(this.OptionMenuItem_Click);
            // 
            // RenameMenuItem
            // 
            this.RenameMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RenameMenuItem.BackgroundImage")));
            this.RenameMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.RenameMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.RenameMenuItem.Name = "RenameMenuItem";
            this.RenameMenuItem.Size = new System.Drawing.Size(133, 24);
            this.RenameMenuItem.Text = "重命名";
            this.RenameMenuItem.Click += new System.EventHandler(this.RenameMenuItem_Click);
            // 
            // RemarkMenuItem
            // 
            this.RemarkMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RemarkMenuItem.BackgroundImage")));
            this.RemarkMenuItem.Enabled = false;
            this.RemarkMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.RemarkMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.RemarkMenuItem.Name = "RemarkMenuItem";
            this.RemarkMenuItem.Size = new System.Drawing.Size(133, 24);
            this.RemarkMenuItem.Text = "备注";
            this.RemarkMenuItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // RunMenuItem
            // 
            this.RunMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RunMenuItem.BackgroundImage")));
            this.RunMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.RunMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.RunMenuItem.Name = "RunMenuItem";
            this.RunMenuItem.Size = new System.Drawing.Size(133, 24);
            this.RunMenuItem.Text = "运行到此";
            this.RunMenuItem.ToolTipText = "将执行模型到本阶段为止";
            this.RunMenuItem.Click += new System.EventHandler(this.RunMenuItem_Click);
            // 
            // ErrorLogMenuItem
            // 
            this.ErrorLogMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ErrorLogMenuItem.BackgroundImage")));
            this.ErrorLogMenuItem.Enabled = false;
            this.ErrorLogMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.ErrorLogMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.ErrorLogMenuItem.Name = "ErrorLogMenuItem";
            this.ErrorLogMenuItem.Size = new System.Drawing.Size(133, 24);
            this.ErrorLogMenuItem.Text = "异常日志";
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DeleteMenuItem.BackgroundImage")));
            this.DeleteMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.DeleteMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.Size = new System.Drawing.Size(133, 24);
            this.DeleteMenuItem.Text = "删除";
            this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // statusBox
            // 
            this.statusBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.statusBox.Image = ((System.Drawing.Image)(resources.GetObject("statusBox.Image")));
            this.statusBox.Location = new System.Drawing.Point(98, 5);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(14, 14);
            this.statusBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.statusBox.TabIndex = 9;
            this.statusBox.TabStop = false;
            this.helpToolTip.SetToolTip(this.statusBox, "配置算子");
            this.statusBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StatusBox_MouseDown);
            this.statusBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.statusBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            // 
            // MoveOpControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.statusBox);
            this.Description = "b";
            this.Name = "MoveOpControl";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MoveOpControl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            this.Controls.SetChildIndex(this.statusBox, 0);
            this.Controls.SetChildIndex(this.leftPictureBox, 0);
            this.Controls.SetChildIndex(this.rightPictureBox, 0);
            this.Controls.SetChildIndex(this.textBox, 0);
            this.Controls.SetChildIndex(this.txtButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem OptionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RenameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemarkMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RunMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ErrorLogMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
        private System.Windows.Forms.PictureBox statusBox;
    }
}
