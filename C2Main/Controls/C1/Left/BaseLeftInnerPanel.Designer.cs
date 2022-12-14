namespace C2.Controls.C1.Left
{
    partial class BaseLeftInnerPanel
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
            this.newEventPanel = new System.Windows.Forms.Panel();
            this.helpInfoLable = new System.Windows.Forms.Label();
            this.addTaskLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.backPanel = new System.Windows.Forms.Panel();
            this.manageButtonPanel = new System.Windows.Forms.Panel();
            this.newEventPanel.SuspendLayout();
            this.backPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // newEventPanel
            // 
            this.newEventPanel.Controls.Add(this.helpInfoLable);
            this.newEventPanel.Controls.Add(this.addTaskLabel);
            this.newEventPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.newEventPanel.Location = new System.Drawing.Point(0, 0);
            this.newEventPanel.Name = "newEventPanel";
            this.newEventPanel.Size = new System.Drawing.Size(177, 32);
            this.newEventPanel.TabIndex = 0;
            // 
            // helpInfoLable
            // 
            this.helpInfoLable.AutoSize = true;
            this.helpInfoLable.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.helpInfoLable.ForeColor = System.Drawing.SystemColors.GrayText;
            this.helpInfoLable.Location = new System.Drawing.Point(95, 10);
            this.helpInfoLable.Name = "helpInfoLable";
            this.helpInfoLable.Size = new System.Drawing.Size(75, 14);
            this.helpInfoLable.TabIndex = 1;
            this.helpInfoLable.Text = "+帮助说明";
            // 
            // addTaskLabel
            // 
            this.addTaskLabel.AutoSize = true;
            this.addTaskLabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addTaskLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.addTaskLabel.Location = new System.Drawing.Point(12, 10);
            this.addTaskLabel.Name = "addTaskLabel";
            this.addTaskLabel.Size = new System.Drawing.Size(70, 14);
            this.addTaskLabel.TabIndex = 0;
            this.addTaskLabel.Text = "+新建任务";
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(185, 30);
            this.titleLabel.TabIndex = 2;
            this.titleLabel.Text = "面板标题";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backPanel
            // 
            this.backPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.backPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.backPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.backPanel.Controls.Add(this.manageButtonPanel);
            this.backPanel.Controls.Add(this.newEventPanel);
            this.backPanel.Location = new System.Drawing.Point(3, 35);
            this.backPanel.Name = "backPanel";
            this.backPanel.Size = new System.Drawing.Size(179, 621);
            this.backPanel.TabIndex = 3;
            // 
            // manageButtonPanel
            // 
            this.manageButtonPanel.AutoScroll = true;
            this.manageButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manageButtonPanel.Font = new System.Drawing.Font("宋体", 9F);
            this.manageButtonPanel.Location = new System.Drawing.Point(0, 32);
            this.manageButtonPanel.Name = "manageButtonPanel";
            this.manageButtonPanel.Size = new System.Drawing.Size(177, 587);
            this.manageButtonPanel.TabIndex = 1;
            // 
            // BaseLeftInnerPanel
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.backPanel);
            this.Controls.Add(this.titleLabel);
            this.DoubleBuffered = true;
            this.Name = "BaseLeftInnerPanel";
            this.Size = new System.Drawing.Size(185, 660);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BaseLeftInnerPanel_Paint);
            this.newEventPanel.ResumeLayout(false);
            this.newEventPanel.PerformLayout();
            this.backPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.Panel newEventPanel;
        protected System.Windows.Forms.Label titleLabel;
        protected System.Windows.Forms.Panel backPanel;
        protected System.Windows.Forms.Panel manageButtonPanel;
        protected System.Windows.Forms.Label addTaskLabel;
        protected System.Windows.Forms.Label helpInfoLable;
    }
}
