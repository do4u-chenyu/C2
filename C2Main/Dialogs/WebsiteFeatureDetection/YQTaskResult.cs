using C2.Business.WebsiteFeatureDetection;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }
}
