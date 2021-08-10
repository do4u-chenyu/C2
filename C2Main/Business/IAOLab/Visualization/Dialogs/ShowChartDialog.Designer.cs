namespace C2.Business.IAOLab.Visualization.Dialogs
{
    partial class ShowChartDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.settingButton = new System.Windows.Forms.ToolStripButton();
            this.picSaveButton = new System.Windows.Forms.ToolStripButton();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingButton,
            this.picSaveButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(829, 31);
            this.toolStrip1.TabIndex = 10003;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // settingButton
            // 
            this.settingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingButton.Image = global::C2.Properties.Resources.map_setting;
            this.settingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingButton.Name = "settingButton";
            this.settingButton.Size = new System.Drawing.Size(23, 28);
            this.settingButton.Text = "settingButton";
            this.settingButton.Click += new System.EventHandler(this.SettingButton_Click);
            // 
            // picSaveButton
            // 
            this.picSaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.picSaveButton.Image = global::C2.Properties.Resources.map_save;
            this.picSaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.picSaveButton.Name = "picSaveButton";
            this.picSaveButton.Size = new System.Drawing.Size(23, 28);
            this.picSaveButton.Text = "picSaveButton";
            this.picSaveButton.Click += new System.EventHandler(this.PicSaveButton_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(0, 34);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(829, 312);
            this.webBrowser1.TabIndex = 10004;
            // 
            // ShowChartDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 391);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ShowChartDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据可视化";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.webBrowser1, 0);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton settingButton;
        private System.Windows.Forms.ToolStripButton picSaveButton;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}