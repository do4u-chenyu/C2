using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Controls;
using C2.Core;
using C2.Utils;

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
