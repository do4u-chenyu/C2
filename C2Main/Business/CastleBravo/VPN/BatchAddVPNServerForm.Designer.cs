
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
            this.clash = new System.Windows.Forms.RadioButton();
            this.addr = new System.Windows.Forms.RadioButton();
            this.rss = new System.Windows.Forms.RadioButton();
            this.ss = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rssPB = new System.Windows.Forms.ProgressBar();
            this.rssLable = new System.Windows.Forms.Label();
            this.rssLB = new System.Windows.Forms.Label();
            this.vpnPB = new System.Windows.Forms.Label();
            this.vpnLB = new System.Windows.Forms.Label();
            this.sucPB = new System.Windows.Forms.Label();
            this.sucLB = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.label0.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label0.ForeColor = System.Drawing.Color.Red;
            this.label0.Location = new System.Drawing.Point(33, 80);
            this.label0.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(521, 18);
            this.label0.TabIndex = 10029;
            this.label0.Text = "* 格式:一行一个,支持ss,ssr,vmess,vless,trojan五种梯子地址\r\n";
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
            this.wsTextBox.Size = new System.Drawing.Size(567, 236);
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
            this.browserButton.Location = new System.Drawing.Point(621, 35);
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
            this.filePathTextBox.Size = new System.Drawing.Size(469, 33);
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
            this.groupBox1.Controls.Add(this.clash);
            this.groupBox1.Controls.Add(this.addr);
            this.groupBox1.Controls.Add(this.rss);
            this.groupBox1.Controls.Add(this.ss);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(138, 99);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(571, 54);
            this.groupBox1.TabIndex = 10033;
            this.groupBox1.TabStop = false;
            // 
            // clash
            // 
            this.clash.AutoSize = true;
            this.clash.Font = new System.Drawing.Font("宋体", 9F);
            this.clash.Location = new System.Drawing.Point(461, 21);
            this.clash.Margin = new System.Windows.Forms.Padding(4);
            this.clash.Name = "clash";
            this.clash.Size = new System.Drawing.Size(114, 22);
            this.clash.TabIndex = 10023;
            this.clash.Text = "Clash订阅";
            this.clash.UseVisualStyleBackColor = true;
            this.clash.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // addr
            // 
            this.addr.AutoSize = true;
            this.addr.Font = new System.Drawing.Font("宋体", 9F);
            this.addr.Location = new System.Drawing.Point(119, 21);
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
            this.rss.Font = new System.Drawing.Font("宋体", 9F);
            this.rss.Location = new System.Drawing.Point(236, 21);
            this.rss.Margin = new System.Windows.Forms.Padding(4);
            this.rss.Name = "rss";
            this.rss.Size = new System.Drawing.Size(213, 22);
            this.rss.TabIndex = 10021;
            this.rss.Text = "V2ray/小火箭/SSR订阅";
            this.rss.UseVisualStyleBackColor = true;
            this.rss.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // ss
            // 
            this.ss.AutoSize = true;
            this.ss.Checked = true;
            this.ss.Font = new System.Drawing.Font("宋体", 9F);
            this.ss.Location = new System.Drawing.Point(2, 21);
            this.ss.Margin = new System.Windows.Forms.Padding(4);
            this.ss.Name = "ss";
            this.ss.Size = new System.Drawing.Size(105, 22);
            this.ss.TabIndex = 10020;
            this.ss.TabStop = true;
            this.ss.Text = "梯子地址";
            this.ss.UseVisualStyleBackColor = true;
            this.ss.Click += new System.EventHandler(this.RadioButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(33, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(395, 18);
            this.label1.TabIndex = 10034;
            this.label1.Text = "* 格式:一行一个, IP和端口, : 或 空白符 分割";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(33, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(557, 18);
            this.label2.TabIndex = 10035;
            this.label2.Text = "* 格式:一行一个,V2ray/Shadowrocket/SSR订阅地址,http(s)://开头";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(33, 80);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(431, 18);
            this.label3.TabIndex = 10036;
            this.label3.Text = "* 格式:一行一个,Clash(R)订阅地址,http(s)://开头";
            this.label3.Visible = false;
            // 
            // rssPB
            // 
            this.rssPB.Location = new System.Drawing.Point(5, 379);
            this.rssPB.Name = "rssPB";
            this.rssPB.Size = new System.Drawing.Size(120, 23);
            this.rssPB.TabIndex = 10037;
            this.rssPB.Visible = false;
            // 
            // rssLable
            // 
            this.rssLable.AutoSize = true;
            this.rssLable.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rssLable.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.rssLable.Location = new System.Drawing.Point(0, 352);
            this.rssLable.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.rssLable.Name = "rssLable";
            this.rssLable.Size = new System.Drawing.Size(80, 18);
            this.rssLable.TabIndex = 10038;
            this.rssLable.Text = "解析进度";
            this.rssLable.Visible = false;
            // 
            // rssLB
            // 
            this.rssLB.AutoSize = true;
            this.rssLB.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rssLB.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.rssLB.Location = new System.Drawing.Point(77, 352);
            this.rssLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rssLB.Name = "rssLB";
            this.rssLB.Size = new System.Drawing.Size(35, 18);
            this.rssLB.TabIndex = 10039;
            this.rssLB.Text = "0/1";
            this.rssLB.Visible = false;
            // 
            // vpnPB
            // 
            this.vpnPB.AutoSize = true;
            this.vpnPB.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.vpnPB.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.vpnPB.Location = new System.Drawing.Point(0, 324);
            this.vpnPB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.vpnPB.Name = "vpnPB";
            this.vpnPB.Size = new System.Drawing.Size(80, 18);
            this.vpnPB.TabIndex = 10040;
            this.vpnPB.Text = "梯子总数";
            this.vpnPB.Visible = false;
            // 
            // vpnLB
            // 
            this.vpnLB.AutoSize = true;
            this.vpnLB.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.vpnLB.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.vpnLB.Location = new System.Drawing.Point(77, 324);
            this.vpnLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.vpnLB.Name = "vpnLB";
            this.vpnLB.Size = new System.Drawing.Size(17, 18);
            this.vpnLB.TabIndex = 10041;
            this.vpnLB.Text = "0";
            this.vpnLB.Visible = false;
            // 
            // sucPB
            // 
            this.sucPB.AutoSize = true;
            this.sucPB.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sucPB.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.sucPB.Location = new System.Drawing.Point(0, 296);
            this.sucPB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sucPB.Name = "sucPB";
            this.sucPB.Size = new System.Drawing.Size(80, 18);
            this.sucPB.TabIndex = 10042;
            this.sucPB.Text = "订阅成比";
            this.sucPB.Visible = false;
            // 
            // sucLB
            // 
            this.sucLB.AutoSize = true;
            this.sucLB.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sucLB.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.sucLB.Location = new System.Drawing.Point(77, 296);
            this.sucLB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sucLB.Name = "sucLB";
            this.sucLB.Size = new System.Drawing.Size(35, 18);
            this.sucLB.TabIndex = 10043;
            this.sucLB.Text = "0/1";
            this.sucLB.Visible = false;
            // 
            // BatchAddVPNServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(722, 488);
            this.Controls.Add(this.sucLB);
            this.Controls.Add(this.sucPB);
            this.Controls.Add(this.vpnLB);
            this.Controls.Add(this.rssLB);
            this.Controls.Add(this.rssLable);
            this.Controls.Add(this.rssPB);
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
            this.Controls.Add(this.label3);
            this.Controls.Add(this.vpnPB);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BatchAddVPNServerForm";
            this.Text = "批量添加";
            this.Controls.SetChildIndex(this.vpnPB, 0);
            this.Controls.SetChildIndex(this.label3, 0);
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
            this.Controls.SetChildIndex(this.rssPB, 0);
            this.Controls.SetChildIndex(this.rssLable, 0);
            this.Controls.SetChildIndex(this.rssLB, 0);
            this.Controls.SetChildIndex(this.vpnLB, 0);
            this.Controls.SetChildIndex(this.sucPB, 0);
            this.Controls.SetChildIndex(this.sucLB, 0);
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
        private System.Windows.Forms.RadioButton clash;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar rssPB;
        private System.Windows.Forms.Label rssLable;
        private System.Windows.Forms.Label rssLB;
        private System.Windows.Forms.Label vpnPB;
        private System.Windows.Forms.Label vpnLB;
        private System.Windows.Forms.Label sucPB;
        private System.Windows.Forms.Label sucLB;
    }
}