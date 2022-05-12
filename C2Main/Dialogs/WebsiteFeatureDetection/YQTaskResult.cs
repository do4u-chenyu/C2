using C2.Business.WebsiteFeatureDetection;
using C2.Core;
using C2.Utils;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    public partial class YQTaskResult : Form
    {
        public YQTaskInfo taskInfo;
        public YQTaskResult()
        {
            InitializeComponent();
        }

        public YQTaskResult(YQTaskInfo task) : this()
        {
            taskInfo = task;
            this.taskNameLabel.Text = taskInfo.TaskName;
            this.taskIDLabel.Text = taskInfo.TaskID;
            this.taskModelLabel.Text = taskInfo.TaskModel;
            this.taskResultTextBox.Text = taskInfo.ResultFilePath;
            this.taskInfoLabel.Text = taskInfo.Status.ToString();
        }

        private void YQTaskResult_Shown(object sender, EventArgs e)
        {
            taskInfo.Status = YQTaskStatus.Running;
            GenTaskResultFromFile();

            if (taskInfo.Status == YQTaskStatus.Done && File.Exists(taskInfo.ResultFilePath))
                return;

            if (taskInfo.IsOverTime())
            {
                HelpUtil.ShowMessageBox("任务已过期，请在下发24小时内获取结果。");
                return;
            }
        }

        private void TaskResultButton_Click(object sender, EventArgs e)
        {
            ProcessUtil.TryOpenDirectory(this.taskInfo.ResultFilePath);
        }

        private void GenTaskResultFromFile()
        {
            string FilePath = Path.Combine(Global.TempDirectory, taskInfo.TaskID + ".txt");
            if (!File.Exists(FilePath))
            {
                HelpUtil.ShowMessageBox(FilePath + "文件不存在");
                taskInfo.Status = YQTaskStatus.Failed;
                return;
            }
            try
            {
                string ret = FileUtil.FileReadToEnd(FilePath);
                string[] retArray = ret.Split(System.Environment.NewLine);
                for (int row = 0; row < retArray.Length; row++)
                {
                    string ruleID = taskInfo.TaskID + row.ToString();
                    string destFilePath = Path.Combine(taskInfo.ResultFilePath, taskInfo.TaskID + "_" + retArray[row].Trim()+ ".txt");
                    GenYQTaskResult(destFilePath, ruleID);
                }
                taskInfo.Status = YQTaskStatus.Done;
            }
            catch(Exception ex)
            {
                HelpUtil.ShowMessageBox(FilePath + ",文件加载出错，请检查文件内容。" + ex.Message);
                taskInfo.Status = YQTaskStatus.Failed;
            }
            return;
        }

        private void GenYQTaskResult(string destFilePath, string ruleID)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "61.177.139.251";
            factory.Port = 25672;
            factory.VirtualHost = "iao2";
            factory.UserName = "iao2";
            factory.Password = "Iao123456";

            string testID = string.Empty;
            StreamWriter sw = new StreamWriter(destFilePath);
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
                        if (testID == ruleID)
                        {
                            sw.Write(gList.ToString());
                        };
                    };
                }
            }
            return;
        }
    }
}
