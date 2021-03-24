using C2.Controls;
using C2.Core;
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
    partial class AddWFDTask : StandardDialog
    {
        public string TaskName { get => this.taskNameTextBox.Text; set => this.taskNameTextBox.Text = value; }
        public string FilePath { get => this.filePathTextBox.Text; set => this.filePathTextBox.Text = value; }
        public AddWFDTask()
        {
            InitializeComponent();
            InitTaskName();
        }

        private void InitTaskName()
        {
            int idx = 1;
            string tmpTaskName = String.Format("网络侦察兵{0}", idx);
            List<string> existTaskNames = Global.GetWebsiteFeatureDetectionControl().GetTaskNames();

            while (existTaskNames.Contains(tmpTaskName))
                tmpTaskName = String.Format("网络侦察兵{0}", ++idx);

            TaskName = tmpTaskName;
        }


        protected override bool OnOKButtonClick()
        {
            TaskName = TaskName.Trim();//去掉首尾空白符

            if (!IsValidityTaskName() || !IsValidityFilePath())
                return false;

            return base.OnOKButtonClick();
        }

        private bool IsValidityTaskName()
        {
            if(string.IsNullOrEmpty(TaskName))
            {
                HelpUtil.ShowMessageBox("任务名不能为空。");
                return false;
            }

            if(Global.GetWebsiteFeatureDetectionControl().GetTaskNames().Contains(TaskName))
            {
                HelpUtil.ShowMessageBox(TaskName + " 该任务名已存在，请重新命名。");
                return false;
            }

            if(FileUtil.IsContainIllegalCharacters(TaskName, "任务名") || FileUtil.NameTooLong(TaskName, "任务名"))
            {
                return false;
            }

            return true;
        }

        private bool IsValidityFilePath()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                HelpUtil.ShowMessageBox("查询文件路径不能为空，请点击预览选择文件。");
                return false;
            }

            return true;
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog
            {
                Filter = "files|*.txt;*.bcp;*.csv"
            };

            if (fd.ShowDialog() == DialogResult.OK)
            {
                this.FilePath = fd.FileName;
            }
        }
    }
}
