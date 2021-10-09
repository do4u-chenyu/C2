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
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.labelPost = new System.Windows.Forms.Label();
            this.textBoxPost = new System.Windows.Forms.TextBox();
            this.labelCookie = new System.Windows.Forms.Label();
            this.textBoxCookie = new System.Windows.Forms.TextBox();
            this.labelCookieFormat = new System.Windows.Forms.Label();
            this.labelHeader = new System.Windows.Forms.Label();
            this.textBoxHeader = new System.Windows.Forms.TextBox();
            this.labelHeaderFormat = new System.Windows.Forms.Label();
            this.labelIp = new System.Windows.Forms.Label();
            this.textBoxIp = new System.Windows.Forms.TextBox();
            this.labelIpFormat = new System.Windows.Forms.Label();
            this.comboBoxHttpMethod = new System.Windows.Forms.ComboBox();
            this.comboBoxEncodeMethod = new System.Windows.Forms.ComboBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.labelUrl = new System.Windows.Forms.Label();
            this.tabControlResponse = new System.Windows.Forms.TabControl();
            this.formatResponse = new System.Windows.Forms.TabPage();
            this.richTextBoxResponse = new System.Windows.Forms.RichTextBox();
            this.tabControlResponse.SuspendLayout();
            this.formatResponse.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUrl.ForeColor = System.Drawing.Color.Black;
            this.textBoxUrl.Location = new System.Drawing.Point(12, 22);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(712, 21);
            this.textBoxUrl.TabIndex = 1;
            // 
            // labelPost
            // 
            this.labelPost.AutoSize = true;
            this.labelPost.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelPost.Location = new System.Drawing.Point(9, 47);
            this.labelPost.Name = "labelPost";
            this.labelPost.Size = new System.Drawing.Size(39, 17);
            this.labelPost.TabIndex = 2;
            this.labelPost.Text = "POST";
            // 
            // textBoxUrl
            // 
            this.textBoxPost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPost.Location = new System.Drawing.Point(12, 64);
            this.textBoxPost.Multiline = true;
            this.textBoxPost.Name = "textBoxPost";
            this.textBoxPost.Size = new System.Drawing.Size(712, 42);
            this.textBoxPost.TabIndex = 2;
            // 
            // labelCookie
            // 
            this.labelCookie.AutoSize = true;
            this.labelCookie.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelCookie.Location = new System.Drawing.Point(9, 110);
            this.labelCookie.Name = "labelCookie";
            this.labelCookie.Size = new System.Drawing.Size(49, 17);
            this.labelCookie.TabIndex = 5;
            this.labelCookie.Text = "Cookie";
            // 
            // textBoxCookie
            // 
            this.textBoxCookie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCookie.Location = new System.Drawing.Point(12, 127);
            this.textBoxCookie.Multiline = true;
            this.textBoxCookie.Name = "textBoxCookie";
            this.textBoxCookie.Size = new System.Drawing.Size(347, 51);
            this.textBoxCookie.TabIndex = 3;
            // 
            // labelCookieFormat
            // 
            this.labelCookieFormat.AutoSize = true;
            this.labelCookieFormat.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCookieFormat.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelCookieFormat.Location = new System.Drawing.Point(11, 186);
            this.labelCookieFormat.Name = "labelCookieFormat";
            this.labelCookieFormat.Size = new System.Drawing.Size(296, 12);
            this.labelCookieFormat.TabIndex = 7;
            this.labelCookieFormat.Text = "格式：Key=Value;Key2=Value2; 分号分割键值对";
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelHeader.Location = new System.Drawing.Point(374, 110);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(51, 17);
            this.labelHeader.TabIndex = 8;
            this.labelHeader.Text = "Header";
            // 
            // textBoxHeader
            // 
            this.textBoxHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxHeader.Location = new System.Drawing.Point(377, 127);
            this.textBoxHeader.Multiline = true;
            this.textBoxHeader.Name = "textBoxHeader";
            this.textBoxHeader.Size = new System.Drawing.Size(347, 51);
            this.textBoxHeader.TabIndex = 4;
            // 
            // labelHeaderFormat
            // 
            this.labelHeaderFormat.AutoSize = true;
            this.labelHeaderFormat.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelHeaderFormat.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelHeaderFormat.Location = new System.Drawing.Point(375, 186);
            this.labelHeaderFormat.Name = "labelHeaderFormat";
            this.labelHeaderFormat.Size = new System.Drawing.Size(173, 12);
            this.labelHeaderFormat.TabIndex = 10;
            this.labelHeaderFormat.Text = "格式：Key:Value  一行一条";
            // 
            // labelIp
            // 
            this.labelIp.AutoSize = true;
            this.labelIp.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelIp.Location = new System.Drawing.Point(374, 210);
            this.labelIp.Name = "labelIp";
            this.labelIp.Size = new System.Drawing.Size(56, 17);
            this.labelIp.TabIndex = 11;
            this.labelIp.Text = "代理设置";
            // 
            // textBoxIp
            // 
            this.textBoxIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxIp.Location = new System.Drawing.Point(377, 233);
            this.textBoxIp.Name = "textBoxIp";
            this.textBoxIp.Size = new System.Drawing.Size(244, 21);
            this.textBoxIp.TabIndex = 7;
            // 
            // labelIpFormat
            // 
            this.labelIpFormat.AutoSize = true;
            this.labelIpFormat.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelIpFormat.Location = new System.Drawing.Point(375, 258);
            this.labelIpFormat.Name = "labelIpFormat";
            this.labelIpFormat.Size = new System.Drawing.Size(251, 12);
            this.labelIpFormat.TabIndex = 13;
            this.labelIpFormat.Text = "格式：127.0.0.1:1080     使用代理访问网络";
            // 
            // comboBoxHttpMethod
            // 
            this.comboBoxHttpMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHttpMethod.Font = new System.Drawing.Font("微软雅黑", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxHttpMethod.FormattingEnabled = true;
            this.comboBoxHttpMethod.Items.AddRange(new object[] {
            "POST",
            "GET",
            "HEAD",
            "PUT"});
            this.comboBoxHttpMethod.Location = new System.Drawing.Point(14, 210);
            this.comboBoxHttpMethod.Name = "comboBoxHttpMethod";
            this.comboBoxHttpMethod.Size = new System.Drawing.Size(95, 27);
            this.comboBoxHttpMethod.TabIndex = 5;
            this.comboBoxHttpMethod.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // comboBoxEncodeMethod
            // 
            this.comboBoxEncodeMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncodeMethod.Font = new System.Drawing.Font("微软雅黑", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxEncodeMethod.FormattingEnabled = true;
            this.comboBoxEncodeMethod.Items.AddRange(new object[] {
            "输出编码 UTF-8",
            "输出编码 GBK"});
            this.comboBoxEncodeMethod.Location = new System.Drawing.Point(173, 210);
            this.comboBoxEncodeMethod.Name = "comboBoxEncodeMethod";
            this.comboBoxEncodeMethod.Size = new System.Drawing.Size(186, 27);
            this.comboBoxEncodeMethod.TabIndex = 6;
            this.comboBoxEncodeMethod.SelectedIndexChanged += new System.EventHandler(this.ComboBox2_SelectedIndexChanged);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.buttonDelete.Location = new System.Drawing.Point(173, 247);
            this.buttonDelete.Name = "button3";
            this.buttonDelete.Size = new System.Drawing.Size(95, 27);
            this.buttonDelete.TabIndex = 9;
            this.buttonDelete.Text = "清空表单";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSubmit.Location = new System.Drawing.Point(13, 247);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(95, 27);
            this.buttonSubmit.TabIndex = 8;
            this.buttonSubmit.Text = "提交";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.Submit_ClickAsync);
            // 
            // labelUrl
            // 
            this.labelUrl.AutoSize = true;
            this.labelUrl.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelUrl.Location = new System.Drawing.Point(9, 3);
            this.labelUrl.Name = "labelUrl";
            this.labelUrl.Size = new System.Drawing.Size(31, 17);
            this.labelUrl.TabIndex = 23;
            this.labelUrl.Text = "URL";
            // 
            // tabControlResponse
            // 
            this.tabControlResponse.Controls.Add(this.formatResponse);
            this.tabControlResponse.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControlResponse.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.tabControlResponse.Location = new System.Drawing.Point(0, 280);
            this.tabControlResponse.Name = "tabControlResponse";
            this.tabControlResponse.Padding = new System.Drawing.Point(1, 2);
            this.tabControlResponse.SelectedIndex = 0;
            this.tabControlResponse.Size = new System.Drawing.Size(736, 297);
            this.tabControlResponse.TabIndex = 20;
            // 
            // formatResponse
            // 
            this.formatResponse.BackColor = System.Drawing.SystemColors.Control;
            this.formatResponse.Controls.Add(this.richTextBoxResponse);
            this.formatResponse.Location = new System.Drawing.Point(4, 26);
            this.formatResponse.Name = "formatResponse";
            this.formatResponse.Padding = new System.Windows.Forms.Padding(3);
            this.formatResponse.Size = new System.Drawing.Size(728, 267);
            this.formatResponse.TabIndex = 0;
            this.formatResponse.Text = "Response";
            // 
            // richTextBoxResponse
            // 
            this.richTextBoxResponse.BackColor = System.Drawing.Color.White;
            this.richTextBoxResponse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxResponse.Font = new System.Drawing.Font("宋体", 9F);
            this.richTextBoxResponse.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxResponse.Name = "richTextBoxResponse";
            this.richTextBoxResponse.ReadOnly = true;
            this.richTextBoxResponse.Size = new System.Drawing.Size(722, 261);
            this.richTextBoxResponse.TabIndex = 10;
            this.richTextBoxResponse.Text = "";
            // 
            // PostAndGetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 577);
            this.Controls.Add(this.labelUrl);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.tabControlResponse);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.comboBoxEncodeMethod);
            this.Controls.Add(this.comboBoxHttpMethod);
            this.Controls.Add(this.labelIpFormat);
            this.Controls.Add(this.textBoxIp);
            this.Controls.Add(this.labelIp);
            this.Controls.Add(this.labelHeaderFormat);
            this.Controls.Add(this.textBoxHeader);
            this.Controls.Add(this.labelHeader);
            this.Controls.Add(this.labelCookieFormat);
            this.Controls.Add(this.textBoxCookie);
            this.Controls.Add(this.labelCookie);
            this.Controls.Add(this.textBoxPost);
            this.Controls.Add(this.labelPost);
            this.Controls.Add(this.textBoxUrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PostAndGetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POST工具";
            this.tabControlResponse.ResumeLayout(false);
            this.formatResponse.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Label labelPost;
        private System.Windows.Forms.TextBox textBoxPost;
        private System.Windows.Forms.Label labelCookie;
        private System.Windows.Forms.TextBox textBoxCookie;
        private System.Windows.Forms.Label labelCookieFormat;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.TextBox textBoxHeader;
        private System.Windows.Forms.Label labelHeaderFormat;
        private System.Windows.Forms.Label labelIp;
        private System.Windows.Forms.TextBox textBoxIp;
        private System.Windows.Forms.Label labelIpFormat;
        private System.Windows.Forms.ComboBox comboBoxHttpMethod;
        private System.Windows.Forms.ComboBox comboBoxEncodeMethod;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Label labelUrl;
        private System.Windows.Forms.TabControl tabControlResponse;
        private System.Windows.Forms.TabPage formatResponse;
        private System.Windows.Forms.RichTextBox richTextBoxResponse;
    }
}