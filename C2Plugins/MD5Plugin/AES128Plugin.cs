using System;
using System.Text;
using System.Security.Cryptography;

namespace MD5Plugin
{
    public partial class AES128Plugin : Base64Plugin
    {
        public AES128Plugin()
        {
            InitializeComponent();
            InitializeControls();
            this.inputTextBox.Text = "请把你需要加密的内容粘贴在这里";
            this.outputTextBox.Text = "请把你需要解密的内容粘贴在这里";

        }
        private void InitializeControls()
        {
            inputTextBox.Select(inputTextBox.TextLength, 0);
            inputTextBox.Select(0, 0);
            encodingComboBox.SelectedIndex = 0;
        }

        public override void encode(string EncryptStr)
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
                    RijndaelManaged rijndaelCipher = new RijndaelManaged();
                    rijndaelCipher.Mode = CipherMode.ECB;
                    rijndaelCipher.Padding = PaddingMode.Zeros;
                    rijndaelCipher.BlockSize = 128;
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
       
        public override void decode(string DecryptStr)
        {
            string Key = textBoxEncryptionkey.Text;
            if (outputTextBox.Text == "请把你需要解密的内容粘贴在这里")
            {
                originOutput();
            }
            else 
            {
                try
                {
                    RijndaelManaged rijndaelCipher = new RijndaelManaged();
                    rijndaelCipher.Mode = CipherMode.ECB;
                    rijndaelCipher.Padding = PaddingMode.Zeros;
                    rijndaelCipher.BlockSize = 128;
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
