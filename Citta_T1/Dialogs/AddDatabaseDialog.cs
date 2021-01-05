﻿using C2.Controls;
using C2.Controls.Left;
using C2.Core;
using C2.Database;
using C2.Model;
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

namespace C2.Dialogs
{
    partial class AddDatabaseDialog : StandardDialog
    {
        public DatabaseItem DatabaseInfo { get; set; }
        private DatabaseDialogMode Mode;
        private LinkButton LinkButton;
        public AddDatabaseDialog(DatabaseItem databaseInfo=null, DatabaseDialogMode mode=DatabaseDialogMode.New, LinkButton linkButton=null)
        {
            InitializeComponent();
            if (databaseInfo != null)
            {
                DatabaseInfo = databaseInfo;
                InitializeContent();
            }
            Mode = mode;
            if (linkButton != null)
            {
                LinkButton = linkButton;
            }
        }

        public void InitializeContent()
        {
            databaseTypeComboBox.SelectedIndex = (int)DatabaseInfo.Type-1;
            this.serverTextBox.Text = DatabaseInfo.Server ;
            this.sidRadiobutton.Checked = DatabaseInfo.SID == "" ? false : true;
            this.sidTextBox.Text = DatabaseInfo.SID;
            this.serviceRadiobutton.Checked = DatabaseInfo.Service == "" ? false : true;
            this.serviceTextBox.Text = DatabaseInfo.Service;
            this.portTextBox.Text = DatabaseInfo.Port;
            this.userTextBox.Text = DatabaseInfo.User;
            this.passwordTextBox.Text = DatabaseInfo.Password;
        }
        private DatabaseItem GenDatabaseInfoFormDialog()
        {
            DatabaseItem tmpDatabaseInfo = new DatabaseItem();
            tmpDatabaseInfo.Type = (DatabaseType)(databaseTypeComboBox.SelectedIndex + 1);
            tmpDatabaseInfo.Server = this.serverTextBox.Text;
            tmpDatabaseInfo.SID = this.sidRadiobutton.Checked ? this.sidTextBox.Text : "";
            tmpDatabaseInfo.Service = this.serviceRadiobutton.Checked ? this.serviceTextBox.Text : "";
            tmpDatabaseInfo.Port = this.portTextBox.Text;
            tmpDatabaseInfo.User = this.userTextBox.Text;
            tmpDatabaseInfo.Password = this.passwordTextBox.Text;
            return tmpDatabaseInfo;
        }

        protected override bool OnOKButtonClick()
        {
            //判断输入框是否有空值
            if (InputHasEmpty())
            {
                HelpUtil.ShowMessageBox(HelpUtil.DbInfoIsEmptyInfo);
                return false;
            }

            DatabaseItem tmpDatabaseInfo = GenDatabaseInfoFormDialog();

            //如果新旧一致，直接返回了
            if (DatabaseInfo != null && DatabaseInfo.AllDatabaseInfo.Equals(tmpDatabaseInfo.AllDatabaseInfo))
                return base.OnOKButtonClick();

            if (Global.GetDataSourceControl().LinkSourceDictI2B.ContainsKey(tmpDatabaseInfo.AllDatabaseInfo))
            {
                HelpUtil.ShowMessageBox("该连接已存在","已存在",MessageBoxIcon.Warning);
                return false;
            } 

            OraConnection conn = new OraConnection(tmpDatabaseInfo);
            if (!DbUtil.TestConn(conn, true))
                return false;

            DatabaseInfo = tmpDatabaseInfo;
            return base.OnOKButtonClick();
        }
        private void TestButton_Click(object sender, EventArgs e)
        {
            if (InputHasEmpty())
            {
                HelpUtil.ShowMessageBox(HelpUtil.DbInfoIsEmptyInfo);
                return;
            }

            DatabaseItem tmpDatabaseInfo = GenDatabaseInfoFormDialog();
            //如果新旧一致，直接返回了

            OraConnection conn = new OraConnection(tmpDatabaseInfo);
            if (DbUtil.TestConn(conn))
                HelpUtil.ShowMessageBox(HelpUtil.DbConnectSucceeded, "连接成功", MessageBoxIcon.Information);
            else
                HelpUtil.ShowMessageBox(HelpUtil.DbConnectFailed, "连接失败", MessageBoxIcon.Information);
        }

        private bool InputHasEmpty()
        {
            return (databaseTypeComboBox.SelectedIndex == -1) || string.IsNullOrEmpty(this.serverTextBox.Text) || string.IsNullOrEmpty(this.portTextBox.Text) ||
                (this.sidRadiobutton.Checked ? string.IsNullOrEmpty(this.sidTextBox.Text) : string.IsNullOrEmpty(this.serviceTextBox.Text)) ||
                string.IsNullOrEmpty(this.userTextBox.Text) || string.IsNullOrEmpty(this.passwordTextBox.Text);
        }
    }
    public enum DatabaseDialogMode
    {
        Edit,
        New
    }
}
