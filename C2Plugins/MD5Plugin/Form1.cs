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
            return "0.0.1";
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
        }

        //md5(64位)
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "加密 =>";
            button2.Visible = false;
        }

        //Base64
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "编码 =>";
            button2.Text = "<= 解码";
            button2.Visible = true;
        }

        //url编解码
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "编码 =>";
            button2.Text = "<= 解码";
            button2.Visible = true;
        }

        //utf8
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "转码 =>";
            button2.Visible = false;
        }

        //gbk
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "转码 =>";
            button2.Visible = false;
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

            switch (num)
            {
                case 1:
                    Md5Code_128(textBox1.Text);
                    break;
                case 2:
                    Md5Code_64(textBox1.Text);
                    break;
                case 3:
                    //Console.WriteLine("base64编码");
                    EncodeBase64(textBox1.Text);
                    break;
                case 4:
                    //Console.WriteLine("UrlDecode编码");
                    UrlEncode(textBox1.Text);
                    break;
                case 5:
                    //Console.WriteLine("utf8");
                    GetUtf8(textBox1.Text);
                    break;
                case 6:
                    //Console.WriteLine("gbk");
                    GetGbk(textBox1.Text);
                    break;
                default:
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
                    DecodeBase64(textBox2.Text);
                    break;
                case 4:
                    //Console.WriteLine("UrlDecode解码");
                    UrlDecode(textBox2.Text);
                    break;
                default:
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
            byte[] bytes = Convert.FromBase64String(str);
            textBox1.Text = Encoding.GetEncoding("utf-8").GetString(bytes);
        }

        public void UrlEncode(string url)
        {
            textBox2.Text = HttpUtility.UrlEncode(url);
        }

        public void UrlDecode(string url)
        {
            textBox1.Text = HttpUtility.UrlDecode(url);
        }

        public void GetUtf8(string str)
        {
            //byte[] utf8 = Encoding.UTF8.GetBytes(str);
            //string s3 = "";
            //string s3d = "";
            //foreach (byte b in utf8)
            //{
            //    s3 += string.Format("{0:X2}", b) + " ";
            //    s3d += b + " ";
            //}
            //textBox2.Text = s3;
            string ss = "";
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                     ss += "&#x" + ((int)str[i]).ToString("X") + ";";
                }

                textBox2.Text = ss;
            }
            else
            {
                textBox2.Text = "";
            }
        }

        public void GetGbk(string str)
        {
            string ss = "";
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    ss += "\\u" + ((int)str[i]).ToString("x");
                }
                textBox2.Text = ss;
            }
            else
            {
                textBox2.Text = "";
            }

        }
    }
}
