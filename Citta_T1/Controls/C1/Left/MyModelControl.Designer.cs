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
            this.AddModelButton = new System.Windows.Forms.Button();
            this.ItemLabel = new System.Windows.Forms.Label();
            this.MyModelPaintPanel = new System.Windows.Forms.Panel();
            this.ItemPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ItemPanel
            // 
            this.ItemPanel.Controls.Add(this.AddModelButton);
            this.ItemPanel.Controls.Add(this.ItemLabel);
            this.ItemPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ItemPanel.Location = new System.Drawing.Point(0, 0);
            this.ItemPanel.Name = "ItemPanel";
            this.ItemPanel.Size = new System.Drawing.Size(185, 30);
            this.ItemPanel.TabIndex = 1;
            // 
            // MyModelCreateButton
            // 
            this.AddModelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AddModelButton.BackColor = System.Drawing.Color.Transparent;
            this.AddModelButton.BackgroundImage = global::C2.Properties.Resources.add;
            this.AddModelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.AddModelButton.FlatAppearance.BorderSize = 0;
            this.AddModelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddModelButton.Location = new System.Drawing.Point(124, 2);
            this.AddModelButton.Name = "AddModelButton";
            this.AddModelButton.Size = new System.Drawing.Size(25, 25);
            this.AddModelButton.TabIndex = 1;
            this.AddModelButton.UseVisualStyleBackColor = false;
            this.AddModelButton.Click += new System.EventHandler(this.AddModelButton_Click);
            this.AddModelButton.MouseHover += new System.EventHandler(this.AddModelButton_MouseHover);
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
            this.ItemLabel.Text = "我的模型";
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
        private System.Windows.Forms.Button AddModelButton;
    }
}
