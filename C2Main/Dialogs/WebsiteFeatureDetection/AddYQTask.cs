using C2.Business.HTTP;
using C2.Business.WebsiteFeatureDetection;
using C2.Controls;
using C2.Core;
using C2.Utils;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class AddYQTask : StandardDialog
    {
        public YQTaskInfo TaskInfo { set; get; }
        private string token;
        private string TaskID;
        private long ruleID;
        private long startTime;
        private long endTime;
        private string areaCode;
        private string ruleName;
        private string ruleHost;
        private long ruleDatasource;
        private string destDirectory;
        private string taskFilePath;
        private string destFilePath;
        private string statusFilePath;
        private string TaskCreateTime;
        private readonly Dictionary<string, string> table;
        string TaskName { get => this.taskNameTextBox.Text; set => this.taskNameTextBox.Text = value; }
        string TaskContent { get => this.taskContentComboBox.Text; set => this.taskContentComboBox.Text = value; }
        string TaskModelName { get => this.taskModelComboBox.Text; set => this.taskModelComboBox.Text = value; }
        string FilePath { get => this.filePathTextBox.Text; set => this.filePathTextBox.Text = value; }
        string provinceName { get => this.provinceCB.Text; set => this.provinceCB.Text = value; }
        string cityName { get => this.cityCB.Text; set => this.cityCB.Text = value; }
        int ruleType { get => this.taskContentComboBox.SelectedIndex; set => this.taskContentComboBox.SelectedIndex = value; }
        readonly int maxRow;
        public AddYQTask()
        {
            InitializeComponent();
            InitTaskInfo();

            token = string.Empty;
            maxRow = 1000;
            startTime = 0;
            endTime = 0;
            ruleDatasource = 0;

            destDirectory = Path.Combine(Global.UserWorkspacePath, "侦察兵", "舆情侦察兵");
            FileUtil.CreateDirectory(destDirectory);

            table = new Dictionary<string, string>(1024 * 4);
            InitCodeTable();

            this.OKButton.Size = new System.Drawing.Size(75, 27);
            this.CancelBtn.Size = new System.Drawing.Size(75, 27);
        }

        private void InitTaskInfo()
        {
            taskModelComboBox.SelectedIndex = 0;
            taskContentComboBox.SelectedIndex = 0;
            provinceCB.SelectedIndex = 0;
            cityCB.SelectedIndex = 0;

            TaskCreateTime = ConvertUtil.TransToUniversalTime(DateTime.Now);
            TaskName = String.Format("{0}任务{1}_{2}", TaskModelName, DateTime.Now.ToString("MMdd"), this.TaskCreateTime);

            Random ran = new Random();
            int n = ran.Next(10, 100);

            this.TaskID = string.Format("{0}{1}", n.ToString(), this.TaskCreateTime);
        }

        public void InitCodeTable()
        {
            string ret = FileUtil.FileReadToEnd(Path.Combine(Global.TemplatesPath, "ProvinceAndCityCode.txt"));
            foreach (string line in ret.Split(System.Environment.NewLine))
            {
                string[] lineSplit = line.Split(":");
                if (lineSplit.Length >= 2)
                    table[lineSplit[0].Trim()] = lineSplit[1].Trim();
            }
        }

        private void TaskModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TaskName = String.Format("{0}任务{1}_{2}", this.TaskModelName, DateTime.Now.ToString("MMdd"), this.TaskCreateTime);
        }

        private void ProvinceCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cityCB.Items.Clear();
            this.cityCB.Items.Add("不限");
            if (provinceCB.SelectedIndex == 0)
            {
                cityName = "不限";
                return;
            }
            string provinceheadCode = table[provinceName].Substring(0,2);
            foreach(var kv in table)
            {
                if (kv.Key == provinceName || kv.Value.Substring(0, 2) != provinceheadCode)
                    continue;
                this.cityCB.Items.Add(kv.Key);
            }
            this.cityCB.SelectedIndex = 0;
        }

        private void PasteModeCB_CheckedChanged(object sender, EventArgs e)
        {
            this.wsTextBox.Clear();
            this.filePathTextBox.Clear();

            this.browserButton.Enabled = !this.pasteModeCB.Checked;

            this.wsTextBox.Enabled = this.pasteModeCB.Checked;
            this.wsTextBox.ReadOnly = !this.pasteModeCB.Checked;

            if (this.pasteModeCB.Checked)
                FilePath = Path.Combine(Global.TempDirectory, TaskID + ".txt");
            else
                FilePath = String.Empty;
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog OpenFileDialog = new OpenFileDialog
            {
                Filter = "文件 | *.txt",
                FileName = FilePath
            };
            if (OpenFileDialog.ShowDialog() != DialogResult.OK)
                return;

            this.FilePath = OpenFileDialog.FileName;
        }

        protected override bool OnOKButtonClick()
        {
            if (this.pasteModeCB.Checked)
            {
                if (this.wsTextBox.Text.Trim().IsEmpty())
                {
                    HelpUtil.ShowMessageBox("请输入查询内容");
                    return false;
                }
                FileUtil.FileWriteToEnd(FilePath, this.wsTextBox.Text);
            }

            if (TaskContent == "账号" && TaskModelName == "不限")
            {
                HelpUtil.ShowMessageBox("查询内容为账号时，必须指定任务类型。");
                return false;
            }

            if (TaskModelName == "抖音APP" || TaskModelName == "快手")
            {
                HelpUtil.ShowMessageBox(" 抖音APP和快手相关查询施工中，敬请期待。");
                return false;
            }

            if (!GenAndCheckToken())
                return false;

            this.TaskInfo = UpdateYQTaskInfo();

            bool genTask = this.pasteModeCB.Checked ? GenTasksFromPaste() : GenTasksFromFile();
            if (!(genTask && base.OnOKButtonClick()))
                return false;

            new Log.Log().LogManualButton("舆情侦察兵", "运行");
            HelpUtil.ShowMessageBox("任务下发成功");
            return true;
        }

        private bool GenAndCheckToken()
        {
            string validate = string.Empty;
            string getTokenURL = "https://api.fhyqw.com/auth/gettoken?username=iao2&password=60726279d628473f6f3f03d5b81b8c95&apikey=50c9429656499f3b26ca1bd6c8045239";
            try
            {
                JObject json = JObject.Parse(HttpGet(getTokenURL));
                JToken gList = json["results"];
                foreach (JToken g in gList)
                    this.token = g["access_token"].ToString();
            }
            catch
            {
                HelpUtil.ShowMessageBox("获取任务下发令牌失败");
                return false;
            }
            if (this.token.IsNullOrEmpty())
                return false;

            string validTokenURL = string.Format("https://api.fhyqw.com/auth/validtoken?token={0}", this.token);
            try
            {
                JObject json = JObject.Parse(HttpGet(validTokenURL));
                var gList = json["results"];
                foreach (var g in gList)
                    validate = g["validate"].ToString();
                if (validate == "true")
                    return true;
            }
            catch
            {
                HelpUtil.ShowMessageBox("获取的令牌无效");
                return false;
            }
            return true;
        }
        
        public string HttpGet(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Timeout = 20000;
            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            string postContent = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return postContent;
        }


        private YQTaskInfo UpdateYQTaskInfo()
        {
            this.ruleName = this.TaskName;

            this.taskFilePath = Path.Combine(this.destDirectory, this.TaskName);
            FileUtil.CreateDirectory(this.taskFilePath);

            this.statusFilePath = Path.Combine(this.taskFilePath, this.TaskID + "_info.txt");
            //using (File.Create(this.statusFilePath)) { } ;
            

            areaCode = GenCode();

            this.startTime = Convert.ToInt64(ConvertUtil.TransToUniversalTime(dateTimePicker1.Value));
            this.endTime = Convert.ToInt64(ConvertUtil.TransToUniversalTime(dateTimePicker2.Value));
            List<string> result = GenModelHostAndData(this.TaskModelName);
            this.ruleHost = result[0];
            if(!result[1].IsNullOrEmpty())
                this.ruleDatasource = Convert.ToInt64(result[1]);

            return new YQTaskInfo(TaskName, TaskID, TaskModelName, FilePath, taskFilePath, YQTaskStatus.Running, TaskCreateTime);
        }

        private string GenCode()
        {
            string code = string.Empty;

            if (provinceName == "不限")
                return code;
            try
            {
                if (cityName == "不限")
                    code = table[provinceName];
                else
                    code = table[cityName];
            }
            catch { }
            return code;
        }

        private List<string> GenModelHostAndData(string model)
        {
            //抖音： iesdouyin.com
            //今日头条： toutiao.com
            //微信公众号： mp.weixin.qq.com
            //新浪微博： weibo.com
            //Twitter： twitter.com
            //百家号： baijiahao.baidu.com

            //Twitter
            //微博
            //微信公众号
            //今日头条
            //抖音
            //抖音APP
            //快手
            //暗网
            //不限
            string modelHost = string.Empty;
            string dataType = string.Empty;
            

            switch (model)
            {
                case "微博":
                    modelHost = "weibo.com";
                    dataType = "1048576";
                    break;
                case "Twitter":
                    modelHost = "twitter.com";
                    dataType = "32";
                    break;
                case "微信公众号":
                    modelHost = "mp.weixin.qq.com";
                    dataType = "268435456";
                    break;
                case "今日头条":
                    modelHost = "toutiao.com"; 
                    dataType = "16384";
                    break;
                case "抖音":
                    modelHost = "iesdouyin.com";
                    dataType = "131072";
                    break;
            }

            List<string> result = new List<string> { modelHost, dataType };
            return result;
        }

        private bool GenTasksFromPaste()
        {
            if (this.wsTextBox.Text.Trim().IsEmpty())         
                return false;
                
            // 如果粘贴文件不合格,就别清空旧数据了

            string[] lines = this.wsTextBox.Text.SplitLine();
            for (int i = 0; i < Math.Min(lines.Length, maxRow); i++)
                AddTasksByKey(lines[i], i);

            return true;
        }

        private bool GenTasksFromFile()
        {

            if (!File.Exists(FilePath))
            {
                HelpUtil.ShowMessageBox("该数据文件不存在");
                return false;
            }
            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                    for (int row = 0; row < maxRow && !sr.EndOfStream; row++)
                        AddTasksByKey(sr.ReadLine().Trim(), row);
            }
            catch
            {
                HelpUtil.ShowMessageBox(FilePath + ",文件加载出错，请检查文件内容。");
                return false;
            }
            return true;
        }


        private void AddTasksByKey(string keyWord, int number)
        {
            this.ruleID = Convert.ToInt64(this.TaskID + number.ToString());
            int resultNumber = 0;
            //this.ruleID = 1416305260;

            destFilePath = Path.Combine(this.taskFilePath, string.Format("{0}_{1}.txt", this.ruleID.ToString(), keyWord));
            using (File.Create(destFilePath)) { }

            Dictionary<string, object> pairs = new Dictionary<string, object> { };
            pairs.Add("id", this.ruleID);
            pairs.Add("name", this.ruleName);
            pairs.Add("datasource", this.ruleDatasource);
            if (this.startTime != 0)
                pairs.Add("starttime", this.startTime);
            if (this.endTime != 0)
                pairs.Add("endtime", this.endTime);
            if (!this.areaCode.IsNullOrEmpty())
                pairs.Add("areakeyword", this.areaCode);
            if(this.TaskContent == "关键词")
                pairs.Add("keyword", keyWord);
            else if(this.TaskContent == "账号")
            {
                if (this.TaskModelName == "Twitter")
                {
                    keyWord = keyWord.Replace("@", string.Empty).Replace(" ",string.Empty);
                    pairs.Add("url", "https://twitter.com/" + keyWord);
                    string accountUrl = string.Format("https://api.fhyqw.com/twitter/account?token={0}&screenname={1}",this.token,keyWord);
                    if(WriteTwitterAccount(accountUrl,keyWord))
                        resultNumber = 1;
                }
                else
                {
                    if (!this.ruleHost.IsNullOrEmpty())
                        pairs.Add("host", this.ruleHost);
                    pairs.Add("userid", keyWord);
                }
            }
            pairs.Add("type", this.ruleType);

            string error = string.Empty;
            string requestURL = string.Format("https://api.fhyqw.com/rule?token={0}", this.token);
            HttpHandler httpHandler = new HttpHandler();
            try
            {
                Response resp = httpHandler.ObjDicPost(requestURL, pairs);
                if (resp.StatusCode != HttpStatusCode.OK)
                    error = string.Format("错误http状态：{0}。", resp.StatusCode.ToString());

                Dictionary<string, string> resDict = resp.ResDict;
                if (resDict["status"] != "200")
                    error = string.Format("错误http状态：{0}。", resDict["msg"]);

            }
            catch (Exception ex)
            {
                error = keyWord + "任务下发失败：" + ex.Message;
            }
            if (!error.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox(error);
                return;
            }

            List<string> rowHeaderList = new List<string>
            {
                "采集任务url", "文章url", "文章标题", "数据源标识", "用户userid", "发表人昵称", "发表楼层",
                "回复数", "点赞数、热度值", "主线地区", "图片实际网页地址", "图片短串", "发表时间", "网站域名",
                "网站名称", "板块名称", "文章正文", "是否转发", "转发数", "点赞数", "评论数", "粉丝数",
                "关注者数", "发表文章数", "阅读数", "是否为官方认证", "注册地址", "注册地地区编号",
                "头像链接", "境内外标识", "文章所属分类", "文章所属分类得分", "正负面标识", "文章敏感度",
                "是否为噪音标识", "文章命中地区编码", "文章命中地区", "行业情感正负面", "行业id", "行业说明"
            };

            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(destFilePath, true, Encoding.UTF8);
                sw.WriteLine(string.Join("\t", rowHeaderList.ToArray()));
                sw.Flush();
            }
            catch (Exception ex)
            {
                HelpUtil.ShowMessageBox(ex.Message);
            }
            if (sw != null)
                sw.Close();

            WriteTaskInfo(keyWord, resultNumber.ToString());
        }

        private bool WriteTwitterAccount(string accountUrl,string keyWord)
        {
            StringBuilder sb = new StringBuilder();
            string error = string.Empty;
            List<string> colList = new List<string>()
            {
                "id", "screenName", "name", "isProtected", "isVerified", "description", "createdAt",
                "profileImageURL", "profileImageLocalURI", "location", "statusesCount", "friendsCount",
                "followersCount", "favouritesCount", "lastSpideTime", "lastStatTime"
            };
            try
            {
                JObject json = JObject.Parse(HttpGet(accountUrl));
                string status = json["status"].ToString();
                if (status != "200")
                    error = keyWord + "获取Twitter账号信息失败：" + json["msg"].ToString();
                else
                {
                    var gList = json["results"]["data"];
                    foreach (string col in colList)
                    {
                        string value = gList[col].IsNull() ? string.Empty : gList[col].ToString();
                        sb.Append(value + "\t");
                    }
                }
            }
            catch (Exception ex)
            {
                error = keyWord + "获取Twitter账号信息失败：" + ex.Message;
            }
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(this.destFilePath, true, Encoding.UTF8);
                if (!error.IsNullOrEmpty())
                {
                    sw.WriteLine(error);
                    sw.Write(Environment.NewLine);
                    sw.Close();
                    return false;
                }
                sw.WriteLine(string.Join("\t", colList.ToArray()));
                sw.WriteLine(sb.ToString());
                sw.Write(Environment.NewLine);
                sw.Flush();
            }
            catch (Exception ex)
            {
                HelpUtil.ShowMessageBox(ex.Message);
            }
            if (sw != null)
                sw.Close();
            return true;
        }

        private void WriteTaskInfo(string keyWord, string num)
        {
            try
            {
                FileStream file = new FileStream(this.statusFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                using (StreamWriter sw = new StreamWriter(file,Encoding.UTF8))
                {
                    sw.WriteLine(keyWord + "\t" + this.ruleID.ToString() + "\t" + num + "\t" + destFilePath);
                    sw.Flush();
                    sw.Close();
                    file.Close();
                }
            }
            catch (Exception ex)
            { 
                HelpUtil.ShowMessageBox(ex.Message); 
            }
            return;
        }
    }
}
