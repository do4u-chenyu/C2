namespace C2.Business.CastleBravo.WebScan
{
    partial class WebScanForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebScanForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.crawlerCheckBox = new System.Windows.Forms.CheckBox();
            this.statusCodeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.httpMethodCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.headerCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.refreshDictBtn = new System.Windows.Forms.Button();
            this.openDictPathBtn = new System.Windows.Forms.Button();
            this.dictListView = new System.Windows.Forms.ListView();
            this.dictID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dictName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dictRows = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dictSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.sleepTimeCombo = new System.Windows.Forms.ComboBox();
            this.timeOutCombo = new System.Windows.Forms.ComboBox();
            this.threadSizeCombo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.url = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.webStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contentType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.length = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.runTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.resultCms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openUrl = new System.Windows.Forms.ToolStripMenuItem();
            this.copyUrl = new System.Windows.Forms.ToolStripMenuItem();
            this.exportResults = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.progressPercent = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.scanUseTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.scanThreadStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.threadPoolStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.scanSpeed = new System.Windows.Forms.ToolStripStatusLabel();
            this.startBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.exportBtn = new System.Windows.Forms.Button();
            this.scanTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.HelpLabel = new System.Windows.Forms.LinkLabel();
            this.loginCheckBox = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.editorCheckBox = new System.Windows.Forms.CheckBox();
            this.loopCheckBox = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.resultCms.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.crawlerCheckBox);
            this.groupBox1.Controls.Add(this.statusCodeTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.httpMethodCombo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.headerCombo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.urlTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 166);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "扫描选项";
            // 
            // crawlerCheckBox
            // 
            this.crawlerCheckBox.AutoSize = true;
            this.crawlerCheckBox.Location = new System.Drawing.Point(240, 143);
            this.crawlerCheckBox.Name = "crawlerCheckBox";
            this.crawlerCheckBox.Size = new System.Drawing.Size(72, 16);
            this.crawlerCheckBox.TabIndex = 9;
            this.crawlerCheckBox.Text = "爬虫模式";
            this.crawlerCheckBox.UseVisualStyleBackColor = true;
            // 
            // statusCodeTextBox
            // 
            this.statusCodeTextBox.Location = new System.Drawing.Point(58, 141);
            this.statusCodeTextBox.Name = "statusCodeTextBox";
            this.statusCodeTextBox.Size = new System.Drawing.Size(174, 21);
            this.statusCodeTextBox.TabIndex = 7;
            this.statusCodeTextBox.Text = "200,301,302,303,403,404";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "状态码：";
            // 
            // httpMethodCombo
            // 
            this.httpMethodCombo.BackColor = System.Drawing.Color.White;
            this.httpMethodCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.httpMethodCombo.FormattingEnabled = true;
            this.httpMethodCombo.Items.AddRange(new object[] {
            "GET",
            "HEAD"});
            this.httpMethodCombo.Location = new System.Drawing.Point(230, 111);
            this.httpMethodCombo.Name = "httpMethodCombo";
            this.httpMethodCombo.Size = new System.Drawing.Size(81, 20);
            this.httpMethodCombo.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(167, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "http方法：";
            // 
            // headerCombo
            // 
            this.headerCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.headerCombo.FormattingEnabled = true;
            this.headerCombo.Items.AddRange(new object[] {
            "谷歌",
            "IE10",
            "IE9",
            "火狐"});
            this.headerCombo.Location = new System.Drawing.Point(58, 111);
            this.headerCombo.Name = "headerCombo";
            this.headerCombo.Size = new System.Drawing.Size(87, 20);
            this.headerCombo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Header：";
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(58, 15);
            this.urlTextBox.Multiline = true;
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(254, 91);
            this.urlTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "地址：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.refreshDictBtn);
            this.groupBox2.Controls.Add(this.openDictPathBtn);
            this.groupBox2.Controls.Add(this.dictListView);
            this.groupBox2.Location = new System.Drawing.Point(337, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 166);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "字典（激活0个）";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(246, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "全不选";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(165, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "全选";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // refreshDictBtn
            // 
            this.refreshDictBtn.Location = new System.Drawing.Point(84, 15);
            this.refreshDictBtn.Name = "refreshDictBtn";
            this.refreshDictBtn.Size = new System.Drawing.Size(75, 23);
            this.refreshDictBtn.TabIndex = 2;
            this.refreshDictBtn.Text = "刷新字典";
            this.refreshDictBtn.UseVisualStyleBackColor = true;
            this.refreshDictBtn.Click += new System.EventHandler(this.RefreshDictBtn_Click);
            // 
            // openDictPathBtn
            // 
            this.openDictPathBtn.Location = new System.Drawing.Point(3, 15);
            this.openDictPathBtn.Name = "openDictPathBtn";
            this.openDictPathBtn.Size = new System.Drawing.Size(75, 23);
            this.openDictPathBtn.TabIndex = 1;
            this.openDictPathBtn.Text = "字典目录";
            this.openDictPathBtn.UseVisualStyleBackColor = true;
            this.openDictPathBtn.Click += new System.EventHandler(this.OpenDictPathBtn_Click);
            // 
            // dictListView
            // 
            this.dictListView.CheckBoxes = true;
            this.dictListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dictID,
            this.dictName,
            this.dictRows,
            this.dictSize});
            this.dictListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dictListView.FullRowSelect = true;
            this.dictListView.HideSelection = false;
            this.dictListView.Location = new System.Drawing.Point(3, 41);
            this.dictListView.Name = "dictListView";
            this.dictListView.Size = new System.Drawing.Size(326, 122);
            this.dictListView.TabIndex = 0;
            this.dictListView.UseCompatibleStateImageBehavior = false;
            this.dictListView.View = System.Windows.Forms.View.Details;
            this.dictListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.DictListView_ItemChecked);
            // 
            // dictID
            // 
            this.dictID.Text = "ID";
            this.dictID.Width = 35;
            // 
            // dictName
            // 
            this.dictName.Text = "字典名称";
            this.dictName.Width = 146;
            // 
            // dictRows
            // 
            this.dictRows.Text = "行数";
            // 
            // dictSize
            // 
            this.dictSize.Text = "大小";
            this.dictSize.Width = 80;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.sleepTimeCombo);
            this.groupBox3.Controls.Add(this.timeOutCombo);
            this.groupBox3.Controls.Add(this.threadSizeCombo);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(783, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(119, 166);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "配置参数";
            // 
            // sleepTimeCombo
            // 
            this.sleepTimeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sleepTimeCombo.FormattingEnabled = true;
            this.sleepTimeCombo.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "20",
            "30",
            "60",
            "120",
            "180",
            "240"});
            this.sleepTimeCombo.Location = new System.Drawing.Point(54, 136);
            this.sleepTimeCombo.Name = "sleepTimeCombo";
            this.sleepTimeCombo.Size = new System.Drawing.Size(59, 20);
            this.sleepTimeCombo.TabIndex = 5;
            // 
            // timeOutCombo
            // 
            this.timeOutCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeOutCombo.FormattingEnabled = true;
            this.timeOutCombo.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "30",
            "40",
            "50",
            "60"});
            this.timeOutCombo.Location = new System.Drawing.Point(54, 81);
            this.timeOutCombo.Name = "timeOutCombo";
            this.timeOutCombo.Size = new System.Drawing.Size(58, 20);
            this.timeOutCombo.TabIndex = 4;
            // 
            // threadSizeCombo
            // 
            this.threadSizeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.threadSizeCombo.FormattingEnabled = true;
            this.threadSizeCombo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "5",
            "10"});
            this.threadSizeCombo.Location = new System.Drawing.Point(55, 26);
            this.threadSizeCombo.Name = "threadSizeCombo";
            this.threadSizeCombo.Size = new System.Drawing.Size(58, 20);
            this.threadSizeCombo.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "延迟/s";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "超时/s";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "线程数";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.listView1);
            this.groupBox4.Location = new System.Drawing.Point(12, 180);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(965, 178);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.url,
            this.webStatus,
            this.contentType,
            this.length,
            this.runTime});
            this.listView1.ContextMenuStrip = this.resultCms;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 17);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(959, 158);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView1_ColumnClick);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListView1_KeyDown);
            // 
            // ID
            // 
            this.ID.Text = "ID";
            // 
            // url
            // 
            this.url.Text = "网页地址";
            this.url.Width = 455;
            // 
            // webStatus
            // 
            this.webStatus.Text = "HTTP响应";
            this.webStatus.Width = 102;
            // 
            // contentType
            // 
            this.contentType.Text = "文档类型";
            this.contentType.Width = 119;
            // 
            // length
            // 
            this.length.Text = "返回长度";
            this.length.Width = 104;
            // 
            // runTime
            // 
            this.runTime.Text = "用时[毫秒]";
            this.runTime.Width = 100;
            // 
            // resultCms
            // 
            this.resultCms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openUrl,
            this.copyUrl,
            this.exportResults});
            this.resultCms.Name = "resultCms";
            this.resultCms.Size = new System.Drawing.Size(140, 70);
            // 
            // openUrl
            // 
            this.openUrl.Name = "openUrl";
            this.openUrl.Size = new System.Drawing.Size(139, 22);
            this.openUrl.Text = "浏览器打开";
            this.openUrl.Click += new System.EventHandler(this.OpenUrl_Click);
            // 
            // copyUrl
            // 
            this.copyUrl.Name = "copyUrl";
            this.copyUrl.Size = new System.Drawing.Size(139, 22);
            this.copyUrl.Text = "复制选中url";
            this.copyUrl.Click += new System.EventHandler(this.CopyUrl_Click);
            // 
            // exportResults
            // 
            this.exportResults.Name = "exportResults";
            this.exportResults.Size = new System.Drawing.Size(139, 22);
            this.exportResults.Text = "导出结果";
            this.exportResults.Click += new System.EventHandler(this.ExportResults_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.logTextBox);
            this.groupBox5.Location = new System.Drawing.Point(13, 355);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(965, 160);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            // 
            // logTextBox
            // 
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Location = new System.Drawing.Point(3, 17);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(959, 140);
            this.logTextBox.TabIndex = 0;
            this.logTextBox.Text = string.Empty;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.progressBar,
            this.progressPercent,
            this.toolStripStatusLabel3,
            this.scanUseTime,
            this.toolStripStatusLabel5,
            this.scanThreadStatus,
            this.toolStripStatusLabel2,
            this.threadPoolStatus,
            this.toolStripStatusLabel7,
            this.scanSpeed});
            this.statusStrip1.Location = new System.Drawing.Point(0, 518);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(986, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel1.Text = "进度";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(150, 16);
            // 
            // progressPercent
            // 
            this.progressPercent.Name = "progressPercent";
            this.progressPercent.Size = new System.Drawing.Size(26, 17);
            this.progressPercent.Text = "0%";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel3.Text = "用时：";
            // 
            // scanUseTime
            // 
            this.scanUseTime.Name = "scanUseTime";
            this.scanUseTime.Size = new System.Drawing.Size(15, 17);
            this.scanUseTime.Text = "0";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel5.Text = "线程：";
            // 
            // scanThreadStatus
            // 
            this.scanThreadStatus.Name = "scanThreadStatus";
            this.scanThreadStatus.Size = new System.Drawing.Size(27, 17);
            this.scanThreadStatus.Text = "0/0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(92, 17);
            this.toolStripStatusLabel2.Text = "预计检查队列：";
            // 
            // threadPoolStatus
            // 
            this.threadPoolStatus.Name = "threadPoolStatus";
            this.threadPoolStatus.Size = new System.Drawing.Size(15, 17);
            this.threadPoolStatus.Text = "0";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel7.Text = "速度：";
            // 
            // scanSpeed
            // 
            this.scanSpeed.Name = "scanSpeed";
            this.scanSpeed.Size = new System.Drawing.Size(15, 17);
            this.scanSpeed.Text = "0";
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(908, 23);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(66, 23);
            this.startBtn.TabIndex = 6;
            this.startBtn.Text = "开始扫描";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(908, 85);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(66, 23);
            this.stopBtn.TabIndex = 7;
            this.stopBtn.Text = "停止扫描";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // exportBtn
            // 
            this.exportBtn.Location = new System.Drawing.Point(908, 147);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(66, 23);
            this.exportBtn.TabIndex = 8;
            this.exportBtn.Text = "导出结果";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.ExportBtn_Click);
            // 
            // scanTimer
            // 
            this.scanTimer.Interval = 1000;
            this.scanTimer.Tick += new System.EventHandler(this.ScanTimer_Tick);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.HelpLabel);
            this.groupBox6.Controls.Add(this.loginCheckBox);
            this.groupBox6.Controls.Add(this.checkBox3);
            this.groupBox6.Controls.Add(this.editorCheckBox);
            this.groupBox6.Controls.Add(this.loopCheckBox);
            this.groupBox6.Location = new System.Drawing.Point(675, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(100, 166);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "扫描策略_TODO";
            // 
            // HelpLabel
            // 
            this.HelpLabel.AutoSize = true;
            this.HelpLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HelpLabel.Location = new System.Drawing.Point(13, 146);
            this.HelpLabel.Name = "HelpLabel";
            this.HelpLabel.Size = new System.Drawing.Size(83, 12);
            this.HelpLabel.TabIndex = 10;
            this.HelpLabel.TabStop = true;
            this.HelpLabel.Text = "必看帮助说明";
            this.HelpLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HelpLabel_LinkClicked);
            // 
            // loginCheckBox
            // 
            this.loginCheckBox.AutoSize = true;
            this.loginCheckBox.Location = new System.Drawing.Point(15, 119);
            this.loginCheckBox.Name = "loginCheckBox";
            this.loginCheckBox.Size = new System.Drawing.Size(72, 16);
            this.loginCheckBox.TabIndex = 3;
            this.loginCheckBox.Text = "入口素描";
            this.toolTip1.SetToolTip(this.loginCheckBox, "扫描各种等级的登录入口");
            this.loginCheckBox.UseVisualStyleBackColor = true;
            this.loginCheckBox.CheckedChanged += new System.EventHandler(this.LoginCheckBox_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(15, 88);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(72, 16);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "指纹素描";
            this.toolTip1.SetToolTip(this.checkBox3, "cms，thinkphp，wp等模板指纹探测");
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // editorCheckBox
            // 
            this.editorCheckBox.AutoSize = true;
            this.editorCheckBox.Location = new System.Drawing.Point(15, 57);
            this.editorCheckBox.Name = "editorCheckBox";
            this.editorCheckBox.Size = new System.Drawing.Size(84, 16);
            this.editorCheckBox.TabIndex = 1;
            this.editorCheckBox.Text = "编辑器素描";
            this.toolTip1.SetToolTip(this.editorCheckBox, "探测网站所用的编辑器控件，经常会被用来后续的文件上传漏洞检测。");
            this.editorCheckBox.UseVisualStyleBackColor = true;
            this.editorCheckBox.CheckedChanged += new System.EventHandler(this.EditorCheckBox_CheckedChanged);
            // 
            // loopCheckBox
            // 
            this.loopCheckBox.AutoSize = true;
            this.loopCheckBox.Location = new System.Drawing.Point(15, 26);
            this.loopCheckBox.Name = "loopCheckBox";
            this.loopCheckBox.Size = new System.Drawing.Size(66, 16);
            this.loopCheckBox.TabIndex = 0;
            this.loopCheckBox.Text = "403遍历";
            this.toolTip1.SetToolTip(this.loopCheckBox, "利用403返回码和专用字典，高效遍历网站目录。");
            this.loopCheckBox.UseVisualStyleBackColor = true;
            // 
            // WebScanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 540);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.exportBtn);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WebScanForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "目录扫描";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.resultCms.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader url;
        private System.Windows.Forms.ColumnHeader webStatus;
        private System.Windows.Forms.ColumnHeader contentType;
        private System.Windows.Forms.ColumnHeader length;
        private System.Windows.Forms.ColumnHeader runTime;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel progressPercent;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel scanUseTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel scanThreadStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel scanSpeed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox httpMethodCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox headerCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button refreshDictBtn;
        private System.Windows.Forms.Button openDictPathBtn;
        private System.Windows.Forms.ListView dictListView;
        private System.Windows.Forms.ColumnHeader dictID;
        private System.Windows.Forms.ColumnHeader dictName;
        private System.Windows.Forms.ColumnHeader dictRows;
        private System.Windows.Forms.ColumnHeader dictSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button exportBtn;
        private System.Windows.Forms.ComboBox sleepTimeCombo;
        private System.Windows.Forms.ComboBox timeOutCombo;
        private System.Windows.Forms.ComboBox threadSizeCombo;
        private System.Windows.Forms.CheckBox crawlerCheckBox;
        private System.Windows.Forms.TextBox statusCodeTextBox;
        private System.Windows.Forms.Timer scanTimer;
        private System.Windows.Forms.ContextMenuStrip resultCms;
        private System.Windows.Forms.ToolStripMenuItem openUrl;
        private System.Windows.Forms.ToolStripMenuItem copyUrl;
        private System.Windows.Forms.ToolStripMenuItem exportResults;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel threadPoolStatus;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox loginCheckBox;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox editorCheckBox;
        private System.Windows.Forms.CheckBox loopCheckBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel HelpLabel;
    }
}