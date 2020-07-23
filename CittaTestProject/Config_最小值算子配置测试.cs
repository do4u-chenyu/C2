using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CittaTestProject
{
    [CodedUITest]
    public class Config_最小值算子配置测试
    {
        [TestMethod, TestCategory("业务逻辑测试")]
        public void 算子生成结果检测()
        {
            模型文档构建();
            模型测试数据导入();
            创建最小值算子();
            配置最小值算子();
            最小值算子生成结果检测();
        }
        [TestMethod, TestCategory("快速测试")]
        public void 算子准备()
        {
            创建最小值算子();
            配置最小值算子();
            删除最小值算子();
        }
        [TestMethod, TestCategory("全量测试")]
        public void 算子配置整体测试()
        {
            算子准备();
            最小值算子输出字段为空();
            最小值算子配置字段输入不合法字符();
        }
        [TestMethod, TestCategory("基本测试")]
        public void 配置字段为空判断()
        {
            最小值算子输出字段为空();
        }
        [TestMethod, TestCategory("基本测试")]
        public void 配置字段含非法字段判断()
        {
            最小值算子配置字段输入不合法字符();
        }

        private void 模型文档构建()
        {

            this.UIMap.算子模型文档构建();
        }
        private void 模型测试数据导入()
        {

            this.UIMap.单目算子模型测试数据导入();

        }
        private void 创建最小值算子()
        {

        }
        private void 配置最小值算子()
        {

        }
        private void 删除最小值算子()
        { }
        private void 最小值算子输出字段为空()
        { }
        private void 最小值算子配置字段输入不合法字符()
        { }
        private void 最小值算子生成结果检测()
        {
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
