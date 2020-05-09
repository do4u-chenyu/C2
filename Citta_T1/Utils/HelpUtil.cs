using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Utils
{
    class HelpUtil
    {
        public static string AvgOperatorHelpInfo = "取平均值:计算选择字段的平均值.";
        public static string MinOperatorHelpInfo = "取最小值算子:提取选择字段的最小值，查看相关数据信息.";
        public static string MaxOperatorHelpInfo = "取最大值算子:提取选择字段的最大值，查看相关数据信息.";
        public static string CollideOperatorHelpInfo = "碰撞算子:对两个数据表的选择字段进行取交集，输出两表都存在的数据.";
        public static string UnionOperatorHelpInfo = "取并集:对两个数据表的选择字段进行合并.";
        public static string DifferOperatorHelpInfo = "取差集:比较查找左表存在而右表不存在的数据行.";
        public static string RandomOperatorHelpInfo = "随机采样:随机输出n条数据.";
        public static string FilterOperatorHelpInfo = "过滤算子:根据过滤条件设置查看符合条件的所在行数据.";
        public static string FreqOperatorHelpInfo = "频率统计:统计选择字段出现次数.";
        public static string SortOperatorHelpInfo = "排序算子:根据选择字段进行排序,支持数据去重"; 
        public static string GroupOperatorHelpInfo = "分组算子:根据选择字段对文本进行分组展示";
        public static string RelateOperatorHelpInfo = "关联算子:根据选择的关联条件将两个数据表进行连接，默认左连接.";
        public static string CustomOperator1HelpInfo = "自定义算子:灵活配置算子,用于各种模型探索和展示.一元算子,支持一个输入数据源.";
        public static string PythonOperatorHelpInfo = "Python算子:调用自定义的第三方Python脚本完成运算.";

        public static string CustomOperator2HelpInfo = "自定义算子:灵活配置算子,用于各种模型探索和展示.二元算子,支持二个输入数据源.";
    


        public static string FormatOperatorHelpInfo = "一键排版,智能调整元素版面位置.";
    }
}
