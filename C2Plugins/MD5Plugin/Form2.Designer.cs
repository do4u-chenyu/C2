namespace MD5Plugin
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.urlRadioButton = new System.Windows.Forms.RadioButton();
            this.md5128RadioButton = new System.Windows.Forms.RadioButton();
            this.md564RadioButton = new System.Windows.Forms.RadioButton();
            this.base64RadioButton = new System.Windows.Forms.RadioButton();
            this.unicodeRadioButton = new System.Windows.Forms.RadioButton();
            this.hexRadioButton = new System.Windows.Forms.RadioButton();
            this.ASE128RadioButton = new System.Windows.Forms.RadioButton();
            this.sha1RadioButton = new System.Windows.Forms.RadioButton();
            this.sha512RadioButton = new System.Windows.Forms.RadioButton();
            this.sha256RadioButton = new System.Windows.Forms.RadioButton();
            this.NTLMRadioButton = new System.Windows.Forms.RadioButton();
            this.commonPlugin = new MD5Plugin.CommonPlugin();
            this.SuspendLayout();
            // 
            // urlRadioButton
            // 
            this.urlRadioButton.AutoSize = true;
            this.urlRadioButton.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.urlRadioButton.Location = new System.Drawing.Point(310, 31);
            this.urlRadioButton.Name = "urlRadioButton";
            this.urlRadioButton.Size = new System.Drawing.Size(88, 18);
            this.urlRadioButton.TabIndex = 1;
            this.urlRadioButton.TabStop = true;
            this.urlRadioButton.Text = "Url编解码";
            this.urlRadioButton.UseVisualStyleBackColor = true;
            this.urlRadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // md5128RadioButton
            // 
            this.md5128RadioButton.AutoSize = true;
            this.md5128RadioButton.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.md5128RadioButton.Location = new System.Drawing.Point(26, 31);
            this.md5128RadioButton.Name = "md5128RadioButton";
            this.md5128RadioButton.Size = new System.Drawing.Size(67, 18);
            this.md5128RadioButton.TabIndex = 2;
            this.md5128RadioButton.TabStop = true;
            this.md5128RadioButton.Text = "MD5128";
            this.md5128RadioButton.UseVisualStyleBackColor = true;
            this.md5128RadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // md564RadioButton
            // 
            this.md564RadioButton.AutoSize = true;
            this.md564RadioButton.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.md564RadioButton.Location = new System.Drawing.Point(113, 31);
            this.md564RadioButton.Name = "md564RadioButton";
            this.md564RadioButton.Size = new System.Drawing.Size(60, 18);
            this.md564RadioButton.TabIndex = 3;
            this.md564RadioButton.TabStop = true;
            this.md564RadioButton.Text = "MD564";
            this.md564RadioButton.UseVisualStyleBackColor = true;
            this.md564RadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // base64RadioButton
            // 
            this.base64RadioButton.AutoSize = true;
            this.base64RadioButton.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.base64RadioButton.Location = new System.Drawing.Point(195, 31);
            this.base64RadioButton.Name = "base64RadioButton";
            this.base64RadioButton.Size = new System.Drawing.Size(95, 18);
            this.base64RadioButton.TabIndex = 4;
            this.base64RadioButton.TabStop = true;
            this.base64RadioButton.Text = "超级Base64";
            this.base64RadioButton.UseVisualStyleBackColor = true;
            this.base64RadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // unicodeRadioButton
            // 
            this.unicodeRadioButton.AutoSize = true;
            this.unicodeRadioButton.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.unicodeRadioButton.Location = new System.Drawing.Point(414, 31);
            this.unicodeRadioButton.Name = "unicodeRadioButton";
            this.unicodeRadioButton.Size = new System.Drawing.Size(116, 18);
            this.unicodeRadioButton.TabIndex = 5;
            this.unicodeRadioButton.TabStop = true;
            this.unicodeRadioButton.Text = "Unicode编解码";
            this.unicodeRadioButton.UseVisualStyleBackColor = true;
            this.unicodeRadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // hexRadioButton
            // 
            this.hexRadioButton.AutoSize = true;
            this.hexRadioButton.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hexRadioButton.Location = new System.Drawing.Point(545, 31);
            this.hexRadioButton.Name = "hexRadioButton";
            this.hexRadioButton.Size = new System.Drawing.Size(88, 18);
            this.hexRadioButton.TabIndex = 6;
            this.hexRadioButton.TabStop = true;
            this.hexRadioButton.Text = "Hex编解码";
            this.hexRadioButton.UseVisualStyleBackColor = true;
            this.hexRadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // ASE128RadioButton
            // 
            this.ASE128RadioButton.AutoSize = true;
            this.ASE128RadioButton.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ASE128RadioButton.Location = new System.Drawing.Point(652, 31);
            this.ASE128RadioButton.Name = "ASE128RadioButton";
            this.ASE128RadioButton.Size = new System.Drawing.Size(67, 18);
            this.ASE128RadioButton.TabIndex = 7;
            this.ASE128RadioButton.TabStop = true;
            this.ASE128RadioButton.Text = "AES128";
            this.ASE128RadioButton.UseVisualStyleBackColor = true;
            this.ASE128RadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // sha1RadioButton
            // 
            this.sha1RadioButton.AutoSize = true;
            this.sha1RadioButton.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sha1RadioButton.Location = new System.Drawing.Point(735, 31);
            this.sha1RadioButton.Name = "sha1RadioButton";
            this.sha1RadioButton.Size = new System.Drawing.Size(53, 18);
            this.sha1RadioButton.TabIndex = 8;
            this.sha1RadioButton.TabStop = true;
            this.sha1RadioButton.Text = "SHA1";
            this.sha1RadioButton.UseVisualStyleBackColor = true;
            this.sha1RadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // sha512RadioButton
            // 
            this.sha512RadioButton.AutoSize = true;
            this.sha512RadioButton.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sha512RadioButton.Location = new System.Drawing.Point(886, 31);
            this.sha512RadioButton.Name = "sha512RadioButton";
            this.sha512RadioButton.Size = new System.Drawing.Size(67, 18);
            this.sha512RadioButton.TabIndex = 9;
            this.sha512RadioButton.TabStop = true;
            this.sha512RadioButton.Text = "SHA512";
            this.sha512RadioButton.UseVisualStyleBackColor = true;
            this.sha512RadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // sha256RadioButton
            // 
            this.sha256RadioButton.AutoSize = true;
            this.sha256RadioButton.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sha256RadioButton.Location = new System.Drawing.Point(810, 31);
            this.sha256RadioButton.Name = "sha256RadioButton";
            this.sha256RadioButton.Size = new System.Drawing.Size(67, 18);
            this.sha256RadioButton.TabIndex = 10;
            this.sha256RadioButton.TabStop = true;
            this.sha256RadioButton.Text = "SHA256";
            this.sha256RadioButton.UseVisualStyleBackColor = true;
            this.sha256RadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // NTLMRadioButton
            // 
            this.NTLMRadioButton.AutoSize = true;
            this.NTLMRadioButton.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NTLMRadioButton.Location = new System.Drawing.Point(975, 31);
            this.NTLMRadioButton.Name = "NTLMRadioButton";
            this.NTLMRadioButton.Size = new System.Drawing.Size(53, 18);
            this.NTLMRadioButton.TabIndex = 11;
            this.NTLMRadioButton.TabStop = true;
            this.NTLMRadioButton.Text = "NTLM";
            this.NTLMRadioButton.UseVisualStyleBackColor = true;
            this.NTLMRadioButton.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // commonPlugin1
            // 
            this.commonPlugin.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.commonPlugin.Location = new System.Drawing.Point(12, 55);
            this.commonPlugin.Name = "commonPlugin1";
            this.commonPlugin.Size = new System.Drawing.Size(1046, 549);
            this.commonPlugin.TabIndex = 12;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1072, 576);
            this.Controls.Add(this.commonPlugin);
            this.Controls.Add(this.NTLMRadioButton);
            this.Controls.Add(this.sha256RadioButton);
            this.Controls.Add(this.sha512RadioButton);
            this.Controls.Add(this.sha1RadioButton);
            this.Controls.Add(this.ASE128RadioButton);
            this.Controls.Add(this.hexRadioButton);
            this.Controls.Add(this.unicodeRadioButton);
            this.Controls.Add(this.base64RadioButton);
            this.Controls.Add(this.md564RadioButton);
            this.Controls.Add(this.md5128RadioButton);
            this.Controls.Add(this.urlRadioButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "加密解密";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        
        #endregion
        private System.Windows.Forms.RadioButton urlRadioButton;
        private System.Windows.Forms.RadioButton md5128RadioButton;
        private System.Windows.Forms.RadioButton md564RadioButton;
        private System.Windows.Forms.RadioButton base64RadioButton;
        private System.Windows.Forms.RadioButton unicodeRadioButton;
        private System.Windows.Forms.RadioButton hexRadioButton;
        private System.Windows.Forms.RadioButton ASE128RadioButton;
        private System.Windows.Forms.RadioButton sha1RadioButton;
        private System.Windows.Forms.RadioButton sha512RadioButton;
        private System.Windows.Forms.RadioButton sha256RadioButton;
        private System.Windows.Forms.RadioButton NTLMRadioButton;
        private CommonPlugin commonPlugin;
        //private UserInstallSet.MyInstaller myInstaller1;
    }
}