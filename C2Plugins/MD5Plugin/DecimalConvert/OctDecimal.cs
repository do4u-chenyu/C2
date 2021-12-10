using System;
using System.Text;

namespace MD5Plugin
{
    partial class OctDecimal : URLPlugin
    {

        protected string sepType = " ";
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
                sepType = " ";
        }

        public override void Encode(string str)
        {
            if (inputTextBox.Text == "请输入你要编码的内容")
            {
                ResetTextBox();
                return;
            }
            outputTextBox.Text = DoConvert(str, decimalBase, 16);
        }
        public override void Decode(string str)
        {
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
                return Convert.ToString(Convert.ToByte(s, baseSrc), baseDst);
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
