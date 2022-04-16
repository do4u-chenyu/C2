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
            this.components = new System.ComponentModel.Container();
            this.wifiMacIR = new System.Windows.Forms.RichTextBox();
            this.inputLabel = new System.Windows.Forms.Label();
            this.confirm = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.export = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tipLable = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.import = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.baseStationIR = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.baseAddressIR = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.IPStationIR = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.PhoneLocationIR = new System.Windows.Forms.RichTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.LngAndLatRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.bankCardIR = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // wifiMacIR
            // 
            this.wifiMacIR.BackColor = System.Drawing.Color.White;
            this.wifiMacIR.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.wifiMacIR.Location = new System.Drawing.Point(3, 96);
            this.wifiMacIR.Margin = new System.Windows.Forms.Padding(2);
            this.wifiMacIR.Name = "wifiMacIR";
            this.wifiMacIR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.wifiMacIR.Size = new System.Drawing.Size(587, 207);
            this.wifiMacIR.TabIndex = 0;
            this.wifiMacIR.Text = "";
            this.wifiMacIR.WordWrap = false;
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Font = new System.Drawing.Font("宋体", 11F);
            this.inputLabel.Location = new System.Drawing.Point(5, 13);
            this.inputLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(219, 22);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.export);
            this.panel1.Controls.Add(this.confirm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 337);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(602, 44);
            this.panel1.TabIndex = 5;
            // 
            // export
            // 
            this.export.Location = new System.Drawing.Point(515, 10);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(56, 23);
            this.export.TabIndex = 12;
            this.export.Text = "导出";
            this.export.UseVisualStyleBackColor = true;
            this.export.Click += new System.EventHandler(this.Export_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 22);
            this.label2.TabIndex = 6;
            // 
            // tipLable
            // 
            this.tipLable.AutoSize = true;
            this.tipLable.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.tipLable.Location = new System.Drawing.Point(6, 61);
            this.tipLable.Name = "tipLable";
            this.tipLable.Size = new System.Drawing.Size(758, 44);
            this.tipLable.TabIndex = 7;
            this.tipLable.Text = "单次输入格式：04a1518006c2 或 04-a1-51-80-06-c2 或 04:a1:51:80:06:c2\r\n批量查询格式：多个mac间用换行分割，最" +
    "大支持2000条";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.ForeColor = System.Drawing.Color.SkyBlue;
            this.label1.Location = new System.Drawing.Point(9, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 27);
            this.label1.TabIndex = 9;
            this.label1.Text = "查询进度";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(118, 60);
            this.progressBar1.Maximum = 10000;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(348, 23);
            this.progressBar1.TabIndex = 10;
            // 
            // import
            // 
            this.import.Location = new System.Drawing.Point(505, 60);
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(56, 23);
            this.import.TabIndex = 11;
            this.import.Text = "导入";
            this.import.UseVisualStyleBackColor = true;
            this.import.Click += new System.EventHandler(this.Import_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(601, 342);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.inputLabel);
            this.tabPage1.Controls.Add(this.tipLable);
            this.tabPage1.Controls.Add(this.wifiMacIR);
            this.tabPage1.Location = new System.Drawing.Point(4, 32);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(593, 306);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Wifi查询";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.baseStationIR);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 32);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(593, 306);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "基站查询";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // baseStationIR
            // 
            this.baseStationIR.BackColor = System.Drawing.Color.White;
            this.baseStationIR.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.baseStationIR.Location = new System.Drawing.Point(3, 96);
            this.baseStationIR.Margin = new System.Windows.Forms.Padding(2);
            this.baseStationIR.Name = "baseStationIR";
            this.baseStationIR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.baseStationIR.Size = new System.Drawing.Size(587, 207);
            this.baseStationIR.TabIndex = 9;
            this.baseStationIR.Text = "";
            this.baseStationIR.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label4.Location = new System.Drawing.Point(6, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(648, 44);
            this.label4.TabIndex = 8;
            this.label4.Text = "单次输入格式：4600051162c01(2G/3G) 或 46001590a8089407(4G)\r\n 或 37b900018bd0(电信2G) 最大支持200" +
    "0条";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F);
            this.label3.Location = new System.Drawing.Point(5, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "请在下方输入基站号";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.baseAddressIR);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Location = new System.Drawing.Point(4, 32);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(593, 306);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "地址查询";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // baseAddressIR
            // 
            this.baseAddressIR.BackColor = System.Drawing.Color.White;
            this.baseAddressIR.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.baseAddressIR.Location = new System.Drawing.Point(3, 96);
            this.baseAddressIR.Margin = new System.Windows.Forms.Padding(2);
            this.baseAddressIR.Name = "baseAddressIR";
            this.baseAddressIR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.baseAddressIR.Size = new System.Drawing.Size(587, 207);
            this.baseAddressIR.TabIndex = 9;
            this.baseAddressIR.Text = "";
            this.baseAddressIR.WordWrap = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 11F);
            this.label7.Location = new System.Drawing.Point(5, 13);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(252, 22);
            this.label7.TabIndex = 2;
            this.label7.Text = "请在下方输入待查询地址";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label8.Location = new System.Drawing.Point(6, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(604, 22);
            this.label8.TabIndex = 8;
            this.label8.Text = "输入格式:南京市鼓楼区汉口路22号南京大学(xx市+具体地址)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(6, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(758, 22);
            this.label9.TabIndex = 8;
            this.label9.Text = "注意:地址中涉及到城市名称必须含有[市]（比如[南京市],不能只写[南京]）";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.IPStationIR);
            this.tabPage4.Controls.Add(this.label12);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Location = new System.Drawing.Point(4, 32);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(593, 306);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "IP归属地查询";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // IPStationIR
            // 
            this.IPStationIR.BackColor = System.Drawing.Color.White;
            this.IPStationIR.DetectUrls = false;
            this.IPStationIR.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.IPStationIR.Location = new System.Drawing.Point(3, 96);
            this.IPStationIR.Margin = new System.Windows.Forms.Padding(2);
            this.IPStationIR.Name = "IPStationIR";
            this.IPStationIR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.IPStationIR.Size = new System.Drawing.Size(587, 207);
            this.IPStationIR.TabIndex = 11;
            this.IPStationIR.Text = "";
            this.IPStationIR.WordWrap = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label12.Location = new System.Drawing.Point(6, 81);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(604, 22);
            this.label12.TabIndex = 10;
            this.label12.Text = "查询结果以TAB为分隔符追加在每行最后一列,最大支持1万行.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label11.Location = new System.Drawing.Point(6, 61);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(406, 22);
            this.label11.TabIndex = 9;
            this.label11.Text = "一行一个,TAB按列分割,IP必须为第一列.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 11F);
            this.label10.Location = new System.Drawing.Point(5, 13);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(230, 22);
            this.label10.TabIndex = 3;
            this.label10.Text = "请在下方输入待查询IP";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.PhoneLocationIR);
            this.tabPage5.Controls.Add(this.label16);
            this.tabPage5.Controls.Add(this.label15);
            this.tabPage5.Controls.Add(this.label14);
            this.tabPage5.Location = new System.Drawing.Point(4, 32);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(593, 306);
            this.tabPage5.TabIndex = 3;
            this.tabPage5.Text = "手机归属地查询";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // PhoneLocationIR
            // 
            this.PhoneLocationIR.BackColor = System.Drawing.Color.White;
            this.PhoneLocationIR.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PhoneLocationIR.Location = new System.Drawing.Point(3, 96);
            this.PhoneLocationIR.Margin = new System.Windows.Forms.Padding(2);
            this.PhoneLocationIR.Name = "PhoneLocationIR";
            this.PhoneLocationIR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.PhoneLocationIR.Size = new System.Drawing.Size(587, 207);
            this.PhoneLocationIR.TabIndex = 12;
            this.PhoneLocationIR.Text = "";
            this.PhoneLocationIR.WordWrap = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label16.Location = new System.Drawing.Point(6, 61);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(604, 22);
            this.label16.TabIndex = 11;
            this.label16.Text = "查询结果以TAB为分隔符追加在每行最后一列,最大支持1万行.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label15.Location = new System.Drawing.Point(6, 81);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(637, 22);
            this.label15.TabIndex = 10;
            this.label15.Text = "一行一个,任意空白符(TAB,空格)按列分割,手机号必须为第一列.";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 11F);
            this.label14.Location = new System.Drawing.Point(5, 13);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(274, 22);
            this.label14.TabIndex = 5;
            this.label14.Text = "请在下方输入待查询手机号";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.LngAndLatRichTextBox);
            this.tabPage6.Controls.Add(this.label18);
            this.tabPage6.Controls.Add(this.label17);
            this.tabPage6.Controls.Add(this.label13);
            this.tabPage6.Location = new System.Drawing.Point(4, 32);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(593, 306);
            this.tabPage6.TabIndex = 4;
            this.tabPage6.Text = "经纬度定位查询";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // LngAndLatRichTextBox
            // 
            this.LngAndLatRichTextBox.BackColor = System.Drawing.Color.White;
            this.LngAndLatRichTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LngAndLatRichTextBox.Location = new System.Drawing.Point(3, 96);
            this.LngAndLatRichTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.LngAndLatRichTextBox.Name = "LngAndLatRichTextBox";
            this.LngAndLatRichTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LngAndLatRichTextBox.Size = new System.Drawing.Size(587, 207);
            this.LngAndLatRichTextBox.TabIndex = 14;
            this.LngAndLatRichTextBox.Text = "";
            this.LngAndLatRichTextBox.WordWrap = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label18.Location = new System.Drawing.Point(6, 81);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(648, 22);
            this.label18.TabIndex = 13;
            this.label18.Text = "一行一个,TAB或空格分割,经度必须为第一列,例如:79.063 39.867";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label17.Location = new System.Drawing.Point(6, 61);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(516, 22);
            this.label17.TabIndex = 12;
            this.label17.Text = "查询结果TAB分隔,单次最大1万行,推荐5000,单日30W";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 11F);
            this.label13.Location = new System.Drawing.Point(5, 13);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(274, 22);
            this.label13.TabIndex = 6;
            this.label13.Text = "请在下方输入待查询经纬度";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11F);
            this.label5.Location = new System.Drawing.Point(9, 24);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(230, 22);
            this.label5.TabIndex = 13;
            this.label5.Text = "请在下方输入银行卡号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label6.Location = new System.Drawing.Point(10, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(758, 44);
            this.label6.TabIndex = 14;
            this.label6.Text = "单次输入格式：04a1518006c2 或 04-a1-51-80-06-c2 或 04:a1:51:80:06:c2\r\n批量查询格式：多个mac间用换行分割，最" +
    "大支持5000条";
            // 
            // bankCardIR
            // 
            this.bankCardIR.BackColor = System.Drawing.Color.White;
            this.bankCardIR.Location = new System.Drawing.Point(0, 134);
            this.bankCardIR.Margin = new System.Windows.Forms.Padding(2);
            this.bankCardIR.Name = "bankCardIR";
            this.bankCardIR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bankCardIR.Size = new System.Drawing.Size(602, 207);
            this.bankCardIR.TabIndex = 15;
            this.bankCardIR.Text = "";
            this.bankCardIR.WordWrap = false;
            // 
            // WifiLocation
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(602, 381);
            this.Controls.Add(this.import);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bankCardIR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "WifiLocation";
            this.Text = "定位工具";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WifiLocation_FormClosed);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox wifiMacIR;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label tipLable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button import;
        private System.Windows.Forms.Button export;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox baseStationIR;
        private System.Windows.Forms.RichTextBox baseAddressIR;
        private System.Windows.Forms.RichTextBox bankCardIR;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.RichTextBox IPStationIR;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RichTextBox PhoneLocationIR;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.RichTextBox LngAndLatRichTextBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
