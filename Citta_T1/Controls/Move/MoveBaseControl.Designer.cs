namespace Citta_T1.Controls.Move
{
    partial class MoveBaseControl
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.txtButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(42, 2);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(21, 21);
            this.textBox.TabIndex = 0;
            // 
            // txtButton
            // 
            this.txtButton.Location = new System.Drawing.Point(81, 2);
            this.txtButton.Name = "txtButton";
            this.txtButton.Size = new System.Drawing.Size(25, 21);
            this.txtButton.TabIndex = 1;
            this.txtButton.UseVisualStyleBackColor = true;
            // 
            // MoveBaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtButton);
            this.Controls.Add(this.textBox);
            this.Name = "MoveBaseControl";
            this.Size = new System.Drawing.Size(150, 29);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button txtButton;
    }
}
