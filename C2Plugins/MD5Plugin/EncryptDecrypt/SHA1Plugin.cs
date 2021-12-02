using System;
using System.Text;
using System.Security.Cryptography;

namespace MD5Plugin
{
    public partial class SHA1Plugin : CommonHashPlugin
    {
        public SHA1Plugin() : base()
        {
            InitializeComponent();
        }
        public override void encode(string str)
        {
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里")
            {
                ResetTextBox();
            }
            else
            {
                byte[] strRes = Encoding.Default.GetBytes(str);
                HashAlgorithm iSha = new SHA1CryptoServiceProvider();
                strRes = iSha.ComputeHash(strRes);
                var enText = new StringBuilder();
                foreach (byte iByte in strRes)
                {
                    enText.AppendFormat("{0:x2}", iByte);
                }
                outputTextBox.Text = enText.ToString();
            }
        }
    }
}
