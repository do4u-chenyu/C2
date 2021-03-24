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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class UserAuthentication : StandardDialog
    {
        public string UserName { get => this.userName.Text; set => this.userName.Text = value; }
        public string Otp { get => this.otp.Text; set => this.otp.Text = value; }
        public string Token;
        public UserAuthentication()
        {
            InitializeComponent();
        }

        protected override bool OnOKButtonClick()
        {
            //TODO 向接口传参
            Token = WFDApi.UserAuthentication(UserName, Otp);
            if (string.IsNullOrEmpty(Token))
            {
                HelpUtil.ShowMessageBox("用户认证失败，请重试或联系相关负责人。");
                return false;
            }
                
            
            return base.OnOKButtonClick();
        }
    }
}
