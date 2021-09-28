using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Controls;

namespace C2.Business.IAOLab.PostAndGet
{
    public partial class PostAndGetForm : BaseDialog
    {
        public PostAndGetForm()
        {
            InitializeComponent();
        }

        private void textbox_MouseDown(object sender, EventArgs e)
        {
            if (textBox.Text == "输入你测试的url")
            {
                textBox.Text = string.Empty;
            }
            textBox.ForeColor = Color.Black;
        }
       
        private void textbox_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "输入你测试的url";
                textBox.ForeColor = Color.Gray;
            }
        }


    }
}
