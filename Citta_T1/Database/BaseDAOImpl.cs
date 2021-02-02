using C2.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace C2.Database
{
    public class BaseDAOImpl : IDAO
    {
        protected const int maxPreviewNum = 1000;
        #region 构造函数

        protected string Name, User, Pass, Host, Sid, Service, Port, Schema;
        public delegate void UpdateLog(string log);
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
        public BaseDAOImpl(DataItem item) : this(item.DBItem) { }
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

        ///<summary>
        ///异常:
        ///<para>QueryFailureException</para>
        ///</summary>
        public virtual string Query(string sql, bool header = true, int returnNum = OpUtil.PreviewMaxNum)
        {
           return String.Empty;
        }
        public virtual bool TestConn()
        {
            return false;
        }
        #endregion
        #region 业务逻辑
        public List<string> GetUsers()
        {
            string result = this.TryQuery(this.GetUserSQL(), false);
            return String.IsNullOrEmpty(result) ? new List<String>() : new List<string>(result.Split(OpUtil.DefaultLineSeparator));
        }

        public List<Table> GetTables(string schema)
        {
            List<Table> tables = new List<Table>();
            string result = this.TryQuery(this.GetTablesSQL(schema), false);
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
            return this.TryQuery(this.GetTableContentSQL(table), true, maxNum);
        }

        private string TryQuery(string sql,bool header=true, int maxReturnNum=OpUtil.PreviewMaxNum)
        {
            string result = string.Empty;
            try
            {
                result = this.Query(sql, header, maxReturnNum);
            }
            catch { }
            return result;
        }
        public List<List<string>> GetTableContent(Table table, int maxNum)
        {
            string result = this.GetTableContentString(table, maxNum);
            return String.IsNullOrEmpty(result) ? new List<List<string>>() : DbUtil.StringTo2DString(result);
        }
        public Dictionary<string, List<string>> GetColNameBySchema(string schema)
        {
            string result = this.TryQuery(this.GetColNameBySchemaSQL(schema), false);
            return String.IsNullOrEmpty(result) ? new Dictionary<string, List<string>>() : DbUtil.StringToDict(result);
        }
 
        public Dictionary<string, List<string>> GetColNameByTables(List<Table> tables)
        {
            string result = this.TryQuery(this.GetColNameByTablesSQL(tables), false);
            return String.IsNullOrEmpty(result) ? new Dictionary<string, List<string>>() : DbUtil.StringToDict(result);
        }
        public string GetTableColumnNames(Table table)
        {
            return this.TryQuery(this.GetColNameByTableSQL(table));
        }
        public void FillDGVWithTbSchema(DataGridView dataGridView, Table table)
        {
            string schemaString = this.Query(this.GetColNameByTableSQL(table));
            List<List<string>> schema = DbUtil.StringTo2DString(schemaString);
            FileUtil.FillTable(dataGridView, schema);
        }
        public void FillDGVWithTbContent(DataGridView dataGridView, Table table, int maxNum)
        {
            string contentString = this.Query(this.GetTableContentSQL(table), true, maxNum);
            List<List<string>> tableCols = DbUtil.StringTo2DString(contentString);
            FileUtil.FillTable(dataGridView, tableCols);
        }

        public void FillDGVWithSQL(DataGridView dataGridView, string sql)
        {
            string contentString = this.Query(sql);
            List<List<string>> tableCols = DbUtil.StringTo2DString(contentString);
            FileUtil.FillTable(dataGridView, tableCols);
        }


        public virtual bool ExecuteSQL(string sqlText, string outPutPath, int maxReturnNum = -1, int pageSize = 100000)
        {
            int pageIndex = 0;
            bool returnHeader = true;
            int totalReturnNum = 0, subMaxNum;
            using (StreamWriter sw = new StreamWriter(outPutPath, false))
            {
                while (maxReturnNum == -1 ? true : totalReturnNum < maxReturnNum)
                {
                    if (pageSize * pageIndex < maxReturnNum && pageSize * (pageIndex + 1) > maxReturnNum)
                        subMaxNum = maxReturnNum - pageIndex * pageSize;
                    else
                        subMaxNum = pageSize;
                    QueryResult contentAndNum = ExecuteSQL_Page(sqlText, pageSize, pageIndex, subMaxNum, returnHeader);

                    string result = contentAndNum.content;
                    totalReturnNum += contentAndNum.returnNum;

                    if (returnHeader)
                    {
                        if (String.IsNullOrEmpty(result))
                            return false;
                        returnHeader = false;
                    }
                    if (String.IsNullOrEmpty(result))
                        break;
                    sw.Write(result);
                    sw.Flush();
                    pageIndex += 1;
                }
            }
            return true;
        }
        protected virtual QueryResult ExecuteSQL_Page(string sqlText, int pageSize, int pageIndex, int maxNum, bool returnHeader)
        {
            return new QueryResult();
        }
        public virtual string DefaultSchema()
        {
           return String.Empty;
        }

        #endregion
        #region SQL

        public virtual string GetUserSQL()
        {
           return String.Empty;
        }
        public virtual string GetTableContentSQL(Table table)
        {
           return String.Empty;
        }
        public virtual string GetTablesSQL(string schema)
        {
           return String.Empty;
        }
        public virtual string GetColNameBySchemaSQL(string schema)
        {
           return String.Empty;
        }
        public virtual string GetColNameByTablesSQL(List<Table> tables)
        {
           return String.Empty;
        }
        public virtual string GetColNameByTableSQL(Table table)
        {
           return String.Empty;
        }
        #endregion
        #region 自定义异常


        protected void QueryFailureException(string message)
        { 
            throw new Exception(message);
        }

        #endregion
    }

    class EmptyDAOImpl : BaseDAOImpl
    {
        public override bool TestConn()
        {
            return false;
        }
    }
}
