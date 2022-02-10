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
            this.label4 = new System.Windows.Forms.Label();
            this.multlineCB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // inputTextBox
            // 
            this.inputTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.inputTextBox.Size = new System.Drawing.Size(611, 1091);
            // 
            // outputTextBox
            // 
            this.outputTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.outputTextBox.Size = new System.Drawing.Size(611, 1091);
            // 
            // encodeTypeCB
            // 
            this.encodeTypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.encodeTypeCB.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.encodeTypeCB.FormattingEnabled = true;
            this.encodeTypeCB.Items.AddRange(new object[] {
            "文本",
            "HEX"});
            this.encodeTypeCB.Location = new System.Drawing.Point(626, 292);
            this.encodeTypeCB.Margin = new System.Windows.Forms.Padding(4);
            this.encodeTypeCB.Name = "encodeTypeCB";
            this.encodeTypeCB.Size = new System.Drawing.Size(110, 33);
            this.encodeTypeCB.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(616, 422);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = "移步 喝彩城堡";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(621, 387);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 18);
            this.label2.TabIndex = 10;
            this.label2.Text = "解密实为撞库";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(618, 352);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "MD5本身不可逆";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(622, 454);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 18);
            this.label4.TabIndex = 12;
            this.label4.Text = ">>HASH彩虹表";
            // 
            // multlineCB
            // 
            this.multlineCB.AutoSize = true;
            this.multlineCB.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.multlineCB.Location = new System.Drawing.Point(623, 169);
            this.multlineCB.Name = "multlineCB";
            this.multlineCB.Size = new System.Drawing.Size(114, 29);
            this.multlineCB.TabIndex = 13;
            this.multlineCB.Text = "多行模式";
            this.multlineCB.UseVisualStyleBackColor = true;
            // 
            // CommonHashPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.multlineCB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.encodeTypeCB);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "CommonHashPlugin";
            this.Controls.SetChildIndex(this.inputTextBox, 0);
            this.Controls.SetChildIndex(this.outputTextBox, 0);
            this.Controls.SetChildIndex(this.buttonEncode, 0);
            this.Controls.SetChildIndex(this.encodeTypeCB, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.multlineCB, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.Label label3;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.ComboBox encodeTypeCB;
        protected System.Windows.Forms.Label label4;
        protected System.Windows.Forms.CheckBox multlineCB;
    }
}
