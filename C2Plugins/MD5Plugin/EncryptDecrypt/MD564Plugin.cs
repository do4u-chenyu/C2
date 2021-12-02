using System;
using System.Text;
using System.Security.Cryptography;

namespace MD5Plugin
{
    public partial class MD564Plugin : CommonHashPlugin
    {
        public MD564Plugin() : base()
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
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(str)), 4, 8);
                t2 = t2.Replace("-", string.Empty);
                t2 = t2.ToLower();
                outputTextBox.Text = t2;
            }
        }
    }
}
