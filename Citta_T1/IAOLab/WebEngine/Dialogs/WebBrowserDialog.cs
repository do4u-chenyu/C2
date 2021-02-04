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
using C2.Controls;

namespace C2.IAOLab.WebEngine
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    partial class WebBrowserDialog :  StandardDialog
    {
        public WebBrowserDialog()
        {
            InitializeComponent();
        }

        private void WebBrowserDialog_Load(object sender, EventArgs e)
        {
            string str_url = Application.StartupPath + "\\StartMap.html";
            //Uri url = new Uri(str_url);
            //webBrowser1.Url = url;
            //webBrowser1.ObjectForScripting = this;
        }
    }
}
