using C2.Business.Model;
using C2.Business.Option;
using C2.Controls;
using C2.Utils;
using System;
using System.Collections.Generic;
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
    }
    public class OperatorWidget : C2BaseWidget, IRemark
    {
        public const string TypeID = "OPERATOR";
        public override string Description => HelpUtil.OperatorWidgetHelpInfo;
        public OperatorWidget()
        {
            DisplayIndex = 1;
            widgetIcon = Properties.Resources.算子;
            DataSourceItem = DataItem.Empty;  // 尽量不要用null作为初值,避免空指针异常
            Option = new OperatorOption();
            ResultItem = DataItem.Empty;
            OpType = OpType.Null;
            Status = OpStatus.Null;
        }
        [Browsable(false)]
        public bool HasModelOperator { get; set; }//是否包含模型算子
        [Browsable(false)]
        public TabItem ModelRelateTab { get; set; }//模型对应的tab
        [Browsable(false)]
        public DataItem ModelDataItem { get; set; }//模型相关属性
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
         
        public override string GetTypeID()
        {
            return TypeID;
        }

        public override void Serialize(XmlDocument dom, XmlElement node)
        {
            base.Serialize(dom, node);
            if (OpName == null)
                return;
            XmlElement opItemNode = node.OwnerDocument.CreateElement("op_items");          
            ModelXmlWriter mxw = new ModelXmlWriter("op_item", opItemNode);
            mxw.Write("name", OpName)
               .Write("subtype", OpType)
               .Write("status", Status);

            // 模型算子无后续信息，直接返回
            //if (OpType == OpType.ModelOperator)
            //{
            //    node.AppendChild(opItemNode);
            //    return;
            //}             
            /*
             *  单算子配置
             */
            ModelXmlWriter opWrite = new ModelXmlWriter("option", mxw.Element);
            foreach (KeyValuePair<string, string> kvp in Option.OptionDict)
            {
                opWrite.Write(kvp.Key, kvp.Value);
            }
            /*
            *  单算子的数据源持久化
            */
            if (DataSourceItem != null)
                WriteAttribute(opItemNode, DataSourceItem, "data_item");
            /*
            *  单算子的结果持久化
            */
            if (ResultItem != null)
                WriteAttribute(opItemNode, ResultItem, "result_item");
            node.AppendChild(opItemNode);
        }

        public override void Deserialize(Version documentVersion, XmlElement node)
        {
            base.Deserialize(documentVersion, node);
            var opitems = node.SelectNodes("op_items/op_item");
            foreach (XmlElement opItem in opitems)
            {
                string subtype = Utils.XmlUtil.GetInnerText(opItem, "subtype");
                if (string.IsNullOrEmpty(subtype))
                    return;
                // 读取模型算子
                //if (subtype == OpType.ModelOperator.ToString())
                //{

                //    return;
                //}
                // 读取单算子
  
            }
        }

        public Image GetOpOpenOperatorImage()
        {
            switch (Status)
            {
                case OpStatus.Null:
                    return Properties.Resources.opSet;
                case OpStatus.Ready:
                    return Properties.Resources.opSetSuccess;
                case OpStatus.Done:
                    return Properties.Resources.opDone;
                case OpStatus.Warn:
                    return Properties.Resources.opWarn;
                default:
                    return Properties.Resources.算子;
            }
        }
    }
}
