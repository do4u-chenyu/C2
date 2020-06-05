using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using System;
using System.Collections.Generic;


namespace Citta_T1.OperatorViews.Base
{
    class OptionViewInfo
    {
        private MoveOpControl opControl;          // 对应的OP算子 
        private string dataPath0;                 // 左表数据源路径
        private string dataPath1;                 // 右表数据源路径
        private string[] nowColumnName0;          // 当前左表(pin0)数据源表头字段(columnName)
        private string[] nowColumnName1;          // 当前右表(pin1)数据源表头字段
        private List<string> oldColumnName0;      // 上一次左表(pin0)数据源表头字段
        private List<string> oldColumnName1;      // 上一次右表(pin1数据源表头字段
        private List<int> oldOutList0;            // 上一次用户选择的左表输出字段的索引
        private List<int> oldOutList1;            // 上一次用户选择的右表输出字段的索引
        private List<string> selectedColumns;     // 本次配置用户选择的输出字段名称
        private string oldOptionDictStr;          // 旧配置字典的字符串表述
        
        private OptionInfoCheck optionInfoCheck;  // 用户配置信息通用检查

        public static readonly OptionViewInfo Empty; 

        public MoveOpControl OpControl { get => opControl; set => opControl = value; }
        public string DataPath0 { get => dataPath0; set => dataPath0 = value; }
        public string DataPath1 { get => dataPath1; set => dataPath1 = value; }
        public string[] NowColumnName0 { get => nowColumnName0; set => nowColumnName0 = value; }
        public string[] NowColumnName1 { get => nowColumnName1; set => nowColumnName1 = value; }
        public List<string> OldColumnName0 { get => oldColumnName0; set => oldColumnName0 = value; }
        public List<string> OldColumnName1 { get => oldColumnName1; set => oldColumnName1 = value; }
        public List<int> OldOutList0 { get => oldOutList0; set => oldOutList0 = value; }
        public List<int> OldOutList1 { get => oldOutList1; set => oldOutList1 = value; }
        public List<string> SelectedColumns { get => selectedColumns; set => selectedColumns = value; }
        public string OldOptionDictStr { get => oldOptionDictStr; set => oldOptionDictStr = value; }
        public OptionInfoCheck OptionInfoCheck { get => optionInfoCheck; set => optionInfoCheck = value; }

        public OptionViewInfo()
        {
            OpControl = null;
            DataPath0 = String.Empty;
            DataPath1 = String.Empty;
            NowColumnName0 = new string[0];
            NowColumnName1 = new string[1];
            OldColumnName0 = new List<string>();
            OldColumnName1 = new List<string>();
            OldOutList0 = new List<int>();
            OldOutList1 = new List<int>();
            SelectedColumns = new List<string>();
            OldOptionDictStr = String.Empty;
            OptionInfoCheck = new OptionInfoCheck();
        }
    }
}
