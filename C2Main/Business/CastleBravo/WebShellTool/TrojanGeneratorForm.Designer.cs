namespace C2.Business.CastleBravo.WebShellTool
{
    partial class TrojanGeneratorForm
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
            this.passTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.trojanComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.encryComboBox = new System.Windows.Forms.ComboBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "连接密码";
            // 
            // passTextBox
            // 
            this.passTextBox.Font = new System.Drawing.Font("微软雅黑", 9.25F);
            this.passTextBox.Location = new System.Drawing.Point(74, 16);
            this.passTextBox.Name = "passTextBox";
            this.passTextBox.Size = new System.Drawing.Size(260, 24);
            this.passTextBox.TabIndex = 1;
            this.passTextBox.Text = "yxs";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(0, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Trojan类型";
            // 
            // trojanComboBox
            // 
            this.trojanComboBox.Enabled = false;
            this.trojanComboBox.Font = new System.Drawing.Font("微软雅黑", 9.25F);
            this.trojanComboBox.FormattingEnabled = true;
            this.trojanComboBox.Location = new System.Drawing.Point(74, 51);
            this.trojanComboBox.Name = "trojanComboBox";
            this.trojanComboBox.Size = new System.Drawing.Size(260, 25);
            this.trojanComboBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label3.Location = new System.Drawing.Point(40, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 10003;
            this.label3.Text = "密钥";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label4.Location = new System.Drawing.Point(159, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 10004;
            this.label4.Text = "加密器";
            // 
            // keyTextBox
            // 
            this.keyTextBox.Enabled = false;
            this.keyTextBox.Font = new System.Drawing.Font("微软雅黑", 9.25F);
            this.keyTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.keyTextBox.Location = new System.Drawing.Point(74, 87);
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.Size = new System.Drawing.Size(78, 24);
            this.keyTextBox.TabIndex = 3;
            this.keyTextBox.Text = "无需配置";
            // 
            // encryComboBox
            // 
            this.encryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.encryComboBox.DropDownWidth = 130;
            this.encryComboBox.Enabled = false;
            this.encryComboBox.Font = new System.Drawing.Font("微软雅黑", 9.25F);
            this.encryComboBox.FormattingEnabled = true;
            this.encryComboBox.Items.AddRange(new object[] {
            "PHP_XOR_RAW",
            "PHP_XOR_BASE64",
            "PHP_EVAL_BASE64"});
            this.encryComboBox.Location = new System.Drawing.Point(204, 86);
            this.encryComboBox.Name = "encryComboBox";
            this.encryComboBox.Size = new System.Drawing.Size(130, 25);
            this.encryComboBox.TabIndex = 4;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "php 文件|*.php|所有文件|*.*";
            // 
            // label5
            // 
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(64, 23);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(9, 8);
            this.label5.TabIndex = 10005;
            this.label5.Text = "*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TrojanGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 171);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.encryComboBox);
            this.Controls.Add(this.keyTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trojanComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.passTextBox);
            this.Controls.Add(this.label1);
            this.Name = "TrojanGeneratorForm";
            this.Text = "Trojan配置";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.passTextBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.trojanComboBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.keyTextBox, 0);
            this.Controls.SetChildIndex(this.encryComboBox, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox trojanComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox keyTextBox;
        private System.Windows.Forms.ComboBox encryComboBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label5;
    }
}