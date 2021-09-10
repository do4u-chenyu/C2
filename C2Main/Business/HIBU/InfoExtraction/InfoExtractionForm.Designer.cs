
namespace C2.Business.HIBU.InfoExtraction
{
    partial class InfoExtractionForm
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
            this.textPathTextBox = new System.Windows.Forms.TextBox();
            this.singleButton = new System.Windows.Forms.Button();
            this.folderButton = new System.Windows.Forms.Button();
            this.extractButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.local = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 10005;
            this.label1.Text = "文本路径:";
            // 
            // textPathTextBox
            // 
            this.textPathTextBox.BackColor = System.Drawing.Color.White;
            this.textPathTextBox.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textPathTextBox.Location = new System.Drawing.Point(100, 13);
            this.textPathTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textPathTextBox.Name = "textPathTextBox";
            this.textPathTextBox.ReadOnly = true;
            this.textPathTextBox.Size = new System.Drawing.Size(458, 26);
            this.textPathTextBox.TabIndex = 10006;
            // 
            // singleButton
            // 
            this.singleButton.BackColor = System.Drawing.Color.Transparent;
            this.singleButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.singleButton.Location = new System.Drawing.Point(579, 13);
            this.singleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.singleButton.Name = "singleButton";
            this.singleButton.Size = new System.Drawing.Size(62, 26);
            this.singleButton.TabIndex = 10008;
            this.singleButton.Text = "单文本";
            this.singleButton.UseVisualStyleBackColor = false;
            this.singleButton.Click += new System.EventHandler(this.SingleButton_Click);
            // 
            // folderButton
            // 
            this.folderButton.BackColor = System.Drawing.Color.Transparent;
            this.folderButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.folderButton.Location = new System.Drawing.Point(663, 12);
            this.folderButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.folderButton.Name = "folderButton";
            this.folderButton.Size = new System.Drawing.Size(63, 25);
            this.folderButton.TabIndex = 10009;
            this.folderButton.Text = "多文本";
            this.folderButton.UseVisualStyleBackColor = false;
            this.folderButton.Click += new System.EventHandler(this.FolderButton_Click);
            // 
            // extractButton
            // 
            this.extractButton.BackColor = System.Drawing.Color.Transparent;
            this.extractButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.extractButton.Location = new System.Drawing.Point(753, 12);
            this.extractButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.extractButton.Name = "extractButton";
            this.extractButton.Size = new System.Drawing.Size(68, 25);
            this.extractButton.TabIndex = 10010;
            this.extractButton.Text = "开始抽取";
            this.extractButton.UseVisualStyleBackColor = false;
            this.extractButton.Click += new System.EventHandler(this.ExtractButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(14, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 10011;
            this.label2.Text = "抽取结果：";
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name.FillWeight = 15F;
            this.name.HeaderText = "姓名";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // local
            // 
            this.local.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.local.FillWeight = 50F;
            this.local.HeaderText = "地址";
            this.local.Name = "local";
            this.local.ReadOnly = true;
            // 
            // textName
            // 
            this.textName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.textName.FillWeight = 35F;
            this.textName.HeaderText = "文件名";
            this.textName.Name = "textName";
            this.textName.ReadOnly = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.textName,
            this.local,
            this.name});
            this.dataGridView1.Location = new System.Drawing.Point(100, 77);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(721, 338);
            this.dataGridView1.TabIndex = 10012;
            // 
            // InfoExtractionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(833, 472);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.extractButton);
            this.Controls.Add(this.folderButton);
            this.Controls.Add(this.singleButton);
            this.Controls.Add(this.textPathTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "InfoExtractionForm";
            this.Text = "InfoExtractionForm";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.textPathTextBox, 0);
            this.Controls.SetChildIndex(this.singleButton, 0);
            this.Controls.SetChildIndex(this.folderButton, 0);
            this.Controls.SetChildIndex(this.extractButton, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.dataGridView1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textPathTextBox;
        private System.Windows.Forms.Button singleButton;
        private System.Windows.Forms.Button folderButton;
        private System.Windows.Forms.Button extractButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn local;
        private System.Windows.Forms.DataGridViewTextBoxColumn textName;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}