namespace Citta_T1
{
    partial class MoveOpControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveOpControl));
            this.RightPictureBox = new System.Windows.Forms.PictureBox();
            this.TextButton = new System.Windows.Forms.Button();
            this.LeftPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.RightPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // RightPictureBox
            // 
            this.RightPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("RightPictureBox.Image")));
            this.RightPictureBox.Location = new System.Drawing.Point(78, 0);
            this.RightPictureBox.Name = "RightPictureBox";
            this.RightPictureBox.Size = new System.Drawing.Size(30, 21);
            this.RightPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.RightPictureBox.TabIndex = 1;
            this.RightPictureBox.TabStop = false;
            this.RightPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.RightPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            this.RightPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OpPicture_MouseMove);
            // 
            // TextButton
            // 
            this.TextButton.AllowDrop = true;
            this.TextButton.FlatAppearance.BorderSize = 0;
            this.TextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TextButton.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TextButton.Location = new System.Drawing.Point(21, 0);
            this.TextButton.Name = "TextButton";
            this.TextButton.Size = new System.Drawing.Size(68, 21);
            this.TextButton.TabIndex = 2;
            this.TextButton.Text = "连接算子";
            this.TextButton.UseVisualStyleBackColor = true;
            this.TextButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.TextButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            this.TextButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OpButton_MouseMove);
            // 
            // LeftPicture
            // 
            this.LeftPicture.Image = ((System.Drawing.Image)(resources.GetObject("LeftPicture.Image")));
            this.LeftPicture.Location = new System.Drawing.Point(0, 0);
            this.LeftPicture.Name = "LeftPicture";
            this.LeftPicture.Size = new System.Drawing.Size(21, 21);
            this.LeftPicture.TabIndex = 0;
            this.LeftPicture.TabStop = false;
            this.LeftPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.LeftPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            this.LeftPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OpPicture_MouseMove);
            // 
            // MoveOpControl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.RightPictureBox);
            this.Controls.Add(this.TextButton);
            this.Controls.Add(this.LeftPicture);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "MoveOpControl";
            this.Size = new System.Drawing.Size(183, 94);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveOpControl_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.RightPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox RightPictureBox;
        private System.Windows.Forms.PictureBox LeftPicture;
        public System.Windows.Forms.Button TextButton;
    }
}
