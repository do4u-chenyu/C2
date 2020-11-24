using System.Windows.Forms;
namespace C2.Utils
{
    class HelpUtil
    {
        public static string AvgOperatorHelpInfo = "取平均值:计算选择字段的平均值.";
        public static string MinOperatorHelpInfo = "取最小值:提取选择字段的最小值,查看相关数据信息.";
        public static string MaxOperatorHelpInfo = "取最大值:提取选择字段的最大值,查看相关数据信息.";
        public static string CollideOperatorHelpInfo = "碰撞算子:对两个数据表的选择字段进行取交集,输出两表都存在的数据.";
        public static string UnionOperatorHelpInfo  = "取并集:对两个数据表的选择字段进行合并.";
        public static string DifferOperatorHelpInfo = "取差集:比较查找左表存在而右表不存在的数据行.";
        public static string RandomOperatorHelpInfo = "随机采样:随机输出n条数据.";
        public static string FilterOperatorHelpInfo = "条件筛选:根据数值筛选条件设置查看符合条件的所在行数据.";
        public static string FreqOperatorHelpInfo = "频率算子:统计选择字段出现次数.";
        public static string SortOperatorHelpInfo = "排序算子:根据选择字段进行排序,支持数据去重.";
        public static string GroupOperatorHelpInfo  = "分组算子:根据选择字段对文本进行分组展示.";
        public static string RelateOperatorHelpInfo = "关联算子:根据选择的关联条件将两个数据表进行连接,默认左连接.";
        public static string CustomOperator1HelpInfo = "AI实践:灵活配置算子,用于各种模型探索和展示;一元算子,支持一个输入数据源.";
        public static string PythonOperatorHelpInfo  = "Py算子:调用自定义的第三方Python脚本完成运算.";
        public static string KeyWordOperatorHelpInfo = "关键词过滤:根据输入的关键词,对数据进行基础的关键词命中或去噪处理.";
        public static string DataFormatOperatorHelpInfo = "数据标准化:对数据进行输出列选择,顺序调整,列项重命名处理.";
        public static string CustomOperator2HelpInfo = "多源算子:灵活配置算子,用于各种模型探索和展示;二元算子,支持两个输入数据源.";
        public static string UndoButtonHelpInfo = "撤销按钮:撤销当前操作,目前支持添加,删除,重命名,移动,关系添加,关系删除6种操作的撤销";
        public static string RedoButtonHelpInfo = "恢复按钮:恢复上一步的撤销操作,目前支持添加,删除,重命名,移动,关系添加,关系删除6种操作的恢复";
        public static string FormatOperatorHelpInfo = "一键排版:智能调整元素版面位置";
        public static string SaveModelButtonHelpInfo = "保存模型";
        public static string SaveAllButtonHelpInfo = "保存所有模型";
        public static string ImportModelHelpInfo = "导入模型";
        public static string RemarkPictureBoxHelpInfo = "编写备注信息";
        public static string ZoomUpPictureBoxHelpInfo = "放大屏幕,支持三级缩放";
        public static string ZoomDownPictureBoxHelpInfo = "缩小屏幕,支持三级缩放";
        public static string MovePictureBoxHelpInfo = "拖动当前视野框";
        public static string FramePictureBoxHelpInfo = "框选屏幕中算子进行整体拖动";
        public static string MoreButtonHelpInfo = "首选项:配置程序的各项参数";

        public static string AttachmentWidgetHelpInfo = "附件:支持添加多个附件,支持多种类型文档";
        public static string ChartWidgetHelpInfo = "图表:柱状图,折线图,饼图,环形图,雷达图等";
        public static string OperatorWidgetHelpInfo = "算子:内置多种算子,支持Python脚本和自主建模";
        public static string ResultWidgetHelpInfo = "运算结果:算子和高级模型运算后生成的结果,支持预览";
        public static string DataSourceWidgetHelpInfo = "数据源:支持添加多个数据源,支持多种类型文档";
        public static string ExportImageHelpInfo = "导出图片:支持导出成多种格式的图片";

        public static string ReviewToolStripMenuItemInfo = "首页状态下无法预览数据，请点击左侧面板打开一个业务视图后再试";



        /// <summary>
        /// 默认为Information,除非有非常严重的错误,否则一般尽量用温和的提示信息.
        /// MessageBoxIcon.Hand 
        /// MessageBoxIcon.Asterisk      星号
        /// MessageBoxIcon.Exclamation
        /// MessageBoxIcon.Information   通知
        /// MessageBoxIcon.Warning       警告
        /// MessageBoxIcon.Error         错误
        /// MessageBoxIcon.Question      疑问
        /// MessageBoxIcon.Stop         
        /// MessageBoxIcon.None          无图标
        /// </summary>
        public static DialogResult ShowMessageBox(string message, string caption = "提示信息", MessageBoxIcon type = MessageBoxIcon.Information)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.OK, type);
        }
    }
}
