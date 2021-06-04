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
            textBox1.Select(textBox1.TextLength , 0);
            textBox1.Select(0, 0);
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



        //md5(128位)
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //button1.Visible;
            button1.Text = "加密 =>";
            button2.Visible = false;
            textBox1.Text = "请把你需要加密的内容粘贴在这里";
            textBox2.Text = "加密后的结果";
            textBox1.ForeColor = Color.DarkGray;
            textBox2.ForeColor = Color.DarkGray;
        }

        //md5(64位)
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "加密 =>";
            button2.Visible = false;
            textBox1.Text = "请把你需要加密的内容粘贴在这里";
            textBox2.Text = "加密后的结果";
            textBox1.ForeColor = Color.DarkGray;
            textBox2.ForeColor = Color.DarkGray;
        }

        //Base64
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "编码 =>";
            button2.Text = "<= 解码";
            button2.Visible = true;
            textBox1.Text = "请输入你要用Base64加密的内容";
            textBox2.Text = "请输入你要用Base64解密的内容";
            textBox1.ForeColor = Color.DarkGray;
            textBox2.ForeColor = Color.DarkGray;
        }

        //url编解码
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "编码 =>";
            button2.Text = "<= 解码";
            button2.Visible = true;
            textBox1.Text = "请输入你要编码的Url";
            textBox2.Text = "请输入你要解码的Url";
            textBox1.ForeColor = Color.DarkGray;
            textBox2.ForeColor = Color.DarkGray;

        }

        //使用sha1对字符串进行加密
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "加密 =>";
            button2.Visible = false;
            textBox1.Text = "请把你需要加密的内容粘贴在这里";
            textBox2.Text = "加密后的结果";
            textBox1.ForeColor = Color.DarkGray;
            textBox2.ForeColor = Color.DarkGray;
        }

        //使用sha256对字符串进行加密
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "加密 =>";
            button2.Visible = false;
            textBox1.Text = "请把你需要加密的内容粘贴在这里";
            textBox2.Text = "加密后的结果";
            textBox1.ForeColor = Color.DarkGray;
            textBox2.ForeColor = Color.DarkGray;
        }

        //使用sha512对字符串进行加密
        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "加密 =>";
            button2.Visible = false;
            textBox1.Text = "请把你需要加密的内容粘贴在这里";
            textBox2.Text = "加密后的结果";
            textBox1.ForeColor = Color.DarkGray;
            textBox2.ForeColor = Color.DarkGray;
        }



        private void textBox1_MouseDown(object sender, EventArgs e)
        {
            if (textBox1.Text == "请把你需要加密的内容粘贴在这里" || textBox1.Text == "请输入你要用Base64加密的内容" || textBox1.Text == "请输入你要编码的Url")
            {
                textBox1.Text = "";
            }
            textBox1.ForeColor = Color.Black;
        }

        private void textBox2_MouseDown(object sender, EventArgs e)
        {
            if (textBox2.Text == "加密后的结果" || textBox2.Text == "请输入你要用Base64解密的内容" || textBox2.Text == "请输入你要解码的Url")
            {
                textBox2.Text = "";
            }
            textBox2.ForeColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num = 3;
            if (radioButton1.Checked)
            {
                num = 1;
            }
            if (radioButton4.Checked)
            {
                num = 2;
            }
            if (radioButton2.Checked)
            {
                num = 3;
            }
            if (radioButton3.Checked)
            {
                num = 4;
            }
            if (radioButton5.Checked)
            {
                num = 5;
            }
            if (radioButton6.Checked)
            {
                num = 6;
            }
            if (radioButton7.Checked)
            {
                num = 7;
            }
            switch (num)
            {
                case 1:
                    textBox2.ForeColor = Color.Black;
                    Md5Code_128(textBox1.Text);
                    break;
                case 2:
                    textBox2.ForeColor = Color.Black;
                    Md5Code_64(textBox1.Text);
                    break;
                case 3:
                    textBox2.ForeColor = Color.Black;
                    //Console.WriteLine("base64编码");
                    EncodeBase64(textBox1.Text);
                    break;
                case 4:
                    textBox2.ForeColor = Color.Black;
                    //Console.WriteLine("UrlDecode编码");
                    UrlEncode(textBox1.Text);
                    break;
                case 5:
                    textBox2.ForeColor = Color.Black;
                    //Console.WriteLine("sha1加密");
                    SHA1Encrypt(textBox1.Text);
                    break;
                case 6:
                    textBox2.ForeColor = Color.Black;
                    //Console.WriteLine("sha256加密");
                    SHA256Encrypt(textBox1.Text);
                    break;
                case 7:
                    textBox2.ForeColor = Color.Black;
                    //Console.WriteLine("sha512加密");
                    SHA512Encrypt(textBox1.Text);
                    break;
                default:
                    textBox2.ForeColor = Color.Black;
                    //Console.WriteLine("base64");
                    EncodeBase64(textBox1.Text);
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int num = 3;
            if (radioButton2.Checked)
            {
                num = 3;
            }
            if (radioButton3.Checked)
            {
                num = 4;
            }
            switch (num)
            {
                case 3:
                    textBox1.ForeColor = Color.Black;
                    DecodeBase64(textBox2.Text);
                    break;
                case 4:
                    textBox1.ForeColor = Color.Black;
                    //Console.WriteLine("UrlDecode解码");
                    UrlDecode(textBox2.Text);
                    break;
                default:
                    textBox1.ForeColor = Color.Black;
                    //Console.WriteLine("base64解码");
                    DecodeBase64(textBox2.Text);
                    break;
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
            textBox2.Text = sBuilder.ToString();

        }

        public void Md5Code_64(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(str)), 4, 8);
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            textBox2.Text = t2;
        }

        public void EncodeBase64(string str)
        {
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(str);
            textBox2.Text = Convert.ToBase64String(bytes);
        }

        public void DecodeBase64(string str)
        {
            if(IsBase64Formatted(str))
            {
                byte[] bytes = Convert.FromBase64String(str);
                textBox1.Text = Encoding.GetEncoding("utf-8").GetString(bytes);
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
            textBox2.Text = HttpUtility.UrlEncode(url);
        }

        public void UrlDecode(string url)
        {
    
            textBox1.Text = HttpUtility.UrlDecode(url);
            
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
            textBox2.Text = enText.ToString();
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
            textBox2.Text = sb.ToString();

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
            textBox2.Text = sb.ToString();
        }
    }
}
