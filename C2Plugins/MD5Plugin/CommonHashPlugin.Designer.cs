namespace MD5Plugin
{
    partial class CommonHashPlugin
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
            this.encodeTypeCB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // encodeTypeCB
            // 
            this.encodeTypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.encodeTypeCB.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.encodeTypeCB.FormattingEnabled = true;
            this.encodeTypeCB.Items.AddRange(new object[] {
            "文本",
            "HEX"});
            this.encodeTypeCB.Location = new System.Drawing.Point(417, 195);
            this.encodeTypeCB.Name = "encodeTypeCB";
            this.encodeTypeCB.Size = new System.Drawing.Size(75, 27);
            this.encodeTypeCB.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(414, 281);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "移步 喝彩城堡";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(414, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "解密实为撞库";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(412, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "MD5本身不可逆";
            // 
            // CommonHashPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.encodeTypeCB);
            this.Name = "CommonHashPlugin";
            this.Controls.SetChildIndex(this.inputTextBox, 0);
            this.Controls.SetChildIndex(this.outputTextBox, 0);
            this.Controls.SetChildIndex(this.buttonEncode, 0);
            this.Controls.SetChildIndex(this.encodeTypeCB, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.ComboBox encodeTypeCB;
    }
}
