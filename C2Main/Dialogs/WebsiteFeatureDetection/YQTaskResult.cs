using C2.Business.WebsiteFeatureDetection;
using C2.Utils;
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
