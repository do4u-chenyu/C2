using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using C2.Utils;

namespace C2.SearchToolkit
{
    public partial class SearchToolkitForm : Form
    {
        private Control[] inputControls;
        private Dictionary<String, String> taskDict;
        
        public SearchToolkitForm()
        {
            InitializeComponent();
            InitializeInputControls();
        }

        private void InitializeInputControls()
        {
            inputControls = new Control[] { 
                this.usernameTB, 
                this.passwordTB,
                this.bastionIPTB,
                this.searchAgentIPTB,
                this.remoteWorkspaceTB,
                this.taskModelComboBox,
                this.taskNameTB
            };

            taskDict = new Dictionary<string, string>
            {
                ["全文涉赌模型"] = "gamble",
                ["全文涉枪模型"] = "gun",
                ["全文涉黄模型"] = "yellow",
                ["全文飞机场模型"] = "plane"
            };

            this.taskModelComboBox.SelectedIndex = 0; // 默认选择 涉赌任务
        }
        private void LoadTaskInfo(TaskInfo taskInfo)
        {
            this.usernameTB.Text = taskInfo.Username;
            this.passwordTB.Text = taskInfo.Password;
            this.taskNameTB.Text = taskInfo.TaskName;
            this.bastionIPTB.Text = taskInfo.BastionIP;
            this.taskModelComboBox.Text = taskInfo.TaskModel;
            this.searchAgentIPTB.Text = taskInfo.SearchAgentIP;
            this.remoteWorkspaceTB.Text = taskInfo.RemoteWorkspace;

            this.taskInfoGB.Text = "任务ID:" + taskInfo.TaskID; 
            this.taskStatusLabel.Text = taskInfo.TaskStatus;
        }

        private TaskInfo GenTaskInfo()
        {
           String value = String.Join("\t", new string[] {
                                            this.taskNameTB.Text,  // 刚开始创建时，没有ID
                                            this.taskNameTB.Text,
                                            DateTime.Now.ToShortDateString(),
                                            this.taskModelComboBox.Text,
                                            "NULL",
                                            this.usernameTB.Text,
                                            this.passwordTB.Text,
                                            this.bastionIPTB.Text,
                                            this.searchAgentIPTB.Text,
                                            this.remoteWorkspaceTB.Text
            });

            return TaskInfo.GenTaskInfo(value);
        }

        private void ReadOnlyInputControls()
        {
            foreach (Control ct in inputControls)
                ct.Enabled = false;
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            //TODO 
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputControls())
                this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LinuxWorkspaceTB_Enter(object sender, EventArgs e)
        {
            remoteWorkspaceTB.ForeColor = Color.Black;
        }

        private void ModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.taskNameTB.Text = GenTaskName();
            this.remoteWorkspaceTB.Text = GenWorkspace();
        }

        private String GenTaskName()
        {
             return this.taskModelComboBox.Text + "_" + DateTime.Now.ToString("yyyyMMdd");
        }

        private String GenWorkspace()
        {
            return @"/tmp/iao/search_toolkit/" + taskDict[this.taskModelComboBox.Text];
        }

        private bool ValidateIP(String value)
        {
            return Regex.IsMatch(value, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$") && 
                ConvertUtil.TryParseIntList(value, '.').TrueForAll(item => item >= 0 && item <= 255);
        }

        private bool ValidateTooLong(String value, int defaultMaxLength = 128)
        {
            return value.Length < defaultMaxLength;
        }

        private bool ValidateSpecialChars(String value, String specialChars = @"!@#$%^&*()+=\/'~`|[],")
        {
            return value.IndexOfAny(specialChars.ToCharArray()) == -1;
        }

        private bool ValidateNotEmpty(String value)
        {
            return !String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value);
        }

        private bool ValidateTaskName()
        {
            String value = this.taskNameTB.Text;
            return ValidateNotEmpty(value) && ValidateTooLong(value) && ValidateSpecialChars(value);
        }

        private bool ValidateBastionIP() 
        {
            return ValidateIP(this.bastionIPTB.Text);
        }

        private bool ValidateSearchAgentIP()
        {
            return ValidateIP(this.searchAgentIPTB.Text);
        }

        private bool ValidateUsername()
        {
            String value = this.usernameTB.Text;
            return ValidateNotEmpty(value) && ValidateTooLong(value) && ValidateSpecialChars(value);
        }

        private bool ValidatePassword()
        {
            String value = this.passwordTB.Text;
            return ValidateNotEmpty(value) && ValidateTooLong(value) && ValidateSpecialChars(value);
        }
        private bool ValidateInputControls()
        {
            return  ValidateTaskName() &&
                    ValidateUsername() && 
                    ValidatePassword() &&
                    ValidateBastionIP() &&
                    ValidateSearchAgentIP();
        }
        public TaskInfo ShowTaskConfigDialog()
        {
            taskInfoGB.Visible = false;
            return this.ShowDialog() == DialogResult.OK ? GenTaskInfo() : TaskInfo.EmptyTaskInfo;
        }

        public DialogResult ShowTaskInfoDialog(TaskInfo taskInfo)
        {
            taskInfoGB.Visible = true;
            LoadTaskInfo(taskInfo);
            ReadOnlyInputControls();   // 展示任务信息时, 不需要更改
            return this.ShowDialog();
        }
    }
}
