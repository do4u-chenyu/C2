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

            this.encodingComboBox = new System.Windows.Forms.ComboBox();
            this.textBoxEncryptionkey = new System.Windows.Forms.TextBox();
            this.labelEncryptionkey = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // encodingComboBox
            // 
            
            this.encodingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.encodingComboBox.Font = new System.Drawing.Font("微软雅黑", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.encodingComboBox.FormattingEnabled = true;
            this.encodingComboBox.Items.AddRange(new object[] {
            "UTF-8",
            "GB2312",
            "HEX"});
            this.encodingComboBox.Location = new System.Drawing.Point(483, 310);
            this.encodingComboBox.Name = "encodingComboBox";
            this.encodingComboBox.Size = new System.Drawing.Size(75, 27);
            this.encodingComboBox.TabIndex = 4;
            this.encodingComboBox.SelectedIndexChanged += new System.EventHandler(this.ModelComboBox_SelectedIndexChanged);
            
            // 
            // textBoxEncryptionkey
            // 
            this.textBoxEncryptionkey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxEncryptionkey.ForeColor = System.Drawing.Color.Black;
            this.textBoxEncryptionkey.Location = new System.Drawing.Point(483, 148);
            this.textBoxEncryptionkey.Name = "textBoxEncryptionkey";
            this.textBoxEncryptionkey.Size = new System.Drawing.Size(74, 21);
            this.textBoxEncryptionkey.TabIndex = 5;
            // 
            // labelEncryptionkey
            // 
            this.labelEncryptionkey.AutoSize = true;
            this.labelEncryptionkey.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelEncryptionkey.Location = new System.Drawing.Point(503, 128);
            this.labelEncryptionkey.Name = "labelEncryptionkey";
            this.labelEncryptionkey.Size = new System.Drawing.Size(32, 17);
            this.labelEncryptionkey.TabIndex = 6;
            this.labelEncryptionkey.Text = "密钥";
            // 
            // AES128Plugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Controls.Add(this.labelEncryptionkey);
            this.Controls.Add(this.textBoxEncryptionkey);
            this.Controls.Add(this.encodingComboBox);
         
            this.Name = "AES128Plugin";
            this.Size = new System.Drawing.Size(1046, 549);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private new System.Windows.Forms.ComboBox encodingComboBox;
        private System.Windows.Forms.TextBox textBoxEncryptionkey;
        private System.Windows.Forms.Label labelEncryptionkey;
    }
}
