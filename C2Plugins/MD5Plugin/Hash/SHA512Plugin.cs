using C2.Log;
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


        protected override string EncodeLine(string str)
        {
            new Log().LogManualButton("SHA-512", "运行");
            byte[] bytValue = GetBytes(str);
            SHA512 sha512 = new SHA512CryptoServiceProvider();
            byte[] retVal = sha512.ComputeHash(bytValue);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
                sb.Append(retVal[i].ToString("x2"));
  
            return sb.ToString();
        }
    }
}
