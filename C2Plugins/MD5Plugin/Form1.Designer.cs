namespace MD5Plugin
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
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.encodeButton = new System.Windows.Forms.Button();
            this.decodeButton = new System.Windows.Forms.Button();
            this.FileInputButton = new System.Windows.Forms.Button();
            this.base64strButton = new System.Windows.Forms.Button();
            this.UnicodeToChineseButton = new System.Windows.Forms.Button();
            this.UnicodeToAsciiButton = new System.Windows.Forms.Button();
            this.md5128RadioButton = new System.Windows.Forms.RadioButton();
            this.base64RadioButton = new System.Windows.Forms.RadioButton();
            this.urlRadioButton = new System.Windows.Forms.RadioButton();
            this.unicodeRadioButton = new System.Windows.Forms.RadioButton();
            this.md564RadioButton = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.sha1RadioButton = new System.Windows.Forms.RadioButton();
            this.sha256RadioButton = new System.Windows.Forms.RadioButton();
            this.sha512RadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // inputTextBox
            // 
            this.inputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputTextBox.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.inputTextBox.Location = new System.Drawing.Point(6, 52);
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(460, 450);
            this.inputTextBox.TabIndex = 0;
            this.inputTextBox.Text = "请输入你要编码的内容或者需要加密文件的路径";
            this.inputTextBox.WordWrap = false;
            this.inputTextBox.TextChanged += new System.EventHandler(this.inputTextBox_TextChanged);
            this.inputTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InputTextBox_MouseDown);
            // 
            // outputTextBox
            // 
            this.outputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputTextBox.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.outputTextBox.Location = new System.Drawing.Point(561, 52);
            this.outputTextBox.MaxLength = 0;
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputTextBox.Size = new System.Drawing.Size(460, 450);
            this.outputTextBox.TabIndex = 1;
            this.outputTextBox.Text = "加密后的结果";
            this.outputTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OutputTextBox_MouseDown);
            // 
            // encodeButton
            // 
            this.encodeButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.encodeButton.Location = new System.Drawing.Point(476, 229);
            this.encodeButton.Name = "encodeButton";
            this.encodeButton.Size = new System.Drawing.Size(75, 30);
            this.encodeButton.TabIndex = 2;
            this.encodeButton.Text = "编码 =>";
            this.encodeButton.UseVisualStyleBackColor = true;
            this.encodeButton.Click += new System.EventHandler(this.EncodeButton_Click);
            // 
            // decodeButton
            // 
            this.decodeButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.decodeButton.Location = new System.Drawing.Point(476, 284);
            this.decodeButton.Name = "decodeButton";
            this.decodeButton.Size = new System.Drawing.Size(75, 30);
            this.decodeButton.TabIndex = 3;
            this.decodeButton.Text = "<= 解码";
            this.decodeButton.UseVisualStyleBackColor = true;
            this.decodeButton.Click += new System.EventHandler(this.DecodeButton_Click);
            // 
            // FileInputButton
            // 
            /*
            this.FileInputButton.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FileInputButton.Location = new System.Drawing.Point(476, 229);
            this.FileInputButton.Name = "FileInputButton";
            this.FileInputButton.Size = new System.Drawing.Size(100, 40);
            this.FileInputButton.TabIndex = 4;
            this.FileInputButton.Text = "编码 =>";
            this.FileInputButton.UseVisualStyleBackColor = true;
            this.FileInputButton.Click += new System.EventHandler(this.FileInputButton_Click);
            */
            // 
            // base64strButton
            // 
            /*
            this.base64strButton.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.base64strButton.Location = new System.Drawing.Point(476, 284);
            this.base64strButton.Name = "base64strButton";
            this.base64strButton.Size = new System.Drawing.Size(100, 40);
            this.base64strButton.TabIndex = 4;
            this.base64strButton.Text = "<= 解码";
            this.base64strButton.UseVisualStyleBackColor = true;
            this.base64strButton.Click += new System.EventHandler(this.base64strButton_Click);
            */
            // 
            // UnicodeToChineseButton
            // 
            this.UnicodeToChineseButton.Location = new System.Drawing.Point(0, 0);
            this.UnicodeToChineseButton.Name = "UnicodeToChineseButton";
            this.UnicodeToChineseButton.Size = new System.Drawing.Size(75, 23);
            this.UnicodeToChineseButton.TabIndex = 14;
            // 
            // UnicodeToAsciiButton
            // 
            this.UnicodeToAsciiButton.Location = new System.Drawing.Point(0, 0);
            this.UnicodeToAsciiButton.Name = "UnicodeToAsciiButton";
            this.UnicodeToAsciiButton.Size = new System.Drawing.Size(75, 23);
            this.UnicodeToAsciiButton.TabIndex = 15;
            // 
            // md5128RadioButton
            // 
            this.md5128RadioButton.AutoSize = true;
            this.md5128RadioButton.Font = new System.Drawing.Font("宋体", 10F);
            this.md5128RadioButton.Location = new System.Drawing.Point(9, 13);
            this.md5128RadioButton.Name = "md5128RadioButton";
            this.md5128RadioButton.Size = new System.Drawing.Size(95, 18);
            this.md5128RadioButton.TabIndex = 8;
            this.md5128RadioButton.Text = "MD5(128位)";
            this.toolTip1.SetToolTip(this.md5128RadioButton, "使用MD5(128位)编码加密字符串");
            this.md5128RadioButton.UseVisualStyleBackColor = true;
            this.md5128RadioButton.CheckedChanged += new System.EventHandler(this.Md5128RadioButton_CheckedChanged);
            // 
            // base64RadioButton
            // 
            this.base64RadioButton.AutoSize = true;
            this.base64RadioButton.Checked = true;
            this.base64RadioButton.Font = new System.Drawing.Font("宋体", 10F);
            this.base64RadioButton.Location = new System.Drawing.Point(199, 13);
            this.base64RadioButton.Name = "base64RadioButton";
            this.base64RadioButton.Size = new System.Drawing.Size(67, 18);
            this.base64RadioButton.TabIndex = 5;
            this.base64RadioButton.TabStop = true;
            this.base64RadioButton.Text = "Base64";
            this.toolTip1.SetToolTip(this.base64RadioButton, "使用Base64编码对字符串进行编码解码");
            this.base64RadioButton.UseVisualStyleBackColor = true;
            this.base64RadioButton.CheckedChanged += new System.EventHandler(this.Base64RadioButton_CheckedChanged);
            // 
            // urlRadioButton
            // 
            this.urlRadioButton.AutoSize = true;
            this.urlRadioButton.Font = new System.Drawing.Font("宋体", 10F);
            this.urlRadioButton.Location = new System.Drawing.Point(273, 13);
            this.urlRadioButton.Name = "urlRadioButton";
            this.urlRadioButton.Size = new System.Drawing.Size(88, 18);
            this.urlRadioButton.TabIndex = 6;
            this.urlRadioButton.Text = "Url编解码";
            this.toolTip1.SetToolTip(this.urlRadioButton, "使用UrlEncode编码对字符串进行编码解码");
            this.urlRadioButton.UseVisualStyleBackColor = true;
            this.urlRadioButton.CheckedChanged += new System.EventHandler(this.UrlRadioButton_CheckedChanged);
            // 
            // unicodeRadioButton
            // 
            this.unicodeRadioButton.AutoSize = true;
            this.unicodeRadioButton.Font = new System.Drawing.Font("宋体", 10F);
            this.unicodeRadioButton.Location = new System.Drawing.Point(370, 13);
            this.unicodeRadioButton.Name = "unicodeRadioButton";
            this.unicodeRadioButton.Size = new System.Drawing.Size(116, 18);
            this.unicodeRadioButton.TabIndex = 12;
            this.unicodeRadioButton.Text = "Unicode编解码";
            this.toolTip1.SetToolTip(this.unicodeRadioButton, "使用Unicode编码对字符串进行编码解码");
            this.unicodeRadioButton.UseVisualStyleBackColor = true;
            this.unicodeRadioButton.CheckedChanged += new System.EventHandler(this.UnicodeRadioButton_CheckedChanged);
            // 
            // md564RadioButton
            // 
            this.md564RadioButton.AutoSize = true;
            this.md564RadioButton.Font = new System.Drawing.Font("宋体", 10F);
            this.md564RadioButton.Location = new System.Drawing.Point(107, 13);
            this.md564RadioButton.Name = "md564RadioButton";
            this.md564RadioButton.Size = new System.Drawing.Size(88, 18);
            this.md564RadioButton.TabIndex = 7;
            this.md564RadioButton.Text = "MD5(64位)";
            this.toolTip1.SetToolTip(this.md564RadioButton, "使用MD5(64位)编码加密字符串");
            this.md564RadioButton.UseVisualStyleBackColor = true;
            this.md564RadioButton.CheckedChanged += new System.EventHandler(this.Md564RadioButton_CheckedChanged);
            // 
            // sha1RadioButton
            // 
            this.sha1RadioButton.AutoSize = true;
            this.sha1RadioButton.Font = new System.Drawing.Font("宋体", 10F);
            this.sha1RadioButton.Location = new System.Drawing.Point(495, 13);
            this.sha1RadioButton.Name = "sha1RadioButton";
            this.sha1RadioButton.Size = new System.Drawing.Size(53, 18);
            this.sha1RadioButton.TabIndex = 9;
            this.sha1RadioButton.Text = "sha1";
            this.toolTip1.SetToolTip(this.sha1RadioButton, "使用sha1对字符串进行加密");
            this.sha1RadioButton.UseVisualStyleBackColor = true;
            this.sha1RadioButton.CheckedChanged += new System.EventHandler(this.Sha1RadioButton_CheckedChanged);
            // 
            // sha256RadioButton
            // 
            this.sha256RadioButton.AutoSize = true;
            this.sha256RadioButton.Font = new System.Drawing.Font("宋体", 10F);
            this.sha256RadioButton.Location = new System.Drawing.Point(555, 13);
            this.sha256RadioButton.Name = "sha256RadioButton";
            this.sha256RadioButton.Size = new System.Drawing.Size(67, 18);
            this.sha256RadioButton.TabIndex = 10;
            this.sha256RadioButton.Text = "sha256";
            this.toolTip1.SetToolTip(this.sha256RadioButton, "使用sha256对字符串进行加密");
            this.sha256RadioButton.UseVisualStyleBackColor = true;
            this.sha256RadioButton.CheckedChanged += new System.EventHandler(this.Sha256RadioButton_CheckedChanged);
            // 
            // sha512RadioButton
            // 
            this.sha512RadioButton.AutoSize = true;
            this.sha512RadioButton.Font = new System.Drawing.Font("宋体", 10F);
            this.sha512RadioButton.Location = new System.Drawing.Point(635, 13);
            this.sha512RadioButton.Name = "sha512RadioButton";
            this.sha512RadioButton.Size = new System.Drawing.Size(67, 18);
            this.sha512RadioButton.TabIndex = 11;
            this.sha512RadioButton.Text = "sha512";
            this.toolTip1.SetToolTip(this.sha512RadioButton, "使用sha512对字符串进行加密");
            this.sha512RadioButton.UseVisualStyleBackColor = true;
            this.sha512RadioButton.CheckedChanged += new System.EventHandler(this.Sha512RadioButton_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1027, 513);
            this.Controls.Add(this.sha512RadioButton);
            this.Controls.Add(this.sha256RadioButton);
            this.Controls.Add(this.sha1RadioButton);
            this.Controls.Add(this.md564RadioButton);
            this.Controls.Add(this.urlRadioButton);
            this.Controls.Add(this.unicodeRadioButton);
            this.Controls.Add(this.base64RadioButton);
            this.Controls.Add(this.md5128RadioButton);
            this.Controls.Add(this.decodeButton);
            this.Controls.Add(this.encodeButton);
            this.Controls.Add(this.FileInputButton);
            this.Controls.Add(this.base64strButton);
            this.Controls.Add(this.UnicodeToChineseButton);
            this.Controls.Add(this.UnicodeToAsciiButton);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.inputTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MD5加密";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Button encodeButton;
        private System.Windows.Forms.Button decodeButton;
        private System.Windows.Forms.Button FileInputButton;
        private System.Windows.Forms.Button base64strButton;
        private System.Windows.Forms.Button UnicodeToChineseButton;
        private System.Windows.Forms.Button UnicodeToAsciiButton;
        private System.Windows.Forms.RadioButton md5128RadioButton;
        private System.Windows.Forms.RadioButton base64RadioButton;
        private System.Windows.Forms.RadioButton urlRadioButton;
        private System.Windows.Forms.RadioButton unicodeRadioButton;
        private System.Windows.Forms.RadioButton md564RadioButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RadioButton sha1RadioButton;
        private System.Windows.Forms.RadioButton sha256RadioButton;
        private System.Windows.Forms.RadioButton sha512RadioButton;
    }
}

