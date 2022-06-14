using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace C2.Log
{
    class Log
    {
        private static DirectoryInfo info = new DirectoryInfo(Environment.GetEnvironmentVariable("TMP"));
        private static string xmlDirectory = Path.Combine(info.FullName, "tmpRedisASK");
        private static string xmlPath = Path.Combine(xmlDirectory, "tmpRedisASK.xml");
        private static readonly XmlDocument xDoc = new XmlDocument();
        public string Token;
        private const string APIUrl = "https://113.31.119.85:53374/apis/";
        private string LoginUrl = APIUrl + "Login";
        private string uploadUrl = "https://47.94.39.209:8000/api/log/upload";
        static LogItem logItem = new LogItem();
        public static ConcurrentQueue<LogItem> ConcurrenLogs = new ConcurrentQueue<LogItem>();
        string userName = string.Empty;

        //日志：工号/功能模块/动作/时间/IP
        public void LogManualButton(string modelName,string type)
        {
            string startTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            // 同理,这里也放到发送进程里，判断文件在不在，读文件都是耗时操作，不要放在主进程里
            // 发送日志的线程里 来获取用户名，而且显然，这个地方只要获取一次就够了
            // 不需要每次都读文件获取用户名
            //if (File.Exists(xmlPath))
            //{
                //xDoc.Load(xmlPath);
                //userName = xDoc.SelectSingleNode(@"IdenInformation/userInfo/userName").InnerText;
            //}
            // IP 放到发送进程里去, 获取IP访问网络会卡,不要放在主进程
            // 而且显然，这个地方只要获取一次就够了,不需要每次都访问网络GetIP
            // string ip = GetPublicIp();

            // 这都是什么神鬼设计, 直接加入队列，东西放入队列不费时间
            // 但你这里又开启线程，让线程把东西放入队列，然后等线程结束
            // 这一顿骚操作 比 直接放入队列费事更多，属于行为艺术
            // Task t = Task.Factory.StartNew(() =>
            //{
            AddQueueEn("", modelName, type, startTime, "");
            //});
            // Task.WaitAll(t);
            // 每个日志动作都单开一个线程，很挫，但勉强接受
            LogThread();
        }
        public void LogThread()
        {
            ThreadStart childref = new ThreadStart(QueueDequeue);
            Thread childThread = new Thread(childref);
            childThread.Start();
        }
        private void QueueDequeue()
        {
            if (ConcurrenLogs.Count > 0)
            {
                LogItem topElement = ConcurrenLogs.ElementAt(0);
                ConcurrenLogs.TryDequeue(out logItem);//出队

                try
                {
                    LogUpload(topElement);
                }
                catch { }
            }
        }
        private void GetToken()
        {
            Dictionary<string, string> pairsL = new Dictionary<string, string>
            {
                { "user_id", userName }, { "password", new TOTP().GetTotp(userName)}
            };
            Response resp = new HttpHandler().Post(LoginUrl, pairsL);
            Dictionary<string, string> resDict = resp.ResDict;
            resDict.TryGetValue("token", out Token);
        }

        private void LogUpload(LogItem reciveMessage)
        {
            GetToken();
            Dictionary<string, string> pairs = new Dictionary<string, string> {
                { "userid", reciveMessage.UserName.Replace(@"""",string.Empty)},
                { "tasktypename", reciveMessage.ModelName.Replace(@"""",string.Empty)},
                { "submit_time", reciveMessage.StartTime.Replace(@"""",string.Empty)},
                { "action",reciveMessage.Type.Replace(@"""",string.Empty)},
                { "ip",reciveMessage.Ip.Replace("}",string.Empty).Replace(@"""",string.Empty)}
            };
            try
            {
                Response resp = new HttpHandler().Post(uploadUrl, pairs, Token);
            }
            catch { }
        }
        private void AddQueueEn(string userName, string modelName, string type, string startTime, string ip)
        {
            logItem.UserName = userName;
            logItem.ModelName = modelName;
            logItem.Type = type;
            logItem.StartTime = startTime;
            logItem.Ip = ip;
            ConcurrenLogs.Enqueue(logItem);//入队
        }
        private string IPGet()
        {
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    return ipa.ToString();
            }
            return string.Empty;
        }
        private string GetPublicIp()
        {
            var urlList = new List<string>
            {
                "http://ip.qq.com/",
                "http://pv.sohu.com/cityjson?ie=utf-8",
                "http://ip.taobao.com/service/getIpInfo2.php?ip=myip"
            };
            string tempip = string.Empty;
            foreach (var a in urlList)
            {
                try
                {
                    var req = WebRequest.Create(a);
                    req.Timeout = 20000;
                    var response = req.GetResponse();
                    var resStream = response.GetResponseStream();
                    if (resStream != null)
                    {
                        var sr = new StreamReader(resStream, Encoding.UTF8);
                        var htmlinfo = sr.ReadToEnd();
                        //匹配IP的正则表达式
                        var r = new Regex("((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|\\d)\\.){3}(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]\\d|[1-9])", RegexOptions.None);
                        var mc = r.Match(htmlinfo);
                        //获取匹配到的IP
                        tempip = mc.Groups[0].Value;
                        resStream.Close();
                        sr.Close();
                        response.Dispose();
                    }
                    return tempip;
                }
                catch
                {
                    tempip = IPGet();
                }
            }
            return tempip;
        }
    }
    class LogItem
    {
        public string UserName { get; set; }
        public string ModelName { get; set; }
        public string Type { get; set; }
        public string StartTime { get; set; }
        public string Ip { get; set; }
    }
    class HttpHandler
    {
        private bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            //为了通过证书验证，总是返回true
            return true;
        }
        public Response Post(string url, Dictionary<string, string> postData, string token = "", int timeout = 10000)
        {
            Response resp = new Response();
            try
            {
                GC.Collect();//Http相关的资源没有正确释放引起操作超时
                ServicePointManager.DefaultConnectionLimit = 200;//系统支持同时存在http的connection个数过少引起操作超时

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemoteCertificateValidate);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                req.Timeout = timeout;
                string content = DictionaryToJson(postData);
                byte[] data = Encoding.UTF8.GetBytes(content);

                req.Method = "POST";
                req.ContentType = "application/json";
                req.ContentLength = data.Length;
                req.Headers.Add("Authorization", "Bearer " + token);

                req.KeepAlive = true;//解决GetResponse操作超时问题

                using (var stream = req.GetRequestStream())
                    stream.Write(data, 0, data.Length);
                resp = new Response((HttpWebResponse)req.GetResponse());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resp;
        }
        private string DictionaryToJson(Dictionary<string, string> dict)
        {
            if (dict.Count == 0)
                return string.Empty;

            return JsonConvert.SerializeObject(dict);
        }
    }
    class Response
    {
        Dictionary<string, string> resDict;
        string content;
        HttpStatusCode statusCode;
        private HttpWebResponse response;
        public readonly static Response Empty = new Response();
        public string Content
        {
            get { return this.content; }
        }
        public Dictionary<string, string> ResDict
        {
            get { return this.resDict; }
        }
        public HttpStatusCode StatusCode
        {
            get { return this.statusCode; }
        }
        public Response()
        {
            this.response = null;
        }
        public Response(HttpWebResponse resp)
        {
            this.response = resp;
            this.content = GetContent();
            this.resDict = JsonUtil.JsonStringToDictionary(this.content);
            this.statusCode = GetStatusCode();

        }
        private HttpStatusCode GetStatusCode()
        {
            if (response == null)
                throw new Exception("没有实例化的Response");
            return response.StatusCode;
        }
        private string GetContent()
        {
            if (response == null)
                throw new Exception("没有实例化的Response");

            Stream resStream = null;
            StreamReader reader = null;
            string content = string.Empty;
            try
            {
                using (resStream = response.GetResponseStream())
                {
                    using (reader = new StreamReader(resStream, Encoding.UTF8))
                    {
                        //通过ReadToEnd()把整个HTTP响应作为一个字符串取回，
                        content = reader.ReadToEnd().ToString();
                    }
                }
            }
            catch { }
            finally
            {
                if (resStream != null)
                    resStream.Close();
                if (reader != null)
                    reader.Close();
            }
            return content;
        }
    }
    class JsonUtil
    {
        public static Dictionary<string, string> JsonStringToDictionary(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<string, string>();

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);
        }
    }
    class TOTP
    {
        public string GetTotp(string user)
        {
            string base32EncodedSecret = ToBase32String(Encoding.UTF8.GetBytes(user.ToUpper() + "qazwer!@$#")).Replace("=", "");
            DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long counter = (long)Math.Floor((DateTime.UtcNow - epochStart).TotalSeconds / 60);
            return GetHotp(base32EncodedSecret, counter);
        }
        private string ToBase32String(byte[] bytes)
        {
            int inByteSize = 8;
            int outByteSize = 5;
            string base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            int bytesPosition = 0;
            int bytesSubPosition = 0;
            byte outputBase32Byte = 0;
            int outputBase32BytePosition = 0;

            StringBuilder builder = new StringBuilder(bytes.Length * inByteSize / outByteSize);
            while (bytesPosition < bytes.Length)
            {
                int bitsAvailableInByte = Math.Min(inByteSize - bytesSubPosition, outByteSize - outputBase32BytePosition);
                outputBase32Byte <<= bitsAvailableInByte;
                outputBase32Byte |= (byte)(bytes[bytesPosition] >> (inByteSize - (bytesSubPosition + bitsAvailableInByte)));
                bytesSubPosition += bitsAvailableInByte;
                if (bytesSubPosition >= inByteSize)
                {
                    bytesPosition++;
                    bytesSubPosition = 0;
                }

                outputBase32BytePosition += bitsAvailableInByte;
                if (outputBase32BytePosition >= outByteSize)
                {
                    outputBase32Byte &= 0x1F;  // 0x1F = 00011111 in binary
                    builder.Append(base32Alphabet[outputBase32Byte]);
                    outputBase32BytePosition = 0;
                }
            }

            if (outputBase32BytePosition > 0)
            {
                outputBase32Byte <<= (outByteSize - outputBase32BytePosition);
                outputBase32Byte &= 0x1F;  // 0x1F = 00011111 in binary
                builder.Append(base32Alphabet[outputBase32Byte]);
            }

            return builder.ToString();
        }
        private string GetHotp(string base32EncodedSecret, long counter)
        {
            byte[] message = BitConverter.GetBytes(counter).Reverse().ToArray(); // Assuming Intel machine (little endian)
            byte[] secret = ToByteArray(base32EncodedSecret);

            byte[] hash;
            using (HMACSHA1 hmac = new HMACSHA1(secret, true))
            {
                hash = hmac.ComputeHash(message);
            }
            int offset = hash[hash.Length - 1] & 0xf;
            int truncatedHash = ((hash[offset] & 0x7f) << 24) |
                ((hash[offset + 1] & 0xff) << 16) |
                ((hash[offset + 2] & 0xff) << 8) |
                (hash[offset + 3] & 0xff);
            int hotp = truncatedHash % 1000000; // 6-digit code and hence 10 power 6, that is a million
            return hotp.ToString("D6");
        }
        private byte[] ToByteArray(string secret)
        {
            byte[] mapping = { 26, 27, 28, 29, 30, 255, 255, 255, 255, 255,
                                        255, 255, 255, 255, 255, 0, 1, 2, 3, 4,
                                        5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
                                        15, 16, 17, 18, 19, 20, 21, 22, 23, 24,
                                        25 };

            secret = secret.ToUpperInvariant();
            byte[] byteArray = new byte[(secret.Length + 7) / 8 * 5];

            long shiftingNum = 0L;
            int srcCounter = 0;
            int destCounter = 0;
            for (int i = 0; i < secret.Length; i++)
            {
                long num = (long)mapping[secret[i] - 50];
                shiftingNum |= num << (35 - srcCounter * 5);

                if (srcCounter == 7 || i == secret.Length - 1)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        byteArray[destCounter++] = (byte)((shiftingNum >> (32 - j * 8)) & 0xff);
                    }
                    shiftingNum = 0L;
                }
                srcCounter = (srcCounter + 1) % 8;
            }
            return byteArray;
        }
    }
}
