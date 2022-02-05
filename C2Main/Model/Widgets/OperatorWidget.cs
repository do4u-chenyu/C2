﻿using C2.Business.Model;
using C2.Business.Option;
using C2.Controls;
using C2.Core;
using C2.Dialogs.Base;
using C2.Dialogs.C2OperatorViews;
using C2.Model.MindMaps;
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
        SqlOperator     //sql算子
    }
    public class OperatorWidget : C2BaseWidget, IRemark
    {
        private OpStatus _status;
        public const string TypeID = "OPERATOR";
        public override string Description => HasModelOperator ? HelpUtil.ModelWidgetHelpInfo : HelpUtil.OperatorWidgetHelpInfo;
        private bool isModelOperator = false;
        public OperatorWidget()
        {
            DisplayIndex = 1;
            widgetIcon = Properties.Resources.算子;
            DataSourceItem = DataItem.Empty;  // 尽量不要用null作为初值,避免空指针异常
            Option = new OperatorOption();
            ResultItem = DataItem.Empty;
            OpType = OpType.Null;
            Status = OpStatus.Null;
            ModelDataItem = new DataItem();
            _status = OpStatus.Null;

        }
        [Browsable(false)]
        #region 模型算子属性
        public bool HasModelOperator
        {
            get => this.isModelOperator;
            set
            {
                this.isModelOperator = value;
                if (value) widgetIcon = Properties.Resources.聚沙成塔;
            }

        }//是否包含模型算子
        [Browsable(false)]
        public TabItem ModelRelateTab { get; set; }//模型对应的tab
        [Browsable(false)]
        public DataItem ModelDataItem { get; set; }//模型相关属性
        #endregion
        #region 单算子属性
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
        public OpStatus Status 
        {
            get {return _status; }
            set
            {
                if (_status != value)
                {
                    Global.OnModifiedChange();
                }
                _status = value;
            }
        }  //算子状态
        public void InitStatus(OpStatus status)
        {
            _status = status;
        }
        #endregion
        public override string GetTypeID()
        {
            return TypeID;
        }

        public override void Serialize(XmlDocument dom, XmlElement node)
        {
            base.Serialize(dom, node);
            XmlElement opItemNode = node.OwnerDocument.CreateElement("op_items");
            // 模型算子持久化
            if (HasModelOperator)
            {
                ModelXmlWriter modelOp = new ModelXmlWriter("op_item", opItemNode);
                modelOp.Write("name", ModelDataItem.FileName)
                       .Write("path", ModelDataItem.FilePath)
                       .Write("subtype", "model");
            }

            // 单算子持久化
            if (OpName == null)
            {
                node.AppendChild(opItemNode);
                return;
            }

            ModelXmlWriter mxw = new ModelXmlWriter("op_item", opItemNode);
            mxw.Write("name", OpName)
               .Write("subtype", OpType)
               .Write("status", Status);

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
            {
                if (DataSourceItem.DataType == DatabaseType.Null)
                    WriteAttribute(mxw.Element, DataSourceItem, "data_item");
                else
                    WriteExternalDataSource(mxw.Element, DataSourceItem, "data_item");
            }
            /*
            *  单算子的结果持久化
            */
            if (ResultItem != null)
                WriteAttribute(mxw.Element, ResultItem, "result_item");
            node.AppendChild(opItemNode);
        }
        public override void OnDoubleClick(HandledEventArgs e) 
        {
            // 多维算子双击打开
            if (HasModelOperator)
                OpenModelDocumentTab();
            // 非多维算子弹出配置对话框
            else if (Status != OpStatus.Null)
                GenType(this)?.ShowDialog();      
        }

        public void OpenModelDocumentTab()
        {
            TabItem tab = this.ModelRelateTab;
            TabBar tabBar = Global.GetMainForm().TaskBar;
            if (tabBar.Items.Contains(tab))
                tabBar.SelectedItem = tab;
            else
            {
                Topic topic = this.Container as Topic;
                string modelDocumentName = this.ModelDataItem.FileName;
                Global.GetMainForm().LoadCanvasFormByMindMap(modelDocumentName, topic);
            }
        }
        public static C2BaseOperatorView GenType(OperatorWidget operatorWidget)
        {
            switch (operatorWidget.OpType)
            {
                case OpType.SqlOperator: return new C2SqlOperatorView(operatorWidget);
                case OpType.PythonOperator: return new C2PythonOperatorView(operatorWidget);
                case OpType.AvgOperator: return new C2AvgOperatorView(operatorWidget);
                case OpType.CustomOperator: return new C2CustomOperatorView(operatorWidget);
                case OpType.FilterOperator: return new C2FilterOperatorView(operatorWidget);
                case OpType.FreqOperator: return new C2FreqOperatorView(operatorWidget);
                case OpType.GroupOperator: return new C2GroupOperatorView(operatorWidget);
                case OpType.MaxOperator: return new C2MaxOperatorView(operatorWidget);
                case OpType.MinOperator: return new C2MinOperatorView(operatorWidget);
                case OpType.RandomOperator: return new C2RandomOperatorView(operatorWidget);
                case OpType.SortOperator: return new C2SortOperatorView(operatorWidget);
                case OpType.DataFormatOperator: return new C2DataFormatOperatorView(operatorWidget);
                default: return null;
            }
        }
        public override void Deserialize(Version documentVersion, XmlElement node)
        {
            base.Deserialize(documentVersion, node);
            XmlNodeList opitems = node.SelectNodes("op_items/op_item");
            foreach (XmlElement opItem in opitems)
            {
                string subtype = Utils.XmlUtil.GetInnerText(opItem, "subtype");
                if (string.IsNullOrEmpty(subtype))
                    return;
                if (subtype == "model")
                {
                    // 读取模型算子
                    this.HasModelOperator = true;
                    this.ModelDataItem.FileName = opItem.SelectSingleNode("name").InnerText;
                    this.ModelDataItem.FilePath = opItem.SelectSingleNode("path").InnerText;
                }
                else
                {
                    // 读取单算子
                    this.OpName = opItem.SelectSingleNode("name").InnerText;
                    this.OpType = OpUtil.OpType(opItem.SelectSingleNode("subtype").InnerText);
                    //this.Status = OpUtil.OpStatus(opItem.SelectSingleNode("status").InnerText);   文档加载100%后,这个值才能用
                    this.InitStatus(OpUtil.OpStatus(opItem.SelectSingleNode("status").InnerText));

                    XmlNode option = opItem.SelectSingleNode("option");
                    if (option != null)
                    {
                        foreach (XmlNode child in option.ChildNodes)
                            this.Option.SetOption(child.Name, child.InnerText);
                    }
                   
                    List<DataItem> tmp = new List<DataItem>();
                    XmlNodeList dataItems = opItem.SelectNodes("data_item");
                    ReadAttribute(dataItems, tmp);
                    if (tmp.Count > 0)
                    {
                        this.DataSourceItem = new List<DataItem>(tmp)[0];
                    }
                    XmlNodeList resultItems = opItem.SelectNodes("result_item");
                    tmp.Clear();
                    ReadAttribute(resultItems,tmp);
                    if (tmp.Count > 0)
                    {
                        this.ResultItem = new List<DataItem>(tmp)[0];
                    }
                }


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
