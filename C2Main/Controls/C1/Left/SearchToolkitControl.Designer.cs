namespace C2.Controls.C1.Left
{
    partial class SearchToolkitControl
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
            this.newEventPanel.SuspendLayout();
            this.backPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Location = new System.Drawing.Point(3, 0);
            this.titleLabel.Size = new System.Drawing.Size(179, 30);
            this.titleLabel.Text = "全文&&胶水";
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
            // addTaskLabel
            // 
            this.addTaskLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AddTaskLabel_MouseClick);
            // 
            // helpInfoLable
            // 
            this.helpInfoLable.Click += new System.EventHandler(this.HelpInfoLable_Click);
            // 
            // SearchToolkitControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SearchToolkitControl";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.Resize += new System.EventHandler(this.SearchToolkitControl_Resize);
            this.newEventPanel.ResumeLayout(false);
            this.newEventPanel.PerformLayout();
            this.backPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
