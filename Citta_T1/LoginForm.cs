using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Business;

namespace Citta_T1
{
    public partial class LoginForm : Form
    {
        List<string> users;
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
            
            string userName = this.userNameComboBox.Text;
            if (userName == "")
                return;
            LoginInfo lgInfo = new LoginInfo();
            lgInfo.CreatNewXml();
            if (this.loginCheckBox.Checked && !users.Contains(userName))
                lgInfo.WriteUserInfo(userName);
            lgInfo.WriteLastLogin(userName);            
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.UserName = userName;
            mainForm.ShowDialog();

            this.Close();
           
        }
    }
}
