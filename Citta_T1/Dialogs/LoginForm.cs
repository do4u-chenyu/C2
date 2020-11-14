using C2.Business;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using C2.Utils;

namespace C2.Dialogs
{
    public partial class LoginForm : Form
    {
        private List<string> users;
        public static MainForm mainForm { get; set; }
        public LoginForm()
        {
            InitializeComponent();

        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            LoginInfo lgInfo = new LoginInfo();
            List<string> userList = lgInfo.LoadUserInfo("user");
            users = userList;
            foreach (string item in userList)
                this.userNameComboBox.Items.Add(item);
            List<string> lastLogin = lgInfo.LoadUserInfo("lastlogin");
            foreach (string item in lastLogin)
                userNameComboBox.Text = item;

        }
        private void LoginButton_Click(object sender, EventArgs e)
        {
            // 输入用户名长度、非法字符限制
            string userName = this.userNameComboBox.Text;
            if (String.IsNullOrEmpty(userName))
            {
                HelpUtil.ShowMessageBox("请输入用户名");
                return;
            }
            if (FileUtil.ContainIllegalCharacters(userName, "用户名")
                || FileUtil.NameTooLong(userName, "用户名"))
            {
                this.userNameComboBox.Text = String.Empty;
                return;
            }

            LoginInfo lgInfo = new LoginInfo();
            lgInfo.CreatNewXml();
            if (this.loginCheckBox.Checked && !users.Contains(userName))
                lgInfo.WriteUserInfo(userName);
            lgInfo.WriteLastLogin(userName);
            this.Hide();

            mainForm = new MainForm(userName);
            mainForm.ShowDialog();

            this.Close();

        }
      

    }
}
