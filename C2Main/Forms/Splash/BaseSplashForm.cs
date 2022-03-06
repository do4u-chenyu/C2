using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Forms.Splash
{
    public partial class BaseSplashForm : Form
    {
        public BaseSplashForm()
        {
            InitializeComponent();
        }

        public void AddItem(Bitmap icon, string name, string desc)
        {
            DataGridViewRow dgvr = new DataGridViewRow();

            DataGridViewImageCell gdvic = new DataGridViewImageCell
            {
                Value = icon
            };

            DataGridViewTextBoxCell dgvtbc1 = new DataGridViewTextBoxCell
            {
                Value = name
            };

            DataGridViewTextBoxCell dgvtbc2 = new DataGridViewTextBoxCell
            {
                Value = desc
            };

            dgvr.Cells.Add(gdvic);
            dgvr.Cells.Add(dgvtbc1);
            dgvr.Cells.Add(dgvtbc2);
        }
    }
}
