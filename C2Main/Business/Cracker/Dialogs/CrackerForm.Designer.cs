namespace C2.Business.Cracker.Dialogs
{
    partial class CrackerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrackerForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_export = new System.Windows.Forms.Button();
            this.cbox_reTry = new System.Windows.Forms.ComboBox();
            this.cbox_timeOut = new System.Windows.Forms.ComboBox();
            this.cbox_threadSize = new System.Windows.Forms.ComboBox();
            this.btn_stopCracker = new System.Windows.Forms.Button();
            this.btn_cracker = new System.Windows.Forms.Button();
            this.txt_target = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rdp_panle = new System.Windows.Forms.Panel();
            this.services_list = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_log = new System.Windows.Forms.RichTextBox();
            this.list_lvw = new System.Windows.Forms.ListView();
            this.col_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_ip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_serviceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_port = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_username = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_pass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_banner = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_useTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cms_lvw = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_export = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_deleteSelectItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_clearItems = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_openURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_copyItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bt_status = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tools_proBar = new System.Windows.Forms.ToolStripProgressBar();
            this.stxt_percent = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stxt_useTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.s_txt_threadlable = new System.Windows.Forms.ToolStripStatusLabel();
            this.stxt_threadStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stxt_threadPoolStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stxt_crackerSuccessCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stxt_speed = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_notScanPortsSumCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.bt_timer = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.cms_lvw.SuspendLayout();
            this.bt_status.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_export);
            this.groupBox1.Controls.Add(this.cbox_reTry);
            this.groupBox1.Controls.Add(this.cbox_timeOut);
            this.groupBox1.Controls.Add(this.cbox_threadSize);
            this.groupBox1.Controls.Add(this.btn_stopCracker);
            this.groupBox1.Controls.Add(this.btn_cracker);
            this.groupBox1.Controls.Add(this.txt_target);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(743, 96);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btn_export
            // 
            this.btn_export.Location = new System.Drawing.Point(652, 57);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(72, 23);
            this.btn_export.TabIndex = 12;
            this.btn_export.Text = "导出结果";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // cbox_reTry
            // 
            this.cbox_reTry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_reTry.FormattingEnabled = true;
            this.cbox_reTry.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.cbox_reTry.Location = new System.Drawing.Point(445, 60);
            this.cbox_reTry.Name = "cbox_reTry";
            this.cbox_reTry.Size = new System.Drawing.Size(61, 20);
            this.cbox_reTry.TabIndex = 11;
            this.cbox_reTry.TextChanged += new System.EventHandler(this.cbox_reTry_TextChanged);
            // 
            // cbox_timeOut
            // 
            this.cbox_timeOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_timeOut.FormattingEnabled = true;
            this.cbox_timeOut.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "60"});
            this.cbox_timeOut.Location = new System.Drawing.Point(83, 60);
            this.cbox_timeOut.Name = "cbox_timeOut";
            this.cbox_timeOut.Size = new System.Drawing.Size(61, 20);
            this.cbox_timeOut.TabIndex = 4;
            this.cbox_timeOut.TextChanged += new System.EventHandler(this.cbox_timeOut_TextChanged);
            // 
            // cbox_threadSize
            // 
            this.cbox_threadSize.AutoCompleteCustomSource.AddRange(new string[] {
            "导入信息自动选择"});
            this.cbox_threadSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_threadSize.FormattingEnabled = true;
            this.cbox_threadSize.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "10"});
            this.cbox_threadSize.Location = new System.Drawing.Point(264, 60);
            this.cbox_threadSize.Name = "cbox_threadSize";
            this.cbox_threadSize.Size = new System.Drawing.Size(61, 20);
            this.cbox_threadSize.TabIndex = 9;
            this.cbox_threadSize.SelectedIndexChanged += new System.EventHandler(this.cbox_threadSize_SelectedIndexChanged);
            // 
            // btn_stopCracker
            // 
            this.btn_stopCracker.Location = new System.Drawing.Point(652, 21);
            this.btn_stopCracker.Name = "btn_stopCracker";
            this.btn_stopCracker.Size = new System.Drawing.Size(72, 23);
            this.btn_stopCracker.TabIndex = 2;
            this.btn_stopCracker.Text = "停止检查";
            this.btn_stopCracker.UseVisualStyleBackColor = true;
            this.btn_stopCracker.Click += new System.EventHandler(this.btn_stopCracker_Click);
            // 
            // btn_cracker
            // 
            this.btn_cracker.Location = new System.Drawing.Point(556, 21);
            this.btn_cracker.Name = "btn_cracker";
            this.btn_cracker.Size = new System.Drawing.Size(72, 23);
            this.btn_cracker.TabIndex = 2;
            this.btn_cracker.Text = "开始检查";
            this.btn_cracker.UseVisualStyleBackColor = true;
            this.btn_cracker.Click += new System.EventHandler(this.btn_cracker_Click);
            // 
            // txt_target
            // 
            this.txt_target.Location = new System.Drawing.Point(83, 21);
            this.txt_target.Name = "txt_target";
            this.txt_target.Size = new System.Drawing.Size(423, 21);
            this.txt_target.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(398, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "重试：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "超时：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "线程：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP：";
            // 
            // rdp_panle
            // 
            this.rdp_panle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.rdp_panle.Location = new System.Drawing.Point(8, 131);
            this.rdp_panle.Name = "rdp_panle";
            this.rdp_panle.Size = new System.Drawing.Size(1, 1);
            this.rdp_panle.TabIndex = 12;
            // 
            // services_list
            // 
            this.services_list.BackColor = System.Drawing.SystemColors.Window;
            this.services_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.services_list.FormattingEnabled = true;
            this.services_list.Location = new System.Drawing.Point(0, 0);
            this.services_list.Name = "services_list";
            this.services_list.Size = new System.Drawing.Size(95, 408);
            this.services_list.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txt_log);
            this.groupBox2.Location = new System.Drawing.Point(8, 446);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(854, 129);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // txt_log
            // 
            this.txt_log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_log.Location = new System.Drawing.Point(3, 17);
            this.txt_log.Name = "txt_log";
            this.txt_log.Size = new System.Drawing.Size(848, 109);
            this.txt_log.TabIndex = 1;
            this.txt_log.Text = "";
            // 
            // list_lvw
            // 
            this.list_lvw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.list_lvw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_id,
            this.col_ip,
            this.col_serviceName,
            this.col_port,
            this.col_username,
            this.col_pass,
            this.col_banner,
            this.col_useTime});
            this.list_lvw.ContextMenuStrip = this.cms_lvw;
            this.list_lvw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_lvw.FullRowSelect = true;
            this.list_lvw.GridLines = true;
            this.list_lvw.HideSelection = false;
            this.list_lvw.Location = new System.Drawing.Point(3, 17);
            this.list_lvw.Name = "list_lvw";
            this.list_lvw.Size = new System.Drawing.Size(735, 284);
            this.list_lvw.TabIndex = 0;
            this.list_lvw.UseCompatibleStateImageBehavior = false;
            this.list_lvw.View = System.Windows.Forms.View.Details;
            // 
            // col_id
            // 
            this.col_id.Text = "序号";
            // 
            // col_ip
            // 
            this.col_ip.Text = "IP地址";
            this.col_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.col_ip.Width = 116;
            // 
            // col_serviceName
            // 
            this.col_serviceName.Text = "服 务";
            this.col_serviceName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.col_serviceName.Width = 87;
            // 
            // col_port
            // 
            this.col_port.Text = "端口";
            this.col_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.col_port.Width = 63;
            // 
            // col_username
            // 
            this.col_username.Text = "帐户名";
            this.col_username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.col_username.Width = 103;
            // 
            // col_pass
            // 
            this.col_pass.Text = "密码";
            this.col_pass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.col_pass.Width = 105;
            // 
            // col_banner
            // 
            this.col_banner.Text = "BANNER";
            this.col_banner.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.col_banner.Width = 110;
            // 
            // col_useTime
            // 
            this.col_useTime.Text = "用时[毫秒]";
            this.col_useTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.col_useTime.Width = 80;
            // 
            // cms_lvw
            // 
            this.cms_lvw.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_export,
            this.tsmi_deleteSelectItem,
            this.tsmi_clearItems,
            this.tsmi_openURL,
            this.tsmi_copyItem});
            this.cms_lvw.Name = "cms_lvw";
            this.cms_lvw.Size = new System.Drawing.Size(137, 114);
            // 
            // tsmi_export
            // 
            this.tsmi_export.Name = "tsmi_export";
            this.tsmi_export.Size = new System.Drawing.Size(136, 22);
            this.tsmi_export.Text = "导出结果";
            this.tsmi_export.Click += new System.EventHandler(this.tsmi_export_Click);
            // 
            // tsmi_deleteSelectItem
            // 
            this.tsmi_deleteSelectItem.Name = "tsmi_deleteSelectItem";
            this.tsmi_deleteSelectItem.Size = new System.Drawing.Size(136, 22);
            this.tsmi_deleteSelectItem.Text = "删除选中行";
            this.tsmi_deleteSelectItem.Click += new System.EventHandler(this.tsmi_deleteSelectItem_Click);
            // 
            // tsmi_clearItems
            // 
            this.tsmi_clearItems.Name = "tsmi_clearItems";
            this.tsmi_clearItems.Size = new System.Drawing.Size(136, 22);
            this.tsmi_clearItems.Text = "清空结果";
            this.tsmi_clearItems.Click += new System.EventHandler(this.tsmi_clearItems_Click);
            // 
            // tsmi_openURL
            // 
            this.tsmi_openURL.Name = "tsmi_openURL";
            this.tsmi_openURL.Size = new System.Drawing.Size(136, 22);
            this.tsmi_openURL.Text = "打开URL";
            this.tsmi_openURL.Click += new System.EventHandler(this.tsmi_openURL_Click);
            // 
            // tsmi_copyItem
            // 
            this.tsmi_copyItem.Name = "tsmi_copyItem";
            this.tsmi_copyItem.Size = new System.Drawing.Size(136, 22);
            this.tsmi_copyItem.Text = "复 制";
            this.tsmi_copyItem.Click += new System.EventHandler(this.tsmi_copyItem_Click);
            // 
            // bt_status
            // 
            this.bt_status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tools_proBar,
            this.stxt_percent,
            this.toolStripStatusLabel2,
            this.stxt_useTime,
            this.s_txt_threadlable,
            this.stxt_threadStatus,
            this.toolStripStatusLabel6,
            this.stxt_threadPoolStatus,
            this.toolStripStatusLabel3,
            this.stxt_crackerSuccessCount,
            this.toolStripStatusLabel5,
            this.stxt_speed,
            this.toolStripStatusLabel4,
            this.tssl_notScanPortsSumCount});
            this.bt_status.Location = new System.Drawing.Point(0, 578);
            this.bt_status.Name = "bt_status";
            this.bt_status.Size = new System.Drawing.Size(872, 22);
            this.bt_status.TabIndex = 1;
            this.bt_status.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(48, 17);
            this.toolStripStatusLabel1.Text = "进 度：";
            // 
            // tools_proBar
            // 
            this.tools_proBar.Name = "tools_proBar";
            this.tools_proBar.Size = new System.Drawing.Size(150, 16);
            // 
            // stxt_percent
            // 
            this.stxt_percent.Name = "stxt_percent";
            this.stxt_percent.Size = new System.Drawing.Size(26, 17);
            this.stxt_percent.Text = "0%";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel2.Text = "用时：";
            // 
            // stxt_useTime
            // 
            this.stxt_useTime.Name = "stxt_useTime";
            this.stxt_useTime.Size = new System.Drawing.Size(15, 17);
            this.stxt_useTime.Text = "0";
            // 
            // s_txt_threadlable
            // 
            this.s_txt_threadlable.Name = "s_txt_threadlable";
            this.s_txt_threadlable.Size = new System.Drawing.Size(44, 17);
            this.s_txt_threadlable.Text = "线程：";
            // 
            // stxt_threadStatus
            // 
            this.stxt_threadStatus.Name = "stxt_threadStatus";
            this.stxt_threadStatus.Size = new System.Drawing.Size(27, 17);
            this.stxt_threadStatus.Text = "0/0";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(92, 17);
            this.toolStripStatusLabel6.Text = "预计检查队列：";
            // 
            // stxt_threadPoolStatus
            // 
            this.stxt_threadPoolStatus.Name = "stxt_threadPoolStatus";
            this.stxt_threadPoolStatus.Size = new System.Drawing.Size(15, 17);
            this.stxt_threadPoolStatus.Text = "0";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel3.Text = "成功：";
            // 
            // stxt_crackerSuccessCount
            // 
            this.stxt_crackerSuccessCount.Name = "stxt_crackerSuccessCount";
            this.stxt_crackerSuccessCount.Size = new System.Drawing.Size(15, 17);
            this.stxt_crackerSuccessCount.Text = "0";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel5.Text = "速度：";
            // 
            // stxt_speed
            // 
            this.stxt_speed.Name = "stxt_speed";
            this.stxt_speed.Size = new System.Drawing.Size(15, 17);
            this.stxt_speed.Text = "0";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(104, 17);
            this.toolStripStatusLabel4.Text = "剩余端口扫描数：";
            // 
            // tssl_notScanPortsSumCount
            // 
            this.tssl_notScanPortsSumCount.Name = "tssl_notScanPortsSumCount";
            this.tssl_notScanPortsSumCount.Size = new System.Drawing.Size(15, 17);
            this.tssl_notScanPortsSumCount.Text = "0";
            // 
            // bt_timer
            // 
            this.bt_timer.Interval = 1000;
            this.bt_timer.Tick += new System.EventHandler(this.bt_timer_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.splitContainer1);
            this.groupBox3.Location = new System.Drawing.Point(8, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(854, 428);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.services_list);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer1.Panel2.Controls.Add(this.rdp_panle);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(848, 408);
            this.splitContainer1.SplitterDistance = 95;
            this.splitContainer1.TabIndex = 4;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.list_lvw);
            this.groupBox5.Location = new System.Drawing.Point(5, 102);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(741, 304);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "弱口令列表";
            // 
            // CrackerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 600);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.bt_status);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CrackerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "弱口令扫描";
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.cms_lvw.ResumeLayout(false);
            this.bt_status.ResumeLayout(false);
            this.bt_status.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView list_lvw;
        private System.Windows.Forms.ColumnHeader col_id;
        private System.Windows.Forms.ColumnHeader col_ip;
        private System.Windows.Forms.ColumnHeader col_port;
        private System.Windows.Forms.ColumnHeader col_username;
        private System.Windows.Forms.ColumnHeader col_pass;
        private System.Windows.Forms.TextBox txt_target;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbox_threadSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbox_timeOut;
        private System.Windows.Forms.ComboBox cbox_reTry;
        private System.Windows.Forms.Button btn_cracker;
        private System.Windows.Forms.StatusStrip bt_status;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar tools_proBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel stxt_useTime;
        private System.Windows.Forms.ToolStripStatusLabel s_txt_threadlable;
        private System.Windows.Forms.ToolStripStatusLabel stxt_threadStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel stxt_crackerSuccessCount;
        private System.Windows.Forms.Timer bt_timer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel stxt_threadPoolStatus;
        private System.Windows.Forms.ColumnHeader col_serviceName;
        private System.Windows.Forms.ToolStripStatusLabel stxt_percent;
        private System.Windows.Forms.Button btn_stopCracker;
        private System.Windows.Forms.ColumnHeader col_banner;
        private System.Windows.Forms.ContextMenuStrip cms_lvw;
        private System.Windows.Forms.ToolStripMenuItem tsmi_export;
        private System.Windows.Forms.ToolStripMenuItem tsmi_deleteSelectItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_clearItems;
        private System.Windows.Forms.ToolStripMenuItem tsmi_openURL;
        private System.Windows.Forms.ToolStripMenuItem tsmi_copyItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel stxt_speed;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckedListBox services_list;
        private System.Windows.Forms.RichTextBox txt_log;
        private System.Windows.Forms.Panel rdp_panle;
        private System.Windows.Forms.ColumnHeader col_useTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel tssl_notScanPortsSumCount;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_export;
    }
}