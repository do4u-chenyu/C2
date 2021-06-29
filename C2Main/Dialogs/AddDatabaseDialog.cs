using C2.Controls;
using C2.Controls.Left;
using C2.Core;
using C2.Database;
using C2.Model;
using C2.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Dialogs
{
    partial class AddDatabaseDialog : StandardDialog
    {
        public DatabaseItem DatabaseInfo { get; set; }
        private readonly DatabaseDialogMode Mode;
        private LinkButton LinkButton;
        public AddDatabaseDialog(DatabaseItem databaseInfo=null, DatabaseDialogMode mode=DatabaseDialogMode.New, LinkButton linkButton=null)
        {
            InitializeComponent();
            this.databaseTypeComboBox.SelectedIndex = 0;
            if (linkButton != null)
            {
                LinkButton = linkButton;
                this.databaseTypeComboBox.Enabled = false;
            }
            if (databaseInfo != null)
            {
                DatabaseInfo = databaseInfo;
                InitializeContent();
            }
            Mode = mode;
            
        }

        public void InitializeContent()
        {
            databaseTypeComboBox.SelectedIndex = (int)DatabaseInfo.Type-1;
            this.serverTextBox.Text = DatabaseInfo.Server ;
            this.sidRadiobutton.Checked = !DatabaseInfo.SID.IsNullOrEmpty();
            this.sidTextBox.Text = DatabaseInfo.SID;
            this.serviceRadiobutton.Checked = !DatabaseInfo.Service.IsNullOrEmpty() ;
            this.serviceTextBox.Text = DatabaseInfo.Service;
            this.portTextBox.Text = DatabaseInfo.Port;
            this.userTextBox.Text = DatabaseInfo.User;
            this.passwordTextBox.Text = DatabaseInfo.Password;
            this.schemaTextBox.Text = DatabaseInfo.Schema;
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
                this.passwordTextBox.Text,
                this.schemaTextBox.Text
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
            // hive用户名密码有空连接会失败

            if (this.databaseTypeComboBox.Text.Contains("Hive"))
            {
                this.userTextBox.Text = string.IsNullOrEmpty(this.userTextBox.Text) ? "None" : this.userTextBox.Text;
                this.passwordTextBox.Text = string.IsNullOrEmpty(this.passwordTextBox.Text) ? "None" : this.passwordTextBox.Text;
                return (string.IsNullOrEmpty(this.serverTextBox.Text) || string.IsNullOrEmpty(this.portTextBox.Text));
            }
            if (this.databaseTypeComboBox.Text.Contains("PostgreSQL"))
            {
                this.schemaTextBox.Text = string.IsNullOrEmpty(this.schemaTextBox.Text) ? "postgres" : this.schemaTextBox.Text;
                return (string.IsNullOrEmpty(this.serverTextBox.Text) || string.IsNullOrEmpty(this.portTextBox.Text));
            }

            return (databaseTypeComboBox.SelectedIndex == -1) || string.IsNullOrEmpty(this.serverTextBox.Text) || string.IsNullOrEmpty(this.portTextBox.Text) ||
                (this.sidRadiobutton.Checked ? string.IsNullOrEmpty(this.sidTextBox.Text) : string.IsNullOrEmpty(this.serviceTextBox.Text)) ||
                string.IsNullOrEmpty(this.userTextBox.Text) || string.IsNullOrEmpty(this.passwordTextBox.Text);
        }

        private void DatabaseTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            if (databaseTypeComboBox.SelectedItem == null)
                return;
            if(databaseTypeComboBox.SelectedItem.ToString().Contains( "Oracle"))
            {
                this.portTextBox.Text = "1521";
                this.portTextBox.ForeColor = Color.Black ;
                this.serviceTextBox.Enabled = true;
                this.sidTextBox.Enabled = true;
                this.schemaTextBox.Enabled = false;
                this.sidTextBox.Text = "orcl";
                this.schemaTextBox.Text = "";
                this.serviceRadiobutton.Enabled = true;
                this.sidRadiobutton.Enabled = true;
                this.schemaLabel.Visible = false;
                this.schemaTextBox.Visible = false;
                this.serviceRadiobutton.Visible = true;
                this.serviceTextBox.Visible = true;
                this.userTextBox.Text = "";
                this.passwordTextBox.Text = "";
            }

            if (databaseTypeComboBox.SelectedItem.ToString().Contains("PostgreSQL"))
            {
                this.portTextBox.Text = "5432" ;
                this.userTextBox.Text =  "postgres";
                this.passwordTextBox.Text =  "";
                //this.schemaTextBox.Text = "postgres";
                this.serviceTextBox.Enabled = false;
                this.sidTextBox.Enabled = false;
                this.schemaTextBox.Enabled = true;
                this.sidTextBox.Text = "";
                this.serviceRadiobutton.Enabled = false;
                this.sidRadiobutton.Enabled = false;
                this.schemaLabel.Visible = true;
                this.schemaTextBox.Visible = true;
                this.serviceRadiobutton.Visible = false;
                this.serviceTextBox.Visible = false;
            }
            
            if (databaseTypeComboBox.SelectedItem.ToString().Contains("Hive"))
            {
                this.portTextBox.Text = "10000";
                this.serverTextBox.Text = "10.1.126.4";
                this.userTextBox.Text = "None";
                this.passwordTextBox.Text = "None";
                this.schemaTextBox.Text = "";
                this.serviceTextBox.Enabled = false;
                this.sidTextBox.Enabled = false;
                this.schemaTextBox.Enabled = false;
                this.sidTextBox.Text = "";
                this.serviceRadiobutton.Enabled = false;
                this.sidRadiobutton.Enabled = false;
                this.serviceRadiobutton.Visible = true;
                this.serviceTextBox.Visible = true;
                this.schemaLabel.Visible = false;
                this.schemaTextBox.Visible = false;
            }

        }


        private void PortTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            this.portTextBox.ForeColor = Color.Black;
        }

        private void sidRadiobutton_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
    public enum DatabaseDialogMode
    {
        Edit,
        New
    }
}
