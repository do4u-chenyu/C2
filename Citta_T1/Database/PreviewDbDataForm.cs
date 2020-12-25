using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Database
{
    public partial class PreviewDbDataForm : Form
    {
        public DataGridView DataGridView { get { return this.dataGridView; } }
        public int MaxNum { get { return GetIntFromTextBox(); } }
        public PreviewDbDataForm()
        {
            InitializeComponent();
            this.dataGridView.DoubleBuffered(true);
        }
        private int GetIntFromTextBox()
        {
            int x;
            if (int.TryParse(textBox1.Text, out x))
            {
                return x;
            }
            else
            {
                HelpUtil.ShowMessageBox(HelpUtil.CastStringToIntFailedInfo);
                return 0;
            }
        }
    }
}
