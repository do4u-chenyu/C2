using C2.Business.IAOLab.LngAndLat;
using C2.Controls;
using C2.Core;
using C2.IAOLab.BankTool;
using C2.IAOLab.BaseAddress;
using C2.IAOLab.BaseStation;
using C2.IAOLab.IPAddress;
using C2.IAOLab.PhoneLocation;
using C2.IAOLab.WifiMac;
using C2.Utils;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace C2.Dialogs.IAOLab
{
    public partial class WifiLocation : BaseDialog
    {
        private string formType;
        public WifiLocation()
        {
            InitializeComponent();
            tabControl1.Visible = true;
        }
        public string TipBank { set { this.label6.Text = value; } }

        public bool TabControlVisible { set { this.tabControl1.Visible = value; } }


        public string FormType { get { return this.formType; } set { this.formType = value; } }

        public string FormClass()
        {
            string fileType = string.Empty;
            if (!tabControl1.Visible)
                return fileType;

            if (tabControl1.SelectedTab == tabPage1)
                fileType = "mac";
            else if (tabControl1.SelectedTab == tabPage2)
                fileType = "BaseStation";
            else if (tabControl1.SelectedTab == tabPage3)
                fileType = "BaseAddress";
            else if (tabControl1.SelectedTab == tabPage4)
                fileType = "IPAddress";
            else if (tabControl1.SelectedTab == tabPage5)
                fileType = "PhoneLocation";
            else if (tabControl1.SelectedTab == tabPage6)
                fileType = "LngAndLat";
            return fileType;
        }


        private void Search_Click(object sender, EventArgs e)
        {
            StringBuilder tmpResult = new StringBuilder();

            this.Cursor = Cursors.WaitCursor;
            string firstLine;

            if (tabControl1.SelectedTab == tabPage1 && tabControl1.Visible)
            {
                new Log.Log().LogManualButton("实验楼" + "-" + tabPage1.Text, "运行");
                string[] inputArray = this.wifiMacIR.Text.Split('\n');
                this.wifiMacIR.Clear();
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArray(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "WiFiMac号\t纬度\t经度\t范围\ttgdid\t地址\n";
                tmpResult.Append(firstLine);
                this.wifiMacIR.AppendText(firstLine);

                foreach (string mac in inputArray)
                {
                    Application.DoEvents();
                    ShowResult(mac, "mac", tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        this.label1.Text = "查询进度 : 100%";
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
            }

            if (tabControl1.SelectedTab == tabPage2 && tabControl1.Visible)
            {
                new Log.Log().LogManualButton("实验楼" + "-" + tabPage2.Text, "运行");
                string[] inputArray = this.baseStationIR.Text.Split('\n');
                this.baseStationIR.Clear();
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArray(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "基站号\t纬度\t经度\t范围\ttgdid\t地址\n";
                tmpResult.Append(firstLine);
                this.baseStationIR.AppendText(firstLine);

                foreach (string baseStation in inputArray)
                {
                    Application.DoEvents();
                    ShowResult(baseStation, "baseStation", tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        this.label1.Text = "查询进度 : 100%";
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
            }

            if (tabControl1.SelectedTab == tabPage3 && tabControl1.Visible)
            {
                new Log.Log().LogManualButton("实验楼" + "-" + tabPage3.Text, "运行");
                string[] inputArray = this.baseAddressIR.Text.Split('\n');
                this.baseAddressIR.Clear();

                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArray(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "地址\t纬度\t经度\t反查地址\n";
                tmpResult.Append(firstLine);
                this.baseAddressIR.AppendText(firstLine);

                foreach (string baseAddress in inputArray)
                {
                    Application.DoEvents();
                    ShowResult(baseAddress, "baseAddress", tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        this.label1.Text = "查询进度 : 100%";
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
            }
            if (tabControl1.SelectedTab == tabPage4 && tabControl1.Visible)
            {
                new Log.Log().LogManualButton("实验楼" + "-" + tabPage4.Text, "运行");
                string[] inputArray = this.IPStationIR.Text.Split('\n');
                this.IPStationIR.Clear();
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArray(inputArray);
                progressBar1.Minimum = 0;
                
                foreach (string baseAddress in inputArray)
                {
                    Application.DoEvents();
                    ShowResult(baseAddress, "IPAddress", tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        this.label1.Text = "查询进度 : 100%";
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
            }
            if (tabControl1.SelectedTab == tabPage5 && tabControl1.Visible)
            {
                new Log.Log().LogManualButton("实验楼" + "-" + tabPage5.Text, "运行");
                string[] inputArray = this.PhoneLocationIR.Text.Split('\n');
                this.PhoneLocationIR.Clear();
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArray(inputArray);
                progressBar1.Minimum = 0;
                
                foreach (string phoneNum in inputArray)
                {
                    Application.DoEvents();
                    ShowResult(phoneNum, "PhoneLocation", tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        this.label1.Text = "查询进度 : 100%";
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
            }

            if (tabControl1.SelectedTab == tabPage6 && tabControl1.Visible)
            {
                new Log.Log().LogManualButton("实验楼" + "-" + tabPage6.Text, "运行");
                string[] inputArray = this.LngAndLatRichTextBox.Text.Split('\n');
                this.LngAndLatRichTextBox.Clear();
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArray(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "经度\t纬度\t定位地址\t国家\t省\t市\t行政区\t地区编码\n";
                tmpResult.Append(firstLine);
                this.LngAndLatRichTextBox.AppendText(firstLine);

                foreach (string lngLat in inputArray)
                {
                    Application.DoEvents();
                    ShowResult(lngLat, "LngAndLat", tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        this.label1.Text = "查询进度 : 100%";
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
            }

            if (!tabControl1.Visible)
            {
                string[] inputArray = this.bankCardIR.Text.Split('\n');
                this.bankCardIR.Clear();
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArray(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "银行卡号\t银行名称\t卡种\t归属地\n";
                tmpResult.Append(firstLine);
                this.bankCardIR.AppendText(firstLine);

                foreach (string bankCard in inputArray)
                {
                    Application.DoEvents();
                    ShowResult(bankCard, "bankCard", tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        this.label1.Text = "查询进度 : 100%";
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
            }

            this.Cursor = Cursors.Arrow;
        }
        private void ShowResult(string input, string type, StringBuilder tmpResult)
        {
            if (!string.IsNullOrEmpty(input) && progressBar1.Value < progressBar1.Maximum + 1 && !string.IsNullOrEmpty(input.Split('\t')[0].Replace(OpUtil.Blank.ToString(), string.Empty)))
            {
                if (progressBar1.Value % 100 == 0)
                {
                    Thread.Sleep(500);
                    this.label1.Text = string.Format("查询进度 : {0}%", progressBar1.Value * 100.0 / progressBar1.Maximum);
                }
                    

                string line = string.Empty;
                switch (type)
                {
                    case "baseStation":
                        line = BaseStation.GetInstance().BaseStationLocate(input.Split('\t')[0]);
                        tmpResult.Append(line);
                        baseStationIR.AppendText(line);
                        break;
                    case "baseAddress":
                        if (input.Contains("地址"))
                            input = String.Empty;
                        line = BaseAddress.GetInstance().BaseAddressLocate(input.Split('\t')[0]);
                        tmpResult.Append(line);
                        baseAddressIR.AppendText(line);
                        break;
                    case "mac":
                        line = WifiMac.GetInstance().MacLocate(input.Split('\t')[0]);
                        tmpResult.Append(line);
                        wifiMacIR.AppendText(line);
                        break;
                    case "bankCard":
                        line = BankTool.GetInstance().BankToolSearch(input.Split('\t')[0]);
                        tmpResult.Append(line);
                        bankCardIR.AppendText(line);
                        break;
                    case "IPAddress":
                        line = string.Format("{0}\t{1}", input.Trim('\n'), C2IPAddress.GetInstance().GetIPAddress(CollectionExtensions.SplitWhitespace(input)[0]));
                        tmpResult.Append(line);
                        IPStationIR.AppendText(line);
                        break;
                    case "PhoneLocation":
                        line = string.Format("{0}\t{1}", input.Trim('\n'), PhoneLocation.GetInstance().GetPhoneLocation(CollectionExtensions.SplitWhitespace(input)[0]));
                        tmpResult.Append(line);
                        PhoneLocationIR.AppendText(line);
                        break;
                    case "LngAndLat":
                        line = LngAndLat.GetInstance().GetLocation(input.Trim('\n'));
                        tmpResult.Append(line);
                        LngAndLatRichTextBox.AppendText(line);
                        break;
                }

                progressBar1.Value += 1;
            }
        }


        private int GetRelLengthOfArray(string[] arry)
        {
            int relLength = 0;
            foreach (string i in arry)
            {
                if (!string.IsNullOrEmpty(i.Split('\t')[0].Replace(OpUtil.Blank.ToString(), string.Empty)))
                    relLength++;
            }
            return relLength;
        }

        private void WifiLocation_FormClosed(object sender, FormClosedEventArgs e)
        {
            progressBar1.Value = 0;
            this.wifiMacIR.Clear();
        }

        private void Import_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog
            {
                Filter = "文本文档 | *.txt;*.csv;*.bcp;*.tsv"
            };
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string path = OpenFileDialog1.FileName;
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string line;
                        StringBuilder sb = new StringBuilder();
                        // 从文件读取并显示行，直到文件的末尾 
                        while ((line = sr.ReadLine()) != null)
                        {
                            sb.Append(line);
                            sb.Append("\n");
                        }
                        if (tabControl1.SelectedTab == tabPage1 && tabControl1.Visible == true)
                            wifiMacIR.Text = sb.TrimEndN().ToString();
                        if (tabControl1.SelectedTab == tabPage2 && tabControl1.Visible == true)
                            baseStationIR.Text = sb.TrimEndN().ToString();
                        if (tabControl1.SelectedTab == tabPage3 && tabControl1.Visible == true)
                            baseAddressIR.Text = sb.TrimEndN().ToString();
                        if (tabControl1.SelectedTab == tabPage4 && tabControl1.Visible == true)
                            IPStationIR.Text = sb.TrimEndN().ToString();
                        if (tabControl1.SelectedTab == tabPage5 && tabControl1.Visible == true)
                            PhoneLocationIR.Text = sb.TrimEndN().ToString();
                        if (tabControl1.SelectedTab == tabPage6 && tabControl1.Visible == true)
                            LngAndLatRichTextBox.Text = sb.TrimEndN().ToString();
                        if (tabControl1.Visible == false)
                            bankCardIR.Text = sb.TrimEndN().ToString();

                        this.toolTip1.SetToolTip(this.progressBar1, path);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }
            }
        }

        private void ExportData()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "请选择要导出的位置";
            saveDialog.Filter = "文本文件|*.txt";
            string formclass = FormClass();
            saveDialog.FileName = formclass + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";
            //saveDialog.ShowDialog();
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string firstLine = null;
                string text = string.Empty;
                switch (formclass)
                {
                    case "BaseStation":
                        firstLine = "基站号\t纬度\t经度\t范围\ttgdid\t地址\r\n";
                        text = baseStationIR.Text;
                        break;
                    case "BaseAddress":
                        firstLine = "地址\t纬度\t经度\t反查地址\r\n";
                        text = baseAddressIR.Text;
                        break;
                    case "mac":
                        firstLine = "WiFiMac号\t纬度\t经度\t范围\ttgdid\t地址\r\n";
                        text = wifiMacIR.Text;
                        break;
                    case "Card":
                        firstLine = "银行卡号\t银行名称\t卡种\t归属地\r\n";
                        text = bankCardIR.Text;
                        break;
                    case "IPAddress":
                        text = IPStationIR.Text;
                        break;
                    case "PhoneLocation":
                        text = PhoneLocationIR.Text;
                        break;
                    case "LngAndLat":
                        firstLine = "经度\t纬度\t定位地址\t国家\t省\t市\t行政区\t地区编码\r\n";
                        text = LngAndLatRichTextBox.Text;
                        break;

                }
                string path = saveDialog.FileName;

                try
                {
                    using (StreamWriter fs = new StreamWriter(path))
                    {
                        if (text == string.Empty)
                            return;
                        string[] lines = text.Split('\n');
                        fs.Write(firstLine);
                        foreach (string line in lines)
                        {
                            if (line.Contains("银行卡号"))
                                continue;
                            if (line.Contains("基站号"))
                                continue;
                            if (line.Contains("地址"))
                                continue;
                            if (line.Contains("WiFiMac号"))
                                continue;
                            /*
                             * IP不做筛选进行输出
                            if (line.Contains("IP"))
                                continue;
                            */
                            if (line.Contains("经度") || line.Contains("纬度"))
                                continue;
                            fs.WriteLine(line);
                            fs.Flush();
                        }
                        fs.Close();
                    }
                    MessageBox.Show("导出成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }
            }
        }

        private void Export_Click(object sender, EventArgs e)
        {
            if ((tabControl1.SelectedTab == tabPage1 && tabControl1.Visible == true && wifiMacIR.Text == string.Empty) ||
                (tabControl1.Visible == false && bankCardIR.Text == string.Empty) ||
                (tabControl1.SelectedTab == tabPage3 && tabControl1.Visible == true && baseAddressIR.Text == string.Empty) ||
                (tabControl1.SelectedTab == tabPage2 && tabControl1.Visible == true && baseStationIR.Text == string.Empty) ||
                (tabControl1.SelectedTab == tabPage4 && tabControl1.Visible == true && IPStationIR.Text == string.Empty) ||
                (tabControl1.SelectedTab == tabPage5 && tabControl1.Visible == true && PhoneLocationIR.Text == string.Empty) ||
                (tabControl1.SelectedTab == tabPage6 && tabControl1.Visible == true && LngAndLatRichTextBox.Text == string.Empty))
            {
                MessageBox.Show("当前无数据可导出!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                ExportData();
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
                new Log.Log().LogManualButton("实验楼" + "-" + tabPage1.Text, "打开");
            else if (tabControl1.SelectedTab == tabPage2)
                new Log.Log().LogManualButton("实验楼" + "-" + tabPage2.Text, "打开");
            else if (tabControl1.SelectedTab == tabPage3)
                new Log.Log().LogManualButton("实验楼" + "-" + tabPage3.Text, "打开");
            else if (tabControl1.SelectedTab == tabPage4)
                new Log.Log().LogManualButton("实验楼" + "-" + tabPage4.Text, "打开");
            else if (tabControl1.SelectedTab == tabPage5)
                new Log.Log().LogManualButton("实验楼" + "-" + tabPage5.Text, "打开");
            else if (tabControl1.SelectedTab == tabPage6)
                new Log.Log().LogManualButton("实验楼" + "-" + tabPage6.Text, "打开");
        }
    }
}
