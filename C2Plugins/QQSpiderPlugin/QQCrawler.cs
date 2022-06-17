﻿using Newtonsoft.Json;
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

        public QQCrawler(Session session)
        {
            this.session = session;
        }
        /// <summary>
        /// 根据给定的账号爬取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string QueryAct(string id)
        {
            QueryResult defaultResult = new QueryResult();
            defaultResult.result = new ActInfo(id).ToString();
            string url = "https://find.qq.com/proxy/domain/cgi.find.qq.com/qqfind/buddy/search_v3";
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
                    QueryResult qResult = this.ParseAct(id, resp.Text);
                    if (qResult.code > 0)
                        defaultResult = qResult;
                    Thread.Sleep(1000);
                    break;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                    count += 1;
                }
            }
            return defaultResult.result.ToString();
        }
        public string QueryGroup(string id)
        {
            QueryResult defaultResult = new QueryResult();
            defaultResult.result = new GroupInfo(id).ToString();
            string url = "https://qun.qq.com/cgi-bin/group_search/pc_group_search";

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
                    QueryResult qResult = this.ParseGroup(id, resp.Text);
                    if (qResult.code > 0)
                        defaultResult = qResult;
                    Thread.Sleep(1000);
                    break;
                }
                catch // 这里是捕获不到异常的
                {
                    count += 1;
                }
            }
            return defaultResult.result.ToString();
        }
        
        public List<string> QueryKeyWord(string id)
        {
            List<string> resultList = new List<string> { };

            string url = "https://qun.qq.com/cgi-bin/group_search/pc_group_search";
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                {"from", "1" },
                { "keyword", id },
                { "wantnum", "24" },
                { "page", "0" },
                {"sort", "2"},  // sort type: 0 默认排序, 1 人数优先, 2 活跃优先
                {"isRecommend", "false"}
            };
            List<string> target_keys = new List<string> 
            { "code", "name", "member_num", "max_member_num", "owner_uin", "qaddr", "gcate", "labels", "memo", "url"};
            int i = -1;
            while (resultList.Count < 150 && i < 10)  // 一个词最多抓100条，且请求不超过10次
            {
                i++;
                param["page"] = i.ToString();
                try
                {
                    Response resp = this.session.Post(url, param);
                    JObject json = JObject.Parse(resp.Text);
                    if (json["errcode"].ToString()=="0" && json["ec"].ToString() != "99997")  // 不满足这些条件，表示被爬虫被限制了
                    {
                        if (json.ContainsKey("group_list"))
                        {
                            string group_list_string = json["group_list"].ToString();
                            List<JToken> group_list1 = json["group_list"].ToList();
                            foreach(JToken x in group_list1)
                            {
                                string tmp = "";
                                foreach (string key in target_keys)
                                {
                                    if (x.SelectToken(key) != null)
                                    {
                                        string s = x[key].ToString();
                                        if (key == "qaddr")
                                        {
                                            StringBuilder qaddrSb = new StringBuilder();
                                            foreach (var q in x["qaddr"])
                                                qaddrSb.Append(q.ToString());
                                            s = qaddrSb.ToString();
                                        }
                                        if (key == "gcate")
                                        {
                                            StringBuilder gcateSb = new StringBuilder();
                                            foreach (var l in x["gcate"])
                                                gcateSb.Append(l.ToString()).Append("|");
                                            s = Util.GenRwWTS(gcateSb.ToString().Trim('|'));
                                        }
                                        if (key == "labels")
                                        {
                                            StringBuilder labelSb = new StringBuilder();
                                            foreach (var l in x["labels"])
                                                labelSb.Append(l["label"].ToString()).Append("|");
                                            s = Util.GenRwWTS(labelSb.ToString().Trim('|'));
                                        }
                                        tmp = tmp + "\t" + s;
                                    }
                                    else
                                    {
                                        tmp = tmp + "\t" + "";
                                    }
                                }
                                resultList.Add(tmp.Trim('\t'));
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                catch { }
                Thread.Sleep(new Random().Next(3000, 8000));  //随机停3到8秒
            }
            return resultList;
        }

        private QueryResult ParseGroup(string id, string text)
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
                        groupInfo = new GroupInfo(id);
                    }
                    sb.AppendLine(groupInfo.ToString());
                }
                qResult.code = 1;
                qResult.result = sb.ToString();
            }
            catch (Exception)
            {
                qResult.code = -1;
            }
            return qResult;
        }
        /// <summary>
        /// code = 0: 解析成功，但是没数据
        /// code > 0: 解析成功，且有数据
        /// code < 0: 解析异常
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        /// 

        private List<string> ParseKeyWord(string id, string text)
        {
            List<string> resultList = new List<string>();
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
                        groupInfo = new GroupInfo(id);
                     }
                    resultList.Add(groupInfo.ToString());
                }
            }
            catch (Exception)
            {
            }
            return resultList;
        }
        private QueryResult ParseAct(string id, string text)
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
                {
                    // 查询异常
                    qResult.code = -1;
                    return qResult;
                }

                foreach(var a in ((JObject)((JObject)json["result"])["buddy"])["info_list"])
                {
                    try
                    {
                        actInfo = new ActInfo(a);
                    }
                    catch
                    {
                        actInfo = new ActInfo(id);
                    }
                    sb.AppendLine(actInfo.ToString());
                }
                qResult.code = 1;
                qResult.result = sb.ToString();
            }
            catch 
            {
                // 查询异常
                qResult.code = -1;
            }
            return qResult;
        }
        public static bool IsValidQQSession(Session session)
        {
            QQCrawler crawler = new QQCrawler(session);
            return !String.Equals(crawler.QueryGroup("826028580"), new GroupInfo("826028580").ToString());
        }
    }
    public class ActInfo
    {
        string uin;
        string nick;
        string country;
        string province;
        string city;
        String gender;
        int age;
        string url;
        public ActInfo()
        {
            this.uin = String.Empty;
            this.nick = String.Empty;
            this.country = String.Empty;
            this.province = String.Empty;
            this.city = String.Empty;
            this.gender = String.Empty;
            this.age = 0;
            this.url = String.Empty;
        }
        public ActInfo(string uin)
        {
            this.uin = uin;
            this.nick = String.Empty;
            this.country = String.Empty;
            this.province = String.Empty;
            this.city = String.Empty;
            this.gender = String.Empty;
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
            this.gender = (string)obj["gender"] == "1" ? "男" : ((string)obj["gender"] == "2" ? "女" : "未知");
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
        string url;
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
            url = String.Empty;
        }
        public GroupInfo(string code)
        {
            this.code = code;
            this.name = String.Empty;
            this.member_num = String.Empty;
            this.max_member_num = String.Empty;
            this.owner_uin = String.Empty;
            this.qaddr = String.Empty;
            this.gcate = String.Empty;
            this.labels = String.Empty;
            this.memo = String.Empty;
            this.url = String.Empty;
        }
        public GroupInfo(JToken g)
        {
            code = (string)g["code"];
            name = Util.GenRwWTS((string)g["name"]);
            member_num = (string)g["member_num"];
            max_member_num = (string)g["max_member_num"];
            owner_uin = (string)g["owner_uin"];
            url = (string)g["url"];

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
                foreach (var l in g["labels"])
                    labelSb.Append(l["label"].ToString()).Append("|");
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
                    .Append(memo).Append(QQCrawler.filedSeperator)
                    .Append(url);
            return sb.ToString();
        }
    }
}
