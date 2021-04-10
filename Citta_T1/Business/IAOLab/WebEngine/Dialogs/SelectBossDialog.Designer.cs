using C2.Globalization;

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
            this.gradientLineChartCaption = new C2.Controls.CaptionBar();
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
            this.gradientLineChartY = new C2.Controls.Common.ComCheckBoxList();
            this.gradientLineChartX = new System.Windows.Forms.ComboBox();
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
            this.gradientLineChartPanel = new System.Windows.Forms.Panel();
            this.stackBarPanel = new System.Windows.Forms.Panel();
            this.basicScatterPanel = new System.Windows.Forms.Panel();
            this.basicLineChartPanel = new System.Windows.Forms.Panel();
            this.simpleBarPanel = new System.Windows.Forms.Panel();
            this.pictorialBarCaption = new C2.Controls.CaptionBar();
            this.label20 = new System.Windows.Forms.Label();
            this.pictorialBarX = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.pictorialBarY = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.previewBtn = new System.Windows.Forms.Button();
            this.zoomInBtn = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.basicPiePanel = new System.Windows.Forms.Panel();
            this.pictorialBarPanel = new System.Windows.Forms.Panel();
            this.basicMapPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.gradientLineChartPanel.SuspendLayout();
            this.stackBarPanel.SuspendLayout();
            this.basicScatterPanel.SuspendLayout();
            this.basicLineChartPanel.SuspendLayout();
            this.simpleBarPanel.SuspendLayout();
            this.basicPiePanel.SuspendLayout();
            this.pictorialBarPanel.SuspendLayout();
            this.basicMapPanel.SuspendLayout();
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
            this.label2.Location = new System.Drawing.Point(10, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 10004;
            this.label2.Text = "大屏类型：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(26, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 15);
            this.label4.TabIndex = 10006;
            this.label4.Text = "X轴：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(245, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 15);
            this.label5.TabIndex = 10007;
            this.label5.Text = "Y轴：";
            // 
            // datasource
            // 
            this.datasource.BackColor = System.Drawing.SystemColors.Window;
            this.datasource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.datasource.Font = new System.Drawing.Font("宋体", 12F);
            this.datasource.FormattingEnabled = true;
            this.datasource.Location = new System.Drawing.Point(98, 29);
            this.datasource.Name = "datasource";
            this.datasource.Size = new System.Drawing.Size(368, 24);
            this.datasource.TabIndex = 10008;
            this.datasource.SelectedIndexChanged += new System.EventHandler(this.Datasource_SelectedIndexChanged);
            // 
            // bossType
            // 
            this.bossType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bossType.Font = new System.Drawing.Font("宋体", 12F);
            this.bossType.FormattingEnabled = true;
            this.bossType.Items.AddRange(new object[] {
            "样式1（态势大屏）",
            "样式2（运营大数据）",
            "样式3（智慧人口分析）",
            "样式4（重点人分析）",
            "样式5（全息星辰大海）",
            "样式6（网民全息档案）",
            "样式7（态势感知）",
            "样式8（智慧人口分析）",
            "样式9（智慧公安）",
            "样式10（星火平台）",
            "样式11（案件统计）",
            "样式12（运维态势分析）",
            "样式13（人口特征分析）",
            "样式14（警情分析）",
            "样式15（重点人信息态势）"});
            this.bossType.Location = new System.Drawing.Point(98, 74);
            this.bossType.Name = "bossType";
            this.bossType.Size = new System.Drawing.Size(368, 24);
            this.bossType.TabIndex = 10009;
            this.bossType.SelectedIndexChanged += new System.EventHandler(this.BossType_SelectedIndexChanged);
            // 
            // simpleBarX
            // 
            this.simpleBarX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.simpleBarX.FormattingEnabled = true;
            this.simpleBarX.Location = new System.Drawing.Point(72, 48);
            this.simpleBarX.Name = "simpleBarX";
            this.simpleBarX.Size = new System.Drawing.Size(136, 20);
            this.simpleBarX.TabIndex = 10010;
            // 
            // simpleBarCaption
            // 
            this.simpleBarCaption.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.simpleBarCaption.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.simpleBarCaption.Location = new System.Drawing.Point(2, 17);
            this.simpleBarCaption.Name = "simpleBarCaption";
            this.simpleBarCaption.Size = new System.Drawing.Size(440, 22);
            this.simpleBarCaption.TabIndex = 10012;
            this.simpleBarCaption.Text = "柱状图";
            // 
            // stackBarCaption
            // 
            this.stackBarCaption.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.stackBarCaption.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stackBarCaption.Location = new System.Drawing.Point(2, 11);
            this.stackBarCaption.Name = "stackBarCaption";
            this.stackBarCaption.Size = new System.Drawing.Size(440, 22);
            this.stackBarCaption.TabIndex = 10013;
            this.stackBarCaption.Text = "堆叠柱状图";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(26, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 10014;
            this.label3.Text = "X轴：";
            // 
            // stackBarX
            // 
            this.stackBarX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stackBarX.FormattingEnabled = true;
            this.stackBarX.Location = new System.Drawing.Point(72, 42);
            this.stackBarX.Name = "stackBarX";
            this.stackBarX.Size = new System.Drawing.Size(136, 20);
            this.stackBarX.TabIndex = 10015;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(245, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 15);
            this.label6.TabIndex = 10016;
            this.label6.Text = "Y轴：";
            // 
            // basicPieY
            // 
            this.basicPieY.FormattingEnabled = true;
            this.basicPieY.Location = new System.Drawing.Point(297, 52);
            this.basicPieY.Name = "basicPieY";
            this.basicPieY.Size = new System.Drawing.Size(136, 20);
            this.basicPieY.TabIndex = 10017;
            // 
            // gradientLineChartCaption
            // 
            this.gradientLineChartCaption.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.gradientLineChartCaption.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gradientLineChartCaption.Location = new System.Drawing.Point(2, 12);
            this.gradientLineChartCaption.Name = "gradientLineChartCaption";
            this.gradientLineChartCaption.Size = new System.Drawing.Size(440, 22);
            this.gradientLineChartCaption.TabIndex = 10018;
            this.gradientLineChartCaption.Text = "曲线图";
            // 
            // basicScatterCaption
            // 
            this.basicScatterCaption.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.basicScatterCaption.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.basicScatterCaption.Location = new System.Drawing.Point(2, 14);
            this.basicScatterCaption.Name = "basicScatterCaption";
            this.basicScatterCaption.Size = new System.Drawing.Size(440, 22);
            this.basicScatterCaption.TabIndex = 10023;
            this.basicScatterCaption.Text = "散点图";
            // 
            // basicMapCaption
            // 
            this.basicMapCaption.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.basicMapCaption.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.basicMapCaption.Location = new System.Drawing.Point(2, 18);
            this.basicMapCaption.Name = "basicMapCaption";
            this.basicMapCaption.Size = new System.Drawing.Size(440, 22);
            this.basicMapCaption.TabIndex = 10024;
            this.basicMapCaption.Text = "地市分布图";
            // 
            // basicMapX
            // 
            this.basicMapX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.basicMapX.FormattingEnabled = true;
            this.basicMapX.Location = new System.Drawing.Point(72, 49);
            this.basicMapX.Name = "basicMapX";
            this.basicMapX.Size = new System.Drawing.Size(136, 20);
            this.basicMapX.TabIndex = 10028;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(20, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 15);
            this.label10.TabIndex = 10027;
            this.label10.Text = "地市：";
            // 
            // basicMapY
            // 
            this.basicMapY.FormattingEnabled = true;
            this.basicMapY.Location = new System.Drawing.Point(297, 49);
            this.basicMapY.Name = "basicMapY";
            this.basicMapY.Size = new System.Drawing.Size(136, 20);
            this.basicMapY.TabIndex = 10032;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(245, 51);
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
            this.pictureBox1.Image = global::C2.Properties.Resources.BossStyle08;
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
            this.basicPieCaption.Location = new System.Drawing.Point(2, 19);
            this.basicPieCaption.Name = "basicPieCaption";
            this.basicPieCaption.Size = new System.Drawing.Size(440, 22);
            this.basicPieCaption.TabIndex = 10045;
            this.basicPieCaption.Text = "饼状图";
            // 
            // basicLineChartCaption
            // 
            this.basicLineChartCaption.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.basicLineChartCaption.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.basicLineChartCaption.Location = new System.Drawing.Point(2, 17);
            this.basicLineChartCaption.Name = "basicLineChartCaption";
            this.basicLineChartCaption.Size = new System.Drawing.Size(440, 22);
            this.basicLineChartCaption.TabIndex = 10052;
            this.basicLineChartCaption.Text = "折线图";
            // 
            // simpleBarY
            // 
            this.simpleBarY.DataSource = null;
            this.simpleBarY.Location = new System.Drawing.Point(297, 48);
            this.simpleBarY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.simpleBarY.Name = "simpleBarY";
            this.simpleBarY.Size = new System.Drawing.Size(136, 23);
            this.simpleBarY.TabIndex = 10057;
            // 
            // basicLineChartY
            // 
            this.basicLineChartY.DataSource = null;
            this.basicLineChartY.Location = new System.Drawing.Point(297, 49);
            this.basicLineChartY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.basicLineChartY.Name = "basicLineChartY";
            this.basicLineChartY.Size = new System.Drawing.Size(136, 23);
            this.basicLineChartY.TabIndex = 10061;
            // 
            // basicLineChartX
            // 
            this.basicLineChartX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.basicLineChartX.FormattingEnabled = true;
            this.basicLineChartX.Location = new System.Drawing.Point(72, 49);
            this.basicLineChartX.Name = "basicLineChartX";
            this.basicLineChartX.Size = new System.Drawing.Size(136, 20);
            this.basicLineChartX.TabIndex = 10060;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(245, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 15);
            this.label7.TabIndex = 10059;
            this.label7.Text = "Y轴：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(26, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 15);
            this.label8.TabIndex = 10058;
            this.label8.Text = "X轴：";
            // 
            // gradientLineChartY
            // 
            this.gradientLineChartY.DataSource = null;
            this.gradientLineChartY.Location = new System.Drawing.Point(297, 43);
            this.gradientLineChartY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gradientLineChartY.Name = "gradientLineChartY";
            this.gradientLineChartY.Size = new System.Drawing.Size(136, 23);
            this.gradientLineChartY.TabIndex = 10065;
            // 
            // gradientLineChartX
            // 
            this.gradientLineChartX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gradientLineChartX.FormattingEnabled = true;
            this.gradientLineChartX.Location = new System.Drawing.Point(72, 43);
            this.gradientLineChartX.Name = "gradientLineChartX";
            this.gradientLineChartX.Size = new System.Drawing.Size(136, 20);
            this.gradientLineChartX.TabIndex = 10064;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(245, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 15);
            this.label9.TabIndex = 10063;
            this.label9.Text = "Y轴：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(26, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 15);
            this.label11.TabIndex = 10062;
            this.label11.Text = "X轴：";
            // 
            // basicScatterY
            // 
            this.basicScatterY.DataSource = null;
            this.basicScatterY.Location = new System.Drawing.Point(297, 46);
            this.basicScatterY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.basicScatterY.Name = "basicScatterY";
            this.basicScatterY.Size = new System.Drawing.Size(136, 23);
            this.basicScatterY.TabIndex = 10069;
            // 
            // basicScatterX
            // 
            this.basicScatterX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.basicScatterX.FormattingEnabled = true;
            this.basicScatterX.Location = new System.Drawing.Point(72, 46);
            this.basicScatterX.Name = "basicScatterX";
            this.basicScatterX.Size = new System.Drawing.Size(136, 20);
            this.basicScatterX.TabIndex = 10068;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(245, 48);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 15);
            this.label13.TabIndex = 10067;
            this.label13.Text = "Y轴：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(26, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 15);
            this.label14.TabIndex = 10066;
            this.label14.Text = "X轴：";
            // 
            // stackBarY
            // 
            this.stackBarY.DataSource = null;
            this.stackBarY.Location = new System.Drawing.Point(297, 42);
            this.stackBarY.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.stackBarY.Name = "stackBarY";
            this.stackBarY.Size = new System.Drawing.Size(136, 23);
            this.stackBarY.TabIndex = 10073;
            // 
            // basicPieX
            // 
            this.basicPieX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.basicPieX.FormattingEnabled = true;
            this.basicPieX.Location = new System.Drawing.Point(72, 52);
            this.basicPieX.Name = "basicPieX";
            this.basicPieX.Size = new System.Drawing.Size(136, 20);
            this.basicPieX.TabIndex = 10072;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(245, 54);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 15);
            this.label15.TabIndex = 10071;
            this.label15.Text = "数值：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(20, 54);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 15);
            this.label17.TabIndex = 10070;
            this.label17.Text = "扇区：";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.basicMapPanel);
            this.panel1.Controls.Add(this.pictorialBarPanel);
            this.panel1.Controls.Add(this.basicPiePanel);
            this.panel1.Controls.Add(this.gradientLineChartPanel);
            this.panel1.Controls.Add(this.stackBarPanel);
            this.panel1.Controls.Add(this.basicScatterPanel);
            this.panel1.Controls.Add(this.basicLineChartPanel);
            this.panel1.Controls.Add(this.simpleBarPanel);
            this.panel1.Location = new System.Drawing.Point(526, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 369);
            this.panel1.TabIndex = 10074;
            // 
            // gradientLineChartPanel
            // 
            this.gradientLineChartPanel.Controls.Add(this.gradientLineChartCaption);
            this.gradientLineChartPanel.Controls.Add(this.label11);
            this.gradientLineChartPanel.Controls.Add(this.label9);
            this.gradientLineChartPanel.Controls.Add(this.gradientLineChartX);
            this.gradientLineChartPanel.Controls.Add(this.gradientLineChartY);
            this.gradientLineChartPanel.Location = new System.Drawing.Point(3, 278);
            this.gradientLineChartPanel.Name = "gradientLineChartPanel";
            this.gradientLineChartPanel.Size = new System.Drawing.Size(445, 83);
            this.gradientLineChartPanel.TabIndex = 10083;
            // 
            // stackBarPanel
            // 
            this.stackBarPanel.Controls.Add(this.stackBarCaption);
            this.stackBarPanel.Controls.Add(this.label3);
            this.stackBarPanel.Controls.Add(this.stackBarX);
            this.stackBarPanel.Controls.Add(this.label6);
            this.stackBarPanel.Controls.Add(this.stackBarY);
            this.stackBarPanel.Location = new System.Drawing.Point(3, 186);
            this.stackBarPanel.Name = "stackBarPanel";
            this.stackBarPanel.Size = new System.Drawing.Size(445, 83);
            this.stackBarPanel.TabIndex = 10082;
            // 
            // basicScatterPanel
            // 
            this.basicScatterPanel.Controls.Add(this.basicScatterCaption);
            this.basicScatterPanel.Controls.Add(this.label14);
            this.basicScatterPanel.Controls.Add(this.label13);
            this.basicScatterPanel.Controls.Add(this.basicScatterX);
            this.basicScatterPanel.Controls.Add(this.basicScatterY);
            this.basicScatterPanel.Location = new System.Drawing.Point(3, 371);
            this.basicScatterPanel.Name = "basicScatterPanel";
            this.basicScatterPanel.Size = new System.Drawing.Size(445, 83);
            this.basicScatterPanel.TabIndex = 10081;
            // 
            // basicLineChartPanel
            // 
            this.basicLineChartPanel.Controls.Add(this.basicLineChartCaption);
            this.basicLineChartPanel.Controls.Add(this.basicLineChartY);
            this.basicLineChartPanel.Controls.Add(this.basicLineChartX);
            this.basicLineChartPanel.Controls.Add(this.label7);
            this.basicLineChartPanel.Controls.Add(this.label8);
            this.basicLineChartPanel.Location = new System.Drawing.Point(3, 95);
            this.basicLineChartPanel.Name = "basicLineChartPanel";
            this.basicLineChartPanel.Size = new System.Drawing.Size(445, 83);
            this.basicLineChartPanel.TabIndex = 10080;
            // 
            // simpleBarPanel
            // 
            this.simpleBarPanel.Controls.Add(this.simpleBarCaption);
            this.simpleBarPanel.Controls.Add(this.label4);
            this.simpleBarPanel.Controls.Add(this.label5);
            this.simpleBarPanel.Controls.Add(this.simpleBarX);
            this.simpleBarPanel.Controls.Add(this.simpleBarY);
            this.simpleBarPanel.Location = new System.Drawing.Point(3, 3);
            this.simpleBarPanel.Name = "simpleBarPanel";
            this.simpleBarPanel.Size = new System.Drawing.Size(445, 83);
            this.simpleBarPanel.TabIndex = 10079;
            // 
            // pictorialBarCaption
            // 
            this.pictorialBarCaption.BackgroundStyle = C2.Controls.CaptionStyle.BaseLine;
            this.pictorialBarCaption.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pictorialBarCaption.Location = new System.Drawing.Point(2, 18);
            this.pictorialBarCaption.Name = "pictorialBarCaption";
            this.pictorialBarCaption.Size = new System.Drawing.Size(440, 22);
            this.pictorialBarCaption.TabIndex = 10074;
            this.pictorialBarCaption.Text = "一维柱状图";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(20, 51);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(52, 15);
            this.label20.TabIndex = 10075;
            this.label20.Text = "地市：";
            // 
            // pictorialBarX
            // 
            this.pictorialBarX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pictorialBarX.FormattingEnabled = true;
            this.pictorialBarX.Location = new System.Drawing.Point(72, 49);
            this.pictorialBarX.Name = "pictorialBarX";
            this.pictorialBarX.Size = new System.Drawing.Size(136, 20);
            this.pictorialBarX.TabIndex = 10076;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(245, 51);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(52, 15);
            this.label21.TabIndex = 10077;
            this.label21.Text = "数值：";
            // 
            // pictorialBarY
            // 
            this.pictorialBarY.FormattingEnabled = true;
            this.pictorialBarY.Location = new System.Drawing.Point(297, 49);
            this.pictorialBarY.Name = "pictorialBarY";
            this.pictorialBarY.Size = new System.Drawing.Size(136, 20);
            this.pictorialBarY.TabIndex = 10078;
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
            // previewBtn
            // 
            this.previewBtn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.previewBtn.Location = new System.Drawing.Point(739, 452);
            this.previewBtn.Name = "previewBtn";
            this.previewBtn.Size = new System.Drawing.Size(75, 27);
            this.previewBtn.TabIndex = 10076;
            this.previewBtn.Text = "配置预览";
            this.previewBtn.UseVisualStyleBackColor = true;
            this.previewBtn.Click += new System.EventHandler(this.PreviewBtn_ClickAsync);
            // 
            // zoomInBtn
            // 
            this.zoomInBtn.BackColor = System.Drawing.Color.Transparent;
            this.zoomInBtn.BackgroundImage = global::C2.Properties.Resources.zoom_in;
            this.zoomInBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.zoomInBtn.Location = new System.Drawing.Point(98, 119);
            this.zoomInBtn.Name = "zoomInBtn";
            this.zoomInBtn.Size = new System.Drawing.Size(25, 23);
            this.zoomInBtn.TabIndex = 10077;
            this.zoomInBtn.UseVisualStyleBackColor = false;
            this.zoomInBtn.Click += new System.EventHandler(this.ZoomInBtn_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 12F);
            this.label19.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label19.Location = new System.Drawing.Point(184, 418);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(0, 16);
            this.label19.TabIndex = 10078;
            // 
            // basicPiePanel
            // 
            this.basicPiePanel.Controls.Add(this.basicPieCaption);
            this.basicPiePanel.Controls.Add(this.label17);
            this.basicPiePanel.Controls.Add(this.label15);
            this.basicPiePanel.Controls.Add(this.basicPieX);
            this.basicPiePanel.Controls.Add(this.basicPieY);
            this.basicPiePanel.Location = new System.Drawing.Point(3, 462);
            this.basicPiePanel.Name = "basicPiePanel";
            this.basicPiePanel.Size = new System.Drawing.Size(445, 83);
            this.basicPiePanel.TabIndex = 10084;
            // 
            // pictorialBarPanel
            // 
            this.pictorialBarPanel.Controls.Add(this.pictorialBarCaption);
            this.pictorialBarPanel.Controls.Add(this.pictorialBarY);
            this.pictorialBarPanel.Controls.Add(this.label21);
            this.pictorialBarPanel.Controls.Add(this.pictorialBarX);
            this.pictorialBarPanel.Controls.Add(this.label20);
            this.pictorialBarPanel.Location = new System.Drawing.Point(3, 640);
            this.pictorialBarPanel.Name = "pictorialBarPanel";
            this.pictorialBarPanel.Size = new System.Drawing.Size(445, 83);
            this.pictorialBarPanel.TabIndex = 10085;
            // 
            // basicMapPanel
            // 
            this.basicMapPanel.Controls.Add(this.basicMapCaption);
            this.basicMapPanel.Controls.Add(this.basicMapY);
            this.basicMapPanel.Controls.Add(this.label12);
            this.basicMapPanel.Controls.Add(this.basicMapX);
            this.basicMapPanel.Controls.Add(this.label10);
            this.basicMapPanel.Location = new System.Drawing.Point(3, 551);
            this.basicMapPanel.Name = "basicMapPanel";
            this.basicMapPanel.Size = new System.Drawing.Size(445, 83);
            this.basicMapPanel.TabIndex = 10086;
            // 
            // SelectBossDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(994, 491);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.zoomInBtn);
            this.Controls.Add(this.previewBtn);
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
            this.Controls.SetChildIndex(this.previewBtn, 0);
            this.Controls.SetChildIndex(this.zoomInBtn, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.gradientLineChartPanel.ResumeLayout(false);
            this.gradientLineChartPanel.PerformLayout();
            this.stackBarPanel.ResumeLayout(false);
            this.stackBarPanel.PerformLayout();
            this.basicScatterPanel.ResumeLayout(false);
            this.basicScatterPanel.PerformLayout();
            this.basicLineChartPanel.ResumeLayout(false);
            this.basicLineChartPanel.PerformLayout();
            this.simpleBarPanel.ResumeLayout(false);
            this.simpleBarPanel.PerformLayout();
            this.basicPiePanel.ResumeLayout(false);
            this.basicPiePanel.PerformLayout();
            this.pictorialBarPanel.ResumeLayout(false);
            this.pictorialBarPanel.PerformLayout();
            this.basicMapPanel.ResumeLayout(false);
            this.basicMapPanel.PerformLayout();
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
        private Controls.CaptionBar gradientLineChartCaption;
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
        private Controls.Common.ComCheckBoxList gradientLineChartY;
        private System.Windows.Forms.ComboBox gradientLineChartX;
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
        private System.Windows.Forms.Button previewBtn;
        private System.Windows.Forms.Button zoomInBtn;
        private System.Windows.Forms.Label label19;
        private Controls.CaptionBar pictorialBarCaption;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox pictorialBarX;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox pictorialBarY;
        private System.Windows.Forms.Panel simpleBarPanel;
        private System.Windows.Forms.Panel basicLineChartPanel;
        private System.Windows.Forms.Panel stackBarPanel;
        private System.Windows.Forms.Panel basicScatterPanel;
        private System.Windows.Forms.Panel gradientLineChartPanel;
        private System.Windows.Forms.Panel basicPiePanel;
        private System.Windows.Forms.Panel basicMapPanel;
        private System.Windows.Forms.Panel pictorialBarPanel;
    }
}