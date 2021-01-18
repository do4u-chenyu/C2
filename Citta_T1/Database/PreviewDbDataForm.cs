using C2.Core;
using C2.Model;
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
    public delegate void MaxNumChangedEventHandler(object sender, int maxNum);
    public partial class PreviewDbDataForm : Form
    {
        public DataGridView DataGridView { get { return this.dataGridView; } }
        public int MaxNum { get { return GetIntFromTextBox(); } }

        public MaxNumChangedEventHandler MaxNumChanged;
        private DatabaseItem databaseItem;

        public PreviewDbDataForm()
        {
            InitializeComponent();
            this.dataGridView.DoubleBuffered(true);
            this.MaxNumChanged += new MaxNumChangedEventHandler(OnDataGridViewMaxNumChanged);
        }
        private void Init(DatabaseItem dbi)
        {
            this.databaseItem = dbi;
        }
        public bool Flush(DatabaseItem dbi)
        {
            this.Init(dbi);
            Table table = dbi.DataTable;
            IDAO dao = DAOFactory.CreateDAO(dbi);
            if (!dao.TestConn())
                return false;

            try
            {
                dao.FillDGVWithTbContent(dataGridView, table, MaxNum);
            }
            catch
            {
                return false;
            }
            return true;
        }
        private void OnDataGridViewMaxNumChanged(object sender, int maxNum)
        {
            IDAO dao = DAOFactory.CreateDAO(databaseItem);
            dao.FillDGVWithTbContent(this.dataGridView, databaseItem.DataTable, MaxNum);
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
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MaxNumChanged(this, MaxNum);
        }
    }
}
