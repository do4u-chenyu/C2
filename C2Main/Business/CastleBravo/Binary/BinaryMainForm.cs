using C2.Core;
using C2.Utils;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows.Forms;

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
            textBox1.Text = new XiseDES().XiseDecrypt(textBox1.Text);
        }
    }
}
