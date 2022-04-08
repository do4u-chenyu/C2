using C2.Controls;
using C2.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.IAOLab
{
    public partial class GoldEyesForm : BaseDialog
    {
        public GoldEyesForm()
        {
            InitializeComponent();
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

                        richTextBox1.Text = sb.TrimEndN().ToString();
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
        private void Confirm_Click(object sender, EventArgs e)
        {
            StringBuilder tmpResult = new StringBuilder();

            this.Cursor = Cursors.WaitCursor;
            string firstLine;

            if (tabControl1.SelectedTab == SEOTabPage && tabControl1.Visible == true)
            {
                string[] inputArray = this.richTextBox1.Text.Split('\n');
                progressBar1.Value = 0;
                progressBar1.Maximum = GetRelLengthOfArry(inputArray);
                progressBar1.Minimum = 0;
                firstLine = "域名\t备案号\t机构名称\tIP\tIP归属地\n";
                tmpResult.Append(firstLine);
                foreach (string seo in inputArray)
                {
                    ShowResult(seo, "seo", tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                    progressBar1.Value += 1;
                }
            }

            if (tabControl1.SelectedTab == tabPage1 && tabControl1.Visible == true)
            {
                string[] inputArray = richTextBox2.Text.Split('\n');
                tmpResult = new StringBuilder();

                progressBar2.Value = 0;
                progressBar2.Maximum = GetRelLengthOfArry(inputArray);
                progressBar2.Minimum = 0;
                firstLine = "IP\t域名绑定信息\n";

                tmpResult.Append(firstLine);
                foreach (string Ip in inputArray)
                {
                    IPShowResult(Ip, tmpResult);
                    if (progressBar2.Value == progressBar2.Maximum && progressBar2.Maximum != 0)
                    {
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar2.Value = 0;
                    }
                }
            }
        }

        private void Export_Click(object sender, EventArgs e)
        {
            if ((tabControl1.SelectedTab == SEOTabPage && tabControl1.Visible == true && richTextBox1.Text == string.Empty) ||
                (tabControl1.SelectedTab == tabPage1 && tabControl1.Visible == true && richTextBox2.Text == string.Empty))
            {
                MessageBox.Show("当前无数据可导出!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "请选择要导出的位置";
            saveDialog.Filter = "文本文件|*.txt";
            string formclass = FormClass();
            saveDialog.FileName = formclass + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string text = string.Empty;
                switch (formclass)
                {
                    case "SEO":
                        text = richTextBox1.Text;
                        break;

                    case "IP反查":
                        text = richTextBox2.Text;
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
                        foreach (string line in lines)
                        {
                            //if (line.Contains("SEO"))
                            //    continue;
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
                    case "seo":
                        tmpResult.Append(string.Format("{0}{1}{2}{3}", input, "\t", GetSEOTool(input), "\n")); 
                        richTextBox1.Text = tmpResult.ToString();
                        break;
                    
                }
            }
        }

        public string GetSEOTool(string host)
        {
            Thread.Sleep(500);
            string url = "http://47.94.39.209:22222/api/fhge/seo_query";
            Dictionary<string, string> pairs = new Dictionary<string, string> { { "seo", host } };
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            string content = JsonConvert.SerializeObject(pairs);
            byte[] data = Encoding.UTF8.GetBytes(content);
            request.Timeout = 200000;
            request.Method = "POST";
            request.ContentType = "application/json";

            HttpWebResponse response;
            string postContent;
            try
            {
                using (var stream = request.GetRequestStream())
                    stream.Write(data, 0, data.Length);
                response = (HttpWebResponse)request.GetResponse();
                postContent = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch
            {
                return "网络连接中断";
            }

            JObject json = JObject.Parse(postContent);

            if (json["success"].ToString() == "0")
                return "SEO查询接口错误";
            var gList = json["seo_info"];
            string icp = gList["备案号"].ToString();
            string name = gList["机构名称"].ToString();
            string ip = gList["IP"].ToString();
            string ipAddress = gList["IP归属地"].ToString();

            List<string> cardInfo = new List<string>();
            try
            {
                cardInfo.Add(icp.Replace(" ", string.Empty));
                cardInfo.Add(name.Replace(" ", string.Empty));
                cardInfo.Add(ip.Replace(" ", string.Empty));
                cardInfo.Add(ipAddress.Replace("|", "-").Replace("0-", string.Empty).Replace(" ", string.Empty));
            }
            catch { }
            if (cardInfo.Count == 4)
            {
                cardInfo[0] = cardInfo[0] == string.Empty ? "未知" : cardInfo[0];
                cardInfo[1] = cardInfo[1] == string.Empty ? "未知" : cardInfo[1];
                cardInfo[2] = cardInfo[2] == string.Empty ? "未知" : cardInfo[2];
                cardInfo[3] = cardInfo[3] == string.Empty ? "未知" : cardInfo[3];
                return string.Join("\t", cardInfo);
            }

            return "查询失败";
        }

        public string FormClass()
        {
            string fileType = String.Empty;
            if (tabControl1.SelectedTab == SEOTabPage && tabControl1.Visible == true)
            {
                fileType = "SEO";
            }
            if (tabControl1.SelectedTab == tabPage1 && tabControl1.Visible == true)
            {
                fileType = "IP反查";
            }
            return fileType;
        }

        private void button1_Click(object sender, EventArgs e)
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

                        richTextBox2.Text = sb.TrimEndN().ToString();
                        if (tabControl1.Visible == false)
                            richTextBox2.Text = sb.TrimEndN().ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }
            }
        }

        private void IPShowResult(string input, StringBuilder tmpResult)
        {
            if (!string.IsNullOrEmpty(input) && progressBar2.Value < 5001 && !string.IsNullOrEmpty(input.Split('\t')[0].Replace(OpUtil.Blank.ToString(), string.Empty)))
            {
                if (progressBar2.Value % 100 == 0)
                {
                    Thread.Sleep(100);
                }

                tmpResult.Append(GetInfo(input.Split('\t')[0]));
                richTextBox2.Text = tmpResult.ToString();

                progressBar2.Value += 1;
            }
        }

        private string GetInfo(string Ip)
        {
            List<string> resultList = new List<string> { };
            string url = "http://47.94.39.209:8899/api/fhge/capture_host_by_ip";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.Timeout = 20000;
            Dictionary<string, string> pairs = new Dictionary<string, string> { { "ip", Ip } };

            string content = JsonConvert.SerializeObject(pairs);
            byte[] data = Encoding.UTF8.GetBytes(content);

            req.ContentType = "application/json";
            req.ContentLength = data.Length;

            using (var stream = req.GetRequestStream())
                stream.Write(data, 0, data.Length);

            HttpWebResponse resp = null;
            try
            {
                resp = (HttpWebResponse)req.GetResponse();
            }
            catch
            {
                return "网络连接中断";
            }

            JObject json = JObject.Parse(new StreamReader(resp.GetResponseStream()).ReadToEnd());
            var dat = json["data"];
            string resp_data = JsonConvert.SerializeObject(dat);
            string res = Ip + '\t' + resp_data + '\n';

            return res;
        }
    }
}
