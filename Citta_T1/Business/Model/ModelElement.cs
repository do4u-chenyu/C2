using Citta_T1.Controls.Move;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Controls.Move.Rs;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Citta_T1.Business.Model
{
    public enum ElementType
    {
        Empty,      // 空算子,防止NULL指针异常用
        Operator,   // 算子
        DataSource, // 数据源
        Relation,   // 画线关系
        Result,     // 算子运算结果
        Remark      // 模型文档备注     
    }
    public enum ElementSubType
    {
        Null,
        RelateOperator, //关联算子
        CollideOperator,//取交集
        UnionOperator,  //取并集
        DifferOperator, //取差集
        RandomOperator, //随机采样
        FilterOperator, //过滤算子
        MaxOperator,    //取最大值
        MinOperator,    //取最小值
        AvgOperator,    //平均
        SortOperator,   //排序算子
        FreqOperator,   //频率算子 
        GroupOperator,  //分组算子
        CustomOperator1, //自定义算子, 一元算子
        CustomOperator2, //自定义算子, 二元算子
        PythonOperator,  //Python算子
        DataFormatOperator, //数据标准化
        KeywordOperator  //关键词过滤  
    }
    public enum ElementStatus
    {
        Null,    //初始状态
        Runnnig, //正在计算
        Stop,    //停止
        Done,    //运算完毕 
        Suspend, //暂停
        Ready,   //已经完成算子配置,随时可以开始运算
        Warn     //配置错误状态  
    }
    public class ModelElement
    {
        private MoveBaseControl ctl;
        public ElementType Type { get; set; }
        public MoveBaseControl InnerControl { get => ctl; }

        #region  封装底层控件属性,Location, ID, Encoding, ExtType, Separator, Description, FullFilePath, Status

        public ElementSubType SubType
        {
            get
            {
                switch (this.Type)
                {
                    case ElementType.Operator:
                        return OpUtil.SEType((ctl as MoveOpControl).SubTypeName);
                    case ElementType.DataSource:
                    case ElementType.Result:
                    default:
                        return ElementSubType.Null;
                }
            }
        }

        public Point Location { get => ctl.Location; }

        public int ID { get => ctl.ID; }

        public OpUtil.Encoding Encoding { get => ctl.Encoding; set => ctl.Encoding = value; }

        public OpUtil.ExtType ExtType { get => ctl.ExtType; }

        public char Separator { get => ctl.Separator; set => ctl.Separator = value; }

        public string Description { get => ctl.Description; set => ctl.Description = value; }

        public string FullFilePath { get => ctl.FullFilePath; set => ctl.FullFilePath = value; }

        public ElementStatus Status { get => ctl.Status; set => ctl.Status = value; }
        #endregion


        public readonly static ModelElement Empty = new ModelElement();

        private ModelElement()
        {
            Type = ElementType.Empty;
        }


        private ModelElement(ElementType type, MoveBaseControl ctl)
        {
            Init(type, ctl);
        }


        public static ModelElement CreateModelElement(MoveBaseControl ctl)
        {
            return new ModelElement(ctl.Type, ctl);
        }

        private void Init(ElementType type, MoveBaseControl ctl)
        {
            this.Type = type;
            this.ctl = ctl;
        }

        public void Show()
        {
            ctl.Show();
        }
        public void Hide()
        {
            ctl.Hide();
        }

        public void Enable()
        {
            ctl.Enabled = true;
        }
        public void UnEnable()
        {
            ctl.Enabled = false;
        }

        public static ModelElement CreateModelElement(Dictionary<string, string> dict)
        {
            if (!(dict.ContainsKey("id")
                && dict.ContainsKey("name")
                && dict.ContainsKey("location")
                && dict.ContainsKey("type")))
                return ModelElement.Empty;
            string type = dict["type"];
            string name = dict["name"];
            int id = Convert.ToInt32(dict["id"]);
            Point location = OpUtil.ToPointType(dict["location"]);

            if (type == "DataSource")
            {
                if (!(dict.ContainsKey("path")
                    && dict.ContainsKey("separator")
                    && dict.ContainsKey("encoding")))
                    return ModelElement.Empty;
                string path = dict["path"];
                char separator = ConvertUtil.TryParseAscii(dict["separator"]);
                OpUtil.Encoding encoding = OpUtil.EncodingEnum(dict["encoding"]);
                MoveDtControl Control = new MoveDtControl(path, 0, name, location)
                {
                    ID = id,
                    Separator = separator,
                    Encoding = encoding
                };
                return CreateModelElement(Control);
            }
            else if (type == "Operator")
            {
                if (!(dict.ContainsKey("subtype")
                    && dict.ContainsKey("status")
                    && dict.ContainsKey("enableoption")))
                    return ModelElement.Empty;
                string subType = OpUtil.SubTypeName(dict["subtype"]);
                bool enableOption = Convert.ToBoolean(dict["enableoption"]);
                ElementStatus status = OpUtil.EStatus(dict["status"]);
                MoveOpControl Control = new MoveOpControl(0, name, subType, location)
                {
                    ID = id,
                    Status = status,
                    EnableOption = enableOption
                };
                return CreateModelElement(Control);
            }
            else
            {
                if (!(dict.ContainsKey("status")
                    && dict.ContainsKey("path")
                    && dict.ContainsKey("separator")
                    && dict.ContainsKey("encoding")))
                    return ModelElement.Empty;
                string path = dict["path"];
                ElementStatus status = OpUtil.EStatus(dict["status"]);
                char separator = ConvertUtil.TryParseAscii(dict["separator"]);
                OpUtil.Encoding encoding = OpUtil.EncodingEnum(dict["encoding"]);
                MoveRsControl Control = new MoveRsControl(0, name, location)
                {
                    ID = id,
                    Status = status,
                    FullFilePath = path,
                    Separator = separator,
                    Encoding = encoding
                };
                return CreateModelElement(Control);
            }

        }
    }
}
