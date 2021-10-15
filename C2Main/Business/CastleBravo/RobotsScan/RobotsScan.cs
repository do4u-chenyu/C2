using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace C2.Business.CastleBravo.RobotsScan
{

    public partial class RobotsScan : Form
    {

        public RobotsScan()
        {
            InitializeComponent();
        }


        //读取json文件
        public static JObject ReadJson()
        {
            string jsonfile = Path.Combine(Application.StartupPath, "Resources/Templates/WebRobots.json"); 
            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject o = (JObject)JToken.ReadFrom(reader);
                    return o;
                }
            }
        }
        //下载C2内部文件
        private void DownLoad(List<string> results)
        {
            foreach(string item in results)
            {
                string FilePath = Path.Combine(Application.StartupPath, "Resources/Templates/"+item);//文件路径
                string FileName = Path.GetFileName(FilePath);
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "下载文件";
                saveFileDialog.Filter = "txt文件(*.txt)|*.txt";

                saveFileDialog.FileName = FileName;
                DialogResult dialogResult = saveFileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    System.Net.WebClient client = new System.Net.WebClient();
                    byte[] data = client.DownloadData(FilePath);//一个真正存放数据的地址，一般我们将连接存在数据库中，数据存放在数据服务器上
                    FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                    fs.Write(data, 0, data.Length);
                    fs.Close();
                    MessageBox.Show("下载成功！");
                }
            }
            
        }

        //下载推荐漏洞文件

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewLinkColumn && e.RowIndex > -1 && dataGridView1.CurrentCell is DataGridViewLinkCell)
            {
                DataGridViewLinkCell cell = (DataGridViewLinkCell)dataGridView1.CurrentCell;
                if (cell.Tag == null)
                    return;
                List<string> test = new List<string>() { "WebRobots.json", "DocxExample.dotx" };//文件路径

                DownLoad(test);
                //SaveScreenshotsToLocal(new List<WFDResult>() { TaskInfo.PreviewResults[e.RowIndex] });
            }
        }

        //对字符串进行utf-8编码
        public static string get_uft8(string unicodeString)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = utf8.GetBytes(unicodeString);
            String decodedString = utf8.GetString(encodedBytes);
            return decodedString;
        }

        //计算网站ico的hash值
        private string Get_Hash(string domain)
        {   //实现bsae64转码
            string target = "http://" + domain + "/favicon.ico";
            string utf8 = get_uft8(target);
            
            byte[] b = System.Text.Encoding.Default.GetBytes(utf8);
            string base64string = Convert.ToBase64String(b);

            string url = "https://www.fofa.so/result?qbase64=" + base64string;
            HttpWebRequest req = WebRequest.CreateHttp(url);
            req.Method = "GET";
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream resStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(resStream, Encoding.UTF8);
            string result = reader.ReadToEnd();
            string RegexStr = @"icon_hash=(.*?)\)";
            string p = Regex.Match(result, RegexStr).ToString().Replace("\"","").Replace("\\","");
            string q = p.Replace(")", "").Replace("icon_hash=","");
            return q;
        }

        public string ConvertImageToBase64(Image file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.Save(memoryStream, file.RawFormat);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }

        }


        private void InputTextBox_MouseDown(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "多个域名按换行分隔")
            {
                richTextBox1.Text = string.Empty;
            }
            richTextBox1.ForeColor = Color.Black;
        }

        //获取网站robots.txt
        private string RobotsContent(string url)   //参数为url
        {
            if (url.Length >= 4)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.Timeout = 8 * 1000;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream s = response.GetResponseStream();
                    StreamReader sRead = new StreamReader(s);
                    string postContent = sRead.ReadToEnd();
                    sRead.Close();
                    return postContent;
                }
                catch (Exception e)
                {
                    //MessageBox.Show(e.Message);
                    if (e.Message.Contains("SSL"))
                    {
                        try
                        {
                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 
                                                                             | SecurityProtocolType.Tls11 
                                                                             | SecurityProtocolType.Ssl3
                                                                             | SecurityProtocolType.Tls;
                            //忽略ssl证书验证
                            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                            request.Method = "GET";
                            request.Timeout = 8 * 1000;
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            Stream s = response.GetResponseStream();
                            StreamReader sRead = new StreamReader(s);
                            string postContent = sRead.ReadToEnd();
                            sRead.Close();
                            return postContent;
                        }
                        catch
                        {
                            //MessageBox.Show("无法获得该网站的robots.txt");
                            return null;
                        } 
                    }
                    else if (e.Message.Contains("操作超时"))
                    {
                        //MessageBox.Show("网络连接超时，请检查输入网站是否可以打开");
                        return null;
                    }
                    else if (e.Message.Contains("404") || e.Message.Contains("500"))
                    {
                        //MessageBox.Show("无法获得该网站的robots.txt文件");
                        return null;
                    }
                    else
                    {
                        //MessageBox.Show("请检查输入格式为http://域名/");
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }
        //获取匹配网站的特征
        private string Special(string domain)
        {
            string url = "http://" + domain + "/robots.txt";
            string robots = RobotsContent(url);
            List<string> special_result = new List<string>();
            if (Match(domain) != "无" && Match(domain)!=string.Empty)
            {
                string[] inputtext = robots.Split(Environment.NewLine.ToCharArray());
                List<string> inputtext1 = inputtext.ToList<string>();
                for (int k = inputtext1.Count - 1; k >= 0; k--)
                {
                    if (inputtext1[k].Contains("#") || inputtext1[k] == "" || inputtext1[k].Contains("Sitemap") || inputtext1[k].Contains("User-agent: *"))
                    {
                        inputtext1.Remove(inputtext1[k]);
                    }
                }
                for (int i = 0; i < inputtext1.Count; i++)
                {
                    string[] input1 = inputtext1[i].Split('/');
                    int j = input1.ToList<string>().FindLastIndex(str => str != "");

                    special_result.Add(input1[j]);
                }
                string result = string.Join(",", special_result);
                return result;
            }
            else
            {
                return "无";
            }
            
        }



        //获取匹配内网靶场网站结果
        private string Match(string domain)
        {
            List<string> website = new List<string>();
            JObject obj = JObject.Parse(ReadJson().ToString());
            foreach (var x in obj)
            {
                website.Add(x.Key);
            }
            string url = "http://" + domain + "/robots.txt";
            string robots = RobotsContent(url);
            List<string> result = new List<string>();
            if (robots == "User-agent: *\r\nDisallow: " || robots == "User-agent: *" || robots == "User-agent: *\r\nDisallow:\r\n" || robots == "User-agent: * \nDisallow: ")
            {
                string bresult = string.Join(",", result.ToArray());
                return "无";
            }
            else if (robots != null)
            {
                string[] inputtext = robots.Split(Environment.NewLine.ToCharArray());
                List<string> inputtext1 = inputtext.ToList<string>();
                for (int k = inputtext1.Count - 1; k >= 0; k--)
                {
                    if (inputtext1[k].Contains("#") || inputtext1[k] == "" || inputtext1[k].Contains("Sitemap"))
                    {
                        inputtext1.Remove(inputtext1[k]);
                    }
                }
                foreach (string item1 in website)
                {
                    for (int i = 0; i < inputtext1.Count; i++)
                    {
                        string[] input1 = inputtext1[i].Split('/');
                        int j = input1.ToList<string>().FindLastIndex(str => str != "");

                        if (i == inputtext1.Count - 1 && obj[item1].ToString().Replace("[", "").Replace("]", "").Replace(@"""", "").Replace("\n", "").Replace("\r", "").Contains(input1[j]))
                        {
                            result.Add(item1);
                            break;
                        }
                        else if (obj[item1].ToString().Replace("[", "").Replace("]", "").Replace(@"""", "").Replace("\n", "").Replace("\r", "").Contains(input1[j]))
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                string sresult = string.Join(",", result.ToArray());
                if (sresult == string.Empty)
                {
                    return "无";
                }
                else if (result.Count >= 6)
                {
                    return "无";
                }
                else
                {
                    return sresult;
                }
                
            }
            string aresult = string.Join(",", result.ToArray());
            return aresult;
        }

        private int runTime = 0;
        private DateTime StartTime;
        
        
        private void button1_Click(object sender, EventArgs e)
        {

            
            StartTime = DateTime.Now;

            int a = 1;
            string[] urls = richTextBox1.Text.Split('\n');
            foreach (string item in urls)
            //for(int i=0;i<urls.Length;i++ )
            {
                string hash = Get_Hash(item);
                DataGridViewRow dr = new DataGridViewRow();   //一行表格
                DataGridViewTextBoxCell textCellID = new DataGridViewTextBoxCell();   //textCell0  一个单元格
                DataGridViewTextBoxCell textCellURL = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textCellResult = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell textCellSpecial = new DataGridViewTextBoxCell();
                DataGridViewLinkCell textCellBugs = new DataGridViewLinkCell();
                DataGridViewTextBoxCell textCellHash = new DataGridViewTextBoxCell();

                textCellID.Value = a.ToString();
                textCellURL.Value = item;
                textCellResult.Value = Match(item);   //字符串类型
                textCellSpecial.Value = Special(item);
                textCellHash.Value = hash;
                textCellBugs.Value = "暂无";
                textCellBugs.Tag = Match(item);

                dr.Cells.Add(textCellID);
                dr.Cells.Add(textCellURL);
                dr.Cells.Add(textCellHash);
                dr.Cells.Add(textCellResult);   //一行单元格
                dr.Cells.Add(textCellSpecial);
                dr.Cells.Add(textCellBugs);
                

                dataGridView1.Rows.Add(dr);
                progressBar.Value = (a / urls.Length)*100;
                
                progressPercent.Text = progressBar.Value.ToString() + "%";
                scanUseTime.Text = (DateTime.Now - StartTime).ToString();
                runTime++;
                a++;
                
            }     
        }

        private void progressPercent_Click(object sender, EventArgs e)
        {

        }
    }
 }

