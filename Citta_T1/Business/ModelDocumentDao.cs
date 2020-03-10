using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Controls.Move;

namespace Citta_T1.Business
{
    class ModelDocumentDao
    {
        private List<ModelDocument> modelDocuments;
        private ModelDocument currentDocument;
        internal List<ModelDocument> ModelDocuments { get => modelDocuments; set => modelDocuments = value; }
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
        public List<ModelElement>  LoadDocuments(string modelTitle,string userName)
        {
            List<Control> controls = new List<Control>();
            ModelDocument md = new ModelDocument(modelTitle, userName);
            this.modelDocuments.Add(md);
            List<ModelElement> modelElements = md.Load();
            return modelElements;

        }
        public void SwitchDocument(string modelTitle)//----------------------------------------
        {
            this.currentDocument = FindModelDocument(modelTitle);
            Console.WriteLine(modelTitle+"默认打开的模型");

            if (this.currentDocument == null)
                throw new NullReferenceException();
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
                ModelElement modelElement = new ModelElement(ElementType.Operator, (ct as MoveOpControl).ReName, ct, 
                    ElementStatus.Null, SEType((ct as MoveOpControl).subTypeName));
                this.currentDocument.AddModelElement(modelElement);
            }
            else if (ct.Name == "MoveDtControl")
            {
                ModelElement modelElement = new ModelElement(ElementType.DataSource, (ct as MoveDtControl).MDCName, ct, 
                    ElementStatus.Null, ElementSubType.Null, 
                    (ct as MoveDtControl).Name,//Program.inputDataDict[(ct as MoveDtControl).GetIndex].filePath, 
                    (ct as MoveDtControl).GetBcpPath(), 
                    Program.DataPreviewDict[(ct as MoveDtControl).GetBcpPath()]);//Program.inputDataDict[(ct as MoveDtControl).GetIndex].content); 
                this.currentDocument.AddModelElement(modelElement);
                Console.WriteLine("数据源对应的BCP文件路径:" + (ct as MoveDtControl).GetBcpPath());
            }
           
        }
        public void DeleteDocumentOperator(Control ct)
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
            List<ModelElement> modelElements = this.currentDocument.CurrentDocumentElement();
            this.ModelDocuments.Remove(this.currentDocument);
            Console.WriteLine(currentDocument.ModelDocumentTitle+"删除的模型文档");
            return modelElements; 
        }
    }
}
