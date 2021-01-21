using C2.Controls;
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
            this.databaseTypeComboBox.SelectedIndex = 0;
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
            DatabaseItem tmpDatabaseInfo = new DatabaseItem(
                (DatabaseType)(databaseTypeComboBox.SelectedIndex + 1),
                this.serverTextBox.Text,
                this.sidRadiobutton.Checked ? this.sidTextBox.Text : String.Empty,
                this.serviceRadiobutton.Checked ? this.serviceTextBox.Text : String.Empty,
                this.portTextBox.Text,
                this.userTextBox.Text,
                this.passwordTextBox.Text
                );
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
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                if (Global.GetDataSourceControl().LinkSourceDictI2B.ContainsKey(tmpDatabaseInfo.AllDatabaseInfo))
                {
                    HelpUtil.ShowMessageBox("该连接已存在", "已存在", MessageBoxIcon.Warning);
                    return false;
                }

                if (!DAOFactory.CreateDAO(tmpDatabaseInfo).TestConn())
                {
                    HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                    return false;
                }
                DatabaseInfo = tmpDatabaseInfo;
                return base.OnOKButtonClick();
            }
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
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                if (DAOFactory.CreateDAO(tmpDatabaseInfo).TestConn())
                    HelpUtil.ShowMessageBox(HelpUtil.DbConnectSucceeded, "连接成功", MessageBoxIcon.Information);
                else
                    HelpUtil.ShowMessageBox(HelpUtil.DbConnectFailed, "连接失败", MessageBoxIcon.Information);
            }
              
        }

        private bool InputHasEmpty()
        {
            if (String.Equals("Hive",this.databaseTypeComboBox.Text))
                return (string.IsNullOrEmpty(this.serverTextBox.Text) || string.IsNullOrEmpty(this.portTextBox.Text));

            return (databaseTypeComboBox.SelectedIndex == -1) || string.IsNullOrEmpty(this.serverTextBox.Text) || string.IsNullOrEmpty(this.portTextBox.Text) ||
                (this.sidRadiobutton.Checked ? string.IsNullOrEmpty(this.sidTextBox.Text) : string.IsNullOrEmpty(this.serviceTextBox.Text)) ||
                string.IsNullOrEmpty(this.userTextBox.Text) || string.IsNullOrEmpty(this.passwordTextBox.Text);
        }

        private void DatabaseTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            if (databaseTypeComboBox.SelectedItem == null)
                return;
            bool notHive = databaseTypeComboBox.SelectedItem.ToString() != "Hive";
            this.portTextBox.Text = notHive ? "1521" : "10000";
            this.portTextBox.ForeColor = notHive ? Color.Black : Color.Gray;
            this.serviceTextBox.Enabled = notHive;
            this.sidTextBox.Enabled = notHive;
            this.sidTextBox.Text = "";

        }


        private void PortTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            this.portTextBox.ForeColor = Color.Black;
        }
    }
    public enum DatabaseDialogMode
    {
        Edit,
        New
    }
}
