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
    public class BaseDAOImplTests
    {
        [TestMethod()]
        public void ExecuteSQLTest()
        {
            //DatabaseItem item = new DatabaseItem 
            //{
            //    User = "test",
            //    Password = "test",
            //    Server = "111.111.111.111",
            //    SID = "orcl",
            //    Service = "",
            //    Port = "1521",
            //    Schema = "test",
            //};

            //OracleDAOImpl dao = new OracleDAOImpl(item);
            //dao.ExecuteSQL("", "");
            //Assert.Fail();
        }
    }
}