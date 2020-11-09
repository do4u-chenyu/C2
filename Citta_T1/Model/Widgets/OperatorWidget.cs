using C2.Business.Option;
using C2.Controls.MapViews;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml;

namespace C2.Model.Widgets
{
    public enum OpStatus
    {
        Null,
        Ready,
        Done,
        Warn
    }

    public enum OpType
    {
        Null,
        RandomOperator, //随机采样
        FilterOperator, //过滤算子
        MaxOperator,    //取最大值
        MinOperator,    //取最小值
        AvgOperator,    //平均
        SortOperator,   //排序算子
        FreqOperator,   //频率算子 
        GroupOperator,  //分组算子
        CustomOperator, //AI实践
        DataFormatOperator, //数据标准化
        PythonOperator, //python算子
        ModelOperator //模型
    }
    public class OperatorWidget : Widget, IRemark
    {
        public const string TypeID = "OPERATOR";

        public OperatorWidget()
        {
            DisplayIndex = 1;
            widgetIcon = Properties.Resources.operator_w_icon;
            DataSourceItem = DataItem.Empty;  // 尽量不要用null作为初值,避免空指针异常
            Option = new OperatorOption();
            ResultItem = DataItem.Empty;
            Status = OpStatus.Null;
            OpType = OpType.Null;
        }

        [Browsable(false)]
        public string OpName { get; set; }  //菜单栏名称
        [Browsable(false)]
        public OpType OpType { get; set; }  //算子类型
        [Browsable(false)]
        public DataItem DataSourceItem { get; set; }  //选中的数据源
        [Browsable(false)]
        public OperatorOption Option { get; set; }  //算子配置内容
        [Browsable(false)]
        public DataItem ResultItem { get; set; }  //生成的结果
        [Browsable(false)]
        public OpStatus Status { get; set; }  //算子状态
        
        
        
        public override bool ResponseMouse
        {
            get
            {
                return true;
            }
        }

        public override Size CalculateSize(MindMapLayoutArgs e)
        {
            return new Size(20, 20);
        }

        public override string GetTypeID()
        {
            return TypeID;
        }

        public override void Serialize(XmlDocument dom, XmlElement node)
        {
            base.Serialize(dom, node);
            //TODO
            //文档持久化
        }

        public override void Deserialize(Version documentVersion, XmlElement node)
        {
            base.Deserialize(documentVersion, node);
            //TODO
            //文档持久化
        }
    }
}
