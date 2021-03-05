using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QQSpiderPlugin
{
    class QQCrawler
    {
        public const string filedSeperator = "\t";
        private Session session;
        private List<string> groupHeaders = new List<string>() { "群号", "群名称", "群人数", "群上限", "群主", "地域", "分类", "标签", "群简介" };
        private List<string> actHeaders = new List<string>() { "账号", "昵称", "国家", "省市", "城市", "性别", "年龄", "头像地址" };

        public QQCrawler(Session session)
        {
            this.session = session;
        }
        /// <summary>
        /// 根据给定的账号爬取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string QueryAct(List<string> ids)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Join("\t", actHeaders));
            string url = "https://find.qq.com/proxy/domain/cgi.find.qq.com/qqfind/buddy/search_v3";
            foreach (string id in ids)
            {
                Dictionary<string, string> pairs = new Dictionary<string, string>
                {
                    { "num", "20"},
                    {"page", "0" },
                    { "sessionid", "0"},
                    { "keyword", id},
                    { "agerg", "0"},
                    { "sex", "0"},
                    { "firston", "1"},
                    { "video", "0"},
                    { "country", "0"},
                    { "province", "0"},
                    { "city", "0"},
                    { "district", "0"},
                    { "hcountry", "0"},
                    { "hprovince", "0"},
                    { "hcity", "0"},
                    { "hdistrict", "0"},
                    {"online", "1" },
                    {"ldw", session.Ldw }
                };
                int count = 0, retries = 2;
                while (count < retries)
                {
                    try
                    {
                        Response resp = this.session.Post(url, pairs);
                        QueryResult qResult = this.ParseAct(resp.Text);
                        if (qResult.code > 0)
                            sb.Append(qResult.result);
                        Thread.Sleep(1000);
                        break;
                    }
                    catch
                    {
                        count += 1;
                    }
                }
            }
            return sb.ToString();
        }
        public string QueryGroup(List<string> gids)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Join("\t", groupHeaders));
            string url = "https://qun.qq.com/cgi-bin/group_search/pc_group_search";

            foreach (string id in gids)
            {
                Dictionary<string, string> pairs = new Dictionary<string, string>
                {
                    {"k", "交友"},
                    {"n", "8"},
                    {"st", "1"},
                    {"iso", "1"},
                    {"src", "1"},
                    {"v", "4903"},
                    {"bkn", session.Ldw},
                    {"isRecommend", "false"},
                    {"city_id", "0"},
                    {"from", "1"},
                    {"keyword", id},
                    {"sort", "0"}, // sort type: 0 deafult, 1 menber, 2 active
                    {"wantnum", "24"},
                    {"page", "0"},
                    {"ldw", session.Ldw}
                };
                int count = 0, retries = 2;
                while (count < retries)
                {
                    try
                    {
                        Response resp = this.session.Post(url, pairs);
                        QueryResult qResult = this.ParseGroup(resp.Text);
                        if (qResult.code > 0)
                            sb.Append(qResult.result);
                        Thread.Sleep(1000);
                        break;
                    }
                    catch // 这里是捕获不到异常的
                    {
                        count += 1;
                    }
                }
            }
            return sb.ToString();
        }

        private QueryResult ParseGroup(string text)
        {
            QueryResult qResult = new QueryResult();

            StringBuilder sb = new StringBuilder();

            try
            {
                JObject json = JObject.Parse(text);
                var gList = json["group_list"];

                foreach (var g in gList)
                {
                    GroupInfo groupInfo = new GroupInfo();
                    try
                    {
                        groupInfo = new GroupInfo(g);
                    }
                    catch
                    {
                        groupInfo = new GroupInfo();
                    }
                    sb.AppendLine(groupInfo.ToString());
                }
            }
            catch (Exception)
            {
                Console.WriteLine(new StackTrace().ToString());
                Console.WriteLine(text);
                qResult.code = -1;
            }
            qResult.code = 1;
            qResult.result = sb.ToString();
            return qResult;
        }

        private QueryResult ParseAct(string text)
        {
            QueryResult qResult = new QueryResult();
            StringBuilder sb = new StringBuilder();

            try
            {
                int retcode = -1;
                ActInfo actInfo;

                JObject json = JObject.Parse(text);

                retcode = (int)json["retcode"];
                qResult.code = retcode;
                if (retcode != 0)
                    return qResult;
                foreach(var a in ((JObject)((JObject)json["result"])["buddy"])["info_list"])
                {
                    try
                    {
                        actInfo = new ActInfo(a);
                    }
                    catch
                    {
                        actInfo = new ActInfo();
                    }
                    sb.AppendLine(actInfo.ToString());
                }
            }
            catch 
            {
                qResult.code = -1;
            }
            qResult.code = 1;
            qResult.result = sb.ToString();
            return qResult;
        }
        public static bool IsValidQQSession(Session session)
        {
            QQCrawler crawler = new QQCrawler(session);
            return String.IsNullOrEmpty(crawler.QueryGroup(new List<string>() { "826028580" }));
        }
    }
    public class ActInfo
    {
        string uin;
        string nick;
        string country;
        string province;
        string city;
        int gender;
        int age;
        string url;
        public ActInfo()
        {
            this.uin = String.Empty;
            this.nick = String.Empty;
            this.country = String.Empty;
            this.province = String.Empty;
            this.city = String.Empty;
            this.gender = 0;
            this.age = 0;
            this.url = String.Empty;
        }
        public ActInfo(JToken obj)
        {
            this.uin = (string)obj["uin"];
            this.nick = (string)obj["nick"];
            this.country = (string)obj["country"];
            this.province = (string)obj["province"];
            this.city = (string)obj["city"];
            this.gender = (int)obj["gender"]; // 1 男 2 女
            this.age = (int)obj["age"];
            this.url = (string)obj["url"];
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.uin).Append(QQCrawler.filedSeperator)
                .Append(this.nick).Append(QQCrawler.filedSeperator)
                .Append(this.country).Append(QQCrawler.filedSeperator)
                .Append(this.province).Append(QQCrawler.filedSeperator)
                .Append(this.city).Append(QQCrawler.filedSeperator)
                .Append(this.gender).Append(QQCrawler.filedSeperator)
                .Append(this.age).Append(QQCrawler.filedSeperator)
                .Append(this.url);

            return sb.ToString();
        }
    }
    public class GroupInfo
    {
        string code;
        string name;
        string member_num;
        string max_member_num;
        string owner_uin;
        string qaddr;
        string gcate;
        string labels;
        string memo;
        public GroupInfo()
        {
            code = String.Empty;
            name = String.Empty;
            member_num = String.Empty;
            max_member_num = String.Empty;
            owner_uin = String.Empty;
            qaddr = String.Empty;
            gcate = String.Empty;
            labels = String.Empty;
            memo = String.Empty;
        }
        public GroupInfo(JToken g)
        {
            code = (string)g["code"];
            name = Util.GenRwWTS((string)g["name"]);
            member_num = (string)g["member_num"];
            max_member_num = (string)g["max_member_num"];
            owner_uin = (string)g["owner_uin"];

            StringBuilder qaddrSb = new StringBuilder();
            foreach (var q in g["qaddr"])
                qaddrSb.Append(q.ToString());
            qaddr = qaddrSb.ToString();

            try
            {
                StringBuilder gcateSb = new StringBuilder();
                foreach (var l in g["gcate"])
                    gcateSb.Append(l.ToString()).Append("|");
                gcate = Util.GenRwWTS(gcateSb.ToString().Trim('|'));
            }
            catch
            {
                Console.WriteLine("解析gcat失败");
            }
            try
            {
                StringBuilder labelSb = new StringBuilder();
                foreach (var l in g["group_label"])
                    labelSb.Append(l["item"].ToString()).Append("|");
                labels = Util.GenRwWTS(labelSb.ToString().Trim('|'));
            }
            catch
            {
                Console.WriteLine("解析labels失败");
            }
            memo = Util.GenRwWTS((string)g["memo"]);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(code).Append(QQCrawler.filedSeperator)
                    .Append(name).Append(QQCrawler.filedSeperator)
                    .Append(member_num).Append(QQCrawler.filedSeperator)
                    .Append(max_member_num).Append(QQCrawler.filedSeperator)
                    .Append(owner_uin).Append(QQCrawler.filedSeperator)
                    .Append(qaddr).Append(QQCrawler.filedSeperator)
                    .Append(gcate).Append(QQCrawler.filedSeperator)
                    .Append(labels).Append(QQCrawler.filedSeperator)
                    .Append(memo);
            return sb.ToString();
        }
    }
}
