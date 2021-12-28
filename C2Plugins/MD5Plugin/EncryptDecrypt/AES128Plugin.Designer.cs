namespace MD5Plugin
{
    partial class AES128Plugin
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
            this.label1 = new System.Windows.Forms.Label();
            this.EncryModeComboBox = new System.Windows.Forms.ComboBox();
            this.IvtextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PaddingcomboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DataBlockComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(426, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "加密模式";
            // 
            // EncryModeComboBox
            // 
            this.EncryModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EncryModeComboBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.EncryModeComboBox.FormattingEnabled = true;
            this.EncryModeComboBox.Items.AddRange(new object[] {
            "ECB",
            "CBC"});
            this.EncryModeComboBox.Location = new System.Drawing.Point(418, 52);
            this.EncryModeComboBox.Name = "EncryModeComboBox";
            this.EncryModeComboBox.Size = new System.Drawing.Size(73, 25);
            this.EncryModeComboBox.TabIndex = 9;
            this.EncryModeComboBox.SelectedIndexChanged += new System.EventHandler(this.Encry_SelectedIndexChanged);
            // 
            // IvtextBox
            // 
            this.IvtextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IvtextBox.ForeColor = System.Drawing.Color.Black;
            this.IvtextBox.Location = new System.Drawing.Point(417, 309);
            this.IvtextBox.Multiline = true;
            this.IvtextBox.Name = "IvtextBox";
            this.IvtextBox.Size = new System.Drawing.Size(74, 21);
            this.IvtextBox.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(434, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "偏移量";
            // 
            // PaddingcomboBox
            // 
            this.PaddingcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PaddingcomboBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PaddingcomboBox.FormattingEnabled = true;
            this.PaddingcomboBox.Items.AddRange(new object[] {
            "Zeros",
            "None",
            "PKCS7",
            "ANSIX923",
            "ISO10126"});
            this.PaddingcomboBox.Location = new System.Drawing.Point(418, 353);
            this.PaddingcomboBox.Name = "PaddingcomboBox";
            this.PaddingcomboBox.Size = new System.Drawing.Size(74, 25);
            this.PaddingcomboBox.TabIndex = 12;
            this.PaddingcomboBox.SelectedIndexChanged += new System.EventHandler(this.Padding_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(438, 333);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "填充";
            // 
            // DataBlockComboBox
            // 
            this.DataBlockComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DataBlockComboBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DataBlockComboBox.FormattingEnabled = true;
            this.DataBlockComboBox.Items.AddRange(new object[] {
            "128位",
            "192位",
            "256位"});
            this.DataBlockComboBox.Location = new System.Drawing.Point(417, 401);
            this.DataBlockComboBox.Name = "DataBlockComboBox";
            this.DataBlockComboBox.Size = new System.Drawing.Size(75, 25);
            this.DataBlockComboBox.TabIndex = 14;
            this.DataBlockComboBox.SelectedIndexChanged += new System.EventHandler(this.DataBlock_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(434, 381);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "数据块";
            // 
            // AES128Plugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DataBlockComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PaddingcomboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IvtextBox);
            this.Controls.Add(this.EncryModeComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelEncryptionkey);
            this.Controls.Add(this.textBoxEncryptionkey);
            this.Name = "AES128Plugin";
            this.Controls.SetChildIndex(this.textBoxEncryptionkey, 0);
            this.Controls.SetChildIndex(this.labelEncryptionkey, 0);
            this.Controls.SetChildIndex(this.encodingComboBox, 0);
            this.Controls.SetChildIndex(this.buttonDecode, 0);
            this.Controls.SetChildIndex(this.inputTextBox, 0);
            this.Controls.SetChildIndex(this.outputTextBox, 0);
            this.Controls.SetChildIndex(this.buttonEncode, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.EncryModeComboBox, 0);
            this.Controls.SetChildIndex(this.IvtextBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.PaddingcomboBox, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.DataBlockComboBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxEncryptionkey;
        private System.Windows.Forms.Label labelEncryptionkey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox EncryModeComboBox;//加密模式
        private System.Windows.Forms.TextBox IvtextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox PaddingcomboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox DataBlockComboBox;//数据块
        private System.Windows.Forms.Label label4;
    }
}
