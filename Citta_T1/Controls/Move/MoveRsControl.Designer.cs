
namespace Citta_T1.Controls.Move
{
    partial class MoveRsControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveRsControl));
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
            this.nameToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.textBox = new System.Windows.Forms.TextBox();
            this.helpToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.txtButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPinPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPinPictureBox)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // rightPictureBox
            // 
            this.rightPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("rightPictureBox.Image")));
            this.rightPictureBox.Location = new System.Drawing.Point(149, 2);
            this.rightPictureBox.Name = "rightPictureBox";
            this.rightPictureBox.Size = new System.Drawing.Size(19, 21);
            this.rightPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rightPictureBox.TabIndex = 1;
            this.rightPictureBox.TabStop = false;
            this.rightPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseDown);
            this.rightPictureBox.MouseEnter += new System.EventHandler(this.rightPictureBox_MouseEnter);
            this.rightPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseMove);
            this.rightPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseUp);
            // 
            // leftPicture
            // 
            this.leftPicture.Image = ((System.Drawing.Image)(resources.GetObject("leftPicture.Image")));
            this.leftPicture.Location = new System.Drawing.Point(15, 1);
            this.leftPicture.Name = "leftPicture";
            this.leftPicture.Size = new System.Drawing.Size(18, 23);
            this.leftPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.leftPicture.TabIndex = 0;
            this.leftPicture.TabStop = false;
            this.leftPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseDown);
            this.leftPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseMove);
            this.leftPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseUp);
            // 
            // leftPinPictureBox
            // 
            this.leftPinPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leftPinPictureBox.Location = new System.Drawing.Point(4, 11);
            this.leftPinPictureBox.Name = "leftPinPictureBox";
            this.leftPinPictureBox.Size = new System.Drawing.Size(5, 5);
            this.leftPinPictureBox.TabIndex = 3;
            this.leftPinPictureBox.TabStop = false;
            // 
            // rightPinPictureBox
            // 
            this.rightPinPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rightPinPictureBox.Location = new System.Drawing.Point(171, 11);
            this.rightPinPictureBox.Name = "rightPinPictureBox";
            this.rightPinPictureBox.Size = new System.Drawing.Size(5, 5);
            this.rightPinPictureBox.TabIndex = 4;
            this.rightPinPictureBox.TabStop = false;
            this.rightPinPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rightPinPictureBox_MouseDown);

            this.rightPinPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rightPinPictureBox_MouseMove);
            this.rightPinPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rightPinPictureBox_MouseUp);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionMenuItem,
            this.RenameMenuItem,
            this.RemarkMenuItem,
            this.RunMenuItem,
            this.ErrorLogMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(181, 146);
            // 
            // OptionMenuItem
            // 
            this.OptionMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.OptionMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.OptionMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.OptionMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.OptionMenuItem.Name = "OptionMenuItem";
            this.OptionMenuItem.Size = new System.Drawing.Size(180, 24);
            this.OptionMenuItem.Text = "设置";
            this.OptionMenuItem.Click += new System.EventHandler(this.OptionMenuItem_Click);
            // 
            // RenameMenuItem
            // 
            this.RenameMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.RenameMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.RenameMenuItem.Name = "RenameMenuItem";
            this.RenameMenuItem.Size = new System.Drawing.Size(180, 24);
            this.RenameMenuItem.Text = "重命名";
            this.RenameMenuItem.Click += new System.EventHandler(this.RenameMenuItem_Click);
            // 
            // RemarkMenuItem
            // 
            this.RemarkMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.RemarkMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.RemarkMenuItem.Name = "RemarkMenuItem";
            this.RemarkMenuItem.Size = new System.Drawing.Size(180, 24);
            this.RemarkMenuItem.Text = "备注";
            this.RemarkMenuItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // RunMenuItem
            // 
            this.RunMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.RunMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.RunMenuItem.Name = "RunMenuItem";
            this.RunMenuItem.Size = new System.Drawing.Size(180, 24);
            this.RunMenuItem.Text = "运行到此";
            this.RunMenuItem.ToolTipText = "将执行模型到本阶段为止";
            // 
            // ErrorLogMenuItem
            // 
            this.ErrorLogMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.ErrorLogMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.ErrorLogMenuItem.Name = "ErrorLogMenuItem";
            this.ErrorLogMenuItem.Size = new System.Drawing.Size(180, 24);
            this.ErrorLogMenuItem.Text = "异常日志";
            // 
            // textBox
            // 
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBox.Location = new System.Drawing.Point(37, 1);
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(111, 23);
            this.textBox.TabIndex = 5;
            this.textBox.Text = "连接算子";
            this.textBox.Visible = false;
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.textBox.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // txtButton
            // 
            this.txtButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.txtButton.FlatAppearance.BorderSize = 0;
            this.txtButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtButton.Location = new System.Drawing.Point(37, 1);
            this.txtButton.Margin = new System.Windows.Forms.Padding(2);
            this.txtButton.Name = "txtButton";
            this.txtButton.Size = new System.Drawing.Size(110, 25);
            this.txtButton.TabIndex = 8;
            this.txtButton.Text = "button1";
            this.txtButton.UseVisualStyleBackColor = true;
            this.txtButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TxtButton_MouseDown);
            this.txtButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseMove);
            this.txtButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseUp);
            // 
            // MoveRsControl
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.txtButton);
            this.Controls.Add(this.textBox);
            //this.Controls.Add(this.rightPinPictureBox);
            //this.Controls.Add(this.leftPinPictureBox);
            this.Controls.Add(this.rightPictureBox);
            this.Controls.Add(this.leftPicture);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "MoveRsControl";
            this.Size = new System.Drawing.Size(185, 25);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveRsControl_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MoveOpControl_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPinPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPinPictureBox)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
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
        public System.Windows.Forms.ToolTip nameToolTip;
        public System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.ToolTip helpToolTip;
        public System.Windows.Forms.Button txtButton;
    }
}
