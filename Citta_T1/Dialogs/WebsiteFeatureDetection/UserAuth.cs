using C2.Business.WebsiteFeatureDetection;
using C2.Controls;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class UserAuth : StandardDialog
    {
        private string defaultUserTip = "工号，首字母大写（例如X1234）";
        private string defaultOtpTip = "星空下-工作台-熵情口令6位动态数字";
        WFDWebAPI webAPI;
        public string UserName { get => this.userNameTextBox.Text; set => this.userNameTextBox.Text = value; }
        public string Otp { get => this.otpTextBox.Text; set => this.otpTextBox.Text = value; }
        public string Token;
        public UserAuth()
        {
            InitializeComponent();
            SetDefaultUserTip();
            SetDefaultOtpTip();
            webAPI = new WFDWebAPI();
        }

        private void SetDefaultUserTip()
        {
            this.userNameTextBox.Text = defaultUserTip;
            this.userNameTextBox.ForeColor = Color.Gray;
            this.userNameTextBox.Font = new System.Drawing.Font("宋体", 9F);
        }
        private void SetDefaultOtpTip()
        {
            this.otpTextBox.Text = defaultOtpTip;
            this.otpTextBox.ForeColor = Color.Gray;
            this.otpTextBox.Font = new System.Drawing.Font("宋体", 9F);
        }

        protected override bool OnOKButtonClick()
        {
            if (!IsValidityUser() || !IsValidityOtp())
                return false;

            //TODO 向接口传参
            Token = webAPI.UserAuthentication(UserName, Otp);
            if (string.IsNullOrEmpty(Token))
            {
                HelpUtil.ShowMessageBox("用户认证失败，请重试或联系相关负责人。");
                return false;
            }

            return base.OnOKButtonClick();
        }

        private bool IsValidityUser()
        {
            if (string.IsNullOrEmpty(UserName) || UserName == defaultUserTip)
            {
                HelpUtil.ShowMessageBox("用户名不能为空。");
                return false;
            }

            if (UserName.Length != 5)
            {
                HelpUtil.ShowMessageBox("用户名格式不正确，请重新输入。");
                return false;
            }
            return true;
        }
        private bool IsValidityOtp()
        {
            if (string.IsNullOrEmpty(Otp) || Otp == defaultOtpTip)
            {
                HelpUtil.ShowMessageBox("动态口令不能为空。");
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
            if (this.userNameTextBox.Text == defaultUserTip)
            {
                this.userNameTextBox.Text = "";
                this.userNameTextBox.ForeColor = Color.Black;
                this.userNameTextBox.Font = new System.Drawing.Font("宋体", 10.5F);
            }
        }

        private void UserNameTextBox_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.userNameTextBox.Text))
                SetDefaultUserTip();
        }

        private void OtpTextBox_Enter(object sender, EventArgs e)
        {
            if (this.otpTextBox.Text == defaultOtpTip)
            {
                this.otpTextBox.Text = "";
                this.otpTextBox.ForeColor = Color.Black;
                this.otpTextBox.Font = new System.Drawing.Font("宋体", 10.5F);
            }
        }

        private void OtpTextBox_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.otpTextBox.Text))
                SetDefaultOtpTip();
        }
    }
}
