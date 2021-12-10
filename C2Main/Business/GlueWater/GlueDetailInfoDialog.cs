using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.GlueWater
{
    public partial class GlueDetailInfoDialog : Form
    {
        public DataTable DetailTable;
        public GlueDetailInfoDialog()
        {
            InitializeComponent();
            DetailTable = new DataTable();
            dataGridView1.DataSource = null;
        }

        public void RefreshDGV()
        {
            dataGridView1.DataSource = DetailTable;
            dataGridView1.Refresh();
        }
    }
}
