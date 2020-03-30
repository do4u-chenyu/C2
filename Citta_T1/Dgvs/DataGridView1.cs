using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HZH_Controls;
using HZH_Controls.Controls;
using HZH_Controls.Forms;

namespace Citta_T1
{
    public partial class DataGridView1 : UserControl
    {
        public DataGridView1()
        {
            InitializeComponent();
        }

        public void LogUpdate(string log)
        {
            this.textBox1.AppendText(log + "\r\n");


            if (this.textBox1.Lines.Length > 10000)
            {
                string[] newlines = new string[10000];
                Array.Copy(this.textBox1.Lines, this.textBox1.Lines.Length - 10000, newlines, 0, 10000);
                this.textBox1.Lines = newlines;
            }


            
        }


    }
}
