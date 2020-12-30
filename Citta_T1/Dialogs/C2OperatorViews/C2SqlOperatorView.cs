using C2.Core;
using C2.Database;
using C2.Dialogs.Base;
using C2.Model;
using C2.Model.Widgets;
using C2.Utils;
using System.Collections.Generic;

namespace C2.Dialogs.C2OperatorViews
{
    public partial class C2SqlOperatorView : C2BaseOperatorView
    {
        private List<DatabaseItem> databaseItems;

        public C2SqlOperatorView(OperatorWidget operatorWidget) : base(operatorWidget)
        {
            InitializeComponent();
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            List<string> names = new List<string>() ;
            databaseItems = Global.GetDataSourceControl().GetAllExternalData();
            if(databaseItems != null && databaseItems.Count > 0)
            {
                databaseItems.ForEach(d => names.Add(d.PrettyDatabaeInfo));
                this.comboBoxConnection.Items.AddRange(names.ToArray());
            }
        }

        private void BnConnect_Click(object sender, System.EventArgs e)
        {
            DatabaseItem selectDatabaseItem = new DatabaseItem();
            int idx = this.comboBoxConnection.SelectedIndex;
            if (idx >= 0 && idx < databaseItems.Count)
                selectDatabaseItem = databaseItems[idx];

            //连接数据库
            OraConnection conn = new OraConnection(selectDatabaseItem);
            if (!DbUtil.TestConn(conn, true))
                return;
            //刷新架构
            List<string> users = DbUtil.GetUsers(conn);

            this.comboBoxDataBase.Items.Clear();
            if (databaseItems != null && databaseItems.Count > 0)
            {
                this.comboBoxDataBase.Text = users.Find(x => x.Equals(selectDatabaseItem.User.ToUpper())) == null ? "选择架构" : selectDatabaseItem.User.ToUpper();
                this.comboBoxDataBase.Items.AddRange(users.ToArray());
            }

        }
    }
}
