namespace C2.Controls.Left
{
    partial class MyModelControl
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
            this.ItemPanel = new System.Windows.Forms.Panel();
            this.ItemLabel = new System.Windows.Forms.Label();
            this.MyModelPaintPanel = new System.Windows.Forms.Panel();
            this.MyModelCreateButton = new System.Windows.Forms.Button();
            this.ItemPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ItemPanel
            // 
            this.ItemPanel.Controls.Add(this.MyModelCreateButton);
            this.ItemPanel.Controls.Add(this.ItemLabel);
            this.ItemPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ItemPanel.Location = new System.Drawing.Point(0, 0);
            this.ItemPanel.Name = "ItemPanel";
            this.ItemPanel.Size = new System.Drawing.Size(185, 30);
            this.ItemPanel.TabIndex = 1;
            // 
            // ItemLabel
            // 
            this.ItemLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ItemLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ItemLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ItemLabel.Location = new System.Drawing.Point(0, 0);
            this.ItemLabel.Name = "ItemLabel";
            this.ItemLabel.Size = new System.Drawing.Size(182, 30);
            this.ItemLabel.TabIndex = 0;
            this.ItemLabel.Text = "我的模型市场";
            this.ItemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MyModelPaintPanel
            // 
            this.MyModelPaintPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MyModelPaintPanel.AutoScroll = true;
            this.MyModelPaintPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MyModelPaintPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MyModelPaintPanel.Location = new System.Drawing.Point(3, 35);
            this.MyModelPaintPanel.Name = "MyModelPaintPanel";
            this.MyModelPaintPanel.Size = new System.Drawing.Size(179, 621);
            this.MyModelPaintPanel.TabIndex = 2;
            // 
            // MyModelCreateButton
            // 
            this.MyModelCreateButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MyModelCreateButton.BackColor = System.Drawing.Color.Transparent;
            this.MyModelCreateButton.BackgroundImage = global::C2.Properties.Resources.add;
            this.MyModelCreateButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.MyModelCreateButton.FlatAppearance.BorderSize = 0;
            this.MyModelCreateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MyModelCreateButton.Location = new System.Drawing.Point(141, 2);
            this.MyModelCreateButton.Name = "MyModelCreateButton";
            this.MyModelCreateButton.Size = new System.Drawing.Size(25, 25);
            this.MyModelCreateButton.TabIndex = 1;
            this.MyModelCreateButton.UseVisualStyleBackColor = false;
            // 
            // MyModelControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.MyModelPaintPanel);
            this.Controls.Add(this.ItemPanel);
            this.Name = "MyModelControl";
            this.Size = new System.Drawing.Size(185, 660);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MyModelControl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MyModelControl_MouseDown);
            this.ItemPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel ItemPanel;
        private System.Windows.Forms.Label ItemLabel;
        private System.Windows.Forms.Panel MyModelPaintPanel;
        private System.Windows.Forms.Button MyModelCreateButton;
    }
}
