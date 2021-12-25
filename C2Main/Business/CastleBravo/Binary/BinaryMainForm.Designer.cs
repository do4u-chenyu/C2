namespace C2.Business.CastleBravo.Binary
{
    partial class BinaryMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BinaryMainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.StringsTabPage = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.FileButton = new System.Windows.Forms.Button();
            this.FileTB = new System.Windows.Forms.TextBox();
            this.ResultTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.XiseTabPage = new System.Windows.Forms.TabPage();
            this.XiseS2Button = new System.Windows.Forms.Button();
            this.XiseS1Button = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.XiseClearButton = new System.Windows.Forms.Button();
            this.XiseDecryptButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.XiseTextBox = new System.Windows.Forms.TextBox();
            this.BehinderTabPage = new System.Windows.Forms.TabPage();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.HitPasswordLabel = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.IteratorCountLabel = new System.Windows.Forms.Label();
            this.DictCountLabel = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.BehinderS1Button = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BehinderClearButton = new System.Windows.Forms.Button();
            this.BehinderDecryptButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.BehinderTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.StringsTabPage.SuspendLayout();
            this.XiseTabPage.SuspendLayout();
            this.BehinderTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(269, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "施工中...";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.StringsTabPage);
            this.tabControl1.Controls.Add(this.XiseTabPage);
            this.tabControl1.Controls.Add(this.BehinderTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(723, 461);
            this.tabControl1.TabIndex = 1;
            // 
            // StringsTabPage
            // 
            this.StringsTabPage.Controls.Add(this.label6);
            this.StringsTabPage.Controls.Add(this.FileButton);
            this.StringsTabPage.Controls.Add(this.FileTB);
            this.StringsTabPage.Controls.Add(this.ResultTB);
            this.StringsTabPage.Controls.Add(this.label5);
            this.StringsTabPage.Controls.Add(this.label4);
            this.StringsTabPage.Controls.Add(this.label1);
            this.StringsTabPage.Location = new System.Drawing.Point(4, 26);
            this.StringsTabPage.Name = "StringsTabPage";
            this.StringsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.StringsTabPage.Size = new System.Drawing.Size(715, 431);
            this.StringsTabPage.TabIndex = 0;
            this.StringsTabPage.Text = "提取字符串";
            this.StringsTabPage.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.label6.Location = new System.Drawing.Point(4, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "0 条";
            // 
            // FileButton
            // 
            this.FileButton.Location = new System.Drawing.Point(644, 32);
            this.FileButton.Name = "FileButton";
            this.FileButton.Size = new System.Drawing.Size(63, 23);
            this.FileButton.TabIndex = 6;
            this.FileButton.Text = "+分析";
            this.FileButton.UseVisualStyleBackColor = true;
            this.FileButton.Click += new System.EventHandler(this.FileButton_Click);
            // 
            // FileTB
            // 
            this.FileTB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FileTB.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.FileTB.Location = new System.Drawing.Point(4, 32);
            this.FileTB.Name = "FileTB";
            this.FileTB.ReadOnly = true;
            this.FileTB.Size = new System.Drawing.Size(624, 23);
            this.FileTB.TabIndex = 5;
            // 
            // ResultTB
            // 
            this.ResultTB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ResultTB.Location = new System.Drawing.Point(3, 81);
            this.ResultTB.MaxLength = 16777216;
            this.ResultTB.Multiline = true;
            this.ResultTB.Name = "ResultTB";
            this.ResultTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultTB.Size = new System.Drawing.Size(709, 347);
            this.ResultTB.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(446, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(207, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "支持单字节, 大端双字节和小端双字节\r\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(433, 34);
            this.label4.TabIndex = 1;
            this.label4.Text = "从二进制文件(.exe,.dll,.so,.lib,.a, ...)中提取文本,尤其是IP,域名,手机号和用户名...\r\n\r\n";
            // 
            // XiseTabPage
            // 
            this.XiseTabPage.Controls.Add(this.XiseS2Button);
            this.XiseTabPage.Controls.Add(this.XiseS1Button);
            this.XiseTabPage.Controls.Add(this.label13);
            this.XiseTabPage.Controls.Add(this.label12);
            this.XiseTabPage.Controls.Add(this.label11);
            this.XiseTabPage.Controls.Add(this.XiseClearButton);
            this.XiseTabPage.Controls.Add(this.XiseDecryptButton);
            this.XiseTabPage.Controls.Add(this.label7);
            this.XiseTabPage.Controls.Add(this.label9);
            this.XiseTabPage.Controls.Add(this.XiseTextBox);
            this.XiseTabPage.Location = new System.Drawing.Point(4, 26);
            this.XiseTabPage.Name = "XiseTabPage";
            this.XiseTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.XiseTabPage.Size = new System.Drawing.Size(715, 431);
            this.XiseTabPage.TabIndex = 1;
            this.XiseTabPage.Text = "Xise流量解密";
            this.XiseTabPage.UseVisualStyleBackColor = true;
            // 
            // XiseS2Button
            // 
            this.XiseS2Button.Location = new System.Drawing.Point(386, 57);
            this.XiseS2Button.Name = "XiseS2Button";
            this.XiseS2Button.Size = new System.Drawing.Size(42, 23);
            this.XiseS2Button.TabIndex = 14;
            this.XiseS2Button.Text = "例2";
            this.XiseS2Button.UseVisualStyleBackColor = true;
            this.XiseS2Button.Click += new System.EventHandler(this.XiseS2Button_Click);
            // 
            // XiseS1Button
            // 
            this.XiseS1Button.Location = new System.Drawing.Point(386, 35);
            this.XiseS1Button.Name = "XiseS1Button";
            this.XiseS1Button.Size = new System.Drawing.Size(42, 23);
            this.XiseS1Button.TabIndex = 13;
            this.XiseS1Button.Text = "例1";
            this.XiseS1Button.UseVisualStyleBackColor = true;
            this.XiseS1Button.Click += new System.EventHandler(this.XiseS1Button_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(24, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(355, 17);
            this.label13.TabIndex = 12;
            this.label13.Text = "A)  122?...?214?~226?...?181?, 为内部格式, 前后两部分, \'~\'分割";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 61);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(321, 17);
            this.label12.TabIndex = 11;
            this.label12.Text = "B)  3132323F...33363F, 为报文格式, 就是B格式的Hex编码";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(179, 17);
            this.label11.TabIndex = 10;
            this.label11.Text = "Xise后门加密字符串有两种形式:";
            // 
            // XiseClearButton
            // 
            this.XiseClearButton.Location = new System.Drawing.Point(643, 51);
            this.XiseClearButton.Name = "XiseClearButton";
            this.XiseClearButton.Size = new System.Drawing.Size(60, 23);
            this.XiseClearButton.TabIndex = 9;
            this.XiseClearButton.Text = "清空";
            this.XiseClearButton.UseVisualStyleBackColor = true;
            this.XiseClearButton.Click += new System.EventHandler(this.XiseClearButton_Click);
            // 
            // XiseDecryptButton
            // 
            this.XiseDecryptButton.Location = new System.Drawing.Point(548, 51);
            this.XiseDecryptButton.Name = "XiseDecryptButton";
            this.XiseDecryptButton.Size = new System.Drawing.Size(60, 23);
            this.XiseDecryptButton.TabIndex = 8;
            this.XiseDecryptButton.Text = "解密";
            this.XiseDecryptButton.UseVisualStyleBackColor = true;
            this.XiseDecryptButton.Click += new System.EventHandler(this.XiseDecryptButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(240, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(382, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "熊猫SEO版, 19.9河东版, V8.86, V23.8, V24, V26, V30.0均有加密后门";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(239, 17);
            this.label9.TabIndex = 5;
            this.label9.Text = "针对Xise系列后门加密流量进行定制化解密,";
            // 
            // XiseTextBox
            // 
            this.XiseTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.XiseTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.XiseTextBox.Location = new System.Drawing.Point(3, 81);
            this.XiseTextBox.Multiline = true;
            this.XiseTextBox.Name = "XiseTextBox";
            this.XiseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.XiseTextBox.Size = new System.Drawing.Size(709, 347);
            this.XiseTextBox.TabIndex = 6;
            this.XiseTextBox.Text = resources.GetString("XiseTextBox.Text");
            // 
            // BehinderTabPage
            // 
            this.BehinderTabPage.Controls.Add(this.ProgressLabel);
            this.BehinderTabPage.Controls.Add(this.HitPasswordLabel);
            this.BehinderTabPage.Controls.Add(this.label19);
            this.BehinderTabPage.Controls.Add(this.IteratorCountLabel);
            this.BehinderTabPage.Controls.Add(this.DictCountLabel);
            this.BehinderTabPage.Controls.Add(this.label16);
            this.BehinderTabPage.Controls.Add(this.label15);
            this.BehinderTabPage.Controls.Add(this.BehinderS1Button);
            this.BehinderTabPage.Controls.Add(this.label14);
            this.BehinderTabPage.Controls.Add(this.label2);
            this.BehinderTabPage.Controls.Add(this.BehinderClearButton);
            this.BehinderTabPage.Controls.Add(this.BehinderDecryptButton);
            this.BehinderTabPage.Controls.Add(this.progressBar);
            this.BehinderTabPage.Controls.Add(this.BehinderTextBox);
            this.BehinderTabPage.Controls.Add(this.label10);
            this.BehinderTabPage.Controls.Add(this.label8);
            this.BehinderTabPage.Controls.Add(this.label3);
            this.BehinderTabPage.Location = new System.Drawing.Point(4, 26);
            this.BehinderTabPage.Name = "BehinderTabPage";
            this.BehinderTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.BehinderTabPage.Size = new System.Drawing.Size(715, 431);
            this.BehinderTabPage.TabIndex = 2;
            this.BehinderTabPage.Text = "冰蝎流量解密";
            this.BehinderTabPage.UseVisualStyleBackColor = true;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ProgressLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.ProgressLabel.Location = new System.Drawing.Point(286, 55);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(27, 17);
            this.ProgressLabel.TabIndex = 23;
            this.ProgressLabel.Text = "0/0";
            // 
            // HitPasswordLabel
            // 
            this.HitPasswordLabel.AutoSize = true;
            this.HitPasswordLabel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.HitPasswordLabel.Location = new System.Drawing.Point(408, 55);
            this.HitPasswordLabel.Name = "HitPasswordLabel";
            this.HitPasswordLabel.Size = new System.Drawing.Size(50, 17);
            this.HitPasswordLabel.TabIndex = 22;
            this.HitPasswordLabel.Text = "HitPass";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(342, 55);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 17);
            this.label19.TabIndex = 21;
            this.label19.Text = "命中密码:";
            // 
            // IteratorCountLabel
            // 
            this.IteratorCountLabel.AutoSize = true;
            this.IteratorCountLabel.Location = new System.Drawing.Point(408, 30);
            this.IteratorCountLabel.Name = "IteratorCountLabel";
            this.IteratorCountLabel.Size = new System.Drawing.Size(52, 17);
            this.IteratorCountLabel.TabIndex = 20;
            this.IteratorCountLabel.Text = "Iterator";
            // 
            // DictCountLabel
            // 
            this.DictCountLabel.AutoSize = true;
            this.DictCountLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DictCountLabel.Location = new System.Drawing.Point(408, 6);
            this.DictCountLabel.Name = "DictCountLabel";
            this.DictCountLabel.Size = new System.Drawing.Size(70, 17);
            this.DictCountLabel.TabIndex = 19;
            this.DictCountLabel.Text = "DictCount";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(342, 30);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 17);
            this.label16.TabIndex = 18;
            this.label16.Text = "迭代次数:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(342, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 17);
            this.label15.TabIndex = 17;
            this.label15.Text = "字典数据:";
            // 
            // BehinderS1Button
            // 
            this.BehinderS1Button.Location = new System.Drawing.Point(288, 6);
            this.BehinderS1Button.Name = "BehinderS1Button";
            this.BehinderS1Button.Size = new System.Drawing.Size(42, 23);
            this.BehinderS1Button.TabIndex = 16;
            this.BehinderS1Button.Text = "例1";
            this.BehinderS1Button.UseVisualStyleBackColor = true;
            this.BehinderS1Button.Click += new System.EventHandler(this.BehinderS1Button_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label14.Location = new System.Drawing.Point(552, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(131, 25);
            this.label14.TabIndex = 15;
            this.label14.Text = "施工中.。。。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "进度";
            // 
            // BehinderClearButton
            // 
            this.BehinderClearButton.Location = new System.Drawing.Point(643, 51);
            this.BehinderClearButton.Name = "BehinderClearButton";
            this.BehinderClearButton.Size = new System.Drawing.Size(60, 23);
            this.BehinderClearButton.TabIndex = 13;
            this.BehinderClearButton.Text = "清空";
            this.BehinderClearButton.UseVisualStyleBackColor = true;
            this.BehinderClearButton.Click += new System.EventHandler(this.BehinderClearButton_Click);
            // 
            // BehinderDecryptButton
            // 
            this.BehinderDecryptButton.Location = new System.Drawing.Point(548, 51);
            this.BehinderDecryptButton.Name = "BehinderDecryptButton";
            this.BehinderDecryptButton.Size = new System.Drawing.Size(60, 23);
            this.BehinderDecryptButton.TabIndex = 12;
            this.BehinderDecryptButton.Text = "解密";
            this.BehinderDecryptButton.UseVisualStyleBackColor = true;
            this.BehinderDecryptButton.Click += new System.EventHandler(this.BehinderDecryptButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(38, 52);
            this.progressBar.Maximum = 1000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(240, 23);
            this.progressBar.TabIndex = 11;
            // 
            // BehinderTextBox
            // 
            this.BehinderTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BehinderTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.BehinderTextBox.Location = new System.Drawing.Point(3, 81);
            this.BehinderTextBox.Multiline = true;
            this.BehinderTextBox.Name = "BehinderTextBox";
            this.BehinderTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.BehinderTextBox.Size = new System.Drawing.Size(709, 347);
            this.BehinderTextBox.TabIndex = 7;
            this.BehinderTextBox.Text = resources.GetString("BehinderTextBox.Text");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(275, 17);
            this.label10.TabIndex = 6;
            this.label10.Text = "破解三代冰蝎(Behinder)的加密流量报文(AES128)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 17);
            this.label8.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(255, 250);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 33);
            this.label3.TabIndex = 2;
            this.label3.Text = "施工中...";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "二进制文件|*.exe;*.so;*.dll;*.lib;*.a;*.dat;*.bin|所有文件|*.*";
            // 
            // BinaryMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 461);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BinaryMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "二进制专项";
            this.tabControl1.ResumeLayout(false);
            this.StringsTabPage.ResumeLayout(false);
            this.StringsTabPage.PerformLayout();
            this.XiseTabPage.ResumeLayout(false);
            this.XiseTabPage.PerformLayout();
            this.BehinderTabPage.ResumeLayout(false);
            this.BehinderTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage StringsTabPage;
        private System.Windows.Forms.TabPage XiseTabPage;
        private System.Windows.Forms.TabPage BehinderTabPage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ResultTB;
        private System.Windows.Forms.Button FileButton;
        private System.Windows.Forms.TextBox FileTB;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox XiseTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button XiseClearButton;
        private System.Windows.Forms.Button XiseDecryptButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button XiseS2Button;
        private System.Windows.Forms.Button XiseS1Button;
        private System.Windows.Forms.TextBox BehinderTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BehinderClearButton;
        private System.Windows.Forms.Button BehinderDecryptButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button BehinderS1Button;
        private System.Windows.Forms.Label IteratorCountLabel;
        private System.Windows.Forms.Label DictCountLabel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label HitPasswordLabel;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label ProgressLabel;
    }
}