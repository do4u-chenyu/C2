using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Text;
using System.Net;
using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

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
        private readonly string ReqURL = "https://api.kdniao.com/Ebusiness/EbusinessOrderHandle.aspx"; //请求URL
        
        public string ExpressSearch(string input)
        {
            DateTime.Now.ToString("yyyy-MM-dd");
            Dictionary<string, string> nameCode = new Dictionary<string, string>();
            nameCode.Add("shentong", "STO");  //申通
            nameCode.Add("huitong", "HTKY");   //百世
            nameCode.Add("yuantong", "YTO");   //圆通
            nameCode.Add("tiantian", "HHTT");   //天天
            string name = GetName(input); //我要查 查询快递公司结果
            if (name == string.Empty)
                return string.Format("{0}\t{1}{2}", input, "快递单号查询失败", Environment.NewLine);
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

        public static string Sha256(string data)    //sha256加密
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = SHA256.Create().ComputeHash(bytes);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                builder.Append(hash[i].ToString("X2"));
            return builder.ToString().ToLower();
        }
        
        public string GetName(string input)    //返回快递公司名称
        {
            string sign = Sha256("appid=10000681&module=getExpressInfo&order=" + input + "&appkey=c33d13c4fbdfd31448674b777336c9c9");
            string url = string.Format("http://cha.ebaitian.cn/api/json?type=get&appid=10000681&module=getExpressInfo&order={0}&sign={1}", input, sign);
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string info = client.DownloadString(url);
            string nameStr = @"\u0022id\u0022:\u0022(.*?)\u0022,";
            string name = Regex.Match(info, nameStr).Groups[1].ToString();   //获得快递公司的名称  .*\u0022id\u0022:\u0022(.*?)\u0022,
            return name;  
        }

        public string GetResult(string courier,string input)
        {
            string requestData = "{" + "'CustomerName': ''," + 
                "'OrderCode': ''," + 
                "'ShipperCode':'"+ courier + "'," + 
                "'LogisticCode':'" + input + "'," + "}";
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("RequestData", HttpUtility.UrlEncode(requestData, Encoding.UTF8));
            param.Add("EBusinessID", EBusinessID);
            param.Add("RequestType", "1002");
            string dataSign = Encrypt(requestData, ApiKey, "UTF-8");
            param.Add("DataSign", HttpUtility.UrlEncode(dataSign, Encoding.UTF8));
            param.Add("DataType", "2");
            string result = SendPost(ReqURL, param);
            return result;
        }

        /// Post方式提交数据，返回网页的源代码
        private string SendPost(string url, Dictionary<string, string> param)
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
                request.Referer = url;
                request.Accept = "*/*";
                request.Timeout = 30 * 1000;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
                request.Method = "POST";
                request.ContentLength = byteData.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(byteData, 0, byteData.Length);
                stream.Flush();
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream backStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(backStream, Encoding.GetEncoding("UTF-8"));
                result = sr.ReadToEnd();
                sr.Close();
                backStream.Close();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        ///<summary>
        ///电商Sign签名
        private string Encrypt(string content, string keyValue, string charset)
        {
            if (keyValue != null)
                return Base64(MD5(content + keyValue, charset), charset);
            return Base64(MD5(content, charset), charset);
        }

        ///<summary>
        /// 字符串MD5加密
        private string MD5(string str, string charset)
        {
            byte[] buffer = Encoding.GetEncoding(charset).GetBytes(str);
            try
            {
                MD5CryptoServiceProvider check;
                check = new MD5CryptoServiceProvider();
                byte[] somme = check.ComputeHash(buffer);
                string ret = "";
                foreach (byte a in somme)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("X");
                    else
                        ret += a.ToString("X");
                }
                return ret.ToLower();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// base64编码
        private string Base64(string str, string charset)
        {
            return Convert.ToBase64String(Encoding.GetEncoding(charset).GetBytes(str));
        }
    }
}

