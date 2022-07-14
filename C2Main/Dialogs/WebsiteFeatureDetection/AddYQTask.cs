using C2.Business.HTTP;
using C2.Business.WebsiteFeatureDetection;
using C2.Controls;
using C2.Core;
using C2.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private int expirestime;
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
        private int pid;
        private readonly Dictionary<string, string> table;
        private static readonly LogUtil log = LogUtil.GetInstance("YQTask");
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
            pid = 0;

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
            if (this.TaskModelName == "Twitter" && this.TaskContent == "账号")
                this.exampleLabel.Text = "Twitter账号输入样例：@DreawmParts";
            else
                this.exampleLabel.Text = string.Empty;
        }

        private void TaskContentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TaskModelName == "Twitter" && this.TaskContent == "账号")
                this.exampleLabel.Text = "Twitter账号输入样例：@DreawmParts";
            else
                this.exampleLabel.Text = string.Empty;
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
            this.Cursor = Cursors.WaitCursor;
            if (this.pasteModeCB.Checked)
            {
                if (this.wsTextBox.Text.Trim().IsEmpty())
                {
                    HelpUtil.ShowMessageBox("请输入查询内容");
                    this.Cursor = Cursors.Arrow;
                    return false;
                }
                FileUtil.FileWriteToEnd(FilePath, this.wsTextBox.Text);

            }

            if (!this.pasteModeCB.Checked)
            {
                if (!File.Exists(FilePath))
                {
                    HelpUtil.ShowMessageBox("该数据文件不存在");
                    this.Cursor = Cursors.Arrow;
                    return false;
                }
            }

            if (TaskContent == "账号" && TaskModelName == "不限")
            {
                HelpUtil.ShowMessageBox("查询内容为账号时，必须指定任务类型。");
                this.Cursor = Cursors.Arrow;
                return false;
            }

            if (TaskContent == "账号" && TaskModelName == "暗网")
            {
                HelpUtil.ShowMessageBox("暗网不支持账号查询任务，只支持关键词类型。");
                this.Cursor = Cursors.Arrow;
                return false;
            }

            if (TaskModelName == "抖音APP" || TaskModelName == "快手")
            {
                HelpUtil.ShowMessageBox(" 抖音APP和快手相关查询施工中，敬请期待。");
                this.Cursor = Cursors.Arrow;
                return false;
            }
            
            if (!GenAndCheckToken())
            {
                this.Cursor = Cursors.Arrow;
                return false;
            }

            this.TaskInfo = UpdateYQTaskInfo();

            if(this.startTime == this.endTime)
            {
                HelpUtil.ShowMessageBox("查询起止时间不能相同，请修改。");
                this.Cursor = Cursors.Arrow;
                return false;
            }
            
            List<string> resultList = new List<string>();
            if (this.pasteModeCB.Checked)
                resultList = GenTasksFromPaste();
            else
                resultList = GenTasksFromFile();

            if (!base.OnOKButtonClick())
            {
                this.Cursor = Cursors.Arrow;
                return false;
            }
            WriteTaskInfo(resultList);
            UpLoadUserInfo();
            Thread.Sleep(1000);
            RunPython();
            HelpUtil.ShowMessageBox("任务创建成功");
            new Log.Log().LogManualButton("舆情侦察兵", "运行");
            this.Cursor = Cursors.Arrow;
            return true;
        }

        private void UpLoadUserInfo()
        {
            string userName = new Log.Log().UserNameGet();
            if (userName.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("上传任务信息时获取用户名失败。");
                return;
            }
            string taskCreatTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            Dictionary<string, object> pairs = new Dictionary<string, object> { };
            pairs.Add("taskid", Convert.ToInt64(this.TaskID));
            pairs.Add("username", userName);
            pairs.Add("submittime", taskCreatTime);


            string error = string.Empty;
            string upLoadInfoURL = "http://113.31.114.239:53373/api/yq/upload_user_info";
            //string upLoadInfoURL = "http://47.94.39.209:53373/api/yq/upload_user_info";
            HttpHandler httpHandler = new HttpHandler();
            try
            {
                Response resp = httpHandler.ObjDicPost(upLoadInfoURL, pairs);
                if (resp.StatusCode != HttpStatusCode.OK)
                    error = string.Format("错误http状态：{0}。", resp.StatusCode.ToString());


                Dictionary<string, string> resDict = resp.ResDict;
                if (resDict["status"] != "success")
                    error = string.Format("错误http状态：{0}。", resDict["msg"]);
            }
            catch (Exception ex)
            {
                error = "上传任务信息失败：" + ex.Message;
            }
            if (!error.IsNullOrEmpty())
                HelpUtil.ShowMessageBox(error);
            return;
        }

        private void RunPython()
        {
            string pythonExePath = GetPythonExePaths();
            if (pythonExePath.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("未找到合适的python解释器运行后台脚本");
                return;
            }
            string strInput = @"cd " + Global.TemplatesPath + "&\"" + pythonExePath + "\"" + @" get_yq_result.py --f " + this.statusFilePath;
            log.Info("新建任务执行后台脚本：" + strInput);
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";      //设置要启动的应用程序
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;  // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;   //输出信息
            p.StartInfo.RedirectStandardError = true;   // 输出错误
            p.StartInfo.CreateNoWindow = true;     //不显示程序窗口 
            if(p.Start())
                this.TaskInfo.PId = p.Id;     //启动程序     
            p.StandardInput.WriteLine(strInput + "&exit"); //向cmd窗口发送输入信息
            p.StandardInput.AutoFlush = true;
            
        }

        public string GetPythonExePaths()
        {
            string path = string.Empty;
            if (OpUtil.GetPythonConfigPaths().Count == 0)
            {
                HelpUtil.ShowMessageBox(HelpUtil.InvalidPythonENV2);
                new ConfigForm().ShowDialog();
            }
            string value = ConfigUtil.TryGetAppSettingsByKey("python");

            foreach (string pItem in value.Split(';'))
            {
                string[] oneConfig = pItem.Split('|');
                if (oneConfig.Length != 3)
                    continue;
                if (!oneConfig[0].EndsWith("python.exe") || oneConfig[2] != "True")
                    continue;
                path = oneConfig[0].Trim();
                break;
            }
            return path;
        }

        private bool GenAndCheckToken()
        {
            string getTokenURL = "http://113.31.114.239:53373/api/yq/get_token";
            try
            {
                JObject json = JObject.Parse(HttpGet(getTokenURL));
                JToken token_info = json["data"];
                this.token = token_info["access_token"].ToString();
                this.expirestime = (int)token_info["expirestime"];
            }
            catch
            {
                HelpUtil.ShowMessageBox("获取任务下发令牌失败，请检查网络环境。");
                return false;
            }
            if (this.token.IsNullOrEmpty())
                return false;
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

            areaCode = GenCode();

            this.startTime = Convert.ToInt64(ConvertUtil.TransToUniversalTime(dateTimePicker1.Value));
            this.endTime = Convert.ToInt64(ConvertUtil.TransToUniversalTime(dateTimePicker2.Value));
            List<string> result = GenModelHostAndData(this.TaskModelName);
            this.ruleHost = result[0];
            if(!result[1].IsNullOrEmpty())
                this.ruleDatasource = Convert.ToInt64(result[1]);

            return new YQTaskInfo(TaskName, TaskID, TaskModelName, FilePath, taskFilePath, pid, TaskCreateTime);
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
                case "知乎":
                    modelHost = "zhihu.com";
                    dataType = "32768";
                    break;
                case "贴吧":
                    modelHost = "tieba.baidu.com";
                    dataType = "64";
                    break;
            }

            List<string> result = new List<string> { modelHost, dataType };
            return result;
        }

        private List<string> GenTasksFromPaste()
        {
            List<string> resultList = new List<string>();
            string result = string.Empty;
            string[] lines = this.wsTextBox.Text.SplitLine();
            for (int i = 0; i < Math.Min(lines.Length, maxRow); i++)
            {
                int count = 0;
                while (count < 3)
                {
                    result = AddTasksByKey(lines[i], i);
                    if (!result.IsNullOrEmpty() && result != "错误http状态：TOKEN无效。")
                    {
                        resultList.Add(result);
                        break;
                    }
                    else if (result == "错误http状态：TOKEN无效。")
                    {
                        count++;
                        GenAndCheckToken();
                        continue;
                    }
                }
            }
                

            return resultList;
        }

        private List<string> GenTasksFromFile()
        {
            List<string> resultList = new List<string>();
            string result = string.Empty;
            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs, Encoding.Default))
                {
                    for (int row = 0; row < maxRow && !sr.EndOfStream; row++)
                    {
                        int count = 0;
                        while (count<3)
                        {
                            result = AddTasksByKey(sr.ReadLine().Trim(), row);
                            if (!result.IsNullOrEmpty() && result != "错误http状态：TOKEN无效。")
                            {
                                resultList.Add(result);
                                break;
                            }
                            else if (result == "错误http状态：TOKEN无效。")
                            {
                                count++;
                                GenAndCheckToken();
                                continue;
                            }
                        }

                    }
                }
            }
            catch
            {
                HelpUtil.ShowMessageBox(FilePath + ",文件加载出错，请检查文件内容。");
            }
            return resultList;
        }


        private string AddTasksByKey(string keyWord, int number)
        {
            // 时间检查
            long timeStamp = Convert.ToInt64(ConvertUtil.TransToUniversalTime(DateTime.Now));

            //TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            //int timeStamp = (int)Convert.ToInt64(ts.TotalMilliseconds);
            if (timeStamp >= this.expirestime)  // 如果当前时间大于token到期时间，重新获取token
            {
                GenAndCheckToken();
            }

            string result = string.Empty;
            this.ruleID = Convert.ToInt64(this.TaskID + number.ToString());

            destFilePath = Path.Combine(this.taskFilePath, 
                string.Format("{0}_{1}.txt", 
                this.ruleID.ToString(), 
                keyWord.Replace("\\", "").Replace("/", "").Replace(":", "").Replace("*", "").
                Replace("?", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("|", "")));  // 处理无法当做文件名的特殊字符
            long invalidTime = Convert.ToInt64(this.TaskCreateTime) + 24 * 3600;
            Dictionary<string, object> pairs = new Dictionary<string, object> { };
            pairs.Add("id", this.ruleID);
            
            pairs.Add("datasource", this.ruleDatasource);
            if (this.startTime != 0)
                pairs.Add("starttime", this.startTime);
            if (this.endTime != 0)
                pairs.Add("endtime", invalidTime);
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
                }
                else
                {
                    if (!this.ruleHost.IsNullOrEmpty())
                        pairs.Add("host", this.ruleHost);
                    pairs.Add("nickname", keyWord);
                }
            }
            int domainType = -1;
            pairs.Add("domaintype", domainType);
            pairs.Add("type", this.ruleType);
            pairs.Add("name", this.ruleName);
            if (this.TaskModelName == "暗网")
            {
                int netType = 1;
                pairs.Add("nettype", netType);
            } 

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
                if(error == "错误http状态：TOKEN无效。")
                {
                    return error;
                }
                else
                {
                    HelpUtil.ShowMessageBox(error);
                    return result;
                }

            }

            result = string.Format("{0}\t{1}\t{2}\t{3}", keyWord, this.ruleID.ToString(), "0", destFilePath);
            return result;
        }

        private void WriteTaskInfo(List<string> returnList)
        {
            try
            {
                FileStream file = new FileStream(this.statusFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                using (StreamWriter sw = new StreamWriter(file, Encoding.UTF8))
                {
                    foreach (string line in returnList)
                    {
                        sw.WriteLine(line+ "\t" + this.startTime + "\t" + this.endTime);
                        sw.Flush();
                    }
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

        private List<string> WriteTwitterAccount(string accountUrl,string keyWord)
        {
            List<string> returnList = new List<string>() { "true", string.Empty};
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
                        if (col == "id")
                            returnList[1] = value;
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
                    returnList[0] = "fasle";
                    return returnList;
                }
                sw.WriteLine(string.Join("\t", colList.ToArray()).Replace("id","UserID").Replace("screenName", "UserName").Replace("name", "NickName"));
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
            return returnList;
        }
    }
}
