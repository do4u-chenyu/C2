using C2.Utils;

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
            this.BehinderDTabPage = new System.Windows.Forms.TabPage();
            this.SuccessLabel = new System.Windows.Forms.Label();
            this.HitPasswordLabel = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.DictCountLabel = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.BehinderDS1Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.BehinderDClearButton = new System.Windows.Forms.Button();
            this.BehinderDecryptButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.BehinderDTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.BehinderETabPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton40 = new System.Windows.Forms.RadioButton();
            this.rb20 = new System.Windows.Forms.RadioButton();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.BehinderEClearButton = new System.Windows.Forms.Button();
            this.BehinderDGenButton = new System.Windows.Forms.Button();
            this.BehinderES1Button = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.BehinderETextBox = new System.Windows.Forms.TextBox();
            this.BaiduLBSTabPage = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label20 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.StringsTabPage.SuspendLayout();
            this.XiseTabPage.SuspendLayout();
            this.BehinderDTabPage.SuspendLayout();
            this.BehinderETabPage.SuspendLayout();
            this.BaiduLBSTabPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.StringsTabPage);
            this.tabControl1.Controls.Add(this.XiseTabPage);
            this.tabControl1.Controls.Add(this.BehinderDTabPage);
            this.tabControl1.Controls.Add(this.BehinderETabPage);
            this.tabControl1.Controls.Add(this.BaiduLBSTabPage);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1084, 692);
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
            this.StringsTabPage.Location = new System.Drawing.Point(4, 33);
            this.StringsTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.StringsTabPage.Name = "StringsTabPage";
            this.StringsTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.StringsTabPage.Size = new System.Drawing.Size(1076, 655);
            this.StringsTabPage.TabIndex = 0;
            this.StringsTabPage.Text = "二进制分析";
            this.StringsTabPage.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this.label6.Location = new System.Drawing.Point(6, 90);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 23);
            this.label6.TabIndex = 7;
            this.label6.Text = "0 条";
            // 
            // FileButton
            // 
            this.FileButton.Location = new System.Drawing.Point(966, 48);
            this.FileButton.Margin = new System.Windows.Forms.Padding(4);
            this.FileButton.Name = "FileButton";
            this.FileButton.Size = new System.Drawing.Size(94, 34);
            this.FileButton.TabIndex = 6;
            this.FileButton.Text = "+打开";
            this.FileButton.UseVisualStyleBackColor = true;
            this.FileButton.Click += new System.EventHandler(this.FileButton_Click);
            // 
            // FileTB
            // 
            this.FileTB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FileTB.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.FileTB.Location = new System.Drawing.Point(6, 48);
            this.FileTB.Margin = new System.Windows.Forms.Padding(4);
            this.FileTB.Name = "FileTB";
            this.FileTB.ReadOnly = true;
            this.FileTB.Size = new System.Drawing.Size(934, 31);
            this.FileTB.TabIndex = 5;
            // 
            // ResultTB
            // 
            this.ResultTB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ResultTB.Location = new System.Drawing.Point(4, 133);
            this.ResultTB.Margin = new System.Windows.Forms.Padding(4);
            this.ResultTB.MaxLength = 16777216;
            this.ResultTB.Multiline = true;
            this.ResultTB.Name = "ResultTB";
            this.ResultTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultTB.Size = new System.Drawing.Size(1068, 518);
            this.ResultTB.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(669, 10);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(307, 24);
            this.label5.TabIndex = 2;
            this.label5.Text = "支持单字节, 大端双字节和小端双字节\r\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(523, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "从二进制(exe,dll,.so,.lib,.a, ...)中提取文本,IP,域名,手机号,用户名...";
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
            this.XiseTabPage.Location = new System.Drawing.Point(4, 33);
            this.XiseTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.XiseTabPage.Name = "XiseTabPage";
            this.XiseTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.XiseTabPage.Size = new System.Drawing.Size(1076, 655);
            this.XiseTabPage.TabIndex = 1;
            this.XiseTabPage.Text = "Xise流量解密";
            this.XiseTabPage.UseVisualStyleBackColor = true;
            // 
            // XiseS2Button
            // 
            this.XiseS2Button.Location = new System.Drawing.Point(579, 86);
            this.XiseS2Button.Margin = new System.Windows.Forms.Padding(4);
            this.XiseS2Button.Name = "XiseS2Button";
            this.XiseS2Button.Size = new System.Drawing.Size(63, 34);
            this.XiseS2Button.TabIndex = 14;
            this.XiseS2Button.Text = "例2";
            this.XiseS2Button.UseVisualStyleBackColor = true;
            this.XiseS2Button.Click += new System.EventHandler(this.XiseS2Button_Click);
            // 
            // XiseS1Button
            // 
            this.XiseS1Button.Location = new System.Drawing.Point(579, 52);
            this.XiseS1Button.Margin = new System.Windows.Forms.Padding(4);
            this.XiseS1Button.Name = "XiseS1Button";
            this.XiseS1Button.Size = new System.Drawing.Size(63, 34);
            this.XiseS1Button.TabIndex = 13;
            this.XiseS1Button.Text = "例1";
            this.XiseS1Button.UseVisualStyleBackColor = true;
            this.XiseS1Button.Click += new System.EventHandler(this.XiseS1Button_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(36, 62);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(528, 24);
            this.label13.TabIndex = 12;
            this.label13.Text = "A)  122?...?214?~226?...?181?, 为内部格式, 前后两部分, \'~\'分割";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(36, 92);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(479, 24);
            this.label12.TabIndex = 11;
            this.label12.Text = "B)  3132323F...33363F, 为报文格式, 就是B格式的Hex编码";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 33);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(265, 24);
            this.label11.TabIndex = 10;
            this.label11.Text = "Xise后门加密字符串有两种形式:";
            // 
            // XiseClearButton
            // 
            this.XiseClearButton.Location = new System.Drawing.Point(964, 76);
            this.XiseClearButton.Margin = new System.Windows.Forms.Padding(4);
            this.XiseClearButton.Name = "XiseClearButton";
            this.XiseClearButton.Size = new System.Drawing.Size(90, 34);
            this.XiseClearButton.TabIndex = 9;
            this.XiseClearButton.Text = "清空";
            this.XiseClearButton.UseVisualStyleBackColor = true;
            this.XiseClearButton.Click += new System.EventHandler(this.XiseClearButton_Click);
            // 
            // XiseDecryptButton
            // 
            this.XiseDecryptButton.Location = new System.Drawing.Point(822, 76);
            this.XiseDecryptButton.Margin = new System.Windows.Forms.Padding(4);
            this.XiseDecryptButton.Name = "XiseDecryptButton";
            this.XiseDecryptButton.Size = new System.Drawing.Size(90, 34);
            this.XiseDecryptButton.TabIndex = 8;
            this.XiseDecryptButton.Text = "解密";
            this.XiseDecryptButton.UseVisualStyleBackColor = true;
            this.XiseDecryptButton.Click += new System.EventHandler(this.XiseDecryptButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(360, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(567, 24);
            this.label7.TabIndex = 7;
            this.label7.Text = "熊猫SEO版, 19.9河东版, V8.86, V23.8, V24, V26, V30.0均有加密后门";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 6);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(319, 24);
            this.label9.TabIndex = 5;
            this.label9.Text = "破解Xise后门加密流量,配套黑吃黑战法";
            // 
            // XiseTextBox
            // 
            this.XiseTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.XiseTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.XiseTextBox.Location = new System.Drawing.Point(4, 133);
            this.XiseTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.XiseTextBox.Multiline = true;
            this.XiseTextBox.Name = "XiseTextBox";
            this.XiseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.XiseTextBox.Size = new System.Drawing.Size(1068, 518);
            this.XiseTextBox.TabIndex = 6;
            this.XiseTextBox.Text = resources.GetString("XiseTextBox.Text");
            // 
            // BehinderDTabPage
            // 
            this.BehinderDTabPage.Controls.Add(this.SuccessLabel);
            this.BehinderDTabPage.Controls.Add(this.HitPasswordLabel);
            this.BehinderDTabPage.Controls.Add(this.label19);
            this.BehinderDTabPage.Controls.Add(this.DictCountLabel);
            this.BehinderDTabPage.Controls.Add(this.label15);
            this.BehinderDTabPage.Controls.Add(this.BehinderDS1Button);
            this.BehinderDTabPage.Controls.Add(this.label2);
            this.BehinderDTabPage.Controls.Add(this.BehinderDClearButton);
            this.BehinderDTabPage.Controls.Add(this.BehinderDecryptButton);
            this.BehinderDTabPage.Controls.Add(this.progressBar);
            this.BehinderDTabPage.Controls.Add(this.BehinderDTextBox);
            this.BehinderDTabPage.Controls.Add(this.label10);
            this.BehinderDTabPage.Location = new System.Drawing.Point(4, 33);
            this.BehinderDTabPage.Margin = new System.Windows.Forms.Padding(4);
            this.BehinderDTabPage.Name = "BehinderDTabPage";
            this.BehinderDTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.BehinderDTabPage.Size = new System.Drawing.Size(1076, 655);
            this.BehinderDTabPage.TabIndex = 2;
            this.BehinderDTabPage.Text = "冰蝎流量解密";
            this.BehinderDTabPage.UseVisualStyleBackColor = true;
            // 
            // SuccessLabel
            // 
            this.SuccessLabel.AutoSize = true;
            this.SuccessLabel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.SuccessLabel.Location = new System.Drawing.Point(428, 81);
            this.SuccessLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SuccessLabel.Name = "SuccessLabel";
            this.SuccessLabel.Size = new System.Drawing.Size(0, 24);
            this.SuccessLabel.TabIndex = 23;
            // 
            // HitPasswordLabel
            // 
            this.HitPasswordLabel.AutoSize = true;
            this.HitPasswordLabel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.HitPasswordLabel.Location = new System.Drawing.Point(636, 81);
            this.HitPasswordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.HitPasswordLabel.Name = "HitPasswordLabel";
            this.HitPasswordLabel.Size = new System.Drawing.Size(73, 24);
            this.HitPasswordLabel.TabIndex = 22;
            this.HitPasswordLabel.Text = "HitPass";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(537, 81);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(86, 24);
            this.label19.TabIndex = 21;
            this.label19.Text = "命中密码:";
            // 
            // DictCountLabel
            // 
            this.DictCountLabel.AutoSize = true;
            this.DictCountLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DictCountLabel.Location = new System.Drawing.Point(636, 39);
            this.DictCountLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DictCountLabel.Name = "DictCountLabel";
            this.DictCountLabel.Size = new System.Drawing.Size(102, 25);
            this.DictCountLabel.TabIndex = 19;
            this.DictCountLabel.Text = "DictCount";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(537, 39);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(86, 24);
            this.label15.TabIndex = 17;
            this.label15.Text = "字典数据:";
            // 
            // BehinderDS1Button
            // 
            this.BehinderDS1Button.Location = new System.Drawing.Point(432, 9);
            this.BehinderDS1Button.Margin = new System.Windows.Forms.Padding(4);
            this.BehinderDS1Button.Name = "BehinderDS1Button";
            this.BehinderDS1Button.Size = new System.Drawing.Size(63, 34);
            this.BehinderDS1Button.TabIndex = 16;
            this.BehinderDS1Button.Text = "例1";
            this.BehinderDS1Button.UseVisualStyleBackColor = true;
            this.BehinderDS1Button.Click += new System.EventHandler(this.BehinderS1Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 82);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 24);
            this.label2.TabIndex = 14;
            this.label2.Text = "进度";
            // 
            // BehinderDClearButton
            // 
            this.BehinderDClearButton.Location = new System.Drawing.Point(964, 76);
            this.BehinderDClearButton.Margin = new System.Windows.Forms.Padding(4);
            this.BehinderDClearButton.Name = "BehinderDClearButton";
            this.BehinderDClearButton.Size = new System.Drawing.Size(90, 34);
            this.BehinderDClearButton.TabIndex = 13;
            this.BehinderDClearButton.Text = "清空";
            this.BehinderDClearButton.UseVisualStyleBackColor = true;
            this.BehinderDClearButton.Click += new System.EventHandler(this.BehinderClearButton_Click);
            // 
            // BehinderDecryptButton
            // 
            this.BehinderDecryptButton.Location = new System.Drawing.Point(822, 76);
            this.BehinderDecryptButton.Margin = new System.Windows.Forms.Padding(4);
            this.BehinderDecryptButton.Name = "BehinderDecryptButton";
            this.BehinderDecryptButton.Size = new System.Drawing.Size(90, 34);
            this.BehinderDecryptButton.TabIndex = 12;
            this.BehinderDecryptButton.Text = "解密";
            this.BehinderDecryptButton.UseVisualStyleBackColor = true;
            this.BehinderDecryptButton.Click += new System.EventHandler(this.BehinderDecryptButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(57, 78);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.Maximum = 1000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(360, 34);
            this.progressBar.TabIndex = 11;
            // 
            // BehinderDTextBox
            // 
            this.BehinderDTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BehinderDTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.BehinderDTextBox.Location = new System.Drawing.Point(4, 133);
            this.BehinderDTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.BehinderDTextBox.Multiline = true;
            this.BehinderDTextBox.Name = "BehinderDTextBox";
            this.BehinderDTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.BehinderDTextBox.Size = new System.Drawing.Size(1068, 518);
            this.BehinderDTextBox.TabIndex = 7;
            this.BehinderDTextBox.Text = "\r\n";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 14);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(411, 24);
            this.label10.TabIndex = 6;
            this.label10.Text = "破解三代冰蝎(Behinder)的加密流量报文(AES128)";
            // 
            // BehinderETabPage
            // 
            this.BehinderETabPage.Controls.Add(this.label1);
            this.BehinderETabPage.Controls.Add(this.radioButton40);
            this.BehinderETabPage.Controls.Add(this.rb20);
            this.BehinderETabPage.Controls.Add(this.label18);
            this.BehinderETabPage.Controls.Add(this.label17);
            this.BehinderETabPage.Controls.Add(this.label16);
            this.BehinderETabPage.Controls.Add(this.BehinderEClearButton);
            this.BehinderETabPage.Controls.Add(this.BehinderDGenButton);
            this.BehinderETabPage.Controls.Add(this.BehinderES1Button);
            this.BehinderETabPage.Controls.Add(this.label14);
            this.BehinderETabPage.Controls.Add(this.BehinderETextBox);
            this.BehinderETabPage.Location = new System.Drawing.Point(4, 33);
            this.BehinderETabPage.Margin = new System.Windows.Forms.Padding(4);
            this.BehinderETabPage.Name = "BehinderETabPage";
            this.BehinderETabPage.Padding = new System.Windows.Forms.Padding(4);
            this.BehinderETabPage.Size = new System.Drawing.Size(1076, 655);
            this.BehinderETabPage.TabIndex = 3;
            this.BehinderETabPage.Text = "冰蝎流量加密";
            this.BehinderETabPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(598, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 24);
            this.label1.TabIndex = 25;
            this.label1.Text = "(会对应4个特征串)";
            // 
            // radioButton40
            // 
            this.radioButton40.AutoSize = true;
            this.radioButton40.Location = new System.Drawing.Point(528, 48);
            this.radioButton40.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton40.Name = "radioButton40";
            this.radioButton40.Size = new System.Drawing.Size(75, 28);
            this.radioButton40.TabIndex = 24;
            this.radioButton40.Text = "40位";
            this.radioButton40.UseVisualStyleBackColor = true;
            // 
            // rb20
            // 
            this.rb20.AutoSize = true;
            this.rb20.Checked = true;
            this.rb20.Location = new System.Drawing.Point(432, 48);
            this.rb20.Margin = new System.Windows.Forms.Padding(4);
            this.rb20.Name = "rb20";
            this.rb20.Size = new System.Drawing.Size(75, 28);
            this.rb20.TabIndex = 23;
            this.rb20.TabStop = true;
            this.rb20.Text = "20位";
            this.rb20.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 81);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(205, 24);
            this.label18.TabIndex = 22;
            this.label18.Text = "输出: 加密特征串 \\t 密码";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(330, 50);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 24);
            this.label17.TabIndex = 21;
            this.label17.Text = "一行一个";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 50);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(350, 24);
            this.label16.TabIndex = 20;
            this.label16.Text = "根据密码生成20/40位冰蝎加密特征字符串";
            // 
            // BehinderEClearButton
            // 
            this.BehinderEClearButton.Location = new System.Drawing.Point(964, 76);
            this.BehinderEClearButton.Margin = new System.Windows.Forms.Padding(4);
            this.BehinderEClearButton.Name = "BehinderEClearButton";
            this.BehinderEClearButton.Size = new System.Drawing.Size(90, 34);
            this.BehinderEClearButton.TabIndex = 19;
            this.BehinderEClearButton.Text = "清空";
            this.BehinderEClearButton.UseVisualStyleBackColor = true;
            this.BehinderEClearButton.Click += new System.EventHandler(this.BehinderEClearButton_Click);
            // 
            // BehinderDGenButton
            // 
            this.BehinderDGenButton.Location = new System.Drawing.Point(822, 76);
            this.BehinderDGenButton.Margin = new System.Windows.Forms.Padding(4);
            this.BehinderDGenButton.Name = "BehinderDGenButton";
            this.BehinderDGenButton.Size = new System.Drawing.Size(90, 34);
            this.BehinderDGenButton.TabIndex = 18;
            this.BehinderDGenButton.Text = "生成";
            this.BehinderDGenButton.UseVisualStyleBackColor = true;
            this.BehinderDGenButton.Click += new System.EventHandler(this.BehinderDGenButton_Click);
            // 
            // BehinderES1Button
            // 
            this.BehinderES1Button.Location = new System.Drawing.Point(432, 9);
            this.BehinderES1Button.Margin = new System.Windows.Forms.Padding(4);
            this.BehinderES1Button.Name = "BehinderES1Button";
            this.BehinderES1Button.Size = new System.Drawing.Size(63, 34);
            this.BehinderES1Button.TabIndex = 17;
            this.BehinderES1Button.Text = "例1";
            this.BehinderES1Button.UseVisualStyleBackColor = true;
            this.BehinderES1Button.Click += new System.EventHandler(this.BehinderES1Button_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 14);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(411, 24);
            this.label14.TabIndex = 9;
            this.label14.Text = "破解三代冰蝎(Behinder)的加密流量报文(AES128)";
            // 
            // BehinderETextBox
            // 
            this.BehinderETextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BehinderETextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.BehinderETextBox.Location = new System.Drawing.Point(4, 133);
            this.BehinderETextBox.Margin = new System.Windows.Forms.Padding(4);
            this.BehinderETextBox.MaxLength = 131072;
            this.BehinderETextBox.Multiline = true;
            this.BehinderETextBox.Name = "BehinderETextBox";
            this.BehinderETextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.BehinderETextBox.Size = new System.Drawing.Size(1068, 518);
            this.BehinderETextBox.TabIndex = 8;
            this.BehinderETextBox.Text = "rebeyond\r\n123456\r\nadmin\r\nhack";
            this.BehinderETextBox.WordWrap = false;
            // 
            // BaiduLBSTabPage
            // 
            this.BaiduLBSTabPage.Controls.Add(this.label8);
            this.BaiduLBSTabPage.Controls.Add(this.textBox1);
            this.BaiduLBSTabPage.Controls.Add(this.button1);
            this.BaiduLBSTabPage.Controls.Add(this.button2);
            this.BaiduLBSTabPage.Controls.Add(this.button3);
            this.BaiduLBSTabPage.Controls.Add(this.label3);
            this.BaiduLBSTabPage.Location = new System.Drawing.Point(4, 33);
            this.BaiduLBSTabPage.Name = "BaiduLBSTabPage";
            this.BaiduLBSTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.BaiduLBSTabPage.Size = new System.Drawing.Size(1076, 655);
            this.BaiduLBSTabPage.TabIndex = 4;
            this.BaiduLBSTabPage.Text = "百度LBS报文解密";
            this.BaiduLBSTabPage.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(524, 346);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 24);
            this.label8.TabIndex = 25;
            this.label8.Text = "施工中";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox1.Location = new System.Drawing.Point(3, 134);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.MaxLength = 131072;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(1070, 518);
            this.textBox1.TabIndex = 24;
            this.textBox1.WordWrap = false;
            this.textBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(964, 76);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 34);
            this.button1.TabIndex = 23;
            this.button1.Text = "清空";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(822, 76);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 34);
            this.button2.TabIndex = 22;
            this.button2.Text = "解密";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(432, 9);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(63, 34);
            this.button3.TabIndex = 21;
            this.button3.Text = "例1";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(310, 24);
            this.label3.TabIndex = 20;
            this.label3.Text = "破解百度LBS定位服务的加密流量报文";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1076, 655);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "高德LBS报文解密";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(498, 237);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(64, 24);
            this.label20.TabIndex = 0;
            this.label20.Text = "施工中";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "二进制文件|*.exe;*.so;*.dll;*.lib;*.a;*.dat;*.bin;*.*";
            // 
            // BinaryMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 692);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
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
            this.BehinderDTabPage.ResumeLayout(false);
            this.BehinderDTabPage.PerformLayout();
            this.BehinderETabPage.ResumeLayout(false);
            this.BehinderETabPage.PerformLayout();
            this.BaiduLBSTabPage.ResumeLayout(false);
            this.BaiduLBSTabPage.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage StringsTabPage;
        private System.Windows.Forms.TabPage XiseTabPage;
        private System.Windows.Forms.TabPage BehinderDTabPage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
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
        private System.Windows.Forms.TextBox BehinderDTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BehinderDClearButton;
        private System.Windows.Forms.Button BehinderDecryptButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button BehinderDS1Button;
        private System.Windows.Forms.Label DictCountLabel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label HitPasswordLabel;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label SuccessLabel;
        private System.Windows.Forms.TabPage BehinderETabPage;
        private System.Windows.Forms.Button BehinderEClearButton;
        private System.Windows.Forms.Button BehinderDGenButton;
        private System.Windows.Forms.Button BehinderES1Button;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox BehinderETextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.RadioButton radioButton40;
        private System.Windows.Forms.RadioButton rb20;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage BaiduLBSTabPage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label20;
    }
}