using C2.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs
{  
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    partial class ConnectUrlDialog : StandardDialog
    {
        public ConnectUrlDialog()
        {
            InitializeComponent();
        }

        private void ConnectUrlDialog_Load(object sender, EventArgs e)
        {
            string str_url = Application.StartupPath + "\\StartMap.html";
            Uri url = new Uri(str_url);
            webBrowser1.Url = url;
            webBrowser1.ObjectForScripting = this;
        }
    }
}
