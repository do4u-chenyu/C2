using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public partial class DetailsPageForm : Form
    {
        public string Content;
        public DetailsPageForm(string content)
        {
            InitializeComponent();
            this.Content = content;
            this.comboBox1.SelectedIndex = 0;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBox1.SelectedItem.ToString() == "GB2312")
                this.richTextBox1.Text = Content;
            else
            {
                byte[] getBt = Encoding.GetEncoding("GB2312").GetBytes(Content);
                this.richTextBox1.Text = Encoding.GetEncoding("utf-8").GetString(getBt);
            }
        }
    }
}
