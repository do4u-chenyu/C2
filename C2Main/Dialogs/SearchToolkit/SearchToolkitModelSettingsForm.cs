using C2.Utils;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.SearchToolkit
{
    public partial class SearchToolkitModelSettingsForm : Form
    {
        private int showDialogCount = 0; // 记录Form的show次数
        public String StartTime { get => this.startTimeTB.Text.Trim(); set => this.startTimeTB.Text = value; }
        public String EndTime { get => this.endTimeTB.Text.Trim(); set => this.endTimeTB.Text = value; }

        public String QueryStr { get => this.queryTB.Text.Trim(); set => this.queryTB.Text = value; }

        private String validateMessage;
        public SearchToolkitModelSettingsForm()
        {
            InitializeComponent();
            InitializeInputControls();
        }

        private void InitializeInputControls()
        {
            this.startTimeTB.Text = String.Empty;
            this.endTimeTB.Text   = String.Empty;
            this.queryTB.Text     = String.Empty;
        }

        private void InitializeQueryDefaultTime(int days = 90)
        {
            DateTime e = DateTime.Now;
            DateTime s = e.AddDays(0 - days);
            this.startTimeTB.Text = s.ToString("yyyyMMddHHmmss");
            this.endTimeTB.Text = e.ToString("yyyyMMddHHmmss");
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
            validateMessage = ValidateQueryStr()  ? validateMessage : "查询字符串不能为空";

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

        private bool ValidateQueryStr()
        {
            return !this.queryTB.Enabled || !String.IsNullOrEmpty(this.queryTB.Text.Trim());
        }

        public DialogResult ShowDialog(bool readOnly = true, bool queryReadOnly = true)
        {
            this.confirmButton.Enabled = !readOnly;
            this.startTimeTB.Enabled   = !readOnly;
            this.endTimeTB.Enabled     = !readOnly;
            this.queryTB.Enabled       = !readOnly && !queryReadOnly;
           
            if (!readOnly && showDialogCount == 0)
                InitializeQueryDefaultTime();  // 第一次给个默认90天的事件范围

            showDialogCount++;
            return base.ShowDialog();
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
