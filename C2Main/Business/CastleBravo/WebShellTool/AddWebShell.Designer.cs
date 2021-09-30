namespace C2.Business.CastleBravo.WebShellTool
{
    partial class AddWebShell
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
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pwdTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.remarkTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.typeCombo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.advancedTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(87, 37);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(270, 21);
            this.NameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 10004;
            this.label2.Text = "Url：";
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(87, 71);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(270, 21);
            this.urlTextBox.TabIndex = 10003;
            this.urlTextBox.Text = "http://";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 10006;
            this.label3.Text = "密码：";
            // 
            // pwdTextBox
            // 
            this.pwdTextBox.Location = new System.Drawing.Point(87, 137);
            this.pwdTextBox.Name = "pwdTextBox";
            this.pwdTextBox.Size = new System.Drawing.Size(270, 21);
            this.pwdTextBox.TabIndex = 10005;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 10008;
            this.label4.Text = "备注：";
            // 
            // remarkTextBox
            // 
            this.remarkTextBox.Location = new System.Drawing.Point(87, 169);
            this.remarkTextBox.Name = "remarkTextBox";
            this.remarkTextBox.Size = new System.Drawing.Size(270, 21);
            this.remarkTextBox.TabIndex = 10007;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 10010;
            this.label5.Text = "类型：";
            // 
            // typeCombo
            // 
            this.typeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeCombo.FormattingEnabled = true;
            this.typeCombo.Items.AddRange(new object[] {
            "phpEval"});
            this.typeCombo.Location = new System.Drawing.Point(87, 105);
            this.typeCombo.Name = "typeCombo";
            this.typeCombo.Size = new System.Drawing.Size(270, 20);
            this.typeCombo.TabIndex = 10011;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 10013;
            this.label6.Text = "高级：";
            // 
            // advancedTextBox
            // 
            this.advancedTextBox.Location = new System.Drawing.Point(87, 204);
            this.advancedTextBox.Multiline = true;
            this.advancedTextBox.Name = "advancedTextBox";
            this.advancedTextBox.Size = new System.Drawing.Size(270, 134);
            this.advancedTextBox.TabIndex = 10014;
            // 
            // AddWebShell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(397, 419);
            this.Controls.Add(this.advancedTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.typeCombo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.remarkTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pwdTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.urlTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NameTextBox);
            this.Name = "AddWebShell";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebShell Setting";
            this.Controls.SetChildIndex(this.NameTextBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.urlTextBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.pwdTextBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.remarkTextBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.typeCombo, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.advancedTextBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pwdTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox remarkTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox typeCombo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox advancedTextBox;
    }
}