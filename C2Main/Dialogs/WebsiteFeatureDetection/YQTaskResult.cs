using C2.Business.WebsiteFeatureDetection;
using C2.Controls.C1.Left;
using C2.Core;
using C2.Utils;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    public partial class YQTaskResult : Form
    {
        public YQTaskInfo taskInfo;
        public string statusMsg;
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
            this.taskInfoLabel.Text = taskInfo.Status.ToString();
        }

        private void YQTaskResult_Shown(object sender, EventArgs e)
        {
            
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
    }
}
