using System;
using System.Drawing;
using System.Windows.Forms;

namespace C2.SearchToolkit
{
    public partial class SearchToolkitForm : Form
    {
        private Control[] inputControls;

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
            this.taskModelComboBox.SelectedIndex = 0;
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

        private void ReadOnlyInputControl()
        {
            foreach (Control ct in inputControls)
                ct.Enabled = false;
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LinuxWorkspaceTB_Enter(object sender, EventArgs e)
        {
            remoteWorkspaceTB.ForeColor = Color.Black;
        }

        private void ModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private bool ValidateInputControls()
        {
            return true;
        }
        public TaskInfo ShowTaskConfigDialog()
        {
            taskInfoGB.Visible = false;
            DialogResult ret = this.ShowDialog();
            if (ret != DialogResult.OK || !ValidateInputControls())
                return TaskInfo.EmptyTaskInfo;

            return GenTaskInfo();
        }

        public DialogResult ShowTaskInfoDialog(TaskInfo taskInfo)
        {
            taskInfoGB.Visible = true;
            LoadTaskInfo(taskInfo);
            ReadOnlyInputControl();   // 展示任务信息时, 不需要更改
            return this.ShowDialog();
        }
    }
}
