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
    public partial class DataGridView2 : UserControl
    {
        public DataGridView2()
        {
            InitializeComponent();
        }

        private void ucDataGridView2_Load(object sender, EventArgs e)
        {
            List<DataGridViewColumnEntity> lstColumns = new List<DataGridViewColumnEntity>();
            // 表头
            lstColumns.Add(new DataGridViewColumnEntity() { DataField = "ID", HeadText = "编号", Width = 70, WidthType = SizeType.Absolute });
            lstColumns.Add(new DataGridViewColumnEntity() { DataField = "Info", HeadText = "日志", Width = 200, WidthType = SizeType.Percent });
            this.ucDataGridView2.Columns = lstColumns;
            //this.ucDataGridView1.IsShowCheckBox = true;
            List<object> lstSource = new List<object>();
            for (int i = 0; i < 20; i++)
            {
                LogModel model = new LogModel()
                {
                    ID = i.ToString(),
                    Info = "DEBUG: " + i,
                };
                lstSource.Add(model);
            }

            this.ucDataGridView2.DataSource = lstSource;
            this.ucDataGridView2.First();
        }
    }
}
