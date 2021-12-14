using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Text;
using System.Net;
using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using C2.Core;

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
            DateTime.Now.ToString("yyyy-MM-dd");
            Dictionary<string, string> nameCode = new Dictionary<string, string>
            {
                { "shentong", "STO" },  //申通
                { "huitong", "HTKY" },   //百世
                { "yuantong", "YTO" },   //圆通
                { "tiantian", "HHTT" }   //天天
            };
            string name = QueryName(input); //我要查 查询快递公司结果
            if (name == string.Empty)
                return string.Format("{0}\t{1}{2}", input, "快递单号查询失败", Environment.NewLine);
            if(!nameCode.ContainsKey(name))
                return string.Format("{0}\t{1}{2}", input, "无法查询该快递公司单号", Environment.NewLine);
            string result = GetResult(nameCode[name],input);
            string locRex = @"\u0022AcceptStation\u0022 : \u0022(.*?)\u0022,";    //获取快递途径站点
            MatchCollection loc = Regex.Matches(result,locRex);
            string startLoc = loc[0].Groups[1].Value;
            string endLoc = loc[loc.Count - 2].Groups[1].Value;
            string startRex1 = "【(.*?)】";
            startLoc = Regex.Match(startLoc,startRex1).Groups[1].Value;
            string endRex1 = "(.*?)，";
            endLoc = Regex.Match(endLoc, endRex1).Groups[1].Value;
            return string.Format("{0}\t{1}\t{2}\t{3}",input,startLoc,endLoc,Environment.NewLine);
        }

       
        
        public string QueryName(string input)    //返回快递公司名称
        {
            string appKey = "&appkey=c33d13c4fbdfd31448674b777336c9c9";
            string sign = ST.Sha256("appid=10000681&module=getExpressInfo&order=" + input + "" + appKey);
            string url = string.Format("{0}order={1}&sign={2}", URL, input, sign);
            WebClient client = new WebClient{Encoding = Encoding.UTF8};
            string info = client.DownloadString(url);
            string nameStr = @"\u0022id\u0022:\u0022(.*?)\u0022,";
            string name = Regex.Match(info, nameStr).Groups[1].ToString();   //获得快递公司的名称  .*\u0022id\u0022:\u0022(.*?)\u0022,
            return name;  
        }

        public string GetResult(string courier,string input)
        {
            string requestData = "{'CustomerName': '','OrderCode': ''," + 
                "'ShipperCode':'"+ courier + "','LogisticCode':'" + input + "',}";
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                { "RequestData", HttpUtility.UrlEncode(requestData, Encoding.UTF8) },
                { "EBusinessID", EBusinessID }, 
                { "RequestType", "1002" },
                { "DataType", "2" }
            };
            string dataSign = Encrypt(requestData, ApiKey, "UTF-8");
            param.Add("DataSign", HttpUtility.UrlEncode(dataSign, Encoding.UTF8));
            return Post(ReqURL, param); ;
        }

        /// Post方式提交数据，返回网页的源代码
        private string Post(string url, Dictionary<string, string> param)
        {
            StringBuilder postData = new StringBuilder();
            if (param != null && param.Count > 0)
            {
                foreach (var p in param)
                {
                    if (postData.Length > 0)
                        postData.Append("&");
                    postData.Append(p.Key);
                    postData.Append("=");
                    postData.Append(p.Value);
                }
            }
            byte[] byteData = Encoding.GetEncoding("UTF-8").GetBytes(postData.ToString());
            string result;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Timeout = 30 * 1000;
                request.UserAgent = UserAgent;
                request.Method = "POST";
                request.ContentLength = byteData.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(byteData, 0, byteData.Length);
                    stream.Flush();
                    stream.Close();
                }
                using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream backStream = response.GetResponseStream();
                    StreamReader sr = new StreamReader(backStream, Encoding.GetEncoding("UTF-8"));
                    result = sr.ReadToEnd();
                    sr.Close();
                    backStream.Close();
                    response.Close();
                }
                
                request.Abort();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        ///电商Sign签名
        private string Encrypt(string content, string keyValue, string charset)
        {
            if (keyValue != null)
                return ST.Base64Encode(ST.GenerateCharsetMD5(content + keyValue, charset), charset);
            return ST.Base64Encode(ST.GenerateCharsetMD5(content, charset), charset);
        }
  
    }
}

