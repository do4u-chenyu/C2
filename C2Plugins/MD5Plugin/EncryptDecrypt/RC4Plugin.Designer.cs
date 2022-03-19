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
            this.SuspendLayout();
            // 
            // buttonDecode
            // 
            this.buttonDecode.Text = "<= 解密";
            // 
            // inputTextBox
            // 
            this.inputTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.inputTextBox.Size = new System.Drawing.Size(408, 728);
            // 
            // outputTextBox
            // 
            this.outputTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.outputTextBox.Size = new System.Drawing.Size(408, 728);
            // 
            // buttonEncode
            // 
            this.buttonEncode.Text = "加密 =>";
            // 
            // textBoxEncryptionkey
            // 
            this.textBoxEncryptionkey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxEncryptionkey.ForeColor = System.Drawing.Color.Black;
            this.textBoxEncryptionkey.Location = new System.Drawing.Point(417, 100);
            this.textBoxEncryptionkey.Name = "textBoxEncryptionkey";
            this.textBoxEncryptionkey.Size = new System.Drawing.Size(74, 21);
            this.textBoxEncryptionkey.TabIndex = 5;
            // 
            // labelEncryptionkey
            // 
            this.labelEncryptionkey.AutoSize = true;
            this.labelEncryptionkey.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelEncryptionkey.Location = new System.Drawing.Point(438, 80);
            this.labelEncryptionkey.Name = "labelEncryptionkey";
            this.labelEncryptionkey.Size = new System.Drawing.Size(32, 17);
            this.labelEncryptionkey.TabIndex = 6;
            this.labelEncryptionkey.Text = "密钥";
            // 
            // RC4Plugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Controls.Add(this.labelEncryptionkey);
            this.Controls.Add(this.textBoxEncryptionkey);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "RC4Plugin";
            this.Controls.SetChildIndex(this.textBoxEncryptionkey, 0);
            this.Controls.SetChildIndex(this.labelEncryptionkey, 0);
            this.Controls.SetChildIndex(this.encodingComboBox, 0);
            this.Controls.SetChildIndex(this.buttonDecode, 0);
            this.Controls.SetChildIndex(this.inputTextBox, 0);
            this.Controls.SetChildIndex(this.outputTextBox, 0);
            this.Controls.SetChildIndex(this.buttonEncode, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxEncryptionkey;
        private System.Windows.Forms.Label labelEncryptionkey;
    }
}
