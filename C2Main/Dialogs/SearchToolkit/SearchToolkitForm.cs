using C2.Business.SSH;
using C2.Dialogs;
using C2.Utils;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.SearchToolkit
{
    public partial class SearchToolkitForm : Form
    {
        private SearchTaskInfo task;
        private string validateMessage;
        private Control[] inputControls;
        private SearchToolkitModelSettingsForm modelSettingsForm;

        private static readonly LogUtil log = LogUtil.GetInstance("SearchToolkit");
        public SearchToolkitForm()
        {
            InitializeComponent();
            InitializeInputControls();
            InitializeModelSettingForm();
        }

        private void InitializeModelSettingForm()
        {
            modelSettingsForm = new SearchToolkitModelSettingsForm();
        }

        private void InitializeInputControls()
        {
            task = SearchTaskInfo.EmptyTaskInfo;
            validateMessage = string.Empty;

            inputControls = new Control[] { 
                this.usernameTB, 
                this.passwordTB,
                this.bastionIPTB,
                this.searchAgentIPTB,
                this.remoteWorkspaceTB,
                this.taskModelComboBox,
                this.taskNameTB,
                this.connectTestButton,
                this.interfaceIPTB,
                this.searchPwdTB
            };
            // 根据字典 自动添加 模型下拉列表
            foreach(string key in SearchTaskInfo.TaskDescriptionTable.Keys)
                this.taskModelComboBox.Items.Add(key);

            this.taskModelComboBox.SelectedIndex = 0; // 默认选择 涉赌任务
            this.helpLabel.Visible = !IsReadOnly();
        }


        public SearchToolkitForm GenLastInfo(string bIP, string sIP, string iIP, string name)
        {
            if (!string.IsNullOrEmpty(bIP))
                this.bastionIPTB.Text = bIP;
            if (!string.IsNullOrEmpty(sIP))
                this.searchAgentIPTB.Text = sIP;
            if (!string.IsNullOrEmpty(iIP))
                this.interfaceIPTB.Text = iIP;
            if (!string.IsNullOrEmpty(name))
                this.usernameTB.Text = name;
            return this;
        }
        private SearchTaskInfo GenTaskInfo()
        {
            string value = string.Join(OpUtil.TabSeparatorString, new string[] {
                                            this.taskNameTB.Text,  // 刚开始创建时，没有ID
                                            this.taskNameTB.Text,
                                            DateTime.Now.ToString("yyyyMMddHHmmss"),
                                            this.taskModelComboBox.Text,
                                            "RUNNING",  // NULL, RUNNING, DONE, FAIL, TIMEOUT, CONNECT_FAIL
                                            this.usernameTB.Text,
                                            this.passwordTB.Text,
                                            this.bastionIPTB.Text,
                                            this.searchAgentIPTB.Text,
                                            this.remoteWorkspaceTB.Text,
                                            this.interfaceIPTB.Text,
                                            this.modelSettingsForm.StartTime,
                                            this.modelSettingsForm.EndTime,
                                            this.modelSettingsForm.QueryStr,
                                            this.searchPwdTB.Text
            }) ;

            return SearchTaskInfo.StringToTaskInfo(value);
        }

        private void ReadOnlyInputControls()
        {
            foreach (Control ct in inputControls)
                ct.Enabled = false;
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            if (task == SearchTaskInfo.EmptyTaskInfo)
                return;

            this.saveFileDialog.FileName = string.Format("{0}_{1}", task.TaskName, task.TaskCreateTime);
            DialogResult ret = this.saveFileDialog.ShowDialog();
            if (ret != DialogResult.OK)
                return;


            string ffp = this.saveFileDialog.FileName;
            // TODO ProgressBar 处理
            BastionDownloadProgressBar progressBar = new BastionDownloadProgressBar(task, ffp)
            {
                Status = "下载中",
                ProgressValue = 0,
                ProgressPercentage = "0%",
                MinimumValue = 0,   
                MaximumValue = 100,
            };
            task.LastErrorMsg = string.Empty; // 清空错误信息
            progressBar.Show(this);

            bool succ = progressBar.Download();

            // 用户提前终止任务，进度条关闭，后续无法赋值
            if (progressBar == null || !progressBar.Visible)
                return;

            if (succ)
                progressBar.Status = string.Format("{0}-任务【{1}】下载成功", task.TaskModel, task.TaskName);
            else
                progressBar.Status = task.LastErrorMsg;

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            new Log.Log().LogManualButton(this.Text, "运行");
            if (ValidateInputControls())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }  
            else
                HelpUtil.ShowMessageBox(validateMessage);
        }

        private void ModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.taskNameTB.Text = GenTaskName();
            this.remoteWorkspaceTB.Text = GenWorkspace();
            this.helpLabel.Text = GenHelpInfo();
        }

        private string GenHelpInfo()
        {
            if (IsReadOnly())
                return string.Empty;

            string key = this.taskModelComboBox.SelectedItem.ToString();

            if (!SearchTaskInfo.TaskHelpInfoTable.ContainsKey(key))
                return string.Empty;

            return SearchTaskInfo.TaskHelpInfoTable[key];
        }

        private string GenTaskName()
        {
             return this.taskModelComboBox.Text + DateTime.Now.ToString("MMdd");
        }

        private string GenWorkspace()
        {
            return SearchTaskInfo.SearchWorkspace + SearchTaskInfo.TaskDescriptionTable[this.taskModelComboBox.Text];
        }

        private bool ValidateIP(string value)
        {
            if (string.IsNullOrEmpty(value))  // 有专门的不为空检测，为空时，认为符号要求, 用于可选项的校验
                return true;

            bool match0 = Regex.IsMatch(value, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$") &&
               ConvertUtil.TryParseIntList(value, '.').TrueForAll(item => item >= 0 && item <= 255);
            bool match1 = Regex.IsMatch(value, @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\:\d{0,5}$") &&
                ConvertUtil.TryParseInt(value, 22) <= 65535;
            return match0 || match1;
        }

        private bool ValidateTooLong(string value, int defaultMaxLength = 128)
        {
            return value.Length < defaultMaxLength;
        }

        private bool ValidateSpecialChars(string value, string specialChars = "!@#$%^&*()<>?:;\"+=\\/'~`|[],")
        {
            return value.IndexOfAny(specialChars.ToCharArray()) == -1;
        }

        private bool ValidateNotEmpty(string value)
        {
            return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
        }

        private bool ValidateTaskName()
        {
            string value = this.taskNameTB.Text;
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

        private bool ValidateInterfaceIP()
        {
            return ValidateIP(this.interfaceIPTB.Text);
        }

        private bool ValidateUsername()
        {
            string value = this.usernameTB.Text;
            return ValidateNotEmpty(value) && ValidateTooLong(value) && ValidateSpecialChars(value);
        }

        private bool ValidatePassword()
        {
            string value = this.passwordTB.Text;
            return ValidateNotEmpty(value) && ValidateTooLong(value);
        }

        private void TrimInputControls()
        {
            foreach (Control ct in inputControls)
                ct.Text = ct.Text.Trim();
        }
        private bool ValidateInputControls()
        {
            TrimInputControls(); 

            validateMessage = string.Empty;
            // 从后往前验证
            validateMessage = ValidateSearchAgentIP() ? validateMessage : "全文机【IP:Port】格式不对，标准端口时Port可不填";
            validateMessage = ValidateInterfaceIP()   ? validateMessage : "界面机【IP:Port】格式不对，标准端口时Port可不填";
            validateMessage = ValidateBastionIP()     ? validateMessage : "堡垒机【IP:Port】格式不对，标准端口时Port可不填";
            validateMessage = ValidatePassword()      ? validateMessage : "堡垒机 【密码】  不能为空, 不能超过128个字符";
            validateMessage = ValidateUsername()      ? validateMessage : "堡垒机 【用户名】不能为空, 不能超过128个字符";
            validateMessage = ValidateTaskName()      ? validateMessage : "任务名称 不能为空,不能超过128个字符,不能含有特殊字符";
            
            return string.IsNullOrEmpty(validateMessage);
        }
        public SearchTaskInfo ShowTaskConfigDialog()
        {
            taskInfoGB.Visible = false;
            confirmButton.Enabled = true;

            return this.ShowDialog() == DialogResult.OK ? GenTaskInfo() : SearchTaskInfo.EmptyTaskInfo;
        }

        private void UpdateTaskInfo(SearchTaskInfo task)
        {
            this.task = task;
            // 更新任务状态
            if (task.TaskStatus != "DONE")
            {
                task.TaskStatus = new BastionAPI(task).Login().QueryTaskStatus();
                task.Save();
            }
               
            // 更新界面元素    
            this.usernameTB.Text             = task.Username;
            this.passwordTB.Text             = task.Password;
            this.taskNameTB.Text             = task.TaskName;
            this.bastionIPTB.Text            = task.BastionIP;
            this.taskModelComboBox.Text      = task.TaskModel;
            this.interfaceIPTB.Text          = task.InterfaceIP;
            this.searchAgentIPTB.Text        = task.SearchAgentIP;
            this.remoteWorkspaceTB.Text      = string.Format("{0}/{1}_{2}", task.RemoteWorkspace, task.TaskName, task.TaskCreateTime);
            this.taskStatusLabel.Text        = task.TaskStatus;
            this.downloadButton.Enabled      = task.TaskStatus == "DONE";
            this.modelSettingsForm.StartTime = task.Settings.StartTime;
            this.modelSettingsForm.EndTime   = task.Settings.EndTime;
            this.modelSettingsForm.QueryStr  = task.Settings.QueryStr;
            this.searchPwdTB.Text            = task.SearchPassword;
        }
        private bool IsReadOnly()
        {
            return taskInfoGB.Visible && !confirmButton.Enabled;
        }

        public DialogResult ShowTaskInfoDialog(SearchTaskInfo task)
        {
            taskInfoGB.Visible = true;
            confirmButton.Enabled = false;
            helpLabel.Visible = false;

            // 更新任务状态和界面元素
            using (GuarderUtil.WaitCursor)
                UpdateTaskInfo(task);
            // 展示任务信息时, 不需要更改
            ReadOnlyInputControls();  
            return this.ShowDialog();
        }

        private void ConnectTestButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputControls())
            {
                SearchTaskInfo task = this.GenTaskInfo();
                log.Info(string.Format("=========== 任务:{0} 开始连接测试 ===========", task.TaskName));
                using (GuarderUtil.WaitCursor)
                    if (new BastionAPI(task).TestConn())
                        HelpUtil.ShowMessageBox("登陆堡垒机测试成功");
                    else
                        HelpUtil.ShowMessageBox(string.Format("连接失败:{0}", task.LastErrorMsg));
            }
            else
                HelpUtil.ShowMessageBox(validateMessage);
        }

        private void Label4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FileUtil.TryClipboardSetText(this.remoteWorkspaceTB.Text))
                HelpUtil.ShowMessageBox(string.Format("[{0}] 已复制到剪切板", this.remoteWorkspaceTB.Text));
        }

        private void TaskConfigPB_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            modelSettingsForm.ShowDialog(IsReadOnly(), this.taskModelComboBox.Text != "自定义查询");
        }

        private void TaskConfigPB_MouseEnter(object sender, EventArgs e)
        {
            this.TaskConfigPB.BackColor = SystemColors.InactiveCaption;
        }

        private void TaskConfigPB_MouseLeave(object sender, EventArgs e)
        {
            this.TaskConfigPB.BackColor = Color.Transparent;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            passwordTB.UseSystemPasswordChar  = !checkBox1.Checked;
            searchPwdTB.UseSystemPasswordChar = !checkBox1.Checked;
        }
    }
}
