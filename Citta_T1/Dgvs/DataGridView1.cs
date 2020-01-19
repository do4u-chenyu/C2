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

        private void ucDataGridView1_Load(object sender, EventArgs e)
        {
            List<DataGridViewColumnEntity> lstColumns = new List<DataGridViewColumnEntity>();
            // 表头
            lstColumns.Add(new DataGridViewColumnEntity() { DataField = "ID", HeadText = "编号", Width = 70, WidthType = SizeType.Absolute });
            lstColumns.Add(new DataGridViewColumnEntity() { DataField = "Name", HeadText = "姓名", Width = 50, WidthType = SizeType.Percent });
            lstColumns.Add(new DataGridViewColumnEntity() { DataField = "Age", HeadText = "年龄", Width = 50, WidthType = SizeType.Percent });
            lstColumns.Add(new DataGridViewColumnEntity() { DataField = "Birthday", HeadText = "生日", Width = 50, WidthType = SizeType.Percent, Format = (a) => { return ((DateTime)a).ToString("yyyy-MM-dd"); } });
            lstColumns.Add(new DataGridViewColumnEntity() { DataField = "Sex", HeadText = "性别", Width = 50, WidthType = SizeType.Percent, Format = (a) => { return ((int)a) == 0 ? "女" : "男"; } });
            this.ucDataGridView1.Columns = lstColumns;
            //this.ucDataGridView1.IsShowCheckBox = true;
            List<object> lstSource = new List<object>();
            for (int i = 0; i < 20; i++)
            {
                TestGridModel model = new TestGridModel()
                {
                    ID = i.ToString(),
                    Age = 3 * i,
                    Name = "姓名——" + i,
                    Birthday = DateTime.Now.AddYears(-10),
                    Sex = i % 2
                };
                lstSource.Add(model);
            }

            this.ucDataGridView1.DataSource = lstSource;
            this.ucDataGridView1.First();
        }
    }
}
