﻿using System;

namespace FullTextGrammarAssistant
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox13 = new System.Windows.Forms.CheckBox();
            this.checkBox15 = new System.Windows.Forms.CheckBox();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.checkBox11 = new System.Windows.Forms.CheckBox();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox43 = new System.Windows.Forms.CheckBox();
            this.checkBox44 = new System.Windows.Forms.CheckBox();
            this.checkBox45 = new System.Windows.Forms.CheckBox();
            this.checkBox46 = new System.Windows.Forms.CheckBox();
            this.checkBox47 = new System.Windows.Forms.CheckBox();
            this.checkBox48 = new System.Windows.Forms.CheckBox();
            this.checkBox51 = new System.Windows.Forms.CheckBox();
            this.checkBox52 = new System.Windows.Forms.CheckBox();
            this.checkBox53 = new System.Windows.Forms.CheckBox();
            this.checkBox42 = new System.Windows.Forms.CheckBox();
            this.checkBox49 = new System.Windows.Forms.CheckBox();
            this.checkBox50 = new System.Windows.Forms.CheckBox();
            this.checkBox54 = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.previewCmdText = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(64, 180);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(51, 211);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "关键词：";
            this.toolTip1.SetToolTip(this.label2, "1)支持 AND, OR的组合查询;2)支持指定字段查询,如 HOST:www.baidu.com 就是只在Host字段中查询");
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(112, 211);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(562, 21);
            this.textBox1.TabIndex = 5;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(112, 337);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "全部类型";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click_1);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(10, 3);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 16);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "网页访问";
            this.toolTip1.SetToolTip(this.checkBox2, "HTTP_POST(1000001)");
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(105, 3);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(72, 16);
            this.checkBox3.TabIndex = 9;
            this.checkBox3.Text = "电子邮箱";
            this.toolTip1.SetToolTip(this.checkBox3, "QQ邮箱(1011007)");
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(196, 3);
            this.checkBox5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(72, 16);
            this.checkBox5.TabIndex = 11;
            this.checkBox5.Text = "认证信息";
            this.toolTip1.SetToolTip(this.checkBox5, "身份证(1020007)");
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(290, 3);
            this.checkBox6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(72, 16);
            this.checkBox6.TabIndex = 12;
            this.checkBox6.Text = "即时聊天";
            this.toolTip1.SetToolTip(this.checkBox6, "QQ(1030001)");
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(389, 3);
            this.checkBox7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(42, 16);
            this.checkBox7.TabIndex = 13;
            this.checkBox7.Text = "FTP";
            this.toolTip1.SetToolTip(this.checkBox7, "标准FTP(1050001)");
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
            // 
            // checkBox13
            // 
            this.checkBox13.AutoSize = true;
            this.checkBox13.Location = new System.Drawing.Point(467, 3);
            this.checkBox13.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox13.Name = "checkBox13";
            this.checkBox13.Size = new System.Drawing.Size(72, 16);
            this.checkBox13.TabIndex = 14;
            this.checkBox13.Text = "网络聊天";
            this.toolTip1.SetToolTip(this.checkBox13, "QQ(1060001)");
            this.checkBox13.UseVisualStyleBackColor = true;
            this.checkBox13.CheckedChanged += new System.EventHandler(this.checkBox13_CheckedChanged);
            // 
            // checkBox15
            // 
            this.checkBox15.AutoSize = true;
            this.checkBox15.Location = new System.Drawing.Point(467, 33);
            this.checkBox15.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox15.Name = "checkBox15";
            this.checkBox15.Size = new System.Drawing.Size(72, 16);
            this.checkBox15.TabIndex = 16;
            this.checkBox15.Text = "社交网站";
            this.toolTip1.SetToolTip(this.checkBox15, "腾讯(1197007)");
            this.checkBox15.UseVisualStyleBackColor = true;
            this.checkBox15.CheckedChanged += new System.EventHandler(this.checkBox15_CheckedChanged);
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.Location = new System.Drawing.Point(10, 33);
            this.checkBox12.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(72, 16);
            this.checkBox12.TabIndex = 13;
            this.checkBox12.Text = "网络论坛";
            this.toolTip1.SetToolTip(this.checkBox12, "百度贴吧(1071002)");
            this.checkBox12.UseVisualStyleBackColor = true;
            this.checkBox12.CheckedChanged += new System.EventHandler(this.checkBox12_CheckedChanged);
            // 
            // checkBox11
            // 
            this.checkBox11.AutoSize = true;
            this.checkBox11.Location = new System.Drawing.Point(105, 33);
            this.checkBox11.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox11.Name = "checkBox11";
            this.checkBox11.Size = new System.Drawing.Size(60, 16);
            this.checkBox11.TabIndex = 12;
            this.checkBox11.Text = "TELNET";
            this.toolTip1.SetToolTip(this.checkBox11, "TELNET(1080000)");
            this.checkBox11.UseVisualStyleBackColor = true;
            this.checkBox11.CheckedChanged += new System.EventHandler(this.checkBox11_CheckedChanged);
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.Location = new System.Drawing.Point(196, 33);
            this.checkBox10.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(48, 16);
            this.checkBox10.TabIndex = 11;
            this.checkBox10.Text = "VOIP";
            this.toolTip1.SetToolTip(this.checkBox10, "QQ(1090001)");
            this.checkBox10.UseVisualStyleBackColor = true;
            this.checkBox10.CheckedChanged += new System.EventHandler(this.checkBox10_CheckedChanged);
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Location = new System.Drawing.Point(290, 33);
            this.checkBox9.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(72, 16);
            this.checkBox9.TabIndex = 10;
            this.checkBox9.Text = "网络赌博";
            this.toolTip1.SetToolTip(this.checkBox9, "皇冠(1140005)");
            this.checkBox9.UseVisualStyleBackColor = true;
            this.checkBox9.CheckedChanged += new System.EventHandler(this.checkBox9_CheckedChanged);
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point(389, 33);
            this.checkBox8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(72, 16);
            this.checkBox8.TabIndex = 9;
            this.checkBox8.Text = "博客网站";
            this.toolTip1.SetToolTip(this.checkBox8, "新浪博客(11580001)");
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.CheckedChanged += new System.EventHandler(this.checkBox8_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(12, 241);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 19);
            this.label4.TabIndex = 16;
            this.label4.Text = "条件二次过滤：";
            this.toolTip1.SetToolTip(this.label4, "在关键词查出的结果集上进行二次过滤，对应全文的dbfilter功能；queryclient支持，Jar包不支持");
            // 
            // checkBox43
            // 
            this.checkBox43.AutoSize = true;
            this.checkBox43.Location = new System.Drawing.Point(426, 7);
            this.checkBox43.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox43.Name = "checkBox43";
            this.checkBox43.Size = new System.Drawing.Size(108, 16);
            this.checkBox43.TabIndex = 26;
            this.checkBox43.Text = "关键词精确匹配";
            this.toolTip1.SetToolTip(this.checkBox43, "查询关键词给套上双引号,表示不切词,不做停止字符处理,直接查关键词本身");
            this.checkBox43.UseVisualStyleBackColor = true;
            this.checkBox43.CheckedChanged += new System.EventHandler(this.checkBox43_CheckedChanged);
            // 
            // checkBox44
            // 
            this.checkBox44.AutoSize = true;
            this.checkBox44.Location = new System.Drawing.Point(290, 7);
            this.checkBox44.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox44.Name = "checkBox44";
            this.checkBox44.Size = new System.Drawing.Size(120, 16);
            this.checkBox44.TabIndex = 25;
            this.checkBox44.Text = "过滤内容相似文件";
            this.toolTip1.SetToolTip(this.checkBox44, "查询结果中前后内容相似的结果只取1条");
            this.checkBox44.UseVisualStyleBackColor = true;
            this.checkBox44.CheckedChanged += new System.EventHandler(this.checkBox44_CheckedChanged);
            // 
            // checkBox45
            // 
            this.checkBox45.AutoSize = true;
            this.checkBox45.Location = new System.Drawing.Point(196, 7);
            this.checkBox45.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox45.Name = "checkBox45";
            this.checkBox45.Size = new System.Drawing.Size(72, 16);
            this.checkBox45.TabIndex = 24;
            this.checkBox45.Text = "加密文件";
            this.toolTip1.SetToolTip(this.checkBox45, "主要指类似邮件协议中上传的附件中含有带密码的rar等类型压缩包");
            this.checkBox45.UseVisualStyleBackColor = true;
            this.checkBox45.CheckedChanged += new System.EventHandler(this.checkBox45_CheckedChanged);
            // 
            // checkBox46
            // 
            this.checkBox46.AutoSize = true;
            this.checkBox46.Location = new System.Drawing.Point(105, 7);
            this.checkBox46.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox46.Name = "checkBox46";
            this.checkBox46.Size = new System.Drawing.Size(60, 16);
            this.checkBox46.TabIndex = 23;
            this.checkBox46.Text = "同义词";
            this.checkBox46.UseVisualStyleBackColor = true;
            this.checkBox46.CheckedChanged += new System.EventHandler(this.checkBox46_CheckedChanged);
            // 
            // checkBox47
            // 
            this.checkBox47.AutoSize = true;
            this.checkBox47.Location = new System.Drawing.Point(10, 7);
            this.checkBox47.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox47.Name = "checkBox47";
            this.checkBox47.Size = new System.Drawing.Size(72, 16);
            this.checkBox47.TabIndex = 22;
            this.checkBox47.Text = "含有附件";
            this.toolTip1.SetToolTip(this.checkBox47, "主要指邮件中带有附件");
            this.checkBox47.UseVisualStyleBackColor = true;
            this.checkBox47.CheckedChanged += new System.EventHandler(this.checkBox47_CheckedChanged);
            // 
            // checkBox48
            // 
            this.checkBox48.AutoSize = true;
            this.checkBox48.Checked = true;
            this.checkBox48.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox48.Location = new System.Drawing.Point(112, 499);
            this.checkBox48.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox48.Name = "checkBox48";
            this.checkBox48.Size = new System.Drawing.Size(48, 16);
            this.checkBox48.TabIndex = 21;
            this.checkBox48.Text = "全选";
            this.checkBox48.UseVisualStyleBackColor = true;
            this.checkBox48.CheckedChanged += new System.EventHandler(this.checkBox48_CheckedChanged);
            this.checkBox48.Click += new System.EventHandler(this.checkBox48_Click);
            // 
            // checkBox51
            // 
            this.checkBox51.AutoSize = true;
            this.checkBox51.Location = new System.Drawing.Point(82, 7);
            this.checkBox51.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox51.Name = "checkBox51";
            this.checkBox51.Size = new System.Drawing.Size(48, 16);
            this.checkBox51.TabIndex = 30;
            this.checkBox51.Text = "垃圾";
            this.checkBox51.UseVisualStyleBackColor = true;
            this.checkBox51.CheckedChanged += new System.EventHandler(this.checkBox51_CheckedChanged);
            // 
            // checkBox52
            // 
            this.checkBox52.AutoSize = true;
            this.checkBox52.Location = new System.Drawing.Point(9, 7);
            this.checkBox52.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox52.Name = "checkBox52";
            this.checkBox52.Size = new System.Drawing.Size(48, 16);
            this.checkBox52.TabIndex = 29;
            this.checkBox52.Text = "正常";
            this.checkBox52.UseVisualStyleBackColor = true;
            this.checkBox52.CheckedChanged += new System.EventHandler(this.checkBox52_CheckedChanged);
            // 
            // checkBox53
            // 
            this.checkBox53.AutoSize = true;
            this.checkBox53.Checked = true;
            this.checkBox53.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox53.Location = new System.Drawing.Point(513, 434);
            this.checkBox53.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox53.Name = "checkBox53";
            this.checkBox53.Size = new System.Drawing.Size(48, 16);
            this.checkBox53.TabIndex = 28;
            this.checkBox53.Text = "全选";
            this.checkBox53.UseVisualStyleBackColor = true;
            this.checkBox53.CheckedChanged += new System.EventHandler(this.checkBox53_CheckedChanged);
            this.checkBox53.Click += new System.EventHandler(this.checkBox53_Click);
            // 
            // checkBox42
            // 
            this.checkBox42.AutoSize = true;
            this.checkBox42.Location = new System.Drawing.Point(82, 7);
            this.checkBox42.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox42.Name = "checkBox42";
            this.checkBox42.Size = new System.Drawing.Size(48, 16);
            this.checkBox42.TabIndex = 34;
            this.checkBox42.Text = "附件";
            this.checkBox42.UseVisualStyleBackColor = true;
            this.checkBox42.CheckedChanged += new System.EventHandler(this.checkBox42_CheckedChanged);
            // 
            // checkBox49
            // 
            this.checkBox49.AutoSize = true;
            this.checkBox49.Location = new System.Drawing.Point(10, 7);
            this.checkBox49.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox49.Name = "checkBox49";
            this.checkBox49.Size = new System.Drawing.Size(48, 16);
            this.checkBox49.TabIndex = 33;
            this.checkBox49.Text = "正文";
            this.checkBox49.UseVisualStyleBackColor = true;
            this.checkBox49.CheckedChanged += new System.EventHandler(this.checkBox49_CheckedChanged);
            // 
            // checkBox50
            // 
            this.checkBox50.AutoSize = true;
            this.checkBox50.Checked = true;
            this.checkBox50.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox50.Location = new System.Drawing.Point(112, 435);
            this.checkBox50.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox50.Name = "checkBox50";
            this.checkBox50.Size = new System.Drawing.Size(72, 16);
            this.checkBox50.TabIndex = 32;
            this.checkBox50.Text = "全部范围";
            this.checkBox50.UseVisualStyleBackColor = true;
            this.checkBox50.CheckedChanged += new System.EventHandler(this.checkBox50_CheckedChanged);
            this.checkBox50.Click += new System.EventHandler(this.checkBox50_Click);
            // 
            // checkBox54
            // 
            this.checkBox54.AutoSize = true;
            this.checkBox54.Location = new System.Drawing.Point(157, 7);
            this.checkBox54.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox54.Name = "checkBox54";
            this.checkBox54.Size = new System.Drawing.Size(72, 16);
            this.checkBox54.TabIndex = 35;
            this.checkBox54.Text = "邮件主题";
            this.checkBox54.UseVisualStyleBackColor = true;
            this.checkBox54.CheckedChanged += new System.EventHandler(this.checkBox54_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(12, 18);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 19);
            this.label8.TabIndex = 36;
            this.label8.Text = "对应查询条件：";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.button1.Location = new System.Drawing.Point(528, 17);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 27);
            this.button1.TabIndex = 37;
            this.button1.Text = "复制";
            this.toolTip1.SetToolTip(this.button1, "复制当前生成语法命令到剪贴板");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.button2.Location = new System.Drawing.Point(647, 17);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(59, 27);
            this.button2.TabIndex = 38;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBox2);
            this.panel2.Controls.Add(this.checkBox3);
            this.panel2.Controls.Add(this.checkBox15);
            this.panel2.Controls.Add(this.checkBox13);
            this.panel2.Controls.Add(this.checkBox9);
            this.panel2.Controls.Add(this.checkBox10);
            this.panel2.Controls.Add(this.checkBox7);
            this.panel2.Controls.Add(this.checkBox5);
            this.panel2.Controls.Add(this.checkBox6);
            this.panel2.Controls.Add(this.checkBox8);
            this.panel2.Controls.Add(this.checkBox12);
            this.panel2.Controls.Add(this.checkBox11);
            this.panel2.Location = new System.Drawing.Point(102, 362);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(564, 56);
            this.panel2.TabIndex = 41;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.checkBox47);
            this.panel3.Controls.Add(this.checkBox46);
            this.panel3.Controls.Add(this.checkBox45);
            this.panel3.Controls.Add(this.checkBox44);
            this.panel3.Controls.Add(this.checkBox43);
            this.panel3.Location = new System.Drawing.Point(102, 522);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(564, 28);
            this.panel3.TabIndex = 45;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.checkBox52);
            this.panel4.Controls.Add(this.checkBox51);
            this.panel4.Location = new System.Drawing.Point(504, 454);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(161, 28);
            this.panel4.TabIndex = 46;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.checkBox49);
            this.panel5.Controls.Add(this.checkBox42);
            this.panel5.Controls.Add(this.checkBox54);
            this.panel5.Location = new System.Drawing.Point(102, 455);
            this.panel5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(243, 28);
            this.panel5.TabIndex = 47;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "AND",
            "OR",
            "NOT"});
            this.comboBox5.Location = new System.Drawing.Point(112, 271);
            this.comboBox5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(70, 20);
            this.comboBox5.TabIndex = 42;
            this.comboBox5.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "文件大小（单位：字节）",
            "上网账号",
            "宿端口",
            "设备号",
            "IMSI号",
            "源端口",
            "源IP",
            "宿IP",
            "附件数",
            "域名"});
            this.comboBox3.Location = new System.Drawing.Point(185, 271);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(156, 20);
            this.comboBox3.TabIndex = 18;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "精确匹配",
            "模糊匹配",
            "大于",
            "大于等于",
            "等于",
            "小于",
            "小于等于"});
            this.comboBox4.Location = new System.Drawing.Point(344, 271);
            this.comboBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(122, 20);
            this.comboBox4.TabIndex = 19;
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(469, 271);
            this.textBox5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(204, 21);
            this.textBox5.TabIndex = 20;
            this.textBox5.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(112, 180);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(277, 21);
            this.textBox3.TabIndex = 50;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(392, 180);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(282, 21);
            this.textBox4.TabIndex = 51;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "精确匹配",
            "模糊匹配",
            "大于",
            "大于等于",
            "等于",
            "小于",
            "小于等于"});
            this.comboBox2.Location = new System.Drawing.Point(344, 242);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(122, 20);
            this.comboBox2.TabIndex = 18;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(469, 242);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(204, 21);
            this.textBox2.TabIndex = 19;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "文件大小（单位：字节）",
            "上网账号",
            "宿端口",
            "设备号",
            "IMSI号",
            "源端口",
            "源IP",
            "宿IP",
            "附件数",
            "域名"});
            this.comboBox1.Location = new System.Drawing.Point(112, 242);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(229, 20);
            this.comboBox1.TabIndex = 17;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.previewCmdText);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Size = new System.Drawing.Size(551, 125);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "queryclient";
            this.toolTip1.SetToolTip(this.tabPage1, "全文主节点自带的查询工具");
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // previewCmdText
            // 
            this.previewCmdText.BackColor = System.Drawing.SystemColors.HighlightText;
            this.previewCmdText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.previewCmdText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewCmdText.Location = new System.Drawing.Point(2, 2);
            this.previewCmdText.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.previewCmdText.Multiline = true;
            this.previewCmdText.Name = "previewCmdText";
            this.previewCmdText.ReadOnly = true;
            this.previewCmdText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.previewCmdText.Size = new System.Drawing.Size(547, 121);
            this.previewCmdText.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox9);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Size = new System.Drawing.Size(551, 125);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "batchQueryAndExport_1.7.jar";
            this.toolTip1.SetToolTip(this.tabPage2, "搞不清楚历史沿革的jar包查询全文工具");
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.SystemColors.HighlightText;
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox9.Location = new System.Drawing.Point(2, 2);
            this.textBox9.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textBox9.Multiline = true;
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox9.Size = new System.Drawing.Size(547, 121);
            this.textBox9.TabIndex = 3;
            // 
            // comboBox6
            // 
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Items.AddRange(new object[] {
            "AND",
            "OR",
            "NOT"});
            this.comboBox6.Location = new System.Drawing.Point(112, 301);
            this.comboBox6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(71, 20);
            this.comboBox6.TabIndex = 43;
            this.comboBox6.SelectedIndexChanged += new System.EventHandler(this.comboBox6_SelectedIndexChanged);
            // 
            // comboBox8
            // 
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.Items.AddRange(new object[] {
            "精确匹配",
            "模糊匹配",
            "大于",
            "大于等于",
            "等于",
            "小于",
            "小于等于"});
            this.comboBox8.Location = new System.Drawing.Point(344, 301);
            this.comboBox8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox8.Name = "comboBox8";
            this.comboBox8.Size = new System.Drawing.Size(122, 20);
            this.comboBox8.TabIndex = 45;
            this.comboBox8.SelectedIndexChanged += new System.EventHandler(this.comboBox8_SelectedIndexChanged);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(469, 301);
            this.textBox6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(204, 21);
            this.textBox6.TabIndex = 46;
            this.textBox6.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // comboBox7
            // 
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Items.AddRange(new object[] {
            "文件大小（单位：字节）",
            "上网账号",
            "宿端口",
            "设备号",
            "IMSI号",
            "源端口",
            "源IP",
            "宿IP",
            "附件数",
            "域名"});
            this.comboBox7.Location = new System.Drawing.Point(185, 301);
            this.comboBox7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(156, 20);
            this.comboBox7.TabIndex = 44;
            this.comboBox7.SelectedIndexChanged += new System.EventHandler(this.comboBox7_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(115, 15);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(559, 155);
            this.tabControl1.TabIndex = 57;
            // 
            // groupBox1
            // 
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(40, 432);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(347, 61);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "搜索范围：";
            // 
            // groupBox2
            // 
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(440, 431);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(234, 62);
            this.groupBox2.TabIndex = 59;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据类型：";
            // 
            // groupBox3
            // 
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(40, 496);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(634, 59);
            this.groupBox3.TabIndex = 60;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询方式：";
            // 
            // groupBox4
            // 
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(40, 334);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(634, 91);
            this.groupBox4.TabIndex = 61;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "协议类型：";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel6.Controls.Add(this.button1);
            this.panel6.Controls.Add(this.button2);
            this.panel6.Location = new System.Drawing.Point(0, 570);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(717, 58);
            this.panel6.TabIndex = 62;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(717, 629);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.comboBox7);
            this.Controls.Add(this.comboBox8);
            this.Controls.Add(this.comboBox6);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox5);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.checkBox50);
            this.Controls.Add(this.checkBox53);
            this.Controls.Add(this.checkBox48);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panel6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "全文语法助手";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Form1_HelpButtonClicked);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox15;
        private System.Windows.Forms.CheckBox checkBox13;
        private System.Windows.Forms.CheckBox checkBox12;
        private System.Windows.Forms.CheckBox checkBox11;
        private System.Windows.Forms.CheckBox checkBox10;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox43;
        private System.Windows.Forms.CheckBox checkBox44;
        private System.Windows.Forms.CheckBox checkBox45;
        private System.Windows.Forms.CheckBox checkBox46;
        private System.Windows.Forms.CheckBox checkBox47;
        private System.Windows.Forms.CheckBox checkBox48;
        private System.Windows.Forms.CheckBox checkBox51;
        private System.Windows.Forms.CheckBox checkBox52;
        private System.Windows.Forms.CheckBox checkBox53;
        private System.Windows.Forms.CheckBox checkBox42;
        private System.Windows.Forms.CheckBox checkBox49;
        private System.Windows.Forms.CheckBox checkBox50;
        private System.Windows.Forms.CheckBox checkBox54;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.ComboBox comboBox8;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.ComboBox comboBox7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox previewCmdText;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel6;
    }
}

