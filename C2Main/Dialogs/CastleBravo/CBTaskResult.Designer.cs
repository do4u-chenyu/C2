namespace C2.Dialogs.CastleBravo
{
    partial class CBTaskResult
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.browserButton = new System.Windows.Forms.Button();
            this.taskStatusLabel = new System.Windows.Forms.Label();
            this.taskIdLabel = new System.Windows.Forms.Label();
            this.taskNameLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.md5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.browserButton);
            this.panel1.Controls.Add(this.taskStatusLabel);
            this.panel1.Controls.Add(this.taskIdLabel);
            this.panel1.Controls.Add(this.taskNameLabel);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 152);
            this.panel1.TabIndex = 0;
            // 
            // browserButton
            // 
            this.browserButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.browserButton.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.browserButton.Location = new System.Drawing.Point(156, 109);
            this.browserButton.Name = "browserButton";
            this.browserButton.Size = new System.Drawing.Size(77, 28);
            this.browserButton.TabIndex = 7;
            this.browserButton.Text = "详情";
            this.browserButton.UseVisualStyleBackColor = false;
            this.browserButton.Click += new System.EventHandler(this.BrowserButton_Click);
            // 
            // taskStatusLabel
            // 
            this.taskStatusLabel.AutoSize = true;
            this.taskStatusLabel.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.taskStatusLabel.Location = new System.Drawing.Point(161, 80);
            this.taskStatusLabel.Name = "taskStatusLabel";
            this.taskStatusLabel.Size = new System.Drawing.Size(48, 19);
            this.taskStatusLabel.TabIndex = 6;
            this.taskStatusLabel.Text = "运行中";
            // 
            // taskIdLabel
            // 
            this.taskIdLabel.AutoSize = true;
            this.taskIdLabel.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.taskIdLabel.Location = new System.Drawing.Point(161, 46);
            this.taskIdLabel.Name = "taskIdLabel";
            this.taskIdLabel.Size = new System.Drawing.Size(23, 19);
            this.taskIdLabel.TabIndex = 5;
            this.taskIdLabel.Text = "ID";
            // 
            // taskNameLabel
            // 
            this.taskNameLabel.AutoSize = true;
            this.taskNameLabel.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.taskNameLabel.Location = new System.Drawing.Point(161, 11);
            this.taskNameLabel.Name = "taskNameLabel";
            this.taskNameLabel.Size = new System.Drawing.Size(49, 19);
            this.taskNameLabel.TabIndex = 4;
            this.taskNameLabel.Text = "Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(66, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "结果预览：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(66, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "任务状态：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(78, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "任务ID：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(78, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "任务名：";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.md5,
            this.type,
            this.value,
            this.salt});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 152);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(775, 328);
            this.dataGridView.TabIndex = 1;
            // 
            // md5
            // 
            this.md5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.md5.FillWeight = 40F;
            this.md5.HeaderText = "MD5";
            this.md5.Name = "md5";
            this.md5.ReadOnly = true;
            // 
            // type
            // 
            this.type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.type.FillWeight = 15F;
            this.type.HeaderText = "加密方式";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            // 
            // value
            // 
            this.value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.value.FillWeight = 30F;
            this.value.HeaderText = "结果";
            this.value.Name = "value";
            this.value.ReadOnly = true;
            // 
            // salt
            // 
            this.salt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.salt.FillWeight = 15F;
            this.salt.HeaderText = "盐(Salt)";
            this.salt.Name = "salt";
            this.salt.ReadOnly = true;
            // 
            // CBTaskResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(775, 480);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panel1);
            this.Name = "CBTaskResult";
            this.Text = "喝彩城堡-任务结果";
            this.Shown += new System.EventHandler(this.CBTaskResult_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button browserButton;
        private System.Windows.Forms.Label taskStatusLabel;
        private System.Windows.Forms.Label taskIdLabel;
        private System.Windows.Forms.Label taskNameLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn md5;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.DataGridViewTextBoxColumn salt;
    }
}