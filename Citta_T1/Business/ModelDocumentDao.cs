using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Business;
using Citta_T1.Controls;

namespace Citta_T1.Business
{
    class ModelDocumentDao
    {
        private List<ModelDocument> modelDocuments;
        private ModelDocument currentDocument;

        internal ModelDocument CurrentDocument { get => currentDocument; set => currentDocument = value; }

        public ModelDocumentDao()
        {
            modelDocuments = new List<ModelDocument>();
        }
        public void AddDocument( string modelTitle,  string userName)
        {
            ModelDocument modelDocument = new ModelDocument(modelTitle, userName);
            Console.WriteLine(modelTitle+"--是新建模型的名称");
            this.modelDocuments.Add(modelDocument);
            this.currentDocument = modelDocument;    
            foreach (ModelDocument document in modelDocuments)
            {
                if (document != currentDocument)
                    document.Hide();
            }
        }
        public void SaveDocument()
        {

            this.currentDocument.Save();
            this.currentDocument.Dirty = false;
        }
        public List<Control>  LoadDocuments(string modelTitle,string userName)
        {
            List<Control> controls = new List<Control>();
            ModelDocument md = new ModelDocument(modelTitle, userName);
            this.modelDocuments.Add(md);
            List<ModelElement> modelElements = md.Load();
            foreach (ModelElement me in modelElements)
            {
                switch (me.Type)
                {
                    case ElementType.DataSource:
                        //name = (ctl as MoveOpControl).textBox1.Text;
                        break;
                    case ElementType.Operate:
                        MoveOpControl moControl = new MoveOpControl(0, me.GetName(),me.Location);//默认是0,缩放比例
                        controls.Add(moControl);
                        break;
                    case ElementType.remark:
                        //name = (ctl as RemarkControl).RemarkText;
                        break;
                    default:
                        break;
                }
            }
            return controls;

        }

        public void SwitchDocument(string modelTitle)//----------------------------------------
        {
            this.currentDocument = FindModelDocument(modelTitle);
            Console.WriteLine(modelTitle+"默认打开的模型");

            if (this.currentDocument == null)
            {
                throw new NullReferenceException();
            }
            foreach (ModelDocument md in this.modelDocuments)
            {
                if (md.ModelDocumentTitle == modelTitle)
                    md.Show();
                else
                    md.Hide();
            }
            
        }
        public void AddDocumentOperator(Control ct)
        {
            this.currentDocument.Dirty = true;
            if (ct.Name == "MoveOpControl")
            {
                ModelElement modelElement = new ModelElement(ElementType.Operate, (ct as MoveOpControl).ReName, ct,ElementStatus.Null, SEType((ct as MoveOpControl).ReName));
                this.currentDocument.AddModelElement(modelElement);
            }
           
        }
        public ElementSubType SEType(string subType)
        {
            string type = "";
            switch (subType)
            {
                case "连接算子":
                    type = "JoinOperator";
                    break;
                case "取交集":
                    type = "IntersectionOperator";
                    break;
                case "取并集":
                    type = "UnionOperator";
                    break;
                case "取差集":
                    type = "DifferenceOperator";
                    break;
                case "随机采样":
                    type = "RandomSamplingOperator";
                    break;
                case "过滤算子":
                    type = "FilterOperator";
                    break;
                case "取最大值":
                    type = "MaximumValueOperator";
                    break;
                case "取最小值":
                    type = "MinmumValueOperator";
                    break;
                case "取平均值":
                    type = "MeanValueOperator";
                    break;
                default:
                    break;
            }
            return (ElementSubType)Enum.Parse(typeof(ElementSubType), type); 
        }
        private ModelDocument FindModelDocument(string modelTitle)
        {
            foreach (ModelDocument md in this.modelDocuments)
            {
                if (md.ModelDocumentTitle == modelTitle)
                    return md;
            }
            return null;
        }

    }
}
