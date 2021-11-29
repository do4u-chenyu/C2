namespace MD5Plugin.DecimalConvert
{
    partial class OctDecimal
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
            this.sepComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // sepComboBox
            // 
            this.sepComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sepComboBox.DropDownWidth = 75;
            this.sepComboBox.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.sepComboBox.FormattingEnabled = true;
            this.sepComboBox.Items.AddRange(new object[] {
            "空格分割",
            "|",
            "#",
            ",",
            "-",
            "?",
            ":",
            "&",
            "%",
            "=",
            "+",
            "$",
            "."});
            this.sepComboBox.Location = new System.Drawing.Point(417, 245);
            this.sepComboBox.Name = "sepComboBox";
            this.sepComboBox.Size = new System.Drawing.Size(75, 27);
            this.sepComboBox.TabIndex = 4;
            this.sepComboBox.SelectedIndexChanged += new System.EventHandler(this.ModelComboBox_SelectedIndexChanged);
            // 
            // OctDecimal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sepComboBox);
            this.Name = "OctDecimal";
            this.Controls.SetChildIndex(this.buttonDecode, 0);
            this.Controls.SetChildIndex(this.inputTextBox, 0);
            this.Controls.SetChildIndex(this.outputTextBox, 0);
            this.Controls.SetChildIndex(this.buttonEncode, 0);
            this.Controls.SetChildIndex(this.sepComboBox, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox sepComboBox;
    }
}
