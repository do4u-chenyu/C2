using C2.Controls;
using C2.Core;
using C2.Utils;
using System;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class AddWebShellForm : StandardDialog
    {

        public AddWebShellForm()
        {
            InitializeComponent();
            InitializeWebShell();
        }

        private void InitializeWebShell()
        {
            this.trojanTypeCombox.SelectedIndex = 0;
            this.versionComboBox.SelectedIndex = 0;
        }

        public new WebShellTaskConfig ShowDialog()
        {
            return base.ShowDialog() == System.Windows.Forms.DialogResult.OK ?
                new WebShellTaskConfig(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                       nameTextBox.Text,
                                       urlTextBox.Text,
                                       passwordTextBox.Text,
                                       trojanTypeCombox.Text,
                                       versionComboBox.Text,
                                       databaseConfigTextBox.Text) : WebShellTaskConfig.Empty;
        }

        protected override bool OnOKButtonClick()
        {
            //TODO 判断必填是否有值
            if(urlTextBox.Text.IsNullOrEmpty() || passwordTextBox.Text.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("名称, url, 密码 不能为空。");
                return false;
            }
            return base.OnOKButtonClick();
        }
    }
}
