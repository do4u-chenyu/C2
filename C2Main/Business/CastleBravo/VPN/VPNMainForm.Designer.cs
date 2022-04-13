﻿
namespace C2.Business.CastleBravo.VPN
{
    partial class VPNMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VPNMainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.editDDB = new System.Windows.Forms.ToolStripDropDownButton();
            this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.批量添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查找ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkAliveDDB = new System.Windows.Forms.ToolStripDropDownButton();
            this.refreshAllShellMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.重新开始ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.继续上次ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.secondRefreshMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.infoCollectionMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.passwdBlastingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshStopMenu = new System.Windows.Forms.ToolStripLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.progressMenu = new System.Windows.Forms.ToolStripLabel();
            this.proxySettingMenu = new System.Windows.Forms.ToolStripLabel();
            this.LV = new System.Windows.Forms.ListView();
            this.lvAddTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvRemark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvPass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvMethod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvInfoCollection = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvCountry = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.CheckAliveSelectedItemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DDMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mysqlProbeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mysqlBlastingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置文件探针ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userMYD探针ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.随机探针ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ClearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveResultsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ItemCountSLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProxyEnableSLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.infoConfigStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.actionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.重新开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.继续上次ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新开始ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.继续上次ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(450, 270);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "施工中";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editDDB,
            this.checkAliveDDB,
            this.infoCollectionMenu,
            this.refreshStopMenu,
            this.progressBar,
            this.progressMenu,
            this.proxySettingMenu});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1153, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // editDDB
            // 
            this.editDDB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加ToolStripMenuItem,
            this.批量添加ToolStripMenuItem,
            this.查找ToolStripMenuItem});
            this.editDDB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.editDDB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editDDB.Name = "editDDB";
            this.editDDB.Size = new System.Drawing.Size(45, 22);
            this.editDDB.Text = "常规";
            this.editDDB.ToolTipText = "常规操作";
            // 
            // 添加ToolStripMenuItem
            // 
            this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
            this.添加ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.添加ToolStripMenuItem.Text = "单个添加";
            this.添加ToolStripMenuItem.Click += new System.EventHandler(this.添加ToolStripMenuItem_Click);
            // 
            // 批量添加ToolStripMenuItem
            // 
            this.批量添加ToolStripMenuItem.Name = "批量添加ToolStripMenuItem";
            this.批量添加ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.批量添加ToolStripMenuItem.Text = "批量添加";
            this.批量添加ToolStripMenuItem.Click += new System.EventHandler(this.批量添加ToolStripMenuItem_Click);
            // 
            // 查找ToolStripMenuItem
            // 
            this.查找ToolStripMenuItem.Name = "查找ToolStripMenuItem";
            this.查找ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.查找ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.查找ToolStripMenuItem.Text = "查找";
            this.查找ToolStripMenuItem.Click += new System.EventHandler(this.查找ToolStripMenuItem_Click);
            // 
            // checkAliveDDB
            // 
            this.checkAliveDDB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.checkAliveDDB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshAllShellMenu,
            this.secondRefreshMenu});
            this.checkAliveDDB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.checkAliveDDB.Image = ((System.Drawing.Image)(resources.GetObject("checkAliveDDB.Image")));
            this.checkAliveDDB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.checkAliveDDB.Name = "checkAliveDDB";
            this.checkAliveDDB.Size = new System.Drawing.Size(69, 22);
            this.checkAliveDDB.Text = "验活功能";
            // 
            // refreshAllShellMenu
            // 
            this.refreshAllShellMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重新开始ToolStripMenuItem1,
            this.继续上次ToolStripMenuItem1});
            this.refreshAllShellMenu.Name = "refreshAllShellMenu";
            this.refreshAllShellMenu.Size = new System.Drawing.Size(180, 22);
            this.refreshAllShellMenu.Text = "批量验活";
            // 
            // 重新开始ToolStripMenuItem1
            // 
            this.重新开始ToolStripMenuItem1.Name = "重新开始ToolStripMenuItem1";
            this.重新开始ToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.重新开始ToolStripMenuItem1.Text = "重新开始";
            this.重新开始ToolStripMenuItem1.Click += new System.EventHandler(this.重新开始_批量验活_ToolStripMenuItem_Click);
            // 
            // 继续上次ToolStripMenuItem1
            // 
            this.继续上次ToolStripMenuItem1.Name = "继续上次ToolStripMenuItem1";
            this.继续上次ToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.继续上次ToolStripMenuItem1.Text = "继续上次";
            this.继续上次ToolStripMenuItem1.Click += new System.EventHandler(this.继续上次_二刷不活_ToolStripMenuItem_Click);
            // 
            // secondRefreshMenu
            // 
            this.secondRefreshMenu.Name = "secondRefreshMenu";
            this.secondRefreshMenu.Size = new System.Drawing.Size(180, 22);
            this.secondRefreshMenu.Text = "二刷不活";
            // 
            // infoCollectionMenu
            // 
            this.infoCollectionMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.passwdBlastingMenuItem,
            this.systemInfoToolStripMenuItem});
            this.infoCollectionMenu.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.infoCollectionMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.infoCollectionMenu.Name = "infoCollectionMenu";
            this.infoCollectionMenu.Size = new System.Drawing.Size(69, 22);
            this.infoCollectionMenu.Text = "主动探针";
            this.infoCollectionMenu.ToolTipText = "各种信息探针";
            // 
            // passwdBlastingMenuItem
            // 
            this.passwdBlastingMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重新开始ToolStripMenuItem,
            this.继续上次ToolStripMenuItem});
            this.passwdBlastingMenuItem.Name = "passwdBlastingMenuItem";
            this.passwdBlastingMenuItem.Size = new System.Drawing.Size(180, 22);
            this.passwdBlastingMenuItem.Text = "随机探针";
            // 
            // systemInfoToolStripMenuItem
            // 
            this.systemInfoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重新开始ToolStripMenuItem2,
            this.继续上次ToolStripMenuItem2});
            this.systemInfoToolStripMenuItem.Name = "systemInfoToolStripMenuItem";
            this.systemInfoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.systemInfoToolStripMenuItem.Text = "重放探针";
            // 
            // refreshStopMenu
            // 
            this.refreshStopMenu.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.refreshStopMenu.Name = "refreshStopMenu";
            this.refreshStopMenu.Size = new System.Drawing.Size(56, 22);
            this.refreshStopMenu.Text = "停止任务";
            this.refreshStopMenu.Click += new System.EventHandler(this.StopMenu_Click);
            // 
            // progressBar
            // 
            this.progressBar.AutoSize = false;
            this.progressBar.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.progressBar.Enabled = false;
            this.progressBar.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(150, 20);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // progressMenu
            // 
            this.progressMenu.Enabled = false;
            this.progressMenu.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.progressMenu.Name = "progressMenu";
            this.progressMenu.Size = new System.Drawing.Size(24, 22);
            this.progressMenu.Text = "-/-";
            this.progressMenu.ToolTipText = "验活进度";
            // 
            // proxySettingMenu
            // 
            this.proxySettingMenu.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.proxySettingMenu.Name = "proxySettingMenu";
            this.proxySettingMenu.Size = new System.Drawing.Size(56, 22);
            this.proxySettingMenu.Text = "代理设置";
            this.proxySettingMenu.Click += new System.EventHandler(this.ProxySettingMenu_Click);
            // 
            // LV
            // 
            this.LV.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.LV.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvAddTime,
            this.lvRemark,
            this.lvUrl,
            this.lvPort,
            this.lvPass,
            this.lvMethod,
            this.lvStatus,
            this.lvVersion,
            this.lvInfoCollection,
            this.lvIP,
            this.lvCountry});
            this.LV.ContextMenuStrip = this.contextMenuStrip;
            this.LV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV.Font = new System.Drawing.Font("宋体", 9F);
            this.LV.FullRowSelect = true;
            this.LV.GridLines = true;
            this.LV.HideSelection = false;
            this.LV.LabelWrap = false;
            this.LV.Location = new System.Drawing.Point(0, 25);
            this.LV.Margin = new System.Windows.Forms.Padding(6);
            this.LV.Name = "LV";
            this.LV.ShowGroups = false;
            this.LV.ShowItemToolTips = true;
            this.LV.Size = new System.Drawing.Size(1153, 467);
            this.LV.TabIndex = 4;
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
            this.lvRemark.Width = 68;
            // 
            // lvUrl
            // 
            this.lvUrl.Text = "主机地址";
            this.lvUrl.Width = 120;
            // 
            // lvPort
            // 
            this.lvPort.Text = "端口";
            this.lvPort.Width = 74;
            // 
            // lvPass
            // 
            this.lvPass.Text = "密码";
            this.lvPass.Width = 96;
            // 
            // lvMethod
            // 
            this.lvMethod.Text = "加密算法";
            this.lvMethod.Width = 107;
            // 
            // lvStatus
            // 
            this.lvStatus.Text = "验活";
            this.lvStatus.Width = 53;
            // 
            // lvVersion
            // 
            this.lvVersion.Text = "客户端类型";
            this.lvVersion.Width = 143;
            // 
            // lvInfoCollection
            // 
            this.lvInfoCollection.Text = "探针信息";
            this.lvInfoCollection.Width = 111;
            // 
            // lvIP
            // 
            this.lvIP.Text = "IP";
            this.lvIP.Width = 114;
            // 
            // lvCountry
            // 
            this.lvCountry.Text = "归属地";
            this.lvCountry.Width = 134;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditToolStripMenuItem,
            this.RemoveToolStripMenuItem,
            this.toolStripSeparator,
            this.CheckAliveSelectedItemMenuItem,
            this.DDMenuItem,
            this.toolStripSeparator4,
            this.ClearAllToolStripMenuItem,
            this.CopyToolStripMenuItem,
            this.SaveResultsMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(149, 170);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.EditToolStripMenuItem.Text = "编辑";
            // 
            // RemoveToolStripMenuItem
            // 
            this.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem";
            this.RemoveToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.RemoveToolStripMenuItem.Text = "删除";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(145, 6);
            // 
            // CheckAliveSelectedItemMenuItem
            // 
            this.CheckAliveSelectedItemMenuItem.Name = "CheckAliveSelectedItemMenuItem";
            this.CheckAliveSelectedItemMenuItem.Size = new System.Drawing.Size(148, 22);
            this.CheckAliveSelectedItemMenuItem.Text = "选定项验活";
            // 
            // DDMenuItem
            // 
            this.DDMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mysqlProbeToolStripMenuItem,
            this.随机探针ToolStripMenuItem});
            this.DDMenuItem.Name = "DDMenuItem";
            this.DDMenuItem.Size = new System.Drawing.Size(148, 22);
            this.DDMenuItem.Text = "主动探针";
            // 
            // mysqlProbeToolStripMenuItem
            // 
            this.mysqlProbeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mysqlBlastingToolStripMenuItem,
            this.配置文件探针ToolStripMenuItem,
            this.userMYD探针ToolStripMenuItem});
            this.mysqlProbeToolStripMenuItem.Name = "mysqlProbeToolStripMenuItem";
            this.mysqlProbeToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.mysqlProbeToolStripMenuItem.Text = "重放探针";
            // 
            // mysqlBlastingToolStripMenuItem
            // 
            this.mysqlBlastingToolStripMenuItem.Name = "mysqlBlastingToolStripMenuItem";
            this.mysqlBlastingToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.mysqlBlastingToolStripMenuItem.Text = "Mysql Blasting";
            this.mysqlBlastingToolStripMenuItem.ToolTipText = "取证固证: 尝试Mysql管理员27000次";
            // 
            // 配置文件探针ToolStripMenuItem
            // 
            this.配置文件探针ToolStripMenuItem.Name = "配置文件探针ToolStripMenuItem";
            this.配置文件探针ToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.配置文件探针ToolStripMenuItem.Text = "配置文件探针";
            this.配置文件探针ToolStripMenuItem.ToolTipText = "取证固证: 站点上关于Mysql的配置信息";
            // 
            // userMYD探针ToolStripMenuItem
            // 
            this.userMYD探针ToolStripMenuItem.Name = "userMYD探针ToolStripMenuItem";
            this.userMYD探针ToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.userMYD探针ToolStripMenuItem.Text = "User.MYD探针";
            this.userMYD探针ToolStripMenuItem.ToolTipText = "取证固证: 寻找Mysql的user.MYD表文件";
            // 
            // 随机探针ToolStripMenuItem
            // 
            this.随机探针ToolStripMenuItem.Name = "随机探针ToolStripMenuItem";
            this.随机探针ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.随机探针ToolStripMenuItem.Text = "随机探针";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(145, 6);
            // 
            // ClearAllToolStripMenuItem
            // 
            this.ClearAllToolStripMenuItem.Name = "ClearAllToolStripMenuItem";
            this.ClearAllToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ClearAllToolStripMenuItem.Text = "全部清空";
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.CopyToolStripMenuItem.Text = "复制到剪切板";
            // 
            // SaveResultsMenuItem
            // 
            this.SaveResultsMenuItem.Name = "SaveResultsMenuItem";
            this.SaveResultsMenuItem.Size = new System.Drawing.Size(148, 22);
            this.SaveResultsMenuItem.Text = "导出结果";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemCountSLabel,
            this.ProxyEnableSLabel,
            this.infoConfigStatus,
            this.StatusLabel,
            this.actionStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 461);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(3, 0, 32, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1153, 31);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 6;
            // 
            // ItemCountSLabel
            // 
            this.ItemCountSLabel.AutoSize = false;
            this.ItemCountSLabel.BackColor = System.Drawing.SystemColors.Control;
            this.ItemCountSLabel.Name = "ItemCountSLabel";
            this.ItemCountSLabel.Size = new System.Drawing.Size(64, 26);
            // 
            // ProxyEnableSLabel
            // 
            this.ProxyEnableSLabel.Name = "ProxyEnableSLabel";
            this.ProxyEnableSLabel.Size = new System.Drawing.Size(0, 26);
            // 
            // infoConfigStatus
            // 
            this.infoConfigStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.infoConfigStatus.Name = "infoConfigStatus";
            this.infoConfigStatus.Size = new System.Drawing.Size(0, 26);
            // 
            // StatusLabel
            // 
            this.StatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(35, 26);
            this.StatusLabel.Text = "统计:";
            // 
            // actionStatusLabel
            // 
            this.actionStatusLabel.Name = "actionStatusLabel";
            this.actionStatusLabel.Size = new System.Drawing.Size(44, 26);
            this.actionStatusLabel.Text = "未开始";
            // 
            // 重新开始ToolStripMenuItem
            // 
            this.重新开始ToolStripMenuItem.Name = "重新开始ToolStripMenuItem";
            this.重新开始ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.重新开始ToolStripMenuItem.Text = "重新开始";
            this.重新开始ToolStripMenuItem.Click += new System.EventHandler(this.重新开始ToolStripMenuItem_Click);
            // 
            // 继续上次ToolStripMenuItem
            // 
            this.继续上次ToolStripMenuItem.Name = "继续上次ToolStripMenuItem";
            this.继续上次ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.继续上次ToolStripMenuItem.Text = "继续上次";
            this.继续上次ToolStripMenuItem.Click += new System.EventHandler(this.继续上次ToolStripMenuItem_Click);
            // 
            // 重新开始ToolStripMenuItem2
            // 
            this.重新开始ToolStripMenuItem2.Name = "重新开始ToolStripMenuItem2";
            this.重新开始ToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.重新开始ToolStripMenuItem2.Text = "重新开始";
            // 
            // 继续上次ToolStripMenuItem2
            // 
            this.继续上次ToolStripMenuItem2.Name = "继续上次ToolStripMenuItem2";
            this.继续上次ToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.继续上次ToolStripMenuItem2.Text = "继续上次";
            // 
            // VPNMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 492);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.LV);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VPNMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VPN专项";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton editDDB;
        private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 批量添加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查找ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton checkAliveDDB;
        private System.Windows.Forms.ToolStripMenuItem refreshAllShellMenu;
        private System.Windows.Forms.ToolStripMenuItem 重新开始ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 继续上次ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem secondRefreshMenu;
        private System.Windows.Forms.ToolStripDropDownButton infoCollectionMenu;
        private System.Windows.Forms.ToolStripMenuItem systemInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel refreshStopMenu;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripLabel progressMenu;
        private System.Windows.Forms.ToolStripLabel proxySettingMenu;
        private System.Windows.Forms.ListView LV;
        private System.Windows.Forms.ColumnHeader lvAddTime;
        private System.Windows.Forms.ColumnHeader lvRemark;
        private System.Windows.Forms.ColumnHeader lvUrl;
        private System.Windows.Forms.ColumnHeader lvPass;
        private System.Windows.Forms.ColumnHeader lvMethod;
        private System.Windows.Forms.ColumnHeader lvStatus;
        private System.Windows.Forms.ColumnHeader lvVersion;
        private System.Windows.Forms.ColumnHeader lvInfoCollection;
        private System.Windows.Forms.ColumnHeader lvIP;
        private System.Windows.Forms.ColumnHeader lvCountry;
        private System.Windows.Forms.ColumnHeader lvPort;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem CheckAliveSelectedItemMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DDMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mysqlProbeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mysqlBlastingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 配置文件探针ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userMYD探针ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 随机探针ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem ClearAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveResultsMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ItemCountSLabel;
        private System.Windows.Forms.ToolStripStatusLabel ProxyEnableSLabel;
        private System.Windows.Forms.ToolStripStatusLabel infoConfigStatus;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel actionStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem passwdBlastingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重新开始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 继续上次ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重新开始ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 继续上次ToolStripMenuItem2;
    }
}