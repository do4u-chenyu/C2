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
        JoinOperator,//连接算子
        CollideOperator,//交集
        UnionOperator,
        DifferOperator,
        RandomOperator,
        FilterOperator,
        MaxOperator,
        MinOperator,
        AvgOperator,//平均
        SortOperator,//排序算子
        FreqQperator,//频率算子
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
        private string dataSourcePath;
        private string description;
        private int id;
        private DSUtil.Encoding encoding;



        public ElementType Type { get => type; set => type = value; }
        public ElementStatus Status { get => status; set => status = value; }
        public ElementSubType SubType { get => subType; set => subType = value; }
        public Point Location { get => ctl.Location; }
        public Control GetControl { get => ctl; }
        public string RemarkName { get => this.description; set => this.description = value; }
        public int ID { get => this.id; set => this.id = value; }
        public DSUtil.Encoding Encoding { get => this.encoding; set => this.encoding = value; }

        public ModelElement(ElementType type, Control ctl, string des, string bcpPath, ElementStatus status, ElementSubType subType, int id, DSUtil.Encoding encoding = DSUtil.Encoding.UTF8)
        {
            Init(type, ctl, des, bcpPath, status, subType, id, encoding);
        }

        public static ModelElement CreateOperatorElement(MoveOpControl ctl, string des, ElementStatus status, ElementSubType subType, int id)
        {
            return new ModelElement(ElementType.Operator, ctl, des, "", status, subType, id);
        }
        public static ModelElement CreateResultElement(MoveRsControl ctl, string des, ElementStatus status, ElementSubType subType, int id)
        {
            return new ModelElement(ElementType.Result, ctl, des, "", status, ElementSubType.Null, id);
        }

        public static ModelElement CreateRemarkElement(string remarkText)
        {
            return new ModelElement(ElementType.Remark, new RemarkControl(), remarkText, "", ElementStatus.Null, ElementSubType.Null, 0);
        }

        public static ModelElement CreateDataSourceElement(MoveDtControl ctl, string des, string bcpPath, int id)
        {
            return new ModelElement(ElementType.DataSource, ctl, des, bcpPath, ElementStatus.Done, ElementSubType.Null, id,ctl.Encoding);
        }


        private void Init(ElementType type, Control ctl, string des, string bcpPath, ElementStatus status, ElementSubType subType, int id, DSUtil.Encoding encoding)
        {
            this.type = type;
            this.subType = subType;
            this.ctl = ctl;
            this.status = ElementStatus.Null;
            this.dataSourcePath = bcpPath;
            this.SetName(des);
            this.description = des;
            this.id = id;
            this.encoding = encoding;
        }
        
        public string GetDescription()
        {
            string des = "";
            switch (this.type)
            {
                case ElementType.DataSource:
                    des = (ctl as MoveDtControl).textBox1.Text;
                    break;
                case ElementType.Operator:
                    des = (ctl as MoveOpControl).textBox.Text;
                    break;
                case ElementType.Result:
                    des = (ctl as MoveRsControl).textBox.Text;
                    break;
                default:
                    break;
            }
            return des;

        }

        private void SetName(string name)
        {
            switch (this.type)
            {
                case ElementType.DataSource:
                    (ctl as MoveDtControl).textBox1.Text = name;
                    break;
                case ElementType.Operator:
                    (ctl as MoveOpControl).textBox.Text = name;
                    break;
                case ElementType.Result:
                    (ctl as MoveRsControl).textBox.Text = name;
                    break;
                default:
                    break;
            }
        }
        public string GetPath()
        {
            string path = "";
            if (this.type == ElementType.DataSource)
                path = dataSourcePath;
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
