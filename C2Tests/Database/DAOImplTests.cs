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
    public class DAOImplTests
    {
        OraConnection conn = new OraConnection("test@aliyun", "phx2", "123456", "114.55.248.85", "orcl", String.Empty, "1521");
        private DatabaseItem oralcDBI = new DatabaseItem(DatabaseType.Oracle, "114.55.248.85", "orcl", String.Empty, "1521", "test", "test");
        private DatabaseItem hiveDBI = new DatabaseItem(DatabaseType.Hive, "10.1.126.4", String.Empty, String.Empty, "10000", "root", "123456");
        DAOImpl oracleDAO;
        DAOImpl hiveDAO;

        public void InitDao()
        {
            oracleDAO = new DAOImpl(oralcDBI);
            hiveDAO = new DAOImpl(hiveDBI);
        }
        public void CreateTest()
        {
            InitDao();
            Assert.IsNotNull(oracleDAO);
            Assert.IsNotNull(hiveDAO);
        }

        [TestMethod()]
        public void TestConnTest()
        {
            InitDao();
            Assert.IsTrue(oracleDAO.TestConn() == true);
            Assert.IsTrue(hiveDAO.TestConn() == true);

        }

        [TestMethod()]
        public void GetUsersTest()
        {
            InitDao();
            List<string> oracleUsers = oracleDAO.GetUsers();
            List<string> hiveUsers = hiveDAO.GetUsers();
            Assert.IsTrue(oracleUsers.Count > 0);
            Assert.IsTrue(hiveUsers.Count > 0);
            Console.WriteLine(oracleUsers[0]);
            Console.WriteLine(hiveUsers[0]);
        }

        [TestMethod()]
        public void GetTablesByUserOrDbTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTableContentStringTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTableContentTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetSchemaByTablesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FillDGVWithTbSchemaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FillDGVWithTbContentTest()
        {
            Assert.Fail();
        }
    }
}