using Microsoft.VisualStudio.TestTools.UnitTesting;
using C2.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C2.Model;

namespace C2.Database.Tests
{
    [TestClass()]
    public class DAOFactoryTests
    {
        private DatabaseItem oralcDBI = new DatabaseItem(DatabaseType.Oracle, "114.55.248.85", "orcl", String.Empty, "1521", "test", "test", table: new Table("test", "TEST_100W"));
        private DatabaseItem hiveDBI = new DatabaseItem(DatabaseType.Hive, "10.1.126.4", String.Empty, String.Empty, "10000", "root", "123456");
        BaseDAOImpl oracleDAO;
        BaseDAOImpl hiveDAO;
        public void InitDao()
        {
            oracleDAO = DAOFactory.CreatDAO(oralcDBI);
            //hiveDAO = DAOFactory.CreatDAO(hiveDBI);
        }
        [TestMethod()]
        public void CreatDAOTest()
        {
            BaseDAOImpl oracleDAO = DAOFactory.CreatDAO(oralcDBI);
            BaseDAOImpl hiveDAO = DAOFactory.CreatDAO(hiveDBI);
            Assert.IsNotNull(oracleDAO);
            Assert.IsNotNull(hiveDAO);
        }
        [TestMethod()]
        public void TestConnTest()
        {
            InitDao();
            Assert.IsTrue(oracleDAO.TestConn() == true);
            //Assert.IsTrue(hiveDAO.TestConn() == true);

        }

        [TestMethod()]
        public void GetUsersTest()
        {
            InitDao();
            List<string> oracleUsers = oracleDAO.GetUsers();
            Assert.IsTrue(oracleUsers.Count > 0);
            Console.WriteLine(oracleUsers[0]);
            //List<string> hiveUsers = hiveDAO.GetUsers();
            //Assert.IsTrue(hiveUsers.Count > 0);
            //Console.WriteLine(hiveUsers[0]);

        }

        [TestMethod()]
        public void GetTablesByUserOrDbTest()
        {
            InitDao();
            List<Table> tables = oracleDAO.GetTablesByUserOrDb(oralcDBI.User);
            Assert.IsTrue(tables.Count > 0);
            Console.WriteLine(tables[0].Name);
        }

        [TestMethod()]
        public void GetTableContentStringTest()
        {
            InitDao();
            string result = oracleDAO.GetTableContentString(oralcDBI.DataTable, 1000);
            Assert.IsTrue(!String.IsNullOrEmpty(result));
            Console.WriteLine(result.Substring(0, 10));
        }

        [TestMethod()]
        public void GetTableContentTest()
        {
            InitDao();
            List<List<string>> result = oracleDAO.GetTableContent(oralcDBI.DataTable, 1000);
            Assert.IsTrue(result.Count > 0 && result[0].Count > 0);
            Console.WriteLine(result[0][0]);
        }

        [TestMethod()]
        public void GetSchemaByTablesTest()
        {
            InitDao();
            Dictionary<string, List<string>> result = oracleDAO.GetSchemaByTables(new List<Table>() { oralcDBI.DataTable });
            Assert.IsTrue(result.Keys.Count > 0);
        }
    }
}