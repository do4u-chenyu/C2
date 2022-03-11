using C2.Controls;
using C2.IAOLab.BankTool;
using C2.IAOLab.IDInfoGet;
using C2.IAOLab.ExpressNum;
using C2.IAOLab.BaseAddress;
using C2.Utils;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using C2.Business.IAOLab.IMEI;

namespace C2.Dialogs.IAOLab
{
    public partial class InformationSearch : BaseDialog
    {
        private string formType;  //直接在类中定义，方法体外，实例变量
        public InformationSearch()   //无参构造方法
        {
            InitializeComponent();
            tabControl1.Visible = true;
        }

        public string TipBank { set { this.label6.Text = value; } }   

        public bool TabControlVisible { set { this.tabControl1.Visible = value; } }
        //public string InputLable { set { this.inputLabel.Text = value; } }


        public string FormType { get { return this.formType; } set { this.formType = value; } }

        public string FormClass()
        {
            string fileType = String.Empty;
            if (tabControl1.SelectedTab == tabPage6 && tabControl1.Visible == true)
            {
                fileType = "bankCard";
            }
            else if (tabControl1.SelectedTab == tabPage7 && tabControl1.Visible == true)
            {
                fileType = "webUrl";
            }
            else if (tabControl1.SelectedTab == tabPage8 && tabControl1.Visible == true)
            {
                fileType = "idCard";
            }
            else if (tabControl1.SelectedTab == tabPage9 && tabControl1.Visible == true)
            {
                fileType = "expressNum";
            }
            else if (tabControl1.SelectedTab == tabPage1 && tabControl1.Visible == true)
            {
                fileType = "imei";
            }
            return fileType;
        }


        private void Search_Click(object sender, EventArgs e)
        {
            StringBuilder tmpResult = new StringBuilder();

            this.Cursor = Cursors.WaitCursor;
            string firstLine;

            if (tabControl1.SelectedTab == tabPage6 && tabControl1.Visible == true)
            {
                string[] inputArray = this.richTextBox1.Text.Split('\n');
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

            if (tabControl1.SelectedTab == tabPage7 && tabControl1.Visible == true)
            {
                string[] inputArray = this.richTextBox2.Text.Split('\n');
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArry(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "网站域名\t接口备案号\t网页备案号\n";
                tmpResult.Append(firstLine);
                foreach (string webUrl in inputArray)
                {
                    ShowResult(webUrl, "webUrl", tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
            }

            if (tabControl1.SelectedTab == tabPage8 && tabControl1.Visible == true)     //身份证号查询
            {

                string[] inputArray = this.richTextBox3.Text.Split('\n');
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArry(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "15位身份证号\t18位身份证号\t出生日期\t性别\t归属地\n";
                tmpResult.Append(firstLine);
                foreach (string idCard in inputArray)
                {
                    ShowResult(idCard, "idCard", tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }            
            }

            if (tabControl1.SelectedTab == tabPage9 && tabControl1.Visible == true)     //身份证号查询
            {

                string[] inputArray = this.richTextBox4.Text.Split('\n');
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArry(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "快递单号\t揽件地址\t派件地址\n";
                tmpResult.Append(firstLine);
                foreach (string expressNum in inputArray)
                {
                    ShowResult(expressNum, "expressNum", tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
            }

            if (tabControl1.SelectedTab == tabPage1 && tabControl1.Visible == true)     // IMEI查询
            {
                string[] inputArray = this.richTextBox5.Text.Split('\n');
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArry(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "设备号\t设备号TAC字段\t设备号品牌\n";
                tmpResult.Append(firstLine);
                foreach (string imei in inputArray)
                {
                    ShowResult(imei, "imei", tmpResult);
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
                input = input.Trim(' ') ;//去除前后空格
                if (progressBar1.Value % 100 == 0)
                {
                    Thread.Sleep(500);
                }
                switch (type)
                {
                    case "webUrl":
                        if (input.Contains("网站域名"))
                            input = String.Empty;
                        tmpResult.Append(RecordNumber.GetInstance().WebUrlLocate(input.Split('\t')[0]));
                        richTextBox2.Text = tmpResult.ToString();
                        break;
                    case "bankCard":
                        tmpResult.Append(BankTool.GetInstance().BankToolSearch(input.Split('\t')[0]));
                        richTextBox1.Text = tmpResult.ToString();
                        break;
                    case "idCard":
                        tmpResult.Append(IDInfoGet.GetInstance().IDSearch(input.Split('\t')[0]));
                        richTextBox3.Text = tmpResult.ToString();
                        break;
                    case "expressNum":
                        tmpResult.Append(ExpressNum.GetInstance().ExpressSearch(input.Split('\t')[0]));
                        richTextBox4.Text = tmpResult.ToString();
                        break;
                    case "imei":
                        tmpResult.Append(IMEI.GetInstance().IMEISearch(input.Split('\t')[0]));
                        richTextBox5.Text = tmpResult.ToString();
                        break;

                }

                progressBar1.Value += 1;
            }
        }

        private int GetRelLengthOfArry(string[] arry)
        {
            int relLength = 0;
            foreach (string i in arry)
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
            //this.wifiMacIR.Clear();
        }

        private void Import_Click(object sender, EventArgs e)  //导入文件
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
                        if (tabControl1.SelectedTab == tabPage6 && tabControl1.Visible == true)
                            richTextBox1.Text = sb.TrimEndN().ToString();
                        if (tabControl1.SelectedTab == tabPage7 && tabControl1.Visible == true)
                            richTextBox2.Text = sb.TrimEndN().ToString();
                        if (tabControl1.SelectedTab == tabPage8 && tabControl1.Visible == true)
                            richTextBox3.Text = sb.TrimEndN().ToString();
                        if (tabControl1.SelectedTab == tabPage9 && tabControl1.Visible == true)
                            richTextBox4.Text = sb.TrimEndN().ToString();
                        if (tabControl1.SelectedTab == tabPage1 && tabControl1.Visible == true)
                            richTextBox5.Text = sb.TrimEndN().ToString();

                        if (tabControl1.Visible == false)
                            richTextBox1.Text = sb.TrimEndN().ToString();
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

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string firstLine = null;
                string text = string.Empty;
                switch (formclass)
                {
                    case "bankCard":
                        firstLine = "银行卡号\t银行名称\t卡种\t归属地\r\n";
                        text = richTextBox1.Text;
                        break;
                    case "webUrl":
                        text = richTextBox2.Text;
                        break;
                    case "idCard":
                        text = richTextBox3.Text;
                        break;
                    case "expressNum":
                        text = richTextBox4.Text;
                        break;
                    case "imei":
                        text = richTextBox5.Text;
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
                            if (line.Contains("IP"))
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

        private void Export_Click(object sender, EventArgs e)  //导出文件
        {
            if ((tabControl1.SelectedTab == tabPage6 && tabControl1.Visible == true && richTextBox1.Text == string.Empty) ||
                (tabControl1.SelectedTab == tabPage7 && tabControl1.Visible == true && richTextBox2.Text == string.Empty))
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
