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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.YQHelp = new System.Windows.Forms.Label();
            this.addYQLabel = new System.Windows.Forms.Label();
            this.manageYQPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.newEventPanel.SuspendLayout();
            this.backPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Text = "网站侦察兵";
            // 
            // backPanel
            // 
            this.backPanel.Size = new System.Drawing.Size(179, 305);
            // 
            // manageButtonPanel
            // 
            this.manageButtonPanel.Size = new System.Drawing.Size(177, 271);
            // 
            // helpInfoLable
            // 
            this.helpInfoLable.Click += new System.EventHandler(this.HelpInfoLable_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(15, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "舆情侦察兵(施工中)";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.manageYQPanel);
            this.panel1.Location = new System.Drawing.Point(3, 372);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(179, 285);
            this.panel1.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.YQHelp);
            this.panel3.Controls.Add(this.addYQLabel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(177, 38);
            this.panel3.TabIndex = 0;
            // 
            // YQHelp
            // 
            this.YQHelp.AutoSize = true;
            this.YQHelp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.YQHelp.ForeColor = System.Drawing.SystemColors.GrayText;
            this.YQHelp.Location = new System.Drawing.Point(95, 11);
            this.YQHelp.Name = "YQHelp";
            this.YQHelp.Size = new System.Drawing.Size(75, 14);
            this.YQHelp.TabIndex = 1;
            this.YQHelp.Text = "+帮助说明";
            this.YQHelp.Click += new System.EventHandler(this.YQHelp_Click);
            // 
            // addYQLabel
            // 
            this.addYQLabel.AutoSize = true;
            this.addYQLabel.Font = new System.Drawing.Font("宋体", 10.5F);
            this.addYQLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.addYQLabel.Location = new System.Drawing.Point(12, 11);
            this.addYQLabel.Name = "addYQLabel";
            this.addYQLabel.Size = new System.Drawing.Size(70, 14);
            this.addYQLabel.TabIndex = 0;
            this.addYQLabel.Text = "+新建任务";
            this.addYQLabel.Click += new System.EventHandler(this.AddYQLable_Click);
            // 
            // manageYQPanel
            // 
            this.manageYQPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.manageYQPanel.Location = new System.Drawing.Point(0, 38);
            this.manageYQPanel.Name = "manageYQPanel";
            this.manageYQPanel.Size = new System.Drawing.Size(177, 245);
            this.manageYQPanel.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(4, 340);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(177, 30);
            this.panel2.TabIndex = 6;
            // 
            // WebsiteFeatureDetectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "WebsiteFeatureDetectionControl";
            this.Load += new System.EventHandler(this.WebsiteFeatureDetectionControl_Load);
            this.Controls.SetChildIndex(this.titleLabel, 0);
            this.Controls.SetChildIndex(this.backPanel, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.newEventPanel.ResumeLayout(false);
            this.newEventPanel.PerformLayout();
            this.backPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel manageYQPanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label YQHelp;
        private System.Windows.Forms.Label addYQLabel;
    }
}
