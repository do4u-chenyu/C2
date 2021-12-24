using C2.Controls;
using C2.Core;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool.SettingsDialog
{
    partial class UserTableProbe : StandardDialog
    {
        public string DBUser { get; set; } = string.Empty;
        public string DBPassword { get; set; } = string.Empty;
        public UserTableProbe()
        {
            InitializeComponent();
            this.dbUser.GotFocus += OnGotFocus;
            this.dbPassword.GotFocus += OnGotFocus;
        }

        private void OnGotFocus(object sender, System.EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.ForeColor == SystemColors.InactiveCaption)
            {
                tb.ForeColor = SystemColors.WindowText;
                tb.Clear();
            }
            
        }

        protected override bool OnOKButtonClick()
        {
            dbUser.Focus();
            dbPassword.Focus();

            DBUser = this.dbUser.Text.Trim();
            DBPassword = this.dbPassword.Text.Trim();
            if (DBUser.IsNullOrEmpty() || DBUser.IsNullOrEmpty())
            {
                DBUser = string.Empty;
                DBPassword = string.Empty;
            }
           
            return base.OnOKButtonClick();
        }


        
    }
}
