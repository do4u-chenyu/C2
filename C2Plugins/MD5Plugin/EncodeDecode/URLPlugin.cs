using System;
using System.Drawing;
using System.Windows.Forms;
using System.Web;

namespace MD5Plugin
{
    partial class URLPlugin : CommonPlugin
    {
        public URLPlugin()
        {
            InitializeComponent();
            this.inputTextBox.Text = "请输入你要编码的内容";
            this.outputTextBox.Text = "请输入你要解码的内容";
        }

        public void OriginOutput()
        {
            inputTextBox.Text = string.Empty;
            outputTextBox.Text = string.Empty;
            MessageBox.Show("请输入解码内容", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public override void Encode(string str)
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

        public virtual void Decode(string url)
        {
            if (outputTextBox.Text == "请输入你要解码的内容")
            {
                OriginOutput();
            }
            else
            {
                inputTextBox.Text = Uri.UnescapeDataString(url);
            }
        }

        
        private void ButtonDecode_Click(object sender, EventArgs e)
        {
            inputTextBox.ForeColor = Color.Black;
            Decode(outputTextBox.Text);
        }
    }
}
