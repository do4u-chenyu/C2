namespace Citta_T1.Controls.Left
{
    partial class DataSourceControl
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
            this.DataSourceFrame = new System.Windows.Forms.Panel();
            this.ExternalData = new System.Windows.Forms.Label();
            this.LocalData = new System.Windows.Forms.Label();
            this.LocalFrame = new System.Windows.Forms.Panel();
            this.ExternalFrame = new System.Windows.Forms.Panel();
            this.DataSourceFrame.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataSourceFrame
            // 
            this.DataSourceFrame.BackColor = System.Drawing.Color.White;
            this.DataSourceFrame.Controls.Add(this.ExternalData);
            this.DataSourceFrame.Controls.Add(this.LocalData);
            this.DataSourceFrame.Dock = System.Windows.Forms.DockStyle.Top;
            this.DataSourceFrame.Location = new System.Drawing.Point(0, 0);
            this.DataSourceFrame.Name = "DataSourceFrame";
            this.DataSourceFrame.Size = new System.Drawing.Size(185, 51);
            this.DataSourceFrame.TabIndex = 0;
            // 
            // ExternalData
            // 
            this.ExternalData.BackColor = System.Drawing.Color.Transparent;
            this.ExternalData.Dock = System.Windows.Forms.DockStyle.Right;
            this.ExternalData.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ExternalData.Location = new System.Drawing.Point(93, 0);
            this.ExternalData.Name = "ExternalData";
            this.ExternalData.Size = new System.Drawing.Size(92, 51);
            this.ExternalData.TabIndex = 1;
            this.ExternalData.Text = "外部数据";
            this.ExternalData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ExternalData.Click += new System.EventHandler(this.ExternalData_Click);
            // 
            // LocalData
            // 
            this.LocalData.BackColor = System.Drawing.Color.Transparent;
            this.LocalData.Dock = System.Windows.Forms.DockStyle.Left;
            this.LocalData.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LocalData.Location = new System.Drawing.Point(0, 0);
            this.LocalData.Name = "LocalData";
            this.LocalData.Size = new System.Drawing.Size(99, 51);
            this.LocalData.TabIndex = 0;
            this.LocalData.Text = "本地数据";
            this.LocalData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LocalData.Click += new System.EventHandler(this.LocalData_Click);
            // 
            // LocalFrame
            // 
            this.LocalFrame.BackColor = System.Drawing.Color.White;
            this.LocalFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LocalFrame.Location = new System.Drawing.Point(0, 51);
            this.LocalFrame.Name = "LocalFrame";
            this.LocalFrame.Size = new System.Drawing.Size(185, 586);
            this.LocalFrame.TabIndex = 1;
            this.LocalFrame.Paint += new System.Windows.Forms.PaintEventHandler(this.LocalFrame_Paint);
            // 
            // ExternalFrame
            // 
            this.ExternalFrame.BackColor = System.Drawing.Color.White;
            this.ExternalFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExternalFrame.Location = new System.Drawing.Point(0, 51);
            this.ExternalFrame.Name = "ExternalFrame";
            this.ExternalFrame.Size = new System.Drawing.Size(185, 586);
            this.ExternalFrame.TabIndex = 2;
            this.ExternalFrame.Visible = false;
            // 
            // DataSourceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ExternalFrame);
            this.Controls.Add(this.LocalFrame);
            this.Controls.Add(this.DataSourceFrame);
            this.Name = "DataSourceControl";
            this.Size = new System.Drawing.Size(185, 637);
            this.DataSourceFrame.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel DataSourceFrame;
        private System.Windows.Forms.Panel LocalFrame;
        private System.Windows.Forms.Panel ExternalFrame;
        private System.Windows.Forms.Label ExternalData;
        private System.Windows.Forms.Label LocalData;
    }
}
