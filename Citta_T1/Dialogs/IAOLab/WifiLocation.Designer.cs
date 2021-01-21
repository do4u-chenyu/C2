namespace C2.Dialogs.IAOLab
{
    partial class WifiLocation
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
            this.inputAndResult = new System.Windows.Forms.RichTextBox();
            this.inputLabel = new System.Windows.Forms.Label();
            this.confirm = new System.Windows.Forms.Button();
            this.cancle = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tipLable = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputAndResult
            // 
            this.inputAndResult.BackColor = System.Drawing.Color.White;
            this.inputAndResult.Location = new System.Drawing.Point(0, 84);
            this.inputAndResult.Margin = new System.Windows.Forms.Padding(2);
            this.inputAndResult.Name = "inputAndResult";
            this.inputAndResult.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.inputAndResult.Size = new System.Drawing.Size(602, 219);
            this.inputAndResult.TabIndex = 0;
            this.inputAndResult.Text = "";
            this.inputAndResult.WordWrap = false;
            this.inputAndResult.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Font = new System.Drawing.Font("宋体", 11F);
            this.inputLabel.Location = new System.Drawing.Point(19, 16);
            this.inputLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(191, 19);
            this.inputLabel.TabIndex = 1;
            this.inputLabel.Text = "请在下方输入MAC地址";
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(424, 10);
            this.confirm.Margin = new System.Windows.Forms.Padding(2);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(56, 24);
            this.confirm.TabIndex = 3;
            this.confirm.Text = "查询";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.Search_Click);
            // 
            // cancle
            // 
            this.cancle.Location = new System.Drawing.Point(522, 10);
            this.cancle.Margin = new System.Windows.Forms.Padding(2);
            this.cancle.Name = "cancle";
            this.cancle.Size = new System.Drawing.Size(56, 24);
            this.cancle.TabIndex = 4;
            this.cancle.Text = "取消";
            this.cancle.UseVisualStyleBackColor = true;
            this.cancle.Click += new System.EventHandler(this.Cancle_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.confirm);
            this.panel1.Controls.Add(this.cancle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 297);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(602, 44);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 15);
            this.label2.TabIndex = 6;
            // 
            // tipLable
            // 
            this.tipLable.AutoSize = true;
            this.tipLable.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.tipLable.Location = new System.Drawing.Point(19, 47);
            this.tipLable.Name = "tipLable";
            this.tipLable.Size = new System.Drawing.Size(542, 30);
            this.tipLable.TabIndex = 7;
            this.tipLable.Text = "单次输入格式：04a1518006c2 或 04-a1-51-80-06-c2 或 04:a1:51:80:06:c2\r\n批量查询格式：多个mac间用换行分割，最" +
    "大支持1000条";
            this.tipLable.Click += new System.EventHandler(this.tipLable_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(411, 63);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(155, 25);
            this.textBox1.TabIndex = 8;
            // 
            // WifiLocation
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(602, 341);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tipLable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.inputLabel);
            this.Controls.Add(this.inputAndResult);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "WifiLocation";
            this.Text = "Wifi查询";
            this.Load += new System.EventHandler(this.WifiLocation_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox inputAndResult;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button cancle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label tipLable;
        private System.Windows.Forms.TextBox textBox1;
    }
}