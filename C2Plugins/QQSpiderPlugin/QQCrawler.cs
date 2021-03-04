using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QQSpiderPlugin
{
    class QQCrawler
    {
        private Session session;
        private string ldw;
        private List<string> groupHeaders = new List<string>() { "群名称", "群号", "群人数", "群上限", "群主", "地域", "分类", "标签", "群简介" };
        private List<string> actHeaders = new List<string>() { "uin", "nick", "country", "province", "city", "age", "lnick", "url" };

        public QQCrawler(Session session, string ldw)
        {
            this.session = session;
            this.ldw = ldw;
        }
        /// <summary>
        /// 根据给定的账号爬取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> QueryAct(List<string> ids)
        {
            List<string> results = new List<string>();
            string url = "https://find.qq.com/proxy/domain/cgi.find.qq.com/qqfind/buddy/search_v3";
            foreach(string id in ids)
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
                    {"ldw", this.ldw }
                };
                int count = 0, retries = 2;
                while (count < retries)
                {
                    try
                    {
                        Response resp = this.session.Post(url, pairs);
                        QueryResult qResult = this.ParseAct(resp.Text);
                        if (qResult.code > 0)
                        {
                            results.Add(qResult.result);
                            Thread.Sleep(1000);
                        }
                    }
                    catch
                    {
                        count += 1;
                    }
                }
            }
            return results;
        }
        public List<string> QueryGroup(List<string> gids)
        {
            List<string> results = new List<string>();
            string url = "http://qun.qq.com/cgi-bin/group_search/pc_group_search";
            foreach (string id in gids)
            {
                int page = 0, pageNum = 20;
                while (page < pageNum)
                {
                    Dictionary<string, string> pairs = new Dictionary<string, string>
                    {
                        {"k", "交友"},
                        {"n", "8"},
                        {"st", "1"},
                        {"iso", "1"},
                        {"src", "1"},
                        {"v", "4903"},
                        {"bkn", this.ldw},
                        {"isRecommend", "false"},
                        {"city_id", "0"},
                        {"from", "1"},
                        {"keyword", id},
                        {"sort", "0"}, // sort type: 0 deafult, 1 menber, 2 active
                        {"wantnum", "24"},
                        {"page", page.ToString()},
                        {"ldw", this.ldw}
                    };
                    int count = 0, retries = 2;
                    while (count < retries)
                    {
                        try
                        {
                            Response resp = this.session.Post(url, pairs);
                            QueryResult qResult = this.ParseGroup(resp.Text);
                            if (qResult.code > 0)
                            {
                                results.Add(qResult.result);
                                Thread.Sleep(1000);
                            }
                        }
                        catch
                        {
                            count += 1;
                        }
                    }
                    page += 1;
                }
            }
            return results;
        }

        private QueryResult ParseGroup(string text)
        {
            QueryResult qResult = new QueryResult();

            StringBuilder sb = new StringBuilder();
            sb.Append(String.Join("\t", this.groupHeaders));

            try
            {
                Dictionary<string, string> result = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
                var gList = result["group_list"];
                foreach(var g in gList)
                {
                    //string name = this.WTS
                }

            }
            catch
            {
            }

            return qResult;
        }

        private QueryResult ParseAct(string text)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据给定的账号爬取信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<string> GroupCrawle(List<string> ids)
        {
            List<string> results = new List<string>();
            foreach (string id in ids)
            {
                results.Add(this.Query(id));
            }
            return results;
        }

        protected virtual string Query(string id)
        {
            throw new NotImplementedException();
        }
    }
}
