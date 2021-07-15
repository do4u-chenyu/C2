using C2.IAOLab.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        private bool isReturnNum;


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
            return "0.0.2";
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

        //md5(128位)
        private void Md5128RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //button1.Visible;
            encodeButton.Text = "加密 =>";
            decodeButton.Visible = false;
            SetDefault1();
        }
        

        //md5(64位)
        private void Md564RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            encodeButton.Text = "加密 =>";
            decodeButton.Visible = false;
            SetDefault1();
        }

        //Base64
        private void Base64RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            encodeButton.Text = "编码 =>";
            decodeButton.Text = "<= 解码";
            decodeButton.Visible = true;
            SetDefault2();
        }

        //url编解码
        private void UrlRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            encodeButton.Text = "编码 =>";
            decodeButton.Text = "<= 解码";
            decodeButton.Visible = true;
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
            if (inputTextBox.Text == "请把你需要加密的内容粘贴在这里" || inputTextBox.Text == "请输入你要用Base64加密的内容" || inputTextBox.Text == "请输入你要编码的Url")
            {
                inputTextBox.Text = "";
            }
            inputTextBox.ForeColor = Color.Black;
        }

        private void OutputTextBox_MouseDown(object sender, EventArgs e)
        {
            if (outputTextBox.Text == "加密后的结果" || outputTextBox.Text == "请输入你要用Base64解密的内容" || outputTextBox.Text == "请输入你要解码的Url")
            {
                outputTextBox.Text = "";
            }
            outputTextBox.ForeColor = Color.Black;
        }

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
                //Console.WriteLine("base64编码");
                EncodeBase64(inputTextBox.Text);
            }
            else if (urlRadioButton.Checked)
            {
                //Console.WriteLine("UrlDecode编码");
                UrlEncode(inputTextBox.Text);
            }
            else if (sha1RadioButton.Checked)
            {
                //Console.WriteLine("sha1加密");
                SHA1Encrypt(inputTextBox.Text);
            }
            else if (sha256RadioButton.Checked)
            {
                //Console.WriteLine("sha256加密");
                SHA256Encrypt(inputTextBox.Text);
            }
            else if (sha512RadioButton.Checked)
            {
                //Console.WriteLine("sha512加密");
                SHA512Encrypt(inputTextBox.Text);
            }
            else
            {
                //Console.WriteLine("base64");
                EncodeBase64(inputTextBox.Text);
            }
            outputTextBox.ForeColor = Color.Black;
        }

        private void DecodeButton_Click(object sender, EventArgs e)
        {
            inputTextBox.ForeColor = Color.Black;
            if (base64RadioButton.Checked)
            {
                DecodeBase64(outputTextBox.Text);
            }
            else if (urlRadioButton.Checked)
            {
                //Console.WriteLine("UrlDecode解码");
                UrlDecode(outputTextBox.Text);
            }
            else
            {
                //Console.WriteLine("base64解码");
                DecodeBase64(outputTextBox.Text);
            }
        }


        public void Md5Code_128(string str)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            //byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));
            byte[] data = md5Hasher.ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));

            }
            //return sBuilder.ToString();
            outputTextBox.Text = sBuilder.ToString();

        }

        public void Md5Code_64(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(str)), 4, 8);
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            outputTextBox.Text = t2;
        }

        public void EncodeBase64(string str)
        {
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(str);
            outputTextBox.Text = Convert.ToBase64String(bytes);
        }

        public void DecodeBase64(string str)
        {
            if(IsBase64Formatted(str))
            {
                byte[] bytes = Convert.FromBase64String(str);
                inputTextBox.Text = Encoding.GetEncoding("utf-8").GetString(bytes);
            }
            else
            {
                MessageBox.Show("需要解码的字符串非Base64编码，请重新输入");
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

        public void UrlEncode(string url)
        {
            outputTextBox.Text = HttpUtility.UrlEncode(url);
        }

        public void UrlDecode(string url)
        {
    
            inputTextBox.Text = HttpUtility.UrlDecode(url);
            
        }

        public void SHA1Encrypt(string str)
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

        public void SHA256Encrypt(string str)
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

        public void SHA512Encrypt(string str)
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
}
