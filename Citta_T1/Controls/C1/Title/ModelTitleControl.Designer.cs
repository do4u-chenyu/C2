namespace C2.Controls.Title
{
    partial class ModelTitleControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelTitleControl));
            this.panelRight = new System.Windows.Forms.Panel();
            this.closePictureBox = new System.Windows.Forms.PictureBox();
            this.modelTitlelabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.dirtyPictureBox = new System.Windows.Forms.PictureBox();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelMiddle = new System.Windows.Forms.Panel();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dirtyPictureBox)).BeginInit();
            this.panelLeft.SuspendLayout();
            this.panelMiddle.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.closePictureBox);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(126, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(14, 26);
            this.panelRight.TabIndex = 1;
            // 
            // closePictureBox
            // 
            this.closePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closePictureBox.Image = global::C2.Properties.Resources.close;
            this.closePictureBox.Location = new System.Drawing.Point(0, 0);
            this.closePictureBox.Name = "closePictureBox";
            this.closePictureBox.Size = new System.Drawing.Size(14, 26);
            this.closePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.closePictureBox.TabIndex = 0;
            this.closePictureBox.TabStop = false;
            this.closePictureBox.Click += new System.EventHandler(this.ClosePictureBox_Click);
            // 
            // modelTitlelabel
            // 
            this.modelTitlelabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelTitlelabel.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
            this.modelTitlelabel.Location = new System.Drawing.Point(0, 0);
            this.modelTitlelabel.Name = "modelTitlelabel";
            this.modelTitlelabel.Size = new System.Drawing.Size(113, 26);
            this.modelTitlelabel.TabIndex = 2;
            this.modelTitlelabel.Text = "我的新模型";
            this.modelTitlelabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.modelTitlelabel.Click += new System.EventHandler(this.MdelTitlelabel_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // dirtyPictureBox
            // 
            this.dirtyPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("dirtyPictureBox.Image")));
            this.dirtyPictureBox.Location = new System.Drawing.Point(0, 0);
            this.dirtyPictureBox.Name = "dirtyPictureBox";
            this.dirtyPictureBox.Size = new System.Drawing.Size(12, 13);
            this.dirtyPictureBox.TabIndex = 3;
            this.dirtyPictureBox.TabStop = false;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.dirtyPictureBox);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(13, 26);
            this.panelLeft.TabIndex = 4;
            // 
            // panelMiddle
            // 
            this.panelMiddle.Controls.Add(this.modelTitlelabel);
            this.panelMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMiddle.Location = new System.Drawing.Point(13, 0);
            this.panelMiddle.Name = "panelMiddle";
            this.panelMiddle.Size = new System.Drawing.Size(113, 26);
            this.panelMiddle.TabIndex = 5;
            // 
            // ModelTitleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.Controls.Add(this.panelMiddle);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelRight);
            this.DoubleBuffered = true;
            this.Name = "ModelTitleControl";
            this.Size = new System.Drawing.Size(140, 26);
            this.panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.closePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dirtyPictureBox)).EndInit();
            this.panelLeft.ResumeLayout(false);
            this.panelMiddle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.PictureBox closePictureBox;
        private System.Windows.Forms.Label modelTitlelabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox dirtyPictureBox;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelMiddle;
    }
}
