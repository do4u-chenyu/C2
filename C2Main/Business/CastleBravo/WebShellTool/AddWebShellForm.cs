using C2.Controls;
using C2.Core;
using C2.Utils;
using System.Text;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class AddWebShellForm : StandardDialog
    {
        private WebShellTaskConfig wtc;
        public AddWebShellForm()
        {
            InitializeComponent();
            InitializeWebShell();
        }

        private void InitializeWebShell()
        {
            foreach (string key in ClientSetting.WSDict.Keys)
                this.versionComboBox.Items.Add(key);
            this.trojanTypeCombox.SelectedIndex = 0;
            this.versionComboBox.SelectedIndex = 0;
            wtc = WebShellTaskConfig.Empty;
        }

        public WebShellTaskConfig ShowDialog(string createTime)
        {  
            return base.ShowDialog() == System.Windows.Forms.DialogResult.OK ?
                new WebShellTaskConfig(createTime,
                                       remarkTextBox.Text,
                                       urlTextBox.Text,
                                       passwordTextBox.Text,
                                       trojanTypeCombox.Text,
                                       wtc == WebShellTaskConfig.Empty ? string.Empty : wtc.Status,
                                       versionComboBox.Text,
                                       wtc == WebShellTaskConfig.Empty ? string.Empty : wtc.ProbeInfo,
                                       wtc == WebShellTaskConfig.Empty ? string.Empty : wtc.IP,
                                       wtc == WebShellTaskConfig.Empty ? string.Empty : wtc.Country,
                                       wtc == WebShellTaskConfig.Empty ? string.Empty : wtc.Country2,
                                       databaseConfigTextBox.Text) : WebShellTaskConfig.Empty;
        }

        public WebShellTaskConfig ShowDialog(WebShellTaskConfig task)
        {
            this.wtc = task;
            remarkTextBox.Text = task.Remark;
            urlTextBox.Text = task.Url;
            passwordTextBox.Text = task.Password;
            trojanTypeCombox.Text = task.TrojanType;
            versionComboBox.Text = task.ClientVersion;

            // 木马类型不允许修改, 支持修改没有意义
            trojanTypeCombox.Enabled = false;
            databaseConfigTextBox.Text = task.DatabaseConfig;
            // 创建时间不需要修改
            return ShowDialog(task.CreateTime);
        }

        protected override bool OnOKButtonClick()
        {
            //TODO 判断必填是否有值
            if(urlTextBox.Text.IsNullOrEmpty() || passwordTextBox.Text.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("【Url】 和 【密码】 不能为空。");
                return false;
            }
            return base.OnOKButtonClick();
        }

        private void UrlTextBox_TextChanged(object sender, System.EventArgs e)
        {
            versionComboBox.Text  = WebShellTaskConfig.AutoDetectClientType(urlTextBox.Text, versionComboBox.Text);
            trojanTypeCombox.Text = WebShellTaskConfig.AutoDetectTrojanType(urlTextBox.Text); 
        }

        private void DatabaseConfigTextBox_Focus(object sender, System.EventArgs e)
        {
            databaseConfigTextBox.ForeColor = System.Drawing.Color.Black;
        }

        private void SampleLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if (databaseConfigTextBox.Text.Trim().IsNullOrEmpty())
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("TYPE:mysql")
                  .AppendLine("HOST:localhost")
                  .AppendLine("USER:root")
                  .AppendLine("PASS:root");
                databaseConfigTextBox.Text = sb.ToString();
            }
        }
    }
}
