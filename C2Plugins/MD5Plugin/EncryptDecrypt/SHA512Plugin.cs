using System.Security.Cryptography;
using System.Text;

namespace MD5Plugin
{
    public partial class SHA512Plugin : CommonHashPlugin
    {
        public SHA512Plugin() : base()
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
                byte[] bytValue = GetBytes(str);
                SHA512 sha512 = new SHA512CryptoServiceProvider();
                byte[] retVal = sha512.ComputeHash(bytValue);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                outputTextBox.Text = sb.ToString();
            }
        }
    }
}
