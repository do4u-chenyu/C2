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
        Warn,    //配置错误状态
        Null,    //初始状态
    }
    public class ModelElement
    {
        private Control ctl;

        public ElementType Type { get; set; }
        public Control InnerControl { get => ctl; }

        #region  封装底层控件属性,Location, ID, Encoding, ExtType, Separator, Description, FullFilePath, Status

        public ElementSubType SubType { 
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
        public int ID {
            get
            {
                switch (this.Type)
                { 
                    case ElementType.Operator:
                        return (ctl as MoveOpControl).ID;
                    case ElementType.DataSource:
                        return (ctl as MoveDtControl).ID;
                    case ElementType.Result:
                        return (ctl as MoveRsControl).ID;
                    default:
                        return -1;              
                }
            }
        }
        public DSUtil.Encoding Encoding
        {
            get
            {
                switch (this.Type)
                {
                    case ElementType.DataSource:
                        return (ctl as MoveDtControl).Encoding;
                    case ElementType.Result:
                        return (ctl as MoveRsControl).Encoding;
                    default:
                        return DSUtil.Encoding.NoNeed;
                }
            }
            set
            {
                switch (this.Type)
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
                switch (this.Type)
                {
                    case ElementType.DataSource:
                        return (ctl as MoveDtControl).ExtType;
                    case ElementType.Result:
                        return (ctl as MoveRsControl).ExtType;
                    default:
                        return DSUtil.ExtType.Unknow;
                }
               
            }
        }
        public char Separator {
            get {
                switch (this.Type)
                {
                    case ElementType.DataSource:
                        return (ctl as MoveDtControl).Separator;
                    case ElementType.Result:
                        return (ctl as MoveRsControl).Separator;
                    default:
                        return '\t';
                }
               
            }
            set 
            {
                switch (this.Type)
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
                switch (this.Type)
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
                switch (this.Type)
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
                if (this.Type == ElementType.DataSource)
                    path = (ctl as MoveDtControl).FullFilePath;
                else if (this.Type == ElementType.Result)
                    path = (ctl as MoveRsControl).FullFilePath;
                return path;
            }
        }

        public ElementStatus Status
        {
            get
            {
                switch (this.Type)
                {
                    case ElementType.DataSource:
                        return ElementStatus.Done;
                    case ElementType.Operator:
                        return (ctl as MoveOpControl).Status;
                    case ElementType.Result:
                        return (ctl as MoveRsControl).Status;
                    default:
                        return ElementStatus.Null;
                }
            }
            set
            {
                switch (this.Type)
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
        #endregion


        public readonly static ModelElement Empty = new ModelElement();

        private ModelElement()
        {
            Type = ElementType.Empty;
        }

       
        private ModelElement(ElementType type,  Control ctl)
        {
            Init(type, ctl);
        }

        public static ModelElement CreateOperatorElement(MoveOpControl ctl)
        {
            return new ModelElement(ElementType.Operator, ctl);
        }
        public static ModelElement CreateResultElement(MoveRsControl ctl)
        {
            return new ModelElement(ElementType.Result, ctl);
        }

        public static ModelElement CreateDataSourceElement(MoveDtControl ctl)
        {
            return new ModelElement(ElementType.DataSource, ctl);
        }

        private void Init(ElementType type, Control ctl)
        {
            this.Type = type;
            this.ctl = ctl; 
        }
        
        public void Show()
        {
            switch (this.Type)
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
            switch (this.Type)
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
