using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Controls.Move;
using Citta_T1.Controls.Flow;

namespace Citta_T1.Business
{
    class ModelDocumentDao
    {
        private ModelDocument currentDocument;
        private List<ModelDocument> modelDocuments;
        
        internal List<ModelDocument> ModelDocuments { get => modelDocuments; set => modelDocuments = value; }
        internal ModelDocument CurrentDocument { get => currentDocument; set => currentDocument = value; }

        public ModelDocumentDao()
        {
            modelDocuments = new List<ModelDocument>();
            
        }
        public void AddBlankDocument( string modelTitle,  string userName)
        {
            ModelDocument modelDocument = new ModelDocument(modelTitle, userName);
            foreach (ModelDocument md in this.modelDocuments)
                md.Hide();
            this.modelDocuments.Add(modelDocument);
            this.currentDocument = modelDocument;    
        }
        public string SaveDocument()
        {
            this.currentDocument.Save();
            this.currentDocument.Dirty = false;
            return this.currentDocument.ModelDocumentTitle;
        }
        public ModelDocument LoadDocument(string modelTitle,string userName)
        {
            ModelDocument md = new ModelDocument(modelTitle, userName);
            md.Load();
            md.Hide();
            this.modelDocuments.Add(md);
            this.currentDocument = md;
            return md;

        }
        public void SwitchDocument(string modelTitle)//----------------------------------------
        {
            this.currentDocument = FindModelDocument(modelTitle);
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
            if (ct is MoveDtControl)
            {
                MoveDtControl dt = (ct as MoveDtControl);
                ModelElement e = ModelElement.CreateDataSourceElement(dt, dt.MDCName, dt.GetBcpPath());
                this.currentDocument.AddModelElement(e);
                Console.WriteLine("数据源对应的BCP文件路径:" + dt.GetBcpPath());
                return;
            }

            if (ct is MoveOpControl)
            {
                MoveOpControl op = (ct as MoveOpControl);
                ModelElement e = ModelElement.CreateOperatorElement(op, op.ReName, ElementStatus.Null, SEType(op.SubTypeName));
                this.currentDocument.AddModelElement(e);
                return;
            }

        }
        public void DeleteDocumentElement(Control ct)
        {
            this.currentDocument.DeleteModelElement(ct);
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
        public List<ModelElement> DeleteDocumentElements()
        {
            if (this.currentDocument == null)
                throw new NullReferenceException();
            List<ModelElement> modelElements = this.currentDocument.ModelElements();
            this.ModelDocuments.Remove(this.currentDocument);
            Console.WriteLine(currentDocument.ModelDocumentTitle+"删除的模型文档");
            return modelElements; 
        }
        public void UpdateRemark(RemarkControl remarkControl)
        { 
            if (this.currentDocument == null)
                throw new NullReferenceException();
            List<ModelElement> modelElements = this.currentDocument.ModelElements();          
            foreach (ModelElement me in modelElements)
            {
                if (me.Type == ElementType.Remark)
                {
                    me.RemarkName = remarkControl.RemarkText;
                    return;
                }            
            }
            ModelElement remarkElement = ModelElement.CreateRemarkElement(remarkControl.RemarkText);
            modelElements.Add(remarkElement);
        }
        public string GetRemark()
        {
            string remark = "";
            if (this.currentDocument == null)
                throw new NullReferenceException();
            List<ModelElement> modelElements = this.currentDocument.ModelElements();
            foreach (ModelElement me in modelElements)
            {
                if (me.Type == ElementType.Remark)
                {
                    remark = me.RemarkName;
                    break;
                }     
            }
            return remark;

        }

        public bool NewUserLogin(string username)
        {
            string userDir = Directory.GetCurrentDirectory() + "\\cittaModelDocument\\" + username;
            return !Directory.Exists(userDir);
        }
    }
}
