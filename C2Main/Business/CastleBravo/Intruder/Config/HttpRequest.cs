using C2.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace C2.Business.CastleBravo.Intruder.Config
{
    class HttpRequest
    {
        public static String getHTMLEncoding(String contenType, String body)
        {
            if (String.IsNullOrEmpty(contenType) && String.IsNullOrEmpty(body))
            {
                return string.Empty;
            }
            body = body.ToUpper();

            String encode = string.Empty;
            Match m = Regex.Match(contenType, @"charset=(?<charset>[\w\-]+)", RegexOptions.IgnoreCase);
            if (m.Success)
            {
                encode = m.Groups["charset"].Value.ToUpper();
            }
            else
            {
                if (String.IsNullOrEmpty(body))
                {
                    return string.Empty;
                }
                m = Regex.Match(body, @"charset=['""]{0,1}(?<charset>[\w\-]+)['""]{0,1}", RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    encode = m.Groups["charset"].Value.ToUpper();
                }
            }
            if ("UTF8".Equals(encode))
            {
                encode = "UTF-8";
            }
            return encode;


        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受     
        }

        public static ServerInfo SendRequestGetHeader(ConfigIntruder config, String url, int timeout, bool keepAlive)
        {
            Stopwatch st = new Stopwatch();
            st.Start();
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            ServerInfo res = new ServerInfo();
            Stream rs = null;
            StreamReader sr = null;
            try
            {
                //设置模拟http访问参数
                Uri uri = new Uri(url);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Accept = "*/*";
                request.UserAgent = config.UserAgent;
                request.Method = config.Method;
                request.KeepAlive = keepAlive;
                request.Timeout = timeout * 1000;
                request.Referer = "http://www.baidu.com/";
                request.AllowAutoRedirect = false;
                request.ServicePoint.Expect100Continue = false;
                request.ServicePoint.UseNagleAlgorithm = false;
                request.ServicePoint.ConnectionLimit = 1024;
                request.AllowWriteStreamBuffering = false;
                request.Proxy = null;

                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                }
                try
                {
                    response = (HttpWebResponse)request.GetResponse();

                }
                catch (WebException e)
                {
                    response = (HttpWebResponse)e.Response;
                }

                res.contentType = response.ContentType;
                res.powerBy = response.Headers["X-Powered-By"];
                res.location = response.Headers["Location"];
                //res.length = response.ContentLength;
                res.length = int.Parse(response.Headers["Content-Length"]);
                res.code = (int)(response.StatusCode);
                res.server = response.Server;
            }
            catch { }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
                if (rs != null)
                {
                    rs.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            st.Stop();
            res.runTime = st.ElapsedMilliseconds;
            return res;
        }

        public static ServerInfo SendRequestGetBody(ConfigIntruder config, String url, int timeout,bool keeAlive,string passWord)
        {
            Stopwatch st = new Stopwatch();
            st.Start();

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            ServerInfo res = new ServerInfo();
            Stream rs = null;
            StreamReader sr = null;

            try
            {
                //设置模拟http访问参数
                Uri uri = new Uri(url);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = config.Method;
                request.ContentType = "application/x-www-form-urlencoded";

                //request.Accept = "*/*";
                //request.UserAgent = config.UserAgent;
                request.KeepAlive = keeAlive;
                request.Timeout = timeout * 1000;
                //request.Referer = "http://www.baidu.com/";
                request.AllowAutoRedirect = false;
                //request.ServicePoint.Expect100Continue = false;
                //request.ServicePoint.UseNagleAlgorithm = false;
                //request.ServicePoint.ConnectionLimit = 1024;
                //request.AllowWriteStreamBuffering = false;
                request.Proxy = null;

                byte[] data = Encoding.UTF8.GetBytes(passWord);
                
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }

                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                }
                try
                {
                    response = (HttpWebResponse)request.GetResponse();

                }
                catch (WebException e)
                {
                    res.contentType = e.Message;
                    if (e.Response != null)
                        response = (HttpWebResponse)e.Response;
                }
                if (response != null)
                {
                    res.code = (int)(response.StatusCode);
                    res.content = response.Headers.ToString();
                    if(!res.content.Contains("Connection"))
                        res.responseHeaders = ("HTTP / 1.1" + OpUtil.StringBlank + res.code + "\n" + "Connection: close" + "\n" + res.content);
                    else
                        res.responseHeaders = ("HTTP / 1.1" + OpUtil.StringBlank + res.code + "\n" + res.content);
                    res.contentType = response.ContentType;
                    res.powerBy = response.Headers["powerby"];
                    res.location = response.Headers["location"];
                   
                    res.server = response.Server;
                    rs = response.GetResponseStream();
                    Encoding readerEncode = Encoding.Default;
                    sr = new StreamReader(rs, readerEncode);
                    res.body = sr.ReadToEnd();
                    try 
                    {
                        res.length = int.Parse(response.Headers["Content-Length"]);
                    } catch 
                    {
                        res.length = res.body.Length;
                    };
                    String encoding = getHTMLEncoding(response.ContentType, res.body);

                    /*
                    if (!"".Equals(encoding) && !"UTF-8".Equals(encoding, StringComparison.OrdinalIgnoreCase))
                    {
                        rs = response.GetResponseStream();
                        sr = new StreamReader(rs, Encoding.GetEncoding(encoding));
                        res.body = sr.ReadToEnd();
                    };
                    */
                }
            }
            catch
            {

        }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
                if (rs != null)
                {
                    rs.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            st.Stop();
            res.runTime = st.ElapsedMilliseconds;

            return res;
        }

        public static String getHTML(String url, int timeout)
        {
            String html = string.Empty;
            HttpWebResponse response = null;
            StreamReader sr = null;
            HttpWebRequest request = null;
            try
            {

                //设置模拟http访问参数
                Uri uri = new Uri(url);
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Accept = "*/*";
                request.Method = "GET";
                request.Timeout = timeout * 1000;
                request.AllowAutoRedirect = false;
                response = (HttpWebResponse)request.GetResponse();

                Stream s = response.GetResponseStream();


                //读取服务器端返回的消息 
                String encode = getHTMLEncoding(response.Headers["Content-Type"], string.Empty);
                if (!String.IsNullOrEmpty(encode))
                {
                    sr = new StreamReader(s, Encoding.GetEncoding(encode));
                    html = sr.ReadToEnd();
                }
                else
                {
                    sr = new StreamReader(s, Encoding.UTF8);
                    html = sr.ReadToEnd();
                }
                s.Close();
                sr.Close();
            }
            catch
            {

            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return html;
        }
    }
}
