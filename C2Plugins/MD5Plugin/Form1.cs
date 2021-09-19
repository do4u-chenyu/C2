using C2.IAOLab.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace MD5Plugin
{
    public partial class Form1 : Form, IPlugin
    {
        public Form1()
        {
            InitializeComponent();
            inputTextBox.Select(inputTextBox.TextLength, 0);
            inputTextBox.Select(0, 0);
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

        private void SetDefault1()
        {
            inputTextBox.Text = "请把你需要加密的内容粘贴在这里";
            outputTextBox.Text = "加密后的结果";
            inputTextBox.ForeColor = Color.DarkGray;
            outputTextBox.ForeColor = Color.DarkGray;
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

        //md5(128位)
        private void Md5128RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //button1.Visible;
            encodeButton.Text = "加密 =>";
            decodeButton.Visible = false;
            //FileInputButton.Visible = false;
            //base64strButton.Visible = false;
            SetDefault1();
        }


        //md5(64位)
        private void Md564RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            encodeButton.Text = "加密 =>";
            decodeButton.Visible = false;
            //FileInputButton.Visible = false;
            SetDefault1();
        }

        //Base64
        private void Base64RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            encodeButton.Text = "编码 =>";
            decodeButton.Text = "<= 解码";
            //FileInputButton.Text = "编码 =>";
            //base64strButton.Text = "<= 解码";
            encodeButton.Visible = true;
            decodeButton.Visible = true;
            SetDefault3();
        }

        //url编解码
        private void UrlRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            encodeButton.Text = "编码 =>";
            decodeButton.Text = "<= 解码";
            encodeButton.Visible = true;
            decodeButton.Visible = true;
            SetDefault2();
        }

        //Unicode编解码
        private void UnicodeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            encodeButton.Text = "编码 =>";
            decodeButton.Text = "<= 解码";
            decodeButton.Visible = true;
            encodeButton.Visible = true;
            SetDefault2();
        }

        //使用sha1对字符串进行加密
        private void Sha1RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            encodeButton.Text = "加密 =>";
            decodeButton.Visible = false;
            SetDefault1();
        }

        //使用sha256对字符串进行加密
        private void Sha256RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            encodeButton.Text = "加密 =>";
            decodeButton.Visible = false;
            SetDefault1();
        }

        //使用sha512对字符串进行加密
        private void Sha512RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            encodeButton.Text = "加密 =>";
            decodeButton.Visible = false;
            SetDefault1();
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
                originInput();
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
                originInput();
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
        public void originInput()
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
                originInput();
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
                byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(filePath);
                outputTextBox.Text = Convert.ToBase64String(bytes);
            }
        }
        public void UrlEncode(string url)
        {
            if (inputTextBox.Text == "请输入你要编码的内容")
            {
                originInput();
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
        public void SHA1Encrypt(string str)
        {
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里")
            {
                originInput();
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
                originInput();
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
                originInput();
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
            DateTime dateTime = DateTime.Now;
            Dictionary<string, string> pairs = new Dictionary<string, string>();
            pairs.Add("/9j/", "jpg");
            pairs.Add("iVBORw", "png");
            pairs.Add("Qk", "bmp");
            pairs.Add("R0lGOD", "gif");
            pairs.Add("UEsDBB", "zip");
            pairs.Add("UEsDBA", "zip");
            pairs.Add("UmFyIR", "rar");
            pairs.Add("N3q8rycc", "7z");

            // base64解码前先进行url解码,反复解码直到解不出来为止
            int len = base64Str.Length;
            int cnt = 0;
            do
            {
                base64Str = HttpUtility.UrlDecode(base64Str);
            } while (len != base64Str.Length && cnt++ < 10);
            

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
                    outPath = string.Format(TryGetSysTempDir() + "\\{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}.{6:D2}", dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, value);
                    base64StrToFile(base64Str);
                    inputTextBox.Text = outputTextBox.Text != string.Empty ? "文件解析地址为:" + outPath : string.Empty;
                    break;
                }
                else if (IsBase64Formatted(base64Str))
                {
                    byte[] bytes = Convert.FromBase64String(base64Str);
                    inputTextBox.Text = Encoding.GetEncoding("utf-8").GetString(bytes);
                }
                else
                {
                    inputTextBox.Text = string.Empty;
                    MessageBox.Show("目前仅支持/字符串/.jpg/.png/.gif/.bmp/.zip/.rar/.7z文件的解码", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                //inputTextBox.Text = outputTextBox.Text != string.Empty ? "文件解析地址为:" + outPath : string.Empty;
            }
        }
        public void UrlDecode(string url)
        {
            if (outputTextBox.Text == "请输入你要解码的内容")
            {
                originOutput();
            }
            else
            {
                inputTextBox.Text = HttpUtility.UrlDecode(url);
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


        //Base64解密
        string outPath;
        public void base64StrToFile(string base64Str)
        {
            var contents = Convert.FromBase64String(base64Str);
            using (var fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(contents, 0, contents.Length);
                fs.Flush();
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
        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
