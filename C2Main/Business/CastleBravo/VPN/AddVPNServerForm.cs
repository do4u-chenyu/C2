using C2.Controls;
using C2.Core;
using C2.Utils;
using System.Drawing;

namespace C2.Business.CastleBravo.VPN
{
    partial class AddVPNServerForm : StandardDialog
    {
        public AddVPNServerForm(bool isEditMode = false)
        {
            InitializeComponent();
            InitializeOther();
        }
        private void InitializeOther()
        {
            this.OKButton.Size = new Size(75, 27);
            this.CancelBtn.Size = new Size(75, 27);
        }

        private void InitializeEditMode(VPNTaskConfig task)
        {
            this.Text = "编辑服务器信息";
            this.ssTextBox.ReadOnly = true;

            this.remarkTextBox.Text = task.Remark;
            this.hostTextBox.Text = task.Host;
            this.portTextBox.Text = task.Port;
            this.pwdTextBox.Text = task.Password;
            this.ssTextBox.Text = task.Content;

            //LV.SelectedItems[0].SubItems[5].Text = task.Method;

            //LV.SelectedItems[0].SubItems[7].Text = task.SSVersion;
        }


        public VPNTaskConfig ShowDialogNew(string createTime)
        {

            base.ShowDialog();
            return VPNTaskConfig.Empty;
        }

        public VPNTaskConfig ShowDialogEdit(VPNTaskConfig task)
        {
            InitializeEditMode(task);
            base.ShowDialog();
            return task;
        }

        protected override bool OnOKButtonClick()
        {
            //TODO 判断必填是否有值
            if (hostTextBox.Text.IsNullOrEmpty() || portTextBox.Text.IsNullOrEmpty() || versionCombox.Text.IsNullOrEmpty() ||
                pwdTextBox.Text.IsNullOrEmpty() || encryptComboBox.Text.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("IP、端口等必填项不能为空。");
                return false;
            }
            return base.OnOKButtonClick();
        }
    }
}
