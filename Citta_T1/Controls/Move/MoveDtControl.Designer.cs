namespace Citta_T1.Controls.Move
{
    partial class MoveDtControl
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
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPinPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // leftPicture
            // 
            this.leftPicture.Image = global::Citta_T1.Properties.Resources.u72;
            this.leftPicture.Location = new System.Drawing.Point(15, 4);
            this.leftPicture.Size = new System.Drawing.Size(17, 20);
            // 
            // txtButton
            // 
            this.txtButton.FlatAppearance.BorderSize = 0;
            // 
            // MoveDtControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "MoveDtControl";
            this.Controls.SetChildIndex(this.leftPicture, 0);
            this.Controls.SetChildIndex(this.rightPictureBox, 0);
            this.Controls.SetChildIndex(this.leftPinPictureBox, 0);
            this.Controls.SetChildIndex(this.textBox1, 0);
            this.Controls.SetChildIndex(this.txtButton, 0);
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftPinPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
