using C2.Utils;
using System;
using System.Windows.Forms;

namespace C2.Dialogs.IAOLab
{
    public partial class FraudDialog : Form
    {
        public FraudDialog()
        {
            InitializeComponent();
        }

        private void FraudDialog_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ProcessUtil.GetChromePath()))
            {
                string chromePath = ProcessUtil.GetChromePath();
                System.Diagnostics.Process.Start(chromePath, "http://103.43.17.9:8080/view/index.html");
                this.Close();
            }
            else
            {
                this.webBrowser1.Url = new System.Uri("http://103.43.17.9:8080/view/index.html", System.UriKind.Absolute);
            }
        }
    }
}
