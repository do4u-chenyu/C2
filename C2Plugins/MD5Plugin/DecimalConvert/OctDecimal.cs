using C2.Log;
using System;
using System.Text;

namespace MD5Plugin
{
    partial class OctDecimal : URLPlugin
    {
        private const string blank = " ";
        protected string sepType = blank;
        protected int decimalBase;
        public OctDecimal()
        {
            InitializeComponent();
            InitializeControls();
        }


        private void InitializeControls()
        {
            inputTextBox.Select(inputTextBox.TextLength, 0);
            inputTextBox.Select(0, 0);
            sepComboBox.SelectedIndex = 0;
            decimalBase = 8;
        }

        public void ModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            sepType = sepComboBox.SelectedItem as string;
            if (sepType == "空格分割")
                sepType = blank;
        }

        public override void Encode(string str)
        {
            if(decimalBase == 8)
                new Log().LogManualButton("八进制转十六", "02");
            else
                new Log().LogManualButton("十进制转十六", "02");

            if (inputTextBox.Text == "请输入你要编码的内容")
            {
                ResetTextBox();
                return;
            }
            outputTextBox.Text = DoConvert(str, decimalBase, 16);
        }

        public override void Decode(string str)
        {
            if (decimalBase == 8)
                new Log().LogManualButton("八进制转十六", "02");
            else
                new Log().LogManualButton("十进制转十六", "02");

            if (outputTextBox.Text == "请输入你要解码的内容")
            {
                OriginOutput();
                return;
            }
 
            inputTextBox.Text = DoConvert(str, 16, decimalBase);
        }

        protected string ConvertBaseString(string s, int baseSrc, int baseDst)
        {
            try
            {
                byte b = Convert.ToByte(s, baseSrc);
                // 十六进制前面补0
                if (baseDst == 16)
                    return string.Format("{0:X2}", b);
                return Convert.ToString(b, baseDst);
            }
            catch { }
            return string.Empty;
        }

        protected string DoConvert(string str, int baseStr, int baseDst)
        {
            StringBuilder sb = new StringBuilder();
            string[] strings = str.Split(new string[] { sepType }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in strings)
                sb.Append(ConvertBaseString(s, baseStr, baseDst)).Append(sepType);
            return sb.ToString();
        }
    }
}
