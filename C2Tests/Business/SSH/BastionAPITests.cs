using Microsoft.VisualStudio.TestTools.UnitTesting;
using C2.Business.SSH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.SSH.Tests
{
    [TestClass()]
    public class BastionAPITests
    {
        [TestMethod()]
        public void DownloadGambleTaskResultTest()
        {
            BastionAPI api = new BastionAPI(SearchToolkit.TaskInfo.EmptyTaskInfo);
            api.Login().DownloadGambleTaskResult(String.Empty);
            Assert.Fail();
        }
    }
}