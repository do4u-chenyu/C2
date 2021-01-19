namespace C2.Dialogs.IAOLab
{
    partial class ApkTool
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
            this.label1 = new System.Windows.Forms.Label();
            this.inputPath = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.analyse = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.jdkPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tipLable = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.ICON = new System.Windows.Forms.DataGridViewImageColumn();
            this.fileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApkName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainFunction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入apk存放目录";
            // 
            // inputPath
            // 
            this.inputPath.Location = new System.Drawing.Point(151, 20);
            this.inputPath.Margin = new System.Windows.Forms.Padding(2);
            this.inputPath.Name = "inputPath";
            this.inputPath.Size = new System.Drawing.Size(383, 21);
            this.inputPath.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.analyse);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 317);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(671, 43);
            this.panel1.TabIndex = 6;
            // 
            // analyse
            // 
            this.analyse.Location = new System.Drawing.Point(522, 10);
            this.analyse.Margin = new System.Windows.Forms.Padding(2);
            this.analyse.Name = "analyse";
            this.analyse.Size = new System.Drawing.Size(56, 24);
            this.analyse.TabIndex = 3;
            this.analyse.Text = "解析";
            this.analyse.UseVisualStyleBackColor = true;
            this.analyse.Click += new System.EventHandler(this.Analyse_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(608, 10);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 24);
            this.button2.TabIndex = 4;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // jdkPath
            // 
            this.jdkPath.Location = new System.Drawing.Point(151, 47);
            this.jdkPath.Margin = new System.Windows.Forms.Padding(2);
            this.jdkPath.Name = "jdkPath";
            this.jdkPath.Size = new System.Drawing.Size(383, 21);
            this.jdkPath.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.Location = new System.Drawing.Point(20, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "请输入jdk所在路径";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::C2.Properties.Resources.folder_sys;
            this.pictureBox1.Location = new System.Drawing.Point(546, 22);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 18);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::C2.Properties.Resources.folder_sys;
            this.pictureBox2.Location = new System.Drawing.Point(546, 49);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 18);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ICON,
            this.fileName,
            this.ApkName,
            this.packageName,
            this.mainFunction,
            this.size});
            this.dataGridView1.Location = new System.Drawing.Point(0, 90);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(671, 233);
            this.dataGridView1.TabIndex = 11;
            // 
            // tipLable
            // 
            this.tipLable.AutoSize = true;
            this.tipLable.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.tipLable.Location = new System.Drawing.Point(21, 75);
            this.tipLable.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tipLable.Name = "tipLable";
            this.tipLable.Size = new System.Drawing.Size(299, 12);
            this.tipLable.TabIndex = 12;
            this.tipLable.Text = "jdk路径格式：C:\\Program Files\\Java\\jdk-13.0.1\\bin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Location = new System.Drawing.Point(321, 75);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "APK分析用时较长，请耐心等待";
            // 
            // ICON
            // 
            this.ICON.HeaderText = "ICON";
            this.ICON.Name = "ICON";
            this.ICON.ReadOnly = true;
            // 
            // fileName
            // 
            this.fileName.HeaderText = "文件名";
            this.fileName.Name = "fileName";
            this.fileName.ReadOnly = true;
            // 
            // ApkName
            // 
            this.ApkName.HeaderText = "Apk名";
            this.ApkName.Name = "ApkName";
            this.ApkName.ReadOnly = true;
            // 
            // packageName
            // 
            this.packageName.HeaderText = "包名";
            this.packageName.Name = "packageName";
            this.packageName.ReadOnly = true;
            // 
            // mainFunction
            // 
            this.mainFunction.HeaderText = "主函数";
            this.mainFunction.Name = "mainFunction";
            this.mainFunction.ReadOnly = true;
            // 
            // size
            // 
            this.size.HeaderText = "大小";
            this.size.Name = "size";
            this.size.ReadOnly = true;
            // 
            // ApkTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(671, 360);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tipLable);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.jdkPath);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.inputPath);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ApkTool";
            this.Text = "非法APK分析";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputPath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button analyse;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox jdkPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label tipLable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridViewImageColumn ICON;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApkName;
        private System.Windows.Forms.DataGridViewTextBoxColumn packageName;
        private System.Windows.Forms.DataGridViewTextBoxColumn mainFunction;
        private System.Windows.Forms.DataGridViewTextBoxColumn size;
    }
}