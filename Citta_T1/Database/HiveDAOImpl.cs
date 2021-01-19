using C2.Model;
using C2.Utils;
using Hive2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Database
{
    public class HiveDAOImpl: BaseDAOImpl
    {
        private static readonly LogUtil log = LogUtil.GetInstance("HiveDAOImpl");
        private string getUserSQL = @"show databases";
        private string getTablesSQL = @"use {0};show tables;";
        private string getTableContentSQL = @"use {0};select * from {1} limit {2}";
        //private string getColNameByTablesSQL;
        private string getColNameByTableSQL = "desc {0}";

        public HiveDAOImpl(DatabaseItem dbi) : base(dbi) { }
        public HiveDAOImpl(DataItem di) : base(di) { }
        public HiveDAOImpl(string name, string user, string pass, string host, string sid, string service, string port) : base(name, user, pass, host, sid, service, port) { }
        public override bool TestConn()
        {
            using (var conn = new Connection(this.Host, ConvertUtil.TryParseInt(this.Port),
                                                   this.User, this.Pass))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                    return false;
                }
            }
        }
        public override bool ExecuteSQL(string sqlText, string outPutPath, int maxReturnNum = -1, int pageSize = 100000)
        {
            throw new NotImplementedException(); // TODO
        }
        public override string Query(string sql, bool header=true)
        {
            StringBuilder sb = new StringBuilder(1024 * 16);
            try
            {
                using (Connection conn = new Connection(this.Host, ConvertUtil.TryParseInt(this.Port),
                                                   this.User, this.Pass))
                {

                    var cursor = conn.GetCursor();
                    foreach (var s in sql.Split(';'))
                    {
                        if (!String.IsNullOrEmpty(s))
                            cursor.Execute(s);
                    }
                    var list = cursor.FetchMany(int.MaxValue);
                    if (header)
                    {
                        string headers;
                        if (list.Count > 0)
                        {
                            // 添加表头
                            headers = string.Join(OpUtil.DefaultFieldSeparator.ToString(), (list[0] as IDictionary<string, object>).Keys);
                            sb.Append(headers).Append(OpUtil.DefaultLineSeparator);
                        }
                    }
                    foreach (var item in list)
                    {
                        var dict = item as IDictionary<string, object>;
                        string tmp = string.Empty;

                        foreach (var key in dict.Keys)
                        {
                            tmp += dict[key].ToString() + OpUtil.DefaultFieldSeparator;
                        }
                        sb.Append(tmp.TrimEnd(OpUtil.DefaultFieldSeparator)).Append(OpUtil.DefaultLineSeparator);
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());   // 辅助工具类，showmessage不能放在外面
            }
            return sb.ToString().Trim(OpUtil.DefaultLineSeparator);
        }
        public override string LimitSQL(string sql)
        {
            return String.Format("{0} limit {1}", sql, OpUtil.PreviewMaxNum);
        }
        public override string GetTablesSQL(string schema)
        {
            return String.Format(this.getTablesSQL, schema);
        }
        public override string GetColNameByTablesSQL(List<Table> tables)
        {
            return String.Empty;
        }
        public override string GetTableContentSQL(Table table, int maxNum)
        {
            return String.Format(getTableContentSQL, this.Schema, table.Name, maxNum);
        }
        public override string GetUserSQL()
        {
            return this.getUserSQL;
        }
        public override string DefaultSchema()
        {
            return String.IsNullOrEmpty(this.Schema) ? "default" : this.Schema;
        }
        public override string GetColNameByTableSQL(Table table)
        {
            return String.Format(this.getColNameByTableSQL, table.Name);
        }
    }
}
