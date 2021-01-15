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
    public class DAOImpl
    {
        protected readonly int maxPreviewNum = 1000;
        public BaseDAO dao;
        public DAOImpl(DatabaseItem dbi)
        {
            switch (dbi.Type)
            {
                case DatabaseType.Hive:
                    dao = new HiveDAOImpl(dbi);
                    break;
                case DatabaseType.Oracle:
                    dao = new OracleDAOImpl(dbi);
                    break;
                default:
                    break;
            }
        }
        public bool TestConn()
        {
            return dao.TestConn();
        }
        public List<string> GetUsers()
        {
            return new List<string>(dao.Query(dao.getUserSQL).Split(OpUtil.DefaultLineSeparator));
        }
        public List<Table> GetTablesByUserOrDb(string usersOrDbs)
        {
            List<Table> tables = new List<Table>();
            foreach (var line in dao.Query(String.Format(dao.getTablesByUserSQL, usersOrDbs)).Split(OpUtil.DefaultLineSeparator))
                tables.Add(new Table(usersOrDbs, line));
            return tables;
        }
        public string GetTableContentString(Table table, int maxNum)
        {
            return dao.Query(dao.GenGetTableContentSQL(table, maxNum));
        }
        public List<List<string>> GetTableContent(Table table, int maxNum)
        {
            return DbUtil.StringTo2DString(this.GetTableContentString(table, maxNum));
        }
        public Dictionary<string, List<string>> GetSchemaByTables(List<Table> tables)
        {
            string sql = dao.GenGetSchemaByTablesSQL(dao.getSchemaByTablesSQL, tables);
            return DbUtil.StringToDict(dao.Query(sql));
        }
        public bool FillDGVWithTbSchema(DataGridView dataGridView, Table table)
        {
            string schemaString = dao.Query(dao.getSchemaByTablesSQL);
            if (String.IsNullOrEmpty(schemaString))
                return false;
            List<List<string>> schema = DbUtil.StringTo2DString(schemaString);
            FileUtil.FillTable(dataGridView, schema);
            return true;
        }
        public bool FillDGVWithTbContent(DataGridView dataGridView, Table table)
        {
            string schemaString = dao.Query(dao.getTableContentSQL);
            if (String.IsNullOrEmpty(schemaString))
                return false;
            List<List<string>> schema = DbUtil.StringTo2DString(schemaString);
            FileUtil.FillTable(dataGridView, schema);
            return true;
        }
    }
}
