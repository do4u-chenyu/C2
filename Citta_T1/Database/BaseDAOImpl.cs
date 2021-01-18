using C2.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Database
{
    public class BaseDAOImpl : IDAO
    {
        #region 构造函数
        protected string Name, User, Pass, Host, Sid, Service, Port, Schema;
        protected struct QueryResult
        {
            public string content;
            public int returnNum;
        }
        protected BaseDAOImpl()
        {

        }
        public BaseDAOImpl(DatabaseItem dbi)
        {
            this.Name = dbi.Server;
            this.User = dbi.User;
            this.Pass = dbi.Password;
            this.Host = dbi.Server;
            this.Sid = dbi.SID;
            this.Service = dbi.Service;
            this.Port = dbi.Port;
            this.Schema = dbi.Schema;
        }
        public BaseDAOImpl(DataItem item): this(item.DBItem) { }
        public BaseDAOImpl(string name, string user, string pass, string host, string sid, string service, string port)
        {
            this.Name = name;
            this.User = user;
            this.Pass = pass;
            this.Host = host;
            this.Sid = sid;
            this.Service = service;
            this.Port = port;
        }
        public BaseDAOImpl Clone()
        {
            return new BaseDAOImpl
            {
                Name = this.Name,
                User = this.User,
                Pass = this.Pass,
                Host = this.Host,
                Sid = this.Sid,
                Service = this.Service,
                Port = this.Port
            };
        }
        #endregion
        #region 接口实现
        public virtual string Query(string sql, bool header=true)
        {
            throw new NotImplementedException();
        }
        public virtual bool TestConn()
        {
            return false;
        }
        #endregion
        #region 业务逻辑
        public List<string> GetUsers()
        {
            string result = this.Query(this.GetUserSQL(), false);
            return String.IsNullOrEmpty(result) ? new List<String>() : new List<string>(result.Split(OpUtil.DefaultLineSeparator));
        }
        public List<Table> GetTables(string schema)
        {
            List<Table> tables = new List<Table>();
            string result = this.Query(this.GetTablesSQL(), false);
            if (!String.IsNullOrEmpty(result))
                foreach (var line in result.Split(OpUtil.DefaultLineSeparator))
                {
                    if (!String.IsNullOrEmpty(line))
                        tables.Add(new Table(schema, line));
                }
            return tables;
        }
        public string GetTableContentString(Table table, int maxNum)
        {
            return this.Query(this.GetTableContentSQL(table, maxNum));
        }
        public List<List<string>> GetTableContent(Table table, int maxNum)
        {
            string result = this.GetTableContentString(table, maxNum);
            return String.IsNullOrEmpty(result) ? new List<List<string>>() : DbUtil.StringTo2DString(result);
        }
        public Dictionary<string, List<string>> GetColNameByTables(List<Table> tables)
        {
            string result = this.Query(this.GetColNameByTablesSQL(tables));
            return String.IsNullOrEmpty(result) ? new Dictionary<string, List<string>>() : DbUtil.StringToDict(result);
        }
        public string GetTableColumnNames(Table table)
        {
            return this.Query(this.GetColNameByTableSQL(table));
        }
        public bool FillDGVWithTbSchema(DataGridView dataGridView, Table table)
        {
            string schemaString = this.GetTableColumnNames(table);
            if (String.IsNullOrEmpty(schemaString))
                return false;
            List<List<string>> schema = DbUtil.StringTo2DString(schemaString);
            FileUtil.FillTable(dataGridView, schema);
            return true;
        } 
        public bool FillDGVWithTbContent(DataGridView dataGridView, Table table, int maxNum)
        {
            string contentString = this.GetTableContentString(table, maxNum);
            if (String.IsNullOrEmpty(contentString))
                return false;
            List<List<string>> tableCols = DbUtil.StringTo2DString(contentString);
            FileUtil.FillTable(dataGridView, tableCols);
            return true;
        }
        public bool FillDGVWithSQL(DataGridView dataGridView, string sql)
        {
            string contentString = this.Query(String.Format(this.LimitSQL(sql)));
            if (String.IsNullOrEmpty(contentString))
                return false;
            List<List<string>> tableCols = DbUtil.StringTo2DString(contentString);
            FileUtil.FillTable(dataGridView, tableCols);
            return true;
        }
        public virtual bool ExecuteSQL(string sqlText, string outPutPath, int maxReturnNum = -1, int pageSize = 100000)
        {
            return false;
        }
        public virtual string DefaultSchema()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region SQL
        public virtual string LimitSQL(string sql)
        {
            throw new NotImplementedException();
        }
        public virtual string GetUserSQL()
        {
            throw new NotImplementedException();
        }
        public virtual string GetTableContentSQL(Table table, int maxNum)
        {
            throw new NotImplementedException();
        }
        public virtual string GetTablesSQL()
        {
            throw new NotImplementedException();
        }
        public virtual string GetColNameByTablesSQL(List<Table> tables)
        {
            throw new NotImplementedException();
        }
        public virtual string GetColNameByTableSQL(Table table)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
