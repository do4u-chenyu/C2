using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Database
{
    public abstract class BaseDAO
    {
        public string getUserSQL;
        public string getTablesByUserSQL;
        public string getTableContentSQL;
        public string getSchemaByTablesSQL;
        public abstract bool TestConn();
        public abstract string Query(string sql);
        public abstract string GenGetSchemaByTablesSQL(string getSchemaByTablesSQL, List<Table> tables);

        public abstract string GenGetTableContentSQL(Table table, int maxNum);
    }
}
