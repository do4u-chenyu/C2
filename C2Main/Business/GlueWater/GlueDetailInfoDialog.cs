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

            // 列头高度大小
            dataGridView.ColumnHeadersHeight = 30;
            // 列头居中
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#e5f8fc");

        }

        public void RefreshDGV()
        {
            dataGridView.DataSource = DetailTable;

            int width = 0;
            for (int i = 0; i < this.dataGridView.Columns.Count; i++)
            {
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
