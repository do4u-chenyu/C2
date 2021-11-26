using System;
using System.IO;
using System.Text.RegularExpressions;
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
                return;
            FileStream fs = new FileStream(ClientSetting.UnlockFilePath, FileMode.Create, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write("ID");
            sw.Flush();
            sw.Close();
            this.DialogResult = DialogResult.OK;
        }

        private bool IsID(string ID) 
        {
            return Regex.IsMatch(ID, @"^[X]\d{4}|[x]\d{4}$"); ;
        }
    }
}
