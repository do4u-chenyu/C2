namespace MD5Plugin
{
    partial class HexPlugin
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
            this.splitComboBox = new System.Windows.Forms.ComboBox();
            this.radixComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // encodingComboBox
            // 
            this.encodingComboBox.Location = new System.Drawing.Point(417, 302);
            // 
            // buttonDecode
            // 
            this.buttonDecode.Location = new System.Drawing.Point(417, 248);
            // 
            // splitComboBox
            // 
            this.splitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.splitComboBox.Font = new System.Drawing.Font("微软雅黑", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.splitComboBox.FormattingEnabled = true;
            this.splitComboBox.Items.AddRange(new object[] {
            "无分隔符",
            "\\X",
            "\\x",
            "#",
            "%",
            "/",
            "\\",
            "0x"});
            this.splitComboBox.Location = new System.Drawing.Point(417, 401);
            this.splitComboBox.Name = "splitComboBox";
            this.splitComboBox.Size = new System.Drawing.Size(75, 27);
            this.splitComboBox.TabIndex = 5;
            this.splitComboBox.SelectedIndexChanged += new System.EventHandler(this.Split_SelectedIndexChanged);
            // 
            // radixComboBox
            // 
            this.radixComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.radixComboBox.Font = new System.Drawing.Font("微软雅黑", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radixComboBox.FormattingEnabled = true;
            this.radixComboBox.Items.AddRange(new object[] {
            "十六进制",
            "十进制",
            "八进制"});
            this.radixComboBox.Location = new System.Drawing.Point(417, 353);
            this.radixComboBox.Name = "radixComboBox";
            this.radixComboBox.Size = new System.Drawing.Size(75, 27);
            this.radixComboBox.TabIndex = 6;
            this.radixComboBox.SelectedIndexChanged += new System.EventHandler(this.RadixComboBox_SelectedIndexChanged);
            // 
            // HexPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Controls.Add(this.radixComboBox);
            this.Controls.Add(this.splitComboBox);
            this.Name = "HexPlugin";
            this.Controls.SetChildIndex(this.encodingComboBox, 0);
            this.Controls.SetChildIndex(this.splitComboBox, 0);
            this.Controls.SetChildIndex(this.radixComboBox, 0);
            this.Controls.SetChildIndex(this.buttonDecode, 0);
            this.Controls.SetChildIndex(this.inputTextBox, 0);
            this.Controls.SetChildIndex(this.outputTextBox, 0);
            this.Controls.SetChildIndex(this.buttonEncode, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
       
       
        
       
        private System.Windows.Forms.ComboBox splitComboBox;
        private System.Windows.Forms.ComboBox radixComboBox;
    }
}
