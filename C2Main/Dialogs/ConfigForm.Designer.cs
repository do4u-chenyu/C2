namespace C2.Dialogs
{
    partial class ConfigForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.pythonConfigPage = new System.Windows.Forms.TabPage();
            this.chosenPythonLable = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pythonConfigCancelButton = new System.Windows.Forms.Button();
            this.pythonConfigOkButton = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.interpreterFFPColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aliasColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chosenColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.browseButton = new System.Windows.Forms.Button();
            this.pythonFFPTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gisMapConfigPage = new System.Windows.Forms.TabPage();
            this.gisMapCancelButton = new System.Windows.Forms.Button();
            this.gisMapOKButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.baiduAPIKey = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.baiduScaleTB = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.baiduLatTB = new System.Windows.Forms.TextBox();
            this.baiduLonTB = new System.Windows.Forms.TextBox();
            this.baiduGISTB = new System.Windows.Forms.TextBox();
            this.userModelConfigPage = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.userModelCancelButton = new System.Windows.Forms.Button();
            this.userModelOkButton = new System.Windows.Forms.Button();
            this.userModelTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.aboutConfigPage = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.version = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.aboutCancelButton = new System.Windows.Forms.Button();
            this.aboutOkButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.WFDConfigPage = new System.Windows.Forms.TabPage();
            this.WFDResetButton = new System.Windows.Forms.Button();
            this.WFDInfoTB = new System.Windows.Forms.TextBox();
            this.WFDScreenshotTB = new System.Windows.Forms.TextBox();
            this.WFDResultTB = new System.Windows.Forms.TextBox();
            this.WFDClassifierTB = new System.Windows.Forms.TextBox();
            this.WFDLoginTB = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.WFDCancelButton = new System.Windows.Forms.Button();
            this.WFDOKButton = new System.Windows.Forms.Button();
            this.pythonOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.mainTabControl.SuspendLayout();
            this.pythonConfigPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.gisMapConfigPage.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.userModelConfigPage.SuspendLayout();
            this.aboutConfigPage.SuspendLayout();
            this.WFDConfigPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.pythonConfigPage);
            this.mainTabControl.Controls.Add(this.gisMapConfigPage);
            this.mainTabControl.Controls.Add(this.userModelConfigPage);
            this.mainTabControl.Controls.Add(this.aboutConfigPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.ShowToolTips = true;
            this.mainTabControl.Size = new System.Drawing.Size(642, 410);
            this.mainTabControl.TabIndex = 0;
            // 
            // pythonConfigPage
            // 
            this.pythonConfigPage.Controls.Add(this.chosenPythonLable);
            this.pythonConfigPage.Controls.Add(this.label7);
            this.pythonConfigPage.Controls.Add(this.pythonConfigCancelButton);
            this.pythonConfigPage.Controls.Add(this.pythonConfigOkButton);
            this.pythonConfigPage.Controls.Add(this.dataGridView);
            this.pythonConfigPage.Controls.Add(this.browseButton);
            this.pythonConfigPage.Controls.Add(this.pythonFFPTextBox);
            this.pythonConfigPage.Controls.Add(this.label1);
            this.pythonConfigPage.Location = new System.Drawing.Point(4, 26);
            this.pythonConfigPage.Name = "pythonConfigPage";
            this.pythonConfigPage.Padding = new System.Windows.Forms.Padding(3);
            this.pythonConfigPage.Size = new System.Drawing.Size(634, 380);
            this.pythonConfigPage.TabIndex = 1;
            this.pythonConfigPage.Text = "Python引擎";
            this.pythonConfigPage.ToolTipText = "导入本地的Python虚拟机,用来运行使用者导入的外部自定义PY脚本,支持导入多种版本的Python虚拟机";
            this.pythonConfigPage.UseVisualStyleBackColor = true;
            // 
            // chosenPythonLable
            // 
            this.chosenPythonLable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chosenPythonLable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chosenPythonLable.Location = new System.Drawing.Point(131, 352);
            this.chosenPythonLable.Name = "chosenPythonLable";
            this.chosenPythonLable.Size = new System.Drawing.Size(200, 20);
            this.chosenPythonLable.TabIndex = 7;
            this.chosenPythonLable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 355);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "默认Python虚拟机 :";
            this.toolTip1.SetToolTip(this.label7, "Py算子默认使用的虚拟机");
            // 
            // pythonConfigCancelButton
            // 
            this.pythonConfigCancelButton.Location = new System.Drawing.Point(551, 349);
            this.pythonConfigCancelButton.Name = "pythonConfigCancelButton";
            this.pythonConfigCancelButton.Size = new System.Drawing.Size(75, 23);
            this.pythonConfigCancelButton.TabIndex = 5;
            this.pythonConfigCancelButton.Text = "取消";
            this.toolTip1.SetToolTip(this.pythonConfigCancelButton, "点击\"取消\"关闭配置窗口并返回");
            this.pythonConfigCancelButton.UseVisualStyleBackColor = true;
            this.pythonConfigCancelButton.Click += new System.EventHandler(this.PythonConfigCancelButton_Click);
            // 
            // pythonConfigOkButton
            // 
            this.pythonConfigOkButton.Location = new System.Drawing.Point(458, 349);
            this.pythonConfigOkButton.Name = "pythonConfigOkButton";
            this.pythonConfigOkButton.Size = new System.Drawing.Size(75, 23);
            this.pythonConfigOkButton.TabIndex = 4;
            this.pythonConfigOkButton.Text = "确认";
            this.toolTip1.SetToolTip(this.pythonConfigOkButton, "点击\"确认\"保存当前配置");
            this.pythonConfigOkButton.UseVisualStyleBackColor = true;
            this.pythonConfigOkButton.Click += new System.EventHandler(this.PythonConfigOkButton_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.interpreterFFPColumn,
            this.aliasColumn,
            this.chosenColumn});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridView.Location = new System.Drawing.Point(6, 93);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 62;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView.ShowEditingIcon = false;
            this.dataGridView.Size = new System.Drawing.Size(620, 250);
            this.dataGridView.TabIndex = 3;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellContentClick);
            this.dataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellEndEdit);
            this.dataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DataGridView_CellValidating);
            this.dataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.DataGridView_RowsRemoved);
            // 
            // interpreterFFPColumn
            // 
            this.interpreterFFPColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.interpreterFFPColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.interpreterFFPColumn.FillWeight = 320F;
            this.interpreterFFPColumn.HeaderText = "虚拟机路径";
            this.interpreterFFPColumn.MinimumWidth = 8;
            this.interpreterFFPColumn.Name = "interpreterFFPColumn";
            this.interpreterFFPColumn.ReadOnly = true;
            this.interpreterFFPColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.interpreterFFPColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.interpreterFFPColumn.Width = 422;
            // 
            // aliasColumn
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.aliasColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.aliasColumn.FillWeight = 75F;
            this.aliasColumn.HeaderText = "别名";
            this.aliasColumn.MinimumWidth = 8;
            this.aliasColumn.Name = "aliasColumn";
            this.aliasColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.aliasColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.aliasColumn.ToolTipText = "自定义的别名，用来区分不同版本的Python解释器";
            // 
            // chosenColumn
            // 
            this.chosenColumn.FillWeight = 30F;
            this.chosenColumn.HeaderText = "选中";
            this.chosenColumn.MinimumWidth = 8;
            this.chosenColumn.Name = "chosenColumn";
            this.chosenColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.chosenColumn.ToolTipText = "选中一个作为默认的Python虚拟机";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(580, 49);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(49, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "浏览+";
            this.browseButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.browseButton, "手动选择当前系统的Python解释器");
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.PythonBrowseButton_Click);
            // 
            // pythonFFPTextBox
            // 
            this.pythonFFPTextBox.BackColor = System.Drawing.Color.White;
            this.pythonFFPTextBox.Location = new System.Drawing.Point(5, 49);
            this.pythonFFPTextBox.Name = "pythonFFPTextBox";
            this.pythonFFPTextBox.ReadOnly = true;
            this.pythonFFPTextBox.Size = new System.Drawing.Size(572, 23);
            this.pythonFFPTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pyhton虚拟机路径(Python Interpreter)";
            // 
            // gisMapConfigPage
            // 
            this.gisMapConfigPage.Controls.Add(this.gisMapCancelButton);
            this.gisMapConfigPage.Controls.Add(this.gisMapOKButton);
            this.gisMapConfigPage.Controls.Add(this.tabControl1);
            this.gisMapConfigPage.Location = new System.Drawing.Point(4, 26);
            this.gisMapConfigPage.Name = "gisMapConfigPage";
            this.gisMapConfigPage.Size = new System.Drawing.Size(634, 380);
            this.gisMapConfigPage.TabIndex = 5;
            this.gisMapConfigPage.Text = "图上作战";
            this.gisMapConfigPage.ToolTipText = "将数据展示到地图上的简易工具";
            this.gisMapConfigPage.UseVisualStyleBackColor = true;
            // 
            // gisMapCancelButton
            // 
            this.gisMapCancelButton.Location = new System.Drawing.Point(551, 349);
            this.gisMapCancelButton.Name = "gisMapCancelButton";
            this.gisMapCancelButton.Size = new System.Drawing.Size(75, 23);
            this.gisMapCancelButton.TabIndex = 7;
            this.gisMapCancelButton.Text = "取消";
            this.gisMapCancelButton.UseVisualStyleBackColor = true;
            this.gisMapCancelButton.Click += new System.EventHandler(this.GisMapCancelButton_Click);
            // 
            // gisMapOKButton
            // 
            this.gisMapOKButton.Location = new System.Drawing.Point(458, 349);
            this.gisMapOKButton.Name = "gisMapOKButton";
            this.gisMapOKButton.Size = new System.Drawing.Size(75, 23);
            this.gisMapOKButton.TabIndex = 6;
            this.gisMapOKButton.Text = "确认";
            this.gisMapOKButton.UseVisualStyleBackColor = true;
            this.gisMapOKButton.Click += new System.EventHandler(this.GisMapOKButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(634, 343);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.baiduAPIKey);
            this.tabPage1.Controls.Add(this.label24);
            this.tabPage1.Controls.Add(this.baiduScaleTB);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.baiduLatTB);
            this.tabPage1.Controls.Add(this.baiduLonTB);
            this.tabPage1.Controls.Add(this.baiduGISTB);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(626, 313);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "百度地图(外网)";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // baiduAPIKey
            // 
            this.baiduAPIKey.Location = new System.Drawing.Point(104, 14);
            this.baiduAPIKey.Name = "baiduAPIKey";
            this.baiduAPIKey.Size = new System.Drawing.Size(460, 23);
            this.baiduAPIKey.TabIndex = 16;
            this.toolTip1.SetToolTip(this.baiduAPIKey, "百度地图服务所需的Key");
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(12, 18);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(91, 17);
            this.label24.TabIndex = 14;
            this.label24.Text = "百度API Key：";
            // 
            // baiduScaleTB
            // 
            this.baiduScaleTB.Location = new System.Drawing.Point(104, 103);
            this.baiduScaleTB.Name = "baiduScaleTB";
            this.baiduScaleTB.Size = new System.Drawing.Size(460, 23);
            this.baiduScaleTB.TabIndex = 13;
            this.toolTip1.SetToolTip(this.baiduScaleTB, "初始缩放比值请设置5-9区间");
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(46, 107);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(56, 17);
            this.label15.TabIndex = 12;
            this.label15.Text = "缩放比：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(35, 77);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 17);
            this.label16.TabIndex = 11;
            this.label16.Text = "初始纬度 :";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(35, 47);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 17);
            this.label17.TabIndex = 10;
            this.label17.Text = "初始经度 :";
            // 
            // baiduLatTB
            // 
            this.baiduLatTB.Location = new System.Drawing.Point(104, 72);
            this.baiduLatTB.Name = "baiduLatTB";
            this.baiduLatTB.Size = new System.Drawing.Size(460, 23);
            this.baiduLatTB.TabIndex = 9;
            this.toolTip1.SetToolTip(this.baiduLatTB, "请输入正确纬度！");
            // 
            // baiduLonTB
            // 
            this.baiduLonTB.Location = new System.Drawing.Point(104, 43);
            this.baiduLonTB.Name = "baiduLonTB";
            this.baiduLonTB.Size = new System.Drawing.Size(460, 23);
            this.baiduLonTB.TabIndex = 8;
            this.toolTip1.SetToolTip(this.baiduLonTB, "请输入正确经度！");
            // 
            // baiduGISTB
            // 
            this.baiduGISTB.Location = new System.Drawing.Point(3, 176);
            this.baiduGISTB.Multiline = true;
            this.baiduGISTB.Name = "baiduGISTB";
            this.baiduGISTB.ReadOnly = true;
            this.baiduGISTB.Size = new System.Drawing.Size(620, 102);
            this.baiduGISTB.TabIndex = 7;
            this.baiduGISTB.Text = "基于百度地图服务的图上作战功能，支持图上打点，轨迹图，区域图，热力图和自定义的Javascript脚本\r\n";
            // 
            // userModelConfigPage
            // 
            this.userModelConfigPage.Controls.Add(this.label4);
            this.userModelConfigPage.Controls.Add(this.label3);
            this.userModelConfigPage.Controls.Add(this.userModelCancelButton);
            this.userModelConfigPage.Controls.Add(this.userModelOkButton);
            this.userModelConfigPage.Controls.Add(this.userModelTextBox);
            this.userModelConfigPage.Controls.Add(this.label2);
            this.userModelConfigPage.Location = new System.Drawing.Point(4, 26);
            this.userModelConfigPage.Name = "userModelConfigPage";
            this.userModelConfigPage.Padding = new System.Windows.Forms.Padding(3);
            this.userModelConfigPage.Size = new System.Drawing.Size(634, 380);
            this.userModelConfigPage.TabIndex = 0;
            this.userModelConfigPage.Text = "用户文档路径";
            this.userModelConfigPage.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "由程序在安装时指定。";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(371, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "用户文档目录存储了当前所有用户的文档,配置信息和运算临时文件。";
            // 
            // userModelCancelButton
            // 
            this.userModelCancelButton.Location = new System.Drawing.Point(551, 349);
            this.userModelCancelButton.Name = "userModelCancelButton";
            this.userModelCancelButton.Size = new System.Drawing.Size(75, 23);
            this.userModelCancelButton.TabIndex = 3;
            this.userModelCancelButton.Text = "取消";
            this.userModelCancelButton.UseVisualStyleBackColor = true;
            this.userModelCancelButton.Click += new System.EventHandler(this.AboutCancelButton_Click);
            // 
            // userModelOkButton
            // 
            this.userModelOkButton.Location = new System.Drawing.Point(458, 349);
            this.userModelOkButton.Name = "userModelOkButton";
            this.userModelOkButton.Size = new System.Drawing.Size(75, 23);
            this.userModelOkButton.TabIndex = 2;
            this.userModelOkButton.Text = "确认";
            this.userModelOkButton.UseVisualStyleBackColor = true;
            this.userModelOkButton.Click += new System.EventHandler(this.AboutCancelButton_Click);
            // 
            // userModelTextBox
            // 
            this.userModelTextBox.Location = new System.Drawing.Point(6, 77);
            this.userModelTextBox.Name = "userModelTextBox";
            this.userModelTextBox.ReadOnly = true;
            this.userModelTextBox.Size = new System.Drawing.Size(620, 23);
            this.userModelTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "用户文档存储路径:";
            // 
            // aboutConfigPage
            // 
            this.aboutConfigPage.Controls.Add(this.button2);
            this.aboutConfigPage.Controls.Add(this.button1);
            this.aboutConfigPage.Controls.Add(this.label8);
            this.aboutConfigPage.Controls.Add(this.label9);
            this.aboutConfigPage.Controls.Add(this.version);
            this.aboutConfigPage.Controls.Add(this.textBox4);
            this.aboutConfigPage.Controls.Add(this.button7);
            this.aboutConfigPage.Controls.Add(this.textBox3);
            this.aboutConfigPage.Controls.Add(this.aboutCancelButton);
            this.aboutConfigPage.Controls.Add(this.aboutOkButton);
            this.aboutConfigPage.Controls.Add(this.label6);
            this.aboutConfigPage.Controls.Add(this.label5);
            this.aboutConfigPage.Location = new System.Drawing.Point(4, 26);
            this.aboutConfigPage.Name = "aboutConfigPage";
            this.aboutConfigPage.Padding = new System.Windows.Forms.Padding(3);
            this.aboutConfigPage.Size = new System.Drawing.Size(634, 380);
            this.aboutConfigPage.TabIndex = 3;
            this.aboutConfigPage.Text = "关于和注册";
            this.aboutConfigPage.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(198, 299);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "战术手册下载";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(102, 299);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "内网版下载";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 305);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 17);
            this.label8.TabIndex = 11;
            this.label8.Text = "在线更新";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(130, 268);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(122, 17);
            this.label9.TabIndex = 10;
            this.label9.Text = "到期时间 2022.09.10";
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.Location = new System.Drawing.Point(9, 268);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(47, 17);
            this.version.TabIndex = 9;
            this.version.Text = "V:2.1.9";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.textBox4.Location = new System.Drawing.Point(11, 50);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(537, 133);
            this.textBox4.TabIndex = 8;
            this.textBox4.Text = "分析师单兵作战装备,是一系列分析工具的沉淀集合,并覆盖了历史战例中各种成功经验，旨在提升分析师的单兵和小团队作战能力,在独立环境下能够以一当十,快速展开各类型分析" +
    "实战攻坚任务。装备包含成熟的战术手册、分析笔记、各种实用小工具、智能分析工具和一系列高阶分析工具等，轻量精干、携带方便，是分析师实战工作中强有力的武器库。";
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(489, 229);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(59, 23);
            this.button7.TabIndex = 7;
            this.button7.Text = "提交";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.White;
            this.textBox3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBox3.Location = new System.Drawing.Point(11, 229);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(463, 23);
            this.textBox3.TabIndex = 6;
            this.textBox3.Text = "AH8F-6H7C-9VMF-4UOP";
            // 
            // aboutCancelButton
            // 
            this.aboutCancelButton.Location = new System.Drawing.Point(551, 349);
            this.aboutCancelButton.Name = "aboutCancelButton";
            this.aboutCancelButton.Size = new System.Drawing.Size(75, 23);
            this.aboutCancelButton.TabIndex = 5;
            this.aboutCancelButton.Text = "取消";
            this.aboutCancelButton.UseVisualStyleBackColor = true;
            this.aboutCancelButton.Click += new System.EventHandler(this.AboutCancelButton_Click);
            // 
            // aboutOkButton
            // 
            this.aboutOkButton.Location = new System.Drawing.Point(458, 349);
            this.aboutOkButton.Name = "aboutOkButton";
            this.aboutOkButton.Size = new System.Drawing.Size(75, 23);
            this.aboutOkButton.TabIndex = 4;
            this.aboutOkButton.Text = "确认";
            this.aboutOkButton.UseVisualStyleBackColor = true;
            this.aboutOkButton.Click += new System.EventHandler(this.AboutCancelButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 198);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "注册(填写注册码)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(8, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "关于";
            // 
            // WFDConfigPage
            // 
            this.WFDConfigPage.Controls.Add(this.WFDResetButton);
            this.WFDConfigPage.Controls.Add(this.WFDInfoTB);
            this.WFDConfigPage.Controls.Add(this.WFDScreenshotTB);
            this.WFDConfigPage.Controls.Add(this.WFDResultTB);
            this.WFDConfigPage.Controls.Add(this.WFDClassifierTB);
            this.WFDConfigPage.Controls.Add(this.WFDLoginTB);
            this.WFDConfigPage.Controls.Add(this.label29);
            this.WFDConfigPage.Controls.Add(this.label28);
            this.WFDConfigPage.Controls.Add(this.label27);
            this.WFDConfigPage.Controls.Add(this.label25);
            this.WFDConfigPage.Controls.Add(this.WFDCancelButton);
            this.WFDConfigPage.Controls.Add(this.WFDOKButton);
            this.WFDConfigPage.Location = new System.Drawing.Point(4, 26);
            this.WFDConfigPage.Name = "WFDConfigPage";
            this.WFDConfigPage.Padding = new System.Windows.Forms.Padding(3);
            this.WFDConfigPage.Size = new System.Drawing.Size(634, 380);
            this.WFDConfigPage.TabIndex = 7;
            this.WFDConfigPage.Text = "网站侦察兵";
            this.WFDConfigPage.UseVisualStyleBackColor = true;
            // 
            // WFDResetButton
            // 
            this.WFDResetButton.Location = new System.Drawing.Point(364, 349);
            this.WFDResetButton.Name = "WFDResetButton";
            this.WFDResetButton.Size = new System.Drawing.Size(75, 23);
            this.WFDResetButton.TabIndex = 15;
            this.WFDResetButton.Text = "重置";
            this.WFDResetButton.UseVisualStyleBackColor = true;
            this.WFDResetButton.Click += new System.EventHandler(this.WFDResetButton_Click);
            // 
            // WFDInfoTB
            // 
            this.WFDInfoTB.Location = new System.Drawing.Point(8, 238);
            this.WFDInfoTB.Multiline = true;
            this.WFDInfoTB.Name = "WFDInfoTB";
            this.WFDInfoTB.ReadOnly = true;
            this.WFDInfoTB.Size = new System.Drawing.Size(620, 81);
            this.WFDInfoTB.TabIndex = 14;
            this.WFDInfoTB.Text = "网站侦察兵工具是一个部署在公网上的api，核心功能是对网站进行爬取、分类并截图，最终将判别结果返回给用户。";
            // 
            // WFDScreenshotTB
            // 
            this.WFDScreenshotTB.Location = new System.Drawing.Point(128, 176);
            this.WFDScreenshotTB.Name = "WFDScreenshotTB";
            this.WFDScreenshotTB.Size = new System.Drawing.Size(463, 21);
            this.WFDScreenshotTB.TabIndex = 13;
            this.WFDScreenshotTB.Text = "https://113.31.119.85:53374/apis/Screenshot";
            // 
            // WFDResultTB
            // 
            this.WFDResultTB.Location = new System.Drawing.Point(128, 126);
            this.WFDResultTB.Name = "WFDResultTB";
            this.WFDResultTB.Size = new System.Drawing.Size(463, 21);
            this.WFDResultTB.TabIndex = 12;
            this.WFDResultTB.Text = "https://113.31.119.85:53374/apis/detection/task/result";
            // 
            // WFDClassifierTB
            // 
            this.WFDClassifierTB.Location = new System.Drawing.Point(128, 77);
            this.WFDClassifierTB.Name = "WFDClassifierTB";
            this.WFDClassifierTB.Size = new System.Drawing.Size(463, 21);
            this.WFDClassifierTB.TabIndex = 11;
            this.WFDClassifierTB.Text = "https://113.31.119.85:53374/apis/pro_classifier_api";
            // 
            // WFDLoginTB
            // 
            this.WFDLoginTB.Location = new System.Drawing.Point(128, 28);
            this.WFDLoginTB.Name = "WFDLoginTB";
            this.WFDLoginTB.Size = new System.Drawing.Size(463, 21);
            this.WFDLoginTB.TabIndex = 10;
            this.WFDLoginTB.Text = "https://113.31.119.85:53374/apis/Login";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(38, 179);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(65, 12);
            this.label29.TabIndex = 9;
            this.label29.Text = "获取截图：";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(38, 129);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(65, 12);
            this.label28.TabIndex = 8;
            this.label28.Text = "获取结果：";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(38, 80);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(65, 12);
            this.label27.TabIndex = 7;
            this.label27.Text = "任务下发：";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(38, 31);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(65, 12);
            this.label25.TabIndex = 6;
            this.label25.Text = "登陆验证：";
            // 
            // WFDCancelButton
            // 
            this.WFDCancelButton.Location = new System.Drawing.Point(551, 349);
            this.WFDCancelButton.Name = "WFDCancelButton";
            this.WFDCancelButton.Size = new System.Drawing.Size(75, 23);
            this.WFDCancelButton.TabIndex = 5;
            this.WFDCancelButton.Text = "取消";
            this.WFDCancelButton.UseVisualStyleBackColor = true;
            this.WFDCancelButton.Click += new System.EventHandler(this.WFDCancelButton_Click);
            // 
            // WFDOKButton
            // 
            this.WFDOKButton.Location = new System.Drawing.Point(458, 349);
            this.WFDOKButton.Name = "WFDOKButton";
            this.WFDOKButton.Size = new System.Drawing.Size(75, 23);
            this.WFDOKButton.TabIndex = 4;
            this.WFDOKButton.Text = "确认";
            this.WFDOKButton.UseVisualStyleBackColor = true;
            this.WFDOKButton.Click += new System.EventHandler(this.WFDOKButton_Click);
            // 
            // pythonOpenFileDialog
            // 
            this.pythonOpenFileDialog.DefaultExt = "exe";
            this.pythonOpenFileDialog.Filter = "Python解释器 |python.exe|可执行文件|*.exe";
            this.pythonOpenFileDialog.RestoreDirectory = true;
            this.pythonOpenFileDialog.Title = "选择系统中已安装的Python解释器";
            // 
            // ConfigForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(642, 410);
            this.Controls.Add(this.mainTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "首选项";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.mainTabControl.ResumeLayout(false);
            this.pythonConfigPage.ResumeLayout(false);
            this.pythonConfigPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.gisMapConfigPage.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.userModelConfigPage.ResumeLayout(false);
            this.userModelConfigPage.PerformLayout();
            this.aboutConfigPage.ResumeLayout(false);
            this.aboutConfigPage.PerformLayout();
            this.WFDConfigPage.ResumeLayout(false);
            this.WFDConfigPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage userModelConfigPage;
        private System.Windows.Forms.TabPage pythonConfigPage;
        private System.Windows.Forms.TabPage aboutConfigPage;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox pythonFFPTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog pythonOpenFileDialog;
        private System.Windows.Forms.Button pythonConfigCancelButton;
        private System.Windows.Forms.Button pythonConfigOkButton;
        private System.Windows.Forms.TextBox userModelTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button userModelCancelButton;
        private System.Windows.Forms.Button userModelOkButton;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button aboutCancelButton;
        private System.Windows.Forms.Button aboutOkButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label chosenPythonLable;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label version;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn interpreterFFPColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aliasColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chosenColumn;
        private System.Windows.Forms.TabPage gisMapConfigPage;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button gisMapCancelButton;
        private System.Windows.Forms.Button gisMapOKButton;
        private System.Windows.Forms.TextBox baiduGISTB;
        private System.Windows.Forms.TextBox baiduScaleTB;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox baiduLatTB;
        private System.Windows.Forms.TextBox baiduLonTB;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox baiduAPIKey;
        private System.Windows.Forms.TabPage WFDConfigPage;
        private System.Windows.Forms.Button WFDCancelButton;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox WFDInfoTB;
        private System.Windows.Forms.TextBox WFDScreenshotTB;
        private System.Windows.Forms.TextBox WFDResultTB;
        private System.Windows.Forms.TextBox WFDClassifierTB;
        private System.Windows.Forms.TextBox WFDLoginTB;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button WFDResetButton;
        private System.Windows.Forms.Button WFDOKButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}