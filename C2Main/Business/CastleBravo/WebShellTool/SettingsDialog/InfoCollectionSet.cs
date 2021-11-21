using C2.Controls;
using C2.Utils;
using C2.Core;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class InfoCollectionSet : StandardDialog
    {
        private string Account { get => this.mysqlAccount.Text.Trim(); }
        private string DictAddr { get => this.addrTextBox.Text.Trim(); }

        public InfoCollectionSet()
        {
            InitializeComponent();
            InitializeWebShell();
        }

        private void InitializeWebShell()
        {
            this.mysqlAccount.Text = Global.MysqlAccount;
            this.addrTextBox.Text = Global.MysqlDictAddr;
        }
        protected override bool OnOKButtonClick()
        {
            if (Account.IsNullOrEmpty() || DictAddr.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("【账号】和【地址】不能为空。");
                return false;
            }
            Global.MysqlDictAddr = DictAddr;
            Global.MysqlAccount = Account;
            return base.OnOKButtonClick();
        }
    }
}
