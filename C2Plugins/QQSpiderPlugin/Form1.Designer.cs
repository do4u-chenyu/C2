﻿namespace QQSpiderPlugin
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.idListView = new System.Windows.Forms.ListView();
            this.IDResultRichTextBox = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ResetCookieButton1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.InputActButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ActStartButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupListView = new System.Windows.Forms.ListView();
            this.GroupRichTextBox = new System.Windows.Forms.RichTextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ResetCookieButton2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.InputGroupButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.GroupStartButton = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(658, 485);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(650, 459);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "QQ爬虫";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.idListView);
            this.panel3.Controls.Add(this.IDResultRichTextBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 69);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(644, 332);
            this.panel3.TabIndex = 5;
            // 
            // idListView
            // 
            this.idListView.Dock = System.Windows.Forms.DockStyle.Left;
            this.idListView.HideSelection = false;
            this.idListView.Location = new System.Drawing.Point(0, 0);
            this.idListView.Margin = new System.Windows.Forms.Padding(0);
            this.idListView.Name = "idListView";
            this.idListView.Size = new System.Drawing.Size(121, 332);
            this.idListView.TabIndex = 1;
            this.idListView.UseCompatibleStateImageBehavior = false;
            this.idListView.View = System.Windows.Forms.View.SmallIcon;
            this.idListView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // IDResultRichTextBox
            // 
            this.IDResultRichTextBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.IDResultRichTextBox.Location = new System.Drawing.Point(122, 0);
            this.IDResultRichTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.IDResultRichTextBox.Name = "IDResultRichTextBox";
            this.IDResultRichTextBox.Size = new System.Drawing.Size(522, 332);
            this.IDResultRichTextBox.TabIndex = 0;
            this.IDResultRichTextBox.Text = "";
            this.IDResultRichTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ResetCookieButton1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Controls.Add(this.InputActButton);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(644, 66);
            this.panel2.TabIndex = 4;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // ResetCookieButton1
            // 
            this.ResetCookieButton1.Location = new System.Drawing.Point(522, 3);
            this.ResetCookieButton1.Name = "ResetCookieButton1";
            this.ResetCookieButton1.Size = new System.Drawing.Size(98, 23);
            this.ResetCookieButton1.TabIndex = 6;
            this.ResetCookieButton1.Text = "清除Cookie";
            this.ResetCookieButton1.UseVisualStyleBackColor = true;
            this.ResetCookieButton1.Click += new System.EventHandler(this.ResetCookieButton1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(15, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "查询进度";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(104, 34);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(516, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // InputActButton
            // 
            this.InputActButton.Location = new System.Drawing.Point(441, 3);
            this.InputActButton.Name = "InputActButton";
            this.InputActButton.Size = new System.Drawing.Size(75, 23);
            this.InputActButton.TabIndex = 3;
            this.InputActButton.Text = "导入";
            this.InputActButton.UseVisualStyleBackColor = true;
            this.InputActButton.Click += new System.EventHandler(this.InputIDDataButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选中导入QQ号列表文件，或者粘贴到左侧任务列表中，一行一个QQ号";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ActStartButton);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 401);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 55);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ActStartButton
            // 
            this.ActStartButton.Location = new System.Drawing.Point(441, 17);
            this.ActStartButton.Name = "ActStartButton";
            this.ActStartButton.Size = new System.Drawing.Size(75, 23);
            this.ActStartButton.TabIndex = 1;
            this.ActStartButton.Text = "开始";
            this.ActStartButton.UseVisualStyleBackColor = true;
            this.ActStartButton.Click += new System.EventHandler(this.ActStartButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(543, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel6);
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(650, 459);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "QQ群爬虫";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.groupListView);
            this.panel6.Controls.Add(this.GroupRichTextBox);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 69);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(644, 332);
            this.panel6.TabIndex = 2;
            // 
            // groupListView
            // 
            this.groupListView.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupListView.HideSelection = false;
            this.groupListView.Location = new System.Drawing.Point(0, 0);
            this.groupListView.Margin = new System.Windows.Forms.Padding(0);
            this.groupListView.Name = "groupListView";
            this.groupListView.Size = new System.Drawing.Size(121, 332);
            this.groupListView.TabIndex = 3;
            this.groupListView.UseCompatibleStateImageBehavior = false;
            this.groupListView.View = System.Windows.Forms.View.SmallIcon;
            // 
            // GroupRichTextBox
            // 
            this.GroupRichTextBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.GroupRichTextBox.Location = new System.Drawing.Point(122, 0);
            this.GroupRichTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.GroupRichTextBox.Name = "GroupRichTextBox";
            this.GroupRichTextBox.Size = new System.Drawing.Size(522, 332);
            this.GroupRichTextBox.TabIndex = 2;
            this.GroupRichTextBox.Text = "";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ResetCookieButton2);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.progressBar2);
            this.panel5.Controls.Add(this.InputGroupButton);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(644, 66);
            this.panel5.TabIndex = 1;
            // 
            // ResetCookieButton2
            // 
            this.ResetCookieButton2.Location = new System.Drawing.Point(522, 3);
            this.ResetCookieButton2.Name = "ResetCookieButton2";
            this.ResetCookieButton2.Size = new System.Drawing.Size(98, 23);
            this.ResetCookieButton2.TabIndex = 9;
            this.ResetCookieButton2.Text = "清除Cookie";
            this.ResetCookieButton2.UseVisualStyleBackColor = true;
            this.ResetCookieButton2.Click += new System.EventHandler(this.ResetCookieButton2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(15, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "查询进度";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(104, 34);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(516, 23);
            this.progressBar2.TabIndex = 7;
            // 
            // InputGroupButton
            // 
            this.InputGroupButton.Location = new System.Drawing.Point(441, 3);
            this.InputGroupButton.Name = "InputGroupButton";
            this.InputGroupButton.Size = new System.Drawing.Size(75, 23);
            this.InputGroupButton.TabIndex = 5;
            this.InputGroupButton.Text = "导入";
            this.InputGroupButton.UseVisualStyleBackColor = true;
            this.InputGroupButton.Click += new System.EventHandler(this.InputGroupButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(410, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "选中导入QQ群号列表文件，或者粘贴到左侧任务列表中，一行一个QQ群";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.GroupStartButton);
            this.panel4.Controls.Add(this.button5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(3, 401);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(644, 55);
            this.panel4.TabIndex = 0;
            // 
            // GroupStartButton
            // 
            this.GroupStartButton.Location = new System.Drawing.Point(441, 17);
            this.GroupStartButton.Name = "GroupStartButton";
            this.GroupStartButton.Size = new System.Drawing.Size(75, 23);
            this.GroupStartButton.TabIndex = 3;
            this.GroupStartButton.Text = "开始";
            this.GroupStartButton.UseVisualStyleBackColor = true;
            this.GroupStartButton.Click += new System.EventHandler(this.GroupStartButton_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(543, 17);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "取消";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 485);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QQ爬虫";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView idListView;
        private System.Windows.Forms.RichTextBox IDResultRichTextBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ActStartButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button InputActButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ListView groupListView;
        private System.Windows.Forms.RichTextBox GroupRichTextBox;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button GroupStartButton;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button InputGroupButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ResetCookieButton1;
        private System.Windows.Forms.Button ResetCookieButton2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar2;
    }
}

