using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.VPN
{
    partial class AddVPNServerForm : StandardDialog
    {
        public AddVPNServerForm()
        {
            InitializeComponent();
            this.OKButton.Size = new Size(75, 27);
            this.CancelBtn.Size = new Size(75, 27);
        }

        public VPNTaskConfig ShowDialog(string createTime)
        {
            base.ShowDialog();
            return VPNTaskConfig.Empty;
        }

        protected override bool OnOKButtonClick()
        {
            //TODO 判断必填是否有值
            if (ipTextBox.Text.IsNullOrEmpty() || portTextBox.Text.IsNullOrEmpty() || versionCombox.Text.IsNullOrEmpty() ||
                pwdTextBox.Text.IsNullOrEmpty() || encryptComboBox.Text.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("IP、端口等必填项不能为空。");
                return false;
            }
            return base.OnOKButtonClick();
        }
    }
}
