namespace C2.Controls.Left
{
    partial class ManualControl
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
            this.ItemLabel = new System.Windows.Forms.Label();
            this.ItemPanel = new System.Windows.Forms.Panel();
            this.PaintPanel = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ItemPanel.SuspendLayout();
            this.PaintPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ItemLabel
            // 
            this.ItemLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ItemLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ItemLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ItemLabel.Location = new System.Drawing.Point(0, 0);
            this.ItemLabel.Name = "ItemLabel";
            this.ItemLabel.Size = new System.Drawing.Size(187, 30);
            this.ItemLabel.TabIndex = 0;
            this.ItemLabel.Text = "我的战术手册";
            this.ItemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ItemPanel
            // 
            this.ItemPanel.Controls.Add(this.ItemLabel);
            this.ItemPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ItemPanel.Location = new System.Drawing.Point(0, 0);
            this.ItemPanel.Name = "ItemPanel";
            this.ItemPanel.Size = new System.Drawing.Size(185, 30);
            this.ItemPanel.TabIndex = 1;
            // 
            // PaintPanel
            // 
            this.PaintPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PaintPanel.AutoScroll = true;
            this.PaintPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PaintPanel.BackColor = System.Drawing.Color.White;
            this.PaintPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PaintPanel.Controls.Add(this.textBox1);
            this.PaintPanel.Location = new System.Drawing.Point(3, 35);
            this.PaintPanel.Name = "PaintPanel";
            this.PaintPanel.Size = new System.Drawing.Size(179, 621);
            this.PaintPanel.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.textBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ShortcutsEnabled = false;
            this.textBox1.Size = new System.Drawing.Size(177, 236);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "\r\n没有安装<<战术手册>>插件包\r\n\r\n联系售后群\r\n\r\n获取最新战术手册安装包\r\n\r\n本活动与苹果公司无关";
            // 
            // ManualControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ItemPanel);
            this.Controls.Add(this.PaintPanel);
            this.DoubleBuffered = true;
            this.Name = "ManualControl";
            this.Size = new System.Drawing.Size(185, 660);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ManualControl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ManualControl_MouseDown);
            this.ItemPanel.ResumeLayout(false);
            this.PaintPanel.ResumeLayout(false);
            this.PaintPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.Label ItemLabel;
        protected System.Windows.Forms.Panel ItemPanel;
        protected System.Windows.Forms.Panel PaintPanel;
        protected System.Windows.Forms.ToolTip toolTip1;
        protected System.Windows.Forms.TextBox textBox1;
    }
}
