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
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pyParamTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pyBrowseButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pyFullFilePathTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.previewCmdGroup = new System.Windows.Forms.GroupBox();
            this.previewCmdText = new System.Windows.Forms.TextBox();
            this.gbkRadio = new System.Windows.Forms.RadioButton();
            this.utfRadio = new System.Windows.Forms.RadioButton();
            this.otherSeparatorText = new System.Windows.Forms.TextBox();
            this.otherSeparatorRadio = new System.Windows.Forms.RadioButton();
            this.commaRadio = new System.Windows.Forms.RadioButton();
            this.tabRadio = new System.Windows.Forms.RadioButton();
            this.browseChosenTextBox = new System.Windows.Forms.TextBox();
            this.rsChosenButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.outputFileSeparatorSettingGroup = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.outputFileEncodeSettingGroup = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.previewCmdGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.outputFileSeparatorSettingGroup.SuspendLayout();
            this.outputFileEncodeSettingGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
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
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(45, 16);
            this.label1.Size = new System.Drawing.Size(90, 22);
            // 
            // pythonChosenComboBox
            // 
            this.pythonChosenComboBox.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.pythonChosenComboBox.FormattingEnabled = true;
            this.pythonChosenComboBox.Location = new System.Drawing.Point(137, 57);
            this.pythonChosenComboBox.Name = "pythonChosenComboBox";
            this.pythonChosenComboBox.Size = new System.Drawing.Size(150, 25);
            this.pythonChosenComboBox.TabIndex = 3;
            this.toolTip1.SetToolTip(this.pythonChosenComboBox, "当前已配置的Python虚拟机,如果还没有配置,可以在首选项-Python引擎中配置");
            this.pythonChosenComboBox.SelectedIndexChanged += new System.EventHandler(this.PythonChosenComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 60);
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
            this.label4.Location = new System.Drawing.Point(13, 144);
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
            this.pyParamTextBox.Location = new System.Drawing.Point(137, 143);
            this.pyParamTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.pyParamTextBox.Name = "pyParamTextBox";
            this.pyParamTextBox.Size = new System.Drawing.Size(311, 23);
            this.pyParamTextBox.TabIndex = 17;
            this.pyParamTextBox.TextChanged += new System.EventHandler(this.PyParamTextBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(19, 103);
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
            this.pyBrowseButton.Location = new System.Drawing.Point(454, 102);
            this.pyBrowseButton.Name = "pyBrowseButton";
            this.pyBrowseButton.Size = new System.Drawing.Size(49, 23);
            this.pyBrowseButton.TabIndex = 30;
            this.pyBrowseButton.Text = "浏览+";
            this.pyBrowseButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.pyBrowseButton, "选择你要运行的外部Python脚本");
            this.pyBrowseButton.UseVisualStyleBackColor = true;
            this.pyBrowseButton.Click += new System.EventHandler(this.PyBrowseButton_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "*.bcp";
            this.saveFileDialog1.Filter = "txt文件|*.txt|bcp文件|*.bcp|csv文件|*.csv";
            this.saveFileDialog1.Title = "选择外部Python脚本计划生成的结果文件名,系统将该文件作为结果展示";
            // 
            // pyFullFilePathTextBox
            // 
            this.pyFullFilePathTextBox.BackColor = System.Drawing.Color.White;
            this.pyFullFilePathTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pyFullFilePathTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pyFullFilePathTextBox.Location = new System.Drawing.Point(137, 102);
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
            this.previewCmdGroup.Location = new System.Drawing.Point(10, 185);
            this.previewCmdGroup.Margin = new System.Windows.Forms.Padding(2);
            this.previewCmdGroup.Name = "previewCmdGroup";
            this.previewCmdGroup.Padding = new System.Windows.Forms.Padding(2);
            this.previewCmdGroup.Size = new System.Drawing.Size(513, 80);
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
            this.previewCmdText.Size = new System.Drawing.Size(509, 60);
            this.previewCmdText.TabIndex = 2;
            // 
            // gbkRadio
            // 
            this.gbkRadio.AutoSize = true;
            this.gbkRadio.Location = new System.Drawing.Point(217, 13);
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
            this.utfRadio.Location = new System.Drawing.Point(119, 15);
            this.utfRadio.Margin = new System.Windows.Forms.Padding(2);
            this.utfRadio.Name = "utfRadio";
            this.utfRadio.Size = new System.Drawing.Size(60, 21);
            this.utfRadio.TabIndex = 0;
            this.utfRadio.TabStop = true;
            this.utfRadio.Text = "UTF-8";
            this.utfRadio.UseVisualStyleBackColor = true;
            // 
            // otherSeparatorText
            // 
            this.otherSeparatorText.Location = new System.Drawing.Point(358, 13);
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
            this.otherSeparatorRadio.Location = new System.Drawing.Point(309, 15);
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
            this.commaRadio.Location = new System.Drawing.Point(217, 15);
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
            this.tabRadio.Location = new System.Drawing.Point(118, 15);
            this.tabRadio.Margin = new System.Windows.Forms.Padding(2);
            this.tabRadio.Name = "tabRadio";
            this.tabRadio.Size = new System.Drawing.Size(49, 21);
            this.tabRadio.TabIndex = 0;
            this.tabRadio.TabStop = true;
            this.tabRadio.Text = "TAB";
            this.tabRadio.UseVisualStyleBackColor = true;
            // 
            // browseChosenTextBox
            // 
            this.browseChosenTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.browseChosenTextBox.Location = new System.Drawing.Point(128, 21);
            this.browseChosenTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.browseChosenTextBox.Name = "browseChosenTextBox";
            this.browseChosenTextBox.ReadOnly = true;
            this.browseChosenTextBox.Size = new System.Drawing.Size(311, 23);
            this.browseChosenTextBox.TabIndex = 25;
            // 
            // rsChosenButton
            // 
            this.rsChosenButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rsChosenButton.Location = new System.Drawing.Point(444, 20);
            this.rsChosenButton.Name = "rsChosenButton";
            this.rsChosenButton.Size = new System.Drawing.Size(49, 23);
            this.rsChosenButton.TabIndex = 32;
            this.rsChosenButton.Text = "指定+";
            this.rsChosenButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.rsChosenButton, "选择你的Python脚本生成结果文件的全路径");
            this.rsChosenButton.UseVisualStyleBackColor = true;
            this.rsChosenButton.Click += new System.EventHandler(this.RsChosenButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.outputFileSeparatorSettingGroup);
            this.groupBox1.Controls.Add(this.outputFileEncodeSettingGroup);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.browseChosenTextBox);
            this.groupBox1.Controls.Add(this.rsChosenButton);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(10, 297);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(511, 135);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "结果文件展示设置：";
            // 
            // outputFileSeparatorSettingGroup
            // 
            this.outputFileSeparatorSettingGroup.Controls.Add(this.commaRadio);
            this.outputFileSeparatorSettingGroup.Controls.Add(this.otherSeparatorText);
            this.outputFileSeparatorSettingGroup.Controls.Add(this.label8);
            this.outputFileSeparatorSettingGroup.Controls.Add(this.tabRadio);
            this.outputFileSeparatorSettingGroup.Controls.Add(this.otherSeparatorRadio);
            this.outputFileSeparatorSettingGroup.Location = new System.Drawing.Point(9, 90);
            this.outputFileSeparatorSettingGroup.Name = "outputFileSeparatorSettingGroup";
            this.outputFileSeparatorSettingGroup.Size = new System.Drawing.Size(484, 41);
            this.outputFileSeparatorSettingGroup.TabIndex = 37;
            this.outputFileSeparatorSettingGroup.TabStop = false;
            this.outputFileSeparatorSettingGroup.Paint += new System.Windows.Forms.PaintEventHandler(this.OutputFileSeparatorSettingGroup_Paint);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(1, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 17);
            this.label8.TabIndex = 35;
            this.label8.Text = "结果文件分隔符：";
            // 
            // outputFileEncodeSettingGroup
            // 
            this.outputFileEncodeSettingGroup.Controls.Add(this.label7);
            this.outputFileEncodeSettingGroup.Controls.Add(this.utfRadio);
            this.outputFileEncodeSettingGroup.Controls.Add(this.gbkRadio);
            this.outputFileEncodeSettingGroup.Location = new System.Drawing.Point(9, 49);
            this.outputFileEncodeSettingGroup.Name = "outputFileEncodeSettingGroup";
            this.outputFileEncodeSettingGroup.Size = new System.Drawing.Size(484, 35);
            this.outputFileEncodeSettingGroup.TabIndex = 36;
            this.outputFileEncodeSettingGroup.TabStop = false;
            this.outputFileEncodeSettingGroup.Paint += new System.Windows.Forms.PaintEventHandler(this.OutputFileEncodeSettingGroup_Paint);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(1, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 17);
            this.label7.TabIndex = 34;
            this.label7.Text = "结果文件编码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(8, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 17);
            this.label5.TabIndex = 33;
            this.label5.Text = "指定展示结果文件：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(453, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 37;
            this.label3.Text = "(非必填项)";
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Location = new System.Drawing.Point(10, 281);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(512, 2);
            this.label9.TabIndex = 38;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(465, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 17);
            this.label10.TabIndex = 39;
            this.label10.Text = "帮助文档";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Citta_T1.Properties.Resources.controlhelp;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(442, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 17);
            this.pictureBox1.TabIndex = 40;
            this.pictureBox1.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(293, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 17);
            this.label11.TabIndex = 41;
            this.label11.Text = "(必选项)";
            // 
            // PythonOperatorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(531, 479);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
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
            this.MaximumSize = new System.Drawing.Size(547, 518);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(547, 518);
            this.Name = "PythonOperatorView";
            this.Text = "Python算子设置";
            this.Load += new System.EventHandler(this.PythonOperatorView_Load);
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
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.previewCmdGroup.ResumeLayout(false);
            this.previewCmdGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.outputFileSeparatorSettingGroup.ResumeLayout(false);
            this.outputFileSeparatorSettingGroup.PerformLayout();
            this.outputFileEncodeSettingGroup.ResumeLayout(false);
            this.outputFileEncodeSettingGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox pythonChosenComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox pyParamTextBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox pyFullFilePathTextBox;
        private System.Windows.Forms.Button pyBrowseButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox previewCmdGroup;
        private System.Windows.Forms.RadioButton gbkRadio;
        private System.Windows.Forms.RadioButton utfRadio;
        private System.Windows.Forms.RadioButton otherSeparatorRadio;
        private System.Windows.Forms.RadioButton commaRadio;
        private System.Windows.Forms.RadioButton tabRadio;
        private System.Windows.Forms.TextBox otherSeparatorText;
        private System.Windows.Forms.TextBox previewCmdText;
        private System.Windows.Forms.TextBox browseChosenTextBox;
        private System.Windows.Forms.Button rsChosenButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox outputFileEncodeSettingGroup;
        private System.Windows.Forms.GroupBox outputFileSeparatorSettingGroup;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label11;
    }
}