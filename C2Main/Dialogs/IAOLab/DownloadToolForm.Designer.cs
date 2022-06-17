namespace C2.Dialogs.IAOLab
{
    partial class DownloadToolForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadToolForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.check = new System.Windows.Forms.Button();
            this.confirm = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.DouYinTabPage = new System.Windows.Forms.TabPage();
            this.import = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.DouYinTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.check);
            this.panel1.Controls.Add(this.confirm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 341);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(742, 39);
            this.panel1.TabIndex = 7;
            // 
            // check
            // 
            this.check.Font = new System.Drawing.Font("宋体", 11F);
            this.check.Location = new System.Drawing.Point(670, 7);
            this.check.Name = "check";
            this.check.Size = new System.Drawing.Size(56, 23);
            this.check.TabIndex = 12;
            this.check.Text = "查看";
            this.check.UseVisualStyleBackColor = true;
            this.check.Click += new System.EventHandler(this.Check_Click);
            // 
            // confirm
            // 
            this.confirm.Font = new System.Drawing.Font("宋体", 11F);
            this.confirm.Location = new System.Drawing.Point(602, 7);
            this.confirm.Margin = new System.Windows.Forms.Padding(2);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(56, 23);
            this.confirm.TabIndex = 3;
            this.confirm.Text = "下载";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(742, 341);
            this.panel2.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.DouYinTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(742, 341);
            this.tabControl1.TabIndex = 1;
            // 
            // DouYinTabPage
            // 
            this.DouYinTabPage.Controls.Add(this.import);
            this.DouYinTabPage.Controls.Add(this.progressBar1);
            this.DouYinTabPage.Controls.Add(this.label1);
            this.DouYinTabPage.Controls.Add(this.label18);
            this.DouYinTabPage.Controls.Add(this.label17);
            this.DouYinTabPage.Controls.Add(this.label13);
            this.DouYinTabPage.Controls.Add(this.richTextBox1);
            this.DouYinTabPage.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.DouYinTabPage.Location = new System.Drawing.Point(4, 24);
            this.DouYinTabPage.Name = "DouYinTabPage";
            this.DouYinTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DouYinTabPage.Size = new System.Drawing.Size(734, 313);
            this.DouYinTabPage.TabIndex = 0;
            this.DouYinTabPage.Text = "抖音视频下载";
            this.DouYinTabPage.UseVisualStyleBackColor = true;
            // 
            // import
            // 
            this.import.Font = new System.Drawing.Font("宋体", 11F);
            this.import.Location = new System.Drawing.Point(670, 28);
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(56, 23);
            this.import.TabIndex = 23;
            this.import.Text = "导入";
            this.import.UseVisualStyleBackColor = true;
            this.import.Click += new System.EventHandler(this.Import_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(88, 28);
            this.progressBar1.Maximum = 1000;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(566, 23);
            this.progressBar1.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.ForeColor = System.Drawing.Color.SkyBlue;
            this.label1.Location = new System.Drawing.Point(8, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "下载进度";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label18.Location = new System.Drawing.Point(6, 76);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(406, 14);
            this.label18.TabIndex = 20;
            this.label18.Text = "批量查询格式：多个下载链接间用换行分隔，最大支持500条";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 10.25F);
            this.label17.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label17.Location = new System.Drawing.Point(6, 56);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(455, 14);
            this.label17.TabIndex = 19;
            this.label17.Text = "单次输入格式：https://www.iesdouyin.com/share/video/12345678910/";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10F);
            this.label13.Location = new System.Drawing.Point(5, 8);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(217, 14);
            this.label13.TabIndex = 18;
            this.label13.Text = "请在下方输入待下载抖音视频链接";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox1.Location = new System.Drawing.Point(3, 103);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.richTextBox1.Size = new System.Drawing.Size(728, 207);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            // 
            // DownloadToolForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(742, 380);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DownloadToolForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "下载工具";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.DouYinTabPage.ResumeLayout(false);
            this.DouYinTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button check;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage DouYinTabPage;
        private System.Windows.Forms.Button import;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}