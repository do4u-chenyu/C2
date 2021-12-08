namespace C2.Forms
{
    partial class JSForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.sampleButton = new System.Windows.Forms.Button();
            this.excelPathTextBox = new System.Windows.Forms.TextBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.browserButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabBar1 = new C2.Controls.TabBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.sampleButton);
            this.panel1.Controls.Add(this.excelPathTextBox);
            this.panel1.Controls.Add(this.updateButton);
            this.panel1.Controls.Add(this.browserButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 40);
            this.panel1.TabIndex = 0;
            // 
            // sampleButton
            // 
            this.sampleButton.Location = new System.Drawing.Point(555, 11);
            this.sampleButton.Name = "sampleButton";
            this.sampleButton.Size = new System.Drawing.Size(76, 26);
            this.sampleButton.TabIndex = 4;
            this.sampleButton.Text = "下载模板";
            this.sampleButton.UseVisualStyleBackColor = true;
            this.sampleButton.Click += new System.EventHandler(this.SampleButton_Click);
            // 
            // excelPathTextBox
            // 
            this.excelPathTextBox.Location = new System.Drawing.Point(83, 14);
            this.excelPathTextBox.Name = "excelPathTextBox";
            this.excelPathTextBox.ReadOnly = true;
            this.excelPathTextBox.Size = new System.Drawing.Size(307, 21);
            this.excelPathTextBox.TabIndex = 3;
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(486, 11);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(55, 26);
            this.updateButton.TabIndex = 2;
            this.updateButton.Text = "上传";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // browserButton
            // 
            this.browserButton.Location = new System.Drawing.Point(396, 11);
            this.browserButton.Name = "browserButton";
            this.browserButton.Size = new System.Drawing.Size(76, 26);
            this.browserButton.TabIndex = 1;
            this.browserButton.Text = "选择文件";
            this.browserButton.UseVisualStyleBackColor = true;
            this.browserButton.Click += new System.EventHandler(this.BrowserButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "导入数据包：";
            // 
            // tabBar1
            // 
            this.tabBar1.BackColor = System.Drawing.SystemColors.Window;
            this.tabBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabBar1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabBar1.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.tabBar1.Location = new System.Drawing.Point(0, 0);
            this.tabBar1.Name = "tabBar1";
            this.tabBar1.ShowPreferencesButton = false;
            this.tabBar1.Size = new System.Drawing.Size(784, 39);
            this.tabBar1.TabIndex = 4;
            this.tabBar1.Text = "tabBar1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.webBrowser);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(784, 423);
            this.panel3.TabIndex = 5;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 40);
            this.webBrowser.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(784, 383);
            this.webBrowser.TabIndex = 1;
            // 
            // JSForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tabBar1);
            this.IconImage = global::C2.Properties.Resources.JS;
            this.Name = "JSForm";
            this.Text = "胶水系统";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button sampleButton;
        private System.Windows.Forms.TextBox excelPathTextBox;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button browserButton;
        private System.Windows.Forms.Label label1;
        private Controls.TabBar tabBar1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.WebBrowser webBrowser;
    }
}