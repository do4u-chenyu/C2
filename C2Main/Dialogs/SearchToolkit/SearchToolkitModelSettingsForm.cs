using C2.Utils;
using System;
using System.Text.RegularExpressions;
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
            this.startTimeTB.Text = this.startTimeTB.Text.Trim();
            this.endTimeTB.Text   = this.endTimeTB.Text.Trim();
            validateMessage = String.Empty;

            // 从后往前验证
            validateMessage = startTimeTB.Text.CompareTo(endTimeTB.Text) <= 0 ? validateMessage : "查询【结束时间】应当晚于【开始时间】";
            validateMessage = ValidateEndTime()   ? validateMessage : "查询【结束时间】格式不对,例如: 19831221235959";
            validateMessage = ValidateStartTime() ? validateMessage : "查询【开始时间】格式不对,例如: 20211213235959";
            
            return String.IsNullOrEmpty(validateMessage);
        }

        private bool ValidateDateTimeYYYYMMDDHHmmSS(String value)
        {
            // 开始结束时间同时为空 视为 删除旧数据
            return String.IsNullOrEmpty(this.endTimeTB.Text)   && 
                   String.IsNullOrEmpty(this.startTimeTB.Text) || 
                   Regex.IsMatch(value, @"^\s*[1-3]\d\d\d[0-1]\d[0-3]\d[0-2]\d[0-5]\d[0-5]\d\s*$");
        }

        private bool ValidateEndTime()
        {  
            return ValidateDateTimeYYYYMMDDHHmmSS(this.endTimeTB.Text);
        }

        private bool ValidateStartTime()
        {
            return ValidateDateTimeYYYYMMDDHHmmSS(this.startTimeTB.Text);
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

        private void ApplySettings()
        {
            task.Settings.StartTime = this.startTimeTB.Text;
            task.Settings.EndTime = this.endTimeTB.Text;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (ValidateInputControls())
            {
                this.DialogResult = DialogResult.OK;
                this.ApplySettings();
                this.Close();
            }
            else
                HelpUtil.ShowMessageBox(validateMessage);
        }
    }
}
