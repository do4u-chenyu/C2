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
    public partial class PreviewTableSchema : Form
    {
        public DataGridView DataGridView { get { return this.dataGridView; } }
        public PreviewTableSchema()
        {
            InitializeComponent();
        }
    }
}
