using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1
{
    public partial class loginform : Form
    {
        public string login_name;
        public loginform()
        {
            InitializeComponent();
        }

  

        private void loginform_Load(object sender, EventArgs e)
        {

        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm1 = new MainForm();
            mainForm1.Tag= usernamecomboBox.Text;
            mainForm1.ShowDialog();
            this.Close();
        }
    }
}
