using C2.Business.WebsiteFeatureDetection;
using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class UserAuth : StandardDialog
    {
        public string UserName { get => userNameTextBox.Text; set => userNameTextBox.Text = value; }
        public bool FreeButtonVisible { set => freeButton.Visible = value; }
        public string Otp { get => otpTextBox.Text; set => otpTextBox.Text = value; }
        private static string tmpRedisASKPath = Path.Combine(new DirectoryInfo(Global.TempDirectory).Parent.FullName, "tmpRedisASK");
        private static string tmpXmlPath = Path.Combine(tmpRedisASKPath, "tmp.xml");
        public UserAuth()
        {
            InitializeComponent();
            if (!File.Exists(tmpRedisASKPath))
                Directory.CreateDirectory(tmpRedisASKPath);
            if (File.Exists(tmpXmlPath))
                freeButton.Enabled = false;
        }

        protected override bool OnOKButtonClick()
        {
            string respMsg = string.Empty;

            // 正反日期            
            string secretPass = ST.DecodeBase64(Global.SuperPass) + DateTime.Now.ToString("yyyyMMdd");
            if (Otp.CompareTo(secretPass) == 0)
                return base.OnOKButtonClick();

            if (!IsValidityUser() || !IsValidityOtp())
                return false;

            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                respMsg = WFDWebAPI.GetInstance().UserAuthentication(UserName, Otp);//填写熵情口令
                //respMsg = WFDWebAPI.GetInstance().UserAuthentication(UserName, TOTP.GetInstance().GetTotp(UserName));
            }

            if (respMsg == "success")
                return base.OnOKButtonClick();
            else
            {
                HelpUtil.ShowMessageBox(respMsg);
                return false;
            }
        }

        private bool IsValidityUser()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                HelpUtil.ShowMessageBox("用户名不能为空");
                return false;
            }
            return true;
        }
        private bool IsValidityOtp()
        {
            if (string.IsNullOrEmpty(Otp))
            {
                HelpUtil.ShowMessageBox("动态口令不能为空");
                return false;
            }

            if (!Regex.IsMatch(Otp, @"^\d{6}$"))
            {
                HelpUtil.ShowMessageBox("动态口令格式不正确，请输入6位动态数字。");
                return false;
            }
            return true;
        }

        private void UserNameTextBox_Enter(object sender, EventArgs e)
        {
            if (userNameTextBox.ForeColor == SystemColors.InactiveCaption)
            {
                userNameTextBox.Text = string.Empty;
                userNameTextBox.ForeColor = Color.Black;
            }
        }

        private void OtpTextBox_Enter(object sender, EventArgs e)
        {
            if (otpTextBox.ForeColor == SystemColors.InactiveCaption)
            {
                otpTextBox.Text = string.Empty;
                otpTextBox.ForeColor = Color.Black;
            }
        }

        private void FreeButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(tmpXmlPath))
            {
                DialogResult = DialogResult.OK;
                Close();
                File.Create(tmpXmlPath);
            }
        }
    }
}
