namespace C2.Controls.Left
{
    partial class MyMindMapControl
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
            this.AddMindMapButton = new System.Windows.Forms.Button();
            this.MindMapPaintPanel = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ItemPanel.SuspendLayout();
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
            this.ItemLabel.Text = "我的业务视图";
            this.ItemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ItemPanel
            // 
            this.ItemPanel.Controls.Add(this.AddMindMapButton);
            this.ItemPanel.Controls.Add(this.ItemLabel);
            this.ItemPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ItemPanel.Location = new System.Drawing.Point(0, 0);
            this.ItemPanel.Name = "ItemPanel";
            this.ItemPanel.Size = new System.Drawing.Size(185, 30);
            this.ItemPanel.TabIndex = 1;
            // 
            // AddMindMapButton
            // 
            this.AddMindMapButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AddMindMapButton.BackColor = System.Drawing.Color.Transparent;
            this.AddMindMapButton.BackgroundImage = global::C2.Properties.Resources.add;
            this.AddMindMapButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.AddMindMapButton.FlatAppearance.BorderSize = 0;
            this.AddMindMapButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddMindMapButton.Location = new System.Drawing.Point(144, 3);
            this.AddMindMapButton.Name = "AddMindMapButton";
            this.AddMindMapButton.Size = new System.Drawing.Size(25, 25);
            this.AddMindMapButton.TabIndex = 2;
            this.toolTip1.SetToolTip(this.AddMindMapButton, "导入业务视图");
            this.AddMindMapButton.UseVisualStyleBackColor = false;
            this.AddMindMapButton.Click += new System.EventHandler(this.AddMindMapButton_Click);
            // 
            // MindMapPaintPanel
            // 
            this.MindMapPaintPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MindMapPaintPanel.AutoScroll = true;
            this.MindMapPaintPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MindMapPaintPanel.BackColor = System.Drawing.Color.White;
            this.MindMapPaintPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MindMapPaintPanel.Location = new System.Drawing.Point(3, 35);
            this.MindMapPaintPanel.Name = "MindMapPaintPanel";
            this.MindMapPaintPanel.Size = new System.Drawing.Size(179, 621);
            this.MindMapPaintPanel.TabIndex = 2;
            // 
            // MyMindMapControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ItemPanel);
            this.Controls.Add(this.MindMapPaintPanel);
            this.Name = "MyMindMapControl";
            this.Size = new System.Drawing.Size(185, 660);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MindMapModelControl_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MindMapModelControl_MouseDown);
            this.ItemPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label ItemLabel;
        private System.Windows.Forms.Panel ItemPanel;
        private System.Windows.Forms.Panel MindMapPaintPanel;
        private System.Windows.Forms.Button AddMindMapButton;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
