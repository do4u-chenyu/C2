using C2.Controls;
using C2.Core;
using C2.Utils;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.WebShellTool.SettingsDialog
{
    partial class UserTableProbe : StandardDialog
    {
        public string DBUser { get; set; } = string.Empty;
        public string DBPassword { get; set; } = string.Empty;
        public UserTableProbe()
        {
            InitializeComponent();
        }

        protected override bool OnOKButtonClick()
        {
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
