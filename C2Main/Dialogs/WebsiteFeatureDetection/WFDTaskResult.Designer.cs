namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class WFDTaskResult
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prediction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.screenShot = new System.Windows.Forms.DataGridViewLinkColumn();
            this.webContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ipAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.browserButton = new System.Windows.Forms.Button();
            this.taskStatusLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.taskIDLabel = new System.Windows.Forms.Label();
            this.taskNameLabel = new System.Windows.Forms.Label();
            this.taskInfoLabel = new System.Windows.Forms.Label();
            this.statusInfoLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.screenShotGroupBox = new System.Windows.Forms.GroupBox();
            this.progressNum = new System.Windows.Forms.Label();
            this.progressInfo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.downloadPicsButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.screenShotGroupBox.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(225)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(198)))), ((int)(((byte)(231)))));
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(120)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView.ColumnHeadersHeight = 30;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.url,
            this.prediction,
            this.title,
            this.screenShot,
            this.webContent,
            this.ip,
            this.ipAddress});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 10F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView.RowHeadersWidth = 4;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(856, 315);
            this.dataGridView.TabIndex = 10008;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellContentClick);
            // 
            // url
            // 
            this.url.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.url.FillWeight = 18F;
            this.url.HeaderText = "url";
            this.url.MinimumWidth = 8;
            this.url.Name = "url";
            this.url.ReadOnly = true;
            this.url.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.url.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // prediction
            // 
            this.prediction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.prediction.FillWeight = 11F;
            this.prediction.HeaderText = "分类情况";
            this.prediction.MinimumWidth = 8;
            this.prediction.Name = "prediction";
            this.prediction.ReadOnly = true;
            this.prediction.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.prediction.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // title
            // 
            this.title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.title.FillWeight = 14F;
            this.title.HeaderText = "网站标题";
            this.title.MinimumWidth = 8;
            this.title.Name = "title";
            this.title.ReadOnly = true;
            this.title.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.title.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // screenShot
            // 
            this.screenShot.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.screenShot.FillWeight = 10F;
            this.screenShot.HeaderText = "网站截图";
            this.screenShot.MinimumWidth = 8;
            this.screenShot.Name = "screenShot";
            this.screenShot.ReadOnly = true;
            this.screenShot.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // webContent
            // 
            this.webContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.webContent.FillWeight = 26F;
            this.webContent.HeaderText = "网页文本";
            this.webContent.MinimumWidth = 8;
            this.webContent.Name = "webContent";
            this.webContent.ReadOnly = true;
            this.webContent.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.webContent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ip
            // 
            this.ip.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ip.FillWeight = 12F;
            this.ip.HeaderText = "ip";
            this.ip.MinimumWidth = 8;
            this.ip.Name = "ip";
            this.ip.ReadOnly = true;
            this.ip.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ip.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ipAddress
            // 
            this.ipAddress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ipAddress.FillWeight = 17F;
            this.ipAddress.HeaderText = "归属地";
            this.ipAddress.MinimumWidth = 8;
            this.ipAddress.Name = "ipAddress";
            this.ipAddress.ReadOnly = true;
            this.ipAddress.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ipAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(856, 152);
            this.panel1.TabIndex = 10012;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.browserButton);
            this.panel4.Controls.Add(this.taskStatusLabel);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.taskIDLabel);
            this.panel4.Controls.Add(this.taskNameLabel);
            this.panel4.Controls.Add(this.taskInfoLabel);
            this.panel4.Controls.Add(this.statusInfoLabel);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(523, 152);
            this.panel4.TabIndex = 10018;
            // 
            // browserButton
            // 
            this.browserButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.browserButton.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.browserButton.Location = new System.Drawing.Point(98, 109);
            this.browserButton.Name = "browserButton";
            this.browserButton.Size = new System.Drawing.Size(77, 28);
            this.browserButton.TabIndex = 10016;
            this.browserButton.Text = "详情";
            this.browserButton.UseVisualStyleBackColor = false;
            this.browserButton.Click += new System.EventHandler(this.BrowserButton_Click);
            // 
            // taskStatusLabel
            // 
            this.taskStatusLabel.AutoSize = true;
            this.taskStatusLabel.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.taskStatusLabel.Location = new System.Drawing.Point(97, 78);
            this.taskStatusLabel.Name = "taskStatusLabel";
            this.taskStatusLabel.Size = new System.Drawing.Size(48, 19);
            this.taskStatusLabel.TabIndex = 10019;
            this.taskStatusLabel.Text = "运行中";
            this.taskStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(20, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 10012;
            this.label1.Text = "任务名：";
            // 
            // taskIDLabel
            // 
            this.taskIDLabel.AutoSize = true;
            this.taskIDLabel.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.taskIDLabel.Location = new System.Drawing.Point(97, 45);
            this.taskIDLabel.Name = "taskIDLabel";
            this.taskIDLabel.Size = new System.Drawing.Size(181, 19);
            this.taskIDLabel.TabIndex = 10018;
            this.taskIDLabel.Text = "3ZF9wqCJ6aZmJfrDnFjKJ9";
            this.taskIDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.taskIDLabel, "双击复制任务ID到剪切板");
            this.taskIDLabel.Click += new System.EventHandler(this.TaskIDLabel_MouseDoubleClick);
            // 
            // taskNameLabel
            // 
            this.taskNameLabel.AutoSize = true;
            this.taskNameLabel.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.taskNameLabel.Location = new System.Drawing.Point(97, 10);
            this.taskNameLabel.Name = "taskNameLabel";
            this.taskNameLabel.Size = new System.Drawing.Size(49, 19);
            this.taskNameLabel.TabIndex = 10017;
            this.taskNameLabel.Text = "Name";
            // 
            // taskInfoLabel
            // 
            this.taskInfoLabel.AutoSize = true;
            this.taskInfoLabel.Location = new System.Drawing.Point(292, 51);
            this.taskInfoLabel.Name = "taskInfoLabel";
            this.taskInfoLabel.Size = new System.Drawing.Size(0, 12);
            this.taskInfoLabel.TabIndex = 10016;
            // 
            // statusInfoLabel
            // 
            this.statusInfoLabel.AutoSize = true;
            this.statusInfoLabel.Location = new System.Drawing.Point(292, 85);
            this.statusInfoLabel.Name = "statusInfoLabel";
            this.statusInfoLabel.Size = new System.Drawing.Size(0, 12);
            this.statusInfoLabel.TabIndex = 10015;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(20, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 19);
            this.label2.TabIndex = 10013;
            this.label2.Text = "任务ID：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(8, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 19);
            this.label3.TabIndex = 10014;
            this.label3.Text = "任务状态：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(8, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 19);
            this.label4.TabIndex = 10015;
            this.label4.Text = "结果预览：";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.screenShotGroupBox);
            this.panel3.Controls.Add(this.downloadPicsButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(523, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(333, 152);
            this.panel3.TabIndex = 10017;
            // 
            // screenShotGroupBox
            // 
            this.screenShotGroupBox.Controls.Add(this.progressNum);
            this.screenShotGroupBox.Controls.Add(this.progressInfo);
            this.screenShotGroupBox.Controls.Add(this.label6);
            this.screenShotGroupBox.Controls.Add(this.progressBar1);
            this.screenShotGroupBox.Controls.Add(this.label5);
            this.screenShotGroupBox.Location = new System.Drawing.Point(20, 15);
            this.screenShotGroupBox.Name = "screenShotGroupBox";
            this.screenShotGroupBox.Size = new System.Drawing.Size(299, 89);
            this.screenShotGroupBox.TabIndex = 10016;
            this.screenShotGroupBox.TabStop = false;
            this.screenShotGroupBox.Text = "截图下载";
            // 
            // progressNum
            // 
            this.progressNum.AutoSize = true;
            this.progressNum.Location = new System.Drawing.Point(266, 27);
            this.progressNum.Name = "progressNum";
            this.progressNum.Size = new System.Drawing.Size(17, 12);
            this.progressNum.TabIndex = 10017;
            this.progressNum.Text = "0%";
            // 
            // progressInfo
            // 
            this.progressInfo.AutoSize = true;
            this.progressInfo.Location = new System.Drawing.Point(69, 64);
            this.progressInfo.Name = "progressInfo";
            this.progressInfo.Size = new System.Drawing.Size(125, 12);
            this.progressInfo.TabIndex = 10016;
            this.progressInfo.Text = "已完成0张，失败0张。";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10015;
            this.label6.Text = "详细信息：";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(71, 23);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(189, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 10013;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 10014;
            this.label5.Text = "进度：";
            // 
            // downloadPicsButton
            // 
            this.downloadPicsButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.downloadPicsButton.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.downloadPicsButton.Location = new System.Drawing.Point(212, 110);
            this.downloadPicsButton.Name = "downloadPicsButton";
            this.downloadPicsButton.Size = new System.Drawing.Size(107, 28);
            this.downloadPicsButton.TabIndex = 10015;
            this.downloadPicsButton.Text = "下载全部截图";
            this.downloadPicsButton.UseVisualStyleBackColor = false;
            this.downloadPicsButton.Click += new System.EventHandler(this.DownloadPicsButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 152);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(856, 315);
            this.panel2.TabIndex = 10013;
            // 
            // WFDTaskResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(856, 467);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "WFDTaskResult";
            this.Text = "侦察兵-任务结果";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WFDTaskResult_FormClosing);
            this.Shown += new System.EventHandler(this.WFDTaskResult_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.screenShotGroupBox.ResumeLayout(false);
            this.screenShotGroupBox.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label statusInfoLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn url;
        private System.Windows.Forms.DataGridViewTextBoxColumn prediction;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewLinkColumn screenShot;
        private System.Windows.Forms.DataGridViewTextBoxColumn webContent;
        private System.Windows.Forms.DataGridViewTextBoxColumn ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn ipAddress;
        private System.Windows.Forms.Label taskInfoLabel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button browserButton;
        private System.Windows.Forms.Label taskStatusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label taskIDLabel;
        private System.Windows.Forms.Label taskNameLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox screenShotGroupBox;
        private System.Windows.Forms.Label progressNum;
        private System.Windows.Forms.Label progressInfo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button downloadPicsButton;
    }
}