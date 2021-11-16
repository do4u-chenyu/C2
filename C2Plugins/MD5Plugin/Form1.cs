using C2.IAOLab.Plugins;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace MD5Plugin
{
    public partial class Form1 : Form, IPlugin
    {
        string encodingType = "UTF-8";
        string splitType = "无分隔符";
        string radixType = "十六进制";

        //Base64解密
        string outPath;

        public string EncodingType { 
            get 
            { 
                return encodingType; 
            }
            set => encodingType = value; }

        public Form1()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            inputTextBox.Select(inputTextBox.TextLength, 0);
            inputTextBox.Select(0, 0);
            splitComboBox.SelectedIndex = 0;
            radixComboBox.SelectedIndex = 0;
            encodingComboBox.SelectedIndex = 0;
        }

        public static String TryGetSysTempDir()
        {
            String tempDir;
            try
            {
                tempDir = Path.GetTempPath();
            }
            catch (System.Security.SecurityException)
            {
                tempDir = String.Empty;
            }
            return tempDir;
        }

        public string GetPluginDescription()
        {
            return "将字符串进行常用的加密、解密、编码和解码操作；如MD5加密，Base64，Url编码和解码，UTF8和GBK转码等。";
        }

        public Image GetPluginImage()
        {
            return this.Icon.ToBitmap();
        }

        public string GetPluginName()
        {
            return "MD5加密";
        }

        public string GetPluginVersion()
        {
            return "0.0.3";
        }

        public DialogResult ShowFormDialog()
        {
            return this.ShowDialog();
        }

        private void SetDefaultEncrypFormat()
        {
            inputTextBox.Text = "请把你需要加密的内容粘贴在这里";
            outputTextBox.Text = "加密后的结果";
            inputTextBox.ForeColor = Color.DarkGray;
            outputTextBox.ForeColor = Color.DarkGray;

            encodeButton.Text = "加密 =>";
            decodeButton.Visible = false;
            encodingComboBox.Visible = false;
            splitComboBox.Visible = false;
            radixComboBox.Visible = false;
            labelEncryptionkey.Visible = false;
            textBoxEncryptionkey.Visible = false;
        }
        private void SetDefault2()
        {
            inputTextBox.Text = "请输入你要编码的内容";
            outputTextBox.Text = "请输入你要解码的内容";
            inputTextBox.ForeColor = Color.DarkGray;
            outputTextBox.ForeColor = Color.DarkGray;
        }

        private void SetDefault3()
        {
            inputTextBox.Text = "请输入你要编码的内容或者需要加密文件的路径";
            outputTextBox.Text = "请输入你要解码的内容";
            inputTextBox.ForeColor = Color.DarkGray;
            outputTextBox.ForeColor = Color.DarkGray;
        }

        //Base64
        private void Base64_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            encodeButton.Text = "编码 =>";
            decodeButton.Text = "<= 解码";
            encodeButton.Visible = true;
            decodeButton.Visible = true;
            encodingComboBox.Visible = true;
            splitComboBox.Visible = false;
            radixComboBox.Visible = false;
            labelEncryptionkey.Visible = false;
            textBoxEncryptionkey.Visible = false;
            SetDefault3();
        }

        //url编解码
        private void Url_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            encodeButton.Text = "编码 =>";
            decodeButton.Text = "<= 解码";
            encodeButton.Visible = true;
            decodeButton.Visible = true;
            encodingComboBox.Visible = false;
            splitComboBox.Visible = false;
            radixComboBox.Visible = false;
            labelEncryptionkey.Visible = false;
            textBoxEncryptionkey.Visible = false;
            SetDefault2();
        }

        //Unicode编解码
        private void Unicode_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            encodeButton.Text = "编码 =>";
            decodeButton.Text = "<= 解码";
            decodeButton.Visible = true;
            encodeButton.Visible = true;
            encodingComboBox.Visible = false;
            splitComboBox.Visible = false;
            radixComboBox.Visible = false;
            labelEncryptionkey.Visible = false;
            textBoxEncryptionkey.Visible = false;
            SetDefault2();
        }
        //Hex编解码
        private void HEX_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            encodeButton.Text = "编码 =>";
            decodeButton.Text = "<= 解码";
            decodeButton.Visible = true;
            encodeButton.Visible = true; 
            encodingComboBox.Visible = true;
            splitComboBox.Visible = true;
            radixComboBox.Visible = true;
            labelEncryptionkey.Visible = false;
            textBoxEncryptionkey.Visible = false;
            SetDefault2();
        }
        // ASE128解密
        private void ASE128_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            encodeButton.Text = "加密 =>";
            decodeButton.Text = "<= 解密";
            decodeButton.Visible = true;
            encodeButton.Visible = true;
            encodingComboBox.Visible = true;
            splitComboBox.Visible = false;
            radixComboBox.Visible = false;
            labelEncryptionkey.Visible = true;
            textBoxEncryptionkey.Visible = true;
            SetDefault2();
        }

        //md5(128位)
        private void MD5_128_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetDefaultEncrypFormat();
        }
        //md5(64位)
        private void MD5_64_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetDefaultEncrypFormat();
        }
        //使用sha1对字符串进行加密
        private void SHA1_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetDefaultEncrypFormat();
        }

        //使用sha256对字符串进行加密
        private void SHA256_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetDefaultEncrypFormat();
        }
        //使用sha512对字符串进行加密
        private void Sha512_RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetDefaultEncrypFormat();
        }

        private void InputTextBox_MouseDown(object sender, EventArgs e)
        {
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里" || inputTextBox.Text == "请输入你要用Base64加密的内容" || inputTextBox.Text == "请输入你要编码的Url" || inputTextBox.Text == "请输入你要编码的内容" || inputTextBox.Text == "请输入你要编码的内容或者需要加密文件的路径")
            {
                inputTextBox.Text = string.Empty;
            }
            inputTextBox.ForeColor = Color.Black;
        }

        private void OutputTextBox_MouseDown(object sender, EventArgs e)
        {
            if (outputTextBox.Text == "加密后的结果" || outputTextBox.Text == "请输入你要用Base64解密的内容" || outputTextBox.Text == "请输入你要解码的Url" || outputTextBox.Text == "请输入你要解码的内容")
            {
                outputTextBox.Text = string.Empty;
            }
            outputTextBox.ForeColor = Color.Black;
        }
        public void Md5Code_128(string str)
        {
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里")
            {
                ResetTextBox();
            }
            else
            {
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                byte[] data = md5Hasher.ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(str));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));

                }
                outputTextBox.Text = sBuilder.ToString();
            }
        }
        public void Md5Code_64(string str)
        {
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里")
            {
                ResetTextBox();
            }
            else
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(str)), 4, 8);
                t2 = t2.Replace("-", "");
                t2 = t2.ToLower();
                outputTextBox.Text = t2;
            }
        }
        public void ResetTextBox()
        {
            inputTextBox.Text = string.Empty;
            outputTextBox.Text = string.Empty;
            MessageBox.Show("请输入编码内容", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //Base64编码：如果输入路径存在则执行文件编码，否则执行文本编码
        public void EncodeBase64(string filePath)
        {
            if (inputTextBox.Text == "请输入你要编码的内容或者需要加密文件的路径")
            {
                ResetTextBox();
            }
            else if (File.Exists(filePath))
            {
                string base64Str = string.Empty;
                using (FileStream filestream = new FileStream(filePath, FileMode.Open))
                {
                    byte[] bt = new byte[filestream.Length];
                    //调用read读取方法
                    filestream.Read(bt, 0, bt.Length);
                    base64Str = Convert.ToBase64String(bt);
                    filestream.Close();
                }
                outputTextBox.Text = base64Str;
            }
            else
            {
                byte[] bytes = GetEncodingBytes(filePath);
                outputTextBox.Text = Convert.ToBase64String(bytes);
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        public void UrlEncode(string url)
        {
            if (inputTextBox.Text == "请输入你要编码的内容")
            {
                ResetTextBox();
            }
            else
            {
                outputTextBox.Text = HttpUtility.UrlEncode(url);
            }
        }
        public void UnicodeChineseEncode(string str)
        {
            if (inputTextBox.Text == "请输入你要编码的内容")
            {
                inputTextBox.Text = string.Empty;
                outputTextBox.Text = string.Empty;
                MessageBox.Show("请输入编码内容", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                byte[] bytes = Encoding.Unicode.GetBytes(str);
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i += 2)
                {
                    stringBuilder.AppendFormat("u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
                }
                stringBuilder = stringBuilder.Replace(@"u", @"\u");

                outputTextBox.Text = stringBuilder.ToString();
            }
        }
        public void HexEncode(string str)
        {
            if (inputTextBox.Text == "请输入你要编码的内容")
            {
                inputTextBox.Text = string.Empty;
                outputTextBox.Text = string.Empty;
                MessageBox.Show("请输入编码内容", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                byte[] arrByte = GetEncodingBytes(str);

                string sep = splitType == "无分隔符" ? string.Empty : splitType.Trim();
                int radix = 16;
                if (radixType == "十进制")
                    radix = 10;
                if (radixType == "八进制")
                    radix = 8;

                for (int i = 0; i < arrByte.Length; i++)
                {
                    sb.Append(sep + Convert.ToString(arrByte[i], radix));
                }
                outputTextBox.Text = sb.ToString();
            }
        }

        private byte[] GetEncodingBytes(string str)
        {
            // 编码时不应该选择HEX, 如果选了默认为UTF-8
            EncodingType = EncodingType == "HEX" ? "UTF-8" : EncodingType;
            return Encoding.GetEncoding(EncodingType).GetBytes(str);
        }

        private string GetDecodingString(byte[] bytes)
        {
            if (EncodingType == "HEX")
                return BitConverter.ToString(bytes).Replace("-", string.Empty);
            return Encoding.GetEncoding(EncodingType).GetString(bytes);
        }


        public void AES128Encode(string EncryptStr, string Key,string iv)
        {
            if (inputTextBox.Text == "请输入你要编码的内容")
            {
                inputTextBox.Text = string.Empty;
                outputTextBox.Text = string.Empty;
                MessageBox.Show("请输入编码内容", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EncodingType = encodingComboBox.SelectedItem as string;
        }

        private void Split_SelectedIndexChanged(object sender, EventArgs e)
        {
            splitType = splitComboBox.SelectedItem as string;
        }

        private void RadixComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            radixType = radixComboBox.SelectedItem as string;
        }

        public void SHA1Encrypt(string str)
        {
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里")
            {
                ResetTextBox();
            }
            else
            {
                var strRes = Encoding.Default.GetBytes(str);
                HashAlgorithm iSha = new SHA1CryptoServiceProvider();
                strRes = iSha.ComputeHash(strRes);
                var enText = new StringBuilder();
                foreach (byte iByte in strRes)
                {
                    enText.AppendFormat("{0:x2}", iByte);
                }
                outputTextBox.Text = enText.ToString();
            }
        }
        public void SHA256Encrypt(string str)
        {
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里")
            {
                ResetTextBox();
            }
            else
            {
                byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(str);
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

        public void SHA512Encrypt(string str)
        {
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里")
            {
                ResetTextBox();
            }
            else
            {
                byte[] bytValue = Encoding.UTF8.GetBytes(str);
                SHA512 sha512 = new SHA512CryptoServiceProvider();
                byte[] retVal = sha512.ComputeHash(bytValue);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                outputTextBox.Text = sb.ToString();
            }
        }


        public void NTLMEncrypt(string str)
        {
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里")
            {
                ResetTextBox();
            }
            else
            {
                outputTextBox.Text = NTLM.ComputeHashHexString(str);
            }
        }
        //编码或者加密功能
        private void EncodeButton_Click(object sender, EventArgs e)
        {
            if (md5128RadioButton.Checked)
            {
                Md5Code_128(inputTextBox.Text);
            }
            else if (md564RadioButton.Checked)
            {
                Md5Code_64(inputTextBox.Text);
            }
            else if (base64RadioButton.Checked)
            {
                EncodeBase64(inputTextBox.Text);
            }
            else if (urlRadioButton.Checked)
            {
                UrlEncode(inputTextBox.Text);
            }
            else if (unicodeRadioButton.Checked)
            {
                UnicodeChineseEncode(inputTextBox.Text);
            }
            else if (hexRadioButton.Checked)
            {
                HexEncode(inputTextBox.Text);
            }
            else if (ASE128RadioButton.Checked)
            {
                AES128Encode(inputTextBox.Text,textBoxEncryptionkey.Text,string.Empty);
            }
            else if (sha1RadioButton.Checked)
            {
                SHA1Encrypt(inputTextBox.Text);
            }
            else if (sha256RadioButton.Checked)
            {
                SHA256Encrypt(inputTextBox.Text);
            }
            else if (sha512RadioButton.Checked)
            {
                SHA512Encrypt(inputTextBox.Text);
            }
            else if (NTLMRadioButton.Checked)
            {
                NTLMEncrypt(inputTextBox.Text);
            }
            else
            {
                EncodeBase64(inputTextBox.Text);
            }
            outputTextBox.ForeColor = Color.Black;
        }


        //解码功能
        private void DecodeButton_Click(object sender, EventArgs e)
        {
            inputTextBox.ForeColor = Color.Black;
            if (base64RadioButton.Checked)
            {
                DecodeBase64(outputTextBox.Text);
            }
            else if (urlRadioButton.Checked)
            {
                UrlDecode(outputTextBox.Text);
            }
            else if (unicodeRadioButton.Checked)
            {
                UnicodeChineseDecode(outputTextBox.Text);
            }
            else if(hexRadioButton.Checked)
                HexDecode(outputTextBox.Text);
            else if (ASE128RadioButton.Checked)
                AES128Decode(outputTextBox.Text,textBoxEncryptionkey.Text);
            else
            {
                DecodeBase64(outputTextBox.Text);
            }
        }

        public void originOutput()
        {
            inputTextBox.Text = string.Empty;
            outputTextBox.Text = string.Empty;
            MessageBox.Show("请输入解码内容", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void DecodeBase64(string base64Str)
        {
            //DateTime dateTime = DateTime.Now;
            Dictionary<string, string> pairs = new Dictionary<string, string>();
            pairs.Add("/9j/", "jpg");
            pairs.Add("iVBORw", "png");
            pairs.Add("Qk", "bmp");
            pairs.Add("R0lGOD", "gif");
            pairs.Add("UEsDBB", "zip");
            pairs.Add("UEsDBA", "zip");
            pairs.Add("UmFyIR", "rar");
            pairs.Add("N3q8rycc", "7z");

            // base64解码前先进行url解码,反复3次
            // HttpUtility里的urldecode方法会把+号变成空格, 这个不是标准解法, 采用Uri.UnescapeDataString代替
            base64Str = Uri.UnescapeDataString(Uri.UnescapeDataString(Uri.UnescapeDataString(base64Str)));

            foreach (string key in pairs.Keys)
            {
                if (outputTextBox.Text == "加密后的结果" || outputTextBox.Text == "请输入你要解码的内容")
                {
                    originOutput();
                    break;
                }         
                else if (base64Str.StartsWith(key))
                {
                    string value;
                    pairs.TryGetValue(key, out value);
                    //outPath = string.Format(TryGetSysTempDir() + "{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}.{6:D2}", dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, value);
                    Base64StrToFile(base64Str,value);
                    //inputTextBox.Text = outputTextBox.Text != string.Empty ? "文件解析地址为:" + outPath : string.Empty;
                    break;
                }
                else if (IsBase64Formatted(base64Str))
                {
                    byte[] bytes = Convert.FromBase64String(base64Str);
                    inputTextBox.Text = GetDecodingString(bytes);
                }
                else
                {
                    int baseLengh = base64Str.Length;
                    int i;
                    for (i = 0; i < baseLengh; i++)
                    {
                        base64Str = base64Str.Substring(0, baseLengh-i);
                        if (DecodeNewBase64(base64Str))
                            break;
                    }
                    if (i == baseLengh)
                    {
                        inputTextBox.Text = string.Empty;
                        MessageBox.Show("目前仅支持/字符串/.jpg/.png/.gif/.bmp/.zip/.rar/.7z文件的解码", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }  
                }
            }
        }

        public bool DecodeNewBase64(string ExceptionBase64Str)
        {
            if (IsBase64Formatted(ExceptionBase64Str))
            {
                byte[] bytes = Convert.FromBase64String(ExceptionBase64Str);
                inputTextBox.Text = GetDecodingString(bytes);
                return true;
            }
            return false;
        }

        public void UrlDecode(string url)
        {
            if (outputTextBox.Text == "请输入你要解码的内容")
            {
                originOutput();
            }
            else
            {
                inputTextBox.Text = Uri.UnescapeDataString(url);
            }
        }

        public void dealWithUnicode(string str)
        {
            string resultStr = String.Empty;
            string[] strList = str.Split('u');
            for (int i = 1; i < strList.Length; i++)
            {
                resultStr += (char)int.Parse(strList[i], System.Globalization.NumberStyles.HexNumber);
            }
            inputTextBox.Text = resultStr;
        }

        public void UnicodeChineseDecode(string str)
        {
            string a = "&#".ToString();
            string b = "x".ToString();
            try
            {
                if (outputTextBox.Text == "请输入你要解码的内容")
                {
                    originOutput();
                }
                else if (str.Contains('u'))
                {
                    str = str.Replace(@"\", @"");
                    dealWithUnicode(str);
                }
                else if (str.Contains(a) && str.Contains(b))//十六进制
                {
                    str = str.Replace(@"&#x", @"u").Replace(@";", @"");
                    dealWithUnicode(str);
                }
                else if (str.Contains(a) && !str.Contains(b))//十进制
                {
                    Regex r = new Regex("\\d+\\.?\\d*");
                    MatchCollection mc = r.Matches(str);
                    string result = string.Empty;
                    for (int i = 0; i < mc.Count; i++)
                    {
                        int ss = int.Parse(mc[i].ToString());
                        String strA = ss.ToString("x8");
                        strA = strA.Replace(@"0000", @"x").Replace(strA, @"&#" + strA + ';');
                        result += strA;
                    }
                    string newstr = result;
                    newstr = newstr.Replace(@"&#x", @"u").Replace(@";", @"");
                    dealWithUnicode(newstr);
                }
                else 
                {
                    MessageBox.Show("输入解码格式错误", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch 
            {
                MessageBox.Show("输入解码格式错误", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void HexDecode(string str)
        {
            try
            {
                // 处理十六进制
                byte[] bytes = new byte[0];
                switch(radixType)
                {
                    case "八进制":
                        bytes = HexDecode_8(str);
                        break;
                    case "十进制":  // 十进制必须有分隔符才能转换
                        bytes = splitType == "无分隔符" ? new byte[0] : HexDecode_10(str);
                        break;
                    case "十六进制":
                    default:
                        bytes = HexDecode_16(str);
                        break;
                }

                inputTextBox.Text = GetDecodingString(bytes);
            }
            catch 
            {
                MessageBox.Show("请选择正确的分隔符号", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AES128Decode(string DecryptStr, string Key)
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
                //byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
                //rijndaelCipher.IV = ivBytes;
                ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
                byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                inputTextBox.Text = GetDecodingString(plainText);
            }
            catch (Exception ex)
            {
                inputTextBox.Text = ex.Message;
            }
        }

        byte[] HexDecode_16(string str)
        {
            str = str.Replace(splitType, string.Empty);

            if (str.Length % 2 != 0)
                str = str.Substring(0, str.Length - 1);
            byte[] arrByte = new byte[str.Length / 2];
            int index = 0;
            for (int i = 0; i < str.Length; i += 2)
            {
                arrByte[index++] = Convert.ToByte(str.Substring(i, 2), 16);        //Convert.ToByte(string,16)把十六进制string转化成byte 
            }

            return arrByte;
        }

        byte[] HexDecode_10(string str)
        {
            string[] arr = Regex.Split(str, Regex.Escape(splitType));
            byte[] arrByte = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arrByte[i] = Convert.ToByte(arr[i], 10); 
            }

            return arrByte;
        }

        byte[] HexDecode_8(string str)
        {
            if (splitType == "无分隔符")
            {
                if (str.Length % 3 != 0)
                    str = str.Substring(0, str.Length - str.Length % 3);
                byte[] arrByte = new byte[str.Length / 3];
                int index = 0;
                for (int i = 0; i < str.Length; i += 3)
                    arrByte[index++] = Convert.ToByte(str.Substring(i, 3), 8);
                return arrByte;
            }
            else
            {
                string[] arr = str.Split(new string[] { splitType }, StringSplitOptions.RemoveEmptyEntries);
                byte[] arrByte = new byte[arr.Length];
                for (int i = 0; i < arr.Length; i++)
                    arrByte[i] = Convert.ToByte(arr[i], 8);
                return arrByte;
            }
        }

        public void Base64StrToFile(string base64Str,string value)
        {
            DateTime dateTime = DateTime.Now;
            try
            {
                outPath = string.Format(TryGetSysTempDir() + "{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}.{6:D2}", dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, value);
                var contents = Convert.FromBase64String(base64Str);
                using (var fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(contents, 0, contents.Length);
                    fs.Flush();
                }
                inputTextBox.Text = outputTextBox.Text != string.Empty ? "文件解析地址为:" + outPath : string.Empty;
            }
            catch 
            {
                MessageBox.Show("目前仅支持/字符串/.jpg/.png/.gif/.bmp/.zip/.rar/.7z文件的解码", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static bool IsBase64Formatted(string input)
        {
            try
            {
                Convert.FromBase64String(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void NTLMRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            SetDefaultEncrypFormat();
        }


    }
}
