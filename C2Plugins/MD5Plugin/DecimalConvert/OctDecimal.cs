using System;
using System.Text;

namespace MD5Plugin.DecimalConvert
{
    public partial class OctDecimal : URLlPlugin
    {

        private string sepType = " ";
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
        }

        public void ModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            sepType = sepComboBox.SelectedItem as string;
            if (sepType == "空格分割")
                sepType = " ";
        }

        public override void encode(string str)
        {
            StringBuilder sb = new StringBuilder();
            string[] strings = str.Split(new string[] { sepType }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in strings)
            {
                sb.Append(s).Append(sepType);
            }
            outputTextBox.Text = sb.ToString();
        }


    }
}
