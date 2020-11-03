namespace C2.Controls.Flow
{
    partial class RemarkControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemarkControl));
            this.remarkCtrPanel = new System.Windows.Forms.Panel();
            this.remarkCtrTextBox = new System.Windows.Forms.TextBox();
            this.remarkCtrPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // remarkCtrPanel
            // 
            this.remarkCtrPanel.BackColor = System.Drawing.Color.White;
            this.remarkCtrPanel.BackgroundImage = global::C2.Properties.Resources.remark;
            this.remarkCtrPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.remarkCtrPanel.Controls.Add(this.remarkCtrTextBox);
            this.remarkCtrPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remarkCtrPanel.Location = new System.Drawing.Point(0, 0);
            this.remarkCtrPanel.Name = "remarkCtrPanel";
            this.remarkCtrPanel.Size = new System.Drawing.Size(224, 322);
            this.remarkCtrPanel.TabIndex = 0;
            // 
            // remarkCtrTextBox
            // 
            this.remarkCtrTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(37)))));
            this.remarkCtrTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.remarkCtrTextBox.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remarkCtrTextBox.Location = new System.Drawing.Point(0, 0);
            this.remarkCtrTextBox.Multiline = true;
            this.remarkCtrTextBox.Name = "remarkCtrTextBox";
            this.remarkCtrTextBox.Size = new System.Drawing.Size(211, 314);
            this.remarkCtrTextBox.TabIndex = 0;
            this.remarkCtrTextBox.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            // 
            // RemarkControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.remarkCtrPanel);
            this.DoubleBuffered = true;
            this.Name = "RemarkControl";
            this.Size = new System.Drawing.Size(224, 322);
            this.remarkCtrPanel.ResumeLayout(false);
            this.remarkCtrPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel remarkCtrPanel;
        private System.Windows.Forms.TextBox remarkCtrTextBox;
    }
}
