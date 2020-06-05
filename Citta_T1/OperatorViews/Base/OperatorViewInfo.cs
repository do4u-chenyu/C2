using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using System;
using System.Collections.Generic;


namespace Citta_T1.OperatorViews.Base
{
    public class OperatorViewInfo
    {
        private MoveOpControl opControl;          // 对应的OP算子 
        private string dataSourceFFP0;            // 左表数据源路径
        private string dataSourceFFP1;            // 右表数据源路径
        private string[] nowColumnsName0;          // 当前左表(pin0)数据源表头字段(columnName)
        private string[] nowColumnsName1;          // 当前右表(pin1)数据源表头字段
        private List<string> oldColumnsName0;      // 上一次左表(pin0)数据源表头字段
        private List<string> oldColumnsName1;      // 上一次右表(pin1数据源表头字段
        private List<int> oldOutList0;            // 上一次用户选择的左表输出字段的索引
        private List<int> oldOutList1;            // 上一次用户选择的右表输出字段的索引
        private List<string> selectedColumns;     // 本次配置用户选择的输出字段名称
        private string oldOptionDictStr;          // 旧配置字典的字符串表述
        
        private OptionInfoCheck optionInfoCheck;  // 用户配置信息通用检查

        public MoveOpControl OpControl { get => opControl; set => opControl = value; }
        public string DataSourceFFP0 { get => dataSourceFFP0; set => dataSourceFFP0 = value; }
        public string DataSourceFFP1 { get => dataSourceFFP1; set => dataSourceFFP1 = value; }
        public string[] NowColumnsName0 { get => nowColumnsName0; set => nowColumnsName0 = value; }
        public string[] NowColumnsName1 { get => nowColumnsName1; set => nowColumnsName1 = value; }
        public List<string> OldColumnsName0 { get => oldColumnsName0; set => oldColumnsName0 = value; }
        public List<string> OldColumnsName1 { get => oldColumnsName1; set => oldColumnsName1 = value; }
        public List<int> OldOutList0 { get => oldOutList0; set => oldOutList0 = value; }
        public List<int> OldOutList1 { get => oldOutList1; set => oldOutList1 = value; }
        public List<string> SelectedColumns { get => selectedColumns; set => selectedColumns = value; }
        public string OldOptionDictStr { get => oldOptionDictStr; set => oldOptionDictStr = value; }
        public OptionInfoCheck OptionInfoCheck { get => optionInfoCheck; set => optionInfoCheck = value; }

        public OperatorViewInfo()
        {
            OpControl = null;
            DataSourceFFP0 = String.Empty;
            DataSourceFFP1 = String.Empty;
            NowColumnsName0 = new string[0];
            NowColumnsName1 = new string[1];
            OldColumnsName0 = new List<string>();
            OldColumnsName1 = new List<string>();
            OldOutList0 = new List<int>();
            OldOutList1 = new List<int>();
            SelectedColumns = new List<string>();
            OldOptionDictStr = String.Empty;
            OptionInfoCheck = new OptionInfoCheck();
        }
    }
}
