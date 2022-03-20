﻿using MD5Plugin.SSEncryption;
using System;
using System.Text;

namespace MD5Plugin
{
    partial class RC4Plugin : Base64Plugin
    {
        public RC4Plugin()
        {
            InitializeComponent();
            InitializeRC4Plugin();
            this.inputTextBox.Text = "请把你需要加密的内容粘贴在这里";
            this.outputTextBox.Text = "请把你需要解密的内容粘贴在这里";
        }

        public void InitializeRC4Plugin()
        {
            encodingComboBox.Items.Clear();
            encodingComboBox.Items.Add("文本");
            encodingComboBox.Items.Add("HEX");
            encodingComboBox.SelectedIndex = 0;
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

                    byte[] plainBytes = new byte[0];
                    switch (encodingComboBox.SelectedIndex)
                    {
                        case 0:        // 文本
                            plainBytes = Encoding.UTF8.GetBytes(plainText);
                            break;
                        case 1:        // HEX
                            plainBytes = Utils.HexStringToBytes(plainText);
                            break;
                        default:
                            break;
                    }

                    byte[] passwordBytes = Encoding.UTF8.GetBytes(textBoxEncryptionkey.Text);
                    byte[] retBytes = RC4.Apply(plainBytes, passwordBytes);

                    outputTextBox.Text = Utils.BytesToHexString(retBytes);

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
