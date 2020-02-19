using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1
{
    public partial class DataGridView0 : UserControl
    {
        private string overViewFilePath = ""; // Properties.Resources.text
        public DataGridView0()
        {
            InitializeComponent();
            //InitializeDgv("");
        }

        private void DataGridView_Load(object sender, EventArgs e)
        {
            //_InitializeRows();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
