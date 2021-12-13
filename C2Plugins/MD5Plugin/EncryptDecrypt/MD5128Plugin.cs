using System.Security.Cryptography;
using System.Text;

namespace MD5Plugin
{
    public partial class MD5128Plugin : CommonHashPlugin
    {
        public MD5128Plugin() : base()
        {
            InitializeComponent();
        }

        public override void Encode(string str)
        {
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里")
            {
                ResetTextBox();
            } 
            else
            {
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                byte[] data = md5Hasher.ComputeHash(GetBytes(str));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                outputTextBox.Text = sBuilder.ToString();
            }
        }
    }
}
