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

    partial class RobotsScan : StandardDialog
    {
        public RobotsScan()
        {
            InitializeComponent();
            this.OKButton.Text = "查找";
            this.CancelBtn.Text = "退出";
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
        //获取网站robots.txt
        private string RobotsContent(string url)
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
                    if (e.Message.Contains("SSL"))
                    {
                        try
                        {
                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 
                                                                             | SecurityProtocolType.Tls11 
                                                                             | SecurityProtocolType.Ssl3
                                                                             | SecurityProtocolType.Tls;
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
                        catch
                        {
                            MessageBox.Show("无法获得该网站的robots.txt");
                            return null;
                        } 
                    }
                    else if (e.Message.Contains("操作超时"))
                    {
                        MessageBox.Show("网络连接超时，请检查输入网站是否可以打开");
                        return null;
                    }
                    else if (e.Message.Contains("404"))
                    {
                        MessageBox.Show("无法获得该网站的robots.txt文件");
                        return null;
                    }
                    else
                    {
                        MessageBox.Show("请检查输入格式为http://域名/");
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        protected override bool OnOKButtonClick()
        {         
            JObject obj = JObject.Parse(ReadJson().ToString());  
            List<string> website = new List<string>();
            foreach (var x in obj)
            {
                website.Add(x.Key);
            }
            string url = inputBox.Text + "robots.txt";
            string robots = RobotsContent(url);
            if (robots == "User-agent: *\r\nDisallow: " || robots == "User-agent: *" || robots == "User-agent: *\r\nDisallow:\r\n" || robots == "User-agent: * \nDisallow: ") 
            {
                MessageBox.Show("网站robots.txt过于简单，无法匹配");
            }
            else if (robots != null)
            {
                string[] inputtext = robots.Split(Environment.NewLine.ToCharArray());
                List<string> inputtext1 = inputtext.ToList<string>();
                List<string> result = new List<string>();
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

                        if (i==inputtext1.Count-1 && obj[item1].ToString().Replace("[", "").Replace("]", "").Replace(@"""", "").Replace("\n", "").Replace("\r","").Contains(input1[j]))
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
                    resultBox.Text = "对不起，未匹配到已有的网站模板";
                }
                else if (result.Count >= 6)
                {
                    MessageBox.Show("网站robots.txt过于简单，无法匹配");
                }
                else
                {
                    resultBox.Text = sresult.Replace(",", "\n");
                }
            }
            return false;            
        }

    }
 }

