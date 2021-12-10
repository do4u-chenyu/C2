using System;
using System.Text;

namespace MD5Plugin
{
    public partial class CommonHashPlugin : CommonPlugin
    {
        public CommonHashPlugin()
        {
            InitializeComponent();
            this.encodeTypeCB.SelectedIndex = 0;
        }

        protected byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[0];

            switch(encodeTypeCB.SelectedIndex)
            {
                case 0:
                    bytes = Encoding.Default.GetBytes(str);
                    break;
                case 1:
                    bytes = HexStringToBytes(str);
                    break;
            }

            return bytes;
        }

        private  byte[] HexStringToBytes(string str)
        {
            str = str.ToLower().Trim();
            str = str.StartsWith("0x") ? str.Substring(2) : str;

            byte[] arrByte = new byte[str.Length >> 1];
            str = str.Substring(0, arrByte.Length << 1);

            try
            {
                for (int i = 0; i < arrByte.Length; i++)
                    arrByte[i] = Convert.ToByte(str.Substring(i << 1, 2), 16);       
            }
            catch
            {
                return new byte[0];
            }

            return arrByte;
        }
    }
}
