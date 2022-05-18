using C2.Log;
using System;
using System.Security.Cryptography;

namespace MD5Plugin
{
    public partial class MD564Plugin : CommonHashPlugin
    {
        public MD564Plugin() : base()
        {
            InitializeComponent();
        }

        protected override string EncodeLine(string str)
        {
            new Log().LogManualButton("MD5(64位)", "02");
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(GetBytes(str)), 4, 8);
            return t2.Replace("-", string.Empty).ToLower();
        }
    }
}
