namespace C2.IAOLab.WebEngine.Dialogs
{
    partial class SelectBossDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.datasource = new System.Windows.Forms.ComboBox();
            this.bossType = new System.Windows.Forms.ComboBox();
            this.simpleBarX = new System.Windows.Forms.ComboBox();
            this.captionBar1 = new C2.Controls.CaptionBar();
            this.captionBar2 = new C2.Controls.CaptionBar();
            this.label3 = new System.Windows.Forms.Label();
            this.stackBarX = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.basicPieY = new System.Windows.Forms.ComboBox();
            this.captionBar3 = new C2.Controls.CaptionBar();
            this.captionBar4 = new C2.Controls.CaptionBar();
            this.captionBar5 = new C2.Controls.CaptionBar();
            this.basicMapX = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.basicMapY = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label16 = new System.Windows.Forms.Label();
            this.captionBar6 = new C2.Controls.CaptionBar();
            this.captionBar7 = new C2.Controls.CaptionBar();
            this.simpleBarY = new C2.Controls.Common.ComCheckBoxList();
            this.basicLineChartY = new C2.Controls.Common.ComCheckBoxList();
            this.basicLineChartX = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.smoothedLineChartY = new C2.Controls.Common.ComCheckBoxList();
            this.smoothedLineChartX = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.basicScatterY = new C2.Controls.Common.ComCheckBoxList();
            this.basicScatterX = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.stackBarY = new C2.Controls.Common.ComCheckBoxList();
            this.basicPieX = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 10003;
            this.label1.Text = "数据源：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 10004;
            this.label2.Text = "大屏类型：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(505, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 10006;
            this.label4.Text = "X轴：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(724, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 10007;
            this.label5.Text = "Y轴：";
            // 
            // datasource
            // 
            this.datasource.BackColor = System.Drawing.SystemColors.Window;
            this.datasource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.datasource.FormattingEnabled = true;
            this.datasource.Location = new System.Drawing.Point(98, 30);
            this.datasource.Name = "datasource";
            this.datasource.Size = new System.Drawing.Size(330, 20);
            this.datasource.TabIndex = 10008;
            this.datasource.SelectedIndexChanged += new System.EventHandler(this.Datasource_SelectedIndexChanged);
            // 
            // bossType
            // 
            this.bossType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bossType.FormattingEnabled = true;
            this.bossType.Items.AddRange(new object[] {
            "样式1",
            "样式2",
            "样式3"});
            this.bossType.Location = new System.Drawing.Point(98, 74);
            this.bossType.Name = "bossType";
            this.bossType.Size = new System.Drawing.Size(330, 20);
            this.bossType.TabIndex = 10009;
            // 
            // simpleBarX
            // 
            this.simpleBarX.FormattingEnabled = true;
            this.simpleBarX.Location = new System.Drawing.Point(550, 42);
            this.simpleBarX.Name = "simpleBarX";
            this.simpleBarX.Size = new System.Drawing.Size(136, 20);
            this.simpleBarX.TabIndex = 10010;
            // 
            // captionBar1
            // 
            this.captionBar1.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.captionBar1.Location = new System.Drawing.Point(481, 15);
            this.captionBar1.Name = "captionBar1";
            this.captionBar1.Size = new System.Drawing.Size(440, 22);
            this.captionBar1.TabIndex = 10012;
            this.captionBar1.Text = "柱状图";
            // 
            // captionBar2
            // 
            this.captionBar2.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.captionBar2.Location = new System.Drawing.Point(481, 72);
            this.captionBar2.Name = "captionBar2";
            this.captionBar2.Size = new System.Drawing.Size(440, 22);
            this.captionBar2.TabIndex = 10013;
            this.captionBar2.Text = "堆叠柱状图";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(505, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 10014;
            this.label3.Text = "X轴：";
            // 
            // stackBarX
            // 
            this.stackBarX.FormattingEnabled = true;
            this.stackBarX.Location = new System.Drawing.Point(550, 99);
            this.stackBarX.Name = "stackBarX";
            this.stackBarX.Size = new System.Drawing.Size(136, 20);
            this.stackBarX.TabIndex = 10015;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(724, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 10016;
            this.label6.Text = "Y轴：";
            // 
            // basicPieY
            // 
            this.basicPieY.FormattingEnabled = true;
            this.basicPieY.Location = new System.Drawing.Point(769, 347);
            this.basicPieY.Name = "basicPieY";
            this.basicPieY.Size = new System.Drawing.Size(136, 20);
            this.basicPieY.TabIndex = 10017;
            // 
            // captionBar3
            // 
            this.captionBar3.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.captionBar3.Location = new System.Drawing.Point(481, 192);
            this.captionBar3.Name = "captionBar3";
            this.captionBar3.Size = new System.Drawing.Size(440, 22);
            this.captionBar3.TabIndex = 10018;
            this.captionBar3.Text = "曲线图";
            // 
            // captionBar4
            // 
            this.captionBar4.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.captionBar4.Location = new System.Drawing.Point(481, 256);
            this.captionBar4.Name = "captionBar4";
            this.captionBar4.Size = new System.Drawing.Size(440, 22);
            this.captionBar4.TabIndex = 10023;
            this.captionBar4.Text = "散点图";
            // 
            // captionBar5
            // 
            this.captionBar5.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.captionBar5.Location = new System.Drawing.Point(481, 382);
            this.captionBar5.Name = "captionBar5";
            this.captionBar5.Size = new System.Drawing.Size(440, 22);
            this.captionBar5.TabIndex = 10024;
            this.captionBar5.Text = "地市分布图";
            // 
            // basicMapX
            // 
            this.basicMapX.FormattingEnabled = true;
            this.basicMapX.Location = new System.Drawing.Point(550, 410);
            this.basicMapX.Name = "basicMapX";
            this.basicMapX.Size = new System.Drawing.Size(136, 20);
            this.basicMapX.TabIndex = 10028;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(505, 413);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 10027;
            this.label10.Text = "地市：";
            // 
            // basicMapY
            // 
            this.basicMapY.FormattingEnabled = true;
            this.basicMapY.Location = new System.Drawing.Point(769, 410);
            this.basicMapY.Name = "basicMapY";
            this.basicMapY.Size = new System.Drawing.Size(136, 20);
            this.basicMapY.TabIndex = 10032;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(724, 413);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 10031;
            this.label12.Text = "数值：";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::C2.Properties.Resources.BossStyle01;
            this.pictureBox1.Location = new System.Drawing.Point(12, 153);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(451, 272);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10033;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(26, 123);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 10044;
            this.label16.Text = "预览图：";
            // 
            // captionBar6
            // 
            this.captionBar6.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.captionBar6.Location = new System.Drawing.Point(481, 320);
            this.captionBar6.Name = "captionBar6";
            this.captionBar6.Size = new System.Drawing.Size(440, 22);
            this.captionBar6.TabIndex = 10045;
            this.captionBar6.Text = "饼状图";
            // 
            // captionBar7
            // 
            this.captionBar7.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.captionBar7.Location = new System.Drawing.Point(481, 132);
            this.captionBar7.Name = "captionBar7";
            this.captionBar7.Size = new System.Drawing.Size(440, 22);
            this.captionBar7.TabIndex = 10052;
            this.captionBar7.Text = "折线图";
            // 
            // simpleBarY
            // 
            this.simpleBarY.DataSource = null;
            this.simpleBarY.Location = new System.Drawing.Point(769, 42);
            this.simpleBarY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.simpleBarY.Name = "simpleBarY";
            this.simpleBarY.Size = new System.Drawing.Size(136, 23);
            this.simpleBarY.TabIndex = 10057;
            // 
            // basicLineChartY
            // 
            this.basicLineChartY.DataSource = null;
            this.basicLineChartY.Location = new System.Drawing.Point(769, 159);
            this.basicLineChartY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.basicLineChartY.Name = "basicLineChartY";
            this.basicLineChartY.Size = new System.Drawing.Size(136, 23);
            this.basicLineChartY.TabIndex = 10061;
            // 
            // basicLineChartX
            // 
            this.basicLineChartX.FormattingEnabled = true;
            this.basicLineChartX.Location = new System.Drawing.Point(550, 159);
            this.basicLineChartX.Name = "basicLineChartX";
            this.basicLineChartX.Size = new System.Drawing.Size(136, 20);
            this.basicLineChartX.TabIndex = 10060;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(724, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 10059;
            this.label7.Text = "Y轴：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(505, 162);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 10058;
            this.label8.Text = "X轴：";
            // 
            // smoothedLineChartY
            // 
            this.smoothedLineChartY.DataSource = null;
            this.smoothedLineChartY.Location = new System.Drawing.Point(769, 219);
            this.smoothedLineChartY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.smoothedLineChartY.Name = "smoothedLineChartY";
            this.smoothedLineChartY.Size = new System.Drawing.Size(136, 23);
            this.smoothedLineChartY.TabIndex = 10065;
            // 
            // smoothedLineChartX
            // 
            this.smoothedLineChartX.FormattingEnabled = true;
            this.smoothedLineChartX.Location = new System.Drawing.Point(550, 219);
            this.smoothedLineChartX.Name = "smoothedLineChartX";
            this.smoothedLineChartX.Size = new System.Drawing.Size(136, 20);
            this.smoothedLineChartX.TabIndex = 10064;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(724, 222);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 10063;
            this.label9.Text = "Y轴：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(505, 222);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 12);
            this.label11.TabIndex = 10062;
            this.label11.Text = "X轴：";
            // 
            // basicScatterY
            // 
            this.basicScatterY.DataSource = null;
            this.basicScatterY.Location = new System.Drawing.Point(769, 283);
            this.basicScatterY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.basicScatterY.Name = "basicScatterY";
            this.basicScatterY.Size = new System.Drawing.Size(136, 23);
            this.basicScatterY.TabIndex = 10069;
            // 
            // basicScatterX
            // 
            this.basicScatterX.FormattingEnabled = true;
            this.basicScatterX.Location = new System.Drawing.Point(550, 283);
            this.basicScatterX.Name = "basicScatterX";
            this.basicScatterX.Size = new System.Drawing.Size(136, 20);
            this.basicScatterX.TabIndex = 10068;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(724, 286);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 12);
            this.label13.TabIndex = 10067;
            this.label13.Text = "Y轴：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(505, 286);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(35, 12);
            this.label14.TabIndex = 10066;
            this.label14.Text = "X轴：";
            // 
            // stackBarY
            // 
            this.stackBarY.DataSource = null;
            this.stackBarY.Location = new System.Drawing.Point(769, 99);
            this.stackBarY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.stackBarY.Name = "stackBarY";
            this.stackBarY.Size = new System.Drawing.Size(136, 23);
            this.stackBarY.TabIndex = 10073;
            // 
            // basicPieX
            // 
            this.basicPieX.FormattingEnabled = true;
            this.basicPieX.Location = new System.Drawing.Point(550, 347);
            this.basicPieX.Name = "basicPieX";
            this.basicPieX.Size = new System.Drawing.Size(136, 20);
            this.basicPieX.TabIndex = 10072;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(724, 350);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 10071;
            this.label15.Text = "数值：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(505, 350);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 10070;
            this.label17.Text = "扇区：";
            // 
            // SelectBossDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(928, 491);
            this.Controls.Add(this.stackBarY);
            this.Controls.Add(this.basicPieX);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.basicScatterY);
            this.Controls.Add(this.basicScatterX);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.smoothedLineChartY);
            this.Controls.Add(this.smoothedLineChartX);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.basicLineChartY);
            this.Controls.Add(this.basicLineChartX);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.simpleBarY);
            this.Controls.Add(this.captionBar7);
            this.Controls.Add(this.captionBar6);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.basicMapY);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.basicMapX);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.captionBar5);
            this.Controls.Add(this.captionBar4);
            this.Controls.Add(this.captionBar3);
            this.Controls.Add(this.basicPieY);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.stackBarX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.captionBar2);
            this.Controls.Add(this.captionBar1);
            this.Controls.Add(this.simpleBarX);
            this.Controls.Add(this.bossType);
            this.Controls.Add(this.datasource);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SelectBossDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数配置";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.datasource, 0);
            this.Controls.SetChildIndex(this.bossType, 0);
            this.Controls.SetChildIndex(this.simpleBarX, 0);
            this.Controls.SetChildIndex(this.captionBar1, 0);
            this.Controls.SetChildIndex(this.captionBar2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.stackBarX, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.basicPieY, 0);
            this.Controls.SetChildIndex(this.captionBar3, 0);
            this.Controls.SetChildIndex(this.captionBar4, 0);
            this.Controls.SetChildIndex(this.captionBar5, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.basicMapX, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.basicMapY, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.captionBar6, 0);
            this.Controls.SetChildIndex(this.captionBar7, 0);
            this.Controls.SetChildIndex(this.simpleBarY, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.basicLineChartX, 0);
            this.Controls.SetChildIndex(this.basicLineChartY, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.smoothedLineChartX, 0);
            this.Controls.SetChildIndex(this.smoothedLineChartY, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.basicScatterX, 0);
            this.Controls.SetChildIndex(this.basicScatterY, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.Controls.SetChildIndex(this.basicPieX, 0);
            this.Controls.SetChildIndex(this.stackBarY, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox datasource;
        private System.Windows.Forms.ComboBox bossType;
        private System.Windows.Forms.ComboBox simpleBarX;
        private Controls.CaptionBar captionBar1;
        private Controls.CaptionBar captionBar2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox stackBarX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox basicPieY;
        private Controls.CaptionBar captionBar3;
        private Controls.CaptionBar captionBar4;
        private Controls.CaptionBar captionBar5;
        private System.Windows.Forms.ComboBox basicMapX;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox basicMapY;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label16;
        private Controls.CaptionBar captionBar6;
        private Controls.CaptionBar captionBar7;
        private Controls.Common.ComCheckBoxList simpleBarY;
        private Controls.Common.ComCheckBoxList basicLineChartY;
        private System.Windows.Forms.ComboBox basicLineChartX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private Controls.Common.ComCheckBoxList smoothedLineChartY;
        private System.Windows.Forms.ComboBox smoothedLineChartX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private Controls.Common.ComCheckBoxList basicScatterY;
        private System.Windows.Forms.ComboBox basicScatterX;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private Controls.Common.ComCheckBoxList stackBarY;
        private System.Windows.Forms.ComboBox basicPieX;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
    }
}