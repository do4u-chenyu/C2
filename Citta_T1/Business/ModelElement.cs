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
        MeanValueOperator,
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
        private string index;
        private string dataCode;
        private string name;
   
        public ElementType Type { get => type; set => type = value; }
        public ElementStatus Status { get => status; set => status = value; }
        public ElementSubType SubType { get => subType; set => subType = value; }

        public Point Location { get => ctl.Location; }
        public Control GetControl { get => ctl; }
        public string GetIndex { get => this.index; }
        public string GetCode { get => this.dataCode; }
        public string RemarkName { get => this.name; }


        public ModelElement(ElementType type, string name, Control ctl, ElementStatus status = ElementStatus.Null, ElementSubType subType = ElementSubType.Null, string path = "",string index = "",string datacode = "") 
        {
            this.type = type;
            this.index = index;
            this.subType = subType;
            this.ctl = ctl;
            this.status = status;
            this.dataSourcePath = path;
            this.SetName(name);
            this.dataCode = datacode;
            this.name = name;
 
        }

        public string GetName()
        {
            string name = "";
            switch (this.type)
            {
                case ElementType.DataSource:
                    name = (ctl as MoveDtControl).textBox1.Text;
                    break;
                case ElementType.Operate:
                    name = (ctl as MoveOpControl).textBox1.Text;
                    break;
                //case ElementType.remark:
                //    name =this.name;
                //    break;
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
                    (ctl as MoveDtControl).textBox1.Text = name;
                    break;
                case ElementType.Operate:
                    (ctl as MoveOpControl).textBox1.Text = name;
                    break;
                //case ElementType.remark:
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
            if (this.type == ElementType.DataSource || this.type == ElementType.Operate)
                ctl.Show();
            else
                return;
        }
        public void Hide()
        {
            if (this.type == ElementType.DataSource || this.type == ElementType.Operate)
                ctl.Hide();
            else
                return;
        }


    }
}
