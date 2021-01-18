using System.Collections.Generic;
using System.Windows.Forms;

namespace C2.Database
{
    public interface IDAO
    {
        bool TestConn();
        string Query(string sql);
        List<string> GetUsers();
        List<Table> GetTables(string schema);
        string GetTableContentString(string UserOrDb, Table table, int maxNum);
        List<List<string>> GetTableContent(string UserOrDb, Table table, int maxNum);
        Dictionary<string, List<string>> GetColNameByTables(List<Table> tables);
        string GetTableColumnNames(Table table);
        bool FillDGVWithTbSchema(DataGridView dataGridView, Table table);
        bool FillDGVWithTbContent(DataGridView dataGridView, string UserOrDb, Table table, int maxNum);
        bool ExecuteOracleSQL(string sqlText, string outPutPath, int maxReturnNum = -1, int pageSize = 100000);
    }
}
