namespace C2.Controls.C1.Left
{
    partial class BaseLeftInnerButton
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
            this.components = new System.ComponentModel.Container();
            this.leftPictureBox = new System.Windows.Forms.PictureBox();
            this.noFocusButton = new C2.Controls.Common.NoFocusButton();
            this.textBox = new System.Windows.Forms.TextBox();
            this.rightPictureBox = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // leftPictureBox
            // 
            this.leftPictureBox.Location = new System.Drawing.Point(1, 1);
            this.leftPictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.leftPictureBox.Name = "leftPictureBox";
            this.leftPictureBox.Size = new System.Drawing.Size(24, 24);
            this.leftPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.leftPictureBox.TabIndex = 0;
            this.leftPictureBox.TabStop = false;
            // 
            // noFocusButton
            // 
            this.noFocusButton.AutoEllipsis = true;
            this.noFocusButton.FlatAppearance.BorderSize = 0;
            this.noFocusButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.noFocusButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.noFocusButton.Location = new System.Drawing.Point(25, 1);
            this.noFocusButton.Margin = new System.Windows.Forms.Padding(2);
            this.noFocusButton.Name = "noFocusButton";
            this.noFocusButton.Size = new System.Drawing.Size(95, 25);
            this.noFocusButton.TabIndex = 1;
            this.noFocusButton.UseVisualStyleBackColor = false;
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.White;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox.Location = new System.Drawing.Point(25, 3);
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(94, 16);
            this.textBox.TabIndex = 2;
            this.textBox.Visible = false;
            // 
            // rightPictureBox
            // 
            this.rightPictureBox.Location = new System.Drawing.Point(122, 6);
            this.rightPictureBox.Name = "rightPictureBox";
            this.rightPictureBox.Size = new System.Drawing.Size(16, 16);
            this.rightPictureBox.TabIndex = 3;
            this.rightPictureBox.TabStop = false;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip_Opening);
            // 
            // BaseLeftInnerButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.rightPictureBox);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.noFocusButton);
            this.Controls.Add(this.leftPictureBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BaseLeftInnerButton";
            this.Size = new System.Drawing.Size(140, 27);
            ((System.ComponentModel.ISupportInitialize)(this.leftPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rightPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.PictureBox leftPictureBox;
        protected Controls.Common.NoFocusButton noFocusButton;
        protected System.Windows.Forms.TextBox textBox;
        protected System.Windows.Forms.PictureBox rightPictureBox;
        protected System.Windows.Forms.ToolTip toolTip;
        protected System.Windows.Forms.ContextMenuStrip contextMenuStrip;
    }
}
