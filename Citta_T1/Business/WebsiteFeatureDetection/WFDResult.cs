namespace C2.Business.WebsiteFeatureDetection
{
    class WFDResult
    {
        //这里成员变量名必须和返回报文字段名一致

        public string url { get; set; }
        public string cur_url { get; set; }
        public string html_content_id { get; set; }
        public string title { get; set; }
        public string html_content { get; set; }
        public string prediction { get; set; }
        public string login { get; set; }
        public string screen_shot { get; set; }

        public string JoinAllContent()
        {
            return string.Join("\t", new string[] { url, cur_url, html_content_id, title, html_content, prediction, prediction, login, screen_shot });
        }
    }

    class WFDAPIResult
    {
        public string RespMsg { set; get; }
        public string Datas { set; get; }
        public WFDAPIResult()
        {
            RespMsg = string.Empty;
            Datas = string.Empty;
        }
    }
}
