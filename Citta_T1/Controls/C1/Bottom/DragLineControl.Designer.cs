namespace C2.Controls.Bottom
{
    partial class DragLineControl
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
            // DragLineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(246)))));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DragLineControl";
            this.Size = new System.Drawing.Size(225, 4);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragLineControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DragLineControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DragLineControl_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
