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

        public void ucDataGridView1_Load(List<object> scheduleLogs)
        {
            List<DataGridViewColumnEntity> lstColumns = new List<DataGridViewColumnEntity>();
            // 表头
            lstColumns.Add(new DataGridViewColumnEntity() { DataField = "TripleInfo", HeadText = "三元组信息", Width = 50, WidthType = SizeType.Percent });
            lstColumns.Add(new DataGridViewColumnEntity() { DataField = "Status", HeadText = "运行状态", Width = 50, WidthType = SizeType.Percent });
            this.ucDataGridView1.Columns = lstColumns;
            //this.ucDataGridView1.IsShowCheckBox = true;
            /*
            List<object> lstSource = new List<object>();
            for (int i = 0; i < 2; i++)
            {
                ScheduleLog model = new ScheduleLog()
                {
                    ID = i.ToString(),
                    TripleInfo = "三元组信息——" + i,
                    Status = "状态——" + i,
                };
                lstSource.Add(model);
            }
            */
            this.ucDataGridView1.DataSource = scheduleLogs;
            this.ucDataGridView1.First();
        }

        public void ucDataGridView1_Update(List<object> scheduleLogs)
        {
            this.ucDataGridView1.DataSource = scheduleLogs;
        }


    }
}
