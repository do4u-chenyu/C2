using C2.Controls;
using C2.Core;
using C2.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
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
                firstLine = "域名\t备案号\t机构名称\t机构服务标签\tIP\tIP归属地\t注册商\t注册商邮箱\t注册商手机号\t注册人邮箱\t注册人手机号\t注册时间\t过期时间\n";
                tmpResult.Append(firstLine);
                foreach (string seo in inputArray)
                {
                    ShowResult(seo, tmpResult);
                    if (progressBar1.Value == progressBar1.Maximum && progressBar1.Maximum != 0)
                    {
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar1.Value = 0;
                    }
                }
            }

            if (tabControl1.SelectedTab == tabPage1 && tabControl1.Visible == true)
            {
                string[] inputArray = richTextBox2.Text.Split('\n');
                progressBar2.Value = 0;
                progressBar2.Maximum = GetRelLengthOfArry(inputArray);
                progressBar2.Minimum = 0;

                foreach (string Ip in inputArray)
                {
                    IPShowResult(Ip);
                    if (progressBar2.Value == progressBar2.Maximum && progressBar2.Maximum != 0)
                    {
                        MessageBox.Show("查询完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar2.Value = 0;
                    }
                }
            }
            this.Cursor = Cursors.Arrow;
        }

        private void Export_Click(object sender, EventArgs e)
        {
            if ((tabControl1.SelectedTab == SEOTabPage && tabControl1.Visible == true && richTextBox1.Text == string.Empty) ||
                (tabControl1.SelectedTab == tabPage1 && tabControl1.Visible == true && dataGridView1.RowCount == 0))
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

                        foreach (DataGridViewRow row in this.dataGridView1.Rows)
                        {
                            for (int i = 0; i < row.Cells.Count; i++)
                            {
                                if (row.Cells[i].Value == null)
                                {
                                    text = text + "\t";
                                }
                                else
                                {
                                    text = text + row.Cells[i].Value.ToString() + "\t";
                                }
                            }
                            text += "\n";
                        }
                        break;
                }
                string path = saveDialog.FileName;

                try
                {
                    using (StreamWriter fs = new StreamWriter(path))
                    {
                        if (text == string.Empty)
                            return;
                        fs.WriteLine("IP\t域名\t绑定时间\t解绑时间\t备注");
                        string[] lines = text.Split('\n');
                        foreach (string line in lines)
                        {
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

        private void ShowResult(string input, StringBuilder tmpResult)
        {
            if (!string.IsNullOrEmpty(input) && progressBar1.Value < 5001 && !string.IsNullOrEmpty(input.Split('\t')[0].Replace(OpUtil.Blank.ToString(), string.Empty)))
            {
                if (progressBar1.Value % 100 == 0)
                    Thread.Sleep(500);
                tmpResult.Append(string.Format("{0}{1}{2}{3}", input, "\t", GetSEOTool(input), "\n")); 
                richTextBox1.Text = tmpResult.ToString();
                progressBar1.Value += 1;
            }
        }
        private HttpWebRequest ConfigPost(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 200000;
            request.Method = "POST";
            request.ContentType = "application/json";
            return request;
        }

        public string GetSEOTool(string host)
        {
            string url =  Global.FastIaoUrl + "/seo_query";
            HttpWebRequest request = ConfigPost(url);
            Dictionary<string, string> pairs = new Dictionary<string, string> { { "domain", host } };
            string content = JsonConvert.SerializeObject(pairs);
            byte[] data = Encoding.UTF8.GetBytes(content);
           
            HttpWebResponse response;
            string postContent;
            JObject json;
            try
            {
                using (var stream = request.GetRequestStream())
                    stream.Write(data, 0, data.Length);
                response = (HttpWebResponse)request.GetResponse();
                postContent = new StreamReader(response.GetResponseStream()).ReadToEnd();
                json = JObject.Parse(postContent); 
            }
            catch
            {
                return "网络连接中断";
            }

            var gList = json["data"];
            if (json["status"].IsNull() || gList == null || json["msg"].IsNull())
                return "SEO查询接口错误";

            if (gList.ToString().IsNullOrEmpty())
                return "SEO查询接口无返回数据";

            if (json["status"].ToString() != "success")
                return string.Format("SEO查询接口错误,{0}", json["msg"]);

            string[] cardInfo = new string[12]; 

            cardInfo[0] = gList["备案号"].IsNull() ? "未知" : gList["备案号"].ToString().Trim();
            cardInfo[1] = gList["机构名称"].IsNull() ? "未知" : gList["机构名称"].ToString().Trim();
            cardInfo[2] = gList["机构服务标签"].IsNull() ? "未知" : gList["机构服务标签"].ToString().Trim();
            cardInfo[3] = gList["IP"].IsNull() ? "未知" : gList["IP"].ToString().Trim();
            cardInfo[4] = gList["IP归属地"].IsNull() ? "未知" : gList["IP归属地"].ToString().Trim();
            cardInfo[4] = cardInfo[4].Replace("|", "-").Replace("0-", string.Empty).Replace(" ", string.Empty); ;
            
            if (!gList["域名注册信息"].IsNull())
            {
                var regInfo = gList["域名注册信息"];
                cardInfo[5] = regInfo["注册商"].IsNull() ? "未知" : regInfo["注册商"].ToString().Trim();
                cardInfo[6] = regInfo["注册商邮箱"].IsNull() ? "未知" : regInfo["注册商邮箱"].ToString().Trim();
                cardInfo[7] = regInfo["注册商手机号"].IsNull() ? "未知" : regInfo["注册商手机号"].ToString().Trim();
                cardInfo[7] = cardInfo[7].Replace("+86.", string.Empty);
                cardInfo[8] = regInfo["注册人邮箱"].IsNull() ? "未知" : regInfo["注册人邮箱"].ToString().Trim();
                cardInfo[9] = regInfo["注册人手机号"].IsNull() ? "未知" : regInfo["注册人手机号"].ToString().Trim();
                cardInfo[9] = cardInfo[9].Replace("+86.", string.Empty);
                cardInfo[10] = regInfo["注册时间"].IsNull() ? "未知" : regInfo["注册时间"].ToString().Trim();
                cardInfo[11] = regInfo["过期时间"].IsNull() ? "未知" : regInfo["过期时间"].ToString().Trim();
            }
            for(int i=0;i < cardInfo.Length;i++)
                cardInfo[i] = cardInfo[i].IsNullOrEmpty() ? "未知" : cardInfo[i].Trim();
            return string.Join("\t", cardInfo);
        }

        public string FormClass()
        {
            string fileType = string.Empty;
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

        private void IPShowResult(string input)
        {
            if (!string.IsNullOrEmpty(input) && progressBar2.Value < 5001 && !string.IsNullOrEmpty(input.Split('\t')[0].Replace(OpUtil.Blank.ToString(), string.Empty)))
            {
                if (progressBar2.Value % 100 == 0)
                {
                    Thread.Sleep(100);
                }
                List<Dictionary<string, string>> res = IpConvertHost(input.Split('\t')[0]);
                FillDGV(res);
                progressBar2.Value += 1;
            }
        }

        private List<Dictionary<string, string>> IpConvertHost(string ip)
        {
            string url = Global.FastIaoUrl + "/capture_host_by_ip";
            HttpWebRequest req = ConfigPost(url);
            HttpWebResponse resp;
            Dictionary<string, string> pairs = new Dictionary<string, string> { { "ip", ip } };
            byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(pairs));
            req.ContentLength = data.Length;

            List<Dictionary<string, string>> res = new List<Dictionary<string, string>>();
            Dictionary<string, string> tmp = new Dictionary<string, string>
            {
                { "IP", ip },{ "domain", string.Empty },{ "addtime", string.Empty },
                { "uptime", string.Empty },{ "memo", string.Empty }
            };

            try
            {
                using (var stream = req.GetRequestStream())
                    stream.Write(data, 0, data.Length);
                resp = (HttpWebResponse)req.GetResponse();
            }
            catch (Exception ex)
            {
                tmp["memo"] = ex.Message;
                res.Add(tmp);
                return res;
            }

            JObject json = JObject.Parse(new StreamReader(resp.GetResponseStream()).ReadToEnd());

            if (json["status"].ToString() == "fail" || JsonConvert.SerializeObject(json["data"]) == "[]")
            {
                tmp["memo"] = "未查询到绑定过的域名";  // todo 这里后续需要根据接口的返回值进行优化
                res.Add(tmp);
                return res;
            }
            else
            {
                foreach (var dat in json["data"])
                {
                    Dictionary<string, string> dict = new Dictionary<string, string>
                {
                    { "IP", ip },
                    { "domain", dat["domain"].ToString() },
                    { "addtime", dat["addtime"].ToString() },
                    { "uptime", dat["uptime"].ToString() },
                    { "memo", string.Empty }
                };
                    res.Add(dict);
                }
            }
            return res;
        }

        private void Button1_Click(object sender, EventArgs e)
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

        List<string> contentList = new List<string>() { "IP", "domain", "addtime", "uptime", "memo" };

        private void FillDGV(List<Dictionary<string, string>> result)
        {
            try
            {
                foreach (Dictionary<string, string> tmp in result) 
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    foreach (string content in contentList)
                    {
                        DataGridViewTextBoxCell textCell = new DataGridViewTextBoxCell();
                        textCell.Value = tmp[content];
                        dr.Cells.Add(textCell);
                    }
                    dataGridView1.Rows.Add(dr);
                }
            }
            catch
            {
            }
        }
    }
}
