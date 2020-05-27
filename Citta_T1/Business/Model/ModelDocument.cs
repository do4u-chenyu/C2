using Citta_T1.Business.Model.World;
using Citta_T1.Business.Schedule;
using Citta_T1.Controls.Interface;
using Citta_T1.Controls.Move;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Citta_T1.Business.Model
{
    /*
     * 一个文档对应一个模型
     */
    class ModelDocument
    {
        private string userName;//用户名
        private string modelTitle;

        private List<ModelElement> modelElements;     
        private List<ModelRelation> modelRelations;
        private Dictionary<int, List<int>> modelGraphDict;  // 当前模型,以ID为key的图描述
        private string remarkDescription;  // 备注描述信息
        private bool remarkVisible;        // 备注控件是否可见

        private string savePath;
        private bool dirty;//字段表示模型是否被修改

        private int elementCount = 0;
        
        private WorldMap WorldMap;


        private TaskManager taskManager;
        private string userPath;

        /*
         * 传入参数为模型文档名称，当前用户名
         */
        public string ModelTitle {get => this.modelTitle;}
        public bool Dirty { get => dirty; set => dirty = value; }

        public int ElementCount { get => this.elementCount; set => this.elementCount = value; }
        public string SavePath { get => savePath; set => savePath = value; }
        public List<ModelRelation> ModelRelations { get => this.modelRelations; set => this.modelRelations = value; }
        public List<ModelElement> ModelElements { get => this.modelElements; set => this.modelElements = value; }


        
        public string RemarkDescription { get => remarkDescription; set => remarkDescription = value; }
        public TaskManager TaskManager { get => taskManager; set => taskManager = value; }


        public string UserPath { get => userPath; set => userPath = value; }
        public bool RemarkVisible { get => remarkVisible; set => remarkVisible = value; }

        public Dictionary<int, List<int>> ModelGraphDict { get => modelGraphDict; set => modelGraphDict = value; }
        
        public WorldMap WorldMap1 { get => WorldMap; set => WorldMap = value; }
        private static LogUtil log = LogUtil.GetInstance("ModelDocument");

        

        public ModelDocument(string modelTitle, string userName)
        {
            this.modelTitle = modelTitle;
            this.userName = userName;
            this.modelElements = new List<ModelElement>();
            this.modelRelations = new List<ModelRelation>();
            this.modelGraphDict = new Dictionary<int, List<int>>();
            this.remarkDescription = "";
            this.remarkVisible = false;
            this.userPath = Path.Combine(Global.WorkspaceDirectory, userName);
            this.savePath = Path.Combine(this.userPath, modelTitle);

            this.taskManager = new TaskManager();
            this.WorldMap = new WorldMap();
        }
        /*
         * 保存功能
         */
        public void Save()
        {
            DocumentSaveLoad dSaveLoad = new DocumentSaveLoad(this);
            dSaveLoad.WriteXml();
    
        }
        public void AddModelElement(ModelElement modelElement)
        {
            this.modelElements.Add(modelElement);
        }
        public void RemoveModelElement(ModelElement modelElement)
        {
            this.modelElements.Remove(modelElement);
        }
        public void AddModelRelation(ModelRelation mr, bool setDirty = true)
        {
            this.modelRelations.Add(mr);
            this.AddEdge(mr);
            if (setDirty)
                Global.GetMainForm().SetDocumentDirty();
        }
        public void RemoveModelRelation(ModelRelation mr)
        {
            this.modelRelations.Remove(mr);
            this.RemoveEdge(mr);
        }
        private void AddEdge(ModelRelation mr)
        {
            if (!this.modelGraphDict.ContainsKey(mr.StartID))
                this.modelGraphDict[mr.StartID] = new List<int>() { mr.EndID };
            else
                this.modelGraphDict[mr.StartID].Add(mr.EndID);
        }
        private void RemoveEdge(ModelRelation mr)
        {
            if (this.modelGraphDict.ContainsKey(mr.StartID))
                this.modelGraphDict[mr.StartID].Remove(mr.EndID);
        }

        public void DeleteModelElement(Control control)
        {
            this.ModelElements.Remove(this.ModelElements.Find(me => me.GetControl == control));
        }

        public void DeleteModelElement(ModelElement me)
        {
            this.ModelElements.Remove(me);
        }

        public void StatusChangeWhenDeleteControl(int ID)
        {
            foreach (ModelRelation mr in this.ModelRelations)
            {
                if (mr.StartID != ID) continue;
                foreach (ModelElement me in this.ModelElements)
                {
                    if (me.ID != mr.EndID) continue; 
                    me.Status = ElementStatus.Null;
                    (me.GetControl as MoveOpControl).EnableOption = false;
                    //存在链路，后续链路中算子状态变化
                    DegradeChildrenStatus(me.ID);

                }
            }
        }
        public void StatusChangeWhenDeleteLine(int ID)
        {

            foreach (ModelElement me in this.ModelElements)
            {
                if (me.ID != ID) continue;
                me.Status = ElementStatus.Null;
                (me.GetControl as MoveOpControl).EnableOption = false;
                //存在链路，后续链路中算子状态变化
                DegradeChildrenStatus(me.ID);
            }
        }
        public void DegradeChildrenStatus(int operatorID)
        {
            foreach (ModelRelation mr in this.ModelRelations)
            {
                if (mr.StartID != operatorID) continue;
                foreach (ModelElement me in this.ModelElements)
                {
                    if (me.ID != mr.EndID) continue;
                    DegradeStatus(me);  // 算子状态降级
                    DegradeChildrenStatus(mr.EndID);
                }
            }
        }
        // 算子状态降级规则:
        // Op算子  : Done状态降为Ready; Ready状态保持不变
        // 其他算子: 全部降为Null状态
        private void DegradeStatus(ModelElement me)
        {
            switch (me.Status)
            {
                case ElementStatus.Done:
                case ElementStatus.Ready:
                    me.Status = me.Type == ElementType.Operator ? ElementStatus.Ready : ElementStatus.Null;
                    break;
                case ElementStatus.Null:
                case ElementStatus.Runnnig:
                case ElementStatus.Stop:
                case ElementStatus.Suspend:
                default:
                    me.Status = ElementStatus.Null;
                    break;        
            }
        }
        public void SetChildrenStatusNull(int ID)
        {
           
            foreach (ModelRelation mr in this.ModelRelations)
            {              
                if (mr.StartID != ID)  continue;
                foreach (ModelElement me in this.ModelElements)
                {

                    if (me.ID != mr.EndID) continue;
                    me.Status = ElementStatus.Null;
                    SetChildrenStatusNull(mr.EndID);

                }
            }
        }
       

        public void Load()
        {
            if (File.Exists(Path.Combine(savePath, modelTitle +".xml")))
            {
                DocumentSaveLoad dSaveLoad = new DocumentSaveLoad(this);
                dSaveLoad.ReadXml();        
            }
        }


        public void Show()
        {
            foreach (ModelElement el1 in this.modelElements)
            {
                el1.Show();
                (el1.GetControl as IMoveControl).ControlNoSelect();
            }
                
        }

        public void Hide()
        {
            foreach (ModelElement el1 in this.modelElements)
                el1.Hide();
        }

        public int ReCountDocumentMaxElementID()
        {
            if (this.modelElements.Count == 0)
                return 0;
            foreach (ModelElement me in this.modelElements)
            {
                if (me.ID > ElementCount)
                    ElementCount = me.ID;
            }
            ElementCount += 1;
            return ElementCount;
        }

        

       
        public void UpdateAllLines()
        {
            foreach(ModelRelation mr in this.ModelRelations)
            {
                try
                {
                    ModelElement sEle = SearchElementByID(mr.StartID);
                    ModelElement eEle = SearchElementByID(mr.EndID);
                    // 坐标更新
                    mr.StartP = (sEle.GetControl as IMoveControl).GetStartPinLoc(0);
                    mr.EndP = (eEle.GetControl as IMoveControl).GetEndPinLoc(mr.EndPin);
                    // 引脚更新
                    (sEle.GetControl as IMoveControl).OutPinInit("lineExit");
                    (eEle.GetControl as IMoveControl).rectInAdd(mr.EndPin);
                    mr.UpdatePoints();
                }
                catch (Exception ex)
                {
                    log.Error("ModelDocument UpdateAllLines 出错: " + ex.ToString());
                }
            }
        }
        
        public ModelElement SearchElementByID(int ID)
        {
            return this.modelElements.Find(me => me.ID == ID) ?? ModelElement.Empty;
        }
        // 寻找隶属于同一个二元算子的两个关系
        public List<ModelRelation> SearchBrotherRelations(ModelRelation modelRelation)
        {
            return this.modelRelations.FindAll(me => me.EndID == modelRelation.EndID);
        }
        public ModelElement SearchResultElement(int OpID)
        {
            // 找到OpID开头的Relation
            ModelRelation mr = this.ModelRelations.Find(c => c.StartID == OpID);
            if (mr == null)
                return ModelElement.Empty;
            return SearchElementByID(mr.EndID);
        }
      
        public bool IsDuplicatedRelation(ModelRelation mr)
        {   // 关系终结于同一个元素
            return this.ModelRelations.Exists(c => c.EndID == mr.EndID && c.EndPin == mr.EndPin);
        }
        //修改xml中所有RS的path, 用newPathPrefix替换oldPathPrefix
        public static bool ModifyRsPath(string xmlPath, string oldPathPrefix, string newPathPrefix) 
        {
            bool ret = true;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNodeList nodes = xmlDoc.GetElementsByTagName("ModelElement");
                foreach (XmlNode childNode in nodes)
                {
                    XmlNode pathNode = childNode.SelectSingleNode("path");
                    if (pathNode != null && !String.IsNullOrEmpty(pathNode.InnerText) && pathNode.InnerText.StartsWith(oldPathPrefix))
                        pathNode.InnerText = pathNode.InnerText.Replace(oldPathPrefix, newPathPrefix);

                }
                xmlDoc.Save(xmlPath);
            }
            catch (Exception e) 
            {
                log.Info("ModelDocument ModifyRSPath 出错: " + e.ToString());
                ret = false;
            }
            return ret;
        }
}
}
