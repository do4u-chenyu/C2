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
            this.ImportButton = new System.Windows.Forms.Button();
            this.ItemPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ItemLabel
            // 
            this.ItemLabel.Size = new System.Drawing.Size(169, 30);
            this.ItemLabel.Text = "  我的分析笔记";
            // 
            // ItemPanel
            // 
            this.ItemPanel.Controls.Add(this.ImportButton);
            this.ItemPanel.Controls.SetChildIndex(this.ItemLabel, 0);
            this.ItemPanel.Controls.SetChildIndex(this.ImportButton, 0);
            // 
            // ImportButton
            // 
            this.ImportButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ImportButton.BackColor = System.Drawing.Color.Transparent;
            this.ImportButton.BackgroundImage = global::C2.Properties.Resources.add;
            this.ImportButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ImportButton.FlatAppearance.BorderSize = 0;
            this.ImportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImportButton.Location = new System.Drawing.Point(144, 3);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(25, 25);
            this.ImportButton.TabIndex = 2;
            this.toolTip1.SetToolTip(this.ImportButton, "导入分析笔记");
            this.ImportButton.UseVisualStyleBackColor = false;
            this.ImportButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // MyMindMapControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Name = "MyMindMapControl";
            this.ItemPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        protected System.Windows.Forms.Button ImportButton;
        #endregion
    }
}
