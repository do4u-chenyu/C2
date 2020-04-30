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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.userModelConfigPage = new System.Windows.Forms.TabPage();
            this.pythonConfigPage = new System.Windows.Forms.TabPage();
            this.socialNetworkConfigPage = new System.Windows.Forms.TabPage();
            this.aboutPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.interpreterFullPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.userModelConfigPage.SuspendLayout();
            this.pythonConfigPage.SuspendLayout();
            this.aboutPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.userModelConfigPage);
            this.tabControl.Controls.Add(this.pythonConfigPage);
            this.tabControl.Controls.Add(this.socialNetworkConfigPage);
            this.tabControl.Controls.Add(this.aboutPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(642, 410);
            this.tabControl.TabIndex = 0;
            // 
            // userModelConfigPage
            // 
            this.userModelConfigPage.Controls.Add(this.label4);
            this.userModelConfigPage.Controls.Add(this.label3);
            this.userModelConfigPage.Controls.Add(this.button4);
            this.userModelConfigPage.Controls.Add(this.button3);
            this.userModelConfigPage.Controls.Add(this.textBox2);
            this.userModelConfigPage.Controls.Add(this.label2);
            this.userModelConfigPage.Location = new System.Drawing.Point(4, 26);
            this.userModelConfigPage.Name = "userModelConfigPage";
            this.userModelConfigPage.Padding = new System.Windows.Forms.Padding(3);
            this.userModelConfigPage.Size = new System.Drawing.Size(634, 380);
            this.userModelConfigPage.TabIndex = 0;
            this.userModelConfigPage.Text = "用户模型路径";
            this.userModelConfigPage.UseVisualStyleBackColor = true;
            // 
            // pythonConfigPage
            // 
            this.pythonConfigPage.Controls.Add(this.button2);
            this.pythonConfigPage.Controls.Add(this.button1);
            this.pythonConfigPage.Controls.Add(this.dataGridView);
            this.pythonConfigPage.Controls.Add(this.browseButton);
            this.pythonConfigPage.Controls.Add(this.textBox1);
            this.pythonConfigPage.Controls.Add(this.label1);
            this.pythonConfigPage.Location = new System.Drawing.Point(4, 26);
            this.pythonConfigPage.Name = "pythonConfigPage";
            this.pythonConfigPage.Padding = new System.Windows.Forms.Padding(3);
            this.pythonConfigPage.Size = new System.Drawing.Size(634, 380);
            this.pythonConfigPage.TabIndex = 1;
            this.pythonConfigPage.Text = "Python引擎";
            this.pythonConfigPage.UseVisualStyleBackColor = true;
            // 
            // socialNetworkConfigPage
            // 
            this.socialNetworkConfigPage.Location = new System.Drawing.Point(4, 26);
            this.socialNetworkConfigPage.Name = "socialNetworkConfigPage";
            this.socialNetworkConfigPage.Size = new System.Drawing.Size(634, 380);
            this.socialNetworkConfigPage.TabIndex = 2;
            this.socialNetworkConfigPage.Text = "社交关系分析";
            this.socialNetworkConfigPage.UseVisualStyleBackColor = true;
            // 
            // aboutPage
            // 
            this.aboutPage.Controls.Add(this.button7);
            this.aboutPage.Controls.Add(this.textBox3);
            this.aboutPage.Controls.Add(this.button5);
            this.aboutPage.Controls.Add(this.button6);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pyhton虚拟机路径(Python Interpreter)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(5, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(572, 23);
            this.textBox1.TabIndex = 1;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(580, 49);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(49, 23);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "浏览+";
            this.browseButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.browseButton.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.interpreterFullPath,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView.Location = new System.Drawing.Point(6, 93);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView.Size = new System.Drawing.Size(620, 250);
            this.dataGridView.TabIndex = 3;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(458, 349);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(551, 349);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // interpreterFullPath
            // 
            this.interpreterFullPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.interpreterFullPath.DefaultCellStyle = dataGridViewCellStyle1;
            this.interpreterFullPath.FillWeight = 320F;
            this.interpreterFullPath.HeaderText = "虚拟机路径";
            this.interpreterFullPath.Name = "interpreterFullPath";
            this.interpreterFullPath.ReadOnly = true;
            this.interpreterFullPath.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.interpreterFullPath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.interpreterFullPath.Width = 422;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 80F;
            this.Column2.HeaderText = "别名";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 30F;
            this.Column3.HeaderText = "选中";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "用户模型存储路径:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 77);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(620, 23);
            this.textBox2.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(458, 349);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "确认";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(551, 349);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "取消";
            this.button4.UseVisualStyleBackColor = true;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "由程序在安装时指定。";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "关于";
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
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(551, 349);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "取消";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(458, 349);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 4;
            this.button6.Text = "确认";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(11, 256);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(463, 23);
            this.textBox3.TabIndex = 6;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(489, 256);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(59, 23);
            this.button7.TabIndex = 7;
            this.button7.Text = "提交";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 410);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "首选项";
            this.tabControl.ResumeLayout(false);
            this.userModelConfigPage.ResumeLayout(false);
            this.userModelConfigPage.PerformLayout();
            this.pythonConfigPage.ResumeLayout(false);
            this.pythonConfigPage.PerformLayout();
            this.aboutPage.ResumeLayout(false);
            this.aboutPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage userModelConfigPage;
        private System.Windows.Forms.TabPage pythonConfigPage;
        private System.Windows.Forms.TabPage socialNetworkConfigPage;
        private System.Windows.Forms.TabPage aboutPage;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn interpreterFullPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button7;
    }
}