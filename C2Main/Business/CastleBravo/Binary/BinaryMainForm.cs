using C2.Core;
using C2.Utils;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using C2.Business.CastleBravo.Binary.Info;
using System;

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
            InitializeOther();
            InitializeBehinderLabels();
        }

        private void InitializeOther()
        {
            this.XiseTextBox.GotFocus += TextBox_GotFocus;
            this.BehinderDTextBox.GotFocus += TextBox_GotFocus;
            this.BehinderETextBox.GotFocus += TextBox_GotFocus;
            this.BehinderDTextBox.Text = Settings.BehinderPlainText;
        }

        private void InitializeBehinderLabels()
        {
            using (GuarderUtil.WaitCursor)
                this.DictCountLabel.Text = Password.GetInstance().Pass.Count.ToString();

            this.HitPasswordLabel.Text = string.Empty;
            this.progressBar.Minimum = 0;
            this.progressBar.Maximum = Password.GetInstance().Pass.Count;
            this.progressBar.Value = 0;
        }

        private void TextBox_GotFocus(object sender, System.EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.ForeColor == SystemColors.InactiveCaption)
            {
                tb.ForeColor = SystemColors.WindowText;
                tb.Clear();
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

        private void XiseDecryptButton_Click(object sender, System.EventArgs e)
        {
            string plainText = XiseTextBox.Text.Trim().ToLower();
            if (plainText.Contains("?"))
                if (plainText.Contains("~"))
                    XiseTextBox.Text = new XiseDES().XiseDecrypt(plainText);
                else
                    XiseTextBox.Text = new XiseDES().XiseSimpleDecrypt(plainText);
            else if (Regex.IsMatch(plainText, @"^[\da-f]+$") && plainText.Length % 2 == 0)
                XiseTextBox.Text = new XiseDES().XiseHexDecrypt(plainText);
            else
                XiseTextBox.Text = "格式错误";
        }

        private void XiseS1Button_Click(object sender, System.EventArgs e)
        {
            this.XiseTextBox.Focus();
            this.XiseTextBox.Text = Settings.XiseString;
        }

        private void XiseS2Button_Click(object sender, System.EventArgs e)
        {
            this.XiseTextBox.Focus();
            this.XiseTextBox.Text = Settings.XiseHexString;
        }

        private void BehinderS1Button_Click(object sender, System.EventArgs e)
        {
            this.BehinderDTextBox.Focus();
            this.BehinderDTextBox.Text = Settings.BehinderPlainText;
        }

        private void BehinderClearButton_Click(object sender, System.EventArgs e)
        {
            InitializeBehinderLabels();
            this.BehinderDTextBox.Clear(); 
        }

        private void BehinderDecryptButton_Click(object sender, System.EventArgs e)
        {
            BehinderDTextBox.Focus();
            InitializeBehinderLabels();
            Behinder bh = new Behinder();
            bh.OnIteratorCount += Bh_OnIteratorCount;
            try 
            {
                BehinderDTextBox.Text = bh.Format(bh.Descrypt(BehinderDTextBox.Text.Trim()));
            }
            catch (Exception ex)
            {
                BehinderDTextBox.Text = ex.Message;
            }
            
            HitPasswordLabel.Text = bh.HitPassword;
            SuccessLabel.Text = bh.Success ? "成功" : "完成";
        }

        private void Bh_OnIteratorCount(object sender, System.EventArgs e)
        {
            Behinder bh  = sender as Behinder;
            this.progressBar.Value = bh.IteratorCount;
        }

        private void XiseClearButton_Click(object sender, System.EventArgs e)
        {
            this.XiseTextBox.Clear();
        }

        private void BehinderEClearButton_Click(object sender, EventArgs e)
        {
            BehinderETextBox.Clear();
        }

        private void BehinderDGenButton_Click(object sender, EventArgs e)
        {
            BehinderETextBox.Focus();

            Behinder bh = new Behinder();
            StringBuilder sb = new StringBuilder();
            string[] ss = BehinderETextBox.Text.SplitLine();
            foreach (string s in ss)
            {
                sb.Append(rb20.Checked ? bh.Encrypt20(s.Trim()) : bh.Encrypt40(s.Trim()))  
                  .Append('\t')
                  .AppendLine(s.Trim());
            }
            BehinderETextBox.Text = sb.ToString();
        }

        private void BehinderES1Button_Click(object sender, EventArgs e)
        {
            BehinderETextBox.Focus();
            BehinderETextBox.Clear();
            BehinderETextBox.Text = new StringBuilder()
                .AppendLine("rebeyond")
                .AppendLine("123456")
                .AppendLine("admin")
                .AppendLine("hack")
                .ToString();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
