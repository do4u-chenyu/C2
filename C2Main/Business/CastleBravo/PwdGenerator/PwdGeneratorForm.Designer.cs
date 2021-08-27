using ICSharpCode.TextEditor.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2.Utils;


namespace C2.Business.CastleBravo.PwdGenerator
{
    partial class PwdGeneratorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PwdGeneratorForm));
            this.button2 = new System.Windows.Forms.Button();
            this.tBName = new System.Windows.Forms.TextBox();
            this.tBSpecial = new System.Windows.Forms.TextBox();
            this.tBBirth = new System.Windows.Forms.TextBox();
            this.tBQQ = new System.Windows.Forms.TextBox();
            this.tBPhone = new System.Windows.Forms.TextBox();
            this.tBOname = new System.Windows.Forms.TextBox();
            this.tBEname = new System.Windows.Forms.TextBox();
            this.tBWname = new System.Windows.Forms.TextBox();
            this.tBMail = new System.Windows.Forms.TextBox();
            this.tBOldpass = new System.Windows.Forms.TextBox();
            this.tBWifename = new System.Windows.Forms.TextBox();
            this.tBWifeWname = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tBSelf = new System.Windows.Forms.TextBox();
            this.tBWifePhone = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(185)))), ((int)(((byte)(94)))));
            this.button2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.Honeydew;
            this.button2.Location = new System.Drawing.Point(531, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 29);
            this.button2.TabIndex = 0;
            this.button2.Text = "提交";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tBName
            // 
            this.tBName.BackColor = System.Drawing.SystemColors.Window;
            this.tBName.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tBName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tBName.Location = new System.Drawing.Point(100, 79);
            this.tBName.Name = "tBName";
            this.tBName.Size = new System.Drawing.Size(170, 26);
            this.tBName.TabIndex = 0;
            this.tBName.Tag = "";
            this.tBName.Text = "姓名简拼";
            // 
            // tBSpecial
            // 
            this.tBSpecial.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tBSpecial.Location = new System.Drawing.Point(411, 214);
            this.tBSpecial.Name = "tBSpecial";
            this.tBSpecial.Size = new System.Drawing.Size(170, 26);
            this.tBSpecial.TabIndex = 7;
            this.tBSpecial.Text = "特殊数字 \'|\' 分隔";
            // 
            // tBBirth
            // 
            this.tBBirth.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tBBirth.Location = new System.Drawing.Point(411, 124);
            this.tBBirth.MaxLength = 10;
            this.tBBirth.Name = "tBBirth";
            this.tBBirth.Size = new System.Drawing.Size(170, 26);
            this.tBBirth.TabIndex = 3;
            this.tBBirth.Text = "出生日期";
            this.tBBirth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Birth_KeyPress);
            // 
            // tBQQ
            // 
            this.tBQQ.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tBQQ.Location = new System.Drawing.Point(100, 124);
            this.tBQQ.MaxLength = 12;
            this.tBQQ.Name = "tBQQ";
            this.tBQQ.Size = new System.Drawing.Size(170, 26);
            this.tBQQ.TabIndex = 2;
            this.tBQQ.Text = "QQ号";
            this.tBQQ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.QQ_KeyPress);
            // 
            // tBPhone
            // 
            this.tBPhone.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tBPhone.Location = new System.Drawing.Point(100, 169);
            this.tBPhone.MaxLength = 11;
            this.tBPhone.Name = "tBPhone";
            this.tBPhone.Size = new System.Drawing.Size(170, 26);
            this.tBPhone.TabIndex = 4;
            this.tBPhone.Text = "手机号";
            this.tBPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Phone_KeyPress);
            // 
            // tBOname
            // 
            this.tBOname.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tBOname.Location = new System.Drawing.Point(100, 214);
            this.tBOname.Name = "tBOname";
            this.tBOname.Size = new System.Drawing.Size(170, 26);
            this.tBOname.TabIndex = 6;
            this.tBOname.Text = "用户名";
            // 
            // tBEname
            // 
            this.tBEname.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tBEname.Location = new System.Drawing.Point(411, 169);
            this.tBEname.Name = "tBEname";
            this.tBEname.Size = new System.Drawing.Size(170, 26);
            this.tBEname.TabIndex = 5;
            this.tBEname.Text = "英文名";
            // 
            // tBWname
            // 
            this.tBWname.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tBWname.Location = new System.Drawing.Point(411, 79);
            this.tBWname.Name = "tBWname";
            this.tBWname.Size = new System.Drawing.Size(170, 26);
            this.tBWname.TabIndex = 1;
            this.tBWname.Text = "姓名全拼  \'|\'分隔";
            // 
            // tBMail
            // 
            this.tBMail.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tBMail.Location = new System.Drawing.Point(100, 259);
            this.tBMail.Name = "tBMail";
            this.tBMail.Size = new System.Drawing.Size(170, 26);
            this.tBMail.TabIndex = 8;
            this.tBMail.Text = "邮箱";
            // 
            // tBOldpass
            // 
            this.tBOldpass.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tBOldpass.Location = new System.Drawing.Point(411, 259);
            this.tBOldpass.Name = "tBOldpass";
            this.tBOldpass.Size = new System.Drawing.Size(170, 26);
            this.tBOldpass.TabIndex = 9;
            this.tBOldpass.Text = "历史密码 \'|\'分隔";
            // 
            // tBWifename
            // 
            this.tBWifename.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tBWifename.Location = new System.Drawing.Point(80, 29);
            this.tBWifename.Name = "tBWifename";
            this.tBWifename.Size = new System.Drawing.Size(170, 26);
            this.tBWifename.TabIndex = 10;
            this.tBWifename.Text = "伴侣姓名简拼";
            // 
            // tBWifeWname
            // 
            this.tBWifeWname.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tBWifeWname.Location = new System.Drawing.Point(391, 29);
            this.tBWifeWname.Name = "tBWifeWname";
            this.tBWifeWname.Size = new System.Drawing.Size(170, 26);
            this.tBWifeWname.TabIndex = 11;
            this.tBWifeWname.Text = "伴侣姓名全拼 \'|\'分隔";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label23.Location = new System.Drawing.Point(253, 27);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(220, 31);
            this.label23.TabIndex = 25;
            this.label23.Text = "利用习惯  精准分析";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 466);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(728, 59);
            this.panel1.TabIndex = 39;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(587, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 17);
            this.label9.TabIndex = 42;
            this.label9.Text = "*";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(584, 127);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(94, 20);
            this.label16.TabIndex = 45;
            this.label16.Text = "例: 19931213";
            // 
            // tBSelf
            // 
            this.tBSelf.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tBSelf.Location = new System.Drawing.Point(100, 304);
            this.tBSelf.Name = "tBSelf";
            this.tBSelf.Size = new System.Drawing.Size(170, 26);
            this.tBSelf.TabIndex = 46;
            this.tBSelf.Text = "自定义";
            // 
            // tBWifePhone
            // 
            this.tBWifePhone.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tBWifePhone.Location = new System.Drawing.Point(80, 73);
            this.tBWifePhone.MaxLength = 11;
            this.tBWifePhone.Name = "tBWifePhone";
            this.tBWifePhone.Size = new System.Drawing.Size(170, 26);
            this.tBWifePhone.TabIndex = 12;
            this.tBWifePhone.Text = "伴侣手机号";
            this.tBWifePhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Wphone_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(278, 79);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 17);
            this.label17.TabIndex = 49;
            this.label17.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(278, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 17);
            this.label6.TabIndex = 50;
            this.label6.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label7.Location = new System.Drawing.Point(85, 414);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(261, 31);
            this.label7.TabIndex = 51;
            this.label7.Text = "利用人的习惯,精准分析";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pictureBox12);
            this.groupBox1.Controls.Add(this.pictureBox13);
            this.groupBox1.Controls.Add(this.pictureBox14);
            this.groupBox1.Controls.Add(this.tBWifename);
            this.groupBox1.Controls.Add(this.tBWifeWname);
            this.groupBox1.Controls.Add(this.tBWifePhone);
            this.groupBox1.Location = new System.Drawing.Point(20, 346);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(670, 114);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "伴侣信息";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(564, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 20);
            this.label5.TabIndex = 71;
            this.label5.Text = "例:du|fu";
            // 
            // pictureBox12
            // 
            this.pictureBox12.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox12.BackgroundImage")));
            this.pictureBox12.Location = new System.Drawing.Point(45, 73);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(35, 26);
            this.pictureBox12.TabIndex = 68;
            this.pictureBox12.TabStop = false;
            // 
            // pictureBox13
            // 
            this.pictureBox13.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox13.BackgroundImage")));
            this.pictureBox13.Location = new System.Drawing.Point(45, 29);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(35, 26);
            this.pictureBox13.TabIndex = 69;
            this.pictureBox13.TabStop = false;
            // 
            // pictureBox14
            // 
            this.pictureBox14.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox14.BackgroundImage")));
            this.pictureBox14.Location = new System.Drawing.Point(356, 29);
            this.pictureBox14.Name = "pictureBox14";
            this.pictureBox14.Size = new System.Drawing.Size(35, 26);
            this.pictureBox14.TabIndex = 70;
            this.pictureBox14.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(584, 262);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(144, 20);
            this.label15.TabIndex = 54;
            this.label15.Text = "例: wzzcb@xx|abc3#";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(376, 79);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 26);
            this.pictureBox1.TabIndex = 56;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(273, 307);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 20);
            this.label1.TabIndex = 57;
            this.label1.Text = "例：aaa|@,分隔符为“|”";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.Location = new System.Drawing.Point(376, 259);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(35, 26);
            this.pictureBox2.TabIndex = 58;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.Location = new System.Drawing.Point(376, 214);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(35, 26);
            this.pictureBox3.TabIndex = 59;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.BackgroundImage")));
            this.pictureBox4.Location = new System.Drawing.Point(65, 124);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(35, 26);
            this.pictureBox4.TabIndex = 60;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox5.BackgroundImage")));
            this.pictureBox5.Location = new System.Drawing.Point(65, 214);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(35, 26);
            this.pictureBox5.TabIndex = 61;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox6.BackgroundImage")));
            this.pictureBox6.Location = new System.Drawing.Point(65, 79);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(35, 26);
            this.pictureBox6.TabIndex = 62;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox7.BackgroundImage")));
            this.pictureBox7.Location = new System.Drawing.Point(65, 304);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(35, 26);
            this.pictureBox7.TabIndex = 63;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox8.BackgroundImage")));
            this.pictureBox8.Location = new System.Drawing.Point(65, 259);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(35, 26);
            this.pictureBox8.TabIndex = 64;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox9.BackgroundImage")));
            this.pictureBox9.Location = new System.Drawing.Point(376, 124);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(35, 26);
            this.pictureBox9.TabIndex = 65;
            this.pictureBox9.TabStop = false;
            // 
            // pictureBox10
            // 
            this.pictureBox10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox10.BackgroundImage")));
            this.pictureBox10.Location = new System.Drawing.Point(65, 169);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(35, 26);
            this.pictureBox10.TabIndex = 66;
            this.pictureBox10.TabStop = false;
            // 
            // pictureBox11
            // 
            this.pictureBox11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox11.BackgroundImage")));
            this.pictureBox11.Location = new System.Drawing.Point(376, 169);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(35, 26);
            this.pictureBox11.TabIndex = 67;
            this.pictureBox11.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 68;
            this.label2.Text = "例: admin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(584, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.TabIndex = 69;
            this.label3.Text = "例: 123|666";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(595, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 70;
            this.label4.Text = "例: li|bai";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(584, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 20);
            this.label8.TabIndex = 71;
            this.label8.Text = "例: Mike";
            // 
            // PwdGeneratorForm
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(728, 525);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox11);
            this.Controls.Add(this.pictureBox10);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tBSelf);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.tBMail);
            this.Controls.Add(this.tBOldpass);
            this.Controls.Add(this.tBWname);
            this.Controls.Add(this.tBEname);
            this.Controls.Add(this.tBOname);
            this.Controls.Add(this.tBPhone);
            this.Controls.Add(this.tBQQ);
            this.Controls.Add(this.tBBirth);
            this.Controls.Add(this.tBSpecial);
            this.Controls.Add(this.tBName);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PwdGeneratorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "社工生成";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tBName;
        private System.Windows.Forms.TextBox tBSpecial;
        private System.Windows.Forms.TextBox tBBirth;
        private System.Windows.Forms.TextBox tBQQ;
        private System.Windows.Forms.TextBox tBPhone;
        private System.Windows.Forms.TextBox tBOname;
        private System.Windows.Forms.TextBox tBEname;
        private System.Windows.Forms.TextBox tBWname;
        private System.Windows.Forms.TextBox tBMail;
        private System.Windows.Forms.TextBox tBOldpass;
        private System.Windows.Forms.TextBox tBWifename;
        private System.Windows.Forms.TextBox tBWifeWname;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tBSelf;
        private System.Windows.Forms.TextBox tBWifePhone;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.PictureBox pictureBox13;
        private System.Windows.Forms.PictureBox pictureBox14;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
    }
}