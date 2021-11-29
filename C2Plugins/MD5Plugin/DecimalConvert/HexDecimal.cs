using System;
using System.Text;

namespace MD5Plugin.DecimalConvert
{
    public partial class HexDecimal : OctDecimal
    {
        public HexDecimal()
        {
            InitializeComponent();
        }

        public override void encode(string str)
        {
            StringBuilder sb = new StringBuilder();
            string[] strings = str.Split(new string[] { sepType }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in strings)
            {
                sb.Append(Convert.ToString(Convert.ToByte(s), 16)).Append(sepType);
            }
            outputTextBox.Text = sb.ToString();
        }

        public override void decode(string str)
        {
            StringBuilder sb = new StringBuilder();
            string[] strings = str.Split(new string[] { sepType }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in strings)
            {
                sb.Append(Convert.ToString(Convert.ToByte(s, 16))).Append(sepType);
            }
            inputTextBox.Text = sb.ToString();
        }

    }
}
