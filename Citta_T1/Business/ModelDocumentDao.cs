using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Controls.Move;
using Citta_T1.Controls.Flow;
using System.Xml;

namespace Citta_T1.Business
{
    class ModelDocumentDao
    {
        private ModelDocument currentDocument;
        private List<ModelDocument> modelDocuments;
        
        internal List<ModelDocument> ModelDocuments { get => modelDocuments; set => modelDocuments = value; }
        internal ModelDocument CurrentDocument { get => currentDocument; set => currentDocument = value; }
        string UserInfoPath = Directory.GetCurrentDirectory().ToString() + "\\cittaModelDocument" + "\\UserInformation.xml";
        public ModelDocumentDao()
        {
            modelDocuments = new List<ModelDocument>();         
        }
        public void AddBlankDocument(string modelTitle,  string userName)
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
            md.ResetCount();
            this.currentDocument = md;
            this.modelDocuments.Add(md);          
            return md;

        }
        public void SwitchDocument(string modelTitle)
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
            this.currentDocument.ElementCount += 1;
            if (ct is MoveDtControl)
            {
                MoveDtControl dt = (ct as MoveDtControl);
                ModelElement e = ModelElement.CreateDataSourceElement(dt, dt.MDCName, dt.GetBcpPath(), this.currentDocument.ElementCount);
                this.currentDocument.AddModelElement(e);
                return;
            }

            if (ct is MoveOpControl)
            {
                MoveOpControl op = (ct as MoveOpControl);
                ModelElement e = ModelElement.CreateOperatorElement(op, op.ReName, ElementStatus.Null, SEType(op.SubTypeName), this.currentDocument.ElementCount);
                this.currentDocument.AddModelElement(e);
                return;
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
        public List<ModelElement> DeleteCurrentDocument()
        {
            if (this.currentDocument == null)
                throw new NullReferenceException();
            List<ModelElement> modelElements = this.currentDocument.ModelElements();
            this.ModelDocuments.Remove(this.currentDocument);
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
            this.currentDocument.AddModelElement(remarkElement);
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

        public bool WithoutDocumentLogin(string userName)
        {
            //新用户登录
            string userDir = Directory.GetCurrentDirectory() + "\\cittaModelDocument\\" + userName;
            if (!Directory.Exists(userDir))
                return true;
            //非新用户但无模型文档
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\cittaModelDocument\\" + userName);
            DirectoryInfo[] directoryInfos = di.GetDirectories();
            return (directoryInfos.Length == 0); 
        }
        public void SaveEndDocuments(string userName)
        {
           
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(UserInfoPath);
            var node = xDoc.SelectSingleNode("login");
            XmlNodeList bodyNodes = xDoc.GetElementsByTagName("user");
            foreach (XmlNode xn in bodyNodes)
            {
                if (xn.SelectSingleNode("name")!=null && xn.SelectSingleNode("name").InnerText == userName)
                {
                    XmlNodeList childNodes = xn.SelectNodes("modeltitle");
                    foreach (XmlNode xmlNode in childNodes)
                        xn.RemoveChild(xmlNode);
                    foreach (ModelDocument mb in this.modelDocuments)
                    {
                        XmlElement childElement = xDoc.CreateElement("modeltitle");
                        childElement.InnerText = mb.ModelDocumentTitle;
                        xn.AppendChild(childElement);                     
                    }
                    xDoc.Save(UserInfoPath);
                    return;
                }
            }
            

        }
        public string[] LoadSaveModelTitle(string userName)
        {
            string[] modelTitles;
            List<string> modelTitleList = new List<string>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(UserInfoPath);
            XmlNodeList userNode = xDoc.GetElementsByTagName("user");
            foreach (XmlNode xn in userNode)
            {
                if (xn.SelectSingleNode("name") != null && xn.SelectSingleNode("name").InnerText == userName)
                { 
                    XmlNodeList childNodes = xn.SelectNodes("modeltitle");
                    if (childNodes.Count > 0)
                    {
                        foreach (XmlNode xn2 in childNodes)
                            modelTitleList.Add(xn2.InnerText);
                        modelTitles = modelTitleList.ToArray();
                        return modelTitles;
                    }                   
                }                   
            }                       
            modelTitles = LoadAllModelTitle(userName);
            return modelTitles;
        }
        public string[] LoadAllModelTitle(string userName)
        {
            string[] modelTitles;
            DirectoryInfo userDir = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\cittaModelDocument\\" + userName);
            DirectoryInfo[] dir = userDir.GetDirectories();
            modelTitles = Array.ConvertAll(dir, value => Convert.ToString(value));
            return modelTitles;
        }
    }
}
