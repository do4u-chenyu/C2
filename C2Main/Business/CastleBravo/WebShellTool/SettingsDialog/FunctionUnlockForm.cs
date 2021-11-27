using C2.Utils;
using System;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class FunctionUnlockForm :  Form
    {
        public FunctionUnlockForm()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            string id = IDBox.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                HelpUtil.ShowMessageBox("输入工号, 解锁SG高级功能", "工号为空");
                return;
            }
            FileUtil.TouchFile(ClientSetting.UnlockFilePath);
            this.DialogResult = DialogResult.OK;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
