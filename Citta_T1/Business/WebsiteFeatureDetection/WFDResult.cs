using C2.Core;
using System.Collections.Generic;

namespace C2.Business.WebsiteFeatureDetection
{
    class WFDResult
    {
        public WFDResult()
        {
            url = string.Empty;
            cur_url = string.Empty;
            title = string.Empty;
            prediction = string.Empty;
            prediction_ = string.Empty;
            Fraud_label = string.Empty;
            screen_shot = string.Empty;
            login = string.Empty;
            html_content_id = string.Empty;
            html_content = string.Empty;
        }

        //这里成员变量名必须和返回报文字段名一致

        public string url { get; set; }
        public string cur_url { get; set; }
        public string title { get; set; }
        public string prediction { get; set; }
        public string prediction_ { get; set; }
        public string Fraud_label { get; set; }
        public string screen_shot { get; set; }
        public string login { get; set; }
        public string html_content_id { get; set; }
        public string html_content { get; set; }

        public string JoinAllContent()
        {
            if (Global.WFDPredictionCodeDict.TryGetValue(prediction, out string tmpPre))
                prediction_ = tmpPre;

            return string.Join("\t", new string[] { url, cur_url, title, prediction, prediction_, Fraud_label, screen_shot, login, html_content_id, html_content});
        }

        public string JoinMember()
        {
            return string.Join("\t", new List<string>() { "url", "cur_url", "title", "prediction", "prediction_", "Fraud_label", "screen_shot_id", "jump", "html_content_id", "html_content" });
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
