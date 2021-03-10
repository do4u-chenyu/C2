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
            this.simpleBarCaption = new C2.Controls.CaptionBar();
            this.stackBarCaption = new C2.Controls.CaptionBar();
            this.label3 = new System.Windows.Forms.Label();
            this.stackBarX = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.basicPieY = new System.Windows.Forms.ComboBox();
            this.smoothedLineChartCaption = new C2.Controls.CaptionBar();
            this.basicScatterCaption = new C2.Controls.CaptionBar();
            this.basicMapCaption = new C2.Controls.CaptionBar();
            this.basicMapX = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.basicMapY = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label16 = new System.Windows.Forms.Label();
            this.basicPieCaption = new C2.Controls.CaptionBar();
            this.basicLineChartCaption = new C2.Controls.CaptionBar();
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(26, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 10003;
            this.label1.Text = "数据源：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(14, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 10004;
            this.label2.Text = "大屏类型：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(27, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 15);
            this.label4.TabIndex = 10006;
            this.label4.Text = "X轴：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(246, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 15);
            this.label5.TabIndex = 10007;
            this.label5.Text = "Y轴：";
            // 
            // datasource
            // 
            this.datasource.BackColor = System.Drawing.SystemColors.Window;
            this.datasource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.datasource.FormattingEnabled = true;
            this.datasource.Location = new System.Drawing.Point(98, 29);
            this.datasource.Name = "datasource";
            this.datasource.Size = new System.Drawing.Size(368, 20);
            this.datasource.TabIndex = 10008;
            this.datasource.SelectedIndexChanged += new System.EventHandler(this.Datasource_SelectedIndexChanged);
            // 
            // bossType
            // 
            this.bossType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bossType.FormattingEnabled = true;
            this.bossType.Items.AddRange(new object[] {
            "样式1（丰富，内含7种图表）",
            "样式2（简约，内含4种图表）",
            "样式3（科技，内含5种图表）"});
            this.bossType.Location = new System.Drawing.Point(98, 74);
            this.bossType.Name = "bossType";
            this.bossType.Size = new System.Drawing.Size(368, 20);
            this.bossType.TabIndex = 10009;
            this.bossType.SelectedIndexChanged += new System.EventHandler(this.BossType_SelectedIndexChanged);
            // 
            // simpleBarX
            // 
            this.simpleBarX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.simpleBarX.FormattingEnabled = true;
            this.simpleBarX.Location = new System.Drawing.Point(73, 49);
            this.simpleBarX.Name = "simpleBarX";
            this.simpleBarX.Size = new System.Drawing.Size(136, 20);
            this.simpleBarX.TabIndex = 10010;
            // 
            // simpleBarCaption
            // 
            this.simpleBarCaption.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.simpleBarCaption.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.simpleBarCaption.Location = new System.Drawing.Point(3, 18);
            this.simpleBarCaption.Name = "simpleBarCaption";
            this.simpleBarCaption.Size = new System.Drawing.Size(440, 22);
            this.simpleBarCaption.TabIndex = 10012;
            this.simpleBarCaption.Text = "柱状图";
            // 
            // captionBar2
            // 
            this.stackBarCaption.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.stackBarCaption.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stackBarCaption.Location = new System.Drawing.Point(3, 198);
            this.stackBarCaption.Name = "captionBar2";
            this.stackBarCaption.Size = new System.Drawing.Size(440, 22);
            this.stackBarCaption.TabIndex = 10013;
            this.stackBarCaption.Text = "堆叠柱状图";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(27, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 10014;
            this.label3.Text = "X轴：";
            // 
            // stackBarX
            // 
            this.stackBarX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stackBarX.FormattingEnabled = true;
            this.stackBarX.Location = new System.Drawing.Point(73, 229);
            this.stackBarX.Name = "stackBarX";
            this.stackBarX.Size = new System.Drawing.Size(136, 20);
            this.stackBarX.TabIndex = 10015;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(246, 231);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 15);
            this.label6.TabIndex = 10016;
            this.label6.Text = "Y轴：";
            // 
            // basicPieY
            // 
            this.basicPieY.FormattingEnabled = true;
            this.basicPieY.Location = new System.Drawing.Point(298, 501);
            this.basicPieY.Name = "basicPieY";
            this.basicPieY.Size = new System.Drawing.Size(136, 20);
            this.basicPieY.TabIndex = 10017;
            // 
            // smoothedLineChartCaption
            // 
            this.smoothedLineChartCaption.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.smoothedLineChartCaption.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.smoothedLineChartCaption.Location = new System.Drawing.Point(3, 288);
            this.smoothedLineChartCaption.Name = "smoothedLineChartCaption";
            this.smoothedLineChartCaption.Size = new System.Drawing.Size(440, 22);
            this.smoothedLineChartCaption.TabIndex = 10018;
            this.smoothedLineChartCaption.Text = "曲线图";
            // 
            // basicScatterCaption
            // 
            this.basicScatterCaption.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.basicScatterCaption.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.basicScatterCaption.Location = new System.Drawing.Point(3, 378);
            this.basicScatterCaption.Name = "basicScatterCaption";
            this.basicScatterCaption.Size = new System.Drawing.Size(440, 22);
            this.basicScatterCaption.TabIndex = 10023;
            this.basicScatterCaption.Text = "散点图";
            // 
            // captionBar5
            // 
            this.basicMapCaption.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.basicMapCaption.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.basicMapCaption.Location = new System.Drawing.Point(3, 558);
            this.basicMapCaption.Name = "captionBar5";
            this.basicMapCaption.Size = new System.Drawing.Size(440, 22);
            this.basicMapCaption.TabIndex = 10024;
            this.basicMapCaption.Text = "地市分布图";
            // 
            // basicMapX
            // 
            this.basicMapX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.basicMapX.FormattingEnabled = true;
            this.basicMapX.Location = new System.Drawing.Point(73, 589);
            this.basicMapX.Name = "basicMapX";
            this.basicMapX.Size = new System.Drawing.Size(136, 20);
            this.basicMapX.TabIndex = 10028;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(21, 591);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 15);
            this.label10.TabIndex = 10027;
            this.label10.Text = "地市：";
            // 
            // basicMapY
            // 
            this.basicMapY.FormattingEnabled = true;
            this.basicMapY.Location = new System.Drawing.Point(298, 589);
            this.basicMapY.Name = "basicMapY";
            this.basicMapY.Size = new System.Drawing.Size(136, 20);
            this.basicMapY.TabIndex = 10032;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(246, 591);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 15);
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
            this.pictureBox1.Location = new System.Drawing.Point(12, 164);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(473, 261);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10033;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(26, 123);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 16);
            this.label16.TabIndex = 10044;
            this.label16.Text = "预览图：";
            // 
            // basicPieCaption
            // 
            this.basicPieCaption.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.basicPieCaption.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.basicPieCaption.Location = new System.Drawing.Point(3, 468);
            this.basicPieCaption.Name = "basicPieCaption";
            this.basicPieCaption.Size = new System.Drawing.Size(440, 22);
            this.basicPieCaption.TabIndex = 10045;
            this.basicPieCaption.Text = "饼状图";
            // 
            // captionBar7
            // 
            this.basicLineChartCaption.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.basicLineChartCaption.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.basicLineChartCaption.Location = new System.Drawing.Point(3, 108);
            this.basicLineChartCaption.Name = "captionBar7";
            this.basicLineChartCaption.Size = new System.Drawing.Size(440, 22);
            this.basicLineChartCaption.TabIndex = 10052;
            this.basicLineChartCaption.Text = "折线图";
            // 
            // simpleBarY
            // 
            this.simpleBarY.DataSource = null;
            this.simpleBarY.Location = new System.Drawing.Point(298, 49);
            this.simpleBarY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.simpleBarY.Name = "simpleBarY";
            this.simpleBarY.Size = new System.Drawing.Size(136, 23);
            this.simpleBarY.TabIndex = 10057;
            // 
            // basicLineChartY
            // 
            this.basicLineChartY.DataSource = null;
            this.basicLineChartY.Location = new System.Drawing.Point(298, 140);
            this.basicLineChartY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.basicLineChartY.Name = "basicLineChartY";
            this.basicLineChartY.Size = new System.Drawing.Size(136, 23);
            this.basicLineChartY.TabIndex = 10061;
            // 
            // basicLineChartX
            // 
            this.basicLineChartX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.basicLineChartX.FormattingEnabled = true;
            this.basicLineChartX.Location = new System.Drawing.Point(73, 140);
            this.basicLineChartX.Name = "basicLineChartX";
            this.basicLineChartX.Size = new System.Drawing.Size(136, 20);
            this.basicLineChartX.TabIndex = 10060;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(246, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 15);
            this.label7.TabIndex = 10059;
            this.label7.Text = "Y轴：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(27, 142);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 15);
            this.label8.TabIndex = 10058;
            this.label8.Text = "X轴：";
            // 
            // smoothedLineChartY
            // 
            this.smoothedLineChartY.DataSource = null;
            this.smoothedLineChartY.Location = new System.Drawing.Point(298, 319);
            this.smoothedLineChartY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.smoothedLineChartY.Name = "smoothedLineChartY";
            this.smoothedLineChartY.Size = new System.Drawing.Size(136, 23);
            this.smoothedLineChartY.TabIndex = 10065;
            // 
            // smoothedLineChartX
            // 
            this.smoothedLineChartX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.smoothedLineChartX.FormattingEnabled = true;
            this.smoothedLineChartX.Location = new System.Drawing.Point(73, 319);
            this.smoothedLineChartX.Name = "smoothedLineChartX";
            this.smoothedLineChartX.Size = new System.Drawing.Size(136, 20);
            this.smoothedLineChartX.TabIndex = 10064;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(246, 321);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 15);
            this.label9.TabIndex = 10063;
            this.label9.Text = "Y轴：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(27, 321);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 15);
            this.label11.TabIndex = 10062;
            this.label11.Text = "X轴：";
            // 
            // basicScatterY
            // 
            this.basicScatterY.DataSource = null;
            this.basicScatterY.Location = new System.Drawing.Point(298, 410);
            this.basicScatterY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.basicScatterY.Name = "basicScatterY";
            this.basicScatterY.Size = new System.Drawing.Size(136, 23);
            this.basicScatterY.TabIndex = 10069;
            // 
            // basicScatterX
            // 
            this.basicScatterX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.basicScatterX.FormattingEnabled = true;
            this.basicScatterX.Location = new System.Drawing.Point(73, 410);
            this.basicScatterX.Name = "basicScatterX";
            this.basicScatterX.Size = new System.Drawing.Size(136, 20);
            this.basicScatterX.TabIndex = 10068;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(246, 412);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 15);
            this.label13.TabIndex = 10067;
            this.label13.Text = "Y轴：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(27, 412);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 15);
            this.label14.TabIndex = 10066;
            this.label14.Text = "X轴：";
            // 
            // stackBarY
            // 
            this.stackBarY.DataSource = null;
            this.stackBarY.Location = new System.Drawing.Point(298, 229);
            this.stackBarY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.stackBarY.Name = "stackBarY";
            this.stackBarY.Size = new System.Drawing.Size(136, 23);
            this.stackBarY.TabIndex = 10073;
            // 
            // basicPieX
            // 
            this.basicPieX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.basicPieX.FormattingEnabled = true;
            this.basicPieX.Location = new System.Drawing.Point(73, 501);
            this.basicPieX.Name = "basicPieX";
            this.basicPieX.Size = new System.Drawing.Size(136, 20);
            this.basicPieX.TabIndex = 10072;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(246, 503);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 15);
            this.label15.TabIndex = 10071;
            this.label15.Text = "数值：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(21, 503);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 15);
            this.label17.TabIndex = 10070;
            this.label17.Text = "扇区：";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.basicScatterCaption);
            this.panel1.Controls.Add(this.stackBarY);
            this.panel1.Controls.Add(this.basicPieY);
            this.panel1.Controls.Add(this.simpleBarY);
            this.panel1.Controls.Add(this.basicPieX);
            this.panel1.Controls.Add(this.smoothedLineChartCaption);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.basicMapCaption);
            this.panel1.Controls.Add(this.stackBarX);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.stackBarCaption);
            this.panel1.Controls.Add(this.basicScatterY);
            this.panel1.Controls.Add(this.simpleBarCaption);
            this.panel1.Controls.Add(this.simpleBarX);
            this.panel1.Controls.Add(this.basicMapX);
            this.panel1.Controls.Add(this.basicScatterX);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.basicMapY);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.basicPieCaption);
            this.panel1.Controls.Add(this.smoothedLineChartY);
            this.panel1.Controls.Add(this.basicLineChartCaption);
            this.panel1.Controls.Add(this.smoothedLineChartX);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.basicLineChartX);
            this.panel1.Controls.Add(this.basicLineChartY);
            this.panel1.Location = new System.Drawing.Point(526, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 369);
            this.panel1.TabIndex = 10074;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(526, 33);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(120, 16);
            this.label18.TabIndex = 10075;
            this.label18.Text = "图表参数配置：";
            // 
            // SelectBossDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(994, 491);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.bossType);
            this.Controls.Add(this.datasource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SelectBossDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数配置";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.datasource, 0);
            this.Controls.SetChildIndex(this.bossType, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.label18, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private Controls.CaptionBar simpleBarCaption;
        private Controls.CaptionBar stackBarCaption;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox stackBarX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox basicPieY;
        private Controls.CaptionBar smoothedLineChartCaption;
        private Controls.CaptionBar basicScatterCaption;
        private Controls.CaptionBar basicMapCaption;
        private System.Windows.Forms.ComboBox basicMapX;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox basicMapY;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label16;
        private Controls.CaptionBar basicPieCaption;
        private Controls.CaptionBar basicLineChartCaption;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label18;
    }
}