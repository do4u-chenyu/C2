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
                string plainText = inputTextBox.Text.Trim();
                //string password = textBoxEncryptionkey.Text.Trim();
                try
                {
                    
                    byte[] plainBytes = Utils.HexStringToBytes(plainText);

                    outputTextBox.Text = Utils.BytesToHexString(plainBytes);

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



        
                }
                catch (Exception ex)
                {
                    inputTextBox.Text = ex.Message;
                }
            }
        }
    }
}
