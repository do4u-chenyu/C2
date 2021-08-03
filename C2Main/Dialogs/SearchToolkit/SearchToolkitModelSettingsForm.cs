using C2.Utils;
using System;
using System.Windows.Forms;

namespace C2.SearchToolkit
{
    public partial class SearchToolkitModelSettingsForm : Form
    {
        private SearchTaskInfo task;
        private String validateMessage;
        public SearchToolkitModelSettingsForm()
        {
            InitializeComponent();
        }

        private bool ValidateInputControls()
        {
            validateMessage = String.Empty;
            // 从后往前验证
            validateMessage = ValidateStartTime() ? validateMessage : "查询【开始时间】格式不对,例如: 20201213041856";
            validateMessage = ValidateEndTime()   ? validateMessage : "查询【结束时间】格式不对,例如: 20201221041856; 或者【结束时间】早于【开始时间】.";
            return String.IsNullOrEmpty(validateMessage);
        }

        private bool ValidateEndTime()
        {
            return true;
        }

        private bool ValidateStartTime()
        {
            return true;
        }

        public DialogResult ShowDialog(SearchTaskInfo task, bool readOnly = true)
        {
            this.confirmButton.Enabled = !readOnly;
            this.startTimeTB.Enabled   = !readOnly;
            this.endTimeTB.Enabled     = !readOnly;

            this.task = task;
            this.startTimeTB.Text = task.Settings.StartTime;
            this.endTimeTB.Text   = task.Settings.EndTime;
            return this.ShowDialog();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputControls())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                HelpUtil.ShowMessageBox(validateMessage);
        }
    }
}
