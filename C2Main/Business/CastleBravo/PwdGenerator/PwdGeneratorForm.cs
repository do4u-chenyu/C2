using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.PwdGenerator
{
    public partial class PwdGeneratorForm : Form
    {

        public PwdGeneratorForm()
        {
            InitializeComponent();
        }

        public List<string> birthday = new List<string>();  //birthday存入所有被排序的值
        

        public List<string> GetBirth(string tbbirth)     //birth
        {

            int start = 2;
            //string birth = "19980405";
            string day = tbbirth.Substring(tbbirth.Length - start);
            string month = tbbirth.Substring(0, 4);
            string year = tbbirth.Substring(4, 2);
            string year1 = tbbirth.Substring(2, 2);
            List<string> birthday = new List<string>();
            birthday.Add(year + month + day);
            birthday.Add(year);
            birthday.Add(day);
            birthday.Add(month + day);
            birthday.Add(year1);
            birthday.Add(year1 + month + day);

            return birthday;

        }



        public List<string> GetRepeat(List<string> li)    //较短字符重复
        {
            //List<string> repeat = new List<string>();
            for (int i = 0; i < li.Count; i++)
            {
                if (li[i].Length <= 3)
                {
                    birthday.Add(li[i] + li[i]);
                }

            }
            return birthday;
        }

        public List<string> GetSpecial(string tbSpecial)     //special number
        {

            List<string> numList = new List<string>() { "520", "5201314", "1314", "iloveu", "iloveyou", "123456", "123123", "1314", "123", "321", "52" };
            for (int i = 0; i < numList.Count; i++)
            {
                birthday.Add(numList[i]);
            }
            birthday.Add(tbSpecial);

            return birthday;

        }

        public List<string> GetEmail(string tbemail)
        {
            //string tbemail = "1037538120@qq.com";
            string num = tbemail.Split('@')[0];
            if (num.Length > 7)
            {
                birthday.Add(num.Substring(0, 3));
                birthday.Add(num.Substring(3, 4));
                birthday.Add(num.Substring(6, num.Length - 6));
            }
            else
            {
                birthday.Add(num);
            }


            return birthday;
        }
        public List<string> Getht(int l, string s)  //获得输入的前几位和后几位
        {
            if (s.Length > l)
            {
                birthday.Add(s.Substring(0, l));
                birthday.Add(s.Substring(s.Length - l, l));
            }

            return birthday;
        }


        public List<string> ShortName(string tbName)  //tBName
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

        
        public List<string> Distinct(List<string> ll)
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
            string Xing = tbFullName[0];
            birthday.Add(Xing.ToUpper());
            string Ming = tbFullName[1];
            birthday.Add(Xing);
            birthday.Add(Xing.Substring(0, 1).ToUpper() + Xing.Substring(1));
            birthday.Add(Ming);

            return birthday;

        }

        private List<string> Group(List<string> list)
        {
            List<string> tmpList = new List<string>();
            int j = list.Count();
            foreach (string item in list)
            {
                for (int i = 0; i < j; i++)
                {
                    if (item == list[i])
                        continue;
                    string newItem = item + list[i] + "\n";
                    if(newItem.Length >= 6)
                        tmpList.Add(newItem);


                }

            }
            return tmpList;
        }


      

        private void button2_Click(object sender, EventArgs e)
        {

            string tbName = tBName.Text;
            string tbeName = tBEname.Text;
            string tbQq = tBQQ.Text;
            string tbBirth = tBBirth.Text;
            string tbEmail = tBMail.Text;
            string[] tbFullName = tBWname.Text.Split('|');
            string[] tbWfName = tBWifeWname.Text.Split('|');
            string tbPhone = tBPhone.Text;
            string tbWifeName = tBWifename.Text;
            string[] tbOldPass = tBOldpass.Text.Split('|');
            string tbSpecial = tBSpecial.Text;
            string tbwPhone = tBWifePhone.Text;
            string[] tbself = tBSelf.Text.Split('|');
            for (int i = 0; i < tbOldPass.Length; i++)
            {
                Getht(3, tbOldPass[i]);
                Getht(4, tbOldPass[i]);
            }
            for (int i = 0; i < tbself.Length; i++)
            {
                birthday.Add(tbself[i]);
            }
            birthday.Add(tbeName);
            GetFullname(tbFullName);
            GetFullname(tbWfName);
            GetBirth(tbBirth);
            GetSpecial(tbSpecial);
            ShortName(tbName);
            ShortName(tbWifeName);
            GetEmail(tbEmail);
            GetEmail(tbQq);
            Getht(3, tbPhone);
            Getht(3, tbwPhone);
            Getht(4, tbPhone);
            Getht(5, tbPhone);
            GetRepeat(ShortName(tbName));
            Distinct(birthday);

            List<string> tmpList = Group(birthday);

            string s = string.Join(",", birthday.ToArray());
            string t = string.Join("", tmpList.ToArray());
            
            PwdResultForm form1 = new PwdResultForm();
            form1.richTextBoxText = t;
            form1.ShowDialog();

        }
    }
}
