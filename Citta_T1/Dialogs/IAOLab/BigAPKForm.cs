using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.IAOLab
{
    public partial class BigAPKForm : Form
    {
        public BigAPKForm()
        {
            InitializeComponent();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void BigAPKForm_Load(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new System.Uri(" http://113.31.110.244:5147/APK/login/", System.UriKind.Absolute);
        }
    }
}
