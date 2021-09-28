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
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addShellMenu = new System.Windows.Forms.ToolStripLabel();
            this.saveShellMenu = new System.Windows.Forms.ToolStripLabel();
            this.settingMenu = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.helpMenu = new System.Windows.Forms.ToolStripLabel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.lvID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvShellUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvRemark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvAddTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EnterToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.RemoveToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 70);
            // 
            // EnterToolStripMenuItem
            // 
            this.EnterToolStripMenuItem.Name = "EnterToolStripMenuItem";
            this.EnterToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.EnterToolStripMenuItem.Text = "进入";
            this.EnterToolStripMenuItem.Click += new System.EventHandler(this.EnterToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.EditToolStripMenuItem.Text = "编辑";
            this.EditToolStripMenuItem.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
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
            this.saveShellMenu,
            this.settingMenu,
            this.toolStripSeparator1,
            this.helpMenu});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addShellMenu
            // 
            this.addShellMenu.Name = "addShellMenu";
            this.addShellMenu.Size = new System.Drawing.Size(32, 22);
            this.addShellMenu.Text = "添加";
            this.addShellMenu.Click += new System.EventHandler(this.AddShellMenu_Click);
            // 
            // saveShellMenu
            // 
            this.saveShellMenu.Name = "saveShellMenu";
            this.saveShellMenu.Size = new System.Drawing.Size(32, 22);
            this.saveShellMenu.Text = "保存";
            this.saveShellMenu.Click += new System.EventHandler(this.SaveShellMenu_Click);
            // 
            // settingMenu
            // 
            this.settingMenu.Name = "settingMenu";
            this.settingMenu.Size = new System.Drawing.Size(56, 22);
            this.settingMenu.Text = "版本配置";
            this.settingMenu.Click += new System.EventHandler(this.SettingMenu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // helpMenu
            // 
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(32, 22);
            this.helpMenu.Text = "帮助";
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvID,
            this.lvName,
            this.lvShellUrl,
            this.lvType,
            this.lvRemark,
            this.lvAddTime});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 25);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(800, 425);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // lvID
            // 
            this.lvID.Text = "ID";
            // 
            // lvName
            // 
            this.lvName.Text = "名称";
            this.lvName.Width = 113;
            // 
            // lvShellUrl
            // 
            this.lvShellUrl.Text = "ShellUrl";
            this.lvShellUrl.Width = 289;
            // 
            // lvType
            // 
            this.lvType.Text = "类型";
            this.lvType.Width = 79;
            // 
            // lvRemark
            // 
            this.lvRemark.Text = "备注";
            this.lvRemark.Width = 74;
            // 
            // lvAddTime
            // 
            this.lvAddTime.Text = "创建时间";
            this.lvAddTime.Width = 156;
            // 
            // WebShellManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WebShellManageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebShell管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WebShellManageForm_FormClosing);
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
        private System.Windows.Forms.ToolStripLabel saveShellMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel helpMenu;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader lvID;
        private System.Windows.Forms.ColumnHeader lvName;
        private System.Windows.Forms.ColumnHeader lvShellUrl;
        private System.Windows.Forms.ColumnHeader lvType;
        private System.Windows.Forms.ColumnHeader lvRemark;
        private System.Windows.Forms.ColumnHeader lvAddTime;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EnterToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel settingMenu;
    }
}