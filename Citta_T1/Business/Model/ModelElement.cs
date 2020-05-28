using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Controls.Move.Rs;
using Citta_T1.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Citta_T1.Business.Model
{
    public enum ElementType
    {
        Operator,   // 算子
        DataSource, // 数据源
        Relation,   // 画线关系
        Result,     // 算子运算结果
        Remark,     // 模型文档备注
        Empty       // 空算子,防止NULL指针异常用
    }
    public enum ElementSubType
    {
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
        KeyWordOperator, //关键词过滤
        Null
    }
    public enum ElementStatus
    {
        Runnnig, //正在计算
        Stop,    //停止
        Done,    //运算完毕 
        Suspend, //暂停
        Ready,   //已经完成算子配置,随时可以开始运算
        Warn,   //配置错误状态
        Null,    //初始状态
    }
    public class ModelElement
    {
        private ElementStatus status;
        private ElementType type;
        private ElementSubType subType;
        private Control ctl;
        private int id;
        private char separator;
        private DSUtil.Encoding encoding;

        public ElementType Type { get => type; set => type = value; }
        public ElementStatus Status
        { 
            get
            {
                switch (this.type)
                {
                    case ElementType.DataSource:
                        this.status = ElementStatus.Done;
                        break;
                    case ElementType.Operator:
                        this.status = (ctl as MoveOpControl).Status;
                        break;
                    case ElementType.Result:
                        this.status = (ctl as MoveRsControl).Status;
                        break;
                    default:
                        break;
                }
                return this.status;
            }
            set
            {
                switch (this.type)
                {
                    case ElementType.Operator:
                        (ctl as MoveOpControl).Status = value;
                        break;
                    case ElementType.Result:
                        (ctl as MoveRsControl).Status = value;
                        break;
                    default:
                        break;
                }
            }
        }
        public ElementSubType SubType { get => subType; set => subType = value; }
        public Point Location { get => ctl.Location; }
        public Control GetControl { get => ctl; }
        public int ID { get => this.id; set => this.id = value; }
        public DSUtil.Encoding Encoding
        {
            get
            {
                switch (this.type)
                {
                    case ElementType.DataSource:
                        this.encoding = (ctl as MoveDtControl).Encoding;
                        break;
                    case ElementType.Result:
                        this.encoding = (ctl as MoveRsControl).Encoding;
                        break;
                    default:
                        break;
                }
                return this.encoding;
            }
            set
            {
                switch (this.type)
                {
                    case ElementType.Operator:
                        (ctl as MoveDtControl).Encoding = value;
                        break;
                    case ElementType.Result:
                        (ctl as MoveRsControl).Encoding = value;
                        break;
                    default:
                        break;
                }
            }
        }
        public DSUtil.ExtType ExtType
        {
            get
            {
                switch (this.type)
                {
                    case ElementType.DataSource:
                        return (ctl as MoveDtControl).ExtType;
                        break;
                    case ElementType.Result:
                        return (ctl as MoveRsControl).ExtType;
                        break;
                    default:
                        break;
                }
                return DSUtil.ExtType.Unknow;
            }
        }
        public char Separator {
            get {
                switch (this.type)
                {
                    case ElementType.DataSource:
                        this.separator = (ctl as MoveDtControl).Separator;
                        break;
                    case ElementType.Result:
                        this.separator = (ctl as MoveRsControl).Separator;
                        break;
                    default:
                        break;
                }
                return this.separator;
            }
            set 
            {
                switch (this.type)
                {
                    case ElementType.Operator:
                        (ctl as MoveDtControl).Separator = value;
                        break;
                    case ElementType.Result:
                        (ctl as MoveRsControl).Separator = value;
                        break;
                    default:
                        break;
                }
            }
        }
        public string Description
        {
            get
            {
                string des = String.Empty;
                switch (this.type)
                {
                    case ElementType.DataSource:
                        des = (ctl as MoveDtControl).Description;
                        break;
                    case ElementType.Operator:
                        des = (ctl as MoveOpControl).DescriptionName;
                        break;
                    case ElementType.Result:
                        des = (ctl as MoveRsControl).DescriptionName;
                        break;
                    default:
                        break;
                }
                return des;
            }
            set
            {
                switch (this.type)
                {
                    case ElementType.DataSource:
                        (ctl as MoveDtControl).Description = value;
                        break;
                    case ElementType.Operator:
                        (ctl as MoveOpControl).DescriptionName = value;
                        break;
                    case ElementType.Result:
                        (ctl as MoveRsControl).DescriptionName = value;
                        break;
                    default:
                        break;
                }
            }
        }
        public string FullFilePath
        {
            get
            {
                string path = String.Empty;
                if (this.type == ElementType.DataSource)
                    path = (ctl as MoveDtControl).FullFilePath;
                else if (this.type == ElementType.Result)
                    path = (ctl as MoveRsControl).FullFilePath;
                return path;
            }
        }


        private ModelElement()
        {
            type = ElementType.Empty;
        }

        public readonly static ModelElement Empty = new ModelElement(); 

        public ModelElement(ElementType type, 
            Control ctl, 
            string description, 
            ElementSubType subType, 
            int id, 
            char separator = '\t',
            DSUtil.Encoding encoding = DSUtil.Encoding.UTF8)
        {
            Init(type, ctl, description, subType, id, separator, encoding);
        }

        public static ModelElement CreateOperatorElement(MoveOpControl ctl, string description, ElementSubType subType, int id)
        {
            return new ModelElement(ElementType.Operator, ctl, description, subType, id);
        }
        public static ModelElement CreateResultElement(MoveRsControl ctl, string description, int id)
        {
            return new ModelElement(ElementType.Result, ctl, description, ElementSubType.Null, id,ctl.Separator,ctl.Encoding);
        }

        public static ModelElement CreateDataSourceElement(MoveDtControl ctl, string description, int id)
        {
            return new ModelElement(ElementType.DataSource, ctl, description, ElementSubType.Null, id, ctl.Separator, ctl.Encoding);
        }


        private void Init(ElementType type, Control ctl, string description, ElementSubType subType, int id, 
            char separator,
            DSUtil.Encoding encoding)
        {
            this.type = type;
            this.subType = subType;
            this.ctl = ctl;
            this.Description = description;
            this.id = id;
            this.separator = separator;
            this.encoding = encoding;
        }
        



        public void Show()
        {
            switch (this.type)
            {
                case ElementType.DataSource:
                case ElementType.Operator:
                case ElementType.Result:
                    ctl.Show();
                    break;
                default:
                    break;
            }

        }
        public void Hide()
        {
            switch (this.type)
            {
                case ElementType.DataSource:
                case ElementType.Operator:
                case ElementType.Result:
                    ctl.Hide();
                    break;
                default:
                    break;
            }
        }


    }
}
