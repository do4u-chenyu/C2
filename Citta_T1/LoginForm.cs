using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1
{
    public partial class loginform : Form
    {
        public string login_name;
        ArrayList al = new ArrayList();
        public loginform()
        {
            InitializeComponent();
        }

  

        private void loginform_Load(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("userinfo.data", FileMode.OpenOrCreate);
            FileStream fs_lastname = new FileStream("lastname.data", FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            if(fs.Length>0)
            {
                al = bf.Deserialize(fs) as ArrayList;
                foreach (string item in al)
                {
                    this.usernamecomboBox.Items.Add(item);
                }
            }
            
            fs.Close();
            if (fs_lastname.Length > 0)
            {
                StreamReader sr = new StreamReader(fs_lastname);
                usernamecomboBox.Text= sr.ReadLine();
                sr.Close();
            }
            fs_lastname.Close();
            logincheckBox.Checked = true;
        }


        private void loginbutton_Click(object sender, EventArgs e)
        {
            string name = this.usernamecomboBox.Text;
            if (this.logincheckBox.Checked)
            {
                if (!al.Contains(name))
                {
                    FileStream fs = new FileStream("userinfo.data", FileMode.Create); //创建新文件user.data
                    BinaryFormatter bf = new BinaryFormatter(); //创建二进制序列化对象
                    al.Add(name);
                    bf.Serialize(fs, al); //序列化到fs
                    fs.Close();
                }
            }
            if (usernamecomboBox.Text != "")
            {
                this.Hide();
                MainForm mainForm1 = new MainForm();
                mainForm1.Tag = usernamecomboBox.Text;
                mainForm1.ShowDialog();
                this.Close();
            }
            FileStream fs_lastname = new FileStream("lastname.data", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs_lastname);
            sw.Write(name);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs_lastname.Close();

        }
    }
}
