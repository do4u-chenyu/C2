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
        private ContextMenuStrip contextMenuStrip;
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
        private Table SelectTable
        {
            get
            {
                if (tableListBox.SelectedItem != null)
                    return new Table(SelectDatabaseItem.User, tableListBox.SelectedItem.ToString());
                else
                    return null;
            }
        }

        public C2SqlOperatorView(OperatorWidget operatorWidget) : base(operatorWidget)
        {
            InitializeComponent();
            InitializeConnection();
            InitializeExecuteSql();
            InitializePreviewTableContextMenu(); // 如果放在Design.cs里，VS2019设计器会报错打不开，故放在这里初始化
            LoadOption();
        }

        private void InitializePreviewTableContextMenu()
        {
            contextMenuStrip = new ContextMenuStrip(this.components);

            ToolStripMenuItem previewTableMenuItem = new ToolStripMenuItem("预览表");
            previewTableMenuItem.Click += PreviewTableMenuItem_Click;
            contextMenuStrip.Items.Add(previewTableMenuItem);
            previewTableMenuItem.ToolTipText = "仅预览数据表前一千行数据";

            ToolStripMenuItem copyTableNameMenuItem = new ToolStripMenuItem("复制表名");
            copyTableNameMenuItem.Click += CopyTableNameMenuItem_Click;
            contextMenuStrip.Items.Add(copyTableNameMenuItem);

            ToolStripMenuItem codeSnippetMenuItem = new ToolStripMenuItem("一键查询");
            contextMenuStrip.Items.Add(codeSnippetMenuItem);
        }

        private void PreviewTableMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectDatabaseItem == null || SelectTable == null)
                return;
            if (!DbUtil.TestConn(SelectDatabaseItem))
            {
                HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                return;
            }
            if (SelectDatabaseItem.Type == DatabaseType.Oracle)
                DbUtil.FillDGVWithTbContent(gridOutput, new OraConnection(SelectDatabaseItem), SelectTable, OpUtil.PreviewMaxNum);
            else if (SelectDatabaseItem.Type == DatabaseType.Hive)
            {
                string sql = string.Format("select * from {0} limit{1}", SelectTable.Name, OpUtil.PreviewMaxNum);
                LoadHiveData(this.comboBoxDataBase.Text, sql); 
            }  
            else
                return;


        }

        private void CopyTableNameMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(tableListBox.SelectedItem.ToString());
        }

        private void InitializeExecuteSql()
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
            if (this.comboBoxConnection.Items.Count == 1)
            {
                this.comboBoxConnection.SelectedIndex = 0; // 如果只有一个选项，默认就是它，此时不需要用户再去选了。
            }
        }

        private void BnConnect_Click(object sender, System.EventArgs e)
        {
            if (SelectDatabaseItem == null)
                return;
            List<string> users;
            if (SelectDatabaseItem.Type == DatabaseType.Hive)
            {
                HiveConnection hiveConn = new HiveConnection(SelectDatabaseItem);
                if (!hiveConn.TestConn())
                {
                    HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                    return;
                }
                //刷新架构
                users = hiveConn.GetHiveDatabases();
            }
            else if (SelectDatabaseItem.Type == DatabaseType.Oracle)
            {   
                //连接数据库
                OraConnection conn = new OraConnection(SelectDatabaseItem);
                if (!DbUtil.TestConn(conn))
                {
                    HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                    return;
                }
                //刷新架构
                users = DbUtil.GetUsers(conn);
            }
            else
                users = new List<string>();
          
            this.comboBoxDataBase.Items.Clear();
            if (users == null || users.Count <= 0)
                return;
            if (databaseItems != null && databaseItems.Count > 0)
            {
                this.comboBoxDataBase.Text = users.Find(x => x.Equals(SelectDatabaseItem.User.ToUpper())) == null ? "选择架构" : SelectDatabaseItem.User.ToUpper();
                // hive加载框架
                if (string.Equals("选择架构", this.comboBoxDataBase.Text))
                    this.comboBoxDataBase.Text = users[0];
                this.comboBoxDataBase.Items.AddRange(users.ToArray());
            }

        }

        private void BnView_Click(object sender, System.EventArgs e)
        {
            if (SelectDatabaseItem == null || string.IsNullOrEmpty(this.comboBoxDataBase.Text) )
                return;
            List<Table> tables;
            //Hive 连接数据库
            if (SelectDatabaseItem.Type == DatabaseType.Hive)
            {
                HiveConnection hiveConn = new HiveConnection(SelectDatabaseItem);
                if (!hiveConn.TestConn())
                    return;
                tables = hiveConn.GetTablesByDB(this.comboBoxDataBase.Text);
            }
            else if (SelectDatabaseItem.Type == DatabaseType.Oracle)
            {
                OraConnection conn = new OraConnection(SelectDatabaseItem);
                if (!DbUtil.TestConn(conn))
                    return;
                //刷新数据表
                tables = DbUtil.GetTablesByUser(conn, this.comboBoxDataBase.Text);
            }
            else
                tables = new List<Table>();
          
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
            {
                HelpUtil.ShowMessageBox(HelpUtil.DatabaseItemIsNull);
                return;
            }
            if (SelectDatabaseItem.Type == DatabaseType.Hive)
            {
                LoadHiveData(this.comboBoxDataBase.Text, textEditorControl1.Text);
                return;

            }
            try
            {
                using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor)) // Display the hourglass
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
        private void LoadHiveData(string database,string sql)
        {
            HiveConnection hiveConnection = new HiveConnection(SelectDatabaseItem);
            string tbContent = hiveConnection.GetSQLResult(database,sql);
            List<string[]> results = new List<string[]>();
            foreach (string row in tbContent.Split(OpUtil.DefaultLineSeparator))
                results.Add(row.Split(OpUtil.DefaultFieldSeparator));

            // Grab all the column names
            gridOutput.Rows.Clear();
            gridOutput.Columns.Clear();
            foreach (string[] row in results)
            {
                if (gridOutput.Columns.Count == 0)
                {
                    for (int i = 0; i < row.Length; i++)
                        gridOutput.Columns.Add(i.ToString(), row[i]);
                    continue;
                }
                gridOutput.Rows.Add(row);
            }
        }
        protected override void SaveOption()
        {
            if (SelectDatabaseItem == null)
                return;
            this.operatorWidget.Option.Clear();
            this.operatorWidget.Option.SetOption("sqlText", textEditorControl1.Text);
            this.operatorWidget.Option.SetOption("connection", SelectDatabaseItem.AllDatabaseInfo);
            this.operatorWidget.Option.SetOption("maxNum", maxNumTextBox.Visible ? maxNumTextBox.Text : "inf");
        }

        //右键打开菜单
        private void TableListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1 || e.Button != MouseButtons.Right)
                return;
            int posindex = tableListBox.IndexFromPoint(e.X, e.Y);
            if (posindex >= 0 && posindex < tableListBox.Items.Count)
            {
                tableListBox.SelectedIndex = posindex;
                contextMenuStrip.Show(tableListBox, e.X, e.Y);
                tableListBox.Refresh();
            }
        }

        private void LoadOption()
        {
            if (!String.IsNullOrEmpty(this.operatorWidget.Option.GetOption("sqlText")))
            {
                textEditorControl1.Text = this.operatorWidget.Option.GetOption("sqlText");
            }
            if (!String.IsNullOrEmpty(this.operatorWidget.Option.GetOption("connection")))
            {
                string allDatabaseInfo = this.operatorWidget.Option.GetOption("connection");
                DatabaseItem oldDatabaseItem = new DatabaseItem(allDatabaseInfo);
                for(int i = 0;i<this.comboBoxConnection.Items.Count;i++)
                {
                    if (oldDatabaseItem.PrettyDatabaseInfo.Equals(this.comboBoxConnection.Items[i].ToString()))
                    {
                        this.comboBoxConnection.SelectedIndex = i;
                        this.comboBoxConnection.Text = oldDatabaseItem.PrettyDatabaseInfo;
                        break;
                    }
                }
            }
        }

        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            if (this.comboBoxConnection.Text == String.Empty)
            {
                HelpUtil.ShowMessageBox("请选择数据库");
                return notReady;
            }
            if (this.textEditorControl1.Text == String.Empty)
            {
                HelpUtil.ShowMessageBox("请输入sql命令");
                return notReady;
            }
            return !notReady;
        }

        private void partialRadioButton_Click(object sender, EventArgs e)
        {
            this.maxNumTextBox.Visible = true;
        }

        private void allRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.maxNumTextBox.Visible = false;
            
        }
        protected override void ConfirmButton_Click(object sender, EventArgs e)
        {
            int maxNum;
            if (maxNumTextBox.Visible && (String.IsNullOrEmpty(maxNumTextBox.Text) || !int.TryParse(maxNumTextBox.Text, out maxNum) || maxNum <= 0))
            {
                HelpUtil.ShowMessageBox(HelpUtil.InvalidMaxNum);
                return;
            }
            base.ConfirmButton_Click(sender, e);
        }
    }
}
