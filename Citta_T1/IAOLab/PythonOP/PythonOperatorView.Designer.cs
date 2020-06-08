namespace Citta_T1.OperatorViews
{
    partial class PythonOperatorView
    {
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
            this.pythonChosenComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pyParamTextBox = new System.Windows.Forms.TextBox();
            this.noOutputFileRadio = new System.Windows.Forms.RadioButton();
            this.rsFullFileNameTextBox = new System.Windows.Forms.TextBox();
            this.browseChosenRadioButton = new System.Windows.Forms.RadioButton();
            this.paramRadioButton = new System.Windows.Forms.RadioButton();
            this.stdoutRadioButton = new System.Windows.Forms.RadioButton();
            this.browseChosenTextBox = new System.Windows.Forms.TextBox();
            this.paramPrefixTagTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pyBrowseButton = new System.Windows.Forms.Button();
            this.rsChosenButton = new System.Windows.Forms.Button();
            this.paramInputFileFullPath = new System.Windows.Forms.TextBox();
            this.paramInputFileRadio = new System.Windows.Forms.RadioButton();
            this.paramInputFileTextBox = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pyFullFilePathTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.previewCmdGroup = new System.Windows.Forms.GroupBox();
            this.previewCmdText = new System.Windows.Forms.TextBox();
            this.gbkRadio = new System.Windows.Forms.RadioButton();
            this.utfRadio = new System.Windows.Forms.RadioButton();
            this.helpLabel = new System.Windows.Forms.Label();
            this.otherSeparatorText = new System.Windows.Forms.TextBox();
            this.otherSeparatorRadio = new System.Windows.Forms.RadioButton();
            this.commaRadio = new System.Windows.Forms.RadioButton();
            this.tabRadio = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.inputFileSettingTab = new System.Windows.Forms.TabPage();
            this.noInputFileRadio = new System.Windows.Forms.RadioButton();
            this.outputFileSettingTab = new System.Windows.Forms.TabPage();
            this.outputFileEncodeSettingGroup = new System.Windows.Forms.GroupBox();
            this.outputFileSeparatorSettingGroup = new System.Windows.Forms.GroupBox();
            this.previewCmdGroup.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.inputFileSettingTab.SuspendLayout();
            this.outputFileSettingTab.SuspendLayout();
            this.outputFileEncodeSettingGroup.SuspendLayout();
            this.outputFileSeparatorSettingGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataSourceTB0
            // 
            this.dataSourceTB0.Location = new System.Drawing.Point(137, 15);
            this.dataSourceTB0.Size = new System.Drawing.Size(150, 23);
            this.dataSourceTB0.TabIndex = 12;
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Location = new System.Drawing.Point(462, 437);
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            this.cancelButton.TabIndex = 2;
            // 
            // confirmButton
            // 
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Location = new System.Drawing.Point(377, 437);
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            this.confirmButton.TabIndex = 1;
            // 
            // comboBox0
            // 
            this.comboBox0.Visible = false;
            // 
            // pythonChosenComboBox
            // 
            this.pythonChosenComboBox.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.pythonChosenComboBox.FormattingEnabled = true;
            this.pythonChosenComboBox.Location = new System.Drawing.Point(137, 49);
            this.pythonChosenComboBox.Name = "pythonChosenComboBox";
            this.pythonChosenComboBox.Size = new System.Drawing.Size(150, 25);
            this.pythonChosenComboBox.TabIndex = 3;
            this.pythonChosenComboBox.Text = "未配置Python虚拟机";
            this.toolTip1.SetToolTip(this.pythonChosenComboBox, "当前已配置的Python虚拟机,如果还没有配置,可以在首选项-Python引擎中配置");
            this.pythonChosenComboBox.SelectedIndexChanged += new System.EventHandler(this.PythonChosenComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(45, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 22);
            this.label1.TabIndex = 11;
            this.label1.Text = "数据信息：";
            this.toolTip1.SetToolTip(this.label1, "数据源名称");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 22);
            this.label2.TabIndex = 13;
            this.label2.Text = "Python虚拟机：";
            this.toolTip1.SetToolTip(this.label2, "当前已配置的Python虚拟机,如果还没有配置,可以在首选项-Python引擎中配置");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(15, 122);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 22);
            this.label4.TabIndex = 16;
            this.label4.Text = "脚本传入参数：";
            this.toolTip1.SetToolTip(this.label4, "脚本需要传入的其他参数");
            // 
            // pyParamTextBox
            // 
            this.pyParamTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pyParamTextBox.Location = new System.Drawing.Point(137, 122);
            this.pyParamTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.pyParamTextBox.Name = "pyParamTextBox";
            this.pyParamTextBox.Size = new System.Drawing.Size(367, 23);
            this.pyParamTextBox.TabIndex = 17;
            this.pyParamTextBox.TextChanged += new System.EventHandler(this.PyParamTextBox_TextChanged);
            // 
            // noOutputFileRadio
            // 
            this.noOutputFileRadio.AutoSize = true;
            this.noOutputFileRadio.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.noOutputFileRadio.Location = new System.Drawing.Point(10, 9);
            this.noOutputFileRadio.Margin = new System.Windows.Forms.Padding(2);
            this.noOutputFileRadio.Name = "noOutputFileRadio";
            this.noOutputFileRadio.Size = new System.Drawing.Size(86, 21);
            this.noOutputFileRadio.TabIndex = 31;
            this.noOutputFileRadio.TabStop = true;
            this.noOutputFileRadio.Text = "无结果文件";
            this.noOutputFileRadio.UseVisualStyleBackColor = true;
            this.noOutputFileRadio.CheckedChanged += new System.EventHandler(this.NoOutputFileRadio_CheckedChanged);
            // 
            // rsFullFileNameTextBox
            // 
            this.rsFullFileNameTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rsFullFileNameTextBox.Location = new System.Drawing.Point(171, 56);
            this.rsFullFileNameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.rsFullFileNameTextBox.Name = "rsFullFileNameTextBox";
            this.rsFullFileNameTextBox.ReadOnly = true;
            this.rsFullFileNameTextBox.Size = new System.Drawing.Size(329, 23);
            this.rsFullFileNameTextBox.TabIndex = 30;
            this.rsFullFileNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.rsFullFileNameTextBox, "自动指定的结果文件路径");
            // 
            // browseChosenRadioButton
            // 
            this.browseChosenRadioButton.AutoSize = true;
            this.browseChosenRadioButton.Checked = true;
            this.browseChosenRadioButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.browseChosenRadioButton.Location = new System.Drawing.Point(10, 84);
            this.browseChosenRadioButton.Name = "browseChosenRadioButton";
            this.browseChosenRadioButton.Size = new System.Drawing.Size(122, 21);
            this.browseChosenRadioButton.TabIndex = 29;
            this.browseChosenRadioButton.TabStop = true;
            this.browseChosenRadioButton.Text = "浏览指定结果文件";
            this.browseChosenRadioButton.UseVisualStyleBackColor = true;
            this.browseChosenRadioButton.CheckedChanged += new System.EventHandler(this.BrowseChosenRadioButton_CheckedChanged);
            // 
            // paramRadioButton
            // 
            this.paramRadioButton.AutoSize = true;
            this.paramRadioButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.paramRadioButton.Location = new System.Drawing.Point(10, 57);
            this.paramRadioButton.Name = "paramRadioButton";
            this.paramRadioButton.Size = new System.Drawing.Size(122, 21);
            this.paramRadioButton.TabIndex = 28;
            this.paramRadioButton.Text = "传参指定结果文件";
            this.toolTip1.SetToolTip(this.paramRadioButton, "结果文件路径以参数的形式传入自定义脚本中");
            this.paramRadioButton.UseVisualStyleBackColor = true;
            this.paramRadioButton.CheckedChanged += new System.EventHandler(this.ParamRadioButton_CheckedChanged);
            // 
            // stdoutRadioButton
            // 
            this.stdoutRadioButton.AutoSize = true;
            this.stdoutRadioButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stdoutRadioButton.Location = new System.Drawing.Point(10, 33);
            this.stdoutRadioButton.Name = "stdoutRadioButton";
            this.stdoutRadioButton.Size = new System.Drawing.Size(195, 21);
            this.stdoutRadioButton.TabIndex = 27;
            this.stdoutRadioButton.Text = "将脚本stdout重定向到结果文件";
            this.toolTip1.SetToolTip(this.stdoutRadioButton, "Python脚本的标准输出流(stdout)作为运算结果重定向到结果文件");
            this.stdoutRadioButton.UseVisualStyleBackColor = true;
            this.stdoutRadioButton.CheckedChanged += new System.EventHandler(this.StdoutRadioButton_CheckedChanged);
            // 
            // browseChosenTextBox
            // 
            this.browseChosenTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.browseChosenTextBox.Location = new System.Drawing.Point(133, 83);
            this.browseChosenTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.browseChosenTextBox.Name = "browseChosenTextBox";
            this.browseChosenTextBox.ReadOnly = true;
            this.browseChosenTextBox.Size = new System.Drawing.Size(311, 23);
            this.browseChosenTextBox.TabIndex = 25;
            // 
            // paramPrefixTagTextBox
            // 
            this.paramPrefixTagTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.paramPrefixTagTextBox.Location = new System.Drawing.Point(134, 56);
            this.paramPrefixTagTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.paramPrefixTagTextBox.Name = "paramPrefixTagTextBox";
            this.paramPrefixTagTextBox.Size = new System.Drawing.Size(32, 23);
            this.paramPrefixTagTextBox.TabIndex = 24;
            this.paramPrefixTagTextBox.Text = "-f2";
            this.paramPrefixTagTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.paramPrefixTagTextBox, "参数标识");
            this.paramPrefixTagTextBox.TextChanged += new System.EventHandler(this.ParamPrefixTagTextBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(19, 86);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 22);
            this.label6.TabIndex = 24;
            this.label6.Text = "Python脚本：";
            this.toolTip1.SetToolTip(this.label6, "自定义的第三方Python脚本");
            // 
            // pyBrowseButton
            // 
            this.pyBrowseButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pyBrowseButton.Location = new System.Drawing.Point(453, 87);
            this.pyBrowseButton.Name = "pyBrowseButton";
            this.pyBrowseButton.Size = new System.Drawing.Size(49, 23);
            this.pyBrowseButton.TabIndex = 30;
            this.pyBrowseButton.Text = "浏览+";
            this.pyBrowseButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.pyBrowseButton, "选择你要运行的外部Python脚本");
            this.pyBrowseButton.UseVisualStyleBackColor = true;
            this.pyBrowseButton.Click += new System.EventHandler(this.PyBrowseButton_Click);
            // 
            // rsChosenButton
            // 
            this.rsChosenButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rsChosenButton.Location = new System.Drawing.Point(449, 83);
            this.rsChosenButton.Name = "rsChosenButton";
            this.rsChosenButton.Size = new System.Drawing.Size(49, 23);
            this.rsChosenButton.TabIndex = 32;
            this.rsChosenButton.Text = "约定+";
            this.rsChosenButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.rsChosenButton, "选择你的Python脚本生成结果文件的全路径");
            this.rsChosenButton.UseVisualStyleBackColor = true;
            this.rsChosenButton.Click += new System.EventHandler(this.RsChosenButton_Click);
            // 
            // paramInputFileFullPath
            // 
            this.paramInputFileFullPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.paramInputFileFullPath.Location = new System.Drawing.Point(171, 59);
            this.paramInputFileFullPath.Margin = new System.Windows.Forms.Padding(2);
            this.paramInputFileFullPath.Name = "paramInputFileFullPath";
            this.paramInputFileFullPath.ReadOnly = true;
            this.paramInputFileFullPath.Size = new System.Drawing.Size(329, 23);
            this.paramInputFileFullPath.TabIndex = 35;
            this.paramInputFileFullPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.paramInputFileFullPath, "自动指定的结果文件路径");
            // 
            // paramInputFileRadio
            // 
            this.paramInputFileRadio.AutoSize = true;
            this.paramInputFileRadio.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.paramInputFileRadio.Location = new System.Drawing.Point(11, 61);
            this.paramInputFileRadio.Name = "paramInputFileRadio";
            this.paramInputFileRadio.Size = new System.Drawing.Size(122, 21);
            this.paramInputFileRadio.TabIndex = 34;
            this.paramInputFileRadio.Text = "传参指定输入文件";
            this.toolTip1.SetToolTip(this.paramInputFileRadio, "结果文件路径以参数的形式传入自定义脚本中");
            this.paramInputFileRadio.UseVisualStyleBackColor = true;
            this.paramInputFileRadio.CheckedChanged += new System.EventHandler(this.ParamInputFileRadio_CheckedChanged);
            // 
            // paramInputFileTextBox
            // 
            this.paramInputFileTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.paramInputFileTextBox.Location = new System.Drawing.Point(135, 59);
            this.paramInputFileTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.paramInputFileTextBox.Name = "paramInputFileTextBox";
            this.paramInputFileTextBox.Size = new System.Drawing.Size(32, 23);
            this.paramInputFileTextBox.TabIndex = 33;
            this.paramInputFileTextBox.Text = "-f1";
            this.paramInputFileTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.paramInputFileTextBox, "参数标识");
            this.paramInputFileTextBox.TextChanged += new System.EventHandler(this.ParamInputFileTextBox_TextChanged);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "*.bcp";
            this.saveFileDialog1.Filter = "Bcp文件|*.bcp|Txt文件|*.txt";
            this.saveFileDialog1.Title = "选择外部Python脚本计划生成的结果文件名,系统将该文件作为结果展示";
            // 
            // pyFullFilePathTextBox
            // 
            this.pyFullFilePathTextBox.BackColor = System.Drawing.Color.White;
            this.pyFullFilePathTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pyFullFilePathTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pyFullFilePathTextBox.Location = new System.Drawing.Point(137, 87);
            this.pyFullFilePathTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.pyFullFilePathTextBox.Name = "pyFullFilePathTextBox";
            this.pyFullFilePathTextBox.ReadOnly = true;
            this.pyFullFilePathTextBox.Size = new System.Drawing.Size(311, 23);
            this.pyFullFilePathTextBox.TabIndex = 25;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "*.py";
            this.openFileDialog1.Filter = "Py脚本|*.py";
            this.openFileDialog1.Title = "选择要运行的外部自定义Python脚本";
            // 
            // previewCmdGroup
            // 
            this.previewCmdGroup.Controls.Add(this.previewCmdText);
            this.previewCmdGroup.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.previewCmdGroup.Location = new System.Drawing.Point(11, 355);
            this.previewCmdGroup.Margin = new System.Windows.Forms.Padding(2);
            this.previewCmdGroup.Name = "previewCmdGroup";
            this.previewCmdGroup.Padding = new System.Windows.Forms.Padding(2);
            this.previewCmdGroup.Size = new System.Drawing.Size(513, 77);
            this.previewCmdGroup.TabIndex = 31;
            this.previewCmdGroup.TabStop = false;
            this.previewCmdGroup.Text = "执行脚本命令预览：";
            // 
            // previewCmdText
            // 
            this.previewCmdText.BackColor = System.Drawing.SystemColors.Control;
            this.previewCmdText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.previewCmdText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewCmdText.Location = new System.Drawing.Point(2, 18);
            this.previewCmdText.Margin = new System.Windows.Forms.Padding(2);
            this.previewCmdText.Multiline = true;
            this.previewCmdText.Name = "previewCmdText";
            this.previewCmdText.ReadOnly = true;
            this.previewCmdText.Size = new System.Drawing.Size(509, 57);
            this.previewCmdText.TabIndex = 2;
            // 
            // gbkRadio
            // 
            this.gbkRadio.AutoSize = true;
            this.gbkRadio.Location = new System.Drawing.Point(109, 18);
            this.gbkRadio.Margin = new System.Windows.Forms.Padding(2);
            this.gbkRadio.Name = "gbkRadio";
            this.gbkRadio.Size = new System.Drawing.Size(51, 21);
            this.gbkRadio.TabIndex = 1;
            this.gbkRadio.TabStop = true;
            this.gbkRadio.Text = "GBK";
            this.gbkRadio.UseVisualStyleBackColor = true;
            // 
            // utfRadio
            // 
            this.utfRadio.AutoSize = true;
            this.utfRadio.Checked = true;
            this.utfRadio.Location = new System.Drawing.Point(11, 18);
            this.utfRadio.Margin = new System.Windows.Forms.Padding(2);
            this.utfRadio.Name = "utfRadio";
            this.utfRadio.Size = new System.Drawing.Size(60, 21);
            this.utfRadio.TabIndex = 0;
            this.utfRadio.TabStop = true;
            this.utfRadio.Text = "UTF-8";
            this.utfRadio.UseVisualStyleBackColor = true;
            // 
            // helpLabel
            // 
            this.helpLabel.AutoSize = true;
            this.helpLabel.Font = new System.Drawing.Font("微软雅黑 Light", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.helpLabel.Image = global::Citta_T1.Properties.Resources.div;
            this.helpLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.helpLabel.Location = new System.Drawing.Point(11, 439);
            this.helpLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.helpLabel.Name = "helpLabel";
            this.helpLabel.Size = new System.Drawing.Size(73, 16);
            this.helpLabel.TabIndex = 33;
            this.helpLabel.Text = "       帮助说明";
            this.helpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // otherSeparatorText
            // 
            this.otherSeparatorText.Location = new System.Drawing.Point(214, 16);
            this.otherSeparatorText.Margin = new System.Windows.Forms.Padding(2);
            this.otherSeparatorText.MaxLength = 1;
            this.otherSeparatorText.Name = "otherSeparatorText";
            this.otherSeparatorText.Size = new System.Drawing.Size(68, 23);
            this.otherSeparatorText.TabIndex = 3;
            this.otherSeparatorText.TextChanged += new System.EventHandler(this.OtherSeparatorText_TextChanged);
            // 
            // otherSeparatorRadio
            // 
            this.otherSeparatorRadio.AutoSize = true;
            this.otherSeparatorRadio.Location = new System.Drawing.Point(159, 18);
            this.otherSeparatorRadio.Margin = new System.Windows.Forms.Padding(2);
            this.otherSeparatorRadio.Name = "otherSeparatorRadio";
            this.otherSeparatorRadio.Size = new System.Drawing.Size(50, 21);
            this.otherSeparatorRadio.TabIndex = 2;
            this.otherSeparatorRadio.TabStop = true;
            this.otherSeparatorRadio.Text = "其他";
            this.otherSeparatorRadio.UseVisualStyleBackColor = true;
            // 
            // commaRadio
            // 
            this.commaRadio.AutoSize = true;
            this.commaRadio.Location = new System.Drawing.Point(87, 18);
            this.commaRadio.Margin = new System.Windows.Forms.Padding(2);
            this.commaRadio.Name = "commaRadio";
            this.commaRadio.Size = new System.Drawing.Size(50, 21);
            this.commaRadio.TabIndex = 1;
            this.commaRadio.TabStop = true;
            this.commaRadio.Text = "逗号";
            this.commaRadio.UseVisualStyleBackColor = true;
            // 
            // tabRadio
            // 
            this.tabRadio.AutoSize = true;
            this.tabRadio.Checked = true;
            this.tabRadio.Location = new System.Drawing.Point(13, 18);
            this.tabRadio.Margin = new System.Windows.Forms.Padding(2);
            this.tabRadio.Name = "tabRadio";
            this.tabRadio.Size = new System.Drawing.Size(49, 21);
            this.tabRadio.TabIndex = 0;
            this.tabRadio.TabStop = true;
            this.tabRadio.Text = "TAB";
            this.tabRadio.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.inputFileSettingTab);
            this.tabControl1.Controls.Add(this.outputFileSettingTab);
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(9, 158);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 1;
            this.tabControl1.Size = new System.Drawing.Size(519, 144);
            this.tabControl1.TabIndex = 35;
            // 
            // inputFileSettingTab
            // 
            this.inputFileSettingTab.BackColor = System.Drawing.SystemColors.Control;
            this.inputFileSettingTab.Controls.Add(this.paramInputFileFullPath);
            this.inputFileSettingTab.Controls.Add(this.paramInputFileRadio);
            this.inputFileSettingTab.Controls.Add(this.paramInputFileTextBox);
            this.inputFileSettingTab.Controls.Add(this.noInputFileRadio);
            this.inputFileSettingTab.Location = new System.Drawing.Point(4, 26);
            this.inputFileSettingTab.Margin = new System.Windows.Forms.Padding(2);
            this.inputFileSettingTab.Name = "inputFileSettingTab";
            this.inputFileSettingTab.Padding = new System.Windows.Forms.Padding(2);
            this.inputFileSettingTab.Size = new System.Drawing.Size(511, 114);
            this.inputFileSettingTab.TabIndex = 3;
            this.inputFileSettingTab.Text = "输入文件设置";
            // 
            // noInputFileRadio
            // 
            this.noInputFileRadio.AutoSize = true;
            this.noInputFileRadio.Checked = true;
            this.noInputFileRadio.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.noInputFileRadio.Location = new System.Drawing.Point(11, 19);
            this.noInputFileRadio.Margin = new System.Windows.Forms.Padding(2);
            this.noInputFileRadio.Name = "noInputFileRadio";
            this.noInputFileRadio.Size = new System.Drawing.Size(434, 21);
            this.noInputFileRadio.TabIndex = 32;
            this.noInputFileRadio.TabStop = true;
            this.noInputFileRadio.Text = "无指定输入文件（脚本中输入文件的路径可与算子连接的数据源路径不一致）";
            this.noInputFileRadio.UseVisualStyleBackColor = true;
            this.noInputFileRadio.CheckedChanged += new System.EventHandler(this.NoInputFileRadio_CheckedChanged);
            // 
            // outputFileSettingTab
            // 
            this.outputFileSettingTab.BackColor = System.Drawing.SystemColors.Control;
            this.outputFileSettingTab.Controls.Add(this.rsChosenButton);
            this.outputFileSettingTab.Controls.Add(this.browseChosenRadioButton);
            this.outputFileSettingTab.Controls.Add(this.browseChosenTextBox);
            this.outputFileSettingTab.Controls.Add(this.rsFullFileNameTextBox);
            this.outputFileSettingTab.Controls.Add(this.noOutputFileRadio);
            this.outputFileSettingTab.Controls.Add(this.stdoutRadioButton);
            this.outputFileSettingTab.Controls.Add(this.paramRadioButton);
            this.outputFileSettingTab.Controls.Add(this.paramPrefixTagTextBox);
            this.outputFileSettingTab.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outputFileSettingTab.Location = new System.Drawing.Point(4, 26);
            this.outputFileSettingTab.Margin = new System.Windows.Forms.Padding(2);
            this.outputFileSettingTab.Name = "outputFileSettingTab";
            this.outputFileSettingTab.Padding = new System.Windows.Forms.Padding(2);
            this.outputFileSettingTab.Size = new System.Drawing.Size(511, 114);
            this.outputFileSettingTab.TabIndex = 0;
            this.outputFileSettingTab.Text = "结果文件设置";
            // 
            // outputFileEncodeSettingGroup
            // 
            this.outputFileEncodeSettingGroup.Controls.Add(this.gbkRadio);
            this.outputFileEncodeSettingGroup.Controls.Add(this.utfRadio);
            this.outputFileEncodeSettingGroup.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outputFileEncodeSettingGroup.Location = new System.Drawing.Point(11, 307);
            this.outputFileEncodeSettingGroup.Margin = new System.Windows.Forms.Padding(2);
            this.outputFileEncodeSettingGroup.Name = "outputFileEncodeSettingGroup";
            this.outputFileEncodeSettingGroup.Padding = new System.Windows.Forms.Padding(2);
            this.outputFileEncodeSettingGroup.Size = new System.Drawing.Size(187, 44);
            this.outputFileEncodeSettingGroup.TabIndex = 36;
            this.outputFileEncodeSettingGroup.TabStop = false;
            this.outputFileEncodeSettingGroup.Text = "结果文件编码";
            // 
            // outputFileSeparatorSettingGroup
            // 
            this.outputFileSeparatorSettingGroup.Controls.Add(this.otherSeparatorText);
            this.outputFileSeparatorSettingGroup.Controls.Add(this.tabRadio);
            this.outputFileSeparatorSettingGroup.Controls.Add(this.otherSeparatorRadio);
            this.outputFileSeparatorSettingGroup.Controls.Add(this.commaRadio);
            this.outputFileSeparatorSettingGroup.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outputFileSeparatorSettingGroup.Location = new System.Drawing.Point(213, 307);
            this.outputFileSeparatorSettingGroup.Margin = new System.Windows.Forms.Padding(2);
            this.outputFileSeparatorSettingGroup.Name = "outputFileSeparatorSettingGroup";
            this.outputFileSeparatorSettingGroup.Padding = new System.Windows.Forms.Padding(2);
            this.outputFileSeparatorSettingGroup.Size = new System.Drawing.Size(310, 44);
            this.outputFileSeparatorSettingGroup.TabIndex = 37;
            this.outputFileSeparatorSettingGroup.TabStop = false;
            this.outputFileSeparatorSettingGroup.Text = "结果文件分隔符";
            // 
            // PythonOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(531, 479);
            this.Controls.Add(this.outputFileSeparatorSettingGroup);
            this.Controls.Add(this.outputFileEncodeSettingGroup);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.helpLabel);
            this.Controls.Add(this.previewCmdGroup);
            this.Controls.Add(this.pyBrowseButton);
            this.Controls.Add(this.pyFullFilePathTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pyParamTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataSourceTB0);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pythonChosenComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PythonOperatorView";
            this.Text = "Python算子设置";
            this.Controls.SetChildIndex(this.confirmButton, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.pythonChosenComboBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dataSourceTB0, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.pyParamTextBox, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.pyFullFilePathTextBox, 0);
            this.Controls.SetChildIndex(this.pyBrowseButton, 0);
            this.Controls.SetChildIndex(this.previewCmdGroup, 0);
            this.Controls.SetChildIndex(this.helpLabel, 0);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.outputFileEncodeSettingGroup, 0);
            this.Controls.SetChildIndex(this.outputFileSeparatorSettingGroup, 0);
            this.previewCmdGroup.ResumeLayout(false);
            this.previewCmdGroup.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.inputFileSettingTab.ResumeLayout(false);
            this.inputFileSettingTab.PerformLayout();
            this.outputFileSettingTab.ResumeLayout(false);
            this.outputFileSettingTab.PerformLayout();
            this.outputFileEncodeSettingGroup.ResumeLayout(false);
            this.outputFileEncodeSettingGroup.PerformLayout();
            this.outputFileSeparatorSettingGroup.ResumeLayout(false);
            this.outputFileSeparatorSettingGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox pythonChosenComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox pyParamTextBox;
        private System.Windows.Forms.TextBox paramPrefixTagTextBox;
        private System.Windows.Forms.TextBox browseChosenTextBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.RadioButton browseChosenRadioButton;
        private System.Windows.Forms.RadioButton paramRadioButton;
        private System.Windows.Forms.RadioButton stdoutRadioButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox pyFullFilePathTextBox;
        private System.Windows.Forms.Button pyBrowseButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox rsFullFileNameTextBox;
        private System.Windows.Forms.RadioButton noOutputFileRadio;
        private System.Windows.Forms.GroupBox previewCmdGroup;
        private System.Windows.Forms.Label helpLabel;
        private System.Windows.Forms.RadioButton gbkRadio;
        private System.Windows.Forms.RadioButton utfRadio;
        private System.Windows.Forms.RadioButton otherSeparatorRadio;
        private System.Windows.Forms.RadioButton commaRadio;
        private System.Windows.Forms.RadioButton tabRadio;
        private System.Windows.Forms.TextBox otherSeparatorText;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage outputFileSettingTab;
        private System.Windows.Forms.Button rsChosenButton;
        private System.Windows.Forms.TabPage inputFileSettingTab;
        private System.Windows.Forms.TextBox paramInputFileFullPath;
        private System.Windows.Forms.RadioButton paramInputFileRadio;
        private System.Windows.Forms.TextBox paramInputFileTextBox;
        private System.Windows.Forms.RadioButton noInputFileRadio;
        private System.Windows.Forms.TextBox previewCmdText;
        private System.Windows.Forms.GroupBox outputFileEncodeSettingGroup;
        private System.Windows.Forms.GroupBox outputFileSeparatorSettingGroup;
    }
}