﻿namespace C2.Dialogs.IAOLab
{
    partial class CoordinateConversion
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.inputAndResult = new System.Windows.Forms.RichTextBox();
            this.tip0 = new System.Windows.Forms.Label();
            this.sixTransform = new System.Windows.Forms.Panel();
            this.wgs_gcj = new System.Windows.Forms.RadioButton();
            this.bd_gcj = new System.Windows.Forms.RadioButton();
            this.wgs_bd = new System.Windows.Forms.RadioButton();
            this.gcj_wgs = new System.Windows.Forms.RadioButton();
            this.gcj_bd = new System.Windows.Forms.RadioButton();
            this.bd_wgs = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tip1 = new System.Windows.Forms.Label();
            this.inputAndResult1 = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.search = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.sixTransform.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(625, 350);
            this.tabControl.TabIndex = 10;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.inputAndResult);
            this.tabPage1.Controls.Add(this.tip0);
            this.tabPage1.Controls.Add(this.sixTransform);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(617, 322);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "坐标系经纬度转换";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // inputAndResult
            // 
            this.inputAndResult.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.inputAndResult.Location = new System.Drawing.Point(-1, 116);
            this.inputAndResult.Margin = new System.Windows.Forms.Padding(2);
            this.inputAndResult.Name = "inputAndResult";
            this.inputAndResult.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.inputAndResult.Size = new System.Drawing.Size(618, 207);
            this.inputAndResult.TabIndex = 10;
            this.inputAndResult.Text = string.Empty;
            this.inputAndResult.WordWrap = false;
            // 
            // tip0
            // 
            this.tip0.AutoSize = true;
            this.tip0.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.tip0.Location = new System.Drawing.Point(17, 16);
            this.tip0.Name = "tip0";
            this.tip0.Size = new System.Drawing.Size(357, 28);
            this.tip0.TabIndex = 10;
            this.tip0.Text = "单次输入格式：40.1 120.2\r\n批量查询格式：多个坐标间用换行分割，最大支持1000条";
            // 
            // sixTransform
            // 
            this.sixTransform.BackColor = System.Drawing.Color.Transparent;
            this.sixTransform.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.sixTransform.Controls.Add(this.wgs_gcj);
            this.sixTransform.Controls.Add(this.bd_gcj);
            this.sixTransform.Controls.Add(this.wgs_bd);
            this.sixTransform.Controls.Add(this.gcj_wgs);
            this.sixTransform.Controls.Add(this.gcj_bd);
            this.sixTransform.Controls.Add(this.bd_wgs);
            this.sixTransform.Location = new System.Drawing.Point(7, 52);
            this.sixTransform.Margin = new System.Windows.Forms.Padding(2);
            this.sixTransform.Name = "sixTransform";
            this.sixTransform.Size = new System.Drawing.Size(603, 54);
            this.sixTransform.TabIndex = 9;
            // 
            // wgs_gcj
            // 
            this.wgs_gcj.AutoSize = true;
            this.wgs_gcj.Location = new System.Drawing.Point(389, 28);
            this.wgs_gcj.Margin = new System.Windows.Forms.Padding(2);
            this.wgs_gcj.Name = "wgs_gcj";
            this.wgs_gcj.Size = new System.Drawing.Size(193, 18);
            this.wgs_gcj.TabIndex = 5;
            this.wgs_gcj.Text = "wgs_gcj:国际转火星(高德)";
            this.wgs_gcj.UseVisualStyleBackColor = true;
            // 
            // bd_gcj
            // 
            this.bd_gcj.AutoSize = true;
            this.bd_gcj.Location = new System.Drawing.Point(192, 28);
            this.bd_gcj.Margin = new System.Windows.Forms.Padding(2);
            this.bd_gcj.Name = "bd_gcj";
            this.bd_gcj.Size = new System.Drawing.Size(186, 18);
            this.bd_gcj.TabIndex = 4;
            this.bd_gcj.Text = "bd_gcj:百度转火星(高德)";
            this.bd_gcj.UseVisualStyleBackColor = true;
            // 
            // wgs_bd
            // 
            this.wgs_bd.AutoSize = true;
            this.wgs_bd.Location = new System.Drawing.Point(13, 28);
            this.wgs_bd.Margin = new System.Windows.Forms.Padding(2);
            this.wgs_bd.Name = "wgs_bd";
            this.wgs_bd.Size = new System.Drawing.Size(144, 18);
            this.wgs_bd.TabIndex = 3;
            this.wgs_bd.Text = "wgs_bd:国际转百度";
            this.wgs_bd.UseVisualStyleBackColor = true;
            // 
            // gcj_wgs
            // 
            this.gcj_wgs.AutoSize = true;
            this.gcj_wgs.Location = new System.Drawing.Point(389, 6);
            this.gcj_wgs.Margin = new System.Windows.Forms.Padding(2);
            this.gcj_wgs.Name = "gcj_wgs";
            this.gcj_wgs.Size = new System.Drawing.Size(193, 18);
            this.gcj_wgs.TabIndex = 2;
            this.gcj_wgs.Text = "gcj_wgs:火星(高德)转国际";
            this.gcj_wgs.UseVisualStyleBackColor = true;
            // 
            // gcj_bd
            // 
            this.gcj_bd.AutoSize = true;
            this.gcj_bd.Location = new System.Drawing.Point(192, 6);
            this.gcj_bd.Margin = new System.Windows.Forms.Padding(2);
            this.gcj_bd.Name = "gcj_bd";
            this.gcj_bd.Size = new System.Drawing.Size(186, 18);
            this.gcj_bd.TabIndex = 1;
            this.gcj_bd.Text = "gcj_bd:火星(高德)转百度";
            this.gcj_bd.UseVisualStyleBackColor = true;
            // 
            // bd_wgs
            // 
            this.bd_wgs.AutoSize = true;
            this.bd_wgs.Checked = true;
            this.bd_wgs.Location = new System.Drawing.Point(13, 6);
            this.bd_wgs.Margin = new System.Windows.Forms.Padding(2);
            this.bd_wgs.Name = "bd_wgs";
            this.bd_wgs.Size = new System.Drawing.Size(144, 18);
            this.bd_wgs.TabIndex = 0;
            this.bd_wgs.TabStop = true;
            this.bd_wgs.Text = "bd_wgs:百度转国际";
            this.bd_wgs.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tip1);
            this.tabPage2.Controls.Add(this.inputAndResult1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.tabPage2.Size = new System.Drawing.Size(617, 322);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "经纬度距离计算";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tip1
            // 
            this.tip1.AutoSize = true;
            this.tip1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.tip1.Location = new System.Drawing.Point(8, 23);
            this.tip1.Name = "tip1";
            this.tip1.Size = new System.Drawing.Size(476, 28);
            this.tip1.TabIndex = 11;
            this.tip1.Text = "单次输入格式：04a1518006c2 或04-a1-51-80-06-c2 或 04:a1:51:80:06:c2\r\n批量查询格式：多个mac间用换行分割，最大" +
    "支持1000条";
            // 
            // inputAndResult1
            // 
            this.inputAndResult1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.inputAndResult1.Location = new System.Drawing.Point(-1, 67);
            this.inputAndResult1.Margin = new System.Windows.Forms.Padding(2);
            this.inputAndResult1.Name = "inputAndResult1";
            this.inputAndResult1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.inputAndResult1.Size = new System.Drawing.Size(618, 256);
            this.inputAndResult1.TabIndex = 1;
            this.inputAndResult1.Text = string.Empty;
            this.inputAndResult1.WordWrap = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.radioButton3);
            this.tabPage3.Controls.Add(this.radioButton4);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.richTextBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(617, 322);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "IP格式转换";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(214, 71);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(81, 18);
            this.radioButton3.TabIndex = 16;
            this.radioButton3.Text = "整形转IP";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Location = new System.Drawing.Point(9, 69);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(81, 18);
            this.radioButton4.TabIndex = 15;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "IP转整形";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 28);
            this.label1.TabIndex = 12;
            this.label1.Text = "单次输入格式：40.1 120.2\r\n批量查询格式：多个坐标间用换行分割，最大支持1000条";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBox1.Location = new System.Drawing.Point(-1, 116);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.richTextBox1.Size = new System.Drawing.Size(618, 207);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = string.Empty;
            this.richTextBox1.WordWrap = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.radioButton2);
            this.tabPage4.Controls.Add(this.radioButton1);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.richTextBox2);
            this.tabPage4.Location = new System.Drawing.Point(4, 24);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(617, 322);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "时间格式转换";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(214, 71);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(137, 18);
            this.radioButton2.TabIndex = 14;
            this.radioButton2.Text = "真实时间转绝对秒";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(9, 69);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(137, 18);
            this.radioButton1.TabIndex = 13;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "绝对秒转真实时间";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(357, 28);
            this.label2.TabIndex = 12;
            this.label2.Text = "单次输入格式：40.1 120.2\r\n批量查询格式：多个坐标间用换行分割，最大支持1000条";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBox2.Location = new System.Drawing.Point(-1, 116);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.richTextBox2.Size = new System.Drawing.Size(618, 207);
            this.richTextBox2.TabIndex = 11;
            this.richTextBox2.Text = string.Empty;
            this.richTextBox2.WordWrap = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.search);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 350);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(625, 44);
            this.panel3.TabIndex = 1;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(515, 10);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(56, 24);
            this.button4.TabIndex = 5;
            this.button4.Text = "取消";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(431, 10);
            this.search.Margin = new System.Windows.Forms.Padding(2);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(56, 24);
            this.search.TabIndex = 4;
            this.search.Text = "查询";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.Search_Click);
            // 
            // CoordinateConversion
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(625, 394);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CoordinateConversion";
            this.Text = "转换工具";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CoordinateFormClosed);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.sixTransform.ResumeLayout(false);
            this.sixTransform.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel sixTransform;
        private System.Windows.Forms.RadioButton wgs_gcj;
        private System.Windows.Forms.RadioButton bd_gcj;
        private System.Windows.Forms.RadioButton wgs_bd;
        private System.Windows.Forms.RadioButton gcj_wgs;
        private System.Windows.Forms.RadioButton gcj_bd;
        private System.Windows.Forms.RadioButton bd_wgs;
        private System.Windows.Forms.Label tip0;
        private System.Windows.Forms.Label tip1;
        private System.Windows.Forms.RichTextBox inputAndResult1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.RichTextBox inputAndResult;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
    }
}