using C2.Core;
using System.Collections.Generic;

namespace C2.Business.WebsiteFeatureDetection
{
    class WFDResult
    {
        private List<string> fixColList;
        public string AllContent { set; get; }
        public string AllCol { set; get; }
        public Dictionary<string, string> ResDict { set; get; }

        public WFDResult()
        {
            //由于字典无序导致结果文件内容顺序混乱，因此记录固定列，剩余列往后放
            fixColList = new List<string>() { "url", "cur_url", "title", "prediction", "prediction_", "Fraud_label", "screen_shot", "login", "html_content_id", "html_content" };
        }

        public WFDResult(Dictionary<string, string> resDict) : this()
        {
            ResDict = resDict;
            SetFixColValue();
            JoinAllContentAndCol();
        }

        private void SetFixColValue()
        {
            url = ResDict.TryGetValue("url", out string tmpUrl) ? tmpUrl : string.Empty;
            cur_url = ResDict.TryGetValue("cur_url", out string tmpCurUrl) ? tmpCurUrl : string.Empty;
            title = ResDict.TryGetValue("title", out string tmpTitle) ? tmpTitle : string.Empty;
            prediction = ResDict.TryGetValue("prediction", out string tmpPrediction) ? tmpPrediction : string.Empty;
            prediction_ = Global.WFDPredictionCodeDict.TryGetValue(prediction, out string tmpPre) ? tmpPre : string.Empty;
            Fraud_label = Global.WFDFraudCodeDict.TryGetValue(prediction, out string tmpFraud) ? tmpFraud : string.Empty;
            screen_shot = ResDict.TryGetValue("screen_shot", out string tmpScreenShot) ? tmpScreenShot : string.Empty;
            login = ResDict.TryGetValue("login", out string tmpLogin) ? tmpLogin : string.Empty;
            html_content_id = ResDict.TryGetValue("html_content_id", out string tmpContentId) ? tmpContentId : string.Empty;
            html_content = ResDict.TryGetValue("html_content", out string tmpContent) ? tmpContent : string.Empty;

            if(!ResDict.ContainsKey("prediction_"))
                ResDict.Add("prediction_", prediction_);
            if (!ResDict.ContainsKey("Fraud_label"))
                ResDict.Add("Fraud_label", Fraud_label);
        }
        public void JoinAllContentAndCol()
        {
            List<string> contentList = new List<string>();
            List<string> colList = new List<string>();
            foreach (string fixCol in fixColList)
            {
                colList.Add(fixCol);
                contentList.Add(ResDict.ContainsKey(fixCol) ? ResDict[fixCol] : string.Empty);
            }

            var key = ResDict.Keys.GetEnumerator();
            while (key.MoveNext())
            {
                if (fixColList.Contains(key.Current))
                    continue;

                colList.Add(key.Current);
                contentList.Add(ResDict[key.Current]);
            }

            AllCol = string.Join("\t", colList);
            AllContent = string.Join("\t", contentList);

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
