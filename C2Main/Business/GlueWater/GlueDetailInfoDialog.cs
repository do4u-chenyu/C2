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
            dataGridView.DataSource = null;
        }

        public void RefreshDGV()
        {
            dataGridView.DataSource = DetailTable;
            
            int width = 0;
            for (int i = 0; i < this.dataGridView.Columns.Count; i++)
            {
                //取消DataGridView排序按键，否则列标题无法居中
                this.dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dataGridView.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.DisplayedCells);
                width += this.dataGridView.Columns[i].Width;
            }
            if (width > this.dataGridView.Size.Width)
            {
                this.dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                this.dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            
            dataGridView.Refresh();
        }
    }
}
