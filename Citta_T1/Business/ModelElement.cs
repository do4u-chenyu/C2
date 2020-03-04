using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Citta_T1.Controls;

namespace Citta_T1.Business
{
    enum ElementType
    {
        Operate,
        DataSource,
        Relatetion,
        Result,
        remark
    }
    enum ElementSubType
    {
        JoinOperator,
        IntersectionOperator,
        UnionOperator,
        DifferenceOperator,
        RandomSamplingOperator,
        FilterOperator,
        MaximumValueOperator,
        MinmumValueOperator,
        Null
    }
    enum ElementStatus
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
        public ElementType Type { get => type; set => type = value; }
        public ElementStatus Status { get => status; set => status = value; }
        public ElementSubType SubType { get => subType; set => subType = value; }

        public Point Location { get => ctl.Location; }

        public ModelElement(ElementType type, string name, Control ctl, ElementStatus status = ElementStatus.Null, ElementSubType subType = ElementSubType.Null, string path = "")
        {
            this.type = type;
            this.subType = subType;
            this.ctl = ctl;
            this.status = status;
            this.dataSourcePath = path;
            this.SetName(name);
        }

        public string GetName()
        {
            string name = "";
            switch (this.type)
            {
                case ElementType.DataSource:
                    name = (ctl as MoveOpControl).textBox1.Text;
                    break;
                case ElementType.Operate:
                    name = (ctl as MoveOpControl).textBox1.Text;
                    break;
                case ElementType.remark:
                    name = (ctl as RemarkControl).RemarkText;
                    break;
                default:
                    break;
            }
            return name;

        }

        public void SetName(string name)
        {
            switch (this.type)
            {
                case ElementType.DataSource:
                    (ctl as MoveOpControl).textBox1.Text = name;
                    break;
                case ElementType.Operate:
                    (ctl as MoveOpControl).textBox1.Text = name;
                    break;
                case ElementType.remark:
                    (ctl as RemarkControl).RemarkText = name;
                    break;
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
            ctl.Show();
        }
        public void Hide()
        {
            ctl.Hide();
        }


    }
}
