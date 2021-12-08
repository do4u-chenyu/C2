namespace C2.Controls.C1.Left
{
    partial class CastleBravoControl
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
            this.hashTitleLabel = new System.Windows.Forms.Label();
            this.newEventPanel.SuspendLayout();
            this.backPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Location = new System.Drawing.Point(3, 0);
            this.titleLabel.Size = new System.Drawing.Size(179, 30);
            this.titleLabel.Text = "喝彩城堡";
            // 
            // backPanel
            // 
            this.backPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.backPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.backPanel.Location = new System.Drawing.Point(3, 330);
            this.backPanel.Size = new System.Drawing.Size(179, 330);
            // 
            // manageButtonPanel
            // 
            this.manageButtonPanel.Size = new System.Drawing.Size(177, 296);
            // 
            // helpInfoLable
            // 
            this.helpInfoLable.Click += new System.EventHandler(this.HelpInfoLable_Click);
            // 
            // hashTitleLabel
            // 
            this.hashTitleLabel.AutoSize = true;
            this.hashTitleLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.hashTitleLabel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.hashTitleLabel.Location = new System.Drawing.Point(44, 303);
            this.hashTitleLabel.Name = "hashTitleLabel";
            this.hashTitleLabel.Size = new System.Drawing.Size(98, 22);
            this.hashTitleLabel.TabIndex = 5;
            this.hashTitleLabel.Text = "Hash彩虹表";
            // 
            // CastleBravoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hashTitleLabel);
            this.Name = "CastleBravoControl";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.Resize += new System.EventHandler(this.CastleBravoControl_Resize);
            this.Controls.SetChildIndex(this.titleLabel, 0);
            this.Controls.SetChildIndex(this.backPanel, 0);
            this.Controls.SetChildIndex(this.hashTitleLabel, 0);
            this.newEventPanel.ResumeLayout(false);
            this.newEventPanel.PerformLayout();
            this.backPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label hashTitleLabel;
    }
}
