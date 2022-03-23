namespace MD5Plugin
{
    partial class RC4Plugin
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxEncryptionkey = new System.Windows.Forms.TextBox();
            this.labelEncryptionkey = new System.Windows.Forms.Label();
            this.EncryModeComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonDecode
            // 
            this.buttonDecode.Text = "<= 解密";
            // 
            // inputTextBox
            // 
            this.inputTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.inputTextBox.Size = new System.Drawing.Size(611, 1091);
            // 
            // outputTextBox
            // 
            this.outputTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.outputTextBox.Size = new System.Drawing.Size(611, 1091);
            // 
            // buttonEncode
            // 
            this.buttonEncode.Text = "加密 =>";
            // 
            // textBoxEncryptionkey
            // 
            this.textBoxEncryptionkey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxEncryptionkey.ForeColor = System.Drawing.Color.Black;
            this.textBoxEncryptionkey.Location = new System.Drawing.Point(626, 150);
            this.textBoxEncryptionkey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxEncryptionkey.Name = "textBoxEncryptionkey";
            this.textBoxEncryptionkey.Size = new System.Drawing.Size(110, 28);
            this.textBoxEncryptionkey.TabIndex = 5;
            // 
            // labelEncryptionkey
            // 
            this.labelEncryptionkey.AutoSize = true;
            this.labelEncryptionkey.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelEncryptionkey.Location = new System.Drawing.Point(657, 120);
            this.labelEncryptionkey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEncryptionkey.Name = "labelEncryptionkey";
            this.labelEncryptionkey.Size = new System.Drawing.Size(46, 24);
            this.labelEncryptionkey.TabIndex = 6;
            this.labelEncryptionkey.Text = "密钥";
            // 
            // EncryModeComboBox
            // 
            this.EncryModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EncryModeComboBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.EncryModeComboBox.FormattingEnabled = true;
            this.EncryModeComboBox.Items.AddRange(new object[] {
            "文本",
            "HEX"});
            this.EncryModeComboBox.Location = new System.Drawing.Point(627, 78);
            this.EncryModeComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.EncryModeComboBox.Name = "EncryModeComboBox";
            this.EncryModeComboBox.Size = new System.Drawing.Size(108, 32);
            this.EncryModeComboBox.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(639, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 11;
            this.label1.Text = "密钥类型";
            // 
            // RC4Plugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EncryModeComboBox);
            this.Controls.Add(this.labelEncryptionkey);
            this.Controls.Add(this.textBoxEncryptionkey);
            this.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
            this.Name = "RC4Plugin";
            this.Controls.SetChildIndex(this.textBoxEncryptionkey, 0);
            this.Controls.SetChildIndex(this.labelEncryptionkey, 0);
            this.Controls.SetChildIndex(this.encodingComboBox, 0);
            this.Controls.SetChildIndex(this.buttonDecode, 0);
            this.Controls.SetChildIndex(this.inputTextBox, 0);
            this.Controls.SetChildIndex(this.outputTextBox, 0);
            this.Controls.SetChildIndex(this.buttonEncode, 0);
            this.Controls.SetChildIndex(this.EncryModeComboBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxEncryptionkey;
        private System.Windows.Forms.Label labelEncryptionkey;
        private System.Windows.Forms.ComboBox EncryModeComboBox;
        private System.Windows.Forms.Label label1;
    }
}
