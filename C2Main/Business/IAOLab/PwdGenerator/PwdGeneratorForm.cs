using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using C2.Utils;

namespace C2.Business.IAOLab.PwdGenerator
{
    public partial class PwdGeneratorForm : Form
    {

        public PwdGeneratorForm()
        {
            InitializeComponent();
        }

        public List<string> birthday = new List<string>();  //birthday存入所有被排序的值
        public List<string> GetBirth(string tbBirth)     //birth
        {
            if (tbBirth == "出生日期" || tbBirth == string.Empty || tbBirth == "出生日" || tbBirth == "出生" || tbBirth == "出" || tbBirth.Length != 8)
            {
                return null;
            }
            else
            {
                int start = 2;
                //string birth = "19980405";
                string day = tbBirth.Substring(tbBirth.Length - start);
                string month = tbBirth.Substring(4, 2);
                string year = tbBirth.Substring(0, 4);
                string year1 = tbBirth.Substring(2, 2);
                birthday.Add(year + month + day);
                birthday.Add(year);
                birthday.Add(day);
                birthday.Add(month + day);
                birthday.Add(year1);
                birthday.Add(year1 + month + day);

                return birthday;
            }

        }

        public List<string> GetRepeat(List<string> li)    //较短字符重复
        {
            //List<string> repeat = new List<string>();
            
            for (int i = 0; i < li.Count; i++)
            {
                if (li[i].Length < 2 && li[i].Length > 0)
                {
                    birthday.Add(li[i] + li[i]);

                }

            }
            return birthday;         
        }

        public List<string> GetSpecial(string[] tbSpecial)     //special number
        {
            try
            {
                if (tbSpecial == ("特殊数字  ' ' 分隔").Split(' ') || tbSpecial[0] == string.Empty)
                {
                    List<string> numList = new List<string>() { "520", "5201314", "1314", "123456", "123123", "1314", "123", "321", "52","666666","111","123456789","12345678","1234567","111111","222222","333333","4444444","555555","888888","777777","999999" };
                    for (int i = 0; i < numList.Count; i++)
                    {
                        birthday.Add(numList[i]);
                    }
                    return birthday;
                }
                else
                {
                    List<string> numList = new List<string>() { "520", "5201314", "1314", "123456", "123123", "1314", "123", "321", "52", "666666", "111", "123456789", "12345678", "1234567", "111111", "222222", "333333", "4444444", "555555", "888888", "777777", "999999" };
                    for (int i = 0; i < numList.Count; i++)
                    {
                        birthday.Add(numList[i]);
                    }
                    foreach(string item in tbSpecial)
                        birthday.Add(item);
                    return birthday;
                }
            }
            catch
            {
                return null;
            }

            

        }

        public List<string> GetEmail(string tbemail)
        {
            if (tbemail == "邮箱" || tbemail == string.Empty)
            {
                return null;
            }
            else
            {
                //string tbemail = "1037538120@qq.com";
                string[] num = tbemail.Split('@');
                if (num[0] == string.Empty)
                {
                    return null;
                }
                else
                {
                    if (num.Length > 7)
                    {
                        birthday.Add(num[0].Substring(0, 3));
                        birthday.Add(num[0].Substring(3, 4));
                        birthday.Add(num[0].Substring(6, num.Length - 6));
                    }
                    else
                    {
                        birthday.Add(num[0]);
                    }

                    return birthday;
                }
                
            }
        }
        public List<string> Getht(int l, string s)  //获得输入的前几位和后几位
        {

            if (s == "伴侣手机号" || s == string.Empty ||s== "历史密码  ' '分隔" )
            {
                return null;
            }
            else
            {
                if (s.Length > l)
                {
                    birthday.Add(s.Substring(0, l));
                    birthday.Add(s.Substring(s.Length - l, l));
                }

                return birthday;
            }
        }


        public List<string> ShortName(string tbName)  //tBName
        {
            try
            {
                if (tbName == "伴侣姓名简拼" || tbName == string.Empty)
                {
                    return null;
                }
                else
                {
                    //string fullName = "xuxiao";
                    //string name = "xx";
                    string Name = tbName.ToUpper();
                    for (int i = 0; i < tbName.Length; i++)
                    {
                        birthday.Add(Name.Substring(i, 1));  //
                        birthday.Add(tbName.Substring(i, 1));
                    }
                    birthday.Add(tbName);
                    birthday.Add(Name);
                    return birthday;
                }
            }
            catch 
            {
                return null;
            }
        }

        public List<string> Distinct(List<string> ll)     //去重
        {
            for (int i = 0; i < ll.Count; i++)
            {
                for (int j = ll.Count - 1; j > i; j--)
                {
                    if (ll[i] == ll[j])
                        ll.RemoveAt(j);
                }
            }
            return ll;
        }


