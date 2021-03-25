namespace C2.SearchToolkit
{
    partial class SearchToolkitForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchToolkitForm));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.searchAgentIPTB = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.bastionIPTB = new System.Windows.Forms.TextBox();
            this.passwordTB = new System.Windows.Forms.TextBox();
            this.usernameTB = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.taskNameTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.taskModelComboBox = new System.Windows.Forms.ComboBox();
            this.downloadButton = new System.Windows.Forms.Button();
            this.remoteWorkspaceTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.confirmButton = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.taskInfoGB = new System.Windows.Forms.GroupBox();
            this.taskStatusLabel = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelCenter.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.taskInfoGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.searchAgentIPTB);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.bastionIPTB);
            this.groupBox2.Controls.Add(this.passwordTB);
            this.groupBox2.Controls.Add(this.usernameTB);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(609, 165);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "堡垒机配置";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.label2.Location = new System.Drawing.Point(428, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 19);
            this.label2.TabIndex = 43;
            this.label2.Text = "已申请的堡垒机用户名和密码";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 120);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(87, 19);
            this.label14.TabIndex = 7;
            this.label14.Text = "全文主节点IP";
            // 
            // searchAgentIPTB
            // 
            this.searchAgentIPTB.Location = new System.Drawing.Point(100, 116);
            this.searchAgentIPTB.Name = "searchAgentIPTB";
            this.searchAgentIPTB.Size = new System.Drawing.Size(136, 25);
            this.searchAgentIPTB.TabIndex = 6;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(29, 77);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 19);
            this.label15.TabIndex = 5;
            this.label15.Text = "堡垒机IP";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(244, 35);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 19);
            this.label16.TabIndex = 4;
            this.label16.Text = "密码";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(42, 35);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 19);
            this.label17.TabIndex = 3;
            this.label17.Text = "用户名";
            // 
            // bastionIPTB
            // 
            this.bastionIPTB.Location = new System.Drawing.Point(100, 75);
            this.bastionIPTB.Name = "bastionIPTB";
            this.bastionIPTB.Size = new System.Drawing.Size(136, 25);
            this.bastionIPTB.TabIndex = 2;
            // 
            // passwordTB
            // 
            this.passwordTB.Location = new System.Drawing.Point(285, 34);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.PasswordChar = '*';
            this.passwordTB.Size = new System.Drawing.Size(136, 25);
            this.passwordTB.TabIndex = 1;
            // 
            // usernameTB
            // 
            this.usernameTB.Location = new System.Drawing.Point(100, 34);
            this.usernameTB.Name = "usernameTB";
            this.usernameTB.Size = new System.Drawing.Size(136, 25);
            this.usernameTB.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox1.Controls.Add(this.taskInfoGB);
            this.groupBox1.Controls.Add(this.taskNameTB);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.taskModelComboBox);
            this.groupBox1.Controls.Add(this.downloadButton);
            this.groupBox1.Controls.Add(this.remoteWorkspaceTB);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(609, 169);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模型配置";
            // 
            // taskNameTB
            // 
            this.taskNameTB.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.taskNameTB.Location = new System.Drawing.Point(99, 36);
            this.taskNameTB.Name = "taskNameTB";
            this.taskNameTB.Size = new System.Drawing.Size(180, 25);
            this.taskNameTB.TabIndex = 44;
            this.taskNameTB.Text = "全文涉赌模型";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 43;
            this.label5.Text = "任务名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 42;
            this.label1.Text = "模型选择";
            // 
            // modelComboBox
            // 
            this.taskModelComboBox.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.taskModelComboBox.FormattingEnabled = true;
            this.taskModelComboBox.Items.AddRange(new object[] {
            "全文涉赌模型",
            "全文涉枪模型",
            "全文涉黄模型",
            "全文飞机场模型"});
            this.taskModelComboBox.Location = new System.Drawing.Point(99, 81);
            this.taskModelComboBox.Name = "modelComboBox";
            this.taskModelComboBox.Size = new System.Drawing.Size(180, 27);
            this.taskModelComboBox.TabIndex = 41;
            this.taskModelComboBox.Text = "全文涉赌模型";
            this.taskModelComboBox.SelectedIndexChanged += new System.EventHandler(this.ModelComboBox_SelectedIndexChanged);
            // 
            // downloadButton
            // 
            this.downloadButton.Enabled = false;
            this.downloadButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.downloadButton.Location = new System.Drawing.Point(527, 128);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(68, 27);
            this.downloadButton.TabIndex = 37;
            this.downloadButton.Text = "下载结果";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // remoteWorkspaceTB
            // 
            this.remoteWorkspaceTB.BackColor = System.Drawing.SystemColors.Window;
            this.remoteWorkspaceTB.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.remoteWorkspaceTB.Location = new System.Drawing.Point(99, 128);
            this.remoteWorkspaceTB.Name = "remoteWorkspaceTB";
            this.remoteWorkspaceTB.Size = new System.Drawing.Size(408, 25);
            this.remoteWorkspaceTB.TabIndex = 36;
            this.remoteWorkspaceTB.Text = "/tmp/iao/search/gamble";
            this.remoteWorkspaceTB.Enter += new System.EventHandler(this.LinuxWorkspaceTB_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 35;
            this.label4.Text = "远程目录";
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.groupBox2);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelCenter.Location = new System.Drawing.Point(0, 169);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(609, 165);
            this.panelCenter.TabIndex = 3;
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panelBottom.Controls.Add(this.cancelButton);
            this.panelBottom.Controls.Add(this.confirmButton);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 334);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(609, 50);
            this.panelBottom.TabIndex = 4;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cancelButton.Location = new System.Drawing.Point(533, 12);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(60, 27);
            this.cancelButton.TabIndex = 40;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.confirmButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.confirmButton.Location = new System.Drawing.Point(410, 12);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            this.confirmButton.TabIndex = 39;
            this.confirmButton.Text = "创建";
            this.confirmButton.UseVisualStyleBackColor = false;
            this.confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.groupBox1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(609, 169);
            this.panelTop.TabIndex = 2;
            // 
            // taskInfoGB
            // 
            this.taskInfoGB.Controls.Add(this.taskStatusLabel);
            this.taskInfoGB.Location = new System.Drawing.Point(294, 24);
            this.taskInfoGB.Name = "taskInfoGB";
            this.taskInfoGB.Size = new System.Drawing.Size(299, 84);
            this.taskInfoGB.TabIndex = 45;
            this.taskInfoGB.TabStop = false;
            this.taskInfoGB.Text = "任务ID：";
            this.taskInfoGB.Visible = false;
            // 
            // taskStatusLabel
            // 
            this.taskStatusLabel.AutoSize = true;
            this.taskStatusLabel.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.taskStatusLabel.ForeColor = System.Drawing.Color.DimGray;
            this.taskStatusLabel.Location = new System.Drawing.Point(90, 28);
            this.taskStatusLabel.Name = "taskStatusLabel";
            this.taskStatusLabel.Size = new System.Drawing.Size(96, 36);
            this.taskStatusLabel.TabIndex = 0;
            this.taskStatusLabel.Text = "运行中";
            // 
            // SearchToolkitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 384);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchToolkitForm";
            this.Text = "全文工具箱";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelCenter.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.taskInfoGB.ResumeLayout(false);
            this.taskInfoGB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox searchAgentIPTB;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox bastionIPTB;
        private System.Windows.Forms.TextBox passwordTB;
        private System.Windows.Forms.TextBox usernameTB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.TextBox remoteWorkspaceTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox taskModelComboBox;
        private System.Windows.Forms.TextBox taskNameTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox taskInfoGB;
        private System.Windows.Forms.Label taskStatusLabel;
    }
}