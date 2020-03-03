namespace Citta_T1.Controls
{
    partial class MyModelControl
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
            this.modelButton1 = new Citta_T1.Controls.ModelButton();
            this.SuspendLayout();
            // 
            // modelButton1
            // 
            this.modelButton1.BackColor = System.Drawing.Color.White;
            this.modelButton1.Location = new System.Drawing.Point(32, 30);
            this.modelButton1.Name = "modelButton1";
            this.modelButton1.Size = new System.Drawing.Size(141, 27);
            this.modelButton1.TabIndex = 0;
            // 
            // MyModelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.modelButton1);
            this.Name = "MyModelControl";
            this.Size = new System.Drawing.Size(187, 637);
            this.ResumeLayout(false);

        }

        #endregion

        private ModelButton modelButton1;
    }
}
