using C2.Controls;
using C2.Utils;
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
    partial class MessageDialog : StandardDialog
    {
        public MessageDialog(string msg)
        {
            InitializeComponent();
            InitializeOther(msg);
        }


        private void InitializeOther(string msg)
        {
            this.OKButton.Size = new Size(75, 27);
            this.CancelBtn.Size = new Size(75, 27);
            this.ApplyButton.Size = new Size(75, 27);
            this.ApplyButton.Text = "复制";
            this.textBox1.Text = msg;
        }

        protected override bool OnApplyButtonClick()
        {
            FileUtil.TryClipboardSetText(this.textBox1.Text);
            HelpUtil.ShowMessageBox("信息已复制到剪切板");
            return false;
        }
    }
}
