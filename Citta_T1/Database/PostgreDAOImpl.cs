using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2.Utils;
using Npgsql;
namespace C2.Database
{
    class PostgreDAOImpl : BaseDAOImpl
    {
        private static readonly LogUtil log = LogUtil.GetInstance("PostgreDAOImpl");
        //private readonly string getUserSQL = @"";
        //private readonly string getTablesSQL = @"";
        //private readonly string getTableContentSQL = @"";
        //private readonly string getColNameByTableSQL = "";
        //private readonly string dataBaseName;

        public override bool TestConn()
        {
            return true;
        }

        public override string Query(string sql, bool header = true, int returnNum = OpUtil.PreviewMaxNum)
        {
            return null;
        }

        private string ExecuteQuery(string sql, NpgsqlConnection conn, bool header, int returnNum)
        {
            return null;
        }

        public override bool ExecuteSQL(string sqlText, string outPutPath, int maxReturnNum = -1)
        {
            return true;
        }
        private string GetColumnName(string name)
        {
            return null;
        }

        public override string GetTablesSQL(string schema)
        {
            return null;
        }
        public override string GetColNameBySchemaSQL(string schema)
        {
            return null;
        }
        public override string GetColNameByTablesSQL(List<Table> tables)
        {
            return null;
        }
        public override string GetTableContentSQL(Table table)
        {
            return null;
        }
        public override string GetUserSQL()
        {
            return null;
        }

        public override string GetColNameByTableSQL(Table table)
        {
            return null;
        }
    }
}
