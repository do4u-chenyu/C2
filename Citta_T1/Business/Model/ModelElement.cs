using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Citta_T1.Controls.Flow;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;

namespace Citta_T1.Business.Model
{
    public enum ElementType
    {
        Operator,   // 算子
        DataSource, // 数据源
        Relation,   // 画线关系
        Result,     // 算子运算结果
        Remark,     // 模型文档备注
        Null
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
        Null
    }
    public enum ElementStatus
    {
        Runnnig, //正在计算
        Stop,    //停止
        Done,    //运算完毕
        Suspend, //暂停
        Ready,   //已经完成算子配置,随时可以开始运算
        Null,    //初始状态
    }
    class ModelElement
    {
        private ElementStatus status;
        private ElementType type;
        private ElementSubType subType;
        private Control ctl;
        private string dataSourceFullFilePath;
        private string description;
        private int id;
        private char separator;
        private DSUtil.Encoding encoding;
        private DSUtil.ExtType extType;



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
        public string RemarkName { get => this.description; set => this.description = value; }
        public int ID { get => this.id; set => this.id = value; }
        public DSUtil.Encoding Encoding { get => this.encoding; set => this.encoding = value; }
        public DSUtil.ExtType ExtType { get => extType; set => extType = value; }
        public char Separator { get => separator; set => separator = value; }

        public ModelElement(ElementType type, Control ctl, string des, string bcpPath,ElementSubType subType, int id, 
            char separator = '\t',
            DSUtil.ExtType extType = DSUtil.ExtType.Unknow, 
            DSUtil.Encoding encoding = DSUtil.Encoding.UTF8)
        {
            Init(type, ctl, des, bcpPath, subType, id, separator, extType, encoding);
        }

        public static ModelElement CreateOperatorElement(MoveOpControl ctl, string des, ElementSubType subType, int id)
        {
            return new ModelElement(ElementType.Operator, ctl, des, "", subType, id);
        }
        public static ModelElement CreateResultElement(MoveRsControl ctl, string des, int id)
        {
            return new ModelElement(ElementType.Result, ctl, des, "",ElementSubType.Null, id,ctl.Separator, DSUtil.ExtType.Unknow,ctl.Encoding);
        }

        public static ModelElement CreateDataSourceElement(MoveDtControl ctl, string des, string fullFilePath, int id)
        {
            return new ModelElement(ElementType.DataSource, ctl, des, fullFilePath, ElementSubType.Null, id, ctl.Separator, ctl.ExtType, ctl.Encoding);
        }


        private void Init(ElementType type, Control ctl, string des, string fullFilePath,  ElementSubType subType, int id, 
            char separator,
            DSUtil.ExtType extType, 
            DSUtil.Encoding encoding)
        {
            this.type = type;
            this.subType = subType;
            this.ctl = ctl;
            this.dataSourceFullFilePath = fullFilePath;
            this.SetDescription(des);
            this.description = des;
            this.id = id;
            this.separator = separator;
            this.extType = extType;
            this.encoding = encoding;
            

        }
        
        public string GetDescription()
        {
            string des = "";
            switch (this.type)
            {
                case ElementType.DataSource:
                    des = (ctl as MoveDtControl).DescriptionName;
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


        private void SetDescription(string des)
        {
            switch (this.type)
            {
                case ElementType.DataSource:
                    (ctl as MoveDtControl).DescriptionName = des;
                    break;
                case ElementType.Operator:
                    (ctl as MoveOpControl).DescriptionName = des;
                    break;
                case ElementType.Result:
                    (ctl as MoveRsControl).DescriptionName = des;
                    break;
                default:
                    break;
            }
        }
        public string GetFullFilePath()
        {
            string path = "";
            if (this.type == ElementType.DataSource)
                path = dataSourceFullFilePath;
            else if (this.type == ElementType.Result)
                path = (ctl as MoveRsControl).FullFilePath;
            return path;
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
