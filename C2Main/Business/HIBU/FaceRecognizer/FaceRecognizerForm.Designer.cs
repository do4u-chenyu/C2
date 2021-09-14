
namespace C2.Business.HIBU.FaceRecognizer
{
    partial class FaceRecognizerForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.picName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folderBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.transBtn = new System.Windows.Forms.Button();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.browserBtn = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(2, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(826, 392);
            this.tabControl1.TabIndex = 10003;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.folderBtn);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.transBtn);
            this.tabPage1.Controls.Add(this.filePathTextBox);
            this.tabPage1.Controls.Add(this.browserBtn);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(818, 362);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "HI-FaceRecognizer";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.label3.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.label3.Location = new System.Drawing.Point(82, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(334, 19);
            this.label3.TabIndex = 10027;
            this.label3.Text = "支持生成单个图片或一个目录里所有图片的人脸识别编码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 10021;
            this.label1.Text = "图片路径:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.picName,
            this.transContent,
            this.prob});
            this.dataGridView1.Location = new System.Drawing.Point(86, 62);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(723, 297);
            this.dataGridView1.TabIndex = 10025;
            // 
            // picName
            // 
            this.picName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.picName.FillWeight = 30F;
            this.picName.HeaderText = "图片名";
            this.picName.Name = "picName";
            this.picName.ReadOnly = true;
            // 
            // transContent
            // 
            this.transContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.transContent.FillWeight = 40F;
            this.transContent.HeaderText = "编码";
            this.transContent.Name = "transContent";
            this.transContent.ReadOnly = true;
            // 
            // prob
            // 
            this.prob.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.prob.FillWeight = 30F;
            this.prob.HeaderText = "注意事项";
            this.prob.Name = "prob";
            this.prob.ReadOnly = true;
            // 
            // folderBtn
            // 
            this.folderBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.folderBtn.Location = new System.Drawing.Point(622, 5);
            this.folderBtn.Name = "folderBtn";
            this.folderBtn.Size = new System.Drawing.Size(66, 26);
            this.folderBtn.TabIndex = 10026;
            this.folderBtn.Text = "多张图片";
            this.folderBtn.UseVisualStyleBackColor = true;
            this.folderBtn.Click += new System.EventHandler(this.FolderBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(7, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 10022;
            this.label2.Text = "生成结果:";
            // 
            // transBtn
            // 
            this.transBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.transBtn.Location = new System.Drawing.Point(691, 5);
            this.transBtn.Name = "transBtn";
            this.transBtn.Size = new System.Drawing.Size(73, 26);
            this.transBtn.TabIndex = 10024;
            this.transBtn.Text = "开始生成";
            this.transBtn.UseVisualStyleBackColor = true;
            this.transBtn.Click += new System.EventHandler(this.TransBtn_Click);
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.BackColor = System.Drawing.Color.White;
            this.filePathTextBox.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filePathTextBox.Location = new System.Drawing.Point(83, 4);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.ReadOnly = true;
            this.filePathTextBox.Size = new System.Drawing.Size(460, 26);
            this.filePathTextBox.TabIndex = 10020;
            // 
            // browserBtn
            // 
            this.browserBtn.BackColor = System.Drawing.Color.Transparent;
            this.browserBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.browserBtn.Location = new System.Drawing.Point(555, 5);
            this.browserBtn.Name = "browserBtn";
            this.browserBtn.Size = new System.Drawing.Size(64, 26);
            this.browserBtn.TabIndex = 10023;
            this.browserBtn.Text = "单张图片";
            this.browserBtn.UseVisualStyleBackColor = false;
            this.browserBtn.Click += new System.EventHandler(this.BrowserBtn_Click);
            // 
            // FaceRecognizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(833, 438);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FaceRecognizerForm";
            this.Text = "人脸识别编码生成";
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn picName;
        private System.Windows.Forms.DataGridViewTextBoxColumn transContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn prob;
        private System.Windows.Forms.Button folderBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button transBtn;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.Button browserBtn;
    }
}