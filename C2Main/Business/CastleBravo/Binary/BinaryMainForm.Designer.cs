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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button7 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.StringsTabPage.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(715, 431);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Xise流量解密";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(386, 57);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(42, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "例2";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(386, 35);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(42, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "例1";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(643, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "清空";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(548, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "解密";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
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
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox1.Location = new System.Drawing.Point(3, 81);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(709, 347);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.button7);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.button6);
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(715, 431);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "冰蝎流量解密";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(228, 6);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(42, 23);
            this.button7.TabIndex = 16;
            this.button7.Text = "例1";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.label14.Location = new System.Drawing.Point(274, 225);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(131, 25);
            this.label14.TabIndex = 15;
            this.label14.Text = "施工中.。。。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "进度";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(643, 52);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(60, 23);
            this.button5.TabIndex = 13;
            this.button5.Text = "清空";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(548, 52);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(60, 23);
            this.button6.TabIndex = 12;
            this.button6.Text = "解密";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(38, 52);
            this.progressBar1.Maximum = 1000;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(232, 23);
            this.progressBar1.TabIndex = 11;
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox2.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox2.Location = new System.Drawing.Point(3, 81);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(709, 347);
            this.textBox2.TabIndex = 7;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(215, 17);
            this.label10.TabIndex = 6;
            this.label10.Text = "破解三代冰蝎的加密流量报文(AES128)";
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
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(342, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 17);
            this.label15.TabIndex = 17;
            this.label15.Text = "字典数据:";
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
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(408, 6);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(50, 17);
            this.label17.TabIndex = 19;
            this.label17.Text = "label17";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(408, 30);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 17);
            this.label18.TabIndex = 20;
            this.label18.Text = "label18";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(342, 54);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 17);
            this.label19.TabIndex = 21;
            this.label19.Text = "命中密码:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(408, 54);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(50, 17);
            this.label20.TabIndex = 22;
            this.label20.Text = "label20";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(276, 55);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(50, 17);
            this.label21.TabIndex = 23;
            this.label21.Text = "label21";
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
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage StringsTabPage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
    }
}