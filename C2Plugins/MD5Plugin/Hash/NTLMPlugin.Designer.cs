namespace MD5Plugin
{
    partial class NTLMPlugin
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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(420, 235);
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.Text = "NTLM不可逆";
            this.label1.Visible = false;
            //
            // label2
            //
            this.label2.Visible = false;
            //
            // label3
            //
            this.label3.Visible = false;
            //
            // label4
            //
            this.label4.Visible = false;

            // 
            // encodeTypeCB
            // 
            this.encodeTypeCB.Visible = false;
            // 
            // NTLMPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "NTLMPlugin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
