using System;
using System.Drawing;
using System.Windows.Forms;

namespace MD5Plugin
{
    public partial class CommonPlugin : UserControl
    {
        public CommonPlugin()
        {
            InitializeComponent();
        }

        public void ResetTextBox()
        {
            inputTextBox.Text = string.Empty;
            outputTextBox.Text = string.Empty;
            MessageBox.Show("请输入加密内容", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public virtual void encode(string str)
        {

        }

        
        public void buttonEncode_Click(object sender, EventArgs e)
        {
            encode(inputTextBox.Text);
            outputTextBox.ForeColor = Color.Black;
        }
        
    
        public void InputTextBox_MouseDown(object sender, EventArgs e)
        {
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里" || inputTextBox.Text == "请输入你要用Base64加密的内容" || inputTextBox.Text == "请输入你要编码的Url" || inputTextBox.Text == "请输入你要编码的内容" || inputTextBox.Text == "请输入你要编码的内容或者需要加密文件的路径")
            {
                inputTextBox.Text = string.Empty;
            }
            inputTextBox.ForeColor = Color.Black;
        }

        public void OutputTextBox_MouseDown(object sender, EventArgs e)
        {
            if (outputTextBox.Text == "加密后的结果" || outputTextBox.Text == "请输入你要用Base64解密的内容" || outputTextBox.Text == "请输入你要解码的Url" || outputTextBox.Text == "请输入你要解码的内容"|| outputTextBox.Text == "请把你需要解密的内容粘贴在这里")
            {
                outputTextBox.Text = string.Empty;
            }
            outputTextBox.ForeColor = Color.Black;
        }
    }
}
