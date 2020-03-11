using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Citta_T1.Controls.Flow;
using Citta_T1.Controls.Move;

namespace Citta_T1.Business
{
    public enum ElementType
    {
        Operator,
        DataSource,
        Relatetion,
        Result,
        Remark,
        Null
    }
    public enum ElementSubType
    {
        JoinOperator,
        IntersectionOperator,
        UnionOperator,
        DifferenceOperator,
        RandomSamplingOperator,
        FilterOperator,
        MaximumValueOperator,
        MinmumValueOperator,
        MeanValueOperator,
        Null
    }
    public enum ElementStatus
    {
        Runnnig,//正在计算
        Stop,//停止
        Done,
        Null,
        Suspend//暂停
    }
    class ModelElement
    {
        private ElementStatus status;
        private ElementType type;
        private ElementSubType subType;
        private Control ctl;
        private string dataSourcePath;
        private string description;
   
        public ElementType Type { get => type; set => type = value; }
        public ElementStatus Status { get => status; set => status = value; }
        public ElementSubType SubType { get => subType; set => subType = value; }

        public Point Location { get => ctl.Location; }
        public Control GetControl { get => ctl; }
 
        public string RemarkName { get => this.description; }

       
        public ModelElement(ElementType type, Control ctl, string des, string bcpPath, ElementStatus status, ElementSubType subType) 
        {
            Init(type, ctl, des, bcpPath, status, subType);
        }

        // 加载和界面拖入时构造DataSource元素
        public ModelElement(ElementType type, Control ctl, string des, string bcpPath)
        {
            Init(type, ctl, des, bcpPath, ElementStatus.Null, ElementSubType.Null);
        }

        // 加载时构造Operator元素
        public ModelElement(ElementType type, Control ctl, string des, ElementStatus status, ElementSubType subType)
        {
            Init(type, ctl, des, "", status, subType);
        }
        // 拖入时构造Operator元素
        public ModelElement(ElementType type, Control ctl, string des, ElementSubType subType)
        {
            Init(type, ctl, des, "", ElementStatus.Null, subType);
        }
        // 加载Remark元素
        public ModelElement(ElementType type, Control ctl, string des)
        {
            Init(type, ctl, des, "", ElementStatus.Null, ElementSubType.Null);
        }

        private void Init(ElementType type, Control ctl, string des, string bcpPath, ElementStatus status, ElementSubType subType)
        {
            this.type = type;
            this.subType = subType;
            this.ctl = ctl;
            this.status = ElementStatus.Null;
            this.dataSourcePath = bcpPath;
            this.SetName(des);
            this.description = des;
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
                    des = (ctl as MoveOpControl).textBox1.Text;
                    break;
                //case ElementType.Remark:
                //    name = (ctl as RemarkControl).RemarkText;
                //    break;
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
                    (ctl as MoveOpControl).textBox1.Text = name;
                    break;
                //case ElementType.Remark:
                //    (ctl as RemarkControl).RemarkText = name;
                //    break;
                default:
                    break;
            }
        }
        public string GetPath()
        {
            string path = "";
            if (this.type == ElementType.DataSource)
            {
                path = dataSourcePath;
            }
            return path;
        }

        public void Show()
        {
            if (this.type == ElementType.DataSource || this.type == ElementType.Operator)
                ctl.Show();
            else
                return;
        }
        public void Hide()
        {
            if (this.type == ElementType.DataSource || this.type == ElementType.Operator)
                ctl.Hide();
            else
                return;
        }


    }
}
