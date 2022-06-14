namespace C2.Controls.C1.Left
{
    partial class WebsiteFeatureDetectionControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.yqFeatureDetectionControl1 = new C2.Controls.C1.Left.YQFeatureDetectionControl();
            this.newEventPanel.SuspendLayout();
            this.backPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // newEventPanel
            // 
            this.newEventPanel.Size = new System.Drawing.Size(183, 32);
            // 
            // titleLabel
            // 
            this.titleLabel.Text = "网站侦察兵";
            // 
            // backPanel
            // 
            this.backPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.backPanel.AutoScroll = true;
            this.backPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.backPanel.Location = new System.Drawing.Point(0, 30);
            this.backPanel.Size = new System.Drawing.Size(185, 342);
            // 
            // manageButtonPanel
            // 
            this.manageButtonPanel.Size = new System.Drawing.Size(183, 308);
            // 
            // helpInfoLable
            // 
            this.helpInfoLable.Click += new System.EventHandler(this.HelpInfoLable_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.yqFeatureDetectionControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 372);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 288);
            this.panel1.TabIndex = 4;
            // 
            // yqFeatureDetectionControl1
            // 
            this.yqFeatureDetectionControl1.AutoScroll = true;
            this.yqFeatureDetectionControl1.BackColor = System.Drawing.Color.White;
            this.yqFeatureDetectionControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yqFeatureDetectionControl1.Location = new System.Drawing.Point(0, 0);
            this.yqFeatureDetectionControl1.Name = "yqFeatureDetectionControl1";
            this.yqFeatureDetectionControl1.Size = new System.Drawing.Size(185, 288);
            this.yqFeatureDetectionControl1.TabIndex = 0;
            // 
            // WebsiteFeatureDetectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "WebsiteFeatureDetectionControl";
            this.Load += new System.EventHandler(this.WebsiteFeatureDetectionControl_Load);
            this.Controls.SetChildIndex(this.titleLabel, 0);
            this.Controls.SetChildIndex(this.backPanel, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.newEventPanel.ResumeLayout(false);
            this.newEventPanel.PerformLayout();
            this.backPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public YQFeatureDetectionControl yqFeatureDetectionControl1;
    }
}
