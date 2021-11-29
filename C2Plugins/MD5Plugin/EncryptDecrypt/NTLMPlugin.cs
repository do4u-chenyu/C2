using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace MD5Plugin
{
    public partial class NTLMPlugin : CommonPlugin
    {
        public NTLMPlugin()
        {
            InitializeComponent();
        }

        public static class NTLM
        {
            public static HashAlgorithm MD4Singleton;
            static NTLM()
            {
                MD4Singleton = MD4.Create();
            }
            private static byte[] ComputeHash(string s)
            {
                return MD4Singleton.ComputeHash(Encoding.Unicode.GetBytes(s));
            }
            public static string ComputeHashHexString(string s)
            {
                return String.Join("", ComputeHash(s).Select(h => h.ToString("x2")));
            }
        }

        public override void encode(string str)
        {
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里")
            {
                ResetTextBox();
            }
            else
            {
                outputTextBox.Text = NTLM.ComputeHashHexString(str);
            }
        }
    }
}
