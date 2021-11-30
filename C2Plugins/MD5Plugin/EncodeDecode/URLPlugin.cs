using System;
using System.Drawing;
using System.Windows.Forms;
using System.Web;

namespace MD5Plugin
{
    public partial class URLPlugin : CommonPlugin
    {
        public URLPlugin()
        {
            InitializeComponent();
            this.inputTextBox.Text = "请输入你要编码的内容";
            this.outputTextBox.Text = "请输入你要解码的内容";
        }

        public void originOutput()
        {
            inputTextBox.Text = string.Empty;
            outputTextBox.Text = string.Empty;
            MessageBox.Show("请输入解码内容", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public override void encode(string str)
        {
            if (inputTextBox.Text == "请输入你要编码的内容")
            {
                ResetTextBox();
            }
            else
            {
                outputTextBox.Text = HttpUtility.UrlEncode(str);
            }
        }

        public virtual void decode(string url)
        {
            if (outputTextBox.Text == "请输入你要解码的内容")
            {
                originOutput();
            }
            else
            {
                inputTextBox.Text = Uri.UnescapeDataString(url);
            }
        }

        
        private void buttonDecode_Click(object sender, EventArgs e)
        {
            inputTextBox.ForeColor = Color.Black;
            decode(outputTextBox.Text);
        }
    }
}
