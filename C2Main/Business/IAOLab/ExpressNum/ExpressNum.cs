using C2.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace C2.IAOLab.ExpressNum
{
    public class ExpressNum
    {
        private static ExpressNum instance;
        public static ExpressNum GetInstance()
        {
            if (instance == null)
                instance = new ExpressNum();
            return instance;
        }
        private readonly string EBusinessID = "1719835";//即用户ID，快递鸟
        private readonly string ApiKey = "604e4b0f-8d4d-435e-8dcd-7a5e90eeb99c";//即API key
        private readonly string ReqURL = "https://api.kdniao.com/Ebusiness/EbusinessOrderHandle.aspx"; //快递鸟请求URL
        private readonly string UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
        private readonly string URL = "http://cha.ebaitian.cn/api/json?type=get&appid=10000681&module=getExpressInfo&"; //我要查URL
        public string ExpressSearch(string input)
        {
            Dictionary<string, string> nameCode = new Dictionary<string, string>
            {
                { "shentong", "STO" },  //申通
                { "huitong", "HTKY" },   //百世
                { "yuantong", "YTO" },   //圆通
                { "tiantian", "HHTT" }   //天天
            };
            string name = QueryName(input); //我要查 查询快递公司结果
            if (!Regex.IsMatch(input, @"(^[A-Za-z0-9]{10,16}$)"))
                return string.Format("{0}\t{1}{2}", input, "快递单号错误", Environment.NewLine);
            if (name == string.Empty)
                return string.Format("{0}\t{1}{2}", input, "快递单号查询失败", Environment.NewLine);
            if(!nameCode.ContainsKey(name))
                return string.Format("{0}\t{1}{2}", input, "无法查询该快递公司单号", Environment.NewLine);
            try
            {
                string locRex = @"\u0022AcceptStation\u0022 : \u0022(.*?)\u0022,";    //获取快递途径站点
                string result = QueryResult(nameCode[name], input);
                MatchCollection loc = Regex.Matches(result, locRex);
                string startLoc = loc[0].Groups[1].Value;
                string endLoc = loc[loc.Count - 2].Groups[1].Value;

                string startRex1 = "【(.*?)】";
                startLoc = Regex.Match(startLoc, startRex1).Groups[1].Value;

                string endRex1 = "(.*?)，";
                endLoc = Regex.Match(endLoc, endRex1).Groups[1].Value;
                return string.Format("{0}\t{1}\t{2}\t{3}", input, startLoc, endLoc, Environment.NewLine);
            }
            catch
            {
                return string.Format("{0}\t{1}{2}", input, "快递单号查询失败", Environment.NewLine);
            }
            
        }
        
        private string QueryName(string input)    //返回快递公司名称
        {
            string appKey = "&appkey=c33d13c4fbdfd31448674b777336c9c9";
            string sign = ST.SHA256("appid=10000681&module=getExpressInfo&order=" + input + appKey);
            string url = string.Format("{0}order={1}&sign={2}", URL, input, sign);
            WebClient client = new WebClient { Encoding = Encoding.UTF8 };
            string info = client.DownloadString(url);
            string nameStr = @"\u0022id\u0022:\u0022(.*?)\u0022,";
            string name = Regex.Match(info, nameStr).Groups[1].ToString();   //获得快递公司的名称  .*\u0022id\u0022:\u0022(.*?)\u0022,
            return name;  
        }

        private string QueryResult(string courier, string input)
        {
            string requestData = "{'CustomerName': '','OrderCode': '','ShipperCode':'" +
                courier + "','LogisticCode':'" + input + "',}";
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                { "RequestData", HttpUtility.UrlEncode(requestData, Encoding.UTF8) },
                { "EBusinessID", EBusinessID },
                { "RequestType", "1002" },
                { "DataType", "2" },
                { "DataSign", HttpUtility.UrlEncode(Encrypt(requestData), Encoding.UTF8) }
            };
            return Post(ReqURL, param); ;
        }

        /// Post方式提交数据，返回网页的源代码
        private string Post(string url, Dictionary<string, string> param)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var p in param)
                sb.Append(p.Key + "=" + p.Value).Append('&');  // 后面多一个应该没影响
         
            byte[] postData = Encoding.UTF8.GetBytes(sb.ToString());
            string result = string.Empty;
            HttpWebRequest request = null;
            try
            {
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Timeout = 30 * 1000;
                request.UserAgent = UserAgent;
                request.Method = "POST";
                request.ContentLength = postData.Length;
                // 发送
                request.GetRequestStream().Write(postData, 0, postData.Length);
                // 接收
                result = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.UTF8).ReadToEnd(); 
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            finally
            {
                if (request != null)
                    request.Abort();
            }
            return result;
        }

        ///电商Sign签名
        private string Encrypt(string content)
        {
            return ST.EncodeBase64(ST.GenerateMD5(content + ApiKey, "UTF8"));
        }
  
    }
}

