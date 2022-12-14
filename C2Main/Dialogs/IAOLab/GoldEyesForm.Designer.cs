namespace C2.Dialogs.IAOLab
{
    partial class GoldEyesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoldEyesForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.export = new System.Windows.Forms.Button();
            this.confirm = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.IPChecktabPage = new System.Windows.Forms.TabPage();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.memo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uptime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.domain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SEOTabPage = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.import = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.IPChecktabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SEOTabPage.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.export);
            this.panel1.Controls.Add(this.confirm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 341);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(647, 39);
            this.panel1.TabIndex = 6;
            // 
            // export
            // 
            this.export.Font = new System.Drawing.Font("宋体", 11F);
            this.export.Location = new System.Drawing.Point(568, 8);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(56, 23);
            this.export.TabIndex = 12;
            this.export.Text = "导出";
            this.export.UseVisualStyleBackColor = true;
            this.export.Click += new System.EventHandler(this.Export_Click);
            // 
            // confirm
            // 
            this.confirm.Font = new System.Drawing.Font("宋体", 11F);
            this.confirm.Location = new System.Drawing.Point(478, 7);
            this.confirm.Margin = new System.Windows.Forms.Padding(2);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(56, 24);
            this.confirm.TabIndex = 3;
            this.confirm.Text = "查询";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(647, 341);
            this.panel2.TabIndex = 7;
            // 
            // IPChecktabPage
            // 
            this.IPChecktabPage.Controls.Add(this.dataGridView1);
            this.IPChecktabPage.Controls.Add(this.button1);
            this.IPChecktabPage.Controls.Add(this.progressBar2);
            this.IPChecktabPage.Controls.Add(this.label2);
            this.IPChecktabPage.Controls.Add(this.label3);
            this.IPChecktabPage.Controls.Add(this.label4);
            this.IPChecktabPage.Controls.Add(this.label5);
            this.IPChecktabPage.Controls.Add(this.richTextBox2);
            this.IPChecktabPage.Location = new System.Drawing.Point(4, 24);
            this.IPChecktabPage.Name = "IPChecktabPage";
            this.IPChecktabPage.Padding = new System.Windows.Forms.Padding(3);
            this.IPChecktabPage.Size = new System.Drawing.Size(639, 313);
            this.IPChecktabPage.TabIndex = 1;
            this.IPChecktabPage.Text = "IP反查域名";
            this.IPChecktabPage.UseVisualStyleBackColor = true;
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.White;
            this.richTextBox2.Location = new System.Drawing.Point(3, 103);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.richTextBox2.Size = new System.Drawing.Size(113, 207);
            this.richTextBox2.TabIndex = 24;
            this.richTextBox2.Text = "";
            this.richTextBox2.WordWrap = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F);
            this.label5.Location = new System.Drawing.Point(5, 8);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 14);
            this.label5.TabIndex = 25;
            this.label5.Text = "请在下方输入待查询IP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.25F);
            this.label4.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label4.Location = new System.Drawing.Point(6, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(203, 14);
            this.label4.TabIndex = 26;
            this.label4.Text = "单次输入格式：60.247.145.192";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Location = new System.Drawing.Point(6, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(370, 14);
            this.label3.TabIndex = 27;
            this.label3.Text = "批量查询格式：多个IP间用换行分隔，最大支持5000条";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.ForeColor = System.Drawing.Color.SkyBlue;
            this.label2.Location = new System.Drawing.Point(8, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "查询进度";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(88, 27);
            this.progressBar2.Maximum = 1000;
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(442, 23);
            this.progressBar2.TabIndex = 29;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 11F);
            this.button1.Location = new System.Drawing.Point(564, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = "导入";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IP,
            this.domain,
            this.addtime,
            this.uptime,
            this.memo});
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(117, 103);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(519, 207);
            this.dataGridView1.TabIndex = 31;
            // 
            // memo
            // 
            this.memo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.memo.FillWeight = 12F;
            this.memo.HeaderText = "备注";
            this.memo.Name = "memo";
            // 
            // uptime
            // 
            this.uptime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.uptime.FillWeight = 20F;
            this.uptime.HeaderText = "解绑时间";
            this.uptime.Name = "uptime";
            // 
            // addtime
            // 
            this.addtime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.addtime.FillWeight = 20F;
            this.addtime.HeaderText = "绑定时间";
            this.addtime.Name = "addtime";
            // 
            // domain
            // 
            this.domain.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.domain.FillWeight = 24F;
            this.domain.HeaderText = "域名";
            this.domain.Name = "domain";
            // 
            // IP
            // 
            this.IP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IP.FillWeight = 24F;
            this.IP.HeaderText = "IP";
            this.IP.Name = "IP";
            // 
            // SEOTabPage
            // 
            this.SEOTabPage.Controls.Add(this.import);
            this.SEOTabPage.Controls.Add(this.progressBar1);
            this.SEOTabPage.Controls.Add(this.label1);
            this.SEOTabPage.Controls.Add(this.label18);
            this.SEOTabPage.Controls.Add(this.label17);
            this.SEOTabPage.Controls.Add(this.label13);
            this.SEOTabPage.Controls.Add(this.richTextBox1);
            this.SEOTabPage.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.SEOTabPage.Location = new System.Drawing.Point(4, 24);
            this.SEOTabPage.Name = "SEOTabPage";
            this.SEOTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SEOTabPage.Size = new System.Drawing.Size(639, 313);
            this.SEOTabPage.TabIndex = 0;
            this.SEOTabPage.Text = "SEO综合查询";
            this.SEOTabPage.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox1.Location = new System.Drawing.Point(3, 103);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.richTextBox1.Size = new System.Drawing.Size(633, 207);
            this.richTextBox1.TabIndex = 17;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10F);
            this.label13.Location = new System.Drawing.Point(5, 8);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(189, 14);
            this.label13.TabIndex = 18;
            this.label13.Text = "请在下方输入待查询网站域名";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 10.25F);
            this.label17.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label17.Location = new System.Drawing.Point(6, 56);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(175, 14);
            this.label17.TabIndex = 19;
            this.label17.Text = "单次输入格式：taobao.com";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label18.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label18.Location = new System.Drawing.Point(6, 76);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(384, 14);
            this.label18.TabIndex = 20;
            this.label18.Text = "批量查询格式：多个域名间用换行分隔，最大支持5000条";
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
            this.label1.Text = "查询进度";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(88, 27);
            this.progressBar1.Maximum = 1000;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(442, 23);
            this.progressBar1.TabIndex = 22;
            // 
            // import
            // 
            this.import.Font = new System.Drawing.Font("宋体", 11F);
            this.import.Location = new System.Drawing.Point(564, 27);
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(56, 23);
            this.import.TabIndex = 23;
            this.import.Text = "导入";
            this.import.UseVisualStyleBackColor = true;
            this.import.Click += new System.EventHandler(this.Import_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.SEOTabPage);
            this.tabControl1.Controls.Add(this.IPChecktabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(647, 341);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // GoldEyesForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(647, 380);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GoldEyesForm";
            this.Text = "火眼金睛";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.IPChecktabPage.ResumeLayout(false);
            this.IPChecktabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.SEOTabPage.ResumeLayout(false);
            this.SEOTabPage.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button export;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage SEOTabPage;
        private System.Windows.Forms.Button import;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TabPage IPChecktabPage;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn domain;
        private System.Windows.Forms.DataGridViewTextBoxColumn addtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn uptime;
        private System.Windows.Forms.DataGridViewTextBoxColumn memo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox richTextBox2;
    }
}