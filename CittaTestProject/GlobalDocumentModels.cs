using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace CittaTestProject
{
    /// <summary>
    /// CodedUITest1 的摘要说明
    /// </summary>
    [CodedUITest]
    public class GlobalDocumentModels
    {
        public GlobalDocumentModels()
        {
        }

        [TestMethod, TestCategory("CommonDocumentModels")]

        public void CreateDocumentModels()

        {

            /*
             *  在此可创建一些模型，供全局调用
             */


        }

        #region 附加测试特性

        // 编写测试时，可以使用以下附加特性: 

        ////运行每项测试之前使用 TestInitialize 运行代码 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // 若要为此测试生成代码，请从快捷菜单中选择“为编码的 UI 测试生成代码”，然后选择菜单项之一。
        //}

        ////运行每项测试之后使用 TestCleanup 运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // 若要为此测试生成代码，请从快捷菜单中选择“为编码的 UI 测试生成代码”，然后选择菜单项之一。
        //}

        #endregion

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
