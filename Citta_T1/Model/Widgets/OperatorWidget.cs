using C2.Business.Option;
using C2.Controls.MapViews;
using System;
using System.Drawing;
using System.Xml;

namespace C2.Model.Widgets
{
    public enum OpStatus
    {
        Null,
        Ready,
        Done
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
        }

        public string OpName { get; set; }  //菜单栏名称
        public string OpType { get; set; }  //算子类型

        public DataItem DataSourceItem { get; set; }  //选中的数据源
        public OperatorOption Option { get; set; }  //算子配置内容
        public DataItem ResultItem { get; set; }  //生成的结果
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
