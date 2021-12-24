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
            this.textBox2.GotFocus += TextBox2_GotFocus;
        }

        private void TextBox1_GotFocus(object sender, System.EventArgs e)
        {
            if (this.textBox1.ForeColor == SystemColors.InactiveCaption)
            {
                this.textBox1.ForeColor = SystemColors.WindowText;
                this.textBox1.Text = string.Empty;
            }
        }

        private void TextBox2_GotFocus(object sender, System.EventArgs e)
        {
            if (this.textBox2.ForeColor == SystemColors.InactiveCaption)
            {
                this.textBox2.ForeColor = SystemColors.WindowText;
                this.textBox2.Text = string.Empty;
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

        private void button7_Click(object sender, System.EventArgs e)
        {
            this.textBox2.Focus();
            this.textBox2.Text = "AJfGrpmaaRZJ3Hgk5ilZk1oOmqs/iIKQGLKZ0skOEIz03uRR4Kd4mWQ9xl7smsnfmlC6IIdPD6bbSK0LZ1pjnN8zJ5YeA3NriS9y1KtWoymrd09fqjEXZPB4YEsQULtjCB++CGJ0bRN7Kp/k74Zme+aSfRJS8oT8rotF9w0t3Wr67iHrlfbw6Zsz65IKUfmsCHd+okZnCzstjNGmKWeR5n8IH5wURce7CKgZtX5lH59zPnguh+v2BELDy9RmRtS/ucj6D3ewuyFKwRzl4wZFXDRY2GV9LHgt4yrakUV1rdgx0CnCcFRdBYxlQrRKPFOgHBOMP1SoUGdGaokr0PmoxVlT5YKjYeUjxvmY3P7ELG/DsEpxyr8mpY7Tc91pxBpqFsJ2nLYvtBEIBryfrdiA7S1GtVuVXxglKks4hKnT6BFIwBwukXEXIjfKshwRb2YQuLIWr90z1tZMzAIQWWwlK1cRsAg6zMpMMpvzv/CFYWYZradgU6L7YmhGpPFDQDb+h/wCNFygD+VY7f/lVNViRr1MHhSk6fB9nO2JQ65jnw19+Q5Oe+DKeX3x6DbZ2Mgvz43uPve6FH+5d0GicunoW7qaiKj6nkaFFbcrsybftDzcqrh+jF7G4fT1SScXYPzq72cyZ6Y0cckHo65vEWlB+QuZX+/l5DUVfltCgS7U8PrmNZ3A4ME3YiUVWQnKfh67BoW+8fNqUzyyqI7F09NtFBgE11gIRyUlSKyPauL6+uLykvGjjcWUC4Rfezokml32V9XoIuLgvyqUp762Q4FiwWl3qngBaYG4ZyTeCJfTdrml6s7c7Kc7sPJCoVIhKWonxkScw29eUw/d+mj+s9j1xpkA/HlHJ+/NseK8B8kRli+PovBNQ/+NXjSzeTxgS2IwFoA8hwOODE7Hb39zPJsw6wiRVnfvrzQKbAbfwqX2qMEiMfoh+GFif3z+l4FN7+iAn8UOY6B9NeF7AeREcgS1Ay9BDjQlzWi+6Aoz4dreVs8WzapGptY+F52KK131b7Pk+XAJui62EwYMM1ggbnAHSjLdi6NgOltfLoxTc18oCFfFJ2ShO5jM3JR0gHw5jKkfFtr4RTe6dX6ftbOG4srXyeabmy3ALdprMxffJJOWpP98Pg1g5hV05xWGk3tCNLgrTTnqKJLKlrlSHCk3UKBfpqS2+D4LEi+Peg3CWPJPzfkWY+DOWWyRalsa0DQXeLmh7NLlA4T9GAlxLr/OXoq76VB9MVPBYk8Az951p19nR1tmV/y/LKBT7V4OdVYyA2EsDObB+4nxowM6r6xUbAYuoiXBu+wBY7E7XzmXLu9oRiKGWKPHmHPl4257nJcAgFLkP9hcREz+AuW7j98zYY/zT7/TEm/6oUETq76pWfONB4+MWw7TWFEHatoZtqW7hSeCRLOxP9zkpxnNREwokOjxHivOBhJ2Gd2vv0Fp2gDeIfjYIIPuUz5KKTOe0yxAYkDFt1HKWwvhCaHsAM/TEk3/4YvAh+CgY6iirG3bF+gWttNQfaxL7V7uMz8hgyk2zGQWiGB2Jw3AburQNJAc4E5ctgHqdTtUPIRHreYZwMz8/JECNlr/mHWsvUh9hxwuz1RbgCER9NZYaZBH9OdNHX7IEaTgNxYzHZ6h97Gm+usyB2Ci/sgCBTpgGFgeiMJYU2gdE9ut2Nd/VG64w8XXTvdt+yB7CoS3kdDiem5KXqr6mSv2x7OqDS1vbtP8A/fhkiBuTSvTVrIYVwUkN84k7oKW7cXBdM6F0eM5dn6Zp237Ot0WL/x0AOFwAEO1Hqr1EEqbxnTDY4HQMp/GPjVJRdskJERo7mD0sxhmDO3Mn5V9tlY8TJhD30bDHM9fXvBoyBAVrHHqnlzJKieqNn026Tj8lM9oWUgP5XkvYyvZ2YUin4/079SA/g+XlQ5JgUD85UCf+z/xBbI2qohMuEZeDs8YAHqVdEXIr8m7h7LFOvUNRjPUXvSj4EhCXAju590CB8uDG0PjhOwv/dlOSZCfIIz8zRayQHSHUPA072iwniY+SKegPrKwjpYxXNT7/75IhcO3riLF1DNNUucGxwlw828Gz1OPGAIc9UTES0Jz4lGKHmmvhpkn5oZw42s6AGooFvJUhwrOKr6QAoEEgDgAlylSYylXzjaB2Fw4Upd2M3eqDrw=";
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            this.textBox2.Clear();
        }
    }
}
