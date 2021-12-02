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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.lvUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvMember = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvMoney = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.sampleButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.browserButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 462);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(776, 436);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DB专项";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvUrl,
            this.lvName,
            this.lvTime,
            this.lvMember,
            this.lvMoney});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 47);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(770, 386);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // lvUrl
            // 
            this.lvUrl.Text = "网站域名";
            this.lvUrl.Width = 145;
            // 
            // lvName
            // 
            this.lvName.Text = "网站名称";
            this.lvName.Width = 130;
            // 
            // lvTime
            // 
            this.lvTime.Text = "发现时间";
            this.lvTime.Width = 156;
            // 
            // lvMember
            // 
            this.lvMember.Text = "涉赌人数";
            this.lvMember.Width = 100;
            // 
            // lvMoney
            // 
            this.lvMoney.Text = "涉案金额";
            this.lvMoney.Width = 102;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.sampleButton);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.updateButton);
            this.panel1.Controls.Add(this.browserButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(770, 44);
            this.panel1.TabIndex = 0;
            // 
            // sampleButton
            // 
            this.sampleButton.Location = new System.Drawing.Point(440, 12);
            this.sampleButton.Name = "sampleButton";
            this.sampleButton.Size = new System.Drawing.Size(49, 23);
            this.sampleButton.TabIndex = 4;
            this.sampleButton.Text = "模板";
            this.sampleButton.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(127, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(198, 21);
            this.textBox1.TabIndex = 3;
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(383, 12);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(51, 23);
            this.updateButton.TabIndex = 2;
            this.updateButton.Text = "上传";
            this.updateButton.UseVisualStyleBackColor = true;
            // 
            // browserButton
            // 
            this.browserButton.Location = new System.Drawing.Point(331, 12);
            this.browserButton.Name = "browserButton";
            this.browserButton.Size = new System.Drawing.Size(46, 23);
            this.browserButton.TabIndex = 1;
            this.browserButton.Text = "浏览";
            this.browserButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "导入涉赌数据：";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(776, 436);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SQ专项";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(776, 436);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "SH专项";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(776, 436);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "DD专项";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // JSForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.tabControl1);
            this.IconImage = global::C2.Properties.Resources.JS;
            this.Name = "JSForm";
            this.Text = "胶水系统";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader lvUrl;
        private System.Windows.Forms.ColumnHeader lvName;
        private System.Windows.Forms.ColumnHeader lvTime;
        private System.Windows.Forms.ColumnHeader lvMember;
        private System.Windows.Forms.ColumnHeader lvMoney;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button sampleButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button browserButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
    }
}