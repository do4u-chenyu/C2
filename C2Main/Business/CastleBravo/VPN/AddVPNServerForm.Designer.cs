
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
            this.sampleLabel = new System.Windows.Forms.LinkLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.encryptComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.modelConfigTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.remarkTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pwdTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.versionCombox = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sampleLabel
            // 
            this.sampleLabel.AutoSize = true;
            this.sampleLabel.Location = new System.Drawing.Point(18, 248);
            this.sampleLabel.Name = "sampleLabel";
            this.sampleLabel.Size = new System.Drawing.Size(53, 12);
            this.sampleLabel.TabIndex = 10035;
            this.sampleLabel.TabStop = true;
            this.sampleLabel.Text = "配置样例";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(74, 85);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 10034;
            this.label9.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(74, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 10033;
            this.label8.Text = "*";
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
            "camellia-128-cfb"});
            this.encryptComboBox.Location = new System.Drawing.Point(91, 151);
            this.encryptComboBox.Name = "encryptComboBox";
            this.encryptComboBox.Size = new System.Drawing.Size(285, 20);
            this.encryptComboBox.TabIndex = 10032;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 155);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 10031;
            this.label7.Text = "加密算法：";
            // 
            // modelConfigTextBox
            // 
            this.modelConfigTextBox.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.modelConfigTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.modelConfigTextBox.Location = new System.Drawing.Point(91, 225);
            this.modelConfigTextBox.Multiline = true;
            this.modelConfigTextBox.Name = "modelConfigTextBox";
            this.modelConfigTextBox.Size = new System.Drawing.Size(285, 134);
            this.modelConfigTextBox.TabIndex = 10030;
            this.modelConfigTextBox.Text = "主机地址:10.1.126.4\r\n端口:8388\r\n密码:barfoo!\r\n加密方式:chacha20-ietf-poly1305\r\n客户端类型:SS\r\n";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10029;
            this.label6.Text = "添加样例：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 10027;
            this.label5.Text = "密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 10026;
            this.label3.Text = "端口：";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(91, 81);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(285, 21);
            this.portTextBox.TabIndex = 10025;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 10024;
            this.label2.Text = "主机地址：";
            // 
            // ipTextBox
            // 
            this.ipTextBox.Location = new System.Drawing.Point(91, 46);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(285, 21);
            this.ipTextBox.TabIndex = 10023;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 10022;
            this.label1.Text = "备注：";
            // 
            // remarkTextBox
            // 
            this.remarkTextBox.Location = new System.Drawing.Point(91, 11);
            this.remarkTextBox.Name = "remarkTextBox";
            this.remarkTextBox.Size = new System.Drawing.Size(285, 21);
            this.remarkTextBox.TabIndex = 10021;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(74, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 10036;
            this.label4.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(74, 155);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 10037;
            this.label10.Text = "*";
            // 
            // pwdTextBox
            // 
            this.pwdTextBox.Location = new System.Drawing.Point(91, 116);
            this.pwdTextBox.Name = "pwdTextBox";
            this.pwdTextBox.Size = new System.Drawing.Size(285, 21);
            this.pwdTextBox.TabIndex = 10038;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(74, 190);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 10041;
            this.label11.Text = "*";
            // 
            // versionCombox
            // 
            this.versionCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.versionCombox.FormattingEnabled = true;
            this.versionCombox.Items.AddRange(new object[] {
            "SS",
            "SSR",
            "V2rayN"});
            this.versionCombox.Location = new System.Drawing.Point(91, 186);
            this.versionCombox.Name = "versionCombox";
            this.versionCombox.Size = new System.Drawing.Size(285, 20);
            this.versionCombox.TabIndex = 10040;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1, 190);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 12);
            this.label12.TabIndex = 10039;
            this.label12.Text = "客户端类型：";
            // 
            // AddVPNServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 409);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.versionCombox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.pwdTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sampleLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.encryptComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.modelConfigTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.remarkTextBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AddVPNServerForm";
            this.Text = "添加VPN服务器";
            this.Controls.SetChildIndex(this.remarkTextBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.ipTextBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.portTextBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.modelConfigTextBox, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.encryptComboBox, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.sampleLabel, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.pwdTextBox, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.versionCombox, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel sampleLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox encryptComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox modelConfigTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox remarkTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox pwdTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox versionCombox;
        private System.Windows.Forms.Label label12;
    }
}