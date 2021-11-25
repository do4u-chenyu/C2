using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MD5Plugin
{
    public partial class Base64Plugin : URLlPlugin
    {
        string encodingType = "UTF-8";
        string outPath;
        public Base64Plugin()
        {
            InitializeComponent();
            InitializeControls();
        }
        private void InitializeControls()
        {
            inputTextBox.Select(inputTextBox.TextLength, 0);
            inputTextBox.Select(0, 0);
            encodingComboBox.SelectedIndex = 0;
        }

        public void ModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EncodingType = encodingComboBox.SelectedItem as string;
        }
        public string EncodingType
        {
            get
            {
                return encodingType;
            }
            set => encodingType = value;
        }
        public byte[] GetEncodingBytes(string str)
        {
            // 编码时不应该选择HEX, 如果选了默认为UTF-8
            EncodingType = EncodingType == "HEX" ? "UTF-8" : EncodingType;
            return Encoding.GetEncoding(EncodingType).GetBytes(str);
        }

        public override void encode(string str)
        {
            if (inputTextBox.Text == "请输入你要编码的内容")
            {
                ResetTextBox();
            }
            else
            {
                byte[] bytes = GetEncodingBytes(str);
                outputTextBox.Text = Convert.ToBase64String(bytes);
            }
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

        public void Base64StrToFile(string base64Str, string value)
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
                MessageBox.Show("目前仅支持/字符串/.jpg/.png/.gif/.bmp/.zip/.rar/.7z/.gz文件的解码", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public string GetDecodingString(byte[] bytes)
        {
            if (EncodingType == "HEX")
                return BitConverter.ToString(bytes).Replace("-", string.Empty);
            return Encoding.GetEncoding(EncodingType).GetString(bytes);
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

        public override void decode(string base64Str)
        {
            Dictionary<string, string> pairs = new Dictionary<string, string>();
            pairs.Add("/9j/", "jpg");
            pairs.Add("iVBORw", "png");
            pairs.Add("Qk", "bmp");
            pairs.Add("R0lGOD", "gif");
            pairs.Add("UEsDBB", "zip");
            pairs.Add("UEsDBA", "zip");
            pairs.Add("UmFyIR", "rar");
            pairs.Add("N3q8rycc", "7z");
            pairs.Add("H4sIC", "gz");     // 1F8B08

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

                    Base64StrToFile(base64Str, value);
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
                        base64Str = base64Str.Substring(0, baseLengh - i);
                        if (DecodeNewBase64(base64Str))
                            break;
                    }
                    if (i == baseLengh)
                    {
                        inputTextBox.Text = string.Empty;
                        MessageBox.Show("目前仅支持/字符串/.jpg/.png/.gif/.bmp/.zip/.rar/.7z/.gz文件的解码", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }
            }
        }
    }
}
