using System;
using System.IO;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class FunctionUnlockForm :  Form
    {
        public FunctionUnlockForm()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            string ID = IDBox.Text;
            if (!IsID(ID))
            {
                MessageBox.Show("请正确输入工号", "WORNING");
                return;
            }
            if (File.Exists(ClientSetting.UnlockFilePath)) 
            { 
            }
            
        }

        private bool IsID(string ID) 
        {
            return false;
        }
    }
}
