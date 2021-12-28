using C2.Controls;
using C2.Utils;
using C2.Core;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class MysqlBlastingSet : StandardDialog
    {
        private string Account { get => this.mysqlAccount.Text.Trim(); }
        private string DictAddr { get => this.addrTextBox.Text.Trim(); }
        private string Timeout { get => timeoutTB.Text.Trim(); }

        public MysqlBlastingSet()
        {
            InitializeComponent();
            InitializeWebShell();
        }

        private void InitializeWebShell()
        {
            this.mysqlAccount.Text = ClientSetting.MysqlAccount;
            this.addrTextBox.Text = ClientSetting.MysqlDictAddr;
            this.timeoutTB.Text = ClientSetting.MysqlTimeout;
        }
        protected override bool OnOKButtonClick()
        {
            if (Account.IsNullOrEmpty() || DictAddr.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("【账号】和【地址】不能为空。");
                return false;
            }

            ClientSetting.MysqlDictAddr = DictAddr;
            ClientSetting.MysqlAccount = Account;
            ClientSetting.MysqlTimeout = Timeout;

            return base.OnOKButtonClick();
        }
    }
}
