namespace C2.Business.CastleBravo.WebShellTool
{
    partial class ProxySettingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.IPTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.proxyTypeCombox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.enableComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "启用：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 10004;
            this.label2.Text = "IP：";
            // 
            // IPTextBox
            // 
            this.IPTextBox.Location = new System.Drawing.Point(66, 62);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Size = new System.Drawing.Size(205, 21);
            this.IPTextBox.TabIndex = 10003;
            this.IPTextBox.Text = "127.0.0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 10006;
            this.label3.Text = "端口：";
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(66, 97);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(205, 21);
            this.PortTextBox.TabIndex = 10005;
            this.PortTextBox.Text = "10809";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 10010;
            this.label5.Text = "类型：";
            // 
            // proxyTypeCombox
            // 
            this.proxyTypeCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.proxyTypeCombox.Enabled = false;
            this.proxyTypeCombox.FormattingEnabled = true;
            this.proxyTypeCombox.Items.AddRange(new object[] {
            "HTTP"});
            this.proxyTypeCombox.Location = new System.Drawing.Point(66, 132);
            this.proxyTypeCombox.Name = "proxyTypeCombox";
            this.proxyTypeCombox.Size = new System.Drawing.Size(205, 20);
            this.proxyTypeCombox.TabIndex = 10011;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(49, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 10018;
            this.label8.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(49, 102);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 10019;
            this.label9.Text = "*";
            // 
            // enableComboBox
            // 
            this.enableComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enableComboBox.FormattingEnabled = true;
            this.enableComboBox.Items.AddRange(new object[] {
            "否",
            "是"});
            this.enableComboBox.Location = new System.Drawing.Point(66, 28);
            this.enableComboBox.Name = "enableComboBox";
            this.enableComboBox.Size = new System.Drawing.Size(205, 20);
            this.enableComboBox.TabIndex = 10020;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(23, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 12);
            this.label4.TabIndex = 10021;
            this.label4.Text = "代理设置不保存,用前每次设置一下";
            // 
            // ProxySettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(290, 209);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.enableComboBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.proxyTypeCombox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IPTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ProxySettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "代理设置";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.IPTextBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.PortTextBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.proxyTypeCombox, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.enableComboBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox IPTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox proxyTypeCombox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox enableComboBox;
        private System.Windows.Forms.Label label4;
    }
}