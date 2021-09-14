namespace C2.Business.HIBU.TerrorismDetection
{
    partial class TerrorismDetectionForm
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
            this.HITab = new System.Windows.Forms.TabPage();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.browserBtn = new System.Windows.Forms.Button();
            this.folderBtn = new System.Windows.Forms.Button();
            this.transBtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.picName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.organizationContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.personContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.preContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.地址 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HITab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // HITab
            // 
            this.HITab.Controls.Add(this.filePathTextBox);
            this.HITab.Controls.Add(this.browserBtn);
            this.HITab.Controls.Add(this.folderBtn);
            this.HITab.Controls.Add(this.transBtn);
            this.HITab.Controls.Add(this.dataGridView1);
            this.HITab.Controls.Add(this.label1);
            this.HITab.Controls.Add(this.label2);
            this.HITab.Controls.Add(this.label3);
            this.HITab.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HITab.Location = new System.Drawing.Point(4, 26);
            this.HITab.Name = "HITab";
            this.HITab.Padding = new System.Windows.Forms.Padding(3);
            this.HITab.Size = new System.Drawing.Size(772, 337);
            this.HITab.TabIndex = 0;
            this.HITab.Text = "HI-TerrorismDetection";
            this.HITab.UseVisualStyleBackColor = true;
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.BackColor = System.Drawing.Color.White;
            this.filePathTextBox.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filePathTextBox.Location = new System.Drawing.Point(80, 17);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.ReadOnly = true;
            this.filePathTextBox.Size = new System.Drawing.Size(438, 26);
            this.filePathTextBox.TabIndex = 10003;
            // 
            // browserBtn
            // 
            this.browserBtn.BackColor = System.Drawing.Color.Transparent;
            this.browserBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.browserBtn.Location = new System.Drawing.Point(524, 17);
            this.browserBtn.Name = "browserBtn";
            this.browserBtn.Size = new System.Drawing.Size(72, 26);
            this.browserBtn.TabIndex = 10007;
            this.browserBtn.Text = "单张图片";
            this.browserBtn.UseVisualStyleBackColor = false;
            this.browserBtn.Click += new System.EventHandler(this.BrowserBtn_Click);
            // 
            // folderBtn
            // 
            this.folderBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.folderBtn.Location = new System.Drawing.Point(608, 17);
            this.folderBtn.Name = "folderBtn";
            this.folderBtn.Size = new System.Drawing.Size(67, 26);
            this.folderBtn.TabIndex = 10010;
            this.folderBtn.Text = "多张图片";
            this.folderBtn.UseVisualStyleBackColor = true;
            this.folderBtn.Click += new System.EventHandler(this.FolderBtn_Click);
            // 
            // transBtn
            // 
            this.transBtn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.transBtn.Location = new System.Drawing.Point(691, 17);
            this.transBtn.Name = "transBtn";
            this.transBtn.Size = new System.Drawing.Size(73, 26);
            this.transBtn.TabIndex = 10008;
            this.transBtn.Text = "开始识别";
            this.transBtn.UseVisualStyleBackColor = true;
            this.transBtn.Click += new System.EventHandler(this.TransBtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.picName,
            this.addressContent,
            this.organizationContent,
            this.personContent,
            this.preContent});
            this.dataGridView1.Location = new System.Drawing.Point(80, 75);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(686, 258);
            this.dataGridView1.TabIndex = 10009;
            // 
            // picName
            // 
            this.picName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.picName.FillWeight = 40F;
            this.picName.HeaderText = "图片路径";
            this.picName.Name = "picName";
            this.picName.ReadOnly = true;
            // 
            // addressContent
            // 
            this.addressContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.addressContent.FillWeight = 60F;
            this.addressContent.HeaderText = "是否涉恐";
            this.addressContent.Name = "addressContent";
            this.addressContent.ReadOnly = true;
            // 
            // organizationContent
            // 
            this.organizationContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.organizationContent.FillWeight = 60F;
            this.organizationContent.HeaderText = "涉恐类别";
            this.organizationContent.Name = "organizationContent";
            this.organizationContent.ReadOnly = true;
            // 
            // personContent
            // 
            this.personContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.personContent.FillWeight = 60F;
            this.personContent.HeaderText = "准确率";
            this.personContent.Name = "personContent";
            this.personContent.ReadOnly = true;
            // 
            // preContent
            // 
            this.preContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.preContent.FillWeight = 60F;
            this.preContent.HeaderText = "图像位置";
            this.preContent.Name = "preContent";
            this.preContent.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 10004;
            this.label1.Text = "图片路径:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(7, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 10005;
            this.label2.Text = "识别结果:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.label3.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.label3.Location = new System.Drawing.Point(82, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(308, 19);
            this.label3.TabIndex = 10011;
            this.label3.Text = "支持识别单个图片或一个目录里所有图片的涉恐信息";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.HITab);
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(780, 367);
            this.tabControl1.TabIndex = 10011;
            // 
            // 地址
            // 
            this.地址.Name = "地址";
            // 
            // TerrorismDetectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(786, 424);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "TerrorismDetectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "涉恐图像识别";
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.HITab.ResumeLayout(false);
            this.HITab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage HITab;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button browserBtn;
        private System.Windows.Forms.Button folderBtn;
        private System.Windows.Forms.Button transBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 地址;
        private System.Windows.Forms.DataGridViewTextBoxColumn picName;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn organizationContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn personContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn preContent;
    }
}