using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace QQSpiderPlugin
{
    class QrLogin
    {
        private string jsVersion = "21073010";
        Session session;
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
                Dictionary<string, string> param = new Dictionary<string, string>();

                string url = "http://ui.ptlogin2.qq.com/cgi-bin/login";
                //param.Add("id", "2732844");

                param.Add("appid", "715030901");
                param.Add("daid", "73");
                param.Add("pt_no_auth", "1");
                param.Add("s_url", "https://qun.qq.com/member.html");


                resp = this.session.Get(url, param);

                string pattern = @"imgcache.qq.com/ptlogin/ver/(\d+)/js";
                var matches = Regex.Matches(resp.Text, pattern);
                if (matches.Count > 1)
                    this.jsVersion = matches[1].Value;

                this.session.UpdateHeaders("Referer", url);
                //获取二维码
                //https://ssl.ptlogin2.qq.com/ptqrshow?appid=715030901&e=2&l=M&s=3&d=72&v=4&t=0.12108830293255246&daid=73&pt_3rd_aid=0
                url = "https://ssl.ptlogin2.qq.com/ptqrshow";

                param = new Dictionary<string, string>
                       {
                           {"appid", "715030901" },
                           {"e", "2" },
                           {"l", "M" },
                           {"s", "3" },
                           {"d", "72" },
                           {"v", "4" },
                           {"t", String.Format("{0:%.17f}", new Random().Next(0, 1)) },
                           {"daid", "73" },
                           {"pt_3rd_aid", "0" },
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

        public byte[] GetKeyWordQRCode()
        {
            session = new Session(
                new Dictionary<string, string> {
                    { "User-Agent", "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, " +
                    "like Gecko) Chrome/29.0.1547.59 QQ/8.9.3.21169 Safari/537.36"},
                    { "Referer",  "https://qun.qq.com/"}
                });


            Response resp;
            byte[] imgBytes = Response.Empty.Content;
            try
            {
                //需要用到的接口
                String url1 = "https://xui.ptlogin2.qq.com/cgi-bin/xlogin";
                String url2 = "https://ssl.ptlogin2.qq.com/ptqrshow";

                Dictionary<string, string> param1 = new Dictionary<string, string>
                {
                    { "pt_disable_pwd" , "1"},
                    { "appid", "715030901"},
                    { "daid", "73"},
                    { "pt_no_auth", "1"},
                    { "s_url", "https://qun.qq.com/"}
                };
                resp = this.session.Get(url1, param1);
                Dictionary<string, string> param2 = new Dictionary<string, string>
                {
                    { "appid" , "715030901"},
                    { "e", "2"},
                    { "l", "M"},
                    { "s", "3"},
                    { "d", "72"},
                    { "v", "4"},
                    { "t", String.Format("{0:%.16f}", new Random().NextDouble()) },
                    { "daid", "73" },
                    { "pt_3rd_aid", "0" }
                };
                resp = this.session.Get(url2, param2);
                imgBytes = resp.Content;
            }
            catch { }
            return imgBytes;
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
                /*
                QQ登录接口
                https://qun.qq.com/member.html#
                &ptqrtoken=664235145&
                ptredirect=1&
                h=1&
                t=1&
                g=1&
                from_ui=1&
                ptlang=2052&
                action=0-0-1628671582481&
                js_ver=21073010&
                js_type=1&
                login_sig=LXzAxTeoAQNIPbr-G*fTMIyn84RHXI73iHOrJ-YXscgWZr3vTBeo*qfVB1Pbd4Cp&
                pt_uistyle=40&
                aid=715030901&
                daid=73&
                has_onekey=1&
                */
                string url = "https://ssl.ptlogin2.qq.com/ptqrlogin";
                Dictionary<string, string> param = new Dictionary<string, string>
                       {
                           {"u1","https://qun.qq.com/member.html" },
                           {"ptqrtoken", Util.GenQrToken(qrSig) },
                           {"ptredirect", "1" },
                           {"h", "1" },
                           {"t", "1" },
                           {"g", "1" },
                           {"from_ui", "1" },
                           {"ptlang", "2052" },
                           {"action", String.Format("0-0-{0:%d}", Util.GetTimeStamp() * 1000) },
                           {"js_ver", this.jsVersion.ToString() },
                           {"js_type", "1" },
                           {"login_sig", loginSig },
                           {"pt_uistyle", "40" },
                           {"aid", "715030901" },
                           {"daid", "73" },
                           {"has_onekey", "1" }
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
        public Dictionary<string, object> KeyWordLogin()
        {
            String url3 = "https://ssl.ptlogin2.qq.com/ptqrlogin";

            Dictionary<string, object> loginResult = new Dictionary<string, object>();

            int status = -1;
            string errorMsg = String.Empty;

            string qrsig = this.session.Cookies.GetCookieValue("qrsig");
            string pt_login_sig = this.session.Cookies.GetCookieValue("pt_login_sig");

            Dictionary<string, string> params3 = new Dictionary<string, string>
            {
                { "u1", "https://qun.qq.com/" },
                { "ptqrtoken", Util.GenQrToken(qrsig) },
                { "ptredirect", "1"},
                { "h", "1"},
                { "t", "1"},
                { "g", "1"},
                { "from_ui", "1"},
                { "ptlang", "2052"},
                { "action", String.Format("0-0-{0:%d}", Util.GetTimeStamp() * 1000)},
                { "js_type", "1"},
                { "login_sig", pt_login_sig},
                { "pt_uistyle", "40"},
                { "aid", "715030901"},
                { "daid", "73"},
            };
            Response resp3 = new Response();
            try
            {
                resp3 = this.session.Get(url3, params3);
                string result = resp3.Text;
                if (result.Contains("二维码未失效"))
                    status = 0;
                else if (result.Contains("二维码认证中"))
                    status = 1;
                else if (result.Contains("登录成功")) 
                { 
                    status = 2;
                    string new_url = result.Split(',')[2].Trim('\'');
                    this.session.Get(new_url);  // 访问返回的url，以便获取p_skey
                }
                else if (result.Contains("二维码已失效"))
                    status = 3;
                else
                    errorMsg = result;
            }
            catch
            {
                if (resp3 != null)
                    errorMsg = resp3.StatusCode.ToString();
                else
                    errorMsg = "unknow";
            }
            loginResult.Add("status", status);
            loginResult.Add("time", Util.GetTimeStamp());
            loginResult.Add("errorMsg", errorMsg);
            loginResult.Add("session", this.session);
            return loginResult;
        }
    }
}
