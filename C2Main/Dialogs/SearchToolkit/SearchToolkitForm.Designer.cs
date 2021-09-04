﻿namespace C2.SearchToolkit
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
            this.components = new System.ComponentModel.Container();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.interfaceIPTB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.connectTestButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
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
            this.TaskConfigPB = new System.Windows.Forms.PictureBox();
            this.taskInfoGB = new System.Windows.Forms.GroupBox();
            this.taskStatusLabel = new System.Windows.Forms.Label();
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
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TaskConfigPB)).BeginInit();
            this.taskInfoGB.SuspendLayout();
            this.panelCenter.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.interfaceIPTB);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.connectTestButton);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label3);
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
            this.groupBox2.Size = new System.Drawing.Size(613, 165);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "堡垒机配置";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.label10.Location = new System.Drawing.Point(434, 78);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(164, 19);
            this.label10.TabIndex = 50;
            this.label10.Text = "选填,需从界面机2次跳转时";
            // 
            // interfaceIPTB
            // 
            this.interfaceIPTB.Location = new System.Drawing.Point(296, 75);
            this.interfaceIPTB.Name = "interfaceIPTB";
            this.interfaceIPTB.Size = new System.Drawing.Size(136, 25);
            this.interfaceIPTB.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(235, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 19);
            this.label9.TabIndex = 48;
            this.label9.Text = "界面机IP";
            // 
            // connectTestButton
            // 
            this.connectTestButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.connectTestButton.ForeColor = System.Drawing.Color.Navy;
            this.connectTestButton.Location = new System.Drawing.Point(241, 115);
            this.connectTestButton.Name = "connectTestButton";
            this.connectTestButton.Size = new System.Drawing.Size(68, 27);
            this.connectTestButton.TabIndex = 9;
            this.connectTestButton.Text = "测试连通";
            this.connectTestButton.UseVisualStyleBackColor = true;
            this.connectTestButton.Click += new System.EventHandler(this.ConnectTestButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("宋体", 8F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(435, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 11);
            this.label8.TabIndex = 47;
            this.label8.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("宋体", 8F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(222, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 11);
            this.label7.TabIndex = 46;
            this.label7.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("宋体", 8F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(222, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 11);
            this.label6.TabIndex = 45;
            this.label6.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 8F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(222, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 11);
            this.label3.TabIndex = 44;
            this.label3.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.label2.Location = new System.Drawing.Point(472, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 19);
            this.label2.TabIndex = 43;
            this.label2.Text = "堡垒机用户名和密码";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 120);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 19);
            this.label14.TabIndex = 7;
            this.label14.Text = "全文主节点";
            // 
            // searchAgentIPTB
            // 
            this.searchAgentIPTB.Location = new System.Drawing.Point(81, 116);
            this.searchAgentIPTB.Name = "searchAgentIPTB";
            this.searchAgentIPTB.Size = new System.Drawing.Size(137, 25);
            this.searchAgentIPTB.TabIndex = 8;
            this.searchAgentIPTB.Text = "15.1.1.1:22";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 77);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 19);
            this.label15.TabIndex = 5;
            this.label15.Text = "堡垒机IP";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(235, 36);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 19);
            this.label16.TabIndex = 4;
            this.label16.Text = "堡垒密码";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(5, 36);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(74, 19);
            this.label17.TabIndex = 3;
            this.label17.Text = "堡垒用户名";
            // 
            // bastionIPTB
            // 
            this.bastionIPTB.Location = new System.Drawing.Point(81, 75);
            this.bastionIPTB.Name = "bastionIPTB";
            this.bastionIPTB.Size = new System.Drawing.Size(137, 25);
            this.bastionIPTB.TabIndex = 6;
            this.bastionIPTB.Text = "15.0.0.1:22";
            // 
            // passwordTB
            // 
            this.passwordTB.Location = new System.Drawing.Point(296, 34);
            this.passwordTB.Name = "passwordTB";
            this.passwordTB.PasswordChar = '*';
            this.passwordTB.Size = new System.Drawing.Size(136, 25);
            this.passwordTB.TabIndex = 5;
            // 
            // usernameTB
            // 
            this.usernameTB.Location = new System.Drawing.Point(81, 34);
            this.usernameTB.Name = "usernameTB";
            this.usernameTB.Size = new System.Drawing.Size(137, 25);
            this.usernameTB.TabIndex = 4;
            this.usernameTB.Text = "X1587";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Window;
            this.groupBox1.Controls.Add(this.TaskConfigPB);
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
            this.groupBox1.Size = new System.Drawing.Size(613, 169);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模型配置";
            // 
            // TaskConfigPB
            // 
            this.TaskConfigPB.BackColor = System.Drawing.Color.Transparent;
            this.TaskConfigPB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TaskConfigPB.Cursor = System.Windows.Forms.Cursors.Default;
            this.TaskConfigPB.Image = global::C2.Properties.Resources.designer;
            this.TaskConfigPB.InitialImage = global::C2.Properties.Resources.designer;
            this.TaskConfigPB.Location = new System.Drawing.Point(294, 36);
            this.TaskConfigPB.Margin = new System.Windows.Forms.Padding(0);
            this.TaskConfigPB.Name = "TaskConfigPB";
            this.TaskConfigPB.Size = new System.Drawing.Size(24, 24);
            this.TaskConfigPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.TaskConfigPB.TabIndex = 46;
            this.TaskConfigPB.TabStop = false;
            this.toolTip1.SetToolTip(this.TaskConfigPB, "自定义模型参数");
            this.TaskConfigPB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TaskConfigPB_MouseClick);
            this.TaskConfigPB.MouseEnter += new System.EventHandler(this.TaskConfigPB_MouseEnter);
            this.TaskConfigPB.MouseLeave += new System.EventHandler(this.TaskConfigPB_MouseLeave);
            // 
            // taskInfoGB
            // 
            this.taskInfoGB.Controls.Add(this.taskStatusLabel);
            this.taskInfoGB.Location = new System.Drawing.Point(321, 24);
            this.taskInfoGB.Name = "taskInfoGB";
            this.taskInfoGB.Size = new System.Drawing.Size(282, 84);
            this.taskInfoGB.TabIndex = 45;
            this.taskInfoGB.TabStop = false;
            this.taskInfoGB.Text = "任务状态";
            this.taskInfoGB.Visible = false;
            // 
            // taskStatusLabel
            // 
            this.taskStatusLabel.AutoSize = true;
            this.taskStatusLabel.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.taskStatusLabel.ForeColor = System.Drawing.Color.DimGray;
            this.taskStatusLabel.Location = new System.Drawing.Point(70, 28);
            this.taskStatusLabel.Name = "taskStatusLabel";
            this.taskStatusLabel.Size = new System.Drawing.Size(0, 36);
            this.taskStatusLabel.TabIndex = 0;
            this.taskStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // taskNameTB
            // 
            this.taskNameTB.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.taskNameTB.Location = new System.Drawing.Point(81, 36);
            this.taskNameTB.Name = "taskNameTB";
            this.taskNameTB.Size = new System.Drawing.Size(211, 25);
            this.taskNameTB.TabIndex = 0;
            this.taskNameTB.Text = "涉赌模型";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 43;
            this.label5.Text = "任务名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 19);
            this.label1.TabIndex = 42;
            this.label1.Text = "模型选择";
            // 
            // taskModelComboBox
            // 
            this.taskModelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.taskModelComboBox.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.taskModelComboBox.FormattingEnabled = true;
            this.taskModelComboBox.Items.AddRange(new object[] {
            "涉赌模型",
            "涉枪模型",
            "涉黄模型",
            "飞机场模型",
            "黑客模型",
            "宝塔面板",
            "应用分发",
            "ddos模型",
            "xss模型",
            "侵公模型",
            "四方模型",
            "秒播vps",
            "md5逆向",
            "自定义查询"});
            this.taskModelComboBox.Location = new System.Drawing.Point(81, 81);
            this.taskModelComboBox.Name = "taskModelComboBox";
            this.taskModelComboBox.Size = new System.Drawing.Size(211, 27);
            this.taskModelComboBox.TabIndex = 1;
            this.taskModelComboBox.SelectedIndexChanged += new System.EventHandler(this.ModelComboBox_SelectedIndexChanged);
            // 
            // downloadButton
            // 
            this.downloadButton.Enabled = false;
            this.downloadButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.downloadButton.ForeColor = System.Drawing.Color.DarkRed;
            this.downloadButton.Location = new System.Drawing.Point(535, 126);
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
            this.remoteWorkspaceTB.Location = new System.Drawing.Point(81, 128);
            this.remoteWorkspaceTB.Name = "remoteWorkspaceTB";
            this.remoteWorkspaceTB.ReadOnly = true;
            this.remoteWorkspaceTB.Size = new System.Drawing.Size(448, 25);
            this.remoteWorkspaceTB.TabIndex = 2;
            this.remoteWorkspaceTB.Text = "/tmp/iao/search_toolkit/gamble";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 19);
            this.label4.TabIndex = 35;
            this.label4.Text = "远程目录";
            this.toolTip1.SetToolTip(this.label4, "双击复制远程目录到剪切板");
            this.label4.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Label4_MouseDoubleClick);
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.groupBox2);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelCenter.Location = new System.Drawing.Point(0, 169);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(613, 165);
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
            this.panelBottom.Size = new System.Drawing.Size(613, 50);
            this.panelBottom.TabIndex = 4;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.cancelButton.Location = new System.Drawing.Point(533, 12);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(60, 27);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // confirmButton
            // 
            this.confirmButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.confirmButton.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.confirmButton.Location = new System.Drawing.Point(417, 12);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(60, 27);
            this.confirmButton.TabIndex = 10;
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
            this.panelTop.Size = new System.Drawing.Size(613, 169);
            this.panelTop.TabIndex = 2;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "全文模型结果";
            this.saveFileDialog.Filter = "tgz压缩包 (*.tgz)|*.tgz|tar.gz压缩包|*.tar.gz|zip压缩包|*.zip|所有文件|*.*";
            this.saveFileDialog.Title = "全文模型结果下载";
            // 
            // SearchToolkitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 384);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::C2.Properties.Resources.logo;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchToolkitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "全文工具箱";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TaskConfigPB)).EndInit();
            this.taskInfoGB.ResumeLayout(false);
            this.taskInfoGB.PerformLayout();
            this.panelCenter.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button connectTestButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox interfaceIPTB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox TaskConfigPB;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}