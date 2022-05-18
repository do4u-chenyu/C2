using C2.Log;
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

        protected override string EncodeLine(string str)
        {
            new Log().LogManualButton("MD5(128位)", "02");
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }

    }
}
