namespace Citta_T1.Dialogs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.userModelConfigPage = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.userModelCancelButton = new System.Windows.Forms.Button();
            this.userModelOkButton = new System.Windows.Forms.Button();
            this.userModelTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.aboutPage = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.aboutCancelButton = new System.Windows.Forms.Button();
            this.aboutOkButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pythonOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl.SuspendLayout();
            this.userModelConfigPage.SuspendLayout();
            this.pythonConfigPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.aboutPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.userModelConfigPage);
            this.tabControl.Controls.Add(this.pythonConfigPage);
            this.tabControl.Controls.Add(this.aboutPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(642, 410);
            this.tabControl.TabIndex = 0;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl_Selected);
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
            this.userModelConfigPage.Text = "用户模型路径";
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
            this.label3.Text = "用户模型目录存储了当前所有用户的模型,配置信息和运算临时文件。";
            // 
            // userModelCancelButton
            // 
            this.userModelCancelButton.Location = new System.Drawing.Point(551, 349);
            this.userModelCancelButton.Name = "userModelCancelButton";
            this.userModelCancelButton.Size = new System.Drawing.Size(75, 23);
            this.userModelCancelButton.TabIndex = 3;
            this.userModelCancelButton.Text = "取消";
            this.userModelCancelButton.UseVisualStyleBackColor = true;
            this.userModelCancelButton.Click += new System.EventHandler(this.UserModelCancelButton_Click);
            // 
            // userModelOkButton
            // 
            this.userModelOkButton.Location = new System.Drawing.Point(458, 349);
            this.userModelOkButton.Name = "userModelOkButton";
            this.userModelOkButton.Size = new System.Drawing.Size(75, 23);
            this.userModelOkButton.TabIndex = 2;
            this.userModelOkButton.Text = "确认";
            this.userModelOkButton.UseVisualStyleBackColor = true;
            this.userModelOkButton.Click += new System.EventHandler(this.UserModelOkButton_Click);
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
            this.label2.Text = "用户模型存储路径:";
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
            this.toolTip1.SetToolTip(this.label7, "Python算子默认使用的虚拟机");
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
            this.aliasColumn.Name = "aliasColumn";
            this.aliasColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.aliasColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.aliasColumn.ToolTipText = "自定义的别名，用来区分不同版本的Python解释器";
            // 
            // chosenColumn
            // 
            this.chosenColumn.FillWeight = 30F;
            this.chosenColumn.HeaderText = "选中";
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
            // aboutPage
            // 
            this.aboutPage.Controls.Add(this.label8);
            this.aboutPage.Controls.Add(this.textBox4);
            this.aboutPage.Controls.Add(this.button7);
            this.aboutPage.Controls.Add(this.textBox3);
            this.aboutPage.Controls.Add(this.aboutCancelButton);
            this.aboutPage.Controls.Add(this.aboutOkButton);
            this.aboutPage.Controls.Add(this.label6);
            this.aboutPage.Controls.Add(this.label5);
            this.aboutPage.Location = new System.Drawing.Point(4, 26);
            this.aboutPage.Name = "aboutPage";
            this.aboutPage.Padding = new System.Windows.Forms.Padding(3);
            this.aboutPage.Size = new System.Drawing.Size(634, 380);
            this.aboutPage.TabIndex = 3;
            this.aboutPage.Text = "关于和注册";
            this.aboutPage.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 295);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(249, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "授权 FiberHome IAO 到期时间 2025.12.31";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.textBox4.Location = new System.Drawing.Point(11, 50);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(537, 172);
            this.textBox4.TabIndex = 8;
            this.textBox4.Text = "IAO可视化数据分析平台，支持BCP,XLS,CVS,TXT等多种数据格式，支持多模型文档，算子并行运算，同时支持基于Python的自定义算子，具有灵活的分析能力" +
    "和友善的交互界面,学习成本低，功能丰富，是IAO自主研发的新一代可视化数据分析平台。";
            // 
            // button7
            // 
            this.button7.Enabled = false;
            this.button7.Location = new System.Drawing.Point(489, 256);
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
            this.textBox3.Location = new System.Drawing.Point(11, 256);
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
            this.aboutOkButton.Click += new System.EventHandler(this.AboutOkButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 225);
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
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "首选项";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.tabControl.ResumeLayout(false);
            this.userModelConfigPage.ResumeLayout(false);
            this.userModelConfigPage.PerformLayout();
            this.pythonConfigPage.ResumeLayout(false);
            this.pythonConfigPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.aboutPage.ResumeLayout(false);
            this.aboutPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage userModelConfigPage;
        private System.Windows.Forms.TabPage pythonConfigPage;
        private System.Windows.Forms.TabPage aboutPage;
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn interpreterFFPColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aliasColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chosenColumn;
    }
}