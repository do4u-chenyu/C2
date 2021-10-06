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
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.OneWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.behinderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BypassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PHPEvalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GodzillaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.变体1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.变体2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.变体3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.变体4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.变体5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.变体6ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.变体7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.变体8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.变体9ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripSeparator1,
            this.toolStripButton1});
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
            // toolStripButton1
            // 
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OneWordToolStripMenuItem,
            this.behinderToolStripMenuItem,
            this.BypassToolStripMenuItem,
            this.PHPEvalToolStripMenuItem,
            this.GodzillaToolStripMenuItem});
            this.toolStripButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(57, 22);
            this.toolStripButton1.Text = "特洛伊";
            this.toolStripButton1.ToolTipText = "特洛伊样本集合";
            // 
            // OneWordToolStripMenuItem
            // 
            this.OneWordToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.变体1ToolStripMenuItem,
            this.变体2ToolStripMenuItem,
            this.变体3ToolStripMenuItem,
            this.变体4ToolStripMenuItem,
            this.变体5ToolStripMenuItem,
            this.变体6ToolStripMenuItem,
            this.变体7ToolStripMenuItem,
            this.变体8ToolStripMenuItem,
            this.变体9ToolStripMenuItem});
            this.OneWordToolStripMenuItem.Name = "OneWordToolStripMenuItem";
            this.OneWordToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.OneWordToolStripMenuItem.Text = "一句话变种";
            // 
            // behinderToolStripMenuItem
            // 
            this.behinderToolStripMenuItem.Name = "behinderToolStripMenuItem";
            this.behinderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.behinderToolStripMenuItem.Text = "三代冰蝎";
            this.behinderToolStripMenuItem.ToolTipText = "与三代冰蝎配套的Trojan";
            // 
            // BypassToolStripMenuItem
            // 
            this.BypassToolStripMenuItem.Name = "BypassToolStripMenuItem";
            this.BypassToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.BypassToolStripMenuItem.Text = "凯撒变种";
            this.BypassToolStripMenuItem.ToolTipText = "凯撒加密Payload引导段";
            // 
            // PHPEvalToolStripMenuItem
            // 
            this.PHPEvalToolStripMenuItem.Name = "PHPEvalToolStripMenuItem";
            this.PHPEvalToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.PHPEvalToolStripMenuItem.Text = "通用型";
            this.PHPEvalToolStripMenuItem.ToolTipText = "最常见的 php eval一句话";
            // 
            // GodzillaToolStripMenuItem
            // 
            this.GodzillaToolStripMenuItem.Name = "GodzillaToolStripMenuItem";
            this.GodzillaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.GodzillaToolStripMenuItem.Text = "哥斯拉";
            this.GodzillaToolStripMenuItem.ToolTipText = "与Godzilla配套的Trojan";
            // 
            // 变体1ToolStripMenuItem
            // 
            this.变体1ToolStripMenuItem.Name = "变体1ToolStripMenuItem";
            this.变体1ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.变体1ToolStripMenuItem.Text = "变种1";
            // 
            // 变体2ToolStripMenuItem
            // 
            this.变体2ToolStripMenuItem.Name = "变体2ToolStripMenuItem";
            this.变体2ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.变体2ToolStripMenuItem.Text = "变种2";
            // 
            // 变体3ToolStripMenuItem
            // 
            this.变体3ToolStripMenuItem.Name = "变体3ToolStripMenuItem";
            this.变体3ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.变体3ToolStripMenuItem.Text = "变种3";
            // 
            // 变体4ToolStripMenuItem
            // 
            this.变体4ToolStripMenuItem.Name = "变体4ToolStripMenuItem";
            this.变体4ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.变体4ToolStripMenuItem.Text = "变种4";
            // 
            // 变体5ToolStripMenuItem
            // 
            this.变体5ToolStripMenuItem.Name = "变体5ToolStripMenuItem";
            this.变体5ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.变体5ToolStripMenuItem.Text = "变种5";
            // 
            // 变体6ToolStripMenuItem
            // 
            this.变体6ToolStripMenuItem.Name = "变体6ToolStripMenuItem";
            this.变体6ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.变体6ToolStripMenuItem.Text = "变种6";
            // 
            // 变体7ToolStripMenuItem
            // 
            this.变体7ToolStripMenuItem.Name = "变体7ToolStripMenuItem";
            this.变体7ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.变体7ToolStripMenuItem.Text = "变种7";
            // 
            // 变体8ToolStripMenuItem
            // 
            this.变体8ToolStripMenuItem.Name = "变体8ToolStripMenuItem";
            this.变体8ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.变体8ToolStripMenuItem.Text = "变种8";
            // 
            // 变体9ToolStripMenuItem
            // 
            this.变体9ToolStripMenuItem.Name = "变体9ToolStripMenuItem";
            this.变体9ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.变体9ToolStripMenuItem.Text = "变种9";
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
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem OneWordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 变体1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 变体2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 变体3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 变体4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 变体5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 变体6ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 变体7ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 变体8ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 变体9ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem behinderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BypassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PHPEvalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GodzillaToolStripMenuItem;
    }
}