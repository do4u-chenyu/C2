
namespace C2.Business.CastleBravo.VPN
{
    partial class BatchAddVPNServerForm
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
            this.label0 = new System.Windows.Forms.Label();
            this.wsTextBox = new System.Windows.Forms.TextBox();
            this.pasteModeCB = new System.Windows.Forms.CheckBox();
            this.browserButton = new System.Windows.Forms.Button();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.label102 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addr = new System.Windows.Forms.RadioButton();
            this.rss = new System.Windows.Forms.RadioButton();
            this.ss = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label0.AutoSize = true;
            this.label0.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label0.ForeColor = System.Drawing.Color.Red;
            this.label0.Location = new System.Drawing.Point(33, 80);
            this.label0.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label0.Name = "label1";
            this.label0.Size = new System.Drawing.Size(413, 18);
            this.label0.TabIndex = 10029;
            this.label0.Text = "* 格式:一行一个,支持ss,ssr,vmess,vless,trojan";
            // 
            // wsTextBox
            // 
            this.wsTextBox.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.wsTextBox.Location = new System.Drawing.Point(142, 166);
            this.wsTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.wsTextBox.MaxLength = 16777216;
            this.wsTextBox.Multiline = true;
            this.wsTextBox.Name = "wsTextBox";
            this.wsTextBox.ReadOnly = true;
            this.wsTextBox.Size = new System.Drawing.Size(492, 236);
            this.wsTextBox.TabIndex = 10028;
            this.wsTextBox.WordWrap = false;
            // 
            // pasteModeCB
            // 
            this.pasteModeCB.AutoSize = true;
            this.pasteModeCB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.pasteModeCB.Location = new System.Drawing.Point(16, 171);
            this.pasteModeCB.Margin = new System.Windows.Forms.Padding(4);
            this.pasteModeCB.Name = "pasteModeCB";
            this.pasteModeCB.Size = new System.Drawing.Size(110, 29);
            this.pasteModeCB.TabIndex = 10027;
            this.pasteModeCB.Text = "粘贴模式";
            this.pasteModeCB.UseVisualStyleBackColor = true;
            this.pasteModeCB.CheckedChanged += new System.EventHandler(this.PasteModeCB_CheckedChanged);
            // 
            // browserButton
            // 
            this.browserButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.browserButton.Location = new System.Drawing.Point(548, 36);
            this.browserButton.Margin = new System.Windows.Forms.Padding(4);
            this.browserButton.Name = "browserButton";
            this.browserButton.Size = new System.Drawing.Size(88, 34);
            this.browserButton.TabIndex = 10026;
            this.browserButton.Text = "+浏览";
            this.browserButton.UseVisualStyleBackColor = true;
            this.browserButton.Click += new System.EventHandler(this.BrowserButton_Click);
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.BackColor = System.Drawing.Color.White;
            this.filePathTextBox.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filePathTextBox.Location = new System.Drawing.Point(144, 35);
            this.filePathTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.ReadOnly = true;
            this.filePathTextBox.Size = new System.Drawing.Size(392, 33);
            this.filePathTextBox.TabIndex = 10025;
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.label102.Location = new System.Drawing.Point(24, 37);
            this.label102.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(112, 27);
            this.label102.TabIndex = 10024;
            this.label102.Text = "文件路径：";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label100.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label100.Location = new System.Drawing.Point(4, 212);
            this.label100.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(125, 18);
            this.label100.TabIndex = 10030;
            this.label100.Text = "最大[16M]文本";
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label101.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label101.Location = new System.Drawing.Point(4, 241);
            this.label101.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(125, 18);
            this.label101.TabIndex = 10031;
            this.label101.Text = "大约10W-20W行";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Bold);
            this.label104.Location = new System.Drawing.Point(24, 116);
            this.label104.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(112, 27);
            this.label104.TabIndex = 10032;
            this.label104.Text = "地址类型：";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.addr);
            this.groupBox1.Controls.Add(this.rss);
            this.groupBox1.Controls.Add(this.ss);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(144, 99);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(490, 54);
            this.groupBox1.TabIndex = 10033;
            this.groupBox1.TabStop = false;
            // 
            // addr
            // 
            this.addr.AutoSize = true;
            this.addr.Location = new System.Drawing.Point(222, 21);
            this.addr.Margin = new System.Windows.Forms.Padding(4);
            this.addr.Name = "addr";
            this.addr.Size = new System.Drawing.Size(105, 22);
            this.addr.TabIndex = 10022;
            this.addr.Text = "IP和端口";
            this.addr.UseVisualStyleBackColor = true;
            this.addr.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // rss
            // 
            this.rss.AutoSize = true;
            this.rss.Location = new System.Drawing.Point(363, 21);
            this.rss.Margin = new System.Windows.Forms.Padding(4);
            this.rss.Name = "rss";
            this.rss.Size = new System.Drawing.Size(105, 22);
            this.rss.TabIndex = 10021;
            this.rss.Text = "订阅地址";
            this.rss.UseVisualStyleBackColor = true;
            this.rss.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // ss
            // 
            this.ss.AutoSize = true;
            this.ss.Checked = true;
            this.ss.Location = new System.Drawing.Point(9, 21);
            this.ss.Margin = new System.Windows.Forms.Padding(4);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(177, 22);
            this.ss.TabIndex = 10020;
            this.ss.TabStop = true;
            this.ss.Text = "服务器(分享)地址";
            this.ss.UseVisualStyleBackColor = true;
            this.ss.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // label2
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(33, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label2";
            this.label1.Size = new System.Drawing.Size(395, 18);
            this.label1.TabIndex = 10034;
            this.label1.Text = "* 格式:一行一个, IP和端口, : 或 空白符 分割";
            this.label1.Visible = false;
            // 
            // label3
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(33, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label3";
            this.label2.Size = new System.Drawing.Size(278, 18);
            this.label2.TabIndex = 10035;
            this.label2.Text = "* 格式:一行一个,飞机场订阅地址";
            this.label2.Visible = false;
            // 
            // BatchAddVPNServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(656, 488);
            this.Controls.Add(this.label0);
            this.Controls.Add(this.label104);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label100);
            this.Controls.Add(this.label101);
            this.Controls.Add(this.wsTextBox);
            this.Controls.Add(this.pasteModeCB);
            this.Controls.Add(this.browserButton);
            this.Controls.Add(this.filePathTextBox);
            this.Controls.Add(this.label102);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BatchAddVPNServerForm";
            this.Text = "批量添加";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label102, 0);
            this.Controls.SetChildIndex(this.filePathTextBox, 0);
            this.Controls.SetChildIndex(this.browserButton, 0);
            this.Controls.SetChildIndex(this.pasteModeCB, 0);
            this.Controls.SetChildIndex(this.wsTextBox, 0);
            this.Controls.SetChildIndex(this.label101, 0);
            this.Controls.SetChildIndex(this.label100, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label104, 0);
            this.Controls.SetChildIndex(this.label0, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.TextBox wsTextBox;
        private System.Windows.Forms.CheckBox pasteModeCB;
        private System.Windows.Forms.Button browserButton;
        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rss;
        private System.Windows.Forms.RadioButton ss;
        private System.Windows.Forms.RadioButton addr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}