        public List<string> GetFullname(string[] tbFullName)
        {
            try
            {
                if (tbFullName.ToString() == "伴侣姓名全拼  ' '分隔" || tbFullName.ToString() == string.Empty || tbFullName.ToString() == "" || tbFullName[0] == string.Empty)
                {
                    return null;
                }
                else if (tbFullName.Length > 1)
                {

                    string Xing = tbFullName[0];
                    if (Xing.Length > 1)
                    {
                        birthday.Add(Xing.ToUpper());
                        string Ming = tbFullName[1];
                        birthday.Add(Xing);
                        birthday.Add(Xing.Substring(0, 1).ToUpper() + Xing.Substring(1));
                        birthday.Add(Ming);
                        return birthday;
                    }
                    else
                    {
                        return null;
                    }

                }

                else if (tbFullName.Length == 1)
                {
                    birthday.Add(tbFullName[0]);
                    return birthday;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
           

        }

        public List<string> Filter(List<string> list)   //过滤掉汉字，并且将特殊字符不放在开头
        {
            List<string> result1 = new List<string>();
            foreach (string item in list)
            {

                string pattern = @"^[a-zA-Z0-9_][A-Za-z0-9@+#_|$%&\*]*$";
                string match = (Regex.Match(item, pattern).Value);
                if(match.Length>0)

                    result1.Add(match);
            }
            return result1;
        }



        private List<string> Group(List<string> list)    //组合元素
        {
            List<string> tmpList = new List<string>();
            foreach (string item in list)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (item != list[i])
                    {
                        string newitem = item + list[i];
                        if (newitem.Length > 6)
                        {
                            tmpList.Add(newitem);
                        }

                    }

                }
            }
            return tmpList;
        }

        private List<string> Clean(List<string> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if(list[i] == list[i])
                    list.Remove(list[i]);
            }
                
            return list;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (tBName.Text == "姓名简拼" || tBName.Text == string.Empty || tBName.Text.Substring(0,1) == OpUtil.StringBlank) 
            {
                MessageBox.Show("请输入姓名简拼");
                return;
            }

            if(tBWname.Text == "姓名全拼  ' '分隔" || tBWname.Text == string.Empty)
            {
                MessageBox.Show("请输入姓名全拼，姓，名用空格符隔开");
                return;
            }

            if(tBPhone.Text == "手机号" || tBPhone.Text == string.Empty || tBPhone.Text.Length != 11)
            {
                MessageBox.Show("请输入11位手机号");
                return;
            }

            string tbName = tBName.Text;
            string[] tbeName = tBEname.Text.Split(' ');
            string tbQq = tBQQ.Text;
            string tbBirth = tBBirth.Text;
            string tbEmail = tBMail.Text;

            string[] tbFullName = tBWname.Text.Split(' ');
            string[] tbWfName = tBWifeWname.Text.Split(' ');
            string tbPhone = tBPhone.Text;
           
            string tbWifeName = tBWifename.Text;
            string[] tbOldPass = tBOldpass.Text.Split(' ');
            string[] tbSpecial = tBSpecial.Text.Split(' ');
            string tbWPhone = tBWifePhone.Text;
            
            string[] tbSelf = tBSelf.Text.Split(' ');
            for (int i = 0; i < tbOldPass.Length; i++)
            {
                Getht(3, tbOldPass[i]);
                Getht(4, tbOldPass[i]);
            }
            if (tbeName[0] != string.Empty && tbeName[0] != "英文名")
                foreach(string item in tbeName)
                birthday.Add(item);
            GetFullname(tbFullName);
            GetFullname(tbWfName);
            GetBirth(tbBirth);
            GetSpecial(tbSpecial);
            ShortName(tbName);
            ShortName(tbWifeName);
            GetEmail(tbEmail);
            GetEmail(tbQq);
            Getht(3, tbPhone);
            Getht(3, tbWPhone);
            Getht(4, tbPhone);
            Getht(5, tbPhone);
            GetRepeat(ShortName(tbName));


            try
            {
                for (int i = 0; i < tbSelf.Length; i++)
                {
                    birthday.Add(tbSelf[i]);
                }
            }
            catch
            {
                return;
            }
            Distinct(birthday);
            List<string> small = new List<string>();  //small存放较短数据
            List<string> big = new List<string>();  //big存放较长数据

            foreach (string item in birthday)   //将较短数据取出，放置在最后
            {
                if (item.Length <= 2)
                {
                    small.Add(item);
                }
                else if (item.Length >= 4)
                {
                    big.Add(item);
                }
            }

            List<string> tmpList = Group(birthday);   //tmpList存放组合后的数据

            foreach (string item in big)
            {
                foreach (string item1 in small)
                    tmpList.Add(item + '@' + item1);

            }

            

            List<string> result = new List<string>();  //result存放

            string t = string.Join("\n", Filter(tmpList).ToArray()); ;

            PwdResultForm form2 = new PwdResultForm();
            form2.richTextBoxText = t;
            form2.S =  "当前共生成" + Filter(tmpList).Count + "条数据";
            form2.ShowDialog();
           
            Clean(birthday);

            //string t = string.Empty;
        }

        private void QQ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void Phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void Birth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void Wphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void Name_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                (sender as TextBox).SelectAll();
            });
        }

        private void Wname_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                (sender as TextBox).SelectAll();
            });
        }

        private void QQ_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                (sender as TextBox).SelectAll();
            });
        }

        private void Birth_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                (sender as TextBox).SelectAll();
            });
        }

        private void Phone_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                (sender as TextBox).SelectAll();
            });
        }

        private void Ename_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                (sender as TextBox).SelectAll();
            });
        }

        private void User_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                (sender as TextBox).SelectAll();
            });
        }

        private void Special_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                (sender as TextBox).SelectAll();
            });
        }

        private void Email_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                (sender as TextBox).SelectAll();
            });
        }

        private void OldPass_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                (sender as TextBox).SelectAll();
            });
        }

        private void Self_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                (sender as TextBox).SelectAll();
            });
        }

        private void WifeName_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                (sender as TextBox).SelectAll();
            });
        }

        private void WFName_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                (sender as TextBox).SelectAll();
            });
        }

        private void WPhone_Click(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                (sender as TextBox).SelectAll();
            });
        }

        

    }
}
