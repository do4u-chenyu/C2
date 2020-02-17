namespace Citta_T1.Controls
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
            this.textButton = new System.Windows.Forms.Button();
            this.leftPicture = new System.Windows.Forms.PictureBox();
            this.leftPinPictureBox = new System.Windows.Forms.PictureBox();
            this.rightPinPictureBox = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.菜单1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.菜单2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.备注ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运行到此ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.异常日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
            this.rightPictureBox.Location = new System.Drawing.Point(321, 29);
            this.rightPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.rightPictureBox.Name = "rightPictureBox";
            this.rightPictureBox.Size = new System.Drawing.Size(28, 32);
            this.rightPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rightPictureBox.TabIndex = 1;
            this.rightPictureBox.TabStop = false;
            this.rightPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.rightPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.rightPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            // 
            // textButton
            // 
            this.textButton.AllowDrop = true;
            this.textButton.FlatAppearance.BorderSize = 0;
            this.textButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.textButton.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textButton.Location = new System.Drawing.Point(93, 21);
            this.textButton.Margin = new System.Windows.Forms.Padding(4);
            this.textButton.Name = "textButton";
            this.textButton.Size = new System.Drawing.Size(223, 53);
            this.textButton.TabIndex = 2;
            this.textButton.Text = "连接算子";
            this.textButton.UseVisualStyleBackColor = true;
            this.textButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.textButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.textButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            // 
            // leftPicture
            // 
            this.leftPicture.Image = ((System.Drawing.Image)(resources.GetObject("leftPicture.Image")));
            this.leftPicture.Location = new System.Drawing.Point(51, 29);
            this.leftPicture.Margin = new System.Windows.Forms.Padding(4);
            this.leftPicture.Name = "leftPicture";
            this.leftPicture.Size = new System.Drawing.Size(36, 45);
            this.leftPicture.TabIndex = 0;
            this.leftPicture.TabStop = false;
            this.leftPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.leftPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.leftPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            // 
            // leftPinPictureBox
            // 
            this.leftPinPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leftPinPictureBox.Location = new System.Drawing.Point(29, 39);
            this.leftPinPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.leftPinPictureBox.Name = "leftPinPictureBox";
            this.leftPinPictureBox.Size = new System.Drawing.Size(14, 14);
            this.leftPinPictureBox.TabIndex = 3;
            this.leftPinPictureBox.TabStop = false;
            this.leftPinPictureBox.MouseEnter += new System.EventHandler(this.PinOpPictureBox_MouseEnter);
            this.leftPinPictureBox.MouseLeave += new System.EventHandler(this.PinOpPictureBox_MouseLeave);
            // 
            // rightPinPictureBox
            // 
            this.rightPinPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rightPinPictureBox.Location = new System.Drawing.Point(365, 39);
            this.rightPinPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.rightPinPictureBox.Name = "rightPinPictureBox";
            this.rightPinPictureBox.Size = new System.Drawing.Size(14, 14);
            this.rightPinPictureBox.TabIndex = 4;
            this.rightPinPictureBox.TabStop = false;
            this.rightPinPictureBox.MouseEnter += new System.EventHandler(this.PinOpPictureBox_MouseEnter);
            this.rightPinPictureBox.MouseLeave += new System.EventHandler(this.PinOpPictureBox_MouseLeave);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.菜单1ToolStripMenuItem,
            this.菜单2ToolStripMenuItem,
            this.备注ToolStripMenuItem,
            this.运行到此ToolStripMenuItem,
            this.异常日志ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(241, 241);
            // 
            // 菜单1ToolStripMenuItem
            // 
            this.菜单1ToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.菜单1ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("菜单1ToolStripMenuItem.BackgroundImage")));
            this.菜单1ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.菜单1ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.菜单1ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.菜单1ToolStripMenuItem.Name = "菜单1ToolStripMenuItem";
            this.菜单1ToolStripMenuItem.Size = new System.Drawing.Size(162, 34);
            this.菜单1ToolStripMenuItem.Text = "设置";
            // 
            // 菜单2ToolStripMenuItem
            // 
            this.菜单2ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("菜单2ToolStripMenuItem.BackgroundImage")));
            this.菜单2ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.菜单2ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.菜单2ToolStripMenuItem.Name = "菜单2ToolStripMenuItem";
            this.菜单2ToolStripMenuItem.Size = new System.Drawing.Size(162, 34);
            this.菜单2ToolStripMenuItem.Text = "重命名";
            this.菜单2ToolStripMenuItem.Click += new System.EventHandler(this.重命名ToolStripMenuItem_Click_1);
            // 
            // 备注ToolStripMenuItem
            // 
            this.备注ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("备注ToolStripMenuItem.BackgroundImage")));
            this.备注ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.备注ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.备注ToolStripMenuItem.Name = "备注ToolStripMenuItem";
            this.备注ToolStripMenuItem.Size = new System.Drawing.Size(162, 34);
            this.备注ToolStripMenuItem.Text = "备注";
            this.备注ToolStripMenuItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // 运行到此ToolStripMenuItem
            // 
            this.运行到此ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("运行到此ToolStripMenuItem.BackgroundImage")));
            this.运行到此ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.运行到此ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.运行到此ToolStripMenuItem.Name = "运行到此ToolStripMenuItem";
            this.运行到此ToolStripMenuItem.Size = new System.Drawing.Size(162, 34);
            this.运行到此ToolStripMenuItem.Text = "运行到此";
            this.运行到此ToolStripMenuItem.ToolTipText = "将执行模型到本阶段为止";
            // 
            // 异常日志ToolStripMenuItem
            // 
            this.异常日志ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("异常日志ToolStripMenuItem.BackgroundImage")));
            this.异常日志ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.异常日志ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.异常日志ToolStripMenuItem.Name = "异常日志ToolStripMenuItem";
            this.异常日志ToolStripMenuItem.Size = new System.Drawing.Size(162, 34);
            this.异常日志ToolStripMenuItem.Text = "异常日志";
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("删除ToolStripMenuItem.BackgroundImage")));
            this.删除ToolStripMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.删除ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(240, 34);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // MoveOpControl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.rightPinPictureBox);
            this.Controls.Add(this.leftPinPictureBox);
            this.Controls.Add(this.rightPictureBox);
            this.Controls.Add(this.textButton);
            this.Controls.Add(this.leftPicture);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MoveOpControl";
            this.Size = new System.Drawing.Size(411, 97);
            this.Load += new System.EventHandler(this.MoveOpControl_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPinPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPinPictureBox)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox rightPictureBox;
        private System.Windows.Forms.PictureBox leftPicture;
        public System.Windows.Forms.Button textButton;
        private System.Windows.Forms.PictureBox leftPinPictureBox;
        private System.Windows.Forms.PictureBox rightPinPictureBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 菜单1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 菜单2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 备注ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 运行到此ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 异常日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
