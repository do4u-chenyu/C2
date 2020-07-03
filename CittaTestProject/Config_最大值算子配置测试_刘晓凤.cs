using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CittaTestProject
{
    [CodedUITest]
    public class Config_最大值算子配置测试_刘晓凤
    {
        [TestMethod, TestCategory("全量测试")]
        public void 算子配置整体测试()
        {
            算子准备();
            最大值算子输出字段为空();
        }
        [TestMethod, TestCategory("基本测试")]
        public void 配置字段为空判断()
        {
            最大值算子输出字段为空();
        }
        [TestMethod, TestCategory("基本测试")]
        public void 配置字段含非法字段判断()
        {
            最大值算子配置字段输入不合法字符();
        }
        [TestMethod, TestCategory("快速测试")]
        public void 算子准备()
        { 
            创建最大值算子();
            配置最大值算子();
            删除最大值算子();
        }
        private void 创建最大值算子()
        { }
        private void 配置最大值算子()
        { }
        private void 删除最大值算子()
        { }
        private void 最大值算子输出字段为空()
        { }
        private void 最大值算子配置字段输入不合法字符()
        { }

    }
   

}
