using System;
using System.Text;
using System.Security.Cryptography;

namespace MD5Plugin
{
    partial class AES128Plugin : Base64Plugin
    {
        public RijndaelManaged rijndaelCipher = new RijndaelManaged();
        public AES128Plugin()
        {
            InitializeComponent();
            InitializeControls();
            this.inputTextBox.Text = "请把你需要加密的内容粘贴在这里";
            this.outputTextBox.Text = "请把你需要解密的内容粘贴在这里";
        }

        public void Setting(RijndaelManaged rijndaelCipher)
        {
            rijndaelCipher.Mode = CipherMode.ECB;
            rijndaelCipher.Padding = PaddingMode.Zeros;
            rijndaelCipher.BlockSize = 128;
        }

        public override void Encode(string EncryptStr)
        {
            string Key = textBoxEncryptionkey.Text;
            string iv = string.Empty;
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里")
            {
                ResetTextBox();
            }
            else
            {
                try
                {
                    Setting(rijndaelCipher);
                    byte[] pwdBytes = Encoding.UTF8.GetBytes(Key);
                    byte[] keyBytes = new byte[16];
                    int len = pwdBytes.Length;
                    if (len > keyBytes.Length) len = keyBytes.Length;
                    Array.Copy(pwdBytes, keyBytes, len);
                    rijndaelCipher.Key = keyBytes;
                    byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
                    rijndaelCipher.IV = new byte[16];
                    ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
                    byte[] plainText = GetEncodingBytes(EncryptStr);
                    byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
                    outputTextBox.Text = Convert.ToBase64String(cipherBytes);
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
                    Setting(rijndaelCipher);
                    byte[] encryptedData = Convert.FromBase64String(DecryptStr);
                    byte[] pwdBytes = Encoding.UTF8.GetBytes(Key);
                    byte[] keyBytes = new byte[16];
                    int len = pwdBytes.Length;
                    if (len > keyBytes.Length) len = keyBytes.Length;
                    Array.Copy(pwdBytes, keyBytes, len);
                    rijndaelCipher.Key = keyBytes;
                    ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
                    byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                    inputTextBox.Text = GetDecodingString(plainText);
                }
                catch (Exception ex)
                {
                    inputTextBox.Text = ex.Message;
                }
            }
        }
    }
}
