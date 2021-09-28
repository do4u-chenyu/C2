namespace C2.Business.IAOLab.PostAndGet
{
    partial class PostAndGetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PostAndGetForm));
            this.textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.格式化Response = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.原始Response = new System.Windows.Forms.TabPage();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.Headers = new System.Windows.Forms.TabPage();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.baseControl1 = new C2.Controls.BaseControl();
            this.tabControl1.SuspendLayout();
            this.格式化Response.SuspendLayout();
            this.原始Response.SuspendLayout();
            this.Headers.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.textBox.Location = new System.Drawing.Point(12, 29);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(631, 44);
            this.textBox.TabIndex = 1;
            this.textBox.Text = "输入你测试的url";
            this.textBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textbox_MouseDown);
            this.textBox.Leave += new System.EventHandler(this.textbox_Leave);

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(12, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "post参数：";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(14, 100);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(629, 42);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(12, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(527, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "参数：a=b&c=d&f=e,如果传递参数是 json,请修改高级中header为：Content-Type:application/json";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F);
            this.label3.Location = new System.Drawing.Point(12, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "cookie:";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location = new System.Drawing.Point(14, 184);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(631, 51);
            this.textBox2.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label4.Location = new System.Drawing.Point(12, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(287, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "cookie的格式：key=value;key2=value2;key3=value3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F);
            this.label5.Location = new System.Drawing.Point(13, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "header:";
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Location = new System.Drawing.Point(15, 278);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(631, 48);
            this.textBox3.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label6.Location = new System.Drawing.Point(13, 329);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(245, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "header的格式：key:value的格式,一行一条。";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10F);
            this.label7.Location = new System.Drawing.Point(14, 357);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 14);
            this.label7.TabIndex = 11;
            this.label7.Text = "代理:";
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Location = new System.Drawing.Point(16, 372);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(631, 38);
            this.textBox4.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label8.Location = new System.Drawing.Point(14, 413);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(311, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "代理格式：10.10.10.10:8080,使用代理访问你提交的接口";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("微软雅黑", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "GET",
            "POST",
            "HEAD",
            "OPTIONS",
            "PUT"});
            this.comboBox1.Location = new System.Drawing.Point(17, 437);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(142, 27);
            this.comboBox1.TabIndex = 15;
            this.comboBox1.SelectedIndex = comboBox1.Items.IndexOf("GET");
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("微软雅黑", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "UTF-8 --接口输出的编码",
            "GBK   --接口输出的编码"});
            this.comboBox2.Location = new System.Drawing.Point(179, 437);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(214, 27);
            this.comboBox2.TabIndex = 16;
            this.comboBox2.SelectedIndex = comboBox2.Items.IndexOf("UTF-8 --接口输出的编码");
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(253, 484);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 38);
            this.button2.TabIndex = 18;
            this.button2.Text = "测试样例";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(438, 484);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(209, 38);
            this.button3.TabIndex = 19;
            this.button3.Text = "清空表单";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.格式化Response);
            this.tabControl1.Controls.Add(this.原始Response);
            this.tabControl1.Controls.Add(this.Headers);
            this.tabControl1.Font = new System.Drawing.Font("宋体", 10F);
            this.tabControl1.Location = new System.Drawing.Point(18, 540);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(630, 256);
            this.tabControl1.TabIndex = 20;
            // 
            // 格式化Response
            // 
            this.格式化Response.Controls.Add(this.richTextBox1);
            this.格式化Response.Location = new System.Drawing.Point(4, 23);
            this.格式化Response.Name = "格式化Response";
            this.格式化Response.Padding = new System.Windows.Forms.Padding(3);
            this.格式化Response.Size = new System.Drawing.Size(622, 229);
            this.格式化Response.TabIndex = 0;
            this.格式化Response.Text = "格式化Response";
            this.格式化Response.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(621, 229);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // 原始Response
            // 
            this.原始Response.Controls.Add(this.richTextBox2);
            this.原始Response.Location = new System.Drawing.Point(4, 23);
            this.原始Response.Name = "原始Response";
            this.原始Response.Padding = new System.Windows.Forms.Padding(3);
            this.原始Response.Size = new System.Drawing.Size(622, 229);
            this.原始Response.TabIndex = 1;
            this.原始Response.Text = "原始Response";
            this.原始Response.UseVisualStyleBackColor = true;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(0, 3);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(619, 226);
            this.richTextBox2.TabIndex = 0;
            this.richTextBox2.Text = "";
            // 
            // Headers
            // 
            this.Headers.Controls.Add(this.richTextBox3);
            this.Headers.Location = new System.Drawing.Point(4, 23);
            this.Headers.Name = "Headers";
            this.Headers.Padding = new System.Windows.Forms.Padding(3);
            this.Headers.Size = new System.Drawing.Size(622, 229);
            this.Headers.TabIndex = 2;
            this.Headers.Text = "Headers";
            this.Headers.UseVisualStyleBackColor = true;
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(0, 3);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(621, 223);
            this.richTextBox3.TabIndex = 0;
            this.richTextBox3.Text = "";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.Font = new System.Drawing.Font("微软雅黑", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "自动解压(gzip,deflate,flate)",
            "不解压"});
            this.comboBox3.Location = new System.Drawing.Point(418, 437);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(228, 27);
            this.comboBox3.TabIndex = 21;
            this.comboBox3.SelectedIndex = comboBox3.Items.IndexOf("自动解压(gzip,deflate,flate)");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 484);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(202, 38);
            this.button1.TabIndex = 22;
            this.button1.Text = "提交";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // baseControl1
            // 
            this.baseControl1.Location = new System.Drawing.Point(0, 0);
            this.baseControl1.Name = "baseControl1";
            this.baseControl1.Size = new System.Drawing.Size(75, 23);
            this.baseControl1.TabIndex = 14;
            this.baseControl1.Text = "baseControl1";
            // 
            // PostAndGetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 808);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.baseControl1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PostAndGetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "在线Post和Get";
            this.tabControl1.ResumeLayout(false);
            this.格式化Response.ResumeLayout(false);
            this.原始Response.ResumeLayout(false);
            this.Headers.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label8;
        private Controls.BaseControl baseControl1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage 格式化Response;
        private System.Windows.Forms.TabPage 原始Response;
        private System.Windows.Forms.TabPage Headers;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button1;
    }
}