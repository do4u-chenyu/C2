using C2.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

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
            string jsonfile = Path.Combine(Global.TemplatesPath, "WebRobots.json"); 
            using (System.IO.StreamReader file = File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject o = (JObject)JToken.ReadFrom(reader);
                    return o;
                }
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

        //计算本地ico文件的md5值
        private static string GetMD5Hash(string domain)
        {
            try
            {
                string url = "http://" + domain + "/favicon.ico";
                System.Net.WebRequest webreq = System.Net.WebRequest.Create(url);
                System.Net.WebResponse webres = webreq.GetResponse();
                System.IO.Stream stream = webres.GetResponseStream();
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(stream);
                //file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch
            {
                return "无";
            }
           
        }

        private static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
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
                    int j = input1.ToList<string>().FindLastIndex(str => !string.IsNullOrEmpty(str));

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
                        int j = input1.ToList<string>().FindLastIndex(str => !string.IsNullOrEmpty(str));

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
            System.Net.WebClient wc = new System.Net.WebClient();

            StartTime = DateTime.Now;
            while (this.dataGridView1.Rows.Count != 0)
            {
                this.dataGridView1.Rows.RemoveAt(0);
            }
            int a = 1;
            string[] urls = richTextBox1.Text.Split('\n');
            
            foreach (string item in urls)
            {
                if (item != string.Empty)
                {
                    string hash = GetMD5Hash(item);
                    DataGridViewRow dr = new DataGridViewRow();   //一行表格
                    DataGridViewTextBoxCell textCellID = new DataGridViewTextBoxCell();   //textCell0  一个单元格
                    DataGridViewTextBoxCell textCellURL = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textCellResult = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell textCellSpecial = new DataGridViewTextBoxCell();
                    //DataGridViewLinkCell textCellBugs = new DataGridViewLinkCell();
                    DataGridViewTextBoxCell textCellHash = new DataGridViewTextBoxCell();

                    textCellID.Value = a.ToString();
                    textCellURL.Value = item;
                    textCellResult.Value = Match(item);   //字符串类型
                    textCellSpecial.Value = Special(item);
                    textCellHash.Value = hash;
                    dr.Cells.Add(textCellID);
                    dr.Cells.Add(textCellURL);   
                    dr.Cells.Add(textCellHash);
                    dr.Cells.Add(textCellResult);   //一行单元格
                    dr.Cells.Add(textCellSpecial);
                   
                    dataGridView1.Rows.Add(dr);
                    
                }

                progressBar.Value = (a / urls.Length)*100;
                progressBar.Step = 1;
                progressPercent.Text = progressBar.Value.ToString() + "%";
                //startTime = DateTime.Parse(startTime.ToString("yyyy-MM-dd HH:mm"));
                scanUseTime.Text = (DateTime.Now - StartTime).ToString().Split('.')[0];

                runTime++;
                a++;
                
            }     
        }

    }
 }

