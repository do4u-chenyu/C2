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
using System.Windows.Forms;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class AddYQTask : StandardDialog
    {
        public YQTaskInfo TaskInfo { set; get; }
        private String token;
        private String TaskID;
        private long ruleID;
        private int areaCode;
        private String ruleName;
        private string destDirectory ;
        private string taskFilePath;
        string TaskName { get => this.taskNameTextBox.Text; set => this.taskNameTextBox.Text = value; }
        string TaskContent { get => this.taskContentComboBox.Text; set => this.taskContentComboBox.Text = value; }
        string TaskModelName { get => this.taskModelComboBox.Text; set => this.taskModelComboBox.Text = value; }
        string FilePath { get => this.filePathTextBox.Text; set => this.filePathTextBox.Text = value; }
        int ruleType { get => this.taskContentComboBox.SelectedIndex; set => this.taskContentComboBox.SelectedIndex = value; }
        int modelType { get => this.taskModelComboBox.SelectedIndex; set => this.taskModelComboBox.SelectedIndex = value; }
        readonly int maxRow;
        public AddYQTask()
        {
            InitializeComponent();
            maxRow = 100;
            InitTaskInfo();
            token = string.Empty;

            destDirectory = Path.Combine(Global.UserWorkspacePath, "侦察兵", "舆情侦察兵");
            FileUtil.CreateDirectory(destDirectory);

            taskFilePath = Path.Combine(this.destDirectory, this.TaskName);
            FileUtil.CreateDirectory(taskFilePath);

            this.OKButton.Size = new System.Drawing.Size(75, 27);
            this.CancelBtn.Size = new System.Drawing.Size(75, 27);
        }

        private void InitTaskInfo()
        {
            taskModelComboBox.SelectedIndex = 0;
            TaskName = String.Format("{0}任务{1}", TaskModelName, DateTime.Now.ToString("MMdd"));
        }

        private void TaskModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TaskName = String.Format("{0}任务{1}", this.TaskModelName, DateTime.Now.ToString("MMdd"));
        }
        private void PasteModeCB_CheckedChanged(object sender, EventArgs e)
        {
            this.wsTextBox.Clear();
            this.filePathTextBox.Clear();

            this.browserButton.Enabled = !this.pasteModeCB.Checked;

            this.wsTextBox.Enabled = this.pasteModeCB.Checked;
            this.wsTextBox.ReadOnly = !this.pasteModeCB.Checked;
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
            
            bool token = GenAndCheckToken();
            TaskInfo = UpdateYQTaskInfo();
            bool genTask = this.pasteModeCB.Checked ? GenTasksFromPaste() : GenTasksFromFile();
            if (!(token && genTask && base.OnOKButtonClick()))
                return false;

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
        
        private string HttpGet(string url)
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
            this.ruleName = TaskName;
            try
            {
                areaCode = int.Parse(areaTextBox.Text);
            }
            catch
            {
                areaCode = 110000;
            }
            DateTime dtStart = new DateTime(1970, 1, 1, 8, 0, 0);
            long time = Convert.ToInt32(DateTime.Now.Subtract(dtStart).TotalSeconds);

            Random ran = new Random();
            int n = ran.Next(10, 100);
            this.TaskID = string.Format("{0}{1}", n.ToString(), time.ToString());

            return new YQTaskInfo(TaskName, TaskID, TaskModelName, FilePath, taskFilePath, YQTaskStatus.Running);
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
            //this.ruleID = 1416305261;
            
            string destFilePath = Path.Combine(this.taskFilePath, string.Format("{0}_{1}.txt", this.TaskID, keyWord));
            using (File.Create(destFilePath)) { }
            long ss = Convert.ToInt64(this.TaskID + number.ToString());
            Dictionary<string, object> pairs = new Dictionary<string, object> { };


            pairs.Add("id", this.ruleID);
            pairs.Add("type", this.ruleType);
            pairs.Add("keyword", keyWord);
            pairs.Add("name", this.ruleName);

            string error = string.Empty;
            string requestURL = string.Format("https://api.fhyqw.com/rule?token={0}",this.token);
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
                error =  "下发任务失败：" + ex.Message;
            }
            if (!error.IsNullOrEmpty())
                HelpUtil.ShowMessageBox(error);
            
            GenYQTaskResult(destFilePath);
            return;
        }

        private void GenYQTaskResult(string destFilePath)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "61.177.139.251";
            factory.Port = 25672;
            factory.VirtualHost = "iao2";
            factory.UserName = "iao2";
            factory.Password = "Iao123456";

            string testID = string.Empty;
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("iao2", true, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);//消费者
                    channel.BasicConsume("iao2", true, consumer);//消费消息
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        var gList = JObject.Parse(message)["articleinfo"];
                        var ruleInfo = gList["rules"];
                        foreach (var info in ruleInfo)
                        {
                            testID = info["ruleid"].ToString();
                        }
                        if (testID == this.ruleID.ToString())
                        {
                            StreamWriter sw = null;
                            sw = new StreamWriter(destFilePath);
                            sw.WriteLine(gList.ToString());
                        };
                    };
                }
            }
            return;
        }
    }
}
