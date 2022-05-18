using System;
using System.Text;
using System.Security.Cryptography;
using C2.Log;

namespace MD5Plugin
{
    partial class AES128Plugin : Base64Plugin
    {
        public RijndaelManaged rijndaelCipher = new RijndaelManaged();
        string EncryMode = "ECB";
        string paddingMode = "Zeros";
        string dataBlockMode = "128位";
        public AES128Plugin()
        {
            InitializeComponent();
            InitializeControls();
            EncryModeComboBox.SelectedIndex = 0;//加密模式
            PaddingcomboBox.SelectedIndex = 0;//填充
            DataBlockComboBox.SelectedIndex = 0;//数据块
            this.inputTextBox.Text = "请把你需要加密的内容粘贴在这里";
            this.outputTextBox.Text = "请把你需要解密的内容粘贴在这里";
        }

        private void Encry_SelectedIndexChanged(object sender, EventArgs e)
        {
            EncryMode = EncryModeComboBox.SelectedItem as string;
        }
        private void Padding_SelectedIndexChanged(object sender, EventArgs e)
        {
            paddingMode = PaddingcomboBox.SelectedItem as string;
        }
        private void DataBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataBlockMode = DataBlockComboBox.SelectedItem as string;
        }

        public void Setting(RijndaelManaged rijndaelCipher)
        {
            if (IvtextBox.Text != string.Empty)
                rijndaelCipher.IV = Encoding.UTF8.GetBytes(IvtextBox.Text);

            if (EncryMode == "ECB")
                rijndaelCipher.Mode = CipherMode.ECB;
            else if (EncryMode == "CBC")
                rijndaelCipher.Mode = CipherMode.CBC;

            if (paddingMode == "Zeros")
                rijndaelCipher.Padding = PaddingMode.Zeros;
            else if (paddingMode == "None")
                rijndaelCipher.Padding = PaddingMode.None;
            else if (paddingMode == "PKCS7")
                rijndaelCipher.Padding = PaddingMode.PKCS7;
            else if (paddingMode == "ANSIX923")
                rijndaelCipher.Padding = PaddingMode.ANSIX923;
            else if (paddingMode == "ISO10126")
                rijndaelCipher.Padding = PaddingMode.ISO10126;


            if (dataBlockMode == "128位")
                rijndaelCipher.BlockSize = 128;
            else if (dataBlockMode == "192位")
                rijndaelCipher.BlockSize = 192;
            else if (dataBlockMode == "256位")
                rijndaelCipher.BlockSize = 256;
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
                    Setting(rijndaelCipher);
                    byte[] pwdBytes = Encoding.UTF8.GetBytes(Key);
                    byte[] keyBytes = new byte[16];
                    int len = pwdBytes.Length;
                    if (len > keyBytes.Length) len = keyBytes.Length;
                    Array.Copy(pwdBytes, keyBytes, len);
                    rijndaelCipher.Key = keyBytes;

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
                    Array.Copy(pwdBytes, keyBytes, Math.Min(16, pwdBytes.Length));
                    rijndaelCipher.Key = keyBytes;
                    //rijndaelCipher.Key = Convert.FromBase64String(Key); 

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
