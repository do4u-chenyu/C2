using C2.Controls;
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
        public DatabaseObject DatabaseInfo { get; }
        public AddDatabaseDialog()
        {
            InitializeComponent();
            DatabaseInfo = new DatabaseObject();
        }

        private void DatabaseTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseInfo.DatabaseType = this.databaseTypeComboBox.Text;
        }

        private void ServerTextBox_TextChanged(object sender, EventArgs e)
        {
            DatabaseInfo.Server = this.serverTextBox.Text;
        }

        private void ServiceTextBox_TextChanged(object sender, EventArgs e)
        {
            DatabaseInfo.Service = this.serviceTextBox.Text;
        }

        private void PortTextBox_TextChanged(object sender, EventArgs e)
        {
            DatabaseInfo.Port = this.portTextBox.Text;
        }

        private void UserTextBox_TextChanged(object sender, EventArgs e)
        {
            DatabaseInfo.User = this.userTextBox.Text;
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            DatabaseInfo.Password = this.passwordTextBox.Text;
        }
    }

    class DatabaseObject
    {
        public string DatabaseType { set; get; }
        public string Server { set; get; }
        public string Service { set; get; }
        public string Port { set; get; }
        public string User { set; get; }
        public string Password { set; get; }

        public DatabaseObject()
        {

        }
    }
}
