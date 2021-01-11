namespace C2.Controls.Left
{
    partial class IAOModelControl
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
            this.IaoModelPanel = new System.Windows.Forms.Panel();
            this.ItemLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IaoModelPanel
            // 
            this.IaoModelPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IaoModelPanel.AutoScroll = true;
            this.IaoModelPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.IaoModelPanel.BackColor = System.Drawing.Color.White;
            this.IaoModelPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IaoModelPanel.Location = new System.Drawing.Point(3, 35);
            this.IaoModelPanel.Name = "IaoModelPanel";
            this.IaoModelPanel.Size = new System.Drawing.Size(179, 621);
            this.IaoModelPanel.TabIndex = 1;
            // 
            // ItemLabel
            // 
            this.ItemLabel.BackColor = System.Drawing.Color.White;
            this.ItemLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.ItemLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ItemLabel.Location = new System.Drawing.Point(0, 0);
            this.ItemLabel.Name = "ItemLabel";
            this.ItemLabel.Size = new System.Drawing.Size(187, 30);
            this.ItemLabel.TabIndex = 2;
            this.ItemLabel.Text = "IAO实验室";
            this.ItemLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IAOModelControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ItemLabel);
            this.Controls.Add(this.IaoModelPanel);
            this.Name = "IAOModelControl";
            this.Size = new System.Drawing.Size(185, 660);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.IAOModelControl_Paint);
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel IaoModelPanel;
        private System.Windows.Forms.Label ItemLabel;
    }
}
