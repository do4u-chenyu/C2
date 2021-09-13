using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace C2.Business.CastleBravo.RobotsScan
{
    public partial class MainWin : Form
    {
        public MainWin()
        {
            InitializeComponent();
        }

        //读取json文件
        public static JObject Readjson()
        {
            string jsonfile = "C://Users//FH//Desktop//Robots.json"; //JSON文件路径

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

        private void button2_Click(object sender, EventArgs e)
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


            string[] inputtext = InputTextBox.Text.Replace(@"""", "").Split(Environment.NewLine.ToCharArray());  //将输入的robots.txt的文件内容放进inputtext列表
            List<string> inputtext1 = inputtext.ToList<string>();

            List<string> result = new List<string>();  //存放最终结果
            foreach (string item1 in website)  //取出模板内容  
            {
                for (int i = 0; i < inputtext1.Count; i++)    //输入字符串的内容
                {
                    if (inputtext1[i].Contains("#"))
                    {
                        inputtext1.Remove(inputtext1[i]);    //空格报错，#报错
                    }
                    string[] input1 = inputtext[i].Split('/');
                    int j = input1.ToList<string>().FindLastIndex(str => str != "");

                    if (i == inputtext.Length - 1 && ja[0][item1].ToString().Replace("[", "").Replace("]", "").Replace(@"""", "").Replace("\n", "").Replace("\r", "").Contains(input1[j]))
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


            if (sresult == string.Empty)
            {
                OutTextBox.Text = "对不起，未匹配到已有的网站模板";
            }
            else
            {
                OutTextBox.Text = sresult;
            } 

        }
    }
}
