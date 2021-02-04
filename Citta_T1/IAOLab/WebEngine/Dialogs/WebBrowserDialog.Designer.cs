﻿namespace C2.IAOLab.WebEngine.Dialogs
{
    partial class WebBrowserDialog
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.LoadData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SaveHtml = new System.Windows.Forms.ToolStripButton();
            this.SavePic = new System.Windows.Forms.ToolStripButton();
            this.Clear = new System.Windows.Forms.ToolStripButton();
            this.EditCode = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadData,
            this.SaveHtml,
            this.SavePic,
            this.toolStripSeparator1,
            this.Clear,
            this.EditCode});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(589, 25);
            this.toolStrip1.TabIndex = 10003;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(0, 28);
            this.webBrowser1.Margin = new System.Windows.Forms.Padding(0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(589, 279);
            this.webBrowser1.TabIndex = 10004;
            // 
            // LoadData
            // 
            this.LoadData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LoadData.Image = global::C2.Properties.Resources.importDataSource;
            this.LoadData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LoadData.Name = "LoadData";
            this.LoadData.Size = new System.Drawing.Size(23, 22);
            this.LoadData.Text = "导入数据";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // SaveHtml
            // 
            this.SaveHtml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveHtml.Image = global::C2.Properties.Resources.save;
            this.SaveHtml.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveHtml.Name = "SaveHtml";
            this.SaveHtml.Size = new System.Drawing.Size(23, 22);
            this.SaveHtml.Text = "保存成html";
            // 
            // SavePic
            // 
            this.SavePic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SavePic.Image = global::C2.Properties.Resources.image;
            this.SavePic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SavePic.Name = "SavePic";
            this.SavePic.Size = new System.Drawing.Size(23, 22);
            this.SavePic.Text = "保存成图片";
            // 
            // Clear
            // 
            this.Clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Clear.Image = global::C2.Properties.Resources.delete;
            this.Clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(23, 22);
            this.Clear.Text = "清空";
            // 
            // EditCode
            // 
            this.EditCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditCode.Image = global::C2.Properties.Resources.edit_code;
            this.EditCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditCode.Name = "EditCode";
            this.EditCode.Size = new System.Drawing.Size(23, 22);
            this.EditCode.Text = "自定义源码";
            // 
            // WebBrowserDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 352);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "WebBrowserDialog";
            this.Text = "WebBrowserDialog";
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.webBrowser1, 0);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStripButton LoadData;
        private System.Windows.Forms.ToolStripButton SaveHtml;
        private System.Windows.Forms.ToolStripButton SavePic;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Clear;
        private System.Windows.Forms.ToolStripButton EditCode;
    }
}