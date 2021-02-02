using C2.Model;
using C2.Utils;
using Hive2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace C2.Database
{
    public class HiveDAOImpl : BaseDAOImpl
    {
        private static readonly LogUtil log = LogUtil.GetInstance("HiveDAOImpl");
        private readonly string getUserSQL = @"show databases";
        private readonly string getTablesSQL = @"use {0};show tables;";
        private readonly string getTableContentSQL = @"use {0};select * from {1}";
        //private string getColNameByTablesSQL;
        private readonly string getColNameByTableSQL = "desc {0}";
        private readonly string dataBaseName;
        public HiveDAOImpl(DatabaseItem dbi) : base(dbi)
        {
            this.dataBaseName = dbi.Schema;
        }
        public override bool TestConn()
        {
            using (var conn = new Connection(this.Host, ConvertUtil.TryParseInt(this.Port),
                                                   this.User, this.Pass))
            {
                try
                {

                    LimitTimeout(conn);
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
        private void LimitTimeout(Connection conn)
        {
            conn.SetSocketTimeout = 8000;
            conn.SetTcpReceiveTimeout = 8000;
            conn.SetTcpSendTimeout = 8000;
        }
        public override bool ExecuteSQL(string sqlText, string outPutPath, int maxReturnNum = -1)
        {
            int totalReturnNum = 0;
            StreamWriter sw = new StreamWriter(outPutPath, false);
            StringBuilder sb = new StringBuilder(1024); 
            try
            {
                using (Connection con = new Connection(this.Host, ConvertUtil.TryParseInt(this.Port),
                                                   this.User, this.Pass))
                {
                    LimitTimeout(con);
                    var cursor = con.GetCursor();
                    cursor.Execute("use " + dataBaseName);          
                    cursor.Execute(sqlText);
                    var oneRow = cursor.FetchOne();
                    if (!oneRow.IsEmpty())
                    {
                        // 添加表头
                        IDictionary<string, object> iDict = oneRow;
                        for (int i = 0; i < iDict.Count; i++)
                        {
                            string key = GetColumnName(iDict.Keys.ElementAt(i));
                            sb.Append(key).Append(OpUtil.DefaultFieldSeparator);
                        }
                        if (iDict.Count > 0)
                        {
                            sw.WriteLine(sb.ToString().TrimEnd(OpUtil.DefaultFieldSeparator));
                        }
                        
                    }
   
                    while (oneRow != null && (maxReturnNum == -1 ? true : totalReturnNum < maxReturnNum))
                    {
                        sb = new StringBuilder(1024);
                        IDictionary<string, object> dict = oneRow;
                        foreach (var key in dict.Keys)
                        {
                            sb.Append(dict[key].ToString()).Append(OpUtil.DefaultFieldSeparator);
                        }
                        if (!dict.Keys.IsEmpty())
                        {
                            sw.WriteLine(sb.ToString().TrimEnd(OpUtil.DefaultFieldSeparator));
                        }
                        totalReturnNum += 1;
                        oneRow = cursor.FetchOne();
                    }
                    sw.Flush();
                }
            }
            catch (Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());
                return false;
            }
            finally
            {
                sw.Close();
            }
            return true;
        }
      
       
        private string GetColumnName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            string[] names = name.Split('.');
            return names.Length == 2 ? names[1] : name;
        }

        public override string Query(string sql, bool header = true, int returnNum = OpUtil.PreviewMaxNum)
        {
            StringBuilder sb = new StringBuilder(1024 * 16);
            try
            {
                using (Connection conn = new Connection(this.Host, ConvertUtil.TryParseInt(this.Port),
                                                   this.User, this.Pass))
                {
                    LimitTimeout(conn);
                    var cursor = conn.GetCursor();
                    cursor.Execute("use " + dataBaseName);
                    foreach (var s in sql.Split(';'))
                    {
                        if (!String.IsNullOrEmpty(s))
                            cursor.Execute(s);
                    }
                    var list = cursor.FetchMany(returnNum);
                    if (header && !list.IsEmpty())
                    {
                        // 添加表头
                        IDictionary<string, object> iDict = list[0];
                        for (int i = 0; i < iDict.Count; i++)
                        {
                            string key = GetColumnName(iDict.Keys.ElementAt(i));
                            sb.Append(key).Append(OpUtil.DefaultFieldSeparator);
                        }
                        if (iDict.Count > 0)
                            sb.Remove(sb.Length - 1, 1).Append(OpUtil.DefaultLineSeparator); // 最后一列多加了个\t，去掉       

                    }
                    foreach (IDictionary<string, object> dict in list)
                    {
                        foreach (var key in dict.Keys)
                        {
                            sb.Append(dict[key].ToString()).Append(OpUtil.DefaultFieldSeparator);
                        }
                        if (!dict.Keys.IsEmpty())
                            sb.Remove(sb.Length - 1, 1).Append(OpUtil.DefaultLineSeparator);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(HelpUtil.DbCannotBeConnectedInfo + ", 详情：" + ex.ToString());   // 辅助工具类，showmessage不能放在外面
                throw new DAOException(ex.Message);

            }
            return sb.ToString().Trim(OpUtil.DefaultLineSeparator);
        }

        public override string GetTablesSQL(string schema)
        {
            return String.Format(this.getTablesSQL, schema);
        }
        public override string GetColNameByTablesSQL(List<Table> tables)
        {
            return String.Empty;
        }
        public override string GetTableContentSQL(Table table)
        {
            return String.Format(getTableContentSQL, this.Schema, table.Name);
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
