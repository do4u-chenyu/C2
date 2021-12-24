using C2.Core;
using C2.Utils;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace C2.Business.CastleBravo.Binary
{
    public partial class BinaryMainForm : Form
    {
        private string[] strings;
        private readonly List<string> list = new List<string>();
        private readonly StringBuilder sb = new StringBuilder();
        public BinaryMainForm()
        {
            InitializeComponent();
            this.textBox1.GotFocus += TextBox1_GotFocus;
        }

        private void TextBox1_GotFocus(object sender, System.EventArgs e)
        {
            if (this.textBox1.ForeColor == SystemColors.InactiveCaption)
            {
                this.textBox1.ForeColor = SystemColors.WindowText;
                this.textBox1.Text = string.Empty;
            }
        }

        private void FileButton_Click(object sender, System.EventArgs e)
        {
            DialogResult ret = this.openFileDialog1.ShowDialog();
            if (ret != DialogResult.OK)
                return;

            this.ResultTB.Text = string.Empty;
            this.FileTB.Text = this.openFileDialog1.FileName;
            this.list.Clear();
            this.sb.Clear();

            using (GuarderUtil.WaitCursor)
                strings = new BinStrings().Strings(this.FileTB.Text);

            
            int nrIP = CountIP(strings);
            int nrUrl = CountUrl(strings);
            int nrPhones = CountPhoneNumber(strings);
            int nrUsers = CountUsername(strings);

            this.sb.AppendLine("重点文本字符串:")
                   .AppendLine("========================================")
                   .AppendLine(list.JoinString(System.Environment.NewLine))
                   .AppendLine("========================================")
                   .AppendLine("原始文本字符串:")
                   .Append(strings.JoinString(System.Environment.NewLine));

            this.ResultTB.Text = this.sb.ToString();

            this.label6.Text = string.Format("共{0} 条: {1} IP / {2} url / {3} 电话 / {4} 单词短语",
                strings.Length,
                nrIP,
                nrUrl,
                nrPhones,
                nrUsers);
          
        }

        private int CountIP(string[] strings)
        {
            return Count(strings, new Regex(@"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$"), true);
        }

        private int Count(string[] strings, Regex regex, bool add = false)
        {
            int count = 0;
            foreach (string str in strings)
                if (regex.IsMatch(str))
                {
                    if (add)
                        list.Add(str);
                    count++;
                }
                    
            return count;
        }

        private int CountUrl(string[] strings)
        {
            return Count(strings, new Regex(@"^(http:\\)|(https:\\)|(www\.).+$"), true);
        }

        private int CountPhoneNumber(string[] strings)
        {
            return Count(strings, new Regex(@"^1\d{10}$"), true);
        }

        private int CountUsername(string[] strings)
        {
            return Count(strings, new Regex(@"^[\w \._-]+$"));
        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            this.textBox1.Clear();
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            string plainText = textBox1.Text.Trim().ToLower();
            if (plainText.Contains("?"))
                if (plainText.Contains("~"))
                    textBox1.Text = new XiseDES().XiseDecrypt(plainText);
                else
                    textBox1.Text = new XiseDES().XiseSimpleDecrypt(plainText);
            else if (Regex.IsMatch(plainText, @"^[\da-f]+$") && plainText.Length % 2 == 0)
                textBox1.Text = new XiseDES().XiseHexDecrypt(plainText);
            else
                textBox1.Text = "格式错误";
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            this.textBox1.Focus();
            this.textBox1.Text = "122?57?118?39?232?250?196?214?~141?43?244?40?155?102?159?246?108?206?242?154?53?85?183?221?";
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            this.textBox1.Focus();
            this.textBox1.Text = "3132323F35373F3131383F33393F3233323F3235303F3139363F3231343F7E37303F3234323F3131363F363F35343F3230333F3136333F3232373F37343F3134343F3232393F3233343F3230313F36333F3231323F36343F363F3130323F3137373F3137303F35373F34323F32343F3235333F3230383F343F34303F3137373F39303F35383F303F37363F3130323F3130373F3134333F33343F34343F34333F3132373F35353F3133323F3231383F3133373F3135383F39323F39373F3231383F3132393F33303F3231343F3131363F35323F34333F3230353F3234313F34393F36353F343F3230373F3139383F39313F3130383F353F3133353F33343F3139393F3133393F35313F3131383F3138343F3137303F3230363F31343F3132393F36333F37333F333F34323F3233393F3135363F3134303F37393F36343F343F36323F3234383F39313F3139353F34373F3136383F3131383F323F3135333F3230383F3135373F37353F3234303F3132303F3133393F3134393F35353F3230383F37323F3137353F3137333F3138373F3232363F3130373F33363F3138373F38393F39393F39373F34353F3135383F3139353F3133363F35363F3135363F3136353F303F3233353F3138303F37303F3234333F3230363F3234333F3138323F37353F3137333F36383F37323F3231333F373F31303F3135383F";
        }
    }
}
