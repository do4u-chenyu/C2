namespace C2.Business.CastleBravo.WebShellTool
{
    partial class WebShellManageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebShellManageForm));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EnterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addShellMenu = new System.Windows.Forms.ToolStripLabel();
            this.settingMenu = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.LV = new System.Windows.Forms.ListView();
            this.lvAddTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvRemark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvShellUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvPass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvDB = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EnterToolStripMenuItem,
            this.RemoveToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // EnterToolStripMenuItem
            // 
            this.EnterToolStripMenuItem.Name = "EnterToolStripMenuItem";
            this.EnterToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.EnterToolStripMenuItem.Text = "进入";
            this.EnterToolStripMenuItem.Click += new System.EventHandler(this.EnterToolStripMenuItem_Click);
            // 
            // RemoveToolStripMenuItem
            // 
            this.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem";
            this.RemoveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.RemoveToolStripMenuItem.Text = "移除";
            this.RemoveToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addShellMenu,
            this.settingMenu,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1047, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addShellMenu
            // 
            this.addShellMenu.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addShellMenu.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addShellMenu.Name = "addShellMenu";
            this.addShellMenu.Size = new System.Drawing.Size(32, 22);
            this.addShellMenu.Text = "添加";
            this.addShellMenu.Click += new System.EventHandler(this.AddShellMenu_Click);
            // 
            // settingMenu
            // 
            this.settingMenu.Name = "settingMenu";
            this.settingMenu.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // LV
            // 
            this.LV.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.LV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvAddTime,
            this.lvRemark,
            this.lvShellUrl,
            this.lvPass,
            this.lvType,
            this.lvVersion,
            this.lvDB});
            this.LV.ContextMenuStrip = this.contextMenuStrip1;
            this.LV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV.FullRowSelect = true;
            this.LV.GridLines = true;
            this.LV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LV.HideSelection = false;
            this.LV.HoverSelection = true;
            this.LV.Location = new System.Drawing.Point(0, 25);
            this.LV.MultiSelect = false;
            this.LV.Name = "LV";
            this.LV.ShowGroups = false;
            this.LV.ShowItemToolTips = true;
            this.LV.Size = new System.Drawing.Size(1047, 425);
            this.LV.TabIndex = 3;
            this.LV.UseCompatibleStateImageBehavior = false;
            this.LV.View = System.Windows.Forms.View.Details;
            // 
            // lvAddTime
            // 
            this.lvAddTime.Text = "创建时间";
            this.lvAddTime.Width = 130;
            // 
            // lvRemark
            // 
            this.lvRemark.Text = "备注";
            this.lvRemark.Width = 95;
            // 
            // lvShellUrl
            // 
            this.lvShellUrl.Text = "目标Url";
            this.lvShellUrl.Width = 325;
            // 
            // lvPass
            // 
            this.lvPass.Text = "连接密码";
            this.lvPass.Width = 100;
            // 
            // lvType
            // 
            this.lvType.Text = "Trojan类型";
            this.lvType.Width = 100;
            // 
            // lvVersion
            // 
            this.lvVersion.Text = "客户端版本";
            this.lvVersion.Width = 135;
            // 
            // lvDB
            // 
            this.lvDB.Text = "数据库配置";
            this.lvDB.Width = 160;
            // 
            // WebShellManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 450);
            this.Controls.Add(this.LV);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WebShellManageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebShell模拟器";
            this.Load += new System.EventHandler(this.WebShellManageForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel addShellMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ListView LV;
        private System.Windows.Forms.ColumnHeader lvRemark;
        private System.Windows.Forms.ColumnHeader lvShellUrl;
        private System.Windows.Forms.ColumnHeader lvType;
        private System.Windows.Forms.ColumnHeader lvVersion;
        private System.Windows.Forms.ColumnHeader lvAddTime;
        private System.Windows.Forms.ToolStripMenuItem RemoveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EnterToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel settingMenu;
        private System.Windows.Forms.ColumnHeader lvPass;
        private System.Windows.Forms.ColumnHeader lvDB;
    }
}