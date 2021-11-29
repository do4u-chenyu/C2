namespace MD5Plugin
{
    partial class URLlPlugin
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
            this.buttonDecode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonDecode
            // 
            this.buttonDecode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonDecode.Location = new System.Drawing.Point(417, 195);
            this.buttonDecode.Name = "buttonDecode";
            this.buttonDecode.Size = new System.Drawing.Size(75, 30);
            this.buttonDecode.TabIndex = 3;
            this.buttonDecode.Text = "<= 解码";
            this.buttonDecode.UseVisualStyleBackColor = true;
            this.buttonDecode.Click += new System.EventHandler(this.buttonDecode_Click);
            // 
            // URLlPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Controls.Add(this.buttonDecode);
            this.Name = "URLlPlugin";
            this.Size = new System.Drawing.Size(1046, 549);
            this.Controls.SetChildIndex(this.buttonDecode, 0);
            this.Controls.SetChildIndex(this.inputTextBox, 0);
            this.Controls.SetChildIndex(this.outputTextBox, 0);
            this.Controls.SetChildIndex(this.buttonEncode, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
       
        
        public System.Windows.Forms.Button buttonDecode;
    }
}
