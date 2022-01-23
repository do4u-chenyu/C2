using System.Security.Cryptography;
using System.Text;

namespace MD5Plugin
{
    public partial class SHA1Plugin : CommonHashPlugin
    {
        public SHA1Plugin() : base()
        {
            InitializeComponent();
        }

        protected override string EncodeLine(string str)
        {
            byte[] strRes = GetBytes(str);
            HashAlgorithm iSha = new SHA1CryptoServiceProvider();
            strRes = iSha.ComputeHash(strRes);
            StringBuilder sb = new StringBuilder();
            foreach (byte iByte in strRes)
                sb.AppendFormat("{0:x2}", iByte);
            return sb.ToString();
        }
    }
}
