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
using System.IO;


namespace CittaTestProject
{
    /// <summary>
    /// CodedUITest1 的摘要说明
    /// </summary>
    [CodedUITest]
    public class 测试样例
    {
        public 测试样例()
        {
        }

        [TestMethod, TestCategory("CommonDocumentModels")]

        public void 登陆拖拽算子测试样例()

        {
            this.UIMap.ExampleTest();
        }

        #region 附加测试特性

        //运行每项测试之前使用 TestInitialize 运行代码 ,为测试准备一下初始环境
        [TestInitialize()]
        public void MyTestInitialize()
        {
            // 删除本地90模型，测试样例会新建90
            string path = @"D:\FiberHomeIAOModelDocument\90";
            if (Directory.Exists(path))
                Directory.Delete(path, true);


        }


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
