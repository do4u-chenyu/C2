namespace C2.Business.CastleBravo.WebShellTool
{
    partial class InfoCollectionSet
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
            this.useSetComboBox = new System.Windows.Forms.ComboBox();
            this.remoteAddrTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.localAddrTextBox = new System.Windows.Forms.TextBox();
            this.useSet = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.remoteAddr = new System.Windows.Forms.RadioButton();
            this.localAddr = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // useSetComboBox
            // 
            this.useSetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.useSetComboBox.FormattingEnabled = true;
            this.useSetComboBox.Items.AddRange(new object[] {
            "否",
            "是"});
            this.useSetComboBox.Location = new System.Drawing.Point(95, 47);
            this.useSetComboBox.Name = "useSetComboBox";
            this.useSetComboBox.Size = new System.Drawing.Size(205, 20);
            this.useSetComboBox.TabIndex = 10026;
            // 
            // remoteAddrTextBox
            // 
            this.remoteAddrTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.remoteAddrTextBox.Location = new System.Drawing.Point(95, 81);
            this.remoteAddrTextBox.Name = "remoteAddrTextBox";
            this.remoteAddrTextBox.Size = new System.Drawing.Size(205, 21);
            this.remoteAddrTextBox.TabIndex = 10024;
            this.remoteAddrTextBox.Text = "http://xxx.com/";
            this.remoteAddrTextBox.TextChanged += new System.EventHandler(this.PortTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 10023;
            // 
            // localAddrTextBox
            // 
            this.localAddrTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.localAddrTextBox.Location = new System.Drawing.Point(95, 113);
            this.localAddrTextBox.Name = "localAddrTextBox";
            this.localAddrTextBox.Size = new System.Drawing.Size(205, 21);
            this.localAddrTextBox.TabIndex = 10022;
            this.localAddrTextBox.Text = "c:/xxx/";
            // 
            // useSet
            // 
            this.useSet.AutoSize = true;
            this.useSet.Location = new System.Drawing.Point(16, 50);
            this.useSet.Name = "useSet";
            this.useSet.Size = new System.Drawing.Size(41, 12);
            this.useSet.TabIndex = 10021;
            this.useSet.Text = "启用：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(16, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 12);
            this.label4.TabIndex = 10027;
            this.label4.Text = "此设置不保存,用前每次设置一下";
            // 
            // remoteAddr
            // 
            this.remoteAddr.AutoSize = true;
            this.remoteAddr.Checked = true;
            this.remoteAddr.Location = new System.Drawing.Point(12, 86);
            this.remoteAddr.Name = "remoteAddr";
            this.remoteAddr.Size = new System.Drawing.Size(71, 16);
            this.remoteAddr.TabIndex = 10028;
            this.remoteAddr.TabStop = true;
            this.remoteAddr.Text = "远程地址";
            this.remoteAddr.UseVisualStyleBackColor = true;
            // 
            // localAddr
            // 
            this.localAddr.AutoSize = true;
            this.localAddr.Location = new System.Drawing.Point(12, 118);
            this.localAddr.Name = "localAddr";
            this.localAddr.Size = new System.Drawing.Size(71, 16);
            this.localAddr.TabIndex = 10029;
            this.localAddr.Text = "本地地址";
            this.localAddr.UseVisualStyleBackColor = true;
            // 
            // InfoCollectionSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(326, 204);
            this.Controls.Add(this.localAddr);
            this.Controls.Add(this.remoteAddr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.useSetComboBox);
            this.Controls.Add(this.remoteAddrTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.localAddrTextBox);
            this.Controls.Add(this.useSet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "InfoCollectionSet";
            this.Text = "后信息收集设置";
            this.Controls.SetChildIndex(this.useSet, 0);
            this.Controls.SetChildIndex(this.localAddrTextBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.remoteAddrTextBox, 0);
            this.Controls.SetChildIndex(this.useSetComboBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.remoteAddr, 0);
            this.Controls.SetChildIndex(this.localAddr, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox useSetComboBox;
        private System.Windows.Forms.TextBox remoteAddrTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox localAddrTextBox;
        private System.Windows.Forms.Label useSet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton remoteAddr;
        private System.Windows.Forms.RadioButton localAddr;
    }
}