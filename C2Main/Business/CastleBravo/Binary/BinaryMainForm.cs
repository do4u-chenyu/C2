using C2.Core;
using C2.Utils;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.Binary
{
    public partial class BinaryMainForm : Form
    {
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

            using (GuarderUtil.WaitCursor)
            {
                string[] strings = new BinStrings().Strings(this.FileTB.Text);
                this.ResultTB.Text = strings.JoinString(System.Environment.NewLine);

                int nrIP = CountIP(strings);
                int nrUrl = CountUrl(strings);
                int nrPhones = CountPhoneNumber(strings);
                int nrUsers = CountUsername(strings);

                this.label6.Text = string.Format("共{0} 条: {1} IP / {2} url / {3} 电话 / {4} 单词短语",
                    strings.Length,
                    nrIP,
                    nrUrl,
                    nrPhones,
                    nrUsers);
            }   
        }

        private int CountIP(string[] strings)
        {
            return Count(strings, new Regex(@"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$"));
        }

        private int Count(string[] strings, Regex regex)
        {
            int count = 0;
            foreach (string str in strings)
                if (regex.IsMatch(str))
                    count++;
            return count;
        }

        private int CountUrl(string[] strings)
        {
            return Count(strings, new Regex(@"^(http:\\)|(https:\\)|(www\.).+$"));
        }

        private int CountPhoneNumber(string[] strings)
        {
            return Count(strings, new Regex(@"^1\d{10}$"));
        }

        private int CountUsername(string[] strings)
        {
            return Count(strings, new Regex(@"^[\w \._-]+$"));
        }
    }
}
