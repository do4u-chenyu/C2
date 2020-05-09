
namespace Citta_T1.Controls.Move
{
    partial class MoveOpControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveOpControl));
            this.rightPictureBox = new System.Windows.Forms.PictureBox();
            this.leftPicture = new System.Windows.Forms.PictureBox();
            this.leftPinPictureBox = new System.Windows.Forms.PictureBox();
            this.rightPinPictureBox = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RenameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemarkMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RunMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ErrorLogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.textBox = new System.Windows.Forms.TextBox();
            this.helpToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.statusBox = new System.Windows.Forms.PictureBox();
            this.txtButton = new Citta_T1.Controls.Common.NoFocusButton();
            this.BinaryOperatorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SingleOperatorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPinPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPinPictureBox)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBox)).BeginInit();
            this.SuspendLayout();
            // 
            // rightPictureBox
            // 
            this.rightPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("rightPictureBox.Image")));
            this.rightPictureBox.Location = new System.Drawing.Point(117, 5);
            this.rightPictureBox.Name = "rightPictureBox";
            this.rightPictureBox.Size = new System.Drawing.Size(14, 14);
            this.rightPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rightPictureBox.TabIndex = 1;
            this.rightPictureBox.TabStop = false;
            this.rightPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.rightPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.rightPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            // 
            // leftPicture
            // 
            this.leftPicture.Image = ((System.Drawing.Image)(resources.GetObject("leftPicture.Image")));
            this.leftPicture.Location = new System.Drawing.Point(11, 2);
            this.leftPicture.Name = "leftPicture";
            this.leftPicture.Size = new System.Drawing.Size(18, 22);
            this.leftPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.leftPicture.TabIndex = 0;
            this.leftPicture.TabStop = false;
            this.leftPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.leftPicture.MouseEnter += new System.EventHandler(this.LeftPicture_MouseEnter);
            this.leftPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.leftPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            // 
            // leftPinPictureBox
            // 
            this.leftPinPictureBox.Location = new System.Drawing.Point(0, 0);
            this.leftPinPictureBox.Name = "leftPinPictureBox";
            this.leftPinPictureBox.Size = new System.Drawing.Size(100, 50);
            this.leftPinPictureBox.TabIndex = 0;
            this.leftPinPictureBox.TabStop = false;
            // 
            // rightPinPictureBox
            // 
            this.rightPinPictureBox.Location = new System.Drawing.Point(0, 0);
            this.rightPinPictureBox.Name = "rightPinPictureBox";
            this.rightPinPictureBox.Size = new System.Drawing.Size(100, 50);
            this.rightPinPictureBox.TabIndex = 0;
            this.rightPinPictureBox.TabStop = false;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionMenuItem,
            this.RenameMenuItem,
            this.RemarkMenuItem,
            this.RunMenuItem,
            this.ErrorLogMenuItem,
            this.DeleteMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(148, 172);
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
            this.OptionMenuItem.Size = new System.Drawing.Size(147, 28);
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
            this.RenameMenuItem.Size = new System.Drawing.Size(147, 28);
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
            this.RemarkMenuItem.Size = new System.Drawing.Size(147, 28);
            this.RemarkMenuItem.Text = "备注";
            this.RemarkMenuItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // RunMenuItem
            // 
            this.RunMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RunMenuItem.BackgroundImage")));
            this.RunMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.RunMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.RunMenuItem.Name = "RunMenuItem";
            this.RunMenuItem.Size = new System.Drawing.Size(147, 28);
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
            this.ErrorLogMenuItem.Size = new System.Drawing.Size(147, 28);
            this.ErrorLogMenuItem.Text = "异常日志";
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DeleteMenuItem.BackgroundImage")));
            this.DeleteMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.DeleteMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.Size = new System.Drawing.Size(147, 28);
            this.DeleteMenuItem.Text = "删除";
            this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            this.toolStripSeparator1.Visible = false;
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.White;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBox.Location = new System.Drawing.Point(31, 1);
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(60, 27);
            this.textBox.TabIndex = 5;
            this.textBox.Text = "连接算子";
            this.textBox.Visible = false;
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.textBox.Leave += new System.EventHandler(this.TextBox_Leave);
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
            // txtButton
            // 
            this.txtButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.txtButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtButton.FlatAppearance.BorderSize = 0;
            this.txtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtButton.Location = new System.Drawing.Point(30, 2);
            this.txtButton.Margin = new System.Windows.Forms.Padding(2);
            this.txtButton.Name = "txtButton";
            this.txtButton.Size = new System.Drawing.Size(60, 25);
            this.txtButton.TabIndex = 8;
            this.txtButton.Text = "button1";
            this.txtButton.UseVisualStyleBackColor = false;
            this.txtButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TxtButton_MouseDown);
            this.txtButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.txtButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            // 
            // BinaryOperatorMenuItem
            // 
            this.BinaryOperatorMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.BinaryOperatorMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.BinaryOperatorMenuItem.Name = "BinaryOperatorMenuItem";
            this.BinaryOperatorMenuItem.Size = new System.Drawing.Size(180, 24);
            this.BinaryOperatorMenuItem.Text = "二元算子";
            this.BinaryOperatorMenuItem.Visible = false;
            // 
            // SingleOperatorMenuItem
            // 
            this.SingleOperatorMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.SingleOperatorMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.SingleOperatorMenuItem.Name = "SingleOperatorMenuItem";
            this.SingleOperatorMenuItem.Size = new System.Drawing.Size(180, 24);
            this.SingleOperatorMenuItem.Text = "一元算子";
            this.SingleOperatorMenuItem.Visible = false;
            // 
            // MoveOpControl
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.txtButton);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.rightPictureBox);
            this.Controls.Add(this.leftPicture);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "MoveOpControl";
            this.Size = new System.Drawing.Size(169, 25);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MoveOpControl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPinPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPinPictureBox)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statusBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox rightPictureBox;
        public System.Windows.Forms.PictureBox leftPicture;
        public System.Windows.Forms.PictureBox leftPinPictureBox;
        public System.Windows.Forms.PictureBox rightPinPictureBox;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OptionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RenameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemarkMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RunMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ErrorLogMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
        public System.Windows.Forms.ToolTip nameToolTip;
        public System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ToolTip helpToolTip;
        private Citta_T1.Controls.Common.NoFocusButton txtButton;
        private System.Windows.Forms.PictureBox statusBox;
        public System.Windows.Forms.ToolTip idToolTip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem SingleOperatorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BinaryOperatorMenuItem;
    }
}
