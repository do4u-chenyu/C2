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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rsFullFileNameTextBox = new System.Windows.Forms.TextBox();
            this.browseChosenRadioButton = new System.Windows.Forms.RadioButton();
            this.paramRadioButton = new System.Windows.Forms.RadioButton();
            this.stdoutRadioButton = new System.Windows.Forms.RadioButton();
            this.rsChosenButton = new System.Windows.Forms.Button();
            this.browseChosenTextBox = new System.Windows.Forms.TextBox();
            this.paramPrefixTagTextBox = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.pyBrowseButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pyFullFilePathTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.confirmButton.FlatAppearance.BorderSize = 0;
            this.confirmButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.confirmButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.confirmButton.Location = new System.Drawing.Point(360, 304);
            this.confirmButton.Margin = new System.Windows.Forms.Padding(2);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            this.confirmButton.TabIndex = 1;
            this.confirmButton.Text = "确认";
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cancelButton.Location = new System.Drawing.Point(445, 304);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(63, 27);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
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
            // 
            // dataSource0
            // 
            this.dataSource0.Location = new System.Drawing.Point(137, 15);
            this.dataSource0.Margin = new System.Windows.Forms.Padding(2);
            this.dataSource0.Name = "dataSource0";
            this.dataSource0.ReadOnly = true;
            this.dataSource0.Size = new System.Drawing.Size(150, 21);
            this.dataSource0.TabIndex = 12;
            this.dataSource0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.label4.Location = new System.Drawing.Point(9, 269);
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
            this.pyParamTextBox.Location = new System.Drawing.Point(132, 269);
            this.pyParamTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.pyParamTextBox.Name = "pyParamTextBox";
            this.pyParamTextBox.Size = new System.Drawing.Size(375, 23);
            this.pyParamTextBox.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rsFullFileNameTextBox);
            this.groupBox1.Controls.Add(this.browseChosenRadioButton);
            this.groupBox1.Controls.Add(this.paramRadioButton);
            this.groupBox1.Controls.Add(this.stdoutRadioButton);
            this.groupBox1.Controls.Add(this.rsChosenButton);
            this.groupBox1.Controls.Add(this.browseChosenTextBox);
            this.groupBox1.Controls.Add(this.paramPrefixTagTextBox);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(8, 122);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(504, 129);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "结果文件设置：";
            this.toolTip1.SetToolTip(this.groupBox1, "指定第三方脚本返回给平台结果文件的方式");
            // 
            // rsFullFileNameTextBox
            // 
            this.rsFullFileNameTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rsFullFileNameTextBox.Location = new System.Drawing.Point(165, 62);
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
            this.browseChosenRadioButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.browseChosenRadioButton.Location = new System.Drawing.Point(6, 97);
            this.browseChosenRadioButton.Name = "browseChosenRadioButton";
            this.browseChosenRadioButton.Size = new System.Drawing.Size(122, 21);
            this.browseChosenRadioButton.TabIndex = 29;
            this.browseChosenRadioButton.Text = "浏览指定结果文件";
            this.browseChosenRadioButton.UseVisualStyleBackColor = true;
            this.browseChosenRadioButton.CheckedChanged += new System.EventHandler(this.BrowseChosenRadioButton_CheckedChanged);
            // 
            // paramRadioButton
            // 
            this.paramRadioButton.AutoSize = true;
            this.paramRadioButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.paramRadioButton.Location = new System.Drawing.Point(6, 65);
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
            this.stdoutRadioButton.Checked = true;
            this.stdoutRadioButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stdoutRadioButton.Location = new System.Drawing.Point(5, 33);
            this.stdoutRadioButton.Name = "stdoutRadioButton";
            this.stdoutRadioButton.Size = new System.Drawing.Size(195, 21);
            this.stdoutRadioButton.TabIndex = 27;
            this.stdoutRadioButton.TabStop = true;
            this.stdoutRadioButton.Text = "将脚本stdout重定向到结果文件";
            this.toolTip1.SetToolTip(this.stdoutRadioButton, "Python脚本的标准输出流(stdout)作为运算结果重定向到结果文件");
            this.stdoutRadioButton.UseVisualStyleBackColor = true;
            this.stdoutRadioButton.CheckedChanged += new System.EventHandler(this.StdoutRadioButton_CheckedChanged);
            // 
            // rsChosenButton
            // 
            this.rsChosenButton.Enabled = false;
            this.rsChosenButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rsChosenButton.Location = new System.Drawing.Point(445, 96);
            this.rsChosenButton.Name = "rsChosenButton";
            this.rsChosenButton.Size = new System.Drawing.Size(49, 23);
            this.rsChosenButton.TabIndex = 26;
            this.rsChosenButton.Text = "浏览+";
            this.rsChosenButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.rsChosenButton, "选择你的Python脚本生成结果文件的全路径");
            this.rsChosenButton.UseVisualStyleBackColor = true;
            this.rsChosenButton.Click += new System.EventHandler(this.RsChosenButton_Click);
            // 
            // browseChosenTextBox
            // 
            this.browseChosenTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.browseChosenTextBox.Location = new System.Drawing.Point(129, 96);
            this.browseChosenTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.browseChosenTextBox.Name = "browseChosenTextBox";
            this.browseChosenTextBox.ReadOnly = true;
            this.browseChosenTextBox.Size = new System.Drawing.Size(311, 23);
            this.browseChosenTextBox.TabIndex = 25;
            // 
            // paramPrefixTagTextBox
            // 
            this.paramPrefixTagTextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.paramPrefixTagTextBox.Location = new System.Drawing.Point(129, 62);
            this.paramPrefixTagTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.paramPrefixTagTextBox.Name = "paramPrefixTagTextBox";
            this.paramPrefixTagTextBox.Size = new System.Drawing.Size(32, 23);
            this.paramPrefixTagTextBox.TabIndex = 24;
            this.paramPrefixTagTextBox.Text = "-f";
            this.paramPrefixTagTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.paramPrefixTagTextBox, "参数标识");
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
            this.pyBrowseButton.Location = new System.Drawing.Point(464, 87);
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
            this.pyFullFilePathTextBox.Size = new System.Drawing.Size(322, 23);
            this.pyFullFilePathTextBox.TabIndex = 25;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "*.py";
            this.openFileDialog1.Filter = "Py脚本|*.py";
            this.openFileDialog1.Title = "选择要运行的外部自定义Python脚本";
            // 
            // PythonOperatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 339);
            this.ControlBox = false;
            this.Controls.Add(this.pyBrowseButton);
            this.Controls.Add(this.pyFullFilePathTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pyParamTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataSource0);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pythonChosenComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.confirmButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PythonOperatorView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Python算子设置";
            this.Load += new System.EventHandler(this.PythonOperatorView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox paramPrefixTagTextBox;
        private System.Windows.Forms.TextBox browseChosenTextBox;
        private System.Windows.Forms.Button rsChosenButton;
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
    }
}