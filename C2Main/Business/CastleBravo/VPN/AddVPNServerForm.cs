using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Drawing;
using System.Text;

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

            Task2Control(task);
        }

        private void Task2Control(VPNTaskConfig task)
        {
            remarkTB.Text = task.Remark;
            hostTB.Text = task.Host;
            portTB.Text = task.Port;
            pwdTB.Text = task.Password;
            
            ssTextBox.Text = new StringBuilder(task.Content)
                .AppendLine()
                .AppendLine()
                .AppendLine(task.OtherInfo.IsEmpty() ? string.Empty : "其他信息:")
                .AppendLine(task.OtherInfo)
                .AppendLine(task.ProbeInfo.IsEmpty() ? string.Empty : "探测信息:")
                .AppendLine(task.ProbeInfo)
                .ToString();


            methodCB.SelectedIndex = Math.Max(0, methodCB.Items.IndexOf(task.Method));
            versionCB.SelectedIndex = Math.Max(0, versionCB.Items.IndexOf(task.SSVersion));
        }

        private void Control2Task(VPNTaskConfig task)
        {
            task.Remark = this.remarkTB.Text;
            task.Host   = this.hostTB.Text;
            task.Port   = this.portTB.Text;
            task.Password = this.pwdTB.Text;

            task.Method    = methodCB.Text;
            task.SSVersion = versionCB.Text;

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
            Control2Task(task);
            return task;
        }

        protected override bool OnOKButtonClick()
        {
            //TODO 判断必填是否有值
            if (hostTB.Text.IsNullOrEmpty() || portTB.Text.IsNullOrEmpty() || versionCB.Text.IsNullOrEmpty() ||
                pwdTB.Text.IsNullOrEmpty() || methodCB.Text.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("IP、端口等必填项不能为空。");
                return false;
            }
            return base.OnOKButtonClick();
        }
    }
}
