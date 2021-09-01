using C2.Controls;
using C2.IAOLab.BankTool;
using C2.IAOLab.BaseStation;
using C2.IAOLab.BaseAddress;
using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WifiMac;
using C2.Utils;
using System;
using System.Configuration;
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

        public string Tip { set { this.tipLable.Text = value; } }
        public string TipBS { set { this.label4.Text = value; } }
        public string TipBank { set { this.label6.Text = value; } }

        public bool TabControlVisible { set { this.tabControl1.Visible = value; } }
        public string InputLable { set { this.inputLabel.Text = value; } }
      

        public string FormType { get { return this.formType; } set { this.formType = value; } }

        public string FormClass()
        {
            string fileType = String.Empty;
            if (tabControl1.SelectedTab == tabPage1 && tabControl1.Visible == true)
            {
                fileType = "mac";
            }
            else if (tabControl1.SelectedTab == tabPage2 && tabControl1.Visible == true)
            {
                fileType = "BaseStation";
            }
            else if (tabControl1.SelectedTab == tabPage3 && tabControl1.Visible == true)
            {
                fileType = "BaseAddress";
            }
            return fileType;
        }


        private void Search_Click(object sender, EventArgs e)
        {
            StringBuilder tmpResult = new StringBuilder();
            string[] inputArray = this.inputAndResult.Text.Split('\n');
            this.Cursor = Cursors.WaitCursor;
            string firstLine;

            if (tabControl1.SelectedTab == tabPage1 && tabControl1.Visible == true) 
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArry(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "WiFiMac号\t纬度\t经度\t范围\ttgdid\t地址\n";
                tmpResult.Append(firstLine);
                foreach (string mac in inputArray)
                {
                    ShowResult(mac, "mac", tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
            }

            if (tabControl1.SelectedTab == tabPage2 && tabControl1.Visible == true) 
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArry(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "基站号\t纬度\t经度\t范围\ttgdid\t地址\n";
                tmpResult.Append(firstLine);
                foreach (string baseStation in inputArray)
                {
                    ShowResult(baseStation, "baseStation", tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
            }

            if (tabControl1.SelectedTab == tabPage3 && tabControl1.Visible == true)
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArry(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "地址\t纬度\t经度\t反查地址\n";
                tmpResult.Append(firstLine);
                foreach (string baseAddress in inputArray)
                {
                    ShowResult(baseAddress, "baseAddress", tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
            }

            if (tabControl1.Visible == false) 
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArry(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "银行卡号\t银行名称\t卡种\t归属地\n";
                tmpResult.Append(firstLine);
                foreach (string bankCard in inputArray)
                {
                    ShowResult(bankCard, "bankCard", tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
            }

            this.Cursor = Cursors.Arrow;
        }
        private void ShowResult(string input, string type, StringBuilder tmpResult)
        {
            if (!string.IsNullOrEmpty(input) && progressBar1.Value < 5001 && !string.IsNullOrEmpty(input.Split('\t')[0].Replace(OpUtil.Blank.ToString(), string.Empty)))
            {

                    if (progressBar1.Value % 100 == 0)
                    {
                        Thread.Sleep(500);
                    }
                    switch (type)
                    {
                        case "baseStation":
                            tmpResult.Append(BaseStation.GetInstance().BaseStationLocate(input.Split('\t')[0]));
                            break;
                        case "baseAddress":
                            if (input.Contains("地址"))
                                input = String.Empty;
                            tmpResult.Append(BaseAddress.GetInstance().BaseAddressLocate(input.Split('\t')[0]));
                            break;
                        case "mac":
                            tmpResult.Append(WifiMac.GetInstance().MacLocate(input.Split('\t')[0]));
                            break;
                        case "bankCard":
                            tmpResult.Append(BankTool.GetInstance().BankToolSearch(input.Split('\t')[0]));
                            break;
                    }
                    inputAndResult.Text = tmpResult.ToString();
                    progressBar1.Value += 1;
            }
        }
            
        
        private int GetRelLengthOfArry(string[] arry)
        {
            int relLength = 0;
            foreach(string i in arry)
            {   
                if (!string.IsNullOrEmpty(i.Split('\t')[0].Replace(OpUtil.Blank.ToString(), string.Empty)))
                    relLength++;
            }
            return relLength;
        }
        private void Cancle_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            Close();
        }

        private void WifiLocation_FormClosed(object sender, FormClosedEventArgs e)
        {
            progressBar1.Value = 0;
            this.inputAndResult.Clear();
        }

        private void Import_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog
            {
                Filter = "文本文档 | *.txt;*.csv;*.bcp;*.tsv"
            };
            if (OpenFileDialog1.ShowDialog()==DialogResult.OK)
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
                        inputAndResult.Text = sb.TrimEndN().ToString();
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
               
                switch (formclass)
                {
                    case "BaseStation":
                        firstLine = "基站号\t纬度\t经度\t范围\ttgdid\t地址\r\n";
                        break;
                    case "BaseAddress":
                        firstLine = "地址\t纬度\t经度\t反查地址\r\n";
                        break;
                    case "mac":
                        firstLine = "WiFiMac号\t纬度\t经度\t范围\ttgdid\t地址\r\n";
                        break;
                    case "Card":
                        firstLine = "银行卡号\t银行名称\t卡种\t归属地\r\n";
                        break;
                }
                string path = saveDialog.FileName;
                string text = inputAndResult.Text;
                try
                {
                    using (StreamWriter fs = new StreamWriter(path))
                    {
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
            if (inputAndResult.Text == string.Empty)
            {
                MessageBox.Show("当前无数据可导出!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                ExportData();
            }
        }
    }
}
