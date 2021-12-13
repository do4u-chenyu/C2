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
            this.panel2 = new System.Windows.Forms.Panel();
            this.excelTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.itemLabel = new System.Windows.Forms.Label();
            this.sampleButton = new System.Windows.Forms.Button();
            this.browserButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tabBar1 = new C2.Controls.JSTabBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 40);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.excelTextBox);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.itemLabel);
            this.panel2.Controls.Add(this.sampleButton);
            this.panel2.Controls.Add(this.browserButton);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(147, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(637, 40);
            this.panel2.TabIndex = 0;
            // 
            // excelTextBox
            // 
            this.excelTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.excelTextBox.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.excelTextBox.Location = new System.Drawing.Point(249, 15);
            this.excelTextBox.Name = "excelTextBox";
            this.excelTextBox.Size = new System.Drawing.Size(284, 18);
            this.excelTextBox.TabIndex = 7;
            this.excelTextBox.Text = "未选择任何文件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.Location = new System.Drawing.Point(105, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "数据：";
            // 
            // itemLabel
            // 
            this.itemLabel.AutoSize = true;
            this.itemLabel.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.itemLabel.ForeColor = System.Drawing.Color.Red;
            this.itemLabel.Location = new System.Drawing.Point(72, 14);
            this.itemLabel.Name = "itemLabel";
            this.itemLabel.Size = new System.Drawing.Size(39, 20);
            this.itemLabel.TabIndex = 0;
            this.itemLabel.Text = "涉赌";
            // 
            // sampleButton
            // 
            this.sampleButton.AutoSize = true;
            this.sampleButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.sampleButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.sampleButton.Location = new System.Drawing.Point(542, 9);
            this.sampleButton.Name = "sampleButton";
            this.sampleButton.Size = new System.Drawing.Size(76, 30);
            this.sampleButton.TabIndex = 4;
            this.sampleButton.Text = "模板样例";
            this.sampleButton.UseVisualStyleBackColor = false;
            // 
            // browserButton
            // 
            this.browserButton.AutoSize = true;
            this.browserButton.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.browserButton.Location = new System.Drawing.Point(156, 9);
            this.browserButton.Name = "browserButton";
            this.browserButton.Size = new System.Drawing.Size(85, 30);
            this.browserButton.TabIndex = 1;
            this.browserButton.Text = "选择文件";
            this.browserButton.UseVisualStyleBackColor = true;
            this.browserButton.Click += new System.EventHandler(this.BrowserButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label3.Location = new System.Drawing.Point(41, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "导入";
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
            this.tabBar1.Size = new System.Drawing.Size(784, 46);
            this.tabBar1.TabIndex = 4;
            this.tabBar1.Text = "tabBar1";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.webBrowser);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 46);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(784, 416);
            this.panel3.TabIndex = 5;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 40);
            this.webBrowser.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(784, 376);
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
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button sampleButton;
        private System.Windows.Forms.Button browserButton;
        private System.Windows.Forms.Label itemLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Controls.JSTabBar tabBar1;
        private System.Windows.Forms.TextBox excelTextBox;
    }
}