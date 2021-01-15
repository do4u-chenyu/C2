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
    public class HiveDAOImpl: BaseDAO
    {
        private static readonly LogUtil log = LogUtil.GetInstance("HiveDAOImpl");
        public string Server, User, Pass, Host, Port;
        public new string getUserSQL = @"show databases";
        public new string getTablesByUserSQL = @"use {};show tables;";
        public new string getTableContentSQL = @"use {};select * from {} limit {}";
        public new string getSchemaByTablesSQL;

        public HiveDAOImpl(DatabaseItem dbi)
        {
            this.Server = dbi.Server;
            this.User = dbi.User;
            this.Pass = dbi.Password;
            this.Host = dbi.Server;
            this.Port = dbi.Port;
        }
        public HiveDAOImpl(DataItem item) : this(item.DBItem)
        {
        }
        public override bool TestConn()
        {
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                using (var conn = new Connection(this.Server, ConvertUtil.TryParseInt(this.Port),
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
        }
        public override string Query(string sql)
        {
            StringBuilder sb = new StringBuilder(1024 * 16);
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                try
                {
                    using (Connection conn = new Connection(this.Server, ConvertUtil.TryParseInt(this.Port),
                                                       this.User, this.Pass))
                    {

                        var cursor = conn.GetCursor();
                        foreach(var s in sql.Split(';'))
                            cursor.Execute(sql);
                        var list = cursor.FetchMany(int.MaxValue);
                        string headers;
                        if (list.Count > 0)
                        {
                            // 添加表头
                            headers = string.Join(OpUtil.DefaultFieldSeparator.ToString(), (list[0] as IDictionary<string, object>).Keys);
                            sb.Append(headers).Append(OpUtil.DefaultLineSeparator);
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
                return sb.ToString();
            }
        }

        public override string GenGetSchemaByTablesSQL(string getSchemaByTablesSQL, List<Table> tables)
        {
            throw new NotImplementedException();
        }

        public override string GenGetTableContentSQL(Table table, int maxNum)
        {
            return String.Format(this.getTableContentSQL, this.User, table, maxNum);
        }
    }
}
