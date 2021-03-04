using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QQSpiderPlugin
{
    class QrLogin
    {
        private string jsVersion = "10226";
        private string sourceURL = "http://find.qq.com/index.html?version=1&im_version=5533&width=910&height=610&search_target=0";
        Session session;
        public CookieContainer Cookies
        {
            get { return this.session.Cookies; }
        }
        public Session Session
        {
            get { return this.session; }
        }
        public QrLogin()
        {
            ResetSession();
        }
        private void ResetSession()
        {
            session = new Session(
                new Dictionary<string, string> { 
                    { "User-Agent", "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, " +
                    "like Gecko) Chrome/29.0.1547.59 QQ/8.9.3.21169 Safari/537.36"} ,
                    { "Host", "ui.ptlogin2.qq.com"}
                });
        }
        public Response GetQRCode()
        {
            ResetSession();
            Response resp = Response.Empty;
            try
            {
                string url = "http://ui.ptlogin2.qq.com/cgi-bin/login";
                Dictionary<string, string> param = new Dictionary<string, string>(); 
                param.Add("appid", "715030901");
                param.Add("daid", "73");
                param.Add("pt_no_auth", "1");
                param.Add("s_url", this.sourceURL);

                resp = this.session.Get(url, param);

                string pattern = @"imgcache.qq.com/ptlogin/ver/(\d+)/js";
                var matches = Regex.Matches(resp.Text, pattern);
                if (matches.Count > 1)
                    this.jsVersion = matches[1].Value;

                this.session.UpdateHeaders("Referer", url);

                url = "http://ptlogin2.qq.com/ptqrshow";
                param = new Dictionary<string, string>
                       {
                           {"appid", "715030901" },
                           {"e", "2" },
                           {"l", "M" },
                           {"s", "3" },
                           {"d", "72" },
                           {"v", "4" },
                           {"t", String.Format("{0:%.17f}", new Random().Next(0, 1)) },
                           {"daid", "73" }
                       };
                resp = this.session.Get(url, param);
                this.session.UpdateHeaders("Content-Type", "image/png");
                this.session.UpdateHeaders("Cache-Control", "no-cache, no-store");
                this.session.UpdateHeaders("Pragma", "no-cache");
            }
            catch
            {
                resp = Response.Empty;
            }
            return resp;
        }
        public Dictionary<string, object> Login()
        {
            string loginSig = this.session.Cookies.GetCookieValue("pt_login_sig");
            string qrSig = this.session.Cookies.GetCookieValue("qrsig");
            Dictionary<string, object> loginResult = new Dictionary<string, object>();

            int status = -1;
            string errorMsg = String.Empty;

            if (!String.IsNullOrEmpty(loginSig) && !String.IsNullOrEmpty(qrSig))
            {
                string url = "http://ptlogin2.qq.com/ptqrlogin";
                Dictionary<string, string> param = new Dictionary<string, string>
                       {
                           {"u1", this.sourceURL },
                           {"ptqrtoken", Util.GenQrToken(qrSig) },
                           {"ptredirect", "1" },
                           {"h", "1" },
                           {"t", "1" },
                           {"from_ui", "1" },
                           {"action", String.Format("0-0-{0:%d}", Util.GetTimeStamp() * 1000) },
                           {"js_ver", this.jsVersion.ToString() },
                           {"js_type", "1" },
                           {"login_sig", loginSig },
                           {"pt_uistyle", "40" },
                           {"aid", "715030901" },
                           {"daid", "73" },
                       };

                Response resp = null;
                try
                {
                    resp = this.session.Get(url, param);
                    string result = resp.Text;
                    if (result.Contains("二维码未失效"))
                        status = 0;
                    else if (result.Contains("二维码认证中"))
                        status = 1;
                    else if (result.Contains("登录成功"))
                        status = 2;
                    else if (result.Contains("二维码已失效"))
                        status = 3;
                    else
                        errorMsg = result;
                }
                catch
                {
                    if (resp != null)
                        errorMsg = resp.StatusCode.ToString();
                    else
                        errorMsg = "unknow";
                }
            }
            loginResult.Add("status", status);
            loginResult.Add("time", Util.GetTimeStamp());
            loginResult.Add("errorMsg", errorMsg);
            return loginResult;
        }
        public string GetLDW()
        {
            byte[] imgBytes = this.GetQRCode().Content;
            if (imgBytes.Length == 0)
                return String.Empty;
            Image img = Image.FromStream(new MemoryStream(imgBytes));
            Thread _thread = new Thread(() =>
            {
                Application.Run(new QrCodeForm(img));
            });
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.Start();

            int count = 0;
            int maxTimes = 15;
            while (count < maxTimes)
            {
                Dictionary<string, object> result = this.Login();
                object status = -1;
                if (result.TryGetValue("status", out status))
                    if ((int)status == 2)
                        break;
                Console.WriteLine(result.ToString(), "请使用QQ手机客户端扫码登录！");
                System.Threading.Thread.Sleep(1000);
                count += 1;
            }

            if (count == maxTimes)
            {
                Console.WriteLine("扫码超时！");
                return String.Empty;
            }
            string skey = this.session.Cookies.GetCookieValue("skey");
            if (String.IsNullOrEmpty(skey))
                return String.Empty;
            return Util.GenBkn(skey);
        }
    }
}
