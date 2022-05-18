using C2.Log;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MD5Plugin
{
    partial class HexPlugin : Base64Plugin
    {
        string splitType = "无分隔符";
        string radixType = "十六进制";
        public HexPlugin()
        {
            InitializeComponent();
            InitializeControls();
        }
        private new void InitializeControls()
        {
            inputTextBox.Select(inputTextBox.TextLength, 0);
            inputTextBox.Select(0, 0);
            splitComboBox.SelectedIndex = 0;
            radixComboBox.SelectedIndex = 0;
            encodingComboBox.SelectedIndex = 0;
        }

        private void Split_SelectedIndexChanged(object sender, EventArgs e)
        {
            splitType = splitComboBox.SelectedItem as string;
        }

        private void RadixComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            radixType = radixComboBox.SelectedItem as string;
        }

        public override void Encode(string str)
        {
            new Log().LogManualButton("HEX编解码", "02");
            if (inputTextBox.Text == "请输入你要编码的内容")
            {
                ResetTextBox();
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                byte[] arrByte = GetEncodingBytes(str);

                string sep = Sep();

                int radix = 16;
                if (radixType == "十进制")
                    radix = 10;
                if (radixType == "八进制")
                    radix = 8;

                for (int i = 0; i < arrByte.Length; i++)
                {
                    if (sep.Length == 2)
                        sb.Append(sep + Convert.ToString(arrByte[i], radix));
                    else
                        sb.Append(Convert.ToString(arrByte[i], radix) + sep);
                }
                outputTextBox.Text = sb.ToString();
            }
        }

        private string Sep()
        {
            return splitType == "无分隔符" ? string.Empty :
                splitType == "空格分割" ? " " : splitType.Trim();
        }

        byte[] HexDecode_8(string str, string sep)
        {
            if (string.IsNullOrEmpty(sep))
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
                string[] arr = str.Split(new string[] { sep }, StringSplitOptions.RemoveEmptyEntries);
                byte[] arrByte = new byte[arr.Length];
                for (int i = 0; i < arr.Length; i++)
                    arrByte[i] = Convert.ToByte(arr[i], 8);
                return arrByte;
            }
        }

        byte TryConvertToByte(string value, int fromBase)
        {
            try
            {
                return Convert.ToByte(value, 10);
            }
            catch
            {
                return 0;
            }
        }

        byte[] HexDecode_10(string str, string sep)
        {
            string[] arr = str.Split(new string[] { sep }, StringSplitOptions.RemoveEmptyEntries);
            byte[] arrByte = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arrByte[i] = Convert.ToByte(arr[i], 10);
            }

            return arrByte;
        }
        byte[] HexDecode_16(string str, string sep)
        {
            if (!string.IsNullOrEmpty(sep))
                str = str.Replace(sep, string.Empty);

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

        public override void Decode(string str)
        {
            new Log().LogManualButton("HEX编解码", "02");
            string sep = Sep();
            try
            {
                // 处理十六进制
                byte[] bytes = new byte[0];
                switch (radixType)
                {
                    case "八进制":
                        bytes = HexDecode_8(str, sep);
                        break;
                    case "十进制":  // 十进制必须有分隔符才能转换
                        bytes = HexDecode_10(str, sep);
                        break;
                    case "十六进制":
                    default:
                        bytes = HexDecode_16(str, sep);
                        break;
                }

                inputTextBox.Text = GetDecodingString(bytes);
            }
            catch
            {
                string errorMessage = "请选择正确的分隔符号";
                if (radixType == "八进制" && string.IsNullOrEmpty(sep))
                    errorMessage = "八进制Hex解码必须要有分隔符,因为八进制每个值1-3个字符不固定";
                if (radixType == "十进制" && string.IsNullOrEmpty(sep))
                    errorMessage = "十进制Hex解码必须要有分隔符,因为十进制每个值1-3个字符不固定";
                MessageBox.Show(errorMessage, "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
