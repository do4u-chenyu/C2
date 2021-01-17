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
        protected string Name, User, Pass, Host, Sid, Service, Port;
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
        public virtual string Query(string sql)
        {
            throw new NotImplementedException();
        }
        public virtual bool TestConn()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 业务逻辑
        public virtual List<string> GetUsers()
        {
            return new List<string>(this.Query(this.GetUserSQL()).Split(OpUtil.DefaultLineSeparator));
        }
        public virtual List<Table> GetTablesByUserOrDb(string usersOrDbs)
        {
            List<Table> tables = new List<Table>();
            foreach (var line in this.Query(String.Format(this.GetTablesByUserSQL(), usersOrDbs)).Split(OpUtil.DefaultLineSeparator))
                tables.Add(new Table(usersOrDbs, line));
            return tables;
        }
        public virtual string GetTableContentString(Table table, int maxNum)
        {
            return this.Query(this.GetTableContentSQL(table, maxNum));
        }
        public virtual List<List<string>> GetTableContent(Table table, int maxNum)
        {
            return DbUtil.StringTo2DString(this.GetTableContentString(table, maxNum));
        }
        public virtual Dictionary<string, List<string>> GetSchemaByTables(List<Table> tables)
        {
            string sql = this.GetSchemaByTablesSQL(tables);
            return DbUtil.StringToDict(this.Query(sql));
        }
        public virtual bool FillDGVWithTbSchema(DataGridView dataGridView, Table table)
        {
            string schemaString = this.Query(this.GetSchemaByTablesSQL(new List<Table>() { table }));
            if (String.IsNullOrEmpty(schemaString))
                return false;
            List<List<string>> schema = DbUtil.StringTo2DString(schemaString);
            FileUtil.FillTable(dataGridView, schema);
            return true;
        }
        public virtual bool FillDGVWithTbContent(DataGridView dataGridView, Table table, int maxNum)
        {
            string schemaString = this.Query(this.GetTableContentSQL(table, maxNum));
            if (String.IsNullOrEmpty(schemaString))
                return false;
            List<List<string>> schema = DbUtil.StringTo2DString(schemaString);
            FileUtil.FillTable(dataGridView, schema);
            return true;
        }
        #endregion
        #region SQL
        public virtual string GetUserSQL()
        {
            throw new NotImplementedException();
        }
        public virtual string GetTableContentSQL(Table table, int maxNum)
        {
            throw new NotImplementedException();
        }
        public virtual string GetTablesByUserSQL()
        {
            throw new NotImplementedException();
        }
        public virtual string GetSchemaByTablesSQL(List<Table> tables)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
