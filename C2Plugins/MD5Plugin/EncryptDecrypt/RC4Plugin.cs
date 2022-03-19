using System;
using System.Text;

namespace MD5Plugin
{
    partial class RC4Plugin : Base64Plugin
    {

        public RC4Plugin()
        {
            InitializeComponent();
            InitializeControls();
            this.inputTextBox.Text = "请把你需要加密的内容粘贴在这里";
            this.outputTextBox.Text = "请把你需要解密的内容粘贴在这里";
        }



        public override void Encode(string EncryptStr)
        {
            string Key = textBoxEncryptionkey.Text;
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里")
            {
                ResetTextBox();
            }
            else
            {
                try
                {
                    byte[] pwdBytes = Encoding.UTF8.GetBytes(Key);
                    byte[] keyBytes = new byte[16];
                    int len = pwdBytes.Length;
                    if (len > keyBytes.Length) len = keyBytes.Length;
                    Array.Copy(pwdBytes, keyBytes, len);
 
                    byte[] plainText = GetEncodingBytes(EncryptStr);

                }
                catch (Exception ex)
                {
                    outputTextBox.Text = ex.Message;
                }
            }
        }

        public override void Decode(string DecryptStr)
        {
            string Key = textBoxEncryptionkey.Text;
            if (outputTextBox.Text == "请把你需要解密的内容粘贴在这里")
            {
                OriginOutput();
            }
            else
            {
                try
                {           

                    byte[] encryptedData = Convert.FromBase64String(DecryptStr);
                    byte[] pwdBytes = Encoding.UTF8.GetBytes(Key);
                    byte[] keyBytes = new byte[16];
                    Array.Copy(pwdBytes, keyBytes, Math.Min(16, pwdBytes.Length));

        
                }
                catch (Exception ex)
                {
                    inputTextBox.Text = ex.Message;
                }
            }
        }
    }
}
