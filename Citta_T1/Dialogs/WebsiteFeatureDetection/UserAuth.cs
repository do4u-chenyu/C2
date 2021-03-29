using C2.Business.WebsiteFeatureDetection;
using C2.Controls;
using C2.Utils;
using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class UserAuth : StandardDialog
    {
        public string UserName { get => this.userNameTextBox.Text; set => this.userNameTextBox.Text = value; }
        public string Otp { get => this.otpTextBox.Text; set => this.otpTextBox.Text = value; }

        public UserAuth()
        {
            InitializeComponent();
        }

        protected override bool OnOKButtonClick()
        {
            if (!IsValidityUser() || !IsValidityOtp())
                return false;

            //TODO 向接口传参
            string respStatus = WFDWebAPI.GetInstance().UserAuthentication(UserName, Otp);
            if (respStatus == "fail")
            {
                HelpUtil.ShowMessageBox("用户认证失败，请重试或联系相关负责人。");
                return false;
            }

            return base.OnOKButtonClick();
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
            if (this.userNameTextBox.ForeColor == SystemColors.InactiveCaption)
            {
                this.userNameTextBox.Text = String.Empty;
                this.userNameTextBox.ForeColor = Color.Black;
            }
        }

        private void OtpTextBox_Enter(object sender, EventArgs e)
        {
            if (this.otpTextBox.ForeColor == SystemColors.InactiveCaption)
            {
                this.otpTextBox.Text = String.Empty;
                this.otpTextBox.ForeColor = Color.Black;
            }
        }
    }
}
