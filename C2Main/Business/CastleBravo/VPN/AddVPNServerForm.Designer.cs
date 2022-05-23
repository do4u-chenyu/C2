
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
            this.methodCB = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ssTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.portTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.hostTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.remarkTB = new System.Windows.Forms.TextBox();
            this.pwdTB = new System.Windows.Forms.TextBox();
            this.versionCB = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ipCountryTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // methodCB
            // 
            this.methodCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.methodCB.FormattingEnabled = true;
            this.methodCB.Items.AddRange(new object[] {
            "自定义",
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
            "auto"});
            this.methodCB.Location = new System.Drawing.Point(136, 212);
            this.methodCB.Margin = new System.Windows.Forms.Padding(4);
            this.methodCB.Name = "methodCB";
            this.methodCB.Size = new System.Drawing.Size(426, 26);
            this.methodCB.TabIndex = 10032;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 214);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 18);
            this.label7.TabIndex = 10031;
            this.label7.Text = "加密算法：";
            // 
            // ssTextBox
            // 
            this.ssTextBox.Font = new System.Drawing.Font("宋体", 8.75F);
            this.ssTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.ssTextBox.Location = new System.Drawing.Point(136, 359);
            this.ssTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.ssTextBox.Multiline = true;
            this.ssTextBox.Name = "ssTextBox";
            this.ssTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ssTextBox.Size = new System.Drawing.Size(426, 238);
            this.ssTextBox.TabIndex = 10030;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 358);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 18);
            this.label6.TabIndex = 10029;
            this.label6.Text = "其他：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 166);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 18);
            this.label5.TabIndex = 10027;
            this.label5.Text = "密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 118);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 10026;
            this.label3.Text = "端口：";
            // 
            // portTB
            // 
            this.portTB.Location = new System.Drawing.Point(136, 114);
            this.portTB.Margin = new System.Windows.Forms.Padding(4);
            this.portTB.Name = "portTB";
            this.portTB.Size = new System.Drawing.Size(426, 28);
            this.portTB.TabIndex = 10025;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 10024;
            this.label2.Text = "主机地址：";
            // 
            // hostTB
            // 
            this.hostTB.Location = new System.Drawing.Point(136, 65);
            this.hostTB.Margin = new System.Windows.Forms.Padding(4);
            this.hostTB.Name = "hostTB";
            this.hostTB.Size = new System.Drawing.Size(426, 28);
            this.hostTB.TabIndex = 10023;
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
            // remarkTB
            // 
            this.remarkTB.Location = new System.Drawing.Point(136, 16);
            this.remarkTB.Margin = new System.Windows.Forms.Padding(4);
            this.remarkTB.Name = "remarkTB";
            this.remarkTB.Size = new System.Drawing.Size(426, 28);
            this.remarkTB.TabIndex = 10021;
            // 
            // pwdTB
            // 
            this.pwdTB.Location = new System.Drawing.Point(136, 163);
            this.pwdTB.Margin = new System.Windows.Forms.Padding(4);
            this.pwdTB.Name = "pwdTB";
            this.pwdTB.Size = new System.Drawing.Size(426, 28);
            this.pwdTB.TabIndex = 10038;
            // 
            // versionCB
            // 
            this.versionCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.versionCB.FormattingEnabled = true;
            this.versionCB.Items.AddRange(new object[] {
            "自定义",
            "SS",
            "SSR",
            "VMESS",
            "VLESS",
            "TROJAN"});
            this.versionCB.Location = new System.Drawing.Point(136, 259);
            this.versionCB.Margin = new System.Windows.Forms.Padding(4);
            this.versionCB.Name = "versionCB";
            this.versionCB.Size = new System.Drawing.Size(426, 26);
            this.versionCB.TabIndex = 10040;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(2, 262);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(116, 18);
            this.label12.TabIndex = 10039;
            this.label12.Text = "服务端类型：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 310);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 10041;
            this.label4.Text = "IP归属地：";
            // 
            // ipCountryTB
            // 
            this.ipCountryTB.Location = new System.Drawing.Point(136, 307);
            this.ipCountryTB.Margin = new System.Windows.Forms.Padding(4);
            this.ipCountryTB.Name = "ipCountryTB";
            this.ipCountryTB.ReadOnly = true;
            this.ipCountryTB.Size = new System.Drawing.Size(426, 28);
            this.ipCountryTB.TabIndex = 10042;
            // 
            // AddVPNServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 674);
            this.Controls.Add(this.ipCountryTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.versionCB);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.pwdTB);
            this.Controls.Add(this.methodCB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ssTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.portTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hostTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.remarkTB);
            this.Name = "AddVPNServerForm";
            this.Text = "添加VPN服务器";
            this.Controls.SetChildIndex(this.remarkTB, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.hostTB, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.portTB, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.ssTextBox, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.methodCB, 0);
            this.Controls.SetChildIndex(this.pwdTB, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.versionCB, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.ipCountryTB, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox methodCB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ssTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox portTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox hostTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox remarkTB;
        private System.Windows.Forms.TextBox pwdTB;
        private System.Windows.Forms.ComboBox versionCB;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ipCountryTB;
    }
}