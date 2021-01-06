using C2.Core;
using C2.Database;
using C2.Dialogs.Base;
using C2.Model;
using C2.Model.Widgets;
using C2.Utils;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C2.Dialogs.C2OperatorViews
{
    public partial class C2SqlOperatorView : C2BaseOperatorView
    {
        private List<DatabaseItem> databaseItems;
        private DatabaseItem SelectDatabaseItem
        {
            get
            {
                int idx = this.comboBoxConnection.SelectedIndex;
                if (idx >= 0 && idx < databaseItems.Count)
                    return databaseItems[idx];
                else
                    return null;
            }
        }

        public C2SqlOperatorView(OperatorWidget operatorWidget) : base(operatorWidget)
        {
            InitializeComponent();
            InitializeConnection();
            InitializaExecuteSql();
        }

        private void InitializaExecuteSql()
        {
            this.textEditorControl1.Text = OpTypeTransSql();

        }
        
        private string OpTypeTransSql()
        {
            switch (this.operatorWidget.OpType)
            {
                case OpType.MaxOperator: return "--select MAX(tmp.id) from tmp";
                case OpType.MinOperator: return "--select MIN(tmp.id) from tmp";
                case OpType.AvgOperator: return "--select AVG(tmp.id) from tmp";
                case OpType.DataFormatOperator: return "--select id,name,age from tmp";
                case OpType.RandomOperator: return "--select * from (select * from tmp order by dbms_random.value) where rownum< 3;";
                case OpType.FreqOperator: return "--select id,COUNT(id) from tmp group by id";
                case OpType.SortOperator: return "--select id from tmp order by id desc";
                case OpType.FilterOperator: return "--select id from tmp where id=9";
                case OpType.GroupOperator: return "--select name,age from tmp group by age,name order by age;";
                default: return string.Empty;
            }
        }

        private void InitializeConnection()
        {
            List<string> names = new List<string>() ;
            databaseItems = Global.GetDataSourceControl().GetAllExternalData();
            if(databaseItems != null && databaseItems.Count > 0)
            {
                databaseItems.ForEach(d => names.Add(d.PrettyDatabaseInfo));
                this.comboBoxConnection.Items.AddRange(names.ToArray());
            }
        }

        private void BnConnect_Click(object sender, System.EventArgs e)
        {
            if (SelectDatabaseItem == null)
                return;

            //连接数据库
            OraConnection conn = new OraConnection(SelectDatabaseItem);
            if (!DbUtil.TestConn(conn))
            {
                HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                return;
            }

            //刷新架构
            List<string> users = DbUtil.GetUsers(conn);
            this.comboBoxDataBase.Items.Clear();
            if (users == null || users.Count <= 0)
                return;
            if (databaseItems != null && databaseItems.Count > 0)
            {
                this.comboBoxDataBase.Text = users.Find(x => x.Equals(SelectDatabaseItem.User.ToUpper())) == null ? "选择架构" : SelectDatabaseItem.User.ToUpper();
                this.comboBoxDataBase.Items.AddRange(users.ToArray());
            }

        }

        private void BnView_Click(object sender, System.EventArgs e)
        {
            if (SelectDatabaseItem == null || string.IsNullOrEmpty(this.comboBoxDataBase.Text) )
                return;

            //连接数据库
            OraConnection conn = new OraConnection(SelectDatabaseItem);
            if (!DbUtil.TestConn(conn))
                return;

            //刷新数据表
            List<Table> tables = DbUtil.GetTablesByUser(conn, this.comboBoxDataBase.Text);
            this.tableListBox.Items.Clear();
            if (tables == null || tables.Count <= 0)
                return;
            foreach (Table table in tables)
                tableListBox.Items.Add(table.Name);
        }

        private void ComboBoxConnection_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.comboBoxDataBase.Items.Clear();
            this.comboBoxDataBase.Text = string.Empty;

            this.tableListBox.Items.Clear();
        }

        private void ComboBoxDataBase_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.tableListBox.Items.Clear();
        }

        private void bnExecute_Click(object sender, System.EventArgs e)
        {
            if (SelectDatabaseItem == null)
                HelpUtil.ShowMessageBox(HelpUtil.DatabaseItemIsNull);
            try
            {
                using (new CursorUtil.UsingCursor(Cursors.WaitCursor)) // Display the hourglass
                {
                    using (OracleConnection conn = new OracleConnection(new OraConnection(SelectDatabaseItem).ConnectionString))
                    {
                        conn.Open();
                        string sql = textEditorControl1.Text;
                        OracleCommand comm = new OracleCommand(sql, conn);
                        using (OracleDataReader rdr = comm.ExecuteReader())
                        {
                            // Grab all the column names
                            gridOutput.Rows.Clear();
                            gridOutput.Columns.Clear();
                            for (int i = 0; i < rdr.FieldCount; i++)
                                gridOutput.Columns.Add(i.ToString(), rdr.GetName(i));

                            // Read up to 1000 rows
                            int rows = 0;
                            while (rdr.Read() && rows < 1000)
                            {
                                string[] objs = new string[rdr.FieldCount];
                                for (int f = 0; f < rdr.FieldCount; f++) objs[f] = rdr[f].ToString();
                                gridOutput.Rows.Add(objs);
                                rows++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) // Better catch in case they have bad sql
            {
                HelpUtil.ShowMessageBox(ex.Message);
            }
        }
        protected override void SaveOption()
        {
            this.operatorWidget.Option.Clear();
            this.operatorWidget.Option.SetOption("sqlText", textEditorControl1.Text);
            this.operatorWidget.Option.SetOption("connection", SelectDatabaseItem.AllDatabaseInfo);
        }
    }
}
