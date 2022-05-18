using C2.Log;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MD5Plugin
{
    partial class UnicodePlugin : URLPlugin
    {
        string splitType = "\\u";
        public UnicodePlugin()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
        }

        private void Split_SelectedIndexChanged(object sender, EventArgs e)
        {
            splitType = comboBox1.SelectedItem as string;
        }

        public override void Encode(string str)
        {
            new Log().LogManualButton("Unicode编解码", "02");
            if (inputTextBox.Text == "请输入你要编码的内容")
            {
                ResetTextBox();
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

        public void DealWithUnicode(string str)
        {
            string resultStr = String.Empty;
            
            string[] strList = str.Split('u');
            for (int i = 1; i < strList.Length; i++)
            {
                resultStr += (char)int.Parse(strList[i], System.Globalization.NumberStyles.HexNumber);
            }
            inputTextBox.Text = resultStr;
        }

        public override void Decode(string str)
        {
            new Log().LogManualButton("Unicode编解码", "02");
            string a = "&#".ToString();
            string b = "x".ToString();
            try
            {
                if (outputTextBox.Text == "请输入你要解码的内容")
                {
                    OriginOutput();
                }
                else if (splitType == "\\u")
                {
                    str = str.Replace(@"\", string.Empty);
                    DealWithUnicode(str);
                }
                else if (splitType == "&#;") //&#22823;&#28385;&#36143;&#22823;&#22235;
                {
                    if(str.Substring(str.Length - 1, 1) == ";")
                        str = str.Substring(0, str.Length - 1);//去掉最后一个;
                    StringBuilder stringBuilder = new StringBuilder();
                    string[] words = str.Split(';');
                    foreach (var word in words)
                    {
                        char v = Convert.ToChar(Convert.ToInt32(word.Replace("&#", "")));
                        stringBuilder.AppendFormat(v.ToString());
                    }
                    inputTextBox.Text = stringBuilder.ToString();
                }
                else if (str.Contains(a) && str.Contains(b))//十六进制
                {
                    str = str.Replace("&#x", "u").Replace(";", string.Empty);
                    DealWithUnicode(str);
                }
                else if (splitType == "&#")//十进制
                {
                    Regex r = new Regex("\\d+\\.?\\d*");
                    MatchCollection mc = r.Matches(str);
                    string result = string.Empty;
                    for (int i = 0; i < mc.Count; i++)
                    {
                        int ss = int.Parse(mc[i].ToString());
                        String strA = ss.ToString("x8");
                        strA = strA.Replace("0000", "x").Replace(strA, "&#" + strA + ';');
                        result += strA;
                    }
                    string newstr = result;
                    newstr = newstr.Replace("&#x", "u").Replace(@";", string.Empty);
                    DealWithUnicode(newstr); 
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
    }
}
