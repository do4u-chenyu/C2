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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.inputPathTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.analyse = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.jdkPathTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ICON = new System.Windows.Forms.DataGridViewImageColumn();
            this.fileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApkName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainFunction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipLable = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.导出到ExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.Location = new System.Drawing.Point(27, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入apk存放目录";
            // 
            // inputPathTextBox
            // 
            this.inputPathTextBox.Location = new System.Drawing.Point(201, 25);
            this.inputPathTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.inputPathTextBox.Name = "inputPathTextBox";
            this.inputPathTextBox.Size = new System.Drawing.Size(509, 25);
            this.inputPathTextBox.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.analyse);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 396);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(895, 54);
            this.panel1.TabIndex = 6;
            // 
            // analyse
            // 
            this.analyse.Location = new System.Drawing.Point(696, 12);
            this.analyse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.analyse.Name = "analyse";
            this.analyse.Size = new System.Drawing.Size(75, 30);
            this.analyse.TabIndex = 3;
            this.analyse.Text = "解析";
            this.analyse.UseVisualStyleBackColor = true;
            this.analyse.Click += new System.EventHandler(this.Analyse_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(811, 12);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 30);
            this.button2.TabIndex = 4;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // jdkPathTextBox
            // 
            this.jdkPathTextBox.Location = new System.Drawing.Point(201, 59);
            this.jdkPathTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.jdkPathTextBox.Name = "jdkPathTextBox";
            this.jdkPathTextBox.Size = new System.Drawing.Size(509, 25);
            this.jdkPathTextBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.Location = new System.Drawing.Point(27, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "请输入jdk所在路径";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::C2.Properties.Resources.folder_sys;
            this.pictureBox1.Location = new System.Drawing.Point(728, 28);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 22);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.InputPathPictureBox_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::C2.Properties.Resources.folder_sys;
            this.pictureBox2.Location = new System.Drawing.Point(728, 61);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(21, 22);
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.JdkPathPictureBox_Click);
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
            this.dataGridView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dataGridView1.Location = new System.Drawing.Point(0, 112);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(895, 291);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridView1_MouseDown);
            // 
            // ICON
            // 
            this.ICON.FillWeight = 17F;
            this.ICON.HeaderText = "ICON";
            this.ICON.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.ICON.MinimumWidth = 6;
            this.ICON.Name = "ICON";
            this.ICON.ReadOnly = true;
            // 
            // fileName
            // 
            this.fileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.fileName.HeaderText = "文件名";
            this.fileName.MinimumWidth = 6;
            this.fileName.Name = "fileName";
            this.fileName.ReadOnly = true;
            this.fileName.Width = 81;
            // 
            // ApkName
            // 
            this.ApkName.FillWeight = 15F;
            this.ApkName.HeaderText = "Apk名";
            this.ApkName.MinimumWidth = 6;
            this.ApkName.Name = "ApkName";
            this.ApkName.ReadOnly = true;
            // 
            // packageName
            // 
            this.packageName.FillWeight = 30F;
            this.packageName.HeaderText = "包名";
            this.packageName.MinimumWidth = 6;
            this.packageName.Name = "packageName";
            this.packageName.ReadOnly = true;
            // 
            // mainFunction
            // 
            this.mainFunction.FillWeight = 45F;
            this.mainFunction.HeaderText = "主函数";
            this.mainFunction.MinimumWidth = 6;
            this.mainFunction.Name = "mainFunction";
            this.mainFunction.ReadOnly = true;
            // 
            // size
            // 
            this.size.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.size.HeaderText = "大小";
            this.size.MinimumWidth = 6;
            this.size.Name = "size";
            this.size.ReadOnly = true;
            this.size.Width = 66;
            // 
            // tipLable
            // 
            this.tipLable.AutoSize = true;
            this.tipLable.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.tipLable.Location = new System.Drawing.Point(28, 94);
            this.tipLable.Name = "tipLable";
            this.tipLable.Size = new System.Drawing.Size(466, 15);
            this.tipLable.TabIndex = 12;
            this.tipLable.Text = "jdk路径格式：C:\\Program Files\\Java\\jdk-13.0.1\\bin\\java.exe";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导出到ExcelToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 28);
            // 
            // 导出到ExcelToolStripMenuItem
            // 
            this.导出到ExcelToolStripMenuItem.Name = "导出到ExcelToolStripMenuItem";
            this.导出到ExcelToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
            this.导出到ExcelToolStripMenuItem.Text = "导出到Excel";
            this.导出到ExcelToolStripMenuItem.Click += new System.EventHandler(this.ExportToExcelToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(655, 88);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(210, 25);
            this.textBox1.TabIndex = 14;
            // 
            // ApkTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(895, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tipLable);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.jdkPathTextBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.inputPathTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ApkTool";
            this.Text = "非法APK分析";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputPathTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button analyse;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox jdkPathTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label tipLable;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 导出到ExcelToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataGridViewImageColumn ICON;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApkName;
        private System.Windows.Forms.DataGridViewTextBoxColumn packageName;
        private System.Windows.Forms.DataGridViewTextBoxColumn mainFunction;
        private System.Windows.Forms.DataGridViewTextBoxColumn size;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
    }
}