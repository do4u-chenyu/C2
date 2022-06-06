using C2.Business.HTTP;
using C2.Business.WebsiteFeatureDetection;
using C2.Core;
using C2.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    public partial class YQTaskResult : Form
    {
        public YQTaskInfo taskInfo;
        public string statusMsg;
        public string datasourceFilePath;
        public string statusFilePath;
        public int pid;
        public YQTaskResult()
        {
            statusMsg = string.Empty;
            InitializeComponent();
        }

        public YQTaskResult(YQTaskInfo task) : this()
        {
            taskInfo = task;
            this.taskNameLabel.Text = taskInfo.TaskName;
            this.taskIDLabel.Text = taskInfo.TaskID;
            this.taskResultTextBox.Text = taskInfo.ResultFilePath;
            datasourceFilePath = taskInfo.DatasourceFilePath;
            statusFilePath = Path.Combine(taskInfo.ResultFilePath, taskInfo.TaskID + "_info.txt");
            pid = GetPIdByXml();
        }

        public int GetPIdByXml()
        {
            int pId = 0;
            string xmlPath = Path.Combine(Global.UserWorkspacePath, "侦察兵", "YQTasks.xml");
            XmlDocument xDoc = new XmlDocument(); ;
            try
            {
                xDoc.Load(xmlPath);
            }
            catch (Exception ex)
            {
                HelpUtil.ShowMessageBox("任务加载时发生错误:" + ex.Message);
            }
            XmlNodeList xnl = xDoc.SelectNodes(@"YQTasks/task");

            foreach (XmlNode xn in xnl)
            {
                if (xn.SelectSingleNode("taskId").InnerText != taskInfo.TaskID)
                    continue;
                pId = Convert.ToInt32(xn.SelectSingleNode("taskProcessId").InnerText);
                break;
            }
            xDoc.Save(xmlPath);
            return pId;
        }


        private void YQTaskResult_Shown(object sender, EventArgs e)
        {
            // DeleteRuleID();
            if (!Directory.Exists(taskInfo.ResultFilePath))
            {
                HelpUtil.ShowMessageBox(taskInfo.ResultFilePath + "结果文件路径不存在");
                return;
            }
            if (!File.Exists(statusFilePath))
            {
                HelpUtil.ShowMessageBox(statusFilePath + "任务信息文件不存在");
                return;
            }
            FillDGV();
        }
        private void FillDGV()
        {
            string ret = FileUtil.FileReadToEnd(statusFilePath);
            string[] retArray = ret.Split(Environment.NewLine);
            dataGridView.Rows.Clear();
            foreach (string line in retArray)
            {
                if (!line.Contains("\t"))
                    continue;
                string[] lineSplit = line.Split("\t");
                if (lineSplit.Length != 4)
                    continue;
                
                DataGridViewRow dr = new DataGridViewRow();

                dr.Cells.Add(new DataGridViewTextBoxCell { Value = lineSplit[0] });
                dr.Cells.Add(new DataGridViewTextBoxCell { Value = lineSplit[1] });
                dr.Cells.Add(new DataGridViewTextBoxCell { Value = lineSplit[2] });
                if (!File.Exists(lineSplit[3]))
                    dr.Cells.Add(new DataGridViewTextBoxCell { Value = string.Empty });
                else
                    dr.Cells.Add(new DataGridViewLinkCell { Value = "查看", Tag = lineSplit[3] });

                dataGridView.Rows.Add(dr);
            }
        }
         
        private void TaskResultButton_Click(object sender, EventArgs e)
        {
            ProcessUtil.TryOpenDirectory(this.taskInfo.ResultFilePath);
        }

        private void Update_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (pid == 0 || !ProcessExists(pid))
                    RunPython();
                    //UpdateResult();
            }
            catch { }
            FillDGV();
            this.Cursor = Cursors.Arrow;
        }

        private bool ProcessExists(int id)
        {
            return Process.GetProcesses().Any(x => x.Id == id);
        }

        private void RunPython()
        {
            string pyPath = Path.Combine(Global.TemplatesPath, "get_yq_result.py");
            string strInput = @"python " + pyPath + " --f " + this.statusFilePath;
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";      //设置要启动的应用程序
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;  // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;   //输出信息
            p.StartInfo.RedirectStandardError = true;   // 输出错误
            p.StartInfo.CreateNoWindow = true;     //不显示程序窗口 
            if (p.Start())
            {
                this.pid = p.Id;     //启动程序
                SavePIDToXml();
            }
            p.StandardInput.WriteLine(strInput + "&exit"); //向cmd窗口发送输入信息
            p.StandardInput.AutoFlush = true;
        }

        private void SavePIDToXml()
        {
            string xmlPath = Path.Combine(Global.UserWorkspacePath, "侦察兵", "YQTasks.xml");
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(xmlPath);
            }
            catch (Exception ex)
            {
                HelpUtil.ShowMessageBox("任务加载时发生错误:" + ex.Message);
            }
            XmlNodeList xnl = xDoc.SelectNodes(@"YQTasks/task");

            foreach (XmlNode xn in xnl)
            {
                if (xn.SelectSingleNode("taskId").InnerText != taskInfo.TaskID)
                    continue;
                xn.SelectSingleNode("taskProcessId").InnerText = this.pid.ToString();
                break;
            }
            xDoc.Save(xmlPath);
        }

        private void UpdateResult()
        {
            string ret = FileUtil.FileReadToEnd(statusFilePath);
            string[] retArray = ret.Split(Environment.NewLine);
            StringBuilder sb = new StringBuilder();
            foreach (string line in retArray)
            {
                if (!line.Contains("\t"))
                    continue;
                string[] lineSplit = line.Split("\t");
                if (lineSplit.Length != 4)
                    continue;
                lineSplit = UpdateInfo(lineSplit);
                sb.Append(string.Join("\t", lineSplit) + Environment.NewLine);
            }

            StreamWriter sw = new StreamWriter(statusFilePath);
            sw.Write(sb.ToString());
            sw.Flush();
            if (sw != null)
                sw.Close();
        }

        private string[] UpdateInfo(string[] infoArray)
        {
            List<string> resultList = QueryTaskResultsById(infoArray[1]);

            if(resultList.Count == 0)
                return infoArray;

            StreamWriter sw = null;
            sw = new StreamWriter(infoArray[3], true);
            sw.WriteLine(string.Join(Environment.NewLine, resultList.ToArray()));
            sw.Flush();
            if (sw != null)
                sw.Close();
            infoArray[2] = (Convert.ToInt64(infoArray[2]) + resultList.Count).ToString();
            return infoArray;
        }


        public List<string> QueryTaskResultsById(string ruleID)
        {
            List<string> resultList = new List<string>();
            string error = string.Empty;
            string result = string.Empty;
            JObject json = new JObject();
            string requestURL = Global.YQUrl + "get_message";
            Dictionary<string, string> pairs = new Dictionary<string, string> { { "rule_id", ruleID } };
            HttpHandler httpHandler = new HttpHandler();
            try
            {
                Response resp = httpHandler.Post(requestURL, pairs, string.Empty, 1000);
                if (resp.StatusCode != HttpStatusCode.OK)
                    error = string.Format("错误http状态：{0}。", resp.StatusCode.ToString());

                Dictionary<string, string> resDict = resp.ResDict;
                if (resDict.TryGetValue("status", out string value) && value != "success")
                    error = string.Format("错误http状态：{0}。", resDict["msg"]);
                else if (!resDict.ContainsKey("status"))
                    error = string.Format("接口访问失败，请查看网络状态。");
                else
                    result = resDict["data"];
                if (result.IsNullOrEmpty())
                {
                    return resultList;
                }
            }
            catch (Exception ex)
            {
                error = ruleID + "任务结果获取失败：" + ex.Message;
            }
            if (!error.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox(error);
                return resultList;
            }
            string[] array = result.Trim('\n').Split("\n");

            List<string> colList = new List<string>
            { "taskurl", "url", "title", "datasourcetype", "userid", "nickname", "postfloor", "revertcount",
              "heat", "mainareacode", "netimgurl", "imgurl", "posttime", "domain", "domainname", "forumname",
              "context", "isforward", "forwardnum", "likenum", "commentnum", "fannum", "followernum", "blognum",
              "readnum", "verify", "address", "addresscode", "imagepath", "domaintype", "classifyid", "classifyscore",
              "commentsign", "sensitivity", "areacode", "tagarea", "signcode", "eventcode", "tagevent"
            };

            Dictionary<string, string> sourceDict = new Dictionary<string, string>
            {
                {"1","新闻"}, {"2","报刊"}, {"4","新闻APP"}, {"8","Facebook"}, {"16","论坛"}, {"32","Twitter"},
                {"64","贴吧"}, {"256","博客"}, {"65536","视频"}, {"1048576","微博"}, {"268435456","微信"},
                {"16384","今日头条"}, {"32768","知乎"}, {"131072","短视频"}
            };

            Dictionary<string, string> classifyDict = new Dictionary<string, string> 
            {
                {"0X00000000","未知分类"},{"0X00000001","旅游"},{"0X00000002","游戏"},{"0X00000003","人物"},{"0X00000004","体育"},{"0X00000005","音乐"},
                {"0X00000006","影视"},{"0X00000007","软件"},{"0X00000008","文学"},{"0X00000009","美食"},{"0X0000000A","健康"},{"0X0000000B","医药"},
                {"0X0000000C","商铺"},{"0X0000000D","财经"},{"0X0000000E","汽车"},{"0X0000000F","房产"},{"0X00000010","动漫"},
                {"0X00000011","教育学校、学科、考试，培训等"},{"0X00000012","科技"},{"0X00000013","军事"},{"0X00000014","天气"},{"0X00000015","情感"},
                {"0X00000016","广告"},{"0X00000017","群体聚集"},{"0X00000018","自然灾害"},{"0X00000019","交通事故"},{"0X0000001A","暴力执法"},
                {"0X0000001B","刑事犯罪"},{"0X0000001C","求职招聘"},{"0X0000001D","食品安全"},{"0X0000001E","环境污染"},{"0X0000001F","疾病疫情"},
                {"0X00000020","金融安全"},{"0X00000021","敏感政治"},{"0X00000022","贪腐"},{"0X00000023","非法组织"},{"0X00000024","反动言论"},
                {"0X00000025","先进事迹"},{"0X00000026","领导人活动"},{"0X00000027","政策方针"},{"0X00000028","心灵鸡汤"},{"0X00000029","其他社会类"},
                {"0X0000002A","其它政治类"},{"0x0000002B","色情广告/推广"},{"0x0000002C","色情从业"},{"0x0000002D","色情文学"},{"0x0000002E","广告子类营销类"},
                {"0x0000002F","幽默搞笑"},{"0x00000030","敏感政治中六四"},{"0x00000031","敏感政治中领导人"},{"0x00000032","敏感政治中意识形态"},{"0x00000033","色情资源"},
                {"0x00000034","违法犯罪之网络黑产"},{"0x00000035","违法犯罪之毒品"},{"0x00000036","违法犯罪之赌博"},{"0x00000037","娱乐明星"},{"0x00000038","星座"},
                {"0x00000039","亲子"},{"0x0000003A","女性"},{"0x0000003B","招聘广告"},{"0x0000003C","宗教"},{"0x0000003D","文化"},{"0x0000003E","环球"},
                {"0x0000003F","宠物"},{"0x00000040","互联网"},{"0x00000041","数码"},{"0x00000042","手机"},{"0x00000043","软件"},{"0x00000044","网络安全"},
                {"0x00000045","科学"},{"0x00000046","智能化"},{"0x00000047","股票"},{"0x00000048","期货"},{"0x00000049","理财"},{"0x0000004A","互联网金融"},
                {"0x0000004B","外汇"},{"0x0000004C","投资"},{"0x0000004D","基金"},{"0x0000004E","宏观经济"},{"0x0000004F","债券"},{"0x00000050","银行"},
                {"0x00000051","保险"},{"0x00000052","电影"},{"0x00000053","电视剧"},{"0x00000054","综艺节目"},{"0x00000055","戏剧"},{"0x00000056","篮球"},
                {"0x00000057","足球"},{"0x00000058","乒乓球"},{"0x00000059","羽毛球"},{"0x0000005A","排球"},{"0x0000005B","游泳"},{"0x0000005C","健身"},
                {"0x0000005D","高尔夫"},{"0x0000005E","田径"},{"0x0000005F","历史"},{"0X0000FFFF","其他"}
            };

            //Dictionary<string, string> industryDict = new Dictionary<string, string>
            //{
            //    {"1", "行业"}, {"100000", "公安"}, {"100001", "官员形象"}, {"100002", "涉警言论"}, {"100003", "群体事件"}, 
            //    {"100004", "安全生产"}, {"100005", "食品安全"}, {"100006", "大要案件"}, {"100007", "信访维权"}, 
            //    {"100008", "城市交通"}, {"100009", "城市管理"}, {"100010", "两抢一盗"}, {"100011", "黄赌毒黑"}, 
            //    {"100012", "征地拆迁"}, {"100013", "领导相关1"}, {"100014", "涉警信息2"}, {"100016", "朝文负面"}, 
            //    {"100017", "铁路公安"}, {"100019", "涉恐涉爆"}, {"100020", "出租罢工"}, {"100023", "社保低保"}, 
            //    {"100025", "环境污染"}, {"100027", "金融理财"}, {"100028", "舆情新词"}, {"100029", "领导人负面"}, 
            //    {"100031", "交通管理"}, {"100032", "涉政言论"}, {"100033", "政治人物"}, {"100034", "政府组织"}, 
            //    {"100035", "领导相关"}, {"100036", "涉警信息"}, {"100038", "负面信息"}, {"200000", "军队"}, 
            //    {"200001", "军人形象"}, {"200002", "领导形象"}, {"200003", "各军区信息"}, {"200004", "领导信息"}, 
            //    {"300000", "企业"}, {"300001", "企业形象"}, {"400000", "房地产"}, {"400001", "形象"}, {"700000", "宗教"},
            //    {"700001", "宗教相关"}, {"800000", "邮政"}, {"800001", "邮政相关"}, {"900000", "物流"}, {"900001", "物流相关"},
            //    {"1000000", "财经"}, {"1000001", "金融相关"}, {"1100000", "政府"}, {"1100001", "领导人相关"}, 
            //    {"1200000", "纪委"}, {"1200001", "大连纪委"}, {"1300000", "网信办"}, {"1300001", "讽刺国家重大建设"},
            //    {"1300002", "违法售卖泛制身份证1"}, {"1300003", "违反贩卖身份证1"}, {"1300004", "违反贩卖身份证2"}, 
            //    {"1300005", "贩卖人体器官"}, {"1300006", "违法贩卖翻墙工具"}, {"1300007", "1号信息"}
            //};


            for (int i = 0; i < array.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                var gList = JObject.Parse(array[i])["articleinfo"];
                foreach(string col in colList)
                {
                    string key = gList[col].IsNull() ? string.Empty : gList[col].ToString().Replace(Environment.NewLine,string.Empty);
                    if (key.IsNullOrEmpty())
                    {
                        sb.Append(key + "\t");
                        continue;
                    }
                    if (col == "datasourcetype" && sourceDict.TryGetValue(key, out string type))
                        key = type;
                    else if (col == "classifyid" && classifyDict.TryGetValue(key, out string classify))
                        key = classify;

                    sb.Append(key + "\t");
                }
                resultList.Add(sb.ToString());
            }
            return resultList;
        }

        private void DeleteRuleID()
        {
            string ruleID = "1416305261"; //9616533756201
            string token = string.Empty;
            string getTokenURL = "https://api.fhyqw.com/auth/gettoken?username=iao2&password=60726279d628473f6f3f03d5b81b8c95&apikey=50c9429656499f3b26ca1bd6c8045239";
            try
            {
                JObject json = JObject.Parse(HttpReq(getTokenURL, "GET"));
                JToken gList = json["results"];
                foreach (JToken g in gList)
                    token = g["access_token"].ToString();
            }
            catch
            {
                HelpUtil.ShowMessageBox(string.Format("删除规则ID{0}时获取令牌失败", ruleID));
                return;
            }
            try
            {
                long type = 0; //记得改
                string deleteIDURL = string.Format("https://api.fhyqw.com/rule?token={0}&id={1}&type={2}", token, ruleID, type);
                JObject jsonInfo = JObject.Parse(HttpReq(deleteIDURL, "DELETE"));
                if (jsonInfo["status"].ToString() == "200")
                    return;
                else
                    HelpUtil.ShowMessageBox(string.Format("删除规则ID{0}时失败,{1}", ruleID, jsonInfo["msg"].ToString()));
            }
            catch { }
            return;
        }

        private string HttpReq(string url, string method)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = method;
            req.Timeout = 20000;
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            string postContent = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return postContent;
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex] is DataGridViewLinkColumn && e.RowIndex > -1 && dataGridView.CurrentCell is DataGridViewLinkCell)
            {
                DataGridViewLinkCell cell = (DataGridViewLinkCell)dataGridView.CurrentCell;
                if (cell.Tag == null || !File.Exists(cell.Tag.ToString()))
                    return;
                ProcessUtil.TryProcessOpen(cell.Tag.ToString());
            }
        }
    }
}
