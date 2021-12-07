namespace C2.Forms
{
    partial class DBForm
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
            this.dbListView = new System.Windows.Forms.ListView();
            this.lvUrl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvMember = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvMoney = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.browserButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.excelPathTextBox = new System.Windows.Forms.TextBox();
            this.sampleButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dbListView
            // 
            this.dbListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvUrl,
            this.lvName,
            this.lvTime,
            this.lvMember,
            this.lvMoney});
            this.dbListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dbListView.GridLines = true;
            this.dbListView.HideSelection = false;
            this.dbListView.Location = new System.Drawing.Point(0, 56);
            this.dbListView.Name = "dbListView";
            this.dbListView.Size = new System.Drawing.Size(784, 406);
            this.dbListView.TabIndex = 1;
            this.dbListView.UseCompatibleStateImageBehavior = false;
            this.dbListView.View = System.Windows.Forms.View.Details;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "导入涉赌数据：";
            // 
            // browserButton
            // 
            this.browserButton.Location = new System.Drawing.Point(361, 13);
            this.browserButton.Name = "browserButton";
            this.browserButton.Size = new System.Drawing.Size(76, 26);
            this.browserButton.TabIndex = 1;
            this.browserButton.Text = "选择文件";
            this.browserButton.UseVisualStyleBackColor = true;
            this.browserButton.Click += new System.EventHandler(this.BrowserButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(456, 13);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(55, 26);
            this.updateButton.TabIndex = 2;
            this.updateButton.Text = "上传";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // excelPathTextBox
            // 
            this.excelPathTextBox.Location = new System.Drawing.Point(142, 14);
            this.excelPathTextBox.Name = "excelPathTextBox";
            this.excelPathTextBox.Size = new System.Drawing.Size(198, 21);
            this.excelPathTextBox.TabIndex = 3;
            // 
            // sampleButton
            // 
            this.sampleButton.Location = new System.Drawing.Point(528, 14);
            this.sampleButton.Name = "sampleButton";
            this.sampleButton.Size = new System.Drawing.Size(76, 26);
            this.sampleButton.TabIndex = 4;
            this.sampleButton.Text = "下载模板";
            this.sampleButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.sampleButton);
            this.panel1.Controls.Add(this.excelPathTextBox);
            this.panel1.Controls.Add(this.updateButton);
            this.panel1.Controls.Add(this.browserButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 50);
            this.panel1.TabIndex = 0;
            // 
            // DBForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dbListView);
            this.IconImage = global::C2.Properties.Resources.JS;
            this.Name = "DBForm";
            this.Text = "DB专项";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        
        private System.Windows.Forms.ListView dbListView;
        private System.Windows.Forms.ColumnHeader lvUrl;
        private System.Windows.Forms.ColumnHeader lvName;
        private System.Windows.Forms.ColumnHeader lvTime;
        private System.Windows.Forms.ColumnHeader lvMember;
        private System.Windows.Forms.ColumnHeader lvMoney;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button browserButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.TextBox excelPathTextBox;
        private System.Windows.Forms.Button sampleButton;
        private System.Windows.Forms.Panel panel1;
    }
}