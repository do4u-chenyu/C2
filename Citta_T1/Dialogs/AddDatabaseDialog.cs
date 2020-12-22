using C2.Controls;
using C2.Model;
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
        public DatabaseItem DatabaseInfo { get; }
        public AddDatabaseDialog()
        {
            InitializeComponent();
        }

        protected override bool OnOKButtonClick()
        {
            //判断输入框是否有空值
            InputHasEmpty();

            //必填项都有值时给batabseinfo赋值
            DatabaseInfo.Type = (DatabaseType)this.databaseTypeComboBox.SelectedIndex;
            DatabaseInfo.Server = this.serverTextBox.Text;
            DatabaseInfo.Service = this.serviceTextBox.Text;
            DatabaseInfo.Port = this.portTextBox.Text;
            DatabaseInfo.User = this.userTextBox.Text;
            DatabaseInfo.Password = this.passwordTextBox.Text;

            return base.OnOKButtonClick();
        }
        
        private bool InputHasEmpty()
        {


            return false;
        }
    }
}
