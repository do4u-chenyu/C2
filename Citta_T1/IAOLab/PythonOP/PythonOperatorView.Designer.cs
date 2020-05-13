namespace Citta_T1.OperatorViews
{
    partial class PythonOperatorView
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
            this.confirmButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.pythonChosenComboBox = new System.Windows.Forms.ComboBox();
            this.dataSource0 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pyParamTextBox = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rsFullFileNameTextBox = new System.Windows.Forms.TextBox();
            this.browseChosenRadioButton = new System.Windows.Forms.RadioButton();
            this.paramRadioButton = new System.Windows.Forms.RadioButton();
            this.stdoutRadioButton = new System.Windows.Forms.RadioButton();
            this.browseChosenTextBox = new System.Windows.Forms.TextBox();
            this.paramPrefixTagTextBox = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.pyBrowseButton = new System.Windows.Forms.Button();
            this.rsChosenButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pyFullFilePathTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.confirmButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.confirmButton.Location = new System.Drawing.Point(565, 655);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(90, 40);
            this.confirmButton.TabIndex = 1;
            this.confirmButton.Text = "确认";
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cancelButton.Location = new System.Drawing.Point(693, 655);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(94, 40);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // pythonChosenComboBox
            // 
            this.pythonChosenComboBox.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.pythonChosenComboBox.FormattingEnabled = true;
            this.pythonChosenComboBox.Location = new System.Drawing.Point(206, 74);
            this.pythonChosenComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.pythonChosenComboBox.Name = "pythonChosenComboBox";
            this.pythonChosenComboBox.Size = new System.Drawing.Size(223, 32);
            this.pythonChosenComboBox.TabIndex = 3;
            this.pythonChosenComboBox.Text = "未配置Python虚拟机";
            this.toolTip1.SetToolTip(this.pythonChosenComboBox, "当前已配置的Python虚拟机,如果还没有配置,可以在首选项-Python引擎中配置");
            // 
            // dataSource0
            // 
            this.dataSource0.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataSource0.Location = new System.Drawing.Point(206, 22);
            this.dataSource0.Name = "dataSource0";
            this.dataSource0.ReadOnly = true;
            this.dataSource0.Size = new System.Drawing.Size(223, 31);
            this.dataSource0.TabIndex = 12;
            this.dataSource0.Text = "text_utf8_tab1.txt";
            this.dataSource0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(68, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 31);
            this.label1.TabIndex = 11;
            this.label1.Text = "数据信息：";
            this.toolTip1.SetToolTip(this.label1, "数据源名称");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(4, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 31);
            this.label2.TabIndex = 13;
            this.label2.Text = "Python虚拟机：";
            this.toolTip1.SetToolTip(this.label2, "当前已配置的Python虚拟机,如果还没有配置,可以在首选项-Python引擎中配置");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(22, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 31);
            this.label4.TabIndex = 16;
            this.label4.Text = "脚本传入参数：";
            this.toolTip1.SetToolTip(this.label4, "脚本需要传入的其他参数");
            // 
            // pyParamTextBox
            // 
            this.pyParamTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pyParamTextBox.Location = new System.Drawing.Point(206, 183);
            this.pyParamTextBox.Name = "pyParamTextBox";
            this.pyParamTextBox.Size = new System.Drawing.Size(549, 31);
            this.pyParamTextBox.TabIndex = 17;
            this.pyParamTextBox.TextChanged += new System.EventHandler(this.pyParamTextBox_TextChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton1.Location = new System.Drawing.Point(15, 14);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(125, 28);
            this.radioButton1.TabIndex = 31;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "无结果文件";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // rsFullFileNameTextBox
            // 
            this.rsFullFileNameTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rsFullFileNameTextBox.Location = new System.Drawing.Point(256, 84);
            this.rsFullFileNameTextBox.Name = "rsFullFileNameTextBox";
            this.rsFullFileNameTextBox.ReadOnly = true;
            this.rsFullFileNameTextBox.Size = new System.Drawing.Size(492, 31);
            this.rsFullFileNameTextBox.TabIndex = 30;
            this.rsFullFileNameTextBox.Text = "D:\\ModelDocument\\phx\\L19_20200513_081606.bcp";
            this.rsFullFileNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.rsFullFileNameTextBox, "自动指定的结果文件路径");
            // 
            // browseChosenRadioButton
            // 
            this.browseChosenRadioButton.AutoSize = true;
            this.browseChosenRadioButton.Checked = true;
            this.browseChosenRadioButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.browseChosenRadioButton.Location = new System.Drawing.Point(15, 126);
            this.browseChosenRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.browseChosenRadioButton.Name = "browseChosenRadioButton";
            this.browseChosenRadioButton.Size = new System.Drawing.Size(179, 28);
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
            this.paramRadioButton.Location = new System.Drawing.Point(15, 86);
            this.paramRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.paramRadioButton.Name = "paramRadioButton";
            this.paramRadioButton.Size = new System.Drawing.Size(179, 28);
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
            this.stdoutRadioButton.Location = new System.Drawing.Point(15, 49);
            this.stdoutRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.stdoutRadioButton.Name = "stdoutRadioButton";
            this.stdoutRadioButton.Size = new System.Drawing.Size(289, 28);
            this.stdoutRadioButton.TabIndex = 27;
            this.stdoutRadioButton.Text = "将脚本stdout重定向到结果文件";
            this.toolTip1.SetToolTip(this.stdoutRadioButton, "Python脚本的标准输出流(stdout)作为运算结果重定向到结果文件");
            this.stdoutRadioButton.UseVisualStyleBackColor = true;
            this.stdoutRadioButton.CheckedChanged += new System.EventHandler(this.StdoutRadioButton_CheckedChanged);
            // 
            // browseChosenTextBox
            // 
            this.browseChosenTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.browseChosenTextBox.Location = new System.Drawing.Point(200, 124);
            this.browseChosenTextBox.Name = "browseChosenTextBox";
            this.browseChosenTextBox.ReadOnly = true;
            this.browseChosenTextBox.Size = new System.Drawing.Size(464, 31);
            this.browseChosenTextBox.TabIndex = 25;
            // 
            // paramPrefixTagTextBox
            // 
            this.paramPrefixTagTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.paramPrefixTagTextBox.Location = new System.Drawing.Point(201, 84);
            this.paramPrefixTagTextBox.Name = "paramPrefixTagTextBox";
            this.paramPrefixTagTextBox.Size = new System.Drawing.Size(46, 31);
            this.paramPrefixTagTextBox.TabIndex = 24;
            this.paramPrefixTagTextBox.Text = "-f2";
            this.paramPrefixTagTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.paramPrefixTagTextBox, "参数标识");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(28, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 31);
            this.label6.TabIndex = 24;
            this.label6.Text = "Python脚本：";
            this.toolTip1.SetToolTip(this.label6, "自定义的第三方Python脚本");
            // 
            // pyBrowseButton
            // 
            this.pyBrowseButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pyBrowseButton.Location = new System.Drawing.Point(680, 130);
            this.pyBrowseButton.Margin = new System.Windows.Forms.Padding(4);
            this.pyBrowseButton.Name = "pyBrowseButton";
            this.pyBrowseButton.Size = new System.Drawing.Size(74, 34);
            this.pyBrowseButton.TabIndex = 30;
            this.pyBrowseButton.Text = "浏览+";
            this.pyBrowseButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.pyBrowseButton, "选择你要运行的外部Python脚本");
            this.pyBrowseButton.UseVisualStyleBackColor = true;
            this.pyBrowseButton.Click += new System.EventHandler(this.PyBrowseButton_Click);
            // 
            // rsChosenButton
            // 
            this.rsChosenButton.Enabled = false;
            this.rsChosenButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rsChosenButton.Location = new System.Drawing.Point(674, 124);
            this.rsChosenButton.Margin = new System.Windows.Forms.Padding(4);
            this.rsChosenButton.Name = "rsChosenButton";
            this.rsChosenButton.Size = new System.Drawing.Size(74, 34);
            this.rsChosenButton.TabIndex = 32;
            this.rsChosenButton.Text = "约定+";
            this.rsChosenButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.rsChosenButton, "选择你的Python脚本生成结果文件的全路径");
            this.rsChosenButton.UseVisualStyleBackColor = true;
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
            this.pyFullFilePathTextBox.Location = new System.Drawing.Point(206, 130);
            this.pyFullFilePathTextBox.Name = "pyFullFilePathTextBox";
            this.pyFullFilePathTextBox.ReadOnly = true;
            this.pyFullFilePathTextBox.Size = new System.Drawing.Size(464, 31);
            this.pyFullFilePathTextBox.TabIndex = 25;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "*.py";
            this.openFileDialog1.Filter = "Py脚本|*.py";
            this.openFileDialog1.Title = "选择要运行的外部自定义Python脚本";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(17, 532);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(770, 115);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "执行脚本命令预览：";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(164, 27);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(70, 28);
            this.radioButton3.TabIndex = 1;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "GBK";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(17, 27);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(87, 28);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "UTF-8";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑 Light", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Image = global::Citta_T1.Properties.Resources.div;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(16, 659);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 21);
            this.label7.TabIndex = 33;
            this.label7.Text = "       帮助说明";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(321, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 31);
            this.textBox1.TabIndex = 3;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(239, 27);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(71, 28);
            this.radioButton6.TabIndex = 2;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "其他";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(130, 27);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(71, 28);
            this.radioButton5.TabIndex = 1;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "逗号";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Location = new System.Drawing.Point(19, 27);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(69, 28);
            this.radioButton4.TabIndex = 0;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "TAB";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(13, 237);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 1;
            this.tabControl1.Size = new System.Drawing.Size(778, 216);
            this.tabControl1.TabIndex = 35;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.textBox2);
            this.tabPage4.Controls.Add(this.radioButton8);
            this.tabPage4.Controls.Add(this.textBox3);
            this.tabPage4.Controls.Add(this.radioButton7);
            this.tabPage4.Location = new System.Drawing.Point(4, 33);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(770, 179);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "输入文件设置";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.rsChosenButton);
            this.tabPage1.Controls.Add(this.browseChosenRadioButton);
            this.tabPage1.Controls.Add(this.browseChosenTextBox);
            this.tabPage1.Controls.Add(this.rsFullFileNameTextBox);
            this.tabPage1.Controls.Add(this.radioButton1);
            this.tabPage1.Controls.Add(this.stdoutRadioButton);
            this.tabPage1.Controls.Add(this.paramRadioButton);
            this.tabPage1.Controls.Add(this.paramPrefixTagTextBox);
            this.tabPage1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(770, 179);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "结果文件设置";
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Checked = true;
            this.radioButton7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton7.Location = new System.Drawing.Point(17, 28);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(125, 28);
            this.radioButton7.TabIndex = 32;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "无输入文件";
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(257, 89);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(492, 31);
            this.textBox2.TabIndex = 35;
            this.textBox2.Text = "D:\\sqy\\datas\\text_utf8_tab1.txt";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.textBox2, "自动指定的结果文件路径");
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButton8.Location = new System.Drawing.Point(16, 91);
            this.radioButton8.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(179, 28);
            this.radioButton8.TabIndex = 34;
            this.radioButton8.Text = "传参指定输入文件";
            this.toolTip1.SetToolTip(this.radioButton8, "结果文件路径以参数的形式传入自定义脚本中");
            this.radioButton8.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox3.Location = new System.Drawing.Point(202, 89);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(46, 31);
            this.textBox3.TabIndex = 33;
            this.textBox3.Text = "-f1";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.textBox3, "参数标识");
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Control;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox4.Location = new System.Drawing.Point(3, 27);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(764, 85);
            this.textBox4.TabIndex = 2;
            this.textBox4.Text = "python 1.py -f1 D:\\sqy\\datas\\text_utf8_tab1.txt -f2 d:\\work\\2.txt -o iiiii -d1 20" +
    "200512 -d2 20200612 -k testtest";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(17, 460);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 66);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "结果文件编码";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.radioButton4);
            this.groupBox3.Controls.Add(this.radioButton6);
            this.groupBox3.Controls.Add(this.radioButton5);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(319, 460);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(465, 66);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "结果文件分隔符";
            // 
            // PythonOperatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 734);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pyBrowseButton);
            this.Controls.Add(this.pyFullFilePathTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pyParamTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataSource0);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pythonChosenComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PythonOperatorView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Python算子设置";
            this.Load += new System.EventHandler(this.PythonOperatorView_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox pythonChosenComboBox;
        private System.Windows.Forms.TextBox dataSource0;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox pyParamTextBox;
        private System.Windows.Forms.TextBox paramPrefixTagTextBox;
        private System.Windows.Forms.TextBox browseChosenTextBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.RadioButton browseChosenRadioButton;
        private System.Windows.Forms.RadioButton paramRadioButton;
        private System.Windows.Forms.RadioButton stdoutRadioButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox pyFullFilePathTextBox;
        private System.Windows.Forms.Button pyBrowseButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox rsFullFileNameTextBox;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button rsChosenButton;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}