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
        }

        //读取json文件
        public static JObject Readjson()
        {
            string jsonfile = Path.Combine(Application.StartupPath, "Resources/Templates/WebRobots.json"); //JSON文件路径

            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject o = (JObject)JToken.ReadFrom(reader);

                    //var value = o.ToString();
                    return o;
                }
            }
        }

        private string RobotsContent(string url)
        {
            if (url.Length >= 4)
            {
                try
                {
                    //创建一个http请求
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    //请求方式
                    request.Method = "GET";
                    request.Timeout = 5 * 1000;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    Stream s = response.GetResponseStream();
                    StreamReader sRead = new StreamReader(s);
                    string postContent = sRead.ReadToEnd();
                    sRead.Close();
                    return postContent;
                }
                catch
                {
                    MessageBox.Show("请检查输入网站是否可以打开，请检查输入格式为http://域名/");
                    return null;
                }

            }
            else
            {
                return null;
            }

        }

        private void Search_Click(object sender, EventArgs e)
        {
            List<string> s = new List<string>();

            JObject val = Readjson();
            string t = string.Join(",", s.ToArray());
            string data = val.ToString();
            data = "[" + data + "]";
            JArray ja = (JArray)JsonConvert.DeserializeObject(data);
            //string pred = ja[0]["bbs.shouyoufabu.com"].ToString().Replace("[", "").Replace("]", "").Replace(@"""", "").Replace("\n", "").Replace("\r", "").Replace(" ", ""); //此处在ja[0]后加(website里的值)提取出键对应的值
            //string[] pred = ja[0]["bbs.shouyoufabu.com"].ToString().Replace("[", "").Replace("]", "").Split(','); //此处在ja[0]后加(website里的值)提取出键对应的值

            string jsoncontent = ja[0].ToString(); //将json文件内容输出为字符串

            string RegexStr = @"""(.*)""\:";   //在@“”类型的字符串里面输入双引号要写成两个双引号并列
            MatchCollection webname = Regex.Matches(jsoncontent, RegexStr, RegexOptions.IgnoreCase);  //利用正则取出全部模板中的模板名称
            List<string> website = new List<string>();
            foreach (Match w in webname)
            {
                website.Add(w.Groups[0].Value.Replace(":", "").Replace(@"""", ""));     //website列表中存放网站模板的名称

            }
            string webname1 = string.Join(",", website.ToArray());  //website列表转化为字符串进行打印


            string url = inputBox.Text + "robots.txt";
            string robots = RobotsContent(url);


            //string[] inputtext = robots.Replace(@"""", "").Split(Environment.NewLine.ToCharArray());  //将输入的robots.txt的文件内容放进inputtext列表

            if (robots == "User-agent: *\nDisallow:\n" || robots == "User-agent: *" || robots == "User-agent: *\nDisallow:/\n")
            {
                MessageBox.Show("网站robots.txt过于简单，无法匹配");
            }
            else if (robots != null)
            {
                string[] inputtext = robots.Split(Environment.NewLine.ToCharArray());  //将输入的robots.txt的文件内容放进inputtext列表
                List<string> inputtext1 = inputtext.ToList<string>();

                List<string> result = new List<string>();  //存放最终结果

                for (int k = inputtext1.Count - 1; k >= 0; k--)
                {
                    if (inputtext1[k].Contains("#") || inputtext1[k] == "" || inputtext1[k].Contains("Sitemap"))
                    {
                        inputtext1.Remove(inputtext1[k]);
                    }
                }
                foreach (string item1 in website)  //取出模板内容  
                {
                    for (int i = 0; i < inputtext1.Count; i++)    //输入字符串的内容
                    {
                        //if(inputtext1[i])
                        string[] input1 = inputtext1[i].Split('/');
                        int j = input1.ToList<string>().FindLastIndex(str => str != "");

                        if (i == inputtext1.Count - 1 && ja[0][item1].ToString().Replace("[", "").Replace("]", "").Replace(@"""", "").Replace("\n", "").Replace("\r", "").Contains(input1[j]))
                        {
                            result.Add(item1);
                            break;
                        }
                        else if (ja[0][item1].ToString().Replace("[", "").Replace("]", "").Replace(@"""", "").Replace("\n", "").Replace("\r", "").Contains(input1[j]))
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                string sresult = string.Join(",", result.ToArray());  //website列表转化为字符串进行打印

                //resultBox.Text = sresult;

                /*
                 * 调用RobotsContent查结果,要么有：返回匹配结果，要么没有,"未匹配到已有的网站模板";有的话调用Search_Click主逻辑
                 */
                if (sresult == string.Empty)
                {
                    resultBox.Text = "对不起，未匹配到已有的网站模板";
                }
                else
                {
                    resultBox.Text = sresult.Replace(",", "\n");

                }
            }


        }
    }
}
