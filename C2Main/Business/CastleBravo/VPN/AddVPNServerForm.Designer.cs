
namespace C2.Business.CastleBravo.VPN
{
    partial class AddVPNServerForm
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
            this.encryptComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ssTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.remarkTextBox = new System.Windows.Forms.TextBox();
            this.pwdTextBox = new System.Windows.Forms.TextBox();
            this.versionCombox = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // encryptComboBox
            // 
            this.encryptComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.encryptComboBox.FormattingEnabled = true;
            this.encryptComboBox.Items.AddRange(new object[] {
            "chacha20-ietf-poly1305",
            "xchacha20-ietf-poly1305",
            "chacha20-ietf",
            "rc4-md5",
            "salsa20",
            "chacha20",
            "bf-cfb",
            "aes-256-acm",
            "aes-192-gcm",
            "aes-128-gcm",
            "aes-256-cfb",
            "aes-192-cfb",
            "aes-128-cfb",
            "aes-256-ctr",
            "aes-192-ctr",
            "aes-128-ctr",
            "camellia-256-cfb",
            "camellia-192-cfb",
            "camellia-128-cfb",
            "auto",
            "自定义"});
            this.encryptComboBox.Location = new System.Drawing.Point(136, 226);
            this.encryptComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.encryptComboBox.Name = "encryptComboBox";
            this.encryptComboBox.Size = new System.Drawing.Size(426, 26);
            this.encryptComboBox.TabIndex = 10032;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 232);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 18);
            this.label7.TabIndex = 10031;
            this.label7.Text = "加密算法：";
            // 
            // ssTextBox
            // 
            this.ssTextBox.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ssTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.ssTextBox.Location = new System.Drawing.Point(136, 338);
            this.ssTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.ssTextBox.Multiline = true;
            this.ssTextBox.Name = "ssTextBox";
            this.ssTextBox.Size = new System.Drawing.Size(426, 199);
            this.ssTextBox.TabIndex = 10030;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 338);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 18);
            this.label6.TabIndex = 10029;
            this.label6.Text = "分享地址：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 180);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 18);
            this.label5.TabIndex = 10027;
            this.label5.Text = "密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 10026;
            this.label3.Text = "端口：";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(136, 122);
            this.portTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(426, 28);
            this.portTextBox.TabIndex = 10025;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 10024;
            this.label2.Text = "主机地址：";
            // 
            // hostTextBox
            // 
            this.hostTextBox.Location = new System.Drawing.Point(136, 69);
            this.hostTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(426, 28);
            this.hostTextBox.TabIndex = 10023;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 10022;
            this.label1.Text = "备注：";
            // 
            // remarkTextBox
            // 
            this.remarkTextBox.Location = new System.Drawing.Point(136, 16);
            this.remarkTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.remarkTextBox.Name = "remarkTextBox";
            this.remarkTextBox.Size = new System.Drawing.Size(426, 28);
            this.remarkTextBox.TabIndex = 10021;
            // 
            // pwdTextBox
            // 
            this.pwdTextBox.Location = new System.Drawing.Point(136, 174);
            this.pwdTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.pwdTextBox.Name = "pwdTextBox";
            this.pwdTextBox.Size = new System.Drawing.Size(426, 28);
            this.pwdTextBox.TabIndex = 10038;
            // 
            // versionCombox
            // 
            this.versionCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.versionCombox.FormattingEnabled = true;
            this.versionCombox.Items.AddRange(new object[] {
            "SS",
            "SSR",
            "Vmess",
            "Vless",
            "Trojan",
            "自定义"});
            this.versionCombox.Location = new System.Drawing.Point(136, 279);
            this.versionCombox.Margin = new System.Windows.Forms.Padding(4);
            this.versionCombox.Name = "versionCombox";
            this.versionCombox.Size = new System.Drawing.Size(426, 26);
            this.versionCombox.TabIndex = 10040;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(2, 285);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(116, 18);
            this.label12.TabIndex = 10039;
            this.label12.Text = "服务端类型：";
            // 
            // AddVPNServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 614);
            this.Controls.Add(this.versionCombox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.pwdTextBox);
            this.Controls.Add(this.encryptComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ssTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hostTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.remarkTextBox);
            this.Name = "AddVPNServerForm";
            this.Text = "添加VPN服务器";
            this.Controls.SetChildIndex(this.remarkTextBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.hostTextBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.portTextBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.ssTextBox, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.encryptComboBox, 0);
            this.Controls.SetChildIndex(this.pwdTextBox, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.versionCombox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox encryptComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ssTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox remarkTextBox;
        private System.Windows.Forms.TextBox pwdTextBox;
        private System.Windows.Forms.ComboBox versionCombox;
        private System.Windows.Forms.Label label12;
    }
}