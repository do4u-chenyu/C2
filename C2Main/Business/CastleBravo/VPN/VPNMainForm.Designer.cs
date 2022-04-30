
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
            this.单个添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.批量添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查找ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkAliveDDB = new System.Windows.Forms.ToolStripDropDownButton();
            this.refreshAllShellMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.重新开始ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.继续上次ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.secondRefreshMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.infoCollectionMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.passwdBlastingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.继续上次ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新开始ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.继续上次ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshStopMenu = new System.Windows.Forms.ToolStripLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.progressMenu = new System.Windows.Forms.ToolStripLabel();
            this.proxySettingMenu = new System.Windows.Forms.ToolStripLabel();
            this.LV = new System.Windows.Forms.ListView();
            this.lvAddTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvRemark = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvHost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvPassword = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvMethod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvProbeInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvOtherInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvCountry = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveResultsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出IP端口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出分享地址ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ItemCountSLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProxyEnableSLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.infoConfigStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.actionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.label1.Size = new System.Drawing.Size(96, 28);
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
            this.toolStrip1.Size = new System.Drawing.Size(1366, 34);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // editDDB
            // 
            this.editDDB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.单个添加ToolStripMenuItem,
            this.批量添加ToolStripMenuItem,
            this.查找ToolStripMenuItem});
            this.editDDB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.editDDB.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editDDB.Name = "editDDB";
            this.editDDB.Size = new System.Drawing.Size(66, 29);
            this.editDDB.Text = "常规";
            this.editDDB.ToolTipText = "常规操作";
            // 
            // 单个添加ToolStripMenuItem
            // 
            this.单个添加ToolStripMenuItem.Enabled = false;
            this.单个添加ToolStripMenuItem.Name = "单个添加ToolStripMenuItem";
            this.单个添加ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.单个添加ToolStripMenuItem.Text = "单个添加";
            this.单个添加ToolStripMenuItem.Click += new System.EventHandler(this.添加ToolStripMenuItem_Click);
            // 
            // 批量添加ToolStripMenuItem
            // 
            this.批量添加ToolStripMenuItem.Name = "批量添加ToolStripMenuItem";
            this.批量添加ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.批量添加ToolStripMenuItem.Text = "批量添加";
            this.批量添加ToolStripMenuItem.Click += new System.EventHandler(this.批量添加ToolStripMenuItem_Click);
            // 
            // 查找ToolStripMenuItem
            // 
            this.查找ToolStripMenuItem.Name = "查找ToolStripMenuItem";
            this.查找ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.查找ToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
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
            this.checkAliveDDB.Size = new System.Drawing.Size(102, 29);
            this.checkAliveDDB.Text = "验活功能";
            // 
            // refreshAllShellMenu
            // 
            this.refreshAllShellMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重新开始ToolStripMenuItem1,
            this.继续上次ToolStripMenuItem1});
            this.refreshAllShellMenu.Name = "refreshAllShellMenu";
            this.refreshAllShellMenu.Size = new System.Drawing.Size(186, 34);
            this.refreshAllShellMenu.Text = "批量验活";
            // 
            // 重新开始ToolStripMenuItem1
            // 
            this.重新开始ToolStripMenuItem1.Name = "重新开始ToolStripMenuItem1";
            this.重新开始ToolStripMenuItem1.Size = new System.Drawing.Size(186, 34);
            this.重新开始ToolStripMenuItem1.Text = "重新开始";
            this.重新开始ToolStripMenuItem1.Click += new System.EventHandler(this.重新开始_批量验活_ToolStripMenuItem_Click);
            // 
            // 继续上次ToolStripMenuItem1
            // 
            this.继续上次ToolStripMenuItem1.Name = "继续上次ToolStripMenuItem1";
            this.继续上次ToolStripMenuItem1.Size = new System.Drawing.Size(186, 34);
            this.继续上次ToolStripMenuItem1.Text = "继续上次";
            this.继续上次ToolStripMenuItem1.Click += new System.EventHandler(this.继续上次_二刷不活_ToolStripMenuItem_Click);
            // 
            // secondRefreshMenu
            // 
            this.secondRefreshMenu.Name = "secondRefreshMenu";
            this.secondRefreshMenu.Size = new System.Drawing.Size(186, 34);
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
            this.infoCollectionMenu.Size = new System.Drawing.Size(102, 29);
            this.infoCollectionMenu.Text = "主动探针";
            this.infoCollectionMenu.ToolTipText = "各种信息探针";
            // 
            // passwdBlastingMenuItem
            // 
            this.passwdBlastingMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重新开始ToolStripMenuItem,
            this.继续上次ToolStripMenuItem});
            this.passwdBlastingMenuItem.Name = "passwdBlastingMenuItem";
            this.passwdBlastingMenuItem.Size = new System.Drawing.Size(186, 34);
            this.passwdBlastingMenuItem.Text = "随机探针";
            // 
            // 重新开始ToolStripMenuItem
            // 
            this.重新开始ToolStripMenuItem.Name = "重新开始ToolStripMenuItem";
            this.重新开始ToolStripMenuItem.Size = new System.Drawing.Size(186, 34);
            this.重新开始ToolStripMenuItem.Text = "重新开始";
            this.重新开始ToolStripMenuItem.Click += new System.EventHandler(this.重新开始ToolStripMenuItem_Click);
            // 
            // 继续上次ToolStripMenuItem
            // 
            this.继续上次ToolStripMenuItem.Name = "继续上次ToolStripMenuItem";
            this.继续上次ToolStripMenuItem.Size = new System.Drawing.Size(186, 34);
            this.继续上次ToolStripMenuItem.Text = "继续上次";
            this.继续上次ToolStripMenuItem.Click += new System.EventHandler(this.继续上次ToolStripMenuItem_Click);
            // 
            // systemInfoToolStripMenuItem
            // 
            this.systemInfoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重新开始ToolStripMenuItem2,
            this.继续上次ToolStripMenuItem2});
            this.systemInfoToolStripMenuItem.Name = "systemInfoToolStripMenuItem";
            this.systemInfoToolStripMenuItem.Size = new System.Drawing.Size(186, 34);
            this.systemInfoToolStripMenuItem.Text = "重放探针";
            // 
            // 重新开始ToolStripMenuItem2
            // 
            this.重新开始ToolStripMenuItem2.Name = "重新开始ToolStripMenuItem2";
            this.重新开始ToolStripMenuItem2.Size = new System.Drawing.Size(186, 34);
            this.重新开始ToolStripMenuItem2.Text = "重新开始";
            // 
            // 继续上次ToolStripMenuItem2
            // 
            this.继续上次ToolStripMenuItem2.Name = "继续上次ToolStripMenuItem2";
            this.继续上次ToolStripMenuItem2.Size = new System.Drawing.Size(186, 34);
            this.继续上次ToolStripMenuItem2.Text = "继续上次";
            // 
            // refreshStopMenu
            // 
            this.refreshStopMenu.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.refreshStopMenu.Name = "refreshStopMenu";
            this.refreshStopMenu.Size = new System.Drawing.Size(84, 29);
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
            this.progressMenu.Size = new System.Drawing.Size(37, 29);
            this.progressMenu.Text = "-/-";
            this.progressMenu.ToolTipText = "验活进度";
            // 
            // proxySettingMenu
            // 
            this.proxySettingMenu.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.proxySettingMenu.Name = "proxySettingMenu";
            this.proxySettingMenu.Size = new System.Drawing.Size(84, 29);
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
            this.lvHost,
            this.lvPort,
            this.lvPassword,
            this.lvMethod,
            this.lvStatus,
            this.lvVersion,
            this.lvProbeInfo,
            this.lvOtherInfo,
            this.lvIP,
            this.lvCountry,
            this.lvContent});
            this.LV.ContextMenuStrip = this.contextMenuStrip;
            this.LV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV.Font = new System.Drawing.Font("宋体", 9F);
            this.LV.FullRowSelect = true;
            this.LV.GridLines = true;
            this.LV.HideSelection = false;
            this.LV.LabelEdit = true;
            this.LV.LabelWrap = false;
            this.LV.Location = new System.Drawing.Point(0, 34);
            this.LV.Margin = new System.Windows.Forms.Padding(6);
            this.LV.Name = "LV";
            this.LV.ShowGroups = false;
            this.LV.ShowItemToolTips = true;
            this.LV.Size = new System.Drawing.Size(1366, 462);
            this.LV.TabIndex = 4;
            this.LV.UseCompatibleStateImageBehavior = false;
            this.LV.View = System.Windows.Forms.View.Details;
            this.LV.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LV_ColumnClick);
            // 
            // lvAddTime
            // 
            this.lvAddTime.Text = "创建时间";
            this.lvAddTime.Width = 130;
            // 
            // lvRemark
            // 
            this.lvRemark.Text = "备注";
            this.lvRemark.Width = 215;
            // 
            // lvHost
            // 
            this.lvHost.Text = "主机地址";
            this.lvHost.Width = 150;
            // 
            // lvPort
            // 
            this.lvPort.Text = "端口";
            this.lvPort.Width = 54;
            // 
            // lvPassword
            // 
            this.lvPassword.Text = "密码";
            this.lvPassword.Width = 105;
            // 
            // lvMethod
            // 
            this.lvMethod.Text = "加密算法";
            this.lvMethod.Width = 80;
            // 
            // lvStatus
            // 
            this.lvStatus.Text = "验活";
            this.lvStatus.Width = 53;
            // 
            // lvVersion
            // 
            this.lvVersion.Text = "客户端";
            this.lvVersion.Width = 45;
            // 
            // lvProbeInfo
            // 
            this.lvProbeInfo.Text = "探针信息";
            this.lvProbeInfo.Width = 100;
            // 
            // lvOtherInfo
            // 
            this.lvOtherInfo.Text = "其他信息";
            this.lvOtherInfo.Width = 160;
            // 
            // lvIP
            // 
            this.lvIP.Text = "IP";
            this.lvIP.Width = 114;
            // 
            // lvCountry
            // 
            this.lvCountry.Text = "归属地";
            this.lvCountry.Width = 110;
            // 
            // lvContent
            // 
            this.lvContent.Text = "分享地址";
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
            this.toolStripSeparator1,
            this.SaveResultsMenuItem,
            this.导出IP端口ToolStripMenuItem,
            this.导出分享地址ToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(197, 292);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.Enabled = false;
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(196, 30);
            this.EditToolStripMenuItem.Text = "编辑";
            this.EditToolStripMenuItem.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
            // 
            // RemoveToolStripMenuItem
            // 
            this.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem";
            this.RemoveToolStripMenuItem.Size = new System.Drawing.Size(196, 30);
            this.RemoveToolStripMenuItem.Text = "删除";
            this.RemoveToolStripMenuItem.Click += new System.EventHandler(this.RemoveToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(193, 6);
            // 
            // CheckAliveSelectedItemMenuItem
            // 
            this.CheckAliveSelectedItemMenuItem.Name = "CheckAliveSelectedItemMenuItem";
            this.CheckAliveSelectedItemMenuItem.Size = new System.Drawing.Size(196, 30);
            this.CheckAliveSelectedItemMenuItem.Text = "选定项验活";
            // 
            // DDMenuItem
            // 
            this.DDMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mysqlProbeToolStripMenuItem,
            this.随机探针ToolStripMenuItem});
            this.DDMenuItem.Name = "DDMenuItem";
            this.DDMenuItem.Size = new System.Drawing.Size(196, 30);
            this.DDMenuItem.Text = "主动探针";
            // 
            // mysqlProbeToolStripMenuItem
            // 
            this.mysqlProbeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mysqlBlastingToolStripMenuItem,
            this.配置文件探针ToolStripMenuItem,
            this.userMYD探针ToolStripMenuItem});
            this.mysqlProbeToolStripMenuItem.Name = "mysqlProbeToolStripMenuItem";
            this.mysqlProbeToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.mysqlProbeToolStripMenuItem.Text = "重放探针";
            // 
            // mysqlBlastingToolStripMenuItem
            // 
            this.mysqlBlastingToolStripMenuItem.Name = "mysqlBlastingToolStripMenuItem";
            this.mysqlBlastingToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.mysqlBlastingToolStripMenuItem.Text = "Mysql Blasting";
            this.mysqlBlastingToolStripMenuItem.ToolTipText = "取证固证: 尝试Mysql管理员27000次";
            // 
            // 配置文件探针ToolStripMenuItem
            // 
            this.配置文件探针ToolStripMenuItem.Name = "配置文件探针ToolStripMenuItem";
            this.配置文件探针ToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.配置文件探针ToolStripMenuItem.Text = "配置文件探针";
            this.配置文件探针ToolStripMenuItem.ToolTipText = "取证固证: 站点上关于Mysql的配置信息";
            // 
            // userMYD探针ToolStripMenuItem
            // 
            this.userMYD探针ToolStripMenuItem.Name = "userMYD探针ToolStripMenuItem";
            this.userMYD探针ToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.userMYD探针ToolStripMenuItem.Text = "User.MYD探针";
            this.userMYD探针ToolStripMenuItem.ToolTipText = "取证固证: 寻找Mysql的user.MYD表文件";
            // 
            // 随机探针ToolStripMenuItem
            // 
            this.随机探针ToolStripMenuItem.Name = "随机探针ToolStripMenuItem";
            this.随机探针ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.随机探针ToolStripMenuItem.Text = "随机探针";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(193, 6);
            // 
            // ClearAllToolStripMenuItem
            // 
            this.ClearAllToolStripMenuItem.Name = "ClearAllToolStripMenuItem";
            this.ClearAllToolStripMenuItem.Size = new System.Drawing.Size(196, 30);
            this.ClearAllToolStripMenuItem.Text = "全部清空";
            this.ClearAllToolStripMenuItem.Click += new System.EventHandler(this.ClearAllToolStripMenuItem_Click);
            // 
            // CopyToolStripMenuItem
            // 
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new System.Drawing.Size(196, 30);
            this.CopyToolStripMenuItem.Text = "复制到剪切板";
            this.CopyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // SaveResultsMenuItem
            // 
            this.SaveResultsMenuItem.Name = "SaveResultsMenuItem";
            this.SaveResultsMenuItem.Size = new System.Drawing.Size(196, 30);
            this.SaveResultsMenuItem.Text = "导出-所有字段";
            this.SaveResultsMenuItem.Click += new System.EventHandler(this.SaveResultsMenuItem_Click);
            // 
            // 导出IP端口ToolStripMenuItem
            // 
            this.导出IP端口ToolStripMenuItem.Name = "导出IP端口ToolStripMenuItem";
            this.导出IP端口ToolStripMenuItem.Size = new System.Drawing.Size(196, 30);
            this.导出IP端口ToolStripMenuItem.Text = "导出-IP端口";
            this.导出IP端口ToolStripMenuItem.Click += new System.EventHandler(this.导出IP端口ToolStripMenuItem_Click);
            // 
            // 导出分享地址ToolStripMenuItem
            // 
            this.导出分享地址ToolStripMenuItem.Name = "导出分享地址ToolStripMenuItem";
            this.导出分享地址ToolStripMenuItem.Size = new System.Drawing.Size(196, 30);
            this.导出分享地址ToolStripMenuItem.Text = "导出-分享地址";
            this.导出分享地址ToolStripMenuItem.Click += new System.EventHandler(this.导出分享地址ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemCountSLabel,
            this.ProxyEnableSLabel,
            this.infoConfigStatus,
            this.StatusLabel1,
            this.StatusLabel2,
            this.actionStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 465);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(3, 0, 32, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1366, 31);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 6;
            // 
            // ItemCountSLabel
            // 
            this.ItemCountSLabel.AutoSize = false;
            this.ItemCountSLabel.BackColor = System.Drawing.SystemColors.Control;
            this.ItemCountSLabel.Name = "ItemCountSLabel";
            this.ItemCountSLabel.Size = new System.Drawing.Size(64, 24);
            // 
            // ProxyEnableSLabel
            // 
            this.ProxyEnableSLabel.Name = "ProxyEnableSLabel";
            this.ProxyEnableSLabel.Size = new System.Drawing.Size(0, 24);
            // 
            // infoConfigStatus
            // 
            this.infoConfigStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.infoConfigStatus.Name = "infoConfigStatus";
            this.infoConfigStatus.Size = new System.Drawing.Size(0, 24);
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.BackColor = System.Drawing.Color.Transparent;
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(50, 24);
            this.StatusLabel1.Text = "统计:";
            // 
            // StatusLabel2
            // 
            this.StatusLabel2.ForeColor = System.Drawing.Color.Blue;
            this.StatusLabel2.Name = "StatusLabel2";
            this.StatusLabel2.Size = new System.Drawing.Size(120, 24);
            this.StatusLabel2.Text = "StatusLabel2";
            // 
            // actionStatusLabel
            // 
            this.actionStatusLabel.Name = "actionStatusLabel";
            this.actionStatusLabel.Size = new System.Drawing.Size(64, 24);
            this.actionStatusLabel.Text = "未开始";
            // 
            // VPNMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 496);
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
            this.Load += new System.EventHandler(this.VPNMainForm_Load);
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
        private System.Windows.Forms.ToolStripMenuItem 单个添加ToolStripMenuItem;
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
        private System.Windows.Forms.ColumnHeader lvHost;
        private System.Windows.Forms.ColumnHeader lvPassword;
        private System.Windows.Forms.ColumnHeader lvMethod;
        private System.Windows.Forms.ColumnHeader lvStatus;
        private System.Windows.Forms.ColumnHeader lvVersion;
        private System.Windows.Forms.ColumnHeader lvProbeInfo;
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
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel actionStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem passwdBlastingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重新开始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 继续上次ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重新开始ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 继续上次ToolStripMenuItem2;
        private System.Windows.Forms.ColumnHeader lvOtherInfo;
        private System.Windows.Forms.ColumnHeader lvContent;
        private System.Windows.Forms.ToolStripMenuItem 导出IP端口ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 导出分享地址ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel2;
    }
}