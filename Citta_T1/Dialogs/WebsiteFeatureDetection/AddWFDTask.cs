using C2.Controls;
using C2.Utils;
using System;
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
            TaskName = String.Format("网络侦察兵{0}", DateTime.Now.ToString("yyyyMMdd"));
        }


        protected override bool OnOKButtonClick()
        {
            TaskName = TaskName.Trim();//去掉首尾空白符
            return IsValidityTaskName() && IsValidityFilePath() && base.OnOKButtonClick();
        }

        private bool IsValidityTaskName()
        {
            if(string.IsNullOrEmpty(TaskName))
            {
                HelpUtil.ShowMessageBox("任务名不能为空");
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
            this.FilePath = ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : String.Empty;
        }
    }
}
