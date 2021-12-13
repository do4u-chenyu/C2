﻿using System.Security.Cryptography;
using System.Text;

namespace MD5Plugin
{
    public partial class SHA256Plugin : CommonHashPlugin
    {
        public SHA256Plugin() : base()
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
                byte[] bytValue = GetBytes(str);
                SHA256 sha256 = new SHA256CryptoServiceProvider();
                byte[] retVal = sha256.ComputeHash(bytValue);
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